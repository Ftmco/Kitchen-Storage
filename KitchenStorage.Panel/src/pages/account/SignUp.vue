<template>
  <AccountBase
    title="ثبت نام"
    subTitle="ثبت نام در خدمت گذار"
    formHeight="100%"
  >
    <template v-slot:image>
      <v-img
        contain
        src="@/assets/images/register.png"
        lazy-src="@/assets/images/register.png"
        width="100%"
        height="100%"
      />
    </template>

    <template v-slot:form>
      <v-form ref="signUpForm">
        <v-text-field
          v-model="signUpModel.userName"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="نام کاربری"
          placeholder="نام کاربری"
          required
        ></v-text-field>
        <v-text-field
          v-model="signUpModel.fullName"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="نام و نام خانوادگی"
          placeholder="نام و نام خانوادگی"
          required
        ></v-text-field>
        <v-text-field
          v-model="signUpModel.mobileNo"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="موبایل"
          placeholder="موبایل"
          required
        ></v-text-field>
        <v-text-field
          v-model="signUpModel.email"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="ایمیل"
          placeholder="ایمیل"
          required
        ></v-text-field>
        <v-text-field
          v-model="signUpModel.password"
          :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
          :type="show ? 'text' : 'password'"
          :rules="[rules.require, rules.password]"
          outlined
          clearable
          label="کلمه عبور"
          placeholder="کلمه عبور"
          counter
          @click:append="show = !show"
        ></v-text-field>
      </v-form>
    </template>
    <template v-slot:actions>
      <v-btn color="primary" block @click="registerSubmit"> ثبت نام </v-btn>
      <br />
      <v-row>
        <v-col>
          <v-btn color="info" block :to="{ name: 'Login' }">
            <span>ورود</span>
          </v-btn>
        </v-col>
        <v-col>
          <v-btn color="info" block :to="{ name: 'ActiveAccount' }">
            <span>فعال سازی حساب کاربری</span>
          </v-btn>
        </v-col>
      </v-row>
    </template>
  </AccountBase>
</template>

<script lang="ts">
import Vue from "vue";
import AccountBase from "@/components/account/AccountBase.vue";
import { messages, rules } from "@/constants";
import { apiCall } from "@/api";
import { register } from "fteam.identity.package/src/Account/account";
import { changeBaseUrl } from "fteam.identity.package/src/constants/index";
import { mapMutations } from "vuex";
export default Vue.extend({
  data: () => ({
    signUpModel: {
      userName: "",
      fullName: "",
      mobileNo: "",
      email: "",
      password: "",
    },
    rules: rules,
    show: false,
  }),
  components: {
    AccountBase,
  },
  methods: {
    ...mapMutations(["showSnackbar"]),
    registerSubmit() {
      let isValid = (this.$refs.signUpForm as any).validate();
      if (isValid) {
        register({
          userName: this.signUpModel.userName,
          fullName: this.signUpModel.userName,
          password: this.signUpModel.password,
          email: this.signUpModel.email,
          mobileNo: this.signUpModel.mobileNo,
          application: {
            apiKey:
              "f7f2c8d20cd41269d3eb8b2d9a8761a2275c9386ede336e2f03f7ab83dab81c6",
            password: "123456",
          },
        })
          .then((res: any) => {
            this.showSnackbar(res.title);
          })
          .catch((e: any) => {
            this.showSnackbar(e.meesage);
          });
      } else this.showSnackbar(messages.invalidForm);
    },
  },
});
</script>