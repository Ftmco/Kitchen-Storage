<template>
  <v-col cols="12">
    <v-form ref="groupForm">
      <v-row>
        <v-col cols="12">
          <v-list-item-avatar size="150" color="grey" v-if="avatar.base64">
            <v-img :lazy-src="avatar.base64" :src="avatar.base64" />
          </v-list-item-avatar>
          <v-file-input
            :loading="uploadingAvatar"
            placeholder="تصویر"
            label="تصویر"
            show-size
            accept="image/*"
            counter-size-string
            dense
            class="rounded-lg"
            clearable
            outlined
            @change="choceAvatar"
          />
        </v-col>

        <v-col cols="12" md="6" sm="6">
          <v-text-field
            :rules="[rules.require]"
            v-model="group.name"
            outlined
            label="نام"
            placeholder="نام"
            clearable
            class="rounded-lg"
          />
        </v-col>

        <v-col cols="12" md="6" sm="6">
          <v-text-field
            :rules="[rules.require]"
            v-model="group.title"
            outlined
            label="عنوان"
            placeholder="عنوان"
            class="rounded-lg"
            clearable
          />
        </v-col>

        <v-col cols="12" md="6" sm="6">
          <v-text-field
            :rules="[rules.require]"
            v-model="group.code"
            outlined
            label="کد"
            placeholder="کد"
            class="rounded-lg"
            clearable
          />
        </v-col>

        <v-col cols="12" sm="6" md="6">
          <v-select
            v-model="group.parentId"
            outlined
            placeholder="گروه پدر"
            :items="groups"
            label="گروه پدر"
            item-text="title"
            item-value="id"
            clearable
            class="rounded-lg"
            no-data-text="موردی یافت نشد"
          ></v-select>
        </v-col>

        <v-col cols="12">
          <v-btn
            :loading="upserting"
            block
            :color="updateGroup ? 'warning' : 'primary'"
            @click="submitGroup"
            class="rounded-lg"
            elevation="5"
          >
            {{ updateGroup == null ? "ثبت گروه" : "ویرایش گروه" }}
          </v-btn>
        </v-col>
      </v-row>
    </v-form>
  </v-col>
</template>

<script lang="ts">
import Vue from "vue";
import { getGroups, upsertGroup } from "@/api/apis/group.apis";
import { rules } from "@/constants";
import { convertToBase64File } from "@/services/file";
import { uploadFile, getImage } from "@/api/apis/file.api";
import { servantPanel } from "@/constants/file.constants";
import { UpsertGroup } from "@/api/models/group.model";
import { mapMutations } from "vuex";
import { DIALOG, SNACKBAR } from "@/store/store_types";

export default Vue.extend({
  props: ["updateGroup"],
  data: () => ({
    groups: [],
    group: {} as UpsertGroup,
    rules: rules,
    avatar: {} as any,
    uploadingAvatar: false,
    upserting: false,
  }),
  beforeMount() {
    this.loadGroups();
  },
  mounted() {
    this.setGroup();
  },
  methods: {
    ...mapMutations(SNACKBAR, ["showSnackbar"]),
    ...mapMutations(DIALOG, ["hideModal", "setDialogResult",]),
    setGroup() {
      if (this.updateGroup != null) {
        this.group = {
          id: this.updateGroup.id,
          code: this.updateGroup.code,
          name: this.updateGroup.name,
          title: this.updateGroup.title,
          parentId: this.updateGroup.parentId,
          avatar: {
            fileId: this.updateGroup.fileId,
            fileToken: this.updateGroup.fileToken,
            type: 0,
          },
        };
      }
    },
    loadGroups() {
      getGroups(0, 0)
        .then((res) => {
          if (res.status) {
            this.groups = res.result.groups;
          }
          this.showSnackbar(res.title);
        })
        .catch((e) => {
          this.showSnackbar(e.message);
        });
    },
    choceAvatar(e: any) {
      if (e != null)
        convertToBase64File(e).then((res: any) => {
          this.avatar = res;
        });
      else this.avatar = "";
    },
    submitGroup() {
      const isValid = (this.$refs.groupForm as any).validate();
      if (isValid) {
        this.uploadingAvatar = true;
        this.upserting = true;
        uploadFile([
          {
            folderToken:
              "Folder-1d70a80b7ef8449faac14a4ac171d89d54d6e7e59e1697",
            base64: this.avatar.base64,
            type: this.avatar.type,
            fileName: this.avatar.ogName,
            extension: this.avatar.type.split('/')[1],
          },
        ])
          .then((upRes) => {
            if (upRes.status) {
              this.group.avatar = {
                fileId: upRes.result.id,
                fileToken: upRes.result.token,
                type: 0,
              };
              upsertGroup(this.group)
                .then((res) => {
                  if (res.status) {
                    this.setDialogResult({
                      status: res.status,
                      data: {
                        group: res.result.group,
                        new: this.updateGroup == null,
                      },
                    });
                  }
                  this.showSnackbar(res.title);
                })
                .catch((e) => {
                  this.showSnackbar(e.message);
                })
                .finally(() => {
                  this.hideModal();
                  this.uploadingAvatar = false;
                  this.upserting = false;
                });
            }
          })
          .finally(() => {
            this.uploadingAvatar = false;
            this.upserting = false;
          });
      }
    },
  },
});
</script>
