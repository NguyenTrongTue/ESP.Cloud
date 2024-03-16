<template>
  <div class="categories">
    <div class="custom-location">
      <span class="icon-location"><micon type="Location" /></span>
      <span class="location-text">Địa điểm của bạn</span>
      <span class="icon-car"><micon type="Car" /></span>
      <span class="make-text">Mẫu xe</span>
    </div>
    <div class="steps-left-tilte">
      <h2 class="vehicle-describe">Chọn dịch vụ sửa chữa</h2>
    </div>
    <div class="service-list">
      <mcardselect />
      <mcardselect />
    </div>
    <div class="next-steps-4">
      <mbutton buttonText="Nhận ước tính" />
    </div>
  </div>
</template>

<script>
export default {
  name: "Estimator3",
  props: {},
  data() {},
  watch: {},

  methods: {},
};
</script>

<style lang="scss">
@import "./estimator.scss";
</style>
