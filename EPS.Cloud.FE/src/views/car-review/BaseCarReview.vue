<script>

import CarReviewAPI from '@/apis/CarReviewAPI';
import AnswerAPI from '@/apis/AnswerAPI';
export default {
    name: 'BaseCarReview',
    data() {
        return {
            currentStep: 0,
            titleQuestions: "",
            titleListQuestions: "",
            titlePopularQuestions: "",
            listQuestionNavigator: [
                {
                    text: 'Chủ để',
                    grade: 0,
                },
            ],
            getAnswerParam: {
                make: "",
                model: "",
                year: 0
            }
        }
    },
    methods: {
        handleStartQuestionsPage() {
            try {
                const name = this.$route.name;

                const { make, model, year } = this.$route.params;
                this.listQuestionNavigator = this.listQuestionNavigator.filter(item => item.grade !== 1 && item.grade !== 2 && item.grade !== 3);
                let user = this.$ms.cache.getCache("user");
                switch (name) {
                    case 'CarMake':
                        this.getAnswerParam.make = make.replace(/-/g, " ");
                        this.listQuestionNavigator.push({
                            text: make,
                            grade: 1
                        })
                        this.handleDefault(
                            1, {
                            title: `Câu hỏi ${make}`,
                            titleQuestions: `Đánh giá phổ biến theo ${make}`,
                            titlePopular: `Câu hỏi phổ biến về mẫu xe ${make}`,
                            callbackGetQuestions: async () => await CarReviewAPI.GetQuestionByMake(make),
                            callbackGetAnswers: async () => await AnswerAPI.getAnswerRecently(this.getAnswerParam),

                        }
                        );
                        break;
                    case 'CarModel':
                        this.getAnswerParam.make = make.replace(/-/g, " ");
                        this.getAnswerParam.model = model.replace(/-/g, " ");
                        this.listQuestionNavigator.push({
                            text: make,
                            grade: 1
                        }, {
                            text: model,
                            grade: 2
                        });
                        this.handleDefault(
                            2, {
                            title: `Câu hỏi ${this.getAnswerParam.make} ${this.getAnswerParam.model}`,
                            titleQuestions: `Đánh giá phổ biến theo ${this.getAnswerParam.make} ${this.getAnswerParam.model}`,
                            titlePopular: `Câu hỏi phổ biến về mẫu xe ${this.getAnswerParam.make} ${this.getAnswerParam.model}`,
                            callbackGetQuestions: async () => await CarReviewAPI.GetQuestionByModel(this.getAnswerParam.make, this.getAnswerParam.model),
                            callbackGetAnswers: async () => await AnswerAPI.getAnswerRecently(this.getAnswerParam),
                        }
                        );
                        break;
                    case 'CarYear':
                        this.getAnswerParam.make = make.replace(/-/g, " ");
                        this.getAnswerParam.model = model.replace(/-/g, " ");
                        this.getAnswerParam.year = year;
                        let title = `${this.getAnswerParam.make} ${this.getAnswerParam.model} `;
                        this.listQuestionNavigator.push({
                            text: make,
                            grade: 1
                        }, {
                            text: model,
                            grade: 2
                        }, {
                            text: year,
                            grade: 3
                        });
                        this.handleDefault(
                            3, {
                            title: `Câu hỏi ${title} ${year}`,
                            titleQuestions: `Đánh giá phổ biến theo ${title}`,
                            titlePopular: `Câu hỏi phổ biến về xe ${title} ${year}`,
                            callbackGetQuestions: async () => await CarReviewAPI.GetQuestionByModel(this.getAnswerParam.make, this.getAnswerParam.model),
                            callbackGetAnswers: async () => await AnswerAPI.getAnswerRecently(this.getAnswerParam),
                        }
                        );
                        break;
                    default:

                        this.handleDefault(0, {
                            title: 'Đánh giá và xếp hạng xe',
                            titleQuestions: `Tìm đánh giá cho bất kỳ chiếc xe nào`,
                            titlePopular: `Câu hỏi phổ biến gần đây`,
                            callbackGetQuestions: async () => await CarReviewAPI.getQuestionPopular(user.user_id),
                            callbackGetAnswers: async () => await AnswerAPI.getAnswerRecently(this.getAnswerParam),
                        });
                        break;
                }
            } catch (e) {
                console.log(e)
            }
        },

        async handleDefault(step = 0, objectMaster) {
            this.currentStep = step;
            this.titleQuestions = objectMaster.title;
            this.titleListQuestions = objectMaster.titleQuestions;
            this.titlePopularQuestions = objectMaster.titlePopular;
            let datas = await objectMaster.callbackGetQuestions();
            this.listQuestions = datas.data_1;
            this.listAnswerRecently = datas.data_2;
        },
        handleNavigator() {

        }


    }
}

</script>