<template>
  <mmodal v-if="show">
    <template v-slot:container>
      <div class="reset-password">
        <div class="reset-password__top mb-1">
          <Close class="pointer" @click="handleClose" />
        </div>
        <div class="reset-password__body">
          <div class="medium-text mb-3">Lấy lại mật khẩu của bạn</div>
          <p class="mb-2 mt-1">
            Nhập email bạn đã sử dụng để đăng ký và chúng tôi sẽ gửi cho bạn
            liên kết để đặt lại mật khẩu.
          </p>
          <minput
            formName="User"
            label="Email"
            name="Email"
            v-model="user.email"
            ref="Email"
            placeholder-input="Email"
            rules="required|email"
            class="mb-4 mt-1"
          />
          <mbutton
            button-text="Lấy lại mật khẩu"
            @click="handleResetPassword"
          />
        </div>
      </div>
    </template>
  </mmodal>
</template>

<script>
import Close from "@/components/icons/Close.vue";
import validateMixin from "@/mixins/validateMixin.vue";
import AuthAPI from "@/apis/AuthAPI.js";
export default {
  name: "ResetPassword",
  components: { Close },
  mixins: [validateMixin],
  props: {},
  data() {
    return {
      user: {},
      show: true,
      formName: "User",
    };
  },
  watch: {},

  methods: {
    async handleResetPassword() {
      try {
        if (!this.handleValidate()) {
          this.showLoading = true;
          await AuthAPI.getNewPassword(this.user.email);
          this.$store.commit(
            "dialog/setErrorMessage",
            "Vui lòng kiểm tra email để xác nhận mật khẩu!"
          );
          this.handleClose();
          this.showLoading = false;
        }
      } catch (e) {
        console.log(e);
        this.$store.commit("showToast", {
          label: "Có lỗi xảy ra. Liên hệ quản trị viên để được hỗ trợ",
          type: "error",
        });
        this.showLoading = false;
      }
    },
    handleClose() {
      this.show = false;
    },
    handleShow() {
      this.show = true;
    },
  },
};
</script>

<style lang="scss" scoped>
@import "./login.scss";
</style>
