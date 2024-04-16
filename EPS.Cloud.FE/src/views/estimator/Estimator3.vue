<template>
  <div class="categories">
    <div class="custom-location">
      <span class="icon-location">
        <micon type="Location" />
      </span>
      <span class="location-text">Địa điểm của bạn</span>
      <span class="icon-car">
        <micon type="Car" />
      </span>
      <span class="make-text">Mẫu xe</span>
    </div>
    <div class="steps-left-tilte">
      <h2 class="vehicle-describe">Chọn dịch vụ sửa chữa</h2>
    </div>
    <div class="service-list">
      <mcardselect v-for="service in listServices" :key="service.sevice_code" :item="service" />
    </div>
    <div class="next-steps-4 mb-4" @click="nextStep">
      <mbutton buttonText="Nhận ước tính" :disabled="computedDisableBtn" />
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
  computed: {
    computedDisableBtn() {
      return !this.listServices.filter(item => item.value).length > 0;
    }
  },
  watch: {
    "estimateInfo.cars_id"(newValue) {
      if (newValue) {
        this.carId = newValue;
        this.fetchServices();
      }
    },
  },
  async mounted() {
    await this.fetchServices();
    if (this.estimateInfo.selectedServices) {
      let selectedServices = this.estimateInfo.selectedServices;
      this.listServices.forEach(service => {
        if (selectedServices.some(item => item.service_code === service.service_code)) {
          service.value = true;
        }
      });
    }

  },
  methods: {
    nextStep() {
      if (!this.computedDisableBtn) {
        let servicesSelected = this.listServices.filter((item) => item.value);
        this.$emit("nextStep", servicesSelected);
      } else {
        return;
      }
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
