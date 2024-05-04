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
            topRatingTitle: "",
            topRatingDesc: '',
            objectOverviewMaster: {},
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
        /**
         * Handles the start of the questions page based on the current route name and parameters.
         *
         * @return {void} This function does not return anything.
         */
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
                            topRatingTitle: `Tổng quan về xếp hạng xe ${make}`,
                            callbackGetQuestions: async () => await CarReviewAPI.GetQuestionByMake(make, user.user_id),
                            getOverview: async () => await CarReviewAPI.getOverview(make),

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
                            topRatingTitle: `Tổng quan về xếp hạng xe ${make}`,
                            callbackGetQuestions: async () => await CarReviewAPI.GetQuestionByModel(this.getAnswerParam.make, this.getAnswerParam.model, user.user_id),
                            getOverview:  async () => await CarReviewAPI.getOverview(make),
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
                            topRatingTitle: `Tổng quan về xếp hạng xe ${make}`,
                            callbackGetQuestions: async () => await CarReviewAPI.GetQuestionByModel(this.getAnswerParam.make, this.getAnswerParam.model, user.user_id),
                            getOverview:  async () => await CarReviewAPI.getOverview(make),
                        }
                        );
                        break;
                    default:

                        this.handleDefault(0, {
                            title: 'Đánh giá và xếp hạng xe',
                            titleQuestions: `Tìm đánh giá cho bất kỳ chiếc xe nào`,
                            titlePopular: `Câu hỏi phổ biến gần đây`,
                            topRatingTitle: "Xe được đánh giá cao nhất",
                            callbackGetQuestions: async () => await CarReviewAPI.getQuestionPopular(user.user_id),
                            getOverview: async () => await CarReviewAPI.getOverview(),
                        });
                        break;
                }
            } catch (e) {
                console.log(e)
            }
        },

        /**
         * Sets the default values for the component's state based on the provided step and objectMaster.
         *
         * @param {number} step - The step to set the currentStep to. Defaults to 0.
         * @param {object} objectMaster - An object containing the title, titleQuestions, titlePopular, callbackGetQuestions, and callbackGetAnswers properties.
         * @return {Promise<void>} A Promise that resolves once the default values have been set.
         */
        async handleDefault(step = 0, objectMaster) {
            try {
                this.$store.commit("showLoading");
                this.currentStep = step;
                this.titleQuestions = objectMaster.title;
                this.titleListQuestions = objectMaster.titleQuestions;
                this.titlePopularQuestions = objectMaster.titlePopular;
                this.topRatingTitle = objectMaster.topRatingTitle;
                let datas = await objectMaster.callbackGetQuestions();
                this.objectOverviewMaster = await objectMaster.getOverview();
                this.listQuestions = datas.data_1;
                this.listAnswerRecently = datas.data_2;
                this.$store.commit("hideLoading");
            }catch(e) {
                console.log(e);
                 this.$store.commit("hideLoading");
            }
            
        },
        handleNavigator() {

        }


    }
}

</script>