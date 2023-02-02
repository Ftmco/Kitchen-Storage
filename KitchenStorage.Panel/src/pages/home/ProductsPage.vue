<template>
  <v-col cols="12">
    <v-card elevation="4" class="rounded-lg">
      <table-header
        title="محصولات"
        newTitle="محصول جدید"
        :newAction="newProduct"
        :reloadAction="loadProducts"
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
        :loading="inLoading"
        :headers="headers"
        :items="products"
        :search="search"
        no-data-text="نظری یافت نشد"
        loading-text="کمی صبر کنید..."
        no-results-text="موردی یافت نشد"
        hide-default-footer
      >
        <template v-slot:item.actions="{ item }">
          <v-row>
            <v-col>
              <v-btn
                block
                color="info"
                text
                :to="{
                  name: 'ProductSellers',
                  query: {
                    id: item.id,
                    name: item.name,
                  },
                }"
              >
                فروشندگان
                <v-icon>mdi-store</v-icon>
              </v-btn>
            </v-col>
            <v-col>
              <v-btn block color="warning" text @click="editProduct(item)">
                ویرایش
                <v-icon>mdi-pencil</v-icon>
              </v-btn>
            </v-col>
            <v-col>
              <v-btn block color="error" text @click="deleteProduct(item)">
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
import { deleteProduct, getProducts } from "@/api/apis/product.api";
import TableHeader from "@/components/core/TableHeader.vue";
import { ConfirmDialog, Dialog, TableHeaderModel } from "@/components/models";
import { DIALOG, SNACKBAR } from "@/store/store_types";
import Vue from "vue";
import { mapMutations, mapState } from "vuex";
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
        text: "کد",
        value: "code",
        align: "start",
        sortable: true,
      },
      {
        text: "تعداد فروشندگان",
        value: "storesCount",
        align: "center",
        sortable: true,
      },
      {
        text: "تاریخ ایجاد",
        value: "createDate",
        align: "ceneter",
        sortable: true,
      },
      {
        text: " ",
        value: "actions",
        align: "ceneter",
        sortable: false,
      },
    ] as Array<TableHeaderModel>,
    products: [] as Array<any>,
    pageCount: 1,
    page: 1,
  }),
  components: { TableHeader },
  computed: {
    ...mapState(DIALOG, {
      dialogResult: `dialogResult`,
      confirmDialogResult: `confirmDialogResult`,
    }),
  },
  watch: {
    dialogResult(result) {
      if (result != undefined && result.status) {
        this.findAndRemoveProduct(result.data.product.id);
        this.products.push(result.data.product);
      }
    },
    confirmDialogResult(result) {
      if (result.agree) this.deleteConfirm(result.data);
    },
  },
  mounted() {
    this.loadProducts();
  },
  methods: {
    ...mapMutations(DIALOG, ["showModal", "showConfirm"]),
    ...mapMutations(SNACKBAR, ["showSnackbar"]),
    loadProducts() {
      this.inLoading = true;
      getProducts(0, 20)
        .then((productsRes) => {
          if (productsRes.status) {
            this.products = productsRes.result.products;
            this.pageCount = productsRes.result.pageCount + 1;
          }
        })
        .finally(() => (this.inLoading = false));
    },
    newProduct() {
      const create: Dialog = {
        color: "primary",
        title: "افزودن محصول جدید",
        content: {
          component: () => import("@/components/product/Upsert.vue"),
          props: {},
        },
      };
      this.showModal(create);
    },
    editProduct(item: any) {
      const update: Dialog = {
        color: "warning",
        title: `ویرایش ${item.name}`,
        content: {
          component: () => import("@/components/product/Upsert.vue"),
          props: {
            product: item,
          },
        },
      };
      this.showModal(update);
    },
    deleteProduct(item: any) {
      const confirm: ConfirmDialog = {
        agreeText: "حذف",
        disagreeText: "لغو",
        data: item,
        text: `آیا از حذف '${item.name}' مطمئین هستید؟`,
        title: "حذف محصول",
      };
      this.showConfirm(confirm);
    },
    deleteConfirm(data: any) {
      deleteProduct(data.id).then((deleteRes) => {
        if (deleteRes) this.findAndRemoveProduct(data.id);
        this.showSnackbar(deleteRes.title);
      });
    },
    findAndRemoveProduct(id: String) {
      let index = this.products.findIndex((p) => p.id == id);
      if (index != -1) this.products.splice(index, 1);
    },
  },
});
</script>