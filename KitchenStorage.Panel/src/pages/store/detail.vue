<template>
  <v-col cols="12">
    <v-row>
      <v-col cols="6" sm="6" md="3">
        <v-card
          class="mx-auto text-center rounded-lg"
          color="primary darken-4"
          outlined
          elevation="5"
          style="padding-top: 30px"
        >
          <v-icon size="50">mdi-coffee</v-icon>

          <v-card-title> محصولات </v-card-title>
          <v-card-subtitle> {{ details.productsCount }} محصول</v-card-subtitle>
        </v-card>
      </v-col>
      <v-col cols="6" sm="6" md="3">
        <v-card
          class="mx-auto text-center rounded-lg"
          color="primary darken-3"
          outlined
          elevation="5"
          style="padding-top: 30px"
        >
          <v-icon size="50">mdi-account-group</v-icon>

          <v-card-title> مشتری ها </v-card-title>
          <v-card-subtitle> 120 مشتری</v-card-subtitle>
        </v-card>
      </v-col>
      <v-col cols="6" sm="6" md="3">
        <v-card
          class="mx-auto text-center rounded-lg"
          color="primary darken-2"
          outlined
          elevation="5"
          style="padding-top: 30px"
        >
          <v-icon size="50">mdi-table-chair</v-icon>

          <v-card-title> میزها </v-card-title>
          <v-card-subtitle> {{ details.tablesCount }} میز</v-card-subtitle>
        </v-card>
      </v-col>
      <v-col cols="6" sm="6" md="3">
        <v-card
          class="mx-auto text-center rounded-lg"
          color="primary darken-1"
          outlined
          elevation="5"
          style="padding-top: 30px"
        >
          <v-icon size="50">mdi-shopping</v-icon>

          <v-card-title> سفارشات </v-card-title>
          <v-card-subtitle>
            {{ details.currentOrdersCount }} سفارش</v-card-subtitle
          >
        </v-card>
      </v-col>
      <v-col cols="6" sm="6" md="3">
        <v-card
          class="mx-auto text-center rounded-lg"
          color="primary darken-4"
          outlined
          elevation="5"
          style="padding-top: 30px"
        >
          <v-icon size="50">mdi-wallet</v-icon>

          <v-card-title> مالی </v-card-title>
          <v-card-subtitle>حساب رسی</v-card-subtitle>
        </v-card>
      </v-col>
      <v-col cols="6" sm="6" md="3">
        <v-card
          class="mx-auto text-center rounded-lg"
          color="primary darken-3"
          outlined
          elevation="5"
          style="padding-top: 30px"
        >
          <v-icon size="50">mdi-ticket-account</v-icon>

          <v-card-title> تیکت ها </v-card-title>
          <v-card-subtitle>12 تیکت</v-card-subtitle>
        </v-card>
      </v-col>
      <v-col cols="6" sm="6" md="3">
        <v-card
          class="mx-auto text-center rounded-lg"
          color="primary darken-2"
          outlined
          elevation="5"
          style="padding-top: 30px"
        >
          <v-icon size="50">mdi-cog</v-icon>

          <v-card-title> تنظیمات </v-card-title>
          <v-card-subtitle> تنظیمات </v-card-subtitle>
        </v-card>
      </v-col>
      <v-col cols="6" sm="6" md="3">
        <v-card
          @click="changeStatus"
          class="mx-auto text-center rounded-lg"
          color="primary darken-1"
          outlined
          elevation="5"
          style="padding-top: 30px"
        >
          <v-icon size="50">mdi-list-status</v-icon>

          <v-card-title> وضعیت </v-card-title>
          <v-card-subtitle>فعال</v-card-subtitle>
        </v-card>
      </v-col>
    </v-row>
  </v-col>
</template>

<script lang="ts">
import { storeDetails } from "@/api/apis/coffeeShop.api";
import { Dialog } from "@/components/models";
import { DIALOG, SNACKBAR } from "@/store/store_types";
import Vue from "vue";
import { Route } from "vue-router";
import { mapMutations } from "vuex";
export default Vue.extend({
  data: () => ({
    store: {
      name: "",
      id: "",
    },
    details: {} as any,
  }),
  mounted() {
    this.setStore(this.$route);
    this.getDetails();
  },
  watch: {
    $route(route) {
      this.setStore(route);
    },
  },
  methods: {
    ...mapMutations(DIALOG, ["showModal"]),
    ...mapMutations(SNACKBAR, ["showSnackbar"]),
    setStore(route: Route) {
      this.store = {
        name: route.query.name.toString(),
        id: route.query.id.toString(),
      };
    },
    getDetails() {
      storeDetails(this.store.id).then((detailsRes) => {
        if (detailsRes.status) {
          this.details = detailsRes.result.details;
        }
        this.showSnackbar(detailsRes.title);
      });
    },
    changeStatus() {
      const statusDialog: Dialog = {
        color: "primary",
        title: "وضعیت فروشگاه",
        content: {
          component: () => import("@/components/coffeeshop/Status.vue"),
          props: {
            id: this.details.id,
            ownerId: this.details.ownerId,
            storeStatus: this.details.status,
          },
        },
      };
      this.showModal(statusDialog);
    },
  },
});
</script>