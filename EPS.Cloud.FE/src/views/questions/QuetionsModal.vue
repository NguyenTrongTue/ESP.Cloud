<template>
    <div class="modal" v-if="showPopup">
        <div class="modal-container questons_modal">

            <div class="questions_modal__top">
                <div class="questions_modal__title">
                    Đặt câu hỏi
                </div>

                <div class="question_modal__close" @click="showPopup = false">
                    ×
                </div>

            </div>
            <div class="horizontal-separator mt-2 mb-2"></div>

            <div class="question_modal__body">
                <div class="question_group">
                    <div class="question_group__label">
                        Cho chúng tôi biết cụ thể về chiếc xe của bạn
                    </div>
                    <div class="questions_grid">
                        <mcombobox :hasBorder="true" default-text="Chọn hãng xe" class="combobox_item mb-1"
                            :items="makeList" v-model="objectMaster.make" :disabled="false" />

                        <mcombobox :hasBorder="true" default-text="Chọn mẫu xe" v-model="objectMaster.model"
                            class="combobox_item mb-1" :items="modelList" :disabled="!objectMaster.make" />

                        <mcombobox :hasBorder="true" default-text="Chọn năm" v-model="objectMaster.year"
                            class="combobox_item mb-1" :items="yearList" :disabled="!objectMaster.model" />
                    </div>
                </div>
                <div class="question_group">
                    <div class="question_group__label">
                        Câu hỏi của bạn là gì?
                    </div>
                    <minput name="Comment" v-model="objectMaster.questions_title" ref="questionTitle" class="mb-2"
                        type-component="textarea" formName="Booking" :maxLength="80" rules="required" />
                </div>

                <div class="question_group">
                    <div class="question_group__label">
                        Chi tiết cụ thể cho câu hỏi của bạn
                    </div>
                    <div class="question_group_description">

                        Thêm các chi tiết hữu ích như nội dung bạn đang nghe và/hoặc tần suất xảy ra sự cố - bạn càng
                        thêm
                        nhiều chi tiết thì câu hỏi của bạn càng có nhiều khả năng được trả lời chính xác.
                    </div>
                    <minput name="Comment" v-model="objectMaster.question_content" ref="questionDescription"
                        class="mb-2" type-component="textarea" formName="Booking" :maxLength="1000" />

                </div>



            </div>
            <div class="question_modal__footer">
                <mbutton button-text="Gửi câu hỏi" class="mb-2" :disabled="computedDisableBtn" @click="handleSubmit" />
            </div>
        </div>


    </div>


</template>

<script>
import carMixin from "@/mixins/carMixin.vue";
import QuestionAPI from '@/apis/QuestionsAPI';
import enterFormMixin from "@/mixins/enterFormMixin.vue";
export default {
    name: "QuetionsModal",
    mixins: [carMixin, enterFormMixin],
    props: {

    },
    computed: {
        computedDisableBtn() {
            return !this.objectMaster.make || !this.objectMaster.year || !this.objectMaster.model || !this.objectMaster.questions_title
                || !this.objectMaster.question_content
        }
    },
    data() {
        return {
            showPopup: false,
        }
    },
    mounted() {
        this.$nextTick(() => {

            this.$refs['questionTitle'].setHeightInput(60);
        })
        this.fetchMakes();
    },

    methods: {
        show() {
            this.showPopup = true;
        },
        async handleSubmit() {
            try {
                this.$store.commit("showLoading");
                let user = this.$ms.cache.getCache("user");
                this.objectMaster.user_id = user.user_id;
                this.objectMaster.user_name = user.fullname;

                await QuestionAPI.post(this.objectMaster);

                this.$store.commit("showToast", {
                    label: "Câu hỏi được đăng thành công",
                    type: 'success'
                });
                this.showPopup = false;
                this.$store.commit("hideLoading")
            } catch (e) {
                console.log(e);
                this.$store.commit("hideLoading")
            }
        }
    },
};
</script>

<style lang="scss">
@import "./questions.scss";
</style>
