<template>
  <NavigationTop>
    <template v-slot:container>
      <div class="flex-between navigation-top-signup">
        <div class="navigation-top__left flex-center">
          <a href="/">
            <div class="logo">
              <Logo />
            </div>
          </a>
        </div>
        <div class="navigation-top__right">
          <mbutton button-text="Đăng ký" @click="hanldeNavigateToSignup" />
        </div>
      </div>
    </template>
  </NavigationTop>
  <div class="signup">
    <div class="large-text mb-3">Đăng nhập vào Repair</div>
    <div class="logo__center">
      <Logo />
    </div>
    <div class="signup__container">
      <minput formName="User" label="Email" name="Email" v-model="user.email" ref="Email" placeholder-input="Email"
        rules="required|email" class="mb-2" />
      <minput formName="User" label="Mật khẩu" name="Password" v-model="user.password" ref="Password"
        placeholder-input="Mật khẩu" rules="required" class="mb-2" :icon1="icon1" :icon2="icon2" type-input="password"
        :click-icon="handleSwitchVisibility" />
      <div class="forgot-password pointer" @click="handleResetPassword">
        Bạn quên mật khẩu?
      </div>
      <mbutton button-text="Đăng nhập" @click="handleLogin" />
    </div>
  </div>
  <ResetPassword ref="resetPassword" />
</template>

<script>
import NavigationTop from "@/components/navigation-top/NavigationTop.vue";
import Logo from "@/components/logo/Logo.vue";
import Eye from "@/components/icons/Eye.vue";
import HideEye from "@/components/icons/HideEye.vue";
import Done from "@/components/icons/Done.vue";
import ResetPassword from "./ResetPassword.vue";
import AuthAPI from "@/apis/AuthAPI.js";
import validateMixin from "@/mixins/validateMixin.vue";
export default {
  name: "Login",
  components: {
    NavigationTop,
    Logo,
    Done,
    ResetPassword,
  },
  mixins: [validateMixin],
  props: {},
  /**
   * Returns the initial data for the component.
   *
   * @return {Object} The initial data object
   */
  data() {
    return {
      user: {},
      icon1: HideEye,
      icon2: Eye,
      formName: "User",
    };
  },
  watch: {},

  methods: {
    handleResetPassword() {
      this.$refs.resetPassword.handleShow();
    },
    /**
     * A function to handle navigation to the signup page.
     * @author nttue 03.03.2024
     */
    hanldeNavigateToSignup() {
      this.$router.push({
        path: "/signup",
      });
    },
    /**
     * Handles the signup process, checking for required fields and inserting any errors found.
     *
     * @return {boolean} true if there are errors, false otherwise
     * @author nttue 03.03.2024
     */
    async handleLogin() {
      try {
        if (!this.handleValidate()) {
          this.showLoading = true;
          const res = await AuthAPI.login(this.user);
          if (res && res.user_id) {
            this.$ms.cache.setCache("user", res);
            this.$router.push({
              path: "/",
            });
          } else {
            this.$store.commit("showToast", {
              label:
                "Email hoặc mật khẩu người dùng không chính xác. Vui lòng kiểm tra lại!",
              type: "error",
            });
          }
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
  },
};
</script>

<style lang="scss" scroped>
@import "./login.scss";
</style>
