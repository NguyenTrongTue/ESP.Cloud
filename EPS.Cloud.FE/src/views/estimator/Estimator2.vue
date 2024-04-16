<template>
  <div class="tell-vehicle">
    <div class="custom-location">
      <span class="icon-location">
        <micon type="Location" />
      </span>
      <span class="location-text">Địa điểm của bạn</span>
    </div>
    <div class="steps-left-tilte">
      <h2 class="vehicle-describe">Mô tả xe của bạn</h2>
    </div>
    <div class="steps-left-body">
      <div class="row-50">
        <mcombobox label="Hãng xe" default-text="Chọn hãng xe" class="mb-4" :items="makeList"
          v-model="objectMaster.make" :disabled="false" />
        <mcombobox label="Mẫu xe" default-text="Chọn mẫu xe" v-model="objectMaster.model" class="mb-4"
          :items="modelList" :disabled="!objectMaster.make" />
        <mcombobox label="Năm" default-text="Chọn năm" v-model="objectMaster.year" class="mb-4" :items="yearList"
          :disabled="!objectMaster.model" />
      </div>
    </div>
    <div class="next-steps" @click="nextStep">
      <mbutton buttonText="Tiếp theo" :disabled="computedDisableBtn" />
    </div>
  </div>
</template>

<script>

import carMixin from "@/mixins/carMixin.vue";
export default {
  name: "Estimator2",
  mixins: [carMixin],
  props: {
    estimateInfo: {
      type: Object,
      default: {},
    },
  },
  emits: ["nextStep"],
  data() {
    return {
      objectMaster: {},

    };
  },
  watch: {

  },
  mounted() {
    this.fetchMakes();
    if (this.estimateInfo.cars_id) {
      this.isFirstLoadPage = true;
      this.objectMaster = { ...this.estimateInfo };
    }
  },
  computed: {
    computedDisableBtn() {
      return !this.objectMaster.make || !this.objectMaster.year || !this.objectMaster.model;
    }
  },
  methods: {
    nextStep() {
      if (!this.computedDisableBtn) {

        this.$emit("nextStep", this.objectMaster);
      } else {
        return;

      }
    },


  },
};
</script>

<style lang="scss">
@import "./estimator.scss";
</style>
