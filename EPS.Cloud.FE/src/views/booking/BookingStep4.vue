<template>
  <div class="summary-wrapper">
    <div class="summary-drop-off">
      <div class="appointment-details" style="padding: 0">
        <div class="sub-h1">Đặt lịch tại</div>

        <div class="main-info">
          <div class="main-info__center">
            <div class="sidebar-item__left">
              <img
                src="https://storage.googleapis.com/rp-production-public-content/puz5muru666yt3hfcbrs86b222f8"
                alt="Ảnh của gara"
              />
            </div>
            <div class="sidebar-item__right">
              <div class="gara-name__wrapper">
                <div class="ds-body-regular ds-medium ng-font-neutral-80">
                  {{ garage?.garage_name }}
                </div>
                <div class="rating">
                  <micon type="Stars" />
                  <div>{{ garage?.avg_rating }}</div>
                  <span>({{ garage?.total_reviews }})</span>
                </div>
              </div>
              <div class="address">
                <span class="address-text">{{ garage?.address }} </span
                ><span class="fz-12">(2.8 km)</span>
              </div>

              <div class="facebook">
                <a :href="garage?.garage_website" target="_blank"
                  >facebook.com</a
                >
              </div>
              <div class="phone">
                <span>{{ garage?.phone }}</span>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="drop-off-time">
        <div class="time-icon">
          <micon type="Clock" />
        </div>
        <div class="time-text">
          <span
            class="ds-body-regular ds-medium ng-font-neutral-80 ml-1 mr-1"
            >{{ computedDropOff }}</span
          ><span class="ds-body-small ds-link ds-link--variant-regular pointer"
            >Sửa</span
          >
        </div>
      </div>
    </div>
    <div class="summary-vehicle">
      <div class="sub-h1">
        NỘI DUNG SỬA CHỮA
        <span class="ds-body-small ds-link ds-link--variant-regular pointer"
          >Sửa</span
        >
      </div>
      <div class="flex-start">
        <micon type="Car" />
        <div class="ng-pl8 ds-body-regular ng-font-neutral-80 ml-1">
          {{ BookingInfo.make }} {{ BookingInfo.model }} {{ BookingInfo.year }}
        </div>
      </div>
      <div class="flex-start">
        <micon type="Note" />
        <div class="ng-pl8 ds-body-regular ng-font-neutral-80 ml-1">
          Vấn đề của xe
        </div>
      </div>
      <div class="service-details ds-body-small ng-font-neutral-60">
        <ul>
          <li>{{ BookingInfo.comment }}</li>
        </ul>
      </div>
    </div>
    <div class="summary-contact">
      <div class="sub-h1">
        THÔNG TIN LIÊN LẠC
        <span class="ds-body-small ds-link ds-link--variant-regular pointer"
          >Sửa</span
        >
      </div>
      <div class="custom-name">
        {{ BookingInfo.last_name }} {{ BookingInfo.first_name }}
      </div>
      <div class="custom-email">{{ BookingInfo.email }}</div>
      <div class="custom-phone">{{ BookingInfo.phone }}</div>
    </div>
    <div class="next-steps-footer flex-center">
      <button class="next-step pointer" @click="handleNextStep">
        <span>Đặt lịch</span>
        <micon type="ArrowRight" />
      </button>
    </div>
    <p
      class="flex-center ds-body-small ds-medium ng-center ng-full-width ng-font-neutral-60 mb-4"
    >
      Bạn sẽ nhận được thống báo về thông tin đã chọn.
    </p>
  </div>
</template>

<script>
import { convertDate } from "@/utils/common";
export default {
  name: "BookingStep4",
  emits: ["nextStep"],
  props: {
    garageProps: {
      type: Object,
      default: null,
    },
    bookingInfo: {
      type: Object,
      default: {},
    },
  },
  data() {
    return {
      garage: this.garageProps,
      BookingInfo: this.bookingInfo,
    };
  },
  watch: {
    bookingInfo: {
      handler(newValue) {
        if (newValue) {
          this.BookingInfo = newValue;
        }
      },
      deep: true,
    },
  },
  computed: {
    /**
     * Generate the drop-off date and time based on the booking date.
     *
     * @return {string} The formatted drop-off date and time string.
     * @author nttue 12.03.2024
     */
    computedDropOff() {
      if (this.bookingInfo) {
        let day = convertDate(new Date(this.bookingInfo.booking_date).getDay()),
          date = new Date(this.bookingInfo.booking_date).getDate(),
          month = new Date(this.bookingInfo.booking_date).getMonth(),
          hours = new Date(this.bookingInfo.booking_date).getHours(),
          minutes = new Date(this.bookingInfo.booking_date).getMinutes();
        return `${day}, ngày ${date}/${month} lúc ${hours} giờ ${
          minutes > 0 ? minutes : ""
        } sáng`;
      } else {
        return "";
      }
    },
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
  methods: {
    handleNextStep() {
      this.$emit("nextStep");
    },
  },
};
</script>

<style scoped>
@import "./booking.scss";
.next-steps-footer {
  padding: 12px 0px 0px;
  button {
    height: 48px;
  }
}
</style>
