<template>

  <div class="questions__card ">
    <div class="questions__card_body">
      <div class="question__card_left">

        <img src="https://storage.googleapis.com/rp-production-public-content/xlxf7qw1lmi9owu8eowawx93e28w" alt="">

      </div>

      <a class="questions__card_right pointer" :href="computedLinkByTilte(questionCardProps)">
        <div class=" mb-2 mt-1">

          <span class="question__card_title"> Đánh giá xe {{ questionCardProps.car_name }}</span>
          <span class="questions__total">({{ questionCardProps.total_question }})</span>
        </div>

        <div class="rating_avg_wrapper">
          <div class="rating_avg" :class="computedRatingText.style">
            <span>{{ questionCardProps.rating_avg % 1 === 0 ? questionCardProps.rating_avg :
        questionCardProps.rating_avg.toFixed(1) }}</span>


          </div>

          <div class="rating_avg_reliablity">
            <div class="rating">Xếp hạng xe</div>
            <div class="rating_text">{{ computedRatingText.text }}</div>
          </div>


        </div>
      </a>
    </div>

    <div class="questions_card__popular"
      v-if="questionCardProps.list_popular && questionCardProps.list_popular.length > 0">
      <div class="questions_card__popular_title">
        Mẫu xe phổ biến: <span>&nbsp;</span>
      </div>
      <div class="list_popular">

        <span v-for="(item, index) in JSON.parse(questionCardProps.list_popular)" :key="item">
          <a class="popular_item" :href="computedLink(item)">{{ item }}</a>
          <span v-if="index < JSON.parse(questionCardProps.list_popular).length - 1">,
          </span>
        </span>

      </div>
    </div>
  </div>

</template>

<script>
export default {
  name: "QuestionsCard",
  props: {
    questionCardProps: {
      type: Object,
      defautl: {}
    }
  },
  data() {

    return {

    }
  },
  watch: {},
  computed: {
    computedRatingText() {
      return this.questionCardProps.rating_avg >= 4 ?


        { text: "Xuất xắc", style: 'excellent' } :
        this.questionCardProps.rating_avg >= 2 && this.questionCardProps.rating_avg < 4
          ?
          { text: "Trung bình", style: 'everage' } : this.questionCardProps.rating_avg > 0 && this.questionCardProps.rating_avg < 2 ? { text: "Thấp", style: 'low' } : { text: "Không khả dụng", style: 'unavailable' };
    },
    computedLink() {
      return (make) => {
        const carName = this.questionCardProps.car_name.replace(/ /g, "-");
        const formattedMake = make?.toString()?.replace(/ /g, "-") || "";
        return `${window.__baseURLFE}/car_review/${carName}/${formattedMake}`;
      };
    },
    computedLinkByTilte() {
      return questionCardProps => {

        const { year, model, make } = questionCardProps;
        const carName = make.replace(/ /g, "-");

        if (year) {
          const modelSlug = model?.replace(/ /g, "-") || "";
          return `${window.__baseURLFE}/car_review/${carName}/${modelSlug}/${year}`;
        }
        if (model) {
          const modelSlug = model?.replace(/ /g, "-") || "";
          return `${window.__baseURLFE}/car_review/${carName}/${modelSlug}`;
        }
        if (make) {
          return `${window.__baseURLFE}/car_review/${carName}`;
        }
      }
    }

  },

  methods: {


  },
};
</script>

<style lang="scss">
@import "./car-review.scss";
</style>
