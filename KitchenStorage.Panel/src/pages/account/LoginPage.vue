<template>
  <AccountBase
    title="ورود"
    subTitle="به خدمت گذار خوش آمدید"
    formHeight="500px"
  >
    <template v-slot:image>
      <v-img
        contain
        src="@/assets/images/login.png"
        lazy-src="@/assets/images/login.png"
        width="100%"
        height="500px"
      />
    </template>
    <template v-slot:form>
      <v-form ref="loginForm" @submit.prevent="sendCode">
        <v-text-field
          v-model="loginModel.userName"
          :rules="[rules.require]"
          :readonly="codeSended"
          outlined
          :clearable="!codeSended"
          color="blue darken-2"
          label="شماره موبایل"
          placeholder="شماره موبایل"
          required
          class="rounded-lg"
        ></v-text-field>

        <div class="ma-auto position-relative" v-if="codeSended">
          <v-otp-input
            type="number"
            @finish="loginSubmit"
            v-model="loginModel.code"
            length="5"
            :disabled="loading"
             class="rounded-lg"
          ></v-otp-input>
          <v-overlay absolute :value="loading">
            <v-progress-circular
              indeterminate
              color="primary"
            ></v-progress-circular>
          </v-overlay>
        </div>
      </v-form>
    </template>
    <template v-slot:actions>
      <div v-if="!codeSended">
        <v-btn
          color="primary"
          block
          @click="sendCode"
          class="rounded-lg"
          elevation="5"
          :loading="loading"
        >
          <span>ارسال کد</span>
        </v-btn>
        <br />
      </div>
    </template>
  </AccountBase>
</template>

<script lang="ts">
import Vue from "vue";
import { application, messages, rules } from "@/constants";
import AccountBase from "@/components/account/AccountBase.vue";
import { login, activation, otpLogin } from "@/api/apis/account.api";
import { mapMutations } from "vuex";

export default Vue.extend({
  components: { AccountBase },
  data: () => ({
    loginModel: {
      userName: "",
      code: "",
    },
    show: false,
    rules: rules,
    loading: false,
    codeSended: false,
  }),
  mounted() {
    this.loginModel.userName = this.$route.query.userName?.toString();
  },
  methods: {
    ...mapMutations(["showSnackbar"]),
    loginSubmit() {
      this.loading = true;
      activation({
        userName: this.loginModel.userName,
        activeCode: this.loginModel.code,
      })
        .then((res) => {
          if (res.status) (window.location as any) = "/";
          else this.loading = false;
          this.showSnackbar(res.title);
        })
        .catch((e) => {
          this.showSnackbar(e.message);
          this.loading = false;
        });
    },
    sendCode() {
      this.loading = true;
      otpLogin(this.loginModel.userName)
        .then((res) => {
          if (res.status) this.codeSended = true;
          this.showSnackbar(res.title);
        })
        .catch((e) => {
          this.codeSended = false;
          this.showSnackbar(e.message);
        })
        .finally(() => {
          this.loading = false;
        });
    },
  },
});
</script>