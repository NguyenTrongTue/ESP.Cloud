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
        Đọc những trải nghiệm thực tế từ những người có xe như bạn.</div>

      <div class="mt-4 mb-1 quetions__header_description">

        Nhập chi tiết xe của bạn để tìm đánh giá
      </div>
      <div class="find_quetions__top ">

        <mcombobox :hasBorder="true" default-text="Chọn hãng xe" class="combobox_item" :items="makeList"
          v-model="objectMaster.make" :disabled="false" />

        <mcombobox :hasBorder="true" default-text="Chọn mẫu xe" v-model="objectMaster.model" class="combobox_item"
          :items="modelList" :disabled="!objectMaster.make" />

        <mcombobox :hasBorder="true" default-text="Chọn năm" v-model="objectMaster.year" class="combobox_item"
          :items="yearList" :disabled="!objectMaster.model" />

        <div class="combobox_item flex-center">
          <mbutton button-text="Tìm đánh giá" :disabled="computedDisableBtn" type="link"
            :href="computedLinkByTilte(objectMaster)" />
        </div>
      </div>
    </div>

    <div class=" horizontal-separator mt-4">
    </div>
    <div class="mb-2 quetions__header_description mt-4 ">
      {{ topRatingTitle }}
      <div>
        <div class="top_rating_description mt-1 mb-2">
          <span v-if="currentStep === 0">
            Xe <a class="top_rating__link" :href="computedLinkToTopRating(objectOverviewMaster)">{{
          objectOverviewMaster.make }} {{ objectOverviewMaster.model }} {{ objectOverviewMaster.year }}</a>
            đánh giá cao
            nhất
          </span>
          <span v-else>
            Tổng cộng có <span class="bold">{{ objectOverviewMaster.total_rating }}</span> lượt đánh giá xe
            {{ objectOverviewMaster.make }}. Xếp hạng trung bình <span class="bold">{{ objectOverviewMaster.avg_rating
              }}</span> trên 5 sao.
          </span>
        </div>
        <div class="top_rating_view mt-1 mb-2">
          <starrating :rating="objectOverviewMaster?.avg_rating" size="20" @update="handleUpdateRating" />
          <span class="top_rating_view_desc">Tổng số {{ objectOverviewMaster?.total_rating }} đáng giá</span>
        </div>
        <CarRating :objectOverviewMaster="objectOverviewMaster" />

      </div>
    </div>

    <div class="car_review__card mt-4">
      <div class="car_review__card_left">
        <img src="https://storage.googleapis.com/rp-production-public-content/nymoAnxCVpaLxthj8cUnjqHw"
          alt="Ảnh của xe" />


      </div>
      <div class="car_review__card_right">
        <div class="title mb-1">Xe của bạn có đáng giá 5 sao không?</div>
        <div class="description mb-1 mb-2">Hãy để khách truy cập Garahunt biết bạn nghĩ gì về chiếc xe của mình.</div>

        <mbutton button-text="Đánh giá xe của bạn" @click="handleShowPopupQuestions" />
      </div>
    </div>

    <div class="mt- mb-1 quetions__header_description mt-4">
      {{ titleListQuestions }}
    </div>
    <div class="questions__list mb-4">
      <QuetionsCard v-for="( item, index ) in  listQuestions " :questionCardProps="item" :key="index" />
      <QuetionsModal ref="QuestionsModal" />
    </div>
    <div class="mt- mb-1 quetions__header_description mt-4">
      Một số đánh giá phổ biến
    </div>
    <AnswerCard v-for=" answer  in  paginationAnswerList " :key="answer.questions_id" :answer="answer" />

    <pagination :currentPage="currentPage" @handleChangePage="handleChangePage" :length="listAnswerRecently?.length" />
  </div>

</template>

<script>
import QuetionsCard from './QuestionsCard.vue';
import QuetionsModal from './QuetionsModal.vue';
import AnswerCard from './AnswerCard.vue';
import carMixin from "@/mixins/carMixin.vue";
import QuestionAPI from '@/apis/QuestionsAPI';
import debounce from '@/utils/debounce';
import BaseCarReview from './BaseCarReview.vue';
import CarRating from './CarRating.vue';
export default {
  name: "CarReview",
  mixins: [carMixin],
  extends: BaseCarReview,
  components: {
    QuetionsCard,
    QuetionsModal,
    AnswerCard, CarRating
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
        return `${window.__baseURLFE}/car_review/${carName}/${modelSlug}/${year}`;
      }
    },
    paginationAnswerList() {
      return this.listAnswerRecently.slice(this.currentPage * 5, this.currentPage * 5 + 5);
    }
  },
  data() {
    return {
      question: "",
      listQuestions: [],
      listSearchQuestions: [],
      listAnswerRecently: [],
      currentPage: 0,
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
    this.handleStartQuestionsPage();
  },
  methods: {
    handleNavigator(item) {

      let link = `${window.__baseURLFE}/car_review/`;
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
    /**
     * Generates a link to the top rating for a given car review.
     *
     * @param {Object} objectOverviewMaster - The object containing the car review details.
     * @return {string} The generated link to the top rating for the car review.
     */
    computedLinkToTopRating(objectOverviewMaster) {
      const { year, model, make } = objectOverviewMaster;
      const carName = make?.replace(/ /g, "_");
      const modelSlug = model?.replace(/ /g, "_") || "";
      return `${window.__baseURLFE}/car_review/${carName}/${modelSlug}/${year}`;
    },

    /**
     * Handles showing the popup questions modal.
     */
    handleShowPopupQuestions() {
      this.$refs['QuestionsModal'].show();
    },



  },
};
</script>

<style lang="scss">
@import "./car-review.scss";
</style>
