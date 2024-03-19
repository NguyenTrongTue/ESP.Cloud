<template>
  <NavigationTop>
    <template v-slot:container>
      <div class="flex-between">
        <div class="top__left">
          <a href="/"
            ><div class="logo"><micon type="Logo" /></div
          ></a>
        </div>
        <div class="top__center flex-center">
          <div
            v-for="step in listSteps"
            :key="step.step"
            class="pointer"
            @click="handleChooseStep(step)"
          >
            <span
              :class="{
                active: currentStep == step.step,
              }"
            >
              {{ step.text }}</span
            >
          </div>
        </div>
        <div class="top__right ng-heading-5 ng-font-neutral-50 pointer">
          Thoát
        </div>
      </div>
    </template>
  </NavigationTop>
  <div class="progress-bar">
    <div
      class="progress-bar__step"
      :style="{ width: progressWidth + '%' }"
    ></div>
  </div>
  <div class="estimator">
    <div class="main-content">
      <div class="steps" v-if="currentStep == 1">
        <Estimator2 :estimateInfo="EstimateInfo" @nextStep="handleNextStep" />
      </div>
      <div class="steps" v-if="currentStep == 2">
        <Estimator3 :estimateInfo="EstimateInfo" @nextStep="handleNextStep" />
      </div>
      <div class="steps" v-if="currentStep == 3">
        <Estimator4 :estimateInfo="EstimateInfo" @nextStep="handleNextStep" />
      </div>
    </div>
  </div>
</template>

<script>
import Estimator1 from "./Estimator1.vue";
import Estimator2 from "./Estimator2.vue";
import Estimator3 from "./Estimator3.vue";
import Estimator4 from "./Estimator4.vue";
import NavigationTop from "@/components/navigation-top/NavigationTop.vue";
export default {
  name: "Estimator",
  components: {
    Estimator1,
    Estimator2,
    Estimator3,
    Estimator4,
    NavigationTop,
  },
  props: {},
  data() {
    return {
      currentStep: 1,
      progressWidth: 37,
      listSteps: [
        {
          step: 1,
          text: "Phương tiên giao thông",
        },
        {
          step: 2,
          text: "Dịch vụ sửa chữa",
        },
        {
          step: 3,
          text: "Ước tính",
        },
      ],
      makeList: [],
      yearList: [],
      modelList: [],
      modelAndIdCarList: [],
      EstimateInfo: {},
    };
  },
  watch: {},
  computed: {
    progressWidth() {
      return (this.currentStep / 3) * 100;
    },
  },

  methods: {
    handleChooseStep(item) {
      if (item.step > this.currentStep) {
        return;
      } else {
        this.currentStep = item.step;
      }
    },
    handleNextStep(info) {
      if (this.currentStep == 1) {
        this.EstimateInfo.cars_id = info.cars_id;
        this.EstimateInfo.make = info.make;
        this.EstimateInfo.year = info.year;
        this.EstimateInfo.model = info.model;
        this.EstimateInfo.comment = info.comment;
      } else if (this.currentStep == 2) {
        this.EstimateInfo.selectedServices = info;
      }
      if (this.currentStep < 3) {
        this.currentStep++;
      }
    },
  },
};
</script>

<style lang="scss" scoped>
@import "./estimator.scss";
</style>
