<template>
  <div class="estimator">
    <div class="main-content">
      <div class="steps" v-if="step === 1">
        <Estimator1 />
      </div>
      <div class="steps" v-if="step === 2">
        <Estimator2 />
      </div>
      <div class="steps" v-if="step === 3">
        <Estimator3 />
      </div>
      <div class="steps" v-if="step === 4">
        <Estimator4 />
      </div>
    </div>
  </div>
</template>

<script>
import Estimator1 from "./Estimator1.vue";
import Estimator2 from "./Estimator2.vue";
import Estimator3 from "./Estimator3.vue";
import Estimator4 from "./Estimator4.vue";
export default {
  name: "Estimator",
  components: {
    Estimator1,
    Estimator2,
    Estimator3,
    Estimator4,
  },
  props: {},
  data() {
    return {
      step: 4,
      makeList: [],
      yearList: [],
      modelList: [],
      modelAndIdCarList: [],
    };
  },
  watch: {},

  methods: {},
};
</script>

<style lang="scss">
@import "./estimator.scss";
</style>
