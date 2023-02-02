<template>
  <v-col cols="12">
    <v-form ref="addSellerForm">
      <v-row>
        <v-col cols="12" sm="6" md="6">
          <v-text-field
            outlined
            label="کد فروشگاه"
            placeholder="کد فورشگاه"
            clearable
            class="rounded-lg"
            :rules="[rules.require]"
          />
        </v-col>
        <v-col cols="12" sm="6" md="6">
          <v-text-field
            outlined
            label="عنوان کالا"
            placeholder="عنوان کالا"
            clearable
            class="rounded-lg"
            :rules="[rules.require]"
          />
        </v-col>
        <v-col cols="12">
          <v-text-field
            outlined
            label="قیمت"
            placeholder="قیمت"
            type="number"
            clearable
            class="rounded-lg"
            :rules="[rules.require]"
          />
        </v-col>
        <v-col cols="12">
          <v-textarea
            auto-grow
            rows="4"
            outlined
            label="توضیحات کوتاه"
            placeholder="توضیحات کوتاه"
            clearable
            class="rounded-lg"
            :rules="[rules.require]"
          />
        </v-col>
        <v-col cols="12">
          <v-textarea
            auto-grow
            rows="6"
            outlined
            label="توضیحات"
            placeholder="توضیحات"
            clearable
            class="rounded-lg"
            :rules="[rules.require]"
          />
        </v-col>
        <v-col cols="12">
          <v-btn block color="primary" class="rounded-lg" elevation="5">
            افزودن فروشنده
            <v-icon>mdi-add</v-icon>
          </v-btn>
        </v-col>
      </v-row>
    </v-form>
  </v-col>
</template>

<script lang="ts">
import { rules } from "@/constants";
import Vue from "vue";
export default Vue.extend({
  props: ["productId"],
  data: () => ({
    rules: rules,
  }),
});
</script>