<template>
  <v-app>
    <router-view />
  </v-app>
</template>


 <script lang="ts">
import Vue from "vue";
import { themeDetect } from "@/services/mediaQuery";

export default Vue.extend({
  name: "App",
  mounted() {
    themeDetect().then((res: any) => {
      const theme =
        localStorage.getItem("theme")?.toString() ||
        (res.dark ? "Dark" : "Light").toString();
      this.$vuetify.theme.dark = theme == "Dark";
    });
  },
});
</script>

<style>
::-webkit-scrollbar {
  display: none;
}
</style>
