<template>
    <div class="modal" v-if="showPopup">
        <div class="modal-container questons_modal">

            <div class="questions_modal__top">
                <div class="questions_modal__title">
                    Thêm đánh giá xe của bạn
                </div>

                <div class="question_modal__close" @click="showPopup = false">
                    ×
                </div>

            </div>
            <div class="horizontal-separator mt-2 mb-2"></div>

            <div class="question_modal__body">
                <div class="question_group">
                    <div class="question_group__label">
                        Nhập thông tin về chiếc xe của bạn
                    </div>
                    <div class="car_review_grid">
                        <mcombobox :hasBorder="true" default-text="Chọn hãng xe" class="combobox_item mb-1"
                            :items="makeList" v-model="objectMaster.make" :disabled="false" />

                        <mcombobox :hasBorder="true" default-text="Chọn mẫu xe" v-model="objectMaster.model"
                            class="combobox_item mb-1" :items="modelList" :disabled="!objectMaster.make" />

                        <mcombobox :hasBorder="true" default-text="Chọn năm" v-model="objectMaster.year"
                            class="combobox_item mb-1" :items="yearList" :disabled="!objectMaster.model" />
                    </div>
                </div>
                <div class="questons_modal_rating mt-2 mb-2">
                    <div class="title">Đánh giá tổng thể</div>
                    <div class="rating">

                       <starrating :rating="objectMaster?.rating?.toFixed(2)" :disableClick="false" size="30" @update="handleUpdateRating"/>
                    </div>
                </div>


                <div class="question_group">
                    <div class="question_group__label">
                        Đặt tiêu đề cho đánh giá của bạn
                    </div>
                    <minput name="Comment" v-model="objectMaster.car_review_title" ref="questionTitle" class="mb-2"
                        type-component="textarea" formName="Booking" :maxLength="80" rules="required" />
                </div>

                <div class="question_group">
                    <div class="question_group__label">
                        Tóm tắt đánh giá của bạn
                    </div>

                    <minput name="Comment" v-model="objectMaster.car_review_content" ref="questionDescription"
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
import CarReviewAPI from '@/apis/CarReviewAPI';
import enterFormMixin from "@/mixins/enterFormMixin.vue";
import MRatingV2 from '@/components/star-rating/MRatingV2.vue';
export default {
    name: "QuetionsModal",
    components: { MRatingV2 },
    mixins: [carMixin, enterFormMixin],
    props: {

    },
    computed: {
        computedDisableBtn() {
            return !this.objectMaster.make || !this.objectMaster.year || !this.objectMaster.model || !this.objectMaster.car_review_title
                || !this.objectMaster.car_review_content
        }
    },
    data() {
        return {
            showPopup: false,
            objectMaster: {
                make: null,
                year: null,
                model: null,
                rating: 0
            },
        }
    },
    mounted() {
        this.$nextTick(() => {

            this.$refs['questionTitle'].setHeightInput(60);
        })
        this.fetchMakes();
    },

    methods: {
        handleUpdateRating(rating) {
            this.objectMaster.rating = rating;

        },
        show() {
            this.showPopup = true;
        },
        async handleSubmit() {
            try {
                this.$store.commit("showLoading");
                let user = this.$ms.cache.getCache("user");
                this.objectMaster.user_id = user.user_id;
                this.objectMaster.user_name = user.fullname;

                await CarReviewAPI.post(this.objectMaster);

                this.$store.commit("showToast", {
                    label: "Đánh giá được đăng thành công",
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
@import "./car-review.scss";
</style>
