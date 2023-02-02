<template>
  <v-list>
    <div v-for="item in items" :key="item.id">
      <v-list-item v-if="item.children == undefined" :to="item.to">
        <v-list-item-icon>
          <v-icon>{{ item.icon }}</v-icon>
        </v-list-item-icon>
        <v-list-item-title>{{ item.title }}</v-list-item-title>
      </v-list-item>
      <v-list-group
        v-else
        :value="item.active"
        no-action        
        :prepend-icon="item.icon"
      >
        <template v-slot:activator>
          <v-list-item-content>
            <v-list-item-title v-text="item.title"></v-list-item-title>
          </v-list-item-content>
        </template>

        <v-list-item
          v-for="child in item.children"
          :to="child.to"
          :key="child.id"
        >
          <v-list-item-icon>
            <v-icon>{{ child.icon }}</v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title v-text="child.title"></v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list-group>
    </div>
  </v-list>
</template>

<script lang="ts">
import Vue from "vue";
export default Vue.extend({
  props: ["items"],
});
</script>