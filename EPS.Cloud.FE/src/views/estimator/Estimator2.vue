<template>
  <div class="tell-vehicle">
    <div class="custom-location">
      <span class="icon-location"><micon type="Location" /></span>
      <span class="location-text">Địa điểm của bạn</span>
    </div>
    <div class="steps-left-tilte">
      <h2 class="vehicle-describe">Mô tả xe của bạn</h2>
    </div>
    <div class="steps-left-body">
      <div class="row-50">
        <mcombobox
          label="Hãng xe"
          default-text="Chọn hãng xe"
          class="mb-4"
          :items="makeList"
          v-model="EstimateInfo.make"
          :disabled="false"
        />
        <mcombobox
          label="Năm"
          default-text="Chọn năm"
          v-model="EstimateInfo.year"
          class="mb-4"
          :items="yearList"
          :disabled="!EstimateInfo.make"
        />
        <mcombobox
          label="Mẫu xe"
          default-text="Chọn mẫu xe"
          v-model="EstimateInfo.model"
          class="mb-4"
          :items="modelList"
          :disabled="!EstimateInfo.year"
        />
      </div>
    </div>
    <div class="next-steps" @click="nextStep">
      <mbutton buttonText="Tiếp theo" />
    </div>
  </div>
</template>

<script>
import BookingAPI from "@/apis/BookingAPI";
export default {
  name: "Estimator2",
  props: {
    estimateInfo: {
      type: Object,
      default: {},
    },
  },
  emits: ["nextStep"],
  data() {
    return {
      makeList: [],
      yearList: [],
      modelList: [],
      modelAndIdCarList: [],
      garageId: "00000000-0000-0000-0000-000000000000",
      EstimateInfo: {},
    };
  },
  watch: {
    "EstimateInfo.make"(newValue) {
      if (newValue) {
        this.fetchYears();
        this.EstimateInfo.year = null;
        this.EstimateInfo.model = null;
      }
    },
    "EstimateInfo.year"(newValue) {
      if (newValue) {
        this.fetchModel();
        this.EstimateInfo.model = null;
      }
    },
    "EstimateInfo.model"(newValue) {
      if (newValue) {
        this.EstimateInfo.cars_id = this.modelAndIdCarList.find(
          (item) => item.model == newValue
        ).cars_id;
      }
    },
  },
  mounted() {
    this.fetchMakes();
  },
  methods: {
    nextStep() {
      this.$emit("nextStep", this.EstimateInfo);
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
          this.EstimateInfo.make
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
          this.EstimateInfo.make,
          this.EstimateInfo.year
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

<style lang="scss">
@import "./estimator.scss";
</style>
