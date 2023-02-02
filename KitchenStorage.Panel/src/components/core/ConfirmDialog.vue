<template>
  <v-row justify="center">
    <v-dialog v-model="visible" persistent max-width="290">
      <v-card>
        <v-card-title class="text-h5">
          {{ dialog.title }}
        </v-card-title>
        <v-card-text>{{ dialog.text }}</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="red" text @click="action(false)">
            {{ dialog.disagreeText }}
          </v-btn>
          <v-btn color="green darken-1" text @click="action(true)">
            {{ dialog.agreeText }}
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script lang="ts">
import { DIALOG } from "@/store/store_types";
import Vue from "vue";
import { mapMutations, mapState } from "vuex";

export default Vue.extend({
  computed: {
    ...mapState(DIALOG, {
      visible: `confirmVisible`,
      dialog: `confirmDialog`,
    }),
  },
  methods: {
    ...mapMutations(DIALOG, ["confirmResponse", "hideConfirm"]),
    action(agree: boolean) {
      this.confirmResponse({
        agree: agree,
        data: this.dialog.data,
      });
      this.hideConfirm();
    },
  },
});
</script>