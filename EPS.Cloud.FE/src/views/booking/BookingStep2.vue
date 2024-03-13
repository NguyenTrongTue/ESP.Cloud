<template>
  <BaseBooking :garageProps="garageProps" @nextStep="handleNextStep">
    <template v-slot:container>
      <div class="steps-left-tilte">
        <h2>Thông tin liên lạc</h2>
      </div>
      <div class="steps-left-body">
        <div class="row-50">
          <mcombobox
            label="Hãng xe"
            default-text="Chọn hãng xe"
            class="mb-4"
            :items="makeList"
            v-model="BookingInfo.make"
            :disabled="false"
          />
          <mcombobox
            label="Năm"
            default-text="Chọn năm"
            class="mb-4"
            :items="yearList"
            v-model="BookingInfo.year"
            :disabled="!BookingInfo.make"
          />
          <mcombobox
            label="Mẫu xe"
            default-text="Chọn mẫu xe"
            class="mb-4"
            :items="modelList"
            v-model="BookingInfo.model"
            :disabled="!BookingInfo.year"
          />
        </div>
        <div class="ds-heading-5 ng-font-neutral-70">
          Mô tả các vấn đề bạn đang gặp phải với chiếc xe của mình
        </div>
        <div class="description-text ds-body-small mt-1 mb-1">
          Báo cho shop biết lỗi gì để họ chăm sóc chính xác nhé
        </div>
        <minput
          name="Comment"
          v-model="BookingInfo.comment"
          ref="Comment"
          placeholder-input="Comment"
          rules="required"
          class="mb-2"
          type-component="textarea"
          formName="Booking"
        />
      </div>
    </template>
    <template v-slot:main-info__1>
      <div class="ds-sub-heading-1 ng-font-neutral-60 ng-pb8 mt-2 mb-1">
        Thời gian sửa
      </div>
      <div class="flex-start">
        <micon type="Clock" />
        <div class="ng-pl8 ds-body-regular ng-font-neutral-80">
          {{ computedDropOff }}
        </div>
      </div>
    </template>
  </BaseBooking>
</template>

<script>
import BaseBooking from "./BaseBooking.vue";
import { convertDate } from "@/utils/common";
import BookingAPI from "@/apis/BookingAPI";
export default {
  extends: BaseBooking,
  name: "BookingStep2",
  components: {
    BaseBooking,
  },
  props: {
    bookingInfo: {
      type: Object,
      default: {},
    },
    garageId: {
      type: String,
      default: null,
    },
  },

  data() {
    return {
      makeList: [],
      yearList: [],
      modelList: [],
      modelAndIdCarList: [],
      formName: "Booking",
      booking_date: this.bookingInfo.booking_date,
    };
  },
  mounted() {
    this.fetchMakes();
  },
  watch: {
    "bookingInfo.booking_date"(newValue) {
      if (newValue) {
        this.booking_date = newValue;
      }
    },

    "BookingInfo.make"(newValue) {
      if (newValue) {
        this.fetchYears();
        this.BookingInfo.year = null;
        this.BookingInfo.model = null;
      }
    },
    "BookingInfo.year"(newValue) {
      if (newValue) {
        this.fetchModel();
        this.BookingInfo.model = null;
      }
    },
    "BookingInfo.model"(newValue) {
      if (newValue) {
        this.BookingInfo.cars_id = this.modelAndIdCarList.find(
          (item) => item.model == newValue
        ).cars_id;
      }
    },
  },
  computed: {
    /**
     * Calculate the drop-off time based on the booking date, and return the formatted drop-off time string.
     *
     * @return {string} the formatted drop-off time string
     */
    computedDropOff() {
      if (this.booking_date) {
        let day = convertDate(new Date(this.booking_date).getDay()),
          date = new Date(this.booking_date).getDate(),
          month = new Date(this.booking_date).getMonth(),
          hours = new Date(this.booking_date).getHours(),
          minutes = new Date(this.booking_date).getMinutes();
        return `${day}, ngày ${date}/${month} lúc ${hours} giờ ${
          minutes > 0 ? minutes : ""
        } sáng`;
      } else {
        return "";
      }
    },
  },
  methods: {
    /**
     * Handle the next step by validating and emitting the "nextStep" event if validation passes.
     */
    handleNextStep() {
      if (!this.handleValidate()) {
        this.$emit("nextStep", this.BookingInfo);
      }
    },

    async fetchMakes() {
      try {
        const result = await BookingAPI.getMakeByGarageId(this.garageId);
        this.makeList = result.map((item) => item.make);
      } catch {
        // ignore error
      }
    },
    async fetchYears() {
      try {
        const result = await BookingAPI.getYearsByGarageIdAndMake(
          this.garageId,
          this.BookingInfo.make
        );
        this.yearList = result.map((item) => item.year);
      } catch {
        // ignore error
      }
    },
    async fetchModel() {
      try {
        const result = await BookingAPI.getModelsByGarageId(
          this.garageId,
          this.BookingInfo.make,
          this.BookingInfo.year
        );
        this.modelList = result.map((item) => item.model);
        this.modelAndIdCarList = result;
      } catch {
        // ignore error
      }
    },
  },
};
</script>

<style scoped>
@import "./booking.scss";
</style>
