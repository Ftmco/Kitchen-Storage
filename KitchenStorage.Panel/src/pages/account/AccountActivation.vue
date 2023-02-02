<template>
  <AccountBase
    title="فعال سازی حساب"
    subTitle="فعال سازی حساب خدمت گذار"
    formHeight="500px"
  >
    <template v-slot:image>
      <v-img
        contain
        src="@/assets/images/reset-password.png"
        lazy-src="@/assets/images/reset-password.png"
        width="100%"
        height="500px"
      />
    </template>

    <template v-slot:form>
      <v-form ref="activeForm">
        <v-text-field
          v-model="activeModel.userName"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="نام کاربری/ایمیل/شماره موبایل"
          placeholder="نام کاربری/ایمیل/شماره موبایل"
          required
        ></v-text-field>
        <v-text-field
          v-model="activeModel.activeCode"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="کد فعال سازی"
          placeholder="کد فعال سازی"
          required
        ></v-text-field>
      </v-form>
    </template>

    <template v-slot:actions>
      <div>
        <v-btn color="primary" block @click="activeAccount">
          <span>فعال سازی</span>
        </v-btn>
        <br />
        <v-row>
          <v-col>
            <v-btn color="info" block :to="{ name: 'ForgotPassword' }">
              <span>فراموشی کلمه عبور</span>
            </v-btn>
          </v-col>
          <v-col>
            <v-btn color="info" block :to="{ name: 'SignUp' }">
              <span>ثبت نام</span>
            </v-btn>
          </v-col>
          <v-col>
            <v-btn color="info" block :to="{ name: 'Login' }">
              <span>ورود</span>
            </v-btn>
          </v-col>
        </v-row>
      </div>
    </template>
  </AccountBase>
</template>

<script lang="ts">
import Vue from "vue";
import AccountBase from "@/components/account/AccountBase.vue";
import { messages, rules } from "@/constants";
export default Vue.extend({
  data: () => ({
    activeModel: {
      userName: "",
      activeCode: "",
    },
    rules: rules,
  }),
  mounted() {
    this.activeModel.userName = this.$route.query.userName.toString();
  },
  components: {
    AccountBase,
  },
  methods: {
    activeAccount() {
      (this.$root.$refs.loading as any).open();
     
    },
    showMessage(text: string) {
      (this.$root.$refs.snackbar as any).open(text);
      (this.$root.$refs.loading as any).close();
    },
  },
});
</script>