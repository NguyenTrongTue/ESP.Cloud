<template>

  <div class="chatbot" @click="handleShowPopupChat" v-if="showChatBot">
    <img src="@/assets/img/mybot.gif" alt="icon" />
  </div>

  <PopupChat ref="PopupChat" @close="handleHidePopupChat" />
</template>

<script>
import PopupChat from './PopupChat.vue'
export default {
  name: "MChatBot",
  components: {
    PopupChat
  },
  props: {

  },
  data() {
    return {
      showChatBot: true,
    }

  },
  watch: {},

  methods: {
    handleShowPopupChat() {
      this.$refs['PopupChat'].show();
      this.showChatBot = false;
    },
    handleHidePopupChat() {
      this.showChatBot = true;
    }


  },
};
</script>

<style lang="scss" scoped>
@import "./chatbot.scss";
</style>
