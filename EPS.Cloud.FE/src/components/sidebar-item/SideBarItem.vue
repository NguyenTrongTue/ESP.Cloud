<template>
  <div class="sidebar-item">
    <div class="sidebar-item__left">
      <img :src="location.image" alt="Ảnh của gara" />
    </div>
    <div class="sidebar-item__right">
      <div class="gara-name__wrapper">
        <div class="gara-name">{{ location.garage_name }}</div>
        <div class="rating">
          <Stars />
          <div>{{ location.avg_rating.toFixed(2) }}</div>
          <span>({{ location.total_rating }})</span>
        </div>
      </div>
      <div class="address">
        <img
          src="https://www.gstatic.com/images/icons/material/system_gm/1x/place_gm_blue_24dp.png"
          alt="Ảnh của gara"
        />

        <mtooltip
          :content="location.address"
          class="address-text"
          :arrow="false"
          ><span>{{ location.address }} </span></mtooltip
        ><span>({{ location.distance.toFixed(2) }} km)</span>
      </div>
      <div class="time-open">
        <Clock style="color: rgb(0, 60, 255)" />
        <span
          :class="
            buildOpenTimeText.split('·')[0].includes('Đang mở')
              ? 'active'
              : 'close'
          "
          >{{ buildOpenTimeText.split("·")[0] }}</span
        >
        <span> · </span>
        <span>{{ buildOpenTimeText.split("·")[1] }}</span>
      </div>
      <div class="facebook">
        <img
          src="https://www.gstatic.com/images/icons/material/system_gm/1x/public_gm_blue_24dp.png"
          alt="Ảnh của gara"
        />
        <a :href="location.garage_website" target="_blank">facebook</a>
      </div>
      <div class="phone">
        <img
          src="https://www.gstatic.com/images/icons/material/system_gm/1x/phone_gm_blue_24dp.png"
          alt="Ảnh của gara"
        />

        <span>{{ location.phone }}</span>
      </div>
      <div class="button-wrapper">
        <mbutton
          :button-text="$t('Booking')"
          :icon="icon1"
          type="icon"
          @click="handleBooking(location)"
        />
        <mbutton
          :button-text="$t('Direction')"
          :icon="icon2"
          type="icon"
          @click="handleDirecting"
        />
        <mbutton
          :button-text="$t('Share')"
          :icon="icon3"
          type="icon"
          @click="handleSharing"
        />
      </div>
    </div>
  </div>
</template>

<script>
import Stars from "@/components/icons/Stars.vue";
import Clock from "@/components/icons/Clock.vue";
import Books from "@/components/icons/Books.vue";
import Direction from "@/components/icons/Direction.vue";
import Share from "@/components/icons/Share.vue";

import { convertDate } from "@/utils/common";

export default {
  name: "SideBarItem",
  components: {
    Stars,
    Clock,
  },
  props: {
    garaIndex: {
      type: Number,
      default: 0,
    },
    location: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {
      icon1: Books,
      icon2: Direction,
      icon3: Share,
    };
  },

  computed: {
    buildOpenTimeText() {
      let timeOpen = this.location.open_time,
        timeClose = this.location.close_time;

      const current = new Date();
      const open = new Date(
        current.getFullYear(),
        current.getMonth(),
        current.getDate(),
        timeOpen.split(":")[0],
        timeOpen.split(":")[1]
      );
      const close = new Date(
        current.getFullYear(),
        current.getMonth(),
        current.getDate(),
        timeClose.split(":")[0],
        timeClose.split(":")[1]
      );

      if (current >= open && current <= close) {
        const [house, minutes] = timeClose.split(":");
        return `Đang mở · Đóng cửa lúc ${house}h${minutes}`;
      } else if (current <= open) {
        const [house, minutes] = timeOpen.split(":");
        return `Đóng cửa · Mở cửa lúc ${house}h${minutes}`;
      } else {
        const [house, minutes] = timeOpen.split(":");
        return `Đóng cửa · Mở cửa vào ${house}h${minutes} ${convertDate(
          current.getDay()
        )}`;
      }
    },
  },

  methods: {
    handleBooking(location) {
      this.$router.push({ path: `/booking/${location.garage_id}` });
    },
    handleDirecting() {
      this.$emitter.emit("directing", this.garaIndex);
    },
    handleSharing() {},
  },
};
</script>

<style lang="scss" scoped>
@import "./sidebar-item.scss";
</style>
