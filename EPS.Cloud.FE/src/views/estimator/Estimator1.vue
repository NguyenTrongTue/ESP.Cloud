<template>
  <div class="estimator-location">
    <div class="estimator-location-content">
      <h1 class="estimator-location-title">Nhập vị trí của bạn</h1>
      <div class="zipcode-selector">
        <div class="input-search">
          <span class="icon-search"><micon type="Location" /></span>
          <input type="text" class="zipcode-input" placeholder="Nhập mã zip" />
        </div>
        <div class="button-search">
          <button class="zipcode-button">
            <micon type="ArrowRight" />
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Estimator1",
  props: {},
  data() {},
  watch: {},

  methods: {},
};
</script>

<style lang="scss">
@import "./estimator.scss";
</style>
