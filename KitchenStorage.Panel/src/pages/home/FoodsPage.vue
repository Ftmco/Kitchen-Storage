<template>
  <v-col cols="12">
    <v-card elevation="4" class="rounded-lg">
      <table-header
        title="غذا ها"
        newTitle="گروه جدید"
        :newAction="() => {}"
        :reloadAction="() => {}"
      >
        <template v-slot:search>
          <v-text-field
            v-model="search"
            append-icon="mdi-magnify"
            label="جستجو"
            single-line
            hide-details
          ></v-text-field>
        </template>
      </table-header>
    </v-card>
    <br>
    <v-card elevation="4" class="rounded-lg">
      <v-data-table
        :loading="isLoading"
        :headers="headers"
        :items="foods"
        :search="search"
        no-data-text="نظری یافت نشد"
        loading-text="کمی صبر کنید..."
        no-results-text="موردی یافت نشد"
        hide-default-footer
      >
        <template v-slot:item.actions="{ item }">
          <v-row>
            <v-col>
              <v-btn block color="warning" text >
                ویرایش
                <v-icon>mdi-pencil</v-icon>
              </v-btn>
            </v-col>
            <v-col>
              <v-btn block color="error" text >
                حذف
                <v-icon>mdi-delete</v-icon>
              </v-btn>
            </v-col>
          </v-row>
        </template>
      </v-data-table>
      <div class="text-center">
        <v-pagination v-model="page" :length="pageCount"></v-pagination>
      </div>
    </v-card>
  </v-col>
</template>

<script lang="ts">
import TableHeader from "@/components/core/TableHeader.vue";
import { TableHeaderModel } from "@/components/models";
import Vue from "vue";
export default Vue.extend({
  components: { TableHeader },
  data: () => ({
    search: "",
    isLoading: true,
    headers: [] as Array<TableHeaderModel>,
    foods: [],
    page:1,
    pageCount:1
  }),
});
</script>