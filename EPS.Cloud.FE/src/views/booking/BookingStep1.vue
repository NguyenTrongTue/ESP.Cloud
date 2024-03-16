<template>
  <BaseBooking :garageProps="garageProps" @nextStep="handleNextStep">
    <template v-slot:container>
      <div class="steps-left-tilte">
        <h2>Khi nào bạn muốn sửa?</h2>
      </div>
      <div class="steps-left-body">
        <date-picker
          label=""
          v-model="BookingInfo.date"
          name="DateOfBirth"
          ref="DateOfBirth"
          rules="date"
          tabIndex="3"
          class="grid__column-4"
        />
      </div>
      <div class="steps-left-footer">
        <p class="ds-sub-heading-1 mt-3">{{ buildText }}</p>

        <div class="time-list">
          <div
            v-for="(item, index) in timeList"
            :key="index"
            class="time-slots"
            @click="handleSelectedTime(item)"
            :class="{
              'time-picker-selected': item.value == BookingInfo.timeSelected,
            }"
          >
            <span> {{ item.text }}</span>
          </div>
        </div>
      </div>
    </template>
  </BaseBooking>
</template>

<script>
import { buildOpenTimeText } from "@/utils/common";
import BaseBooking from "./BaseBooking.vue";
export default {
  extends: BaseBooking,
  name: "BookingStep1",
  emits: ["nextStep"],
  components: {
    BaseBooking,
  },

  props: {},
  data() {
    var currentDate = new Date();
    return {
      garage: this.garageProps,
      BookingInfo: {
        date: new Date().setDate(currentDate.getDate() + 1),
        timeSelected: null,
      },
      timeList: [],
    };
  },
  mounted() {
    // this.calculationTimeList();
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
          this.calculationTimeList();
        }
      },
      deep: true,
    },
  },
  computed: {
    /**
     * A function to build and return a formatted text representing the date.
     *
     * @return {string} The formatted text representing the date.
     */
    buildText() {
      const { day, month, year } = {
        day: new Date(this.BookingInfo.date).getDate(),
        month: new Date(this.BookingInfo.date).getMonth() + 1,
        year: new Date(this.BookingInfo.date).getFullYear(),
      };
      return `Thời gian ngày ${day} tháng ${month} năm ${year}`;
    },
  },
  methods: {
    /**
     * A method to handle moving to the next step.
     *
     * @param {Date} date - the date to adjust
     * @param {string} timeSelected - the selected time
     * @return {void}
     */
    handleNextStep() {
      let timeInfo = this.adjustTime(
        new Date(this.BookingInfo.date),
        this.BookingInfo.timeSelected
      );

      this.$emit("nextStep", timeInfo);
    },
    /**
     * Adjusts the time of the original date by adding the specified number of hours.
     *
     * @param {Date} originalDate - the original date to be adjusted
     * @param {number} hoursToAdd - the number of hours to add to the original date
     * @return {Date} the adjusted date
     */
    adjustTime(originalDate, hoursToAdd) {
      // Tạo một bản sao của đối tượng Date
      var adjustedDate = new Date(originalDate);
      adjustedDate.setHours(0);
      adjustedDate.setMinutes(0);
      adjustedDate.setSeconds(0);
      // Lấy giờ và phút từ số giờ cần thêm
      var additionalHours = Math.floor(hoursToAdd);
      var additionalMinutes = (hoursToAdd % 1) * 60;

      // Thêm giờ và phút vào ngày đã có
      adjustedDate.setHours(adjustedDate.getHours() + additionalHours);
      adjustedDate.setMinutes(adjustedDate.getMinutes() + additionalMinutes);

      return adjustedDate;
    },

    /**
     * Calculate the list of available times for booking based on the garage's open and close times.
     *
     * @param None
     * @return None
     */
    calculationTimeList() {
      const { open_time, close_time } = this.garage;

      let startHour =
          open_time.split(":")[1] == 30
            ? +open_time.split(":")[0] + 0.5
            : +open_time.split(":")[0],
        endHour =
          close_time.split(":")[1] == 30
            ? +close_time.split(":")[0] + 0.5
            : +close_time.split(":")[0];
      this.BookingInfo.timeSelected = +startHour;
      for (let i = startHour; i <= endHour; i += 0.5) {
        if (i >= 0 && i < 12) {
          this.timeList.push({
            text: i % 1 == 0 ? `${i}:00 am` : `${i - 0.5}:30 am`,
            value: i,
          });
        } else {
          this.timeList.push({
            text:
              i % 1 == 0
                ? `${i == 12 ? i : i - 12}:00 pm`
                : `${i == 12.5 ? i - 0.5 : i - 12.5}:30 pm`,
            value: i,
          });
        }
      }
    },
    handleSelectedTime(time) {
      this.BookingInfo.timeSelected = time.value;
    },
  },
};
</script>

<style scoped>
@import "./booking.scss";
.steps-left-body {
  height: 430px;
}
.steps-left-footer {
  border-top: 1px solid #b8c2cc;
  p {
    text-align: center;
  }
}
.next-step {
  gap: 4px;
  font-weight: 500;
}
</style>
