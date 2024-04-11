<template>
  <div class="promo-filter">
    <h2 class="promo-filter-title">{{ title }}</h2>
    <div class="promo-filter-list">
      <div
        v-for="item in priceItems"
        :key="item.title"
        class="promo-filter-item"
        @click="selectPrice(item.title)"
      >
        <p>
          {{ item.title }}
        </p>
        <div class="checkbox-wrapper">
          <input type="checkbox" v-model="item.value" class="select" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "PromoFilter",
  data() {
    return {
      selectedItem: "", // Giá được chọn
      title: "Lọc theo thời gian",
      priceItems: [
        {
          title: "Hôm nay",
        },
        {
          title: "Tuần này",
        },
        {
          title: "Tháng này",
        },
        
      ],
    };
  },
  methods: {
    selectItem(title) {
      this.selectedItem= title;
      // Thực hiện các xử lý tương ứng 
    },
  },
};
</script>

<style lang="scss" scoped>
@import "./promofilter.scss";
</style>
