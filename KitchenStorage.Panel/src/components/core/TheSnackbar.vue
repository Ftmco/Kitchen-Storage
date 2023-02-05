<template>
  <v-snackbar :timeout="timeout" v-model="open" style="x-index: 500">
    {{ message }}

    <template v-slot:action="{ attrs }">
      <v-btn color="pink" text v-bind="attrs" @click="hideSnackbar">
        <v-icon>mdi-close</v-icon>
      </v-btn>
    </template>
  </v-snackbar>
</template>

<script lang="ts">
import { SNACKBAR } from "@/store/store_types";
import Vue from "vue";
import { mapMutations, mapState } from "vuex";

export default Vue.extend({
  computed: {
    ...mapState("snackBar", {
      message: `message`,
      open: `openSnackbar`,
    }),
  },
  methods: {
    ...mapMutations(SNACKBAR, ["hideSnackbar"]),
  },
  data: () => ({
    timeout: 1500,
  }),
  watch: {
    open() {
      setTimeout(() => {
        this.hideSnackbar();
      }, this.timeout);
    },
  },
});
</script>