<template>
  <NavigationTop>
    <template v-slot:container>
      <div class="flex-between">
        <div class="top__left flex-center pointer" @click="handleBack">
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
          @nextStep="handleNextStep4"
          :garageProps="garage"
          :bookingInfo="BookingInfo"
        />
      </div>
      <!-- Bước tổng kết -->
      <div class="steps" v-else>
        <BookingStep4
          @nextStep="handleSubmit"
          :garageProps="garage"
          :bookingInfo="BookingInfo"
        />
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
import BookingAPI from "@/apis/BookingAPI";
export default {
  name: "Booking",
  components: {
    BookingStep1,
    BookingStep2,
    BookingStep3,
    BookingStep4,
    NavigationTop,
  },
  /**
   * A method to initialize the data properties for the component.
   *
   * @return {object} The initialized data properties
   * @author nttue 12.03.2024
   */
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
    handleBack() {
      this.$router.push({
        path: "/",
      });
    },
    /**
     * Handles the submission process, including showing loading state, posting data to BookingAPI,
     * and displaying success or error messages.
     *
     * @param {void} None
     * @return {Promise<void>} A promise that resolves when the submission process is complete
     */
    async handleSubmit() {
      try {
        this.$store.commit("showLoading");
        const value = this.$common.cache.getCache("user");
        this.BookingInfo.user_id = value.user_id;
        await BookingAPI.post(this.BookingInfo);
        this.$store.commit("hideLoading");

        this.$store.commit("dialog/showMessage", {
          title: "Yêu cầu cuộc hẹn đã được gửi",
          content: "Vui lòng kiểm tra email để xác nhận chi tiết về cuộc hẹn!",
          callback: () => {
            this.$router.push({
              path: "/",
            });
          },
        });
      } catch (e) {
        this.$store.commit("hideLoading");
        console.log(e);
      }
    },
    /**
     * Handle choosing a step based on the item.
     *
     * @param {type} item - the item to determine the step from
     * @author nttue 12.03.2024
     */
    handleChooseStep(item) {
      if (item.step > this.currentStep) {
        return;
      } else {
        this.currentStep = item.step;
      }
    },
    /**
     * Sets the current step to 2 and updates the booking date in BookingInfo.
     *
     * @param {type} timeInfo - the time information to set as the booking date
     * @return {type}
     * @author nttue 12.03.2024
     */
    handleNextStep2(timeInfo) {
      this.currentStep = 2;
      this.BookingInfo.booking_date = timeInfo;
    },
    /**
     * Set the current step to 3 and update booking information with the provided details.
     *
     * @param {Object} bookingInfo - Object containing car details to update the booking information
     * @author nttue 12.03.2024
     */
    handleNextStep3(bookingInfo) {
      this.currentStep = 3;
      this.BookingInfo.cars_id = bookingInfo.cars_id;
      this.BookingInfo.make = bookingInfo.make;
      this.BookingInfo.year = bookingInfo.year;
      this.BookingInfo.model = bookingInfo.model;
      this.BookingInfo.comment = bookingInfo.comment;
    },
    /**
     * Updates the current step to 4 and copies the last name, first name, email, and phone from bookingInfo.
     *
     * @param {Object} bookingInfo - The object containing booking information
     * @author nttue 12.03.2024
     */
    handleNextStep4(bookingInfo) {
      this.currentStep = 4;
      this.BookingInfo.last_name = bookingInfo.last_name;
      this.BookingInfo.first_name = bookingInfo.first_name;
      this.BookingInfo.email = bookingInfo.email;
      this.BookingInfo.phone = bookingInfo.phone;
    },
  },
  /**
   * Set the garage_id in BookingInfo from route params and fetch garage data.
   *
   * @return {Promise<void>}
   * @author nttue 12.03.2024
   */
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
