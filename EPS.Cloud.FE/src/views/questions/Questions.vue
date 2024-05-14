<template>

  <div class="questions">
    <div class="question__header">
      <div class="question__navigator mb-1">
        <div class="question__navigator_item" v-for="(item, index) in  listQuestionNavigator " :key="item.grade"><a
            class="question__navigator_text pointer" :class="{
          'active':
            index === listQuestionNavigator.length - 1
        }" :href="handleNavigator(item)">{{ item.text }}</a>
          <span v-if="index < listQuestionNavigator.length - 1">&nbsp/&nbsp</span>
        </div>

      </div>
      <div class="question__header__title">
        {{ titleQuestions }}
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

        <mcombobox :hasBorder="true" default-text="Chọn mẫu xe" v-model="objectMaster.model" class="combobox_item"
          :items="modelList" :disabled="!objectMaster.make" />

        <mcombobox :hasBorder="true" default-text="Chọn năm" v-model="objectMaster.year" class="combobox_item"
          :items="yearList" :disabled="!objectMaster.model" />

        <div class="combobox_item flex-center">
          <mbutton button-text="Tìm câu hỏi" :disabled="computedDisableBtn" type="link"
            :href="computedLinkByTilte(objectMaster)" />
        </div>
      </div>
    </div>
    <div class=" horizontal-separator mt-4">
    </div>
    <div class="mt- mb-1 quetions__header_description mt-4">

      Tìm kiếm một câu hỏi cụ thể?
    </div>
    <div class="search_input flex-between">
      <div class="search_wrapper">
        <minput v-model="question" class="quetions_input" ref="quetions" placeholderInput="Nhập tiêu đề câu hỏi"
          @update="handleSearchQuestions" />
        <div class="list_search" v-if="listSearchQuestions.length > 0">
          <div class="search_item flex-between" v-for="( searchItem ) in  listSearchQuestions "
            :key="searchItem.questions_id" @click="handleRedirectToAnswer(searchItem.questions_id)">
            <span>{{ searchItem.questions_title }}</span>

            <micon type="ChevronRight" />
          </div>

        </div>
      </div>
      <div class="search_button flex-center " @click="handleShowPopupQuestions">
        <mbutton button-text="Đặt câu hỏi" style="height: 46px;" class="none-background" />
      </div>

    </div>
    <div class="mt- mb-1 quetions__header_description mt-4">
      {{ titleListQuestions }}
    </div>
    <div class="questions__list">
      <QuetionsCard v-for="( item, index ) in  listQuestions " :questionCardProps="item" :key="index" />
      <QuetionsModal ref="QuestionsModal" />
    </div>
    <div class="mt- mb-1 quetions__header_description mt-4">
      {{ titlePopularQuestions }}
    </div>
    <div class="list_answer ">

      <AnswerCard v-for=" answer  in  paginationAnswerList " :key="answer.questions_id" :answer="answer"
        @click="handleRedirectToAnswer(answer.questions_id)" />


    </div>
    <pagination :currentPage="currentPage" @handleChangePage="handleChangePage" :length="listAnswerRecently?.length" />
  </div>
</template>

<script>
import QuetionsCard from './QuestionsCard.vue';
import QuetionsModal from './QuetionsModal.vue';
import AnswerCard from './AnswerCard.vue';
import carMixin from "@/mixins/carMixin.vue";
import QuestionAPI from '@/apis/QuestionsAPI';
import AnswerAPI from '@/apis/AnswerAPI';
import debounce from '@/utils/debounce';
import BaseQuestion from './BaseQuestion.vue';

export default {
  name: "Questions",
  mixins: [carMixin],
  extends: BaseQuestion,
  components: {
    QuetionsCard,
    QuetionsModal,
    AnswerCard
  },
  props: {},
  computed: {
    computedDisableBtn() {
      return !this.objectMaster.make || !this.objectMaster.year || !this.objectMaster.model
    },
    computedLinkByTilte() {
      return objectMaster => {
        const { year, model, make } = objectMaster;
        const carName = make?.replace(/ /g, "_");
        const modelSlug = model?.replace(/ /g, "_") || "";
        return `${window.__baseURLFE}/questions/${carName}/${modelSlug}/${year}`;
      }
    },
    paginationAnswerList() {
      return this.listAnswerRecently.slice(this.currentPage * 5, this.currentPage * 5 + 5);
    }
  },
  data() {

    return {

      question: "",
      currentPage: 0,
      listQuestions: [],
      listSearchQuestions: [],
      listAnswerRecently: []

    }
  },
  watch: {
    question: debounce(async function (newValue) {
      if (newValue && newValue.trim()) {
        const result = await QuestionAPI.searchQuestionByTitle(newValue);
        this.listSearchQuestions = result;
      } else {
        this.listSearchQuestions = []
      }
    }, 500),
  },
  mounted() {
    this.fetchMakes();
    // this.fetchQuestionPopular();
    // this.fetchAnswerRecently();

    this.handleStartQuestionsPage();

  },

  methods: {
    handleNavigator(item) {
      console.log(item);
      let link = `${window.__baseURLFE}/questions/`;
      switch (item.grade) {
        case 1:
          return link + `${item.text}`;
        case 2:
          return link + `${this.listQuestionNavigator[1].text}/${this.listQuestionNavigator[2].text}`;

        case 3:
          return link + `${this.listQuestionNavigator[1].text}/${this.listQuestionNavigator[2].text}/${this.listQuestionNavigator[3].text}`;
        default:
          return link;

      }
    },
    handleChangePage(page) {
      this.currentPage = page;
    },
    handleRedirectToAnswer(questionsId) {
      this.$router.push(`/answer/${questionsId}`);
    },
    /**
     * Handles showing the popup questions modal.
     */
    handleShowPopupQuestions() {
      this.$refs['QuestionsModal'].show();
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
