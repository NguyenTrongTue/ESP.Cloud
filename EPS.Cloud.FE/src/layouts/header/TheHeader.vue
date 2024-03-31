<template>
  <div class="header">
    <div class="header__left">
      <div class="logo">
        <Logo />
      </div>
      <div class="header__left-category">
        <router-link to="/"> <span>Tìm gara</span></router-link>
        <router-link to="/estimator"> <span>Ưóc tính</span></router-link>
        <router-link to="/promo"> <span>Khuyến mãi</span></router-link>
        <router-link to="/history-booking">
          <span>Lịch sử sửa chữa</span></router-link>
        <router-link to="/notify"> <span>Thông báo</span></router-link>
      </div>
    </div>
    <div class="header__right">
      <div class="header__right-user flex-center">
        <micon type="Search" />
      </div>
      <div class="header__right-user flex-center">
        <micon type="Notify" />
      </div>
      <div class="header__right-user flex-center" @click="showPopupAvatar = !showPopupAvatar">
        <div :style="user && { color: '#0098d3' }">
          <micon type="Avatar" />
        </div>
        <div class="popup-avatar" v-if="showPopupAvatar && !user" v-click-outside="handleClickOutSide">
          <div class="title mb-1">
            Xe, dịch vụ và cuộc hẹn của bạn ở cùng một nơi.
          </div>
          <mbutton button-text="Đăng nhập" @click="handleLogin" class="mb-2" />
          <mbutton button-text="Đăng ký" @click="handleSingup" class="none-background" />
        </div>

        <div class="popup-avatar" v-if="showPopupAvatar && user" v-click-outside="handleClickOutSide">
          <div class="title mb-2 mt-1">
            Xin chào, {{ user.fullname }}
          </div>
          <mbutton button-text="Đăng xuất" @click="handleLogout" class="none-background" />
        </div>
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
    return { showPopupAvatar: false, user: null };
  },
  computed: {
    isExternalLink() {
      return typeof this.to === "string" && this.to.startsWith("http");
    },
  },

  methods: {
    handleClickOutSide() {

      this.showPopupAvatar = false
    },


    findParent(currentTag, targetTag) {
      var tag = currentTag,
        result = false;
      while (!result && tag.className != "filter__item") {
        if (tag.className.includes(targetTag)) {
          result = true;
        } else {
          tag = currentTag.parentElement;
        }
      }

      return result;
    },

    handleLogin() {
      this.$router.push({
        path: "/login",
      });
    },
    handleSingup() {
      this.$router.push({
        path: "/signup",
      });
    },

    handleLogout() {
      this.$common.cache.deleteCache("user");
      this.user = null;
      this.showPopupAvatar = false;
      this.$router.push({
        path: "/",
      });
    }


  },

  /**
   * Đăng ký sự kiện resize cho widow
   */
  mounted() {
    const user = this.$common.cache.getCache("user");

    if (user) {
      this.user = user;
    }

  },
};
</script>

<style lang="scss">
@import "./header.scss";
</style>
