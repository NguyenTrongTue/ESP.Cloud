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
          :disabled="false"
        />
        <mcombobox
          label="Năm"
          default-text="Chọn năm"
          class="mb-4"
          :items="yearList"
        />
        <mcombobox
          label="Mẫu xe"
          default-text="Chọn mẫu xe"
          class="mb-4"
          :items="modelList"
        />
      </div>
    </div>
    <div class="next-steps">
      <mbutton buttonText="Tiếp theo" />
    </div>
  </div>
</template>

<script>
export default {
  name: "Estimator2",
  props: {},
  data() {
    return {
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
