<template>
  <v-card class="rounded-lg" elevation="5">
    <v-col>
      <v-list-item v-if="profile != null">
        <v-col align="center">
          <v-list-item-avatar size="150" color="grey">
            <div class="text-center" v-if="avatar.loading">
              <v-progress-circular
                indeterminate
                color="primary"
              ></v-progress-circular>
            </div>
            <v-img
              v-if="!avatar.loading"
              :src="avatar.base64"
              :lazy-src="avatar.base64"
            />
          </v-list-item-avatar>
        </v-col>
      </v-list-item>
      <v-row>
        <v-col cols="12">
          <v-file-input
            outlined
            label="تصویر"
            placeholder="تصویر پروفایل"
            clearable
            @change="imageSelected"
          />
        </v-col>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="profile.firstName"
            outlined
            placeholder="نام"
            label="نام"
            clearable
          />
        </v-col>
        <v-col cols="12" md="6">
          <v-text-field
            outlined
            v-model="profile.lastName"
            placeholder="نام خانوادگی"
            label="نام خانوادگی"
            clearable
          />
        </v-col>
        <v-col cols="12">
          <v-text-field
            v-model="profile.user.email"
            outlined
            placeholder="ایمیل"
            label="ایمیل"
            clearable
          >
            <template v-slot:append-outer>
              <v-btn color="primary darken-2" elevation="5" class="rounded-lg">
                تایید ایمیل
                <v-icon>mdi-account-check</v-icon>
              </v-btn>
            </template>
          </v-text-field>
        </v-col>
      </v-row>
      <v-btn color="primary" block elevation="5" class="rounded-lg">
        ویرایش
      </v-btn>
    </v-col>
  </v-card>
</template>

<script lang="ts">
import { convertToBase64File } from "@/services/file";
import { USER } from "@/store/store_types";
import Vue from "vue";
import { mapMutations, mapState } from "vuex";
export default Vue.extend({
  props: ["userId"],
  computed: {
    ...mapState(USER, {
      profile: `profile`,
      avatar: `avatar`,
    }),
  },
  methods: {
    ...mapMutations(USER, ["setProfileAvatar"]),
    imageSelected(file: any) {
      if (file != null) {
        convertToBase64File(file).then((image:any) => {
          this.setProfileAvatar({
            fileId: undefined,
            filetoken: undefined,
            base64: image.base64,
            loading: false,
          });
        });
      } else
        this.setProfileAvatar({
          fileId: undefined,
          filetoken: undefined,
          base64: "",
          loading: true,
        });
    },
  },
});
</script>