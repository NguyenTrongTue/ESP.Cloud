<template>

  <div class="answer_popup" v-if="showPopup">

    <minput name="Conteent" v-model="answer.answers_content" ref="Conteent" type-component="textarea" :maxLength="1000"
      default-text="Thêm câu trả lời tại đây" />

    <div class="anser_footer mt-2">
      <mbutton button-text="Bỏ qua" @click="hide" class="none-background" />
      <mbutton button-text="Gửi câu trả lời" @click="handleSubmit" />
    </div>
  </div>

</template>

<script>
import AnswersAPI from '@/apis/AnswerAPI';
import enterFormMixin from "@/mixins/enterFormMixin.vue";
export default {
  name: "AnswerPopup",
  mixins: [enterFormMixin],
  props: {
    isReply: {
      type: Boolean,
      default: false,
    },
    questionId: {
      type: String,
      default: "",
      required: true
    },
    replyToAnswerId: {
      type: String,
      default: null,
      required: false
    }
  },
  data() {
    return {
      showPopup: false,
      answer: {}
    }

  },
  watch: {},

  methods: {
    hide() {
      this.showPopup = false;
    },
    show() {
      this.showPopup = true;
    },
    async handleSubmit() {
      try {

        if (!this.answer.answers_content) {

          return;
        } else {
          let user = this.$ms.cache.getCache("user");
          const objectMaster = {
            questions_id: this.questionId,
            user_id: user.user_id,
            user_name: user.fullname,
            answers_content: this.answer.answers_content,
            is_reply: this.isReply,
            reply_to_answer_id: this.replyToAnswerId
          }
          await AnswersAPI.post(objectMaster);
          this.$emit("update", objectMaster);
          this.showPopup = false;
          this.answer.answers_content = "";
        }
      } catch (e) {
        console.log(e);
      }
    }
  },
};
</script>

<style lang="scss" scoped>
.answer_popup {
  background-color: #eeeeee;
  padding: 20px;
  border-radius: 4px;
}

.anser_footer {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: flex-end;
  gap: 12px;

  button {
    width: 20%;
  }
}
</style>
