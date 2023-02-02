<template>
  <v-col>
    <v-dialog
      transition="dialog-top-transition"
      v-model="visible"
      :fullscreen="fullscreen"
      max-width="800px"
      persistent
    >
      <v-card v-if="visible">
        <v-toolbar elevation="4" :color="dialog.color" dark>
          {{ dialog.title }}
          <v-spacer></v-spacer>
          <v-btn icon @click="hideModal">
            <v-icon>mdi-close</v-icon>
          </v-btn>
        </v-toolbar>
        <component :is="component" v-bind="props" />
      </v-card>
    </v-dialog>
  </v-col>
</template>

<script lang="ts">
import { DIALOG } from "@/store/store_types";
import Vue from "vue";
import { mapMutations, mapState } from "vuex";

export default Vue.extend({
  data: () => ({
    component: {} as any,
    props: {},
    fullscreen: window.screen.width <= 790,
  }),
  computed: {
    ...mapState(DIALOG, {
      visible: `modalVisible`,
      dialog: `dialog`,
    }),
  },
  methods: {
    ...mapMutations(DIALOG, ["hideModal"]),
  },
  watch: {
    dialog(dialog) {
      this.component = dialog.content.component;
      this.props = dialog.content.props;
    },
  },
});
</script>