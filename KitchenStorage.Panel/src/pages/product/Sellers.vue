<template>
  <v-col cols="12">
    <v-card elevation="4" class="rounded-lg">
      <table-header
        :title="`فروشندگان ${product.name}`"
        :reloadAction="getStores"
        :newAction="addStore"
        newTitle="افزودن فروشگاه"
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
      <v-data-table
        :loading="isLoading"
        :headers="headers"
        :items="stores"
        :search="search"
        no-data-text="فروشنده ای یافت نشد"
        loading-text="کمی صبر کنید..."
        no-results-text="موردی یافت نشد"
        hide-default-footer
      >
        <template v-slot:item.storeStatus="{ item }">
          <v-chip :color="getStoreStatusObj(item.storeStatus).color">
            {{ getStoreStatusObj(item.storeStatus).title }}
          </v-chip>
        </template>
        <template v-slot:item.productStatus="{ item }">
          <v-chip :color="getBaseStatusObj(item.productStatus).color">
            {{ getBaseStatusObj(item.productStatus).title }}
          </v-chip>
        </template>
        <template v-slot:item.actions="{ item }">
          <v-row>
            <v-col>
              <v-btn block color="error" text>
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
import Vue from "vue";
import TableHeader from "@/components/core/TableHeader.vue";
import { Dialog, TableHeaderModel } from "@/components/models";
import { Route } from "vue-router";
import { getSellers } from "@/api/apis/product.api";
import { getStoreStatusObj, getBaseStatusObj } from "@/services/status";
import { mapMutations } from "vuex";
import { DIALOG } from "@/store/store_types";
export default Vue.extend({
  components: {
    TableHeader,
  },
  data: () => ({
    product: {
      id: "",
      name: "",
    },
    stores: [],
    search: "",
    pageCount: 0,
    page: 1,
    headers: [
      {
        text: "نام فروشگاه",
        align: "start",
        sortable: false,
        value: "storeName",
      },
      {
        text: "کد فروشگاه",
        align: "start",
        sortable: true,
        value: "storeCode",
      },
      {
        text: "وضعیت فروشگاه",
        align: "center",
        sortable: true,
        value: "storeStatus",
      },
      {
        text: "نام کالا",
        align: "start",
        sortable: true,
        value: "productName",
      },
      {
        text: "عنوان کالا",
        align: "start",
        sortable: false,
        value: "productTitle",
      },
      {
        text: "کد کالا",
        align: "start",
        sortable: true,
        value: "productCode",
      },
      {
        text: "وضعیت فروش کالا",
        align: "center",
        sortable: true,
        value: "productStatus",
      },
      {
        text: "قیمت (تومان)",
        align: "center",
        sortable: true,
        value: "amount",
      },
      {
        text: " ",
        align: "center",
        sortable: false,
        value: "actions",
      },
    ] as Array<TableHeaderModel>,
    isLoading: true,
  }),
  mounted() {
    this.setQueryParams(this.$route);
    this.getStores();
  },
  watch: {
    $route(route) {
      this.setQueryParams(route);
    },
  },
  methods: {
    ...mapMutations(DIALOG, ["showModal"]),
    setQueryParams(route: Route) {
      this.product = {
        id: route.query.id.toString(),
        name: route.query.name.toString(),
      };
    },
    getStores() {
      this.isLoading = true;
      getSellers(this.product.id, 0, 20)
        .then((sellers) => {
          if (sellers.status) {
            this.stores = sellers.result.sellers;
            this.pageCount = sellers.result.pageCount + 1;
          }
        })
        .finally(() => (this.isLoading = false));
    },
    addStore() {
      const addSeller: Dialog = {
        color: "primary",
        title: "افزودن فروشنده",
        content: {
          component: () => import("@/components/product/AddSeller.vue"),
          props: { productId: this.product.id },
        },
      };
      this.showModal(addSeller);
    },
    removeStore(item: any) {},
    getStoreStatusObj,
    getBaseStatusObj,
  },
});
</script>