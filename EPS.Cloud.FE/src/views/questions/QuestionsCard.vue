<template>

  <div class="questions__card ">
    <div class="questions__card_body">
      <div class="question__card_left">

        <img src="https://storage.googleapis.com/rp-production-public-content/xlxf7qw1lmi9owu8eowawx93e28w" alt="">

      </div>

      <div class="questions__card_right pointer">
        <div class="flex-start mb-4 mt-1">

          <div class="question__card_title"> Câu hỏi {{ questionCardProps.car_name }}</div>
          <div class="questions__total">({{ questionCardProps.total_question }})</div>
        </div>

        <span class="view_all">Xem tất cả</span>
      </div>
    </div>

    <div class="questions_card__popular">
      <div class="questions_card__popular_title">
        Mẫu xe phổ biến: <span>&nbsp;</span>
      </div>
      <div class="list_popular">

        <span v-for="(item, index) in JSON.parse(questionCardProps.list_popular)" :key="item">
          <span class="popular_item">{{ item }}</span><span
            v-if="index < JSON.parse(questionCardProps.list_popular).length - 1">,
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

  methods: {},
};
</script>

<style lang="scss">
@import "./questions.scss";
</style>
