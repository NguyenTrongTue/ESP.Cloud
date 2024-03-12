<template>
  <div class="header">
    <div class="header__left">
      <div class="logo"><Logo /></div>
      <div class="header__left-category">
        <router-link to="/"> <span>Tìm gara</span></router-link>
        <router-link to="/booking"> <span>Đặt lịch</span></router-link>
        <router-link to="/promo"> <span>Khuyến mãi</span></router-link>
        <router-link to="/history-booking">
          <span>Lịch sử sửa chữa</span></router-link
        >
        <router-link to="/notify"> <span>Thông báo</span></router-link>
      </div>
    </div>
    <div class="header__right">
      <MSearch />
      <div class="header__right-user">
        <img src="@/assets/icon/avatar-default.png" alt="" class="avatar" />
      </div>
    </div>
  </div>
</template>

<script>
import MSearch from "@/components/search/MSearch.vue";
import Logo from "@/components/logo/Logo.vue";
import { RouterLink } from "vue-router";
export default {
  name: "TheHeader",
  components: { MSearch, Logo, RouterLink },
  inheritAttrs: false,
  props: { ...RouterLink.props },
  data() {
    return {};
  },
  computed: {
    isExternalLink() {
      return typeof this.to === "string" && this.to.startsWith("http");
    },
  },

  methods: {},

  /**
   * Đăng ký sự kiện resize cho widow
   */
  mounted() {},
};
</script>

<style lang="scss">
@import "./header.scss";
</style>
