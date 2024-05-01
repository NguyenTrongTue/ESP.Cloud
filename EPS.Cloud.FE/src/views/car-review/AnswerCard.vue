<template>
  <div class="car_review_card">

    <div class="car_review_card__top">
      <div class="title mb-2">{{ answerData.make }} {{ answerData.model }} {{ answerData.year }}</div>
      <div class="rating">
        <starrating :rating="answerData?.rating?.toFixed(2)" />

      </div>
      <div class="author_wrapper mt-2 mb-1">
        Tạo bởi {{ answerData.user_name }} vào {{ computedTime(answerData.created_date) }}
      </div>
    </div>
    <div class="car_review_card___center">
      <div class="description">{{ answerData.car_review_content }}</div>
      <div class="react" :class="{ 'is_like': answerData?.is_liked }" @click="handleLikeOrUnLikePost">
        <span class="total_like">({{ answer?.total_likes }})</span>
        <div class="like">
          <micon type="Like" />
        </div>

      </div>
    </div>




  </div>


</template>

<script>
import CarReviewAPI from '@/apis/CarReviewAPI';
export default {
  name: "AnswerCard",
  props: {

    answer: {
      type: Object,
      default: {}
    }
  },
  data() {
    return {
      answerData: this.answer
    }


  },


  methods: {
    computedTime(date) {
      if (date) {

        let formatDate = new Date(date);
        let day = formatDate.getDate()
          , month = formatDate.getMonth() + 1
          , year = formatDate.getFullYear();


        return `Ngày ${day < 10 ? `0${day}` : day}/${month < 10 ? `0${month}` : month}, ${year}`;
      }
    },
    async handleLikeOrUnLikePost() {
      try {

        let user = this.$ms.cache.getCache("user");
        const objectMaster = {
          car_review_id: this.answer.car_review_id,
          user_id: user.user_id,
          liked: !this.answer.is_liked

        }
        await CarReviewAPI.likeOrUnLikeAsync(objectMaster);

        this.answerData.is_liked = !this.answerData.is_liked;

        if (this.answerData.is_liked) {
          this.answerData.total_likes++;
        } else {
          this.answerData.total_likes--;
        }


      } catch (e) {
        console.log(e);
      }
    }
  },
};
</script>

<style lang="scss">
@import "./car-review.scss";
</style>
