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
    ...mapMutations(DIALOG, ["hideModal", "setDialogResult"]),
    setGroup() {
      if (this.updateGroup != null) {
        this.group = {
          id: this.updateGroup.id,
          name: this.updateGroup.name,
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
      }
    },
  },
});
</script>
