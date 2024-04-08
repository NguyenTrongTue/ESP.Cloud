<template>

  <div class="questions">
    <div class="question__header">

      <div class="question__header__title">
        Thảo luận về ô tô
      </div>


      <div class="quetions__header_description mb-2">
        Nhận câu trả lời cho các vấn đề sửa chữa ô tô và câu hỏi về xe của bạn. Hỏi một thợ cơ khí để được giúp đỡ và
        quay lại đường.</div>

      <div class="mt-4 mb-1 quetions__header_description">

        Tìm kiếm câu hỏi cho chiếc xe của bạn
      </div>
      <div class="find_quetions__top ">

        <mcombobox :hasBorder="true" default-text="Chọn hãng xe" class="combobox_item" :items="makeList"
          v-model="objectMaster.make" :disabled="false" />
        <mcombobox :hasBorder="true" default-text="Chọn năm" v-model="objectMaster.year" class="combobox_item"
          :items="yearList" :disabled="!objectMaster.make" />
        <mcombobox :hasBorder="true" default-text="Chọn mẫu xe" v-model="objectMaster.model" class="combobox_item"
          :items="modelList" :disabled="!objectMaster.year" />
        <div class="combobox_item">
          <mbutton button-text="Tìm câu hỏi" />
        </div>
      </div>
    </div>
    <div class="horizontal-separator mt-4"></div>
    <div class="mt- mb-1 quetions__header_description mt-4">

      Tìm kiếm một câu hỏi cụ thể?
    </div>
    <div class="search_input ">
      <minput v-model="question" class="quetions_input" ref="quetions" />
      <div class="search_button flex-center " @click="handleShowPopupQuestions">
        <mbutton button-text="Đặt câu hỏi" style="height: 46px;" class="none-background" />
      </div>

    </div>
    <div class="mt- mb-1 quetions__header_description mt-4">
      Câu hỏi về mẫu xe
    </div>
    <div class="questions__list">
      <QuetionsCard v-for="(item, index) in listQuestions" :questionCardProps="item" :key="index" />
      <QuetionsModal ref="QuestionsModal" />
    </div>
    <div class="mt- mb-1 quetions__header_description mt-4">
      Câu hỏi gần đây
    </div>
    <div class="list_answer ">

      <AnswerCard v-for="answer in listAnswerRecently" :key="answer.questions_id" :answer="answer"
        @click="handleRedirectToAnswer(answer.questions_id)" />
    </div>
  </div>
</template>

<script>
import QuetionsCard from './QuestionsCard.vue';
import QuetionsModal from './QuetionsModal.vue';
import AnswerCard from './AnswerCard.vue';
import carMixin from "@/mixins/carMixin.vue";
import QuestionAPI from '@/apis/QuestionsAPI';
import AnswerAPI from '@/apis/AnswerAPI';
export default {
  name: "Questions",
  mixins: [carMixin],
  components: {
    QuetionsCard,
    QuetionsModal,
    AnswerCard
  },
  props: {},
  data() {

    return {
      question: "",
      answer: {
        question_title: " Mã lỗi P1700 Làm cách nào để cài đặt phần mềm",
        answer_total: 10,
        make: "Acura",
        model: "Acura MDX",
        year: 2024
      },
      listQuestions: [

      ],
      listAnswerRecently: [

      ]
    }
  },

  mounted() {
    this.fetchMakes();
    this.fetchQuestionPopular();
    this.fetchAnswerRecently();
  },
  methods: {
    handleRedirectToAnswer(questionsId) {
      this.$router.push(`/answer/${questionsId}`);
    },
    /**
     * Handles showing the popup questions modal.
     */
    handleShowPopupQuestions() {
      this.$refs['QuestionsModal'].show();
    },
    /**
     * Fetches popular questions asynchronously.
     *
     * @return {Promise} The list of popular questions.
     */
    async fetchQuestionPopular() {
      const result = await QuestionAPI.getQuestionPopular();
      this.listQuestions = result;
    },

    async fetchAnswerRecently() {
      const result = await AnswerAPI.getAnswerRecently();
      this.listAnswerRecently = result;
    }
  },
};
</script>

<style lang="scss">
@import "./questions.scss";
</style>
