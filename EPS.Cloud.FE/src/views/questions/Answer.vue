<template>

    <div class="questions ">

        <div class="question__header mt-4">

            <div class="question__header__title">
                {{ question.questions_title }}
            </div>
            <div class="car_type">
                {{ question.make }} {{ question.model }} {{ question.year }}

            </div>


            <div class="author_questions mt-2 mb-1">
                {{ question.user_name }} - {{ computedTime(question.created_date) }}
            </div>
            <div class="question_desction mb-2">

                {{ question.question_content }}
            </div>

        </div>

        <div class="answer_button mb-2" @click="handleOpenPopupAnswer">
            Trả lời
        </div>
        <AnswerPopup ref="AnswerPopup" :questionId="question.questions_id" @update="handleUpdateAnswer" />
        <div class="popular_answer flex-start mb-2">
            <micon type="Popular" />
            <span class="ml-1"> Câu trả lời phổ biến</span>

        </div>
        <div class="answer_list ">
            <div v-for="(item) in listAnswer" :key="item.answers_id" class="answer_item mb-2">
                <div class="author_answer">
                    <div>
                        <span class="author_name">{{ item.user_name }}&nbsp - &nbsp</span>
                        <span class="time_answer">{{ computedTime(item.created_date) }}</span>
                    </div>
                    <div class="answer_description"> {{ item.answers_content }}</div>
                    <div class="reply mt-2" @click="handleReply(item.id)">Phản hồi</div>

                    <AnswerPopup :ref="`AnswerPopup_${item.id}`" :isReply="true" :replyToAnswerId="item.answers_id"
                        :questionId="question.questions_id" @update="handleUpdateAnswer" />
                </div>
            </div>
        </div>


    </div>


</template>

<script>
import AnswerPopup from './AnswerPopup.vue';
import AnswersAPI from '@/apis/AnswerAPI'
export default {
    name: "Answer",
    props: {},
    components: {
        AnswerPopup
    },
    data() {
        return {
            question: {

            },
            listAnswer: [],
            question_id: null,
        }
    },
    watch: {},
    mounted() {
        this.handleInitData();
    },

    methods: {
        /**
         * Handle updating an answer object.
         *
         * @param {object} objectMaster - The object to be added to the listAnswer array.
         * @return {void} 
         */
        handleUpdateAnswer(objectMaster) {
            this.listAnswer.push({ ...objectMaster, created_date: new Date() })
        },
        computedTime(date) {
            if (date) {

                let formatDate = new Date(date);
                let day = formatDate.getDate()
                    , month = formatDate.getMonth() + 1
                    , year = formatDate.getFullYear();


                return `Ngày ${day}/${month}, ${year}`;
            }
        },
        handleOpenPopupAnswer() {
            this.$refs['AnswerPopup'].show();
        },
        handleReply(id) {
            this.$refs[`AnswerPopup_${id}`][0].show();

        },
        async handleInitData() {
            this.question_id = this.$route.params.id;

            const result = await AnswersAPI.getAnswerByQuestionId(this.question_id);
            this.listAnswer = result.answers;
            this.question = result.question[0];

        }
    },
};
</script>

<style lang="scss">
@import "./questions.scss";
</style>
