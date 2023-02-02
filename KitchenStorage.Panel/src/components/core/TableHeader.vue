<template>
  <v-card-title>
    {{ title }}
    <v-spacer></v-spacer>
    <slot name="search"></slot>
    <v-spacer></v-spacer>
    <v-btn color="primary" text @click="newAction">
      {{ newTitle }}
      <v-icon>mdi-plus</v-icon>
    </v-btn>
    <v-btn color="info" text @click="reloadAction">
      تازه سازی
      <v-icon>mdi-reload</v-icon>
    </v-btn>
  </v-card-title>
</template>

<script lang="ts">
import Vue from "vue";
export default Vue.extend({
  props: ["title", "newTitle", "newAction", "reloadAction"],
  data: () => ({
    search: "",
  }),
});
</script>