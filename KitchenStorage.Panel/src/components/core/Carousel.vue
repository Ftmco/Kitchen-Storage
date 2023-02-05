<template>
  <v-card elevation="5" height="350" color="gray">
    <slider
      style="height: 100%; z-index: 0"
      :stopOnHover="true"
      :speed="1000"
      :interval="3500"
      :controlBtn="true"
    >
      <slider-item v-for="(item, index) in items" :key="index">
        <v-img
          class="white--text align-end"
          width="100%"
          height="100%"
          :src="item.image"
          :lazy-src="item.image"
        >
          <v-card-title>{{ item.text }}</v-card-title>
        </v-img>
      </slider-item>
    </slider>
  </v-card>
</template>

<script lang="ts">
import Vue from "vue";
import { Slider, SliderItem } from "vue-easy-slider";
export default Vue.extend({
  props: ["items"],
  components: {
    Slider,
    SliderItem,
  },
});
</script>