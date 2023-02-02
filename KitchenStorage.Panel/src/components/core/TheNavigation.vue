<template>
  <v-navigation-drawer app v-model="drawer" fixed temporary>
    <v-list nav dense>
      <v-list-item-group v-model="group" active-class="text--accent-4">
        <v-list-item :to="{ name: 'Settings' }" v-if="user.isAuthenticated">
          <v-col align="left">
            <v-list-item-avatar size="100" color="grey">
              <v-img
                v-if="user.profile.Image != null"
                :src="createFileAddress({ directory: '', name: '' })"
                :lazy-src="createFileAddress({ directory: '', name: '' })"
              />
            </v-list-item-avatar>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title class="text-h6">
                  {{ user.profile.User.FullName }}
                </v-list-item-title>
                <v-list-item-subtitle>{{
                  user.profile.User.Email || user.profile.User.MobileNo
                }}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-col>
        </v-list-item>

        <NavigationListItems :items="items" />
        <v-list-item @click="changeTheme">
          <v-list-item-icon>
            <v-icon>mdi-theme-light-dark</v-icon>
          </v-list-item-icon>
          <v-list-item-title>{{ theme }}</v-list-item-title>
        </v-list-item>
      </v-list-item-group>
    </v-list>

    <template v-slot:append>
      <div class="pa-2">
        <v-btn
          v-if="user.isAuthenticated"
          block
          color="error"
          elevation="10"
          @click="logOut"
        >
          خروج
          <v-icon>mdi-logout</v-icon>
        </v-btn>
        <v-btn v-else block color="info" elevation="10" @click="login">
          ورود
          <v-icon>mdi-login</v-icon>
        </v-btn>
      </div>
    </template>
  </v-navigation-drawer>
</template>


<script lang="ts">
import Vue from "vue";
import { application, navigationItems } from "@/constants/";
import Account from "@/services/account";
import { createFileAddress } from "@/services/file";
import { getProfile } from "fteam.identity.package/src/Account/profile";
import NavigationListItems from "./NavigationListItems.vue";
import { logout } from "@/api/apis/account.api";

export default Vue.extend({
  data: () => ({
    drawer: false,
    user: {
      isAuthenticated: Account.isAuthenticated(),
      profile: {
        Image: "",
        Json: "",
        User: {},
      },
    },
    items: navigationItems,
    group: null,
    theme: "روشن",
    active: 0,
  }),
  created() {
    this.$root.$refs.navigationDrawer = this;
    this.getUser();
  },
  methods: {
    open() {
      this.drawer = true;
    },
    getUser() {
      let currentUserAuthenticated = Account.isAuthenticated();
      if (currentUserAuthenticated) {
        getProfile(application).then((res: any) => {
          if (res.Status) {
            this.user.isAuthenticated = true;
            this.user.profile = res.Result;
          }
        });
      }
    },
    changeTheme() {
      this.$vuetify.theme.dark = !this.$vuetify.theme.dark;
      this.theme = this.$vuetify.theme.dark ? "تاریک" : "روشن";
      localStorage.setItem(
        "theme",
        this.$vuetify.theme.dark ? "Dark" : "Ligth"
      );
    },
    logOut() {
      logout().then((res) => {
        if (res.status) (window.location as any) = "/";
        //showMessage(this, res.title);
      });
    },
    login() {
      this.$router.push({ name: "Login" });
    },
    createFileAddress,
  },
  components: { NavigationListItems },
});
</script>