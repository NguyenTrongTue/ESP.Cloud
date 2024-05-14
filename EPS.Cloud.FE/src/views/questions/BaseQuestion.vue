<script>
import QuestionAPI from '@/apis/QuestionsAPI';
import AnswerAPI from '@/apis/AnswerAPI';
export default {
    name: 'BaseQuestions',
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

                switch (name) {
                    case 'Make':
                        this.getAnswerParam.make = make.replace(/_/g, " ");
                        this.listQuestionNavigator.push({
                            text: make,
                            grade: 1
                        })
                        this.handleDefault(
                            1, {
                            title: `Câu hỏi ${make}`,
                            titleQuestions: `Câu hỏi về các mẫu xe ${make}`,
                            titlePopular: `Câu hỏi phổ biến về mẫu xe ${make}`,
                            callbackGetQuestions: async () => await QuestionAPI.GetQuestionByMake(make),
                            callbackGetAnswers: async () => await AnswerAPI.getAnswerRecently(this.getAnswerParam),

                        }
                        );
                        break;
                    case 'Model':
                        this.getAnswerParam.make = make.replace(/_/g, " ");
                        this.getAnswerParam.model = model.replace(/_/g, " ");
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
                            titleQuestions: `Câu hỏi theo các mẫu xe ${this.getAnswerParam.make} ${this.getAnswerParam.model}`,
                            titlePopular: `Câu hỏi phổ biến về mẫu xe ${this.getAnswerParam.make} ${this.getAnswerParam.model}`,
                            callbackGetQuestions: async () => await QuestionAPI.GetQuestionByModel(this.getAnswerParam.make, this.getAnswerParam.model),
                            callbackGetAnswers: async () => await AnswerAPI.getAnswerRecently(this.getAnswerParam),
                        }
                        );
                        break;
                    case 'Year':
                        this.getAnswerParam.make = make.replace(/_/g, " ");
                        this.getAnswerParam.model = model.replace(/_/g, " ");
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
                            titleQuestions: `Câu hỏi về xe ${title}`,
                            titlePopular: `Câu hỏi phổ biến về xe ${title} ${year}`,
                            callbackGetQuestions: async () => await QuestionAPI.GetQuestionByModel(this.getAnswerParam.make, this.getAnswerParam.model),
                            callbackGetAnswers: async () => await AnswerAPI.getAnswerRecently(this.getAnswerParam),
                        }
                        );
                        break;
                    default:

                        this.handleDefault(0, {
                            title: 'Thảo luận về ô tô',
                            titleQuestions: `Câu hỏi về mẫu xe`,
                            titlePopular: `Câu hỏi phổ biến gần đây`,
                            callbackGetQuestions: async () => await QuestionAPI.getQuestionPopular(),
                            callbackGetAnswers: async () => await AnswerAPI.getAnswerRecently(this.getAnswerParam),
                        });
                        break;
                }
            } catch (e) {
                console.log(e)
            }
        },

        async handleDefault(step = 0, objectMaster) {
            try {
                this.$store.commit("showLoading");
                this.currentStep = step;
                this.titleQuestions = objectMaster.title;
                this.titleListQuestions = objectMaster.titleQuestions;
                this.titlePopularQuestions = objectMaster.titlePopular;
                this.listQuestions = await objectMaster.callbackGetQuestions();
                this.listAnswerRecently = await objectMaster.callbackGetAnswers();;
                this.$store.commit("hideLoading");
            } catch (e) {
                console.log(e);
                this.$store.commit("hideLoading");
            }
        },
        handleNavigator() {

        }


    }
}

</script>