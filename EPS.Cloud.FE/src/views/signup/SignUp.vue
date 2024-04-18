<template>
  <NavigationTop>
    <template v-slot:container>
      <div class="flex-between navigation-top-signup">
        <div class="navigation-top__left flex-center" @="handleBack">
          <micon type="ArrowLeft" />
          <span>Trở lại</span>
        </div>
        <div class="navigation-top__right">
          <a href="/">
            <div class="logo">
              <Logo />
            </div>
          </a>
        </div>
      </div>
    </template>
  </NavigationTop>
  <div class="signup">
    <div class="large-text mb-3">Tạo tài khoản</div>
    <div class="signup__container">
      <minput label="Email" name="Email" v-model="user.email" ref="Email" placeholder-input="Email"
        rules="required|email" formName="User" class="mb-2" />
      <minput label="Họ và tên" name="Fullname" v-model="user.fullname" ref="Fullname" placeholder-input="Họ và tên"
        rules="required|maxlength_100" formName="User" class="mb-2" />
      <minput label="Số điện thoại" name="Phone" v-model="user.phone" ref="Phone" placeholder-input="Số điện thoại"
        class="mb-2" />
      <minput label="Địa chỉ" name="Address" v-model="user.address" ref="Address" placeholder-input="Địa chỉ"
        class="mb-2" />
      <minput label="Mật khẩu" name="Password" v-model="user.password" ref="Password" placeholder-input="Mật khẩu"
        rules="required|minlength_8|containsUppercase|includeNumber|containsSpecialCharacter" formName="User"
        class="mb-2" :icon1="icon1" :icon2="icon2" type-input="password" :click-icon="handleSwitchVisibility" />
      <div class="rules">
        <div class="flex-start">
          <Done class="done" v-show="showWarning.includes('minlength_8')" />
          <p>Mật khẩu cần dài ít nhất 8 ký tự</p>
        </div>
        <div class="flex-start">
          <Done class="done" v-show="!showWarning.includes('containsUppercase')" />
          <p>Phải có ít nhất một ký tự viết thường và một ký tự viết hoa</p>
        </div>
        <div class="flex-start">
          <Done class="done" v-show="!showWarning.includes('includeNumber')" />
          <p>Sử dụng ít nhất một số</p>
        </div>
        <div class="flex-start">
          <Done class="done" v-show="!showWarning.includes('containsSpecialCharacter')" />
          <p>Phải sử dụng ít nhất một ký hiệu (&, $, !, %, v.v.)</p>
        </div>
      </div>

      <mbutton button-text="Đăng ký" class="mt-2" @click="handleSignup" />
    </div>
  </div>
</template>

<script>
import NavigationTop from "@/components/navigation-top/NavigationTop.vue";
import Logo from "@/components/logo/Logo.vue";
import Eye from "@/components/icons/Eye.vue";
import HideEye from "@/components/icons/HideEye.vue";
import Done from "@/components/icons/Done.vue";
import insertError from "@/utils/insertError.js";
import { validate } from "@/utils/validate";
import AuthAPI from "@/apis/AuthAPI.js";
import validateMixin from "@/mixins/validateMixin.vue";
export default {
  name: "Signup",
  components: {
    NavigationTop,
    Logo,
    Done,
  },
  props: {},
  mixins: [validateMixin],
  data() {
    return {
      user: {},
      icon1: HideEye,
      icon2: Eye,
      formName: "User",
    };
  },
  watch: {},
  computed: {
    /**
     * Function to show warning messages based on password validation rules.
     *
     * @return {Array} Array of warning messages
     */
    showWarning() {
      const warnings = [];
      const passwordValue = this.user?.password?.trim();

      if (passwordValue && passwordValue.length >= 8) {
        warnings.push("minlength_8");
      }

      if (
        validate["containsUppercase"](
          passwordValue,
          this.$MResources.User["Password"]["containsUppercase"]
        )
      ) {
        warnings.push("containsUppercase");
      }
      if (
        validate["includeNumber"](
          passwordValue,
          this.$MResources.User["Password"]["includeNumber"]
        )
      ) {
        warnings.push("includeNumber");
      }
      if (
        validate["containsSpecialCharacter"](
          passwordValue,
          this.$MResources.User["Password"]["containsSpecialCharacter"]
        )
      ) {
        warnings.push("containsSpecialCharacter");
      }

      return warnings;
    },
  },

  methods: {
    /**
     * handleBack - A function to handle going back using the router.
     *
     * @return {void}
     */
    handleBack() {
      this.$router.back();
    },
    /**
     * Handles the signup process, checking for required fields and inserting any errors found.
     *
     * @return {boolean} true if there are errors, false otherwise
     */
    async handleSignup() {
      try {
        if (!this.handleValidate()) {
          this.$store.commit("showLoading");
          await AuthAPI.register(this.user);
          this.$store.commit("showToast", {
            label: "Tạo tài khoản thành công",
            type: 'success'
          });
          this.$store.commit("hideLoading")
          this.$router.push({
            path: "/login",
          });
        }
      } catch (e) {
        console.log(e);
        this.$store.commit("hideLoading")
      }
    },
  },
};
</script>

<style lang="scss" scroped>
@import "./signup.scss";
</style>
