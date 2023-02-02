<template>
  <v-col cols="12">
    <v-card elevation="4" class="rounded-lg">
      <table-header
        title="کافی شاپ ها"
        newTitle="کافی شاپ جدید"
        :newAction="newCoffeeShop"
        :reloadAction="loadCoffeeShops"
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
      <br />
      <v-data-table
        :loading="inLoading"
        :headers="headers"
        :items="stores"
        :search="search"
        no-data-text="هیچ فروشگاهی یافت نشد"
        loading-text="کمی صبر کنید..."
        no-results-text="موردی یافت نشد"
        hide-default-footer
      >
        <template v-slot:item.status="{ item }">
          <v-chip :color="getStoreStatusObj(item.status).color">
            {{ getStoreStatusObj(item.status).title }}
          </v-chip>
        </template>
        <template v-slot:item.actions="{ item }">
          <v-row>
            <v-col>
              <v-btn block color="warning" text>
                ویرایش
                <v-icon>mdi-pencil</v-icon>
              </v-btn>
            </v-col>
            <v-col>
              <v-btn block color="error" text>
                حذف
                <v-icon>mdi-delete</v-icon>
              </v-btn>
            </v-col>
            <v-col>
              <v-btn block color="info" text @click="details(item)">
                جزئیات
                <v-icon>mdi-newspaper</v-icon>
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
import { stores } from "@/api/apis/coffeeShop.api";
import TableHeader from "@/components/core/TableHeader.vue";
import { Dialog, TableHeaderModel } from "@/components/models";
import { DIALOG } from "@/store/store_types";
import Vue from "vue";
import { mapMutations } from "vuex";
import { getStoreStatusObj } from "@/services/status";
import router from "@/router";
export default Vue.extend({
  data: () => ({
    search: "",
    inLoading: true,
    headers: [
      {
        text: "نام",
        value: "name",
        align: "start",
        sortable: true,
      },
      {
        text: "عنوان",
        value: "title",
        align: "start",
        sortable: true,
      },
      {
        text: "کد",
        value: "code",
        align: "start",
        sortable: true,
      },
      {
        text: "وضعیت",
        value: "status",
        align: "center",
        sortable: true,
      },
      {
        text: "تاریخ ایجاد",
        value: "createDate",
        align: "center",
        sortable: true,
      },
      {
        text: "",
        value: "actions",
        align: "center",
        sortable: false,
      },
    ] as Array<TableHeaderModel>,
    stores: [] as Array<any>,
    pageCount: 1,
    page: 1,
  }),
  components: { TableHeader },
  mounted() {
    this.loadCoffeeShops();
  },
  methods: {
    ...mapMutations(DIALOG, ["showModal"]),
    loadCoffeeShops() {
      this.inLoading = true;
      stores(0, 15)
        .then((storesRes) => {
          if (storesRes.status) {
            this.stores = storesRes.result.stores;
            this.pageCount = storesRes.result.pageCount + 1;
          }
          this.inLoading = false;
        })
        .finally(() => (this.inLoading = false));
    },
    newCoffeeShop() {
      const create: Dialog = {
        title: "کافی شاپ جدید",
        color: "primary",
        content: {
          component: () => import("@/components/coffeeshop/Upsert.vue"),
          props: {},
        },
      };
      this.showModal(create);
    },
    details(item: any) {
      router.push({
        name: "StoreDetail",
        query: { name: item.name, id: item.id },
      });
    },
    getStoreStatusObj,
  },
});
</script>