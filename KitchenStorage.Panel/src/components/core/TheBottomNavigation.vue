<template>
  <v-bottom-navigation
    :input-value="show"
    v-model="page"
    shift
    fixed
    grow
    color="primary"
  >
    <v-btn
      v-for="(item, i) in items"
      :value="item.title"
      :key="i"
      :to="item.to"
    >
      <span>{{ item.title }}</span>

      <v-icon>{{ item.icon }}</v-icon>
    </v-btn>
  </v-bottom-navigation>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  watch: {
    $route() {
      this.page = this.$route.path.toString();
    },
  },
  data: () => ({
    page: "/tabs/home",
    show: false,
    items: [] as Array<any>,
  }),
  mounted() {
    this.setShow();
  },
  methods: {
    setShow() {
      this.show = window.innerWidth <= 700;
    },
  },
});
</script>