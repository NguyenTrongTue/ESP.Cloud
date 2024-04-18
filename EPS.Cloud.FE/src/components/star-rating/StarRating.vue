<!-- MyComponent.vue -->
<template>
  <vue3starRatings v-model="myRating" :starSize="size" starColor="#ff9800" inactiveColor="#333333" :numberOfStars="5"
    :disableClick="disableClick" />

</template>

<script>
import vue3starRatings from "vue3-star-ratings";

export default {
  components: {
    vue3starRatings,
  },
  props: {
    rating: {
      type: Number,
      default: 4
    },
    disableClick: {
      type: Boolean,
      default: true,
    },
    size: {
      type: Number,
      default: 16
    }
  },
  data() {
    return {
      myRating: this.rating,
    };
  },
  watch: {
    rating(newVaue) {
      this.myRating = newVaue;
    }
  }
};
</script>
