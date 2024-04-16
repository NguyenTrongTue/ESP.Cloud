<template>

  <div class="questions__card ">
    <div class="questions__card_body">
      <div class="question__card_left">

        <img src="https://storage.googleapis.com/rp-production-public-content/xlxf7qw1lmi9owu8eowawx93e28w" alt="">

      </div>

      <a class="questions__card_right pointer" :href="computedLinkByTilte(questionCardProps)">
        <div class=" mb-4 mt-1">

          <span class="question__card_title"> Câu hỏi {{ questionCardProps.car_name }}</span>
          <span class="questions__total">({{ questionCardProps.total_question }})</span>
        </div>

        <span class="view_all">Xem tất cả</span>
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
    computedLink() {
      return (make) => {
        const carName = this.questionCardProps.car_name.replace(/ /g, "-");
        const formattedMake = make?.toString()?.replace(/ /g, "-") || "";
        return `${window.__baseURLFE}/questions/${carName}/${formattedMake}`;
      };
    },
    computedLinkByTilte() {
      return questionCardProps => {

        const { year, model, make } = questionCardProps;
        const carName = make.replace(/ /g, "-");

        if (year) {
          const modelSlug = model?.replace(/ /g, "-") || "";
          return `${window.__baseURLFE}/questions/${carName}/${modelSlug}/${year}`;
        }
        if (model) {
          const modelSlug = model?.replace(/ /g, "-") || "";
          return `${window.__baseURLFE}/questions/${carName}/${modelSlug}`;
        }
        if (make) {
          return `${window.__baseURLFE}/questions/${carName}`;
        }
      }
    }

  },

  methods: {


  },
};
</script>

<style lang="scss">
@import "./questions.scss";
</style>
