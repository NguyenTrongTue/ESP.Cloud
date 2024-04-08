<template>
  <div class="answer_card">
    <div class="answer_card__tile">
      {{ answer.questions_title }}

    </div>

    <div class="answer_card__body mt-1">
      <div class="anser_card__item">
        <micon type="Check" class="anser_card__item-icon" />
        <span>{{ answer.answer_total }} người đã trả lời câu hỏi này</span>

      </div>
      <div class="anser_card__item">
        <micon type="Car" class="anser_card__item-icon" />
        <span>{{ answer.make }} {{ answer.model }} {{ answer.year }}</span>

      </div>
    </div>


  </div>


</template>

<script>
export default {
  name: "AnswerCard",
  props: {

    answer: {
      type: Object,
      default: {}
    }
  },
  data() { },
  watch: {},

  methods: {},
};
</script>

<style lang="scss">
@import "./questions.scss";
</style>
