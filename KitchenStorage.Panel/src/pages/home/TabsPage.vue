<template>
  <v-main>
    <the-loading />
    <the-snackbar />
    <app-dialog />
    <confirm-dialog />
    
    <app-bar />
    <router-view />

    <!-- <the-bottom-navigation /> -->
   
  </v-main>
</template>


<script lang="ts">
import AppBar from "@/components/core/AppBar.vue";
import AppDialog from "@/components/core/AppDialog.vue";
import ConfirmDialog from "@/components/core/ConfirmDialog.vue";
// import TheBottomNavigation from "@/components/core/TheBottomNavigation.vue";
import TheLoading from "@/components/core/TheLoading.vue";
import TheSnackbar from "@/components/core/TheSnackbar.vue";
import { defineComponent } from "@vue/composition-api";

export default defineComponent({
  components: {
    AppBar,
    TheLoading,
    TheSnackbar,
    AppDialog,
    ConfirmDialog,
  },
});
</script>
