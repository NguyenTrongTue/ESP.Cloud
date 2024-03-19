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
      <mcardselect
        v-for="service in listServices"
        :key="service.sevice_code"
        :item="service"
      />
    </div>
    <div class="next-steps-4 mb-4" @click="nextStep">
      <mbutton buttonText="Nhận ước tính" />
    </div>
  </div>
</template>

<script>
import BookingAPI from "@/apis/BookingAPI";
export default {
  name: "Estimator3",
  emits: ["nextStep"],
  props: {
    estimateInfo: {
      type: Object,
      default: {},
    },
  },
  data() {
    return {
      listServices: [],
      carId: this.estimateInfo.cars_id,
    };
  },
  watch: {
    "estimateInfo.cars_id"(newValue) {
      if (newValue) {
        this.carId = newValue;
        this.fetchServices();
      }
    },
  },
  mounted() {
    this.fetchServices();
  },
  methods: {
    nextStep() {
      let servicesSelected = this.listServices.filter((item) => item.value);
      this.$emit("nextStep", servicesSelected);
    },
    async fetchServices() {
      if (this.carId) {
        const res = await BookingAPI.getGarageServices(this.carId);
        this.listServices = res.map((item) => {
          return {
            ...item,
            value: false,
          };
        });
      }
    },
  },
};
</script>

<style lang="scss">
@import "./estimator.scss";
</style>
