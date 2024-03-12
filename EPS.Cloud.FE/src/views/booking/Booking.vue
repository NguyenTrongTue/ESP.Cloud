<template>
  <NavigationTop>
    <template v-slot:container>
      <div class="flex-between">
        <div class="top__left flex-center pointer">
          <micon type="ChevronLeft" />
          <span>{{ $t("i18nBooking.Back") }}</span>
        </div>
        <div class="top__center">
          {{ $t("i18nBooking.Title") }} {{ garage?.garage_name }}
        </div>
        <div class="top__right">
          <a href="/"
            ><div class="logo"><micon type="Logo" /></div
          ></a>
        </div>
      </div>
    </template>
  </NavigationTop>
  <!-- Your template code here -->
  <div class="appointment">
    <!-- Subheader drop off > vehicle > contact > sumary-->
    <div class="sub-header flex-center">
      <div
        v-for="item in listSteps"
        class="step flex-center"
        :class="{
          'step-active': currentStep == item.step,
          'step-done': currentStep > item.step,
        }"
      >
        <span class="step-name" @click="handleChooseStep(item)">{{
          item.text
        }}</span>
        <micon type="ChevronRight" v-if="item.step < listSteps.length" />
      </div>
    </div>
    <!-- Nội dung điền form đặt lịch sửa chữa-->
    <div class="main-content">
      <!-- Bước chọn ngày, giờ -->
      <div class="steps" v-if="currentStep === 1">
        <BookingStep1 @nextStep="handleNextStep2" :garageProps="garage" />
      </div>
      <!-- Bước chọn phương tiện -->
      <div class="steps" v-else-if="currentStep === 2">
        <BookingStep2
          :bookingInfo="BookingInfo"
          :garage-id="BookingInfo.garage_id"
          @nextStep="handleNextStep3"
          :garageProps="garage"
        />
      </div>
      <!-- Bước nhập thông tin liên lạc -->
      <div class="steps" v-else-if="currentStep === 3">
        <BookingStep3
          @nextStep="currentStep = 4"
          :garageProps="garage"
          :bookingInfo="BookingInfo"
        />
      </div>
      <!-- Bước tổng kết -->
      <div class="steps" v-else>
        <BookingStep4 @nextStep="handleSubmit" :garageProps="garage" />
      </div>
    </div>
  </div>
</template>

<script>
import BookingStep1 from "./BookingStep1.vue";
import BookingStep2 from "./BookingStep2.vue";
import BookingStep3 from "./BookingStep3.vue";
import BookingStep4 from "./BookingStep4.vue";
import NavigationTop from "@/components/navigation-top/NavigationTop.vue";
import GarageAPI from "@/apis/GarageAPI";
export default {
  name: "Booking",
  components: {
    BookingStep1,
    BookingStep2,
    BookingStep3,
    BookingStep4,
    NavigationTop,
  },
  data() {
    return {
      // Your data properties here
      currentStep: 1,
      listSteps: [
        {
          step: 1,
          text: "Chọn lịch",
        },
        {
          step: 2,
          text: "Phương tiện",
        },
        {
          step: 3,
          text: "Thông tin liên lạc",
        },
        {
          step: 4,
          text: "Tổng kết",
        },
      ],
      garage: null,
      BookingInfo: { garage_id: null },
    };
  },
  methods: {
    handleSubmit() {},
    handleChooseStep(item) {
      if (item.step > this.currentStep) {
        return;
      } else {
        this.currentStep = item.step;
      }
    },
    handleNextStep2(timeInfo) {
      this.currentStep = 2;
      this.BookingInfo.booking_date = timeInfo;
    },
    handleNextStep3(bookingInfo) {
      this.currentStep = 3;
      this.BookingInfo.cars_id = bookingInfo.cars_id;
      this.BookingInfo.make = bookingInfo.make;
      this.BookingInfo.year = bookingInfo.year;
      this.BookingInfo.model = bookingInfo.model;
    },
  },
  async mounted() {
    this.BookingInfo.garage_id = this.$route.params.id;

    const result = await GarageAPI.getGarageById(this.BookingInfo.garage_id);
    this.garage = result;
  },
};
</script>

<style scoped>
@import "./booking.scss";
</style>
