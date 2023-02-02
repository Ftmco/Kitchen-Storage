<template>
  <v-card elevation="5">
    <v-col cols="12">
      <v-card-title>Profile</v-card-title>
      <v-list-item v-if="profile != null">
        <v-col align="left">
          <v-list-item-avatar size="100" color="grey">
            <v-img :src="imgSrc" :lazy-src="imgSrc" />
          </v-list-item-avatar>
          <v-file-input
            accept="image/*"
            show-size
            chips
            v-if="inEdit"
            label="Profile Image"
            placeholder="Profile Image"
            @change="selectProfile"
          ></v-file-input>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title class="text-h6">
                {{ profile.User.FullName }}
              </v-list-item-title>
              <v-list-item-subtitle>{{
                profile.User.Email || profile.User.MobileNo
              }}</v-list-item-subtitle>
              <v-btn
                class="col-md-3"
                outlined
                :color="inEdit ? 'primary' : 'warning'"
                @click="edit"
              >
                {{ inEdit ? "Update Profile" : "Edit Profile" }}
                <v-icon>mdi-pencil</v-icon>
              </v-btn>
            </v-list-item-content>
          </v-list-item>
        </v-col>
      </v-list-item>
      <v-row>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="profile.User.UserName"
            :clearable="inEdit"
            outlined
            label="User Name"
            placeholder="User Name"
            :readonly="!inEdit"
        /></v-col>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="profile.User.Email"
            outlined
            label="E-main"
            placeholder="E-mail"
            readonly
        /></v-col>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="profile.User.MobileNo"
            outlined
            label="Mobile"
            placeholder="Mobile"
            readonly
        /></v-col>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="profile.User.FullName"
            outlined
            :clearable="inEdit"
            label="Full Name"
            placeholder="Full Name"
            :readonly="!inEdit"
        /></v-col>
      </v-row>
    </v-col>
  </v-card>
</template>

<script lang="ts">
import { convertToBase64File, createFileAddress } from "@/services/file";
import Vue from "vue";
import { getProfile } from "fteam.identity.package/src/Account/profile";
import { application } from "@/constants";
export default Vue.extend({
  data: () => ({
    profile: {
      Image: "",
      Json: "",
      User: {},
    },
    imgSrc: "",
    inEdit: false,
  }),
  created() {
    this.getProfile();
  },
  methods: {
    getProfile() {
      getProfile(application)
        .then((res: any) => {
          if (res.Status) {
            this.profile = res.Result;
            this.imgSrc = createFileAddress((this.profile as any).image.pop());
          }
          //////showMessage(this, res.Title);
        })
        .catch((e) => {
          ////showMessage(this, e.Title);
        });
    },
    selectProfile(e: any) {
      convertToBase64File(e).then((res: any) => {
        (this.profile as any).image = res;
        this.imgSrc = res.base64;
      });
    },  
    createFileAddress,
  },
});
</script>

<style>
@import "../../assets/style/account.css";
@import "../../assets/style/dragscroll.css";
</style>