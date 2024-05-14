<template>
  <div class="steps-left">
    <slot name="container"></slot>
  </div>
  <div class="steps-right">
    <div class="appointment-details">
      <div class="sub-h1">Đặt lịch tại</div>

      <div class="main-info">
        <div class="main-info__center">
          <div class="sidebar-item__left">
            <img :src="garage?.image.split(';')[0]" alt="Ảnh của gara" />
          </div>
          <div class="sidebar-item__right">
            <div class="gara-name__wrapper">

              <mtooltip :content="garage?.garage_name" class="gara-name" :arrow="false">
                <div class="gara-name">{{ garage?.garage_name }}</div>
              </mtooltip>

              <div class="rating">
                <micon type="Stars" />
                <div>{{ garage?.avg_rating.toFixed(2) }}</div>
                <span>({{ garage?.total_rating }})</span>
              </div>
            </div>
            <div class="address">
              <mtooltip :content="garage?.garage_name" class="address-text" :arrow="false">
                <span class="address-text">{{ garage?.address }} </span>
              </mtooltip>
              <span class="fz-12">(2.8 km)</span>
            </div>

            <div class="facebook">
              <a :href="garage?.garage_website" target="_blank">facebook.com</a>
            </div>
            <div class="phone">
              <span>{{ garage?.phone }}</span>
            </div>
          </div>
        </div>
        <div class="time-open fz-14">
          <span :class="computedbuildOpenTimeText.split('·')[0].includes('Đang mở')
              ? 'active'
              : 'close'
              ">{{ computedbuildOpenTimeText.split("·")[0] }}</span>
          <span>&nbsp;·&nbsp;</span>
          <span>{{ computedbuildOpenTimeText.split("·")[1] }}</span>
        </div>
        <slot name="main-info__1"></slot>
        <slot name="main-info__2"></slot>
      </div>
    </div>
    <div class="next-steps-footer flex-center">
      <button class="next-step pointer" @click="handleSubmit">
        <span>Tiếp tục</span>
        <micon type="ArrowRight" />
      </button>
    </div>
  </div>
</template>
<script>
import validateMixin from "@/mixins/validateMixin.vue";
import enterFormMixin from "@/mixins/enterFormMixin.vue";
export default {
  emits: ["nextStep"],
  props: {
    garageProps: {
      type: Object,
      default: null,
    },
  },
  mixins: [validateMixin, enterFormMixin],
  data() {
    return {
      garage: this.garageProps,
      BookingInfo: {},
      formName: "Booking",
    };
  },
  watch: {
    garageProps: {
      /**
       * A description of the entire function.
       *
       * @param {type} newGarage - description of parameter
       * @return {type}
       */
      handler(newGarage) {
        if (newGarage) {
          this.garage = newGarage;
        }
      },
      deep: true,
    },
  },
  computed: {
    computedbuildOpenTimeText() {
      let timeOpen = this.garage?.open_time,
        timeClose = this.garage?.close_time;
      return this.$ms.common.buildOpenTimeText(timeOpen, timeClose);
    },
  },
  methods: {
    handleSubmit() {
      this.$emit("nextStep");
    },
  },
};
</script>

<style scoped>
@import "./booking.scss";
</style>
