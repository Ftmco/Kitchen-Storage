<template>
  <v-card class="mt-4 mx-auto" max-width="400">
    <v-sheet
      class="v-sheet--offset mx-auto"
      :color="color"
      elevation="12"
      max-width="calc(100% - 32px)"
      auto-draw
    >
      <v-sparkline
        :labels="labels"
        :value="value"
        color="white"
        line-width="2"
        padding="16"
      ></v-sparkline>
    </v-sheet>

    <v-card-text class="pt-0">
      <div class="text-h6 font-weight-light mb-2">{{ title }}</div>
      <div class="subheading font-weight-light grey--text">
        {{ subTitle }}
      </div>
      <v-divider class="my-2"></v-divider>
      <slot name="bottom"></slot>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import Vue from "vue";
export default Vue.extend({
  props: ["color", "title", "subTitle", "labels", "value"],
});
</script>

<style>
.v-sheet--offset {
  top: -24px;
  position: relative;
}
</style>