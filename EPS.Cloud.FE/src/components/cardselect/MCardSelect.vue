<template>
  <div class="card-wrapper">
    <div class="card-left">
      <div class="service-name">
        <span class="service-text">Thay dầu</span>
        <span class="service-icon"><micon type="Info" /></span>
      </div>
      <div class="keyword">
        <span class="keyword-icon"><micon type="Tag" /></span>
        <span class="keyword-text">Thay dầu nhớt</span>
      </div>
    </div>
    <div class="card-right">
      <div class="checkbox-wrapper">
        <input type="checkbox" class="select" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MCardSelect",
  props: {},
  data() {},
  watch: {},

  methods: {},
};
</script>

<style lang="scss">
@import "./cardselect.scss";
</style>
