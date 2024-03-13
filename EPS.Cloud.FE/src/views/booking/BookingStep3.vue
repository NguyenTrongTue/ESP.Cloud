<template>
  <BaseBooking :garageProps="garageProps" @nextStep="handleNextStep">
    <template v-slot:container>
      <div class="steps-left-tilte">
        <div class="content-title ds-medium ng-font-neutral-80 ng-text-center">
          Thông tin liên lạc
        </div>
        <p class="ds-body-regular ng-font-neutral-60 ng-text-center">
          Chúng tôi sẽ sử dụng thông tin này để cập nhật cho bạn về cuộc hẹn
          hoặc nếu cửa hàng sửa chữa có thêm câu hỏi về xe của bạn.
        </p>
        <div class="horizontal-separator mt-1"></div>
        <div class="form-step3 mt-3">
          <div class="row-100 flex-center">
            <minput
              label="Họ và tên đệm"
              name="FirstName"
              v-model="BookingInfo.last_name"
              ref="FirstName"
              placeholder-input="Họ và tên"
              rules="required|maxlength_100"
              formName="Booking"
              class="mb-2 row-50 mr-1"
            />
            <minput
              label="Tên"
              name="LastName"
              v-model="BookingInfo.first_name"
              ref="LastName"
              placeholder-input="LastName"
              rules="required|maxlength_100"
              formName="Booking"
              class="mb-2 row-50"
            />
          </div>
          <minput
            label="Email"
            name="Email"
            v-model="BookingInfo.email"
            ref="Email"
            placeholder-input="Email"
            rules="required|email"
            formName="Booking"
            class="mb-2 row-100"
          />
          <minput
            label="Số điện thoại"
            name="Phone"
            v-model="BookingInfo.phone"
            ref="Phone"
            placeholder-input="Số điện thoại"
            rules="required|maxlength_10"
            formName="Booking"
            class="mb-2 row-100"
          />
        </div>
      </div>
    </template>
    <template v-slot:main-info__1>
      <div class="ds-sub-heading-1 ng-font-neutral-60 ng-pb8 mt-2 mb-1">
        Thời gian sửa
      </div>
      <div class="flex-start">
        <div class="icon-clock flex-center"><micon type="Clock" /></div>
        <div class="ds-body-regular ng-font-neutral-80 ml-1">
          {{ computedDropOff }}
        </div>
      </div>
    </template>
    <template v-slot:main-info__2>
      <div class="ds-sub-heading-1 ng-font-neutral-60 ng-pb8 mt-2 mb-1">
        Tóm tắt nội dung
      </div>
      <div class="flex-start">
        <micon type="Car" />
        <div class="ng-pl8 ds-body-regular ng-font-neutral-80 ml-1">
          {{ bookingInfo.make }} {{ bookingInfo.model }} {{ bookingInfo.year }}
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
          <li>{{ bookingInfo.comment }}</li>
        </ul>
      </div>
    </template>
  </BaseBooking>
</template>

<script>
import { convertDate } from "@/utils/common";
import BaseBooking from "./BaseBooking.vue";
export default {
  name: "BookingStep3",
  extends: BaseBooking,
  components: {
    BaseBooking,
  },
  props: {
    bookingInfo: {
      type: Object,
      default: {},
    },
  },
  data() {
    return { booking_date: this.bookingInfo.booking_date };
  },
  methods: {
    /**
     * Handle the next step by validating and emitting the next step event.
     * @author nttue 12.03.2024
     */
    handleNextStep() {
      if (!this.handleValidate()) {
        this.$emit("nextStep", this.BookingInfo);
      }
    },
  },
  watch: {
    "bookingInfo.booking_date"(newValue) {
      if (newValue) {
        this.booking_date = newValue;
      }
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
};
</script>

<style scoped>
@import "./booking.scss";
</style>
