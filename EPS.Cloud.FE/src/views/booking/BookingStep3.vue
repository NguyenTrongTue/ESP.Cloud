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
  </BaseBooking>
</template>

<script>
import { buildOpenTimeText } from "@/utils/common";
import BaseBooking from "./BaseBooking.vue";
export default {
  name: "BookingStep3",
  extends: BaseBooking,
  components: {
    BaseBooking,
  },
  props: {},
  data() {
    return {};
  },
  methods: {
    handleNextStep() {
      if (!this.handleValidate()) {
        this.$emit("nextStep");
      }
    },
  },
};
</script>

<style scoped>
@import "./booking.scss";
</style>
