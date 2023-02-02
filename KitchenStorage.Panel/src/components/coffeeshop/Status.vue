<template>
  <v-col cols="12">
    <v-form ref="statusForm">
      <v-select
        outlined
        label="وضعیت"
        placeholder="وضعیت"
        clearable
        class="rounded-lg"
        v-model="currentStatus"
        :items="status"
        item-value="code"
        item-text="title"
      ></v-select>
      <v-textarea
        v-if="currentStatus == 1 || currentStatus == 4"
        outlined
        :rules="[rules.require]"
        label="توضیحات"
        v-model="description"
        :placeholder="
          currentStatus == 1 ? 'علت غیر فعال سازی' : 'علت رد درخواست ثبت'
        "
        clearable
        class="rounded-lg"
      />
    </v-form>
    <v-btn
      block
      class="rounded-lg"
      elevation="5"
      color="primary"
      :loading="inAction"
      @click="setStatus"
    >
      <v-icon> mdi-check </v-icon> ثبت وضعیت
    </v-btn>
  </v-col>
</template>

<script lang="ts">
import { changeStoreStatus } from "@/api/apis/coffeeShop.api";
import { upsertTicket } from "@/api/apis/ticket.api";
import { rules } from "@/constants";
import { DIALOG, SNACKBAR } from "@/store/store_types";
import Vue from "vue";
import { VCol, VForm, VSelect, VTextarea, VBtn, VIcon } from "vuetify/lib";
import { mapMutations } from "vuex";
import { DialogResult } from "../models";
export default Vue.extend({
  props: ["storeStatus", "id", "ownerId"],
  data: () => ({
    rules: rules,
    status: [
      {
        title: "فعال",
        code: 0,
      },
      {
        title: "غیر فعال",
        code: 1,
      },
      {
        title: "حذف شده",
        code: 2,
      },
      {
        title: "درخواست ثبت",
        code: 3,
      },
      {
        title: "رد درخواست ثبت",
        code: 4,
      },
    ],
    currentStatus: 0,
    inAction: false,
    description: "",
  }),
  mounted() {
    this.currentStatus = this.storeStatus;
  },
  methods: {
    ...mapMutations(DIALOG, ["hideModal", "setDialogResult"]),
    ...mapMutations(SNACKBAR, ["showSnackbar"]),
    setStatus() {
      let isValid = (this.$refs.statusForm as any).validate();
      if (isValid) {
        this.inAction = true;
        changeStoreStatus({
          id: this.id,
          status: this.currentStatus,
        })
          .then(async (statusRes) => {
            if (statusRes) {
              if (this.currentStatus == 1 || this.currentStatus == 4) {
                await upsertTicket({
                  id: null,
                  toUser: this.ownerId,
                  subject: "درخواست ثبت فروشگاه",
                  description: this.description,
                });
              }
              const result: DialogResult = {
                status: statusRes.status,
                data: {
                  status: this.currentStatus,
                },
              };
              this.setDialogResult(result);
              this.hideModal();
            }
            this.showSnackbar(statusRes.title);
          })
          .finally(() => (this.inAction = false));
      }
    },
  },
});
</script>