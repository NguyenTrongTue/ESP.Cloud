<template>
  <div class="header">
    <div class="header__left">
      <div class="logo">
        <Logo />
      </div>
      <div class="header__left-category">
        <router-link to="/"> <span>Tìm gara</span></router-link>
        <router-link to="/estimator"> <span>Ước tính</span></router-link>
        <router-link to="/promo"> <span>Khuyến mãi</span></router-link>
        <router-link to="/questions"> <span>Thảo luận</span></router-link>
        <router-link to="/car_review"> <span>Review xe</span></router-link>
      </div>
    </div>
    <div class="header__right">

      <div class="header__right-user flex-center" @click="showPopupNotification = !showPopupNotification;">
        <div class="icon-notification">
          <micon type="Notify" />
          <span className="badge flex-center" v-if="notificationsUnread.length > 0">{{ notificationsUnread.length
            }}</span>
        </div>

        <PopupNotifications class="popup-notification" :notificationsProps="notifications"
          v-click-outside="handleClickOutSidePopup" v-if="showPopupNotification" />
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
  <div class="horizontal-separator"></div>
</template>

<script>
import MSearch from "@/components/search/MSearch.vue";
import Logo from "@/components/logo/Logo.vue";
import { RouterLink } from "vue-router";
import PopupNotifications from '@/components/popup/notifications/PopupNotifications.vue';
import SignalRService from "@/services/SignalRService";
import NotificationAPI from "@/apis/NotificationAPI";
export default {
  name: "TheHeader",
  components: { MSearch, Logo, RouterLink, PopupNotifications },
  inheritAttrs: false,
  props: { ...RouterLink.props },
  data() {
    return {
      showPopupAvatar: false,
      showPopupNotification: false,
      user: null,
      notifications: []
    };
  },
  computed: {
    isExternalLink() {
      return typeof this.to === "string" && this.to.startsWith("http");
    },

    notificationsUnread() {
      return this.notifications.filter(item => item.unread);
    }
  },

  methods: {

    handleClickOutSidePopup() {
      this.$ms.common.handleClickOutSide(event.target, "header__right-user", () => {
        this.showPopupNotification = false
      });
    },
    handleClickOutSide() {
      this.$ms.common.handleClickOutSide(event.target, "header__right-user", () => {
        this.showPopupAvatar = false
      });
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

    /**
     * Asynchronously handles the logout process by committing loading state, deleting user cache,
     * setting user to null, hiding the avatar popup, and redirecting to the home page.
     *
     * @return {Promise<void>} - A promise indicating the completion of the logout process.
     * @author nttue 17.05.2024
     */
    async handleLogout() {
      try {

        this.$store.commit("showLoading");
        this.$ms.cache.deleteCache("user");
        await new Promise((resovle) =>
          setTimeout(resovle, 500));
        this.user = null;
        this.showPopupAvatar = false;
        this.$router.push({
          path: "/",
        });
        this.$store.commit("hideLoading");
      } catch (e) {
        this.$store.commit("hideLoading");
        console.log(e);
      }
    },
    /**
     * Asynchronously handles incoming data to filter and update notifications.
     *
     * @param {any} data - The data to be processed for notifications.
     * @author nttue 17.05.2024
     */
    handleMessage(data) {
      let filterData = JSON.parse(data).filter(item => item.user_id === '00000000-0000-0000-0000-000000000000'
        || item.user_id === this.user.user_id
      );
      this.notifications = [...this.notifications, ...filterData];


    },
    /**
     * Asynchronously fetches notifications and updates the notifications data.
     *
     * @return {Promise<void>} 
     */
    async fetchNotifications() {
      const res = await NotificationAPI.get();
      if (res && res.length > 0) {
        this.notifications = res.filter(item => item.user_id === '00000000-0000-0000-0000-000000000000'
          || item.user_id === this.user.user_id
        );
      }
    }
  },

  /**
   * Đăng ký sự kiện resize cho widow
   */
  mounted() {
    try {

      const user = this.$ms.cache.getCache("user");
      this.fetchNotifications();
      if (user) {
        this.user = user;
      }

      SignalRService.start()
        .then(() => {
          console.log("Kết nối thành công tới thông báo hệ thống");
        })
        .catch(error => {
          console.error("Kết nối thất bại:", error);
        });


      SignalRService.on("ReceiveNotification", this.handleMessage);
    } catch (e) {
      console.log(e);
    }
  },
  // beforeUnmount() {
  //   SignalRService.disconnect();
  // }
};
</script>

<style lang="scss">
@import "./header.scss";
</style>
