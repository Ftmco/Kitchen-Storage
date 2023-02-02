<template>
  <v-col cols="12">
    <v-form ref="upsertProductForm">
      <v-row>
        <v-col cols="12" sm="6" md="6">
          <v-text-field
            outlined
            class="rounded-lg"
            label="نام"
            placeholder="نام"
            :rules="[rules.require]"
            v-model="upsert.name"
          />
        </v-col>
        <v-col cols="12" sm="6" md="6">
          <v-select
            v-model="upsert.groups"
            multiple
            outlined
            placeholder="گروه ها"
            :items="groups"
            label="گروه ها"
            item-text="title"
            item-value="id"
            clearable
            class="rounded-lg"
            no-data-text="موردی یافت نشد"
          ></v-select>
        </v-col>
        <v-col cols="12">
          <v-btn
            class="rounded-lg"
            block
            elevation="5"
            :color="isUpdate ? 'warning' : 'primary'"
            @click="upsertProduct"
            :loading="inAction"
          >
            <v-icon>{{ isUpdate ? "mdi-pencil" : "mdi-check" }}</v-icon>
            ثبت محصول
          </v-btn>
        </v-col>
      </v-row>
    </v-form>
  </v-col>
</template>

<script lang="ts">
import { getGroups } from "@/api/apis/group.apis";
import { upsertProduct } from "@/api/apis/product.api";
import { UpsertProduct } from "@/api/models/product.model";
import { rules } from "@/constants";
import { DIALOG, SNACKBAR } from "@/store/store_types";
import Vue from "vue";
import { mapMutations } from "vuex";
export default Vue.extend({
  props: ["product"],
  data: () => ({
    upsert: {} as UpsertProduct,
    rules: rules,
    isUpdate: false,
    groups: [] as Array<any>,
    inAction: false,
  }),
  mounted() {
    this.setProps();
    this.loadGroups();
  },
  methods: {
    ...mapMutations(DIALOG, ["hideModal", "setDialogResult"]),
    ...mapMutations(SNACKBAR, ["showSnackbar"]),
    setProps() {
      if (this.product != undefined) {
        this.upsert = {
          id: this.product.id,
          name: this.product.name,
          groups: this.product.groups.map((g: any) => g.id),
        };
        this.isUpdate = true;
      }
    },
    upsertProduct() {
      let isValid = (this.$refs.upsertProductForm as any).validate();
      if (isValid) {
        this.inAction = true;
        upsertProduct(this.upsert)
          .then((upsertRes) => {
            if (upsertRes.status) {
              this.setDialogResult({
                status: upsertRes.status,
                data: {
                  product: upsertRes.result.product,
                  new: !this.isUpdate,
                },
              });
              this.hideModal();
            }
            this.showSnackbar(upsertRes.title);
          })
          .finally(() => {
            this.inAction = false;
          });
      }
    },
    loadGroups() {
      getGroups(0, 0).then((groupsRes) => {
        if (groupsRes.status) this.groups = groupsRes.result.groups;
      });
    },
  },
});
</script>