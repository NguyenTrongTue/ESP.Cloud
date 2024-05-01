<template>

  <div class="modal" v-if="showPopup">
    <div class="modal-container popup__review">

      <div class="questions_modal__top">
        <div class="questions_modal__title">
          Thêm đánh giá
        </div>

        <div class="question_modal__close" @click="showPopup = false">
          ×
        </div>

      </div>
      <div class="horizontal-separator mb-2"></div>
      <div class="popup__review_body">
        <div class="garage_name">{{ garageProps?.garage_name }}</div>


        <div class="review_rating flex-center mt-2 mb-2">
          <MRatingV2 @updateRating="handleUpdateRating" />
        </div>

        <div class="question_group__label mb-1">
          Chi tiết đánh giá của bạn
        </div>
        <minput name="Comment" v-model="objectMaster.comment" ref="reviewComment" class="mb-2" type-component="textarea"
          formName="Booking" :maxLength="1000" rules="required" />

      </div>

      <div class="question_modal__footer">
        <mbutton button-text="Gửi đánh giá" class="mb-2" :disabled="computedDisableBtn" @click="handleSubmitReview" />
      </div>

    </div>


  </div>


</template>

<script>
import MRatingV2 from '@/components/star-rating/MRatingV2.vue';
import ReviewAPI from '@/apis/ReviewAPI';
export default {
  name: "PopupReview",
  components: { MRatingV2 },
  props: {
    garageProps: {
      type: Object,
      default: {}
    }

  },
  data() {
    return {
      showPopup: false,
      objectMaster: {
        comment: "",
        rating: 0
      }
    }
  },
  computed: {
    computedDisableBtn() {
      return !this.objectMaster.comment.trim();
    }
  },
  watch: {},

  methods: {
    show() {
      this.showPopup = true;
    },
    close() {
      this.showPopup = false;
    },
    handleUpdateRating(rating) {
      this.objectMaster.rating = rating;
    },

    async handleSubmitReview() {
      try {
        this.$store.commit("showLoading");
        let user = this.$ms.cache.getCache("user");
        this.objectMaster.user_id = user.user_id;
        this.objectMaster.fullname = user.fullname;
        this.objectMaster.garage_id = this.garageProps.garage_id;

        await ReviewAPI.post(this.objectMaster);

        this.$store.commit("showToast", {
          label: "Đánh giá của bạn được gửi thành công!",
          type: 'success'
        });

        this.$emit("updateReview", this.objectMaster);
        this.$store.commit("hideLoading");
        this.close();
      } catch (e) {
        console.log(e);
        this.$store.commit("hideLoading");
      }

    }
  },
};
</script>

<style lang="scss">
@import "./garage-detail.scss";



.garage_name {
  text-align: center;
  font-size: 20px;
  color: #212121;
}

.questions_modal__top {
  padding: 12px;
  display: flex;
  justify-content: space-between;
  align-items: center;

  .questions_modal__title {
    font-size: 20px;
    font-weight: bold;
  }

  .question_modal__close {
    font-size: 30px;
    font-weight: bold;
    cursor: pointer;
    width: 40px;
    height: 40px;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;

    &:hover {
      background-color: rgba($color: #000000, $alpha: .12);
    }
  }


}

.question_modal__footer {
  padding: 12px 12px 0;
  border-top: 1px solid rgba(0, 0, 0, .12);

}
</style>
