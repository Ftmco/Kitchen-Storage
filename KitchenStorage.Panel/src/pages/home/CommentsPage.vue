<template>
  <v-col cols="12">
    <v-card elevation="4">
      <v-card-title>
        نظرات
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="جستجو"
          single-line
          hide-details
        ></v-text-field>
        <v-spacer></v-spacer>
        <v-btn color="info" text @click="getComments">
          تازه سازی
          <v-icon>mdi-reload</v-icon>
        </v-btn>
      </v-card-title>
      <v-data-table
        :loading="inLoading"
        :headers="headers"
        :items="comments"
        :search="search"
        no-data-text="نظری یافت نشد"
        loading-text="کمی صبر کنید..."
        no-results-text="موردی یافت نشد"
        hide-default-footer
      >
        <template v-slot:item.actions="{ item }">
          <v-row>
            <v-col v-if="item.status != 0">
              <v-btn block color="primary" text>
                پذریش
                <v-icon>mdi-comment-eye</v-icon>
              </v-btn>
            </v-col>
            <v-col>
              <v-btn block color="error" text>
                حذف
                <v-icon>mdi-delete</v-icon>
              </v-btn>
            </v-col>
            <v-col>
              <v-btn block color="info" text @click="item">
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
import { getAdminComments } from "@/api/apis/comment.api";
import Vue from "vue";
import { mapMutations } from "vuex";
export default Vue.extend({
  data: () => ({
    search: "",
    headers: [
      {
        text: "نام و نام خانوادگی",
        align: "start",
        sortable: true,
        value: "user.fullName",
      },
      {
        text: "شماره موبایل",
        align: "start",
        sortable: true,
        value: "user.mobileNo",
      },
      {
        text: "ایمیل",
        align: "start",
        sortable: true,
        value: "user.email",
      },
      {
        text: "متن",
        align: "start",
        sortable: true,
        value: "text",
      },
      {
        text: "تاریخ ارسال",
        align: "start",
        sortable: true,
        value: "sendDate",
      },
      {
        text: "ساعت ارسال",
        align: "start",
        sortable: true,
        value: "sendTime",
      },
      {
        text: "تعداد پاسخ ها",
        align: "start",
        sortable: true,
        value: "replyCount",
      },
      {
        text: "عملیات",
        align: "center",
        sortable: false,
        value: "actions",
      },
    ],
    comments: [] as any[],
    inLoading: false,
    pageCount: 1,
    page: 1,
  }),
  mounted() {
    this.getComments();
  },
  methods: {
    ...mapMutations(["showSnackbar"]),
    getComments() {
      this.inLoading = true;
      getAdminComments(this.page - 1, 15, "")
        .then((commentsRes) => {
          if (commentsRes.status) {
            this.comments = commentsRes.result.comments;
            this.pageCount = commentsRes.result.pageCount + 1;
          } else this.showSnackbar(commentsRes.title);
        })
        .finally(() => {
          this.inLoading = false;
        });
    },
  },
});
</script>