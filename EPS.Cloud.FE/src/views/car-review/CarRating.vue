<template>
    <div class="bar_rating_wrapper">
        <div class="bar_rating">
            <div class="bar_rating__right">
                <span>5</span>
                <micon type="Stars" />
            </div>

            <div class="bar_rating__center" ref="five_stars">

                <div class="bar_rating_center__percent" :style="{
                    width: percentFiveStars + '%'
                }"></div>
            </div>

            <div class="number_of_rating">({{ objectOverviewMaster?.five_stars }})</div>

        </div>
        <div class="bar_rating">
            <div class="bar_rating__right">
                <span>4</span>
                <micon type="Stars" />
            </div>

            <div class="bar_rating__center" ref="four_stars">

                <div class="bar_rating_center__percent" :style="{
                    width: percentFourStars + '%'
                }"></div>
            </div>

            <div class="number_of_rating">({{ objectOverviewMaster?.four_stars }})</div>

        </div>
        <div class="bar_rating">
            <div class="bar_rating__right">
                <span>3</span>
                <micon type="Stars" />
            </div>

            <div class="bar_rating__center" ref="three_stars">

                <div class="bar_rating_center__percent" :style="{
                    width: percentThreeStars + '%'
                }"></div>
            </div>

            <div class="number_of_rating">({{ objectOverviewMaster?.three_stars }})</div>

        </div>
        <div class="bar_rating">
            <div class="bar_rating__right">
                <span>2</span>
                <micon type="Stars" />
            </div>

            <div class="bar_rating__center" ref="two_stars">

                <div class="bar_rating_center__percent" :style="{
                    width: percentTwoStars + '%'
                }"></div>
            </div>

            <div class="number_of_rating">({{ objectOverviewMaster?.two_stars }})</div>

        </div>
        <div class="bar_rating">
            <div class="bar_rating__right">
                <span>1</span>
                <micon type="Stars" />
            </div>

            <div class="bar_rating__center" ref="one_stars">

                <div class="bar_rating_center__percent" :style="{
                    width: percentOneStars + '%'
                }"></div>
            </div>

            <div class="number_of_rating">({{ objectOverviewMaster?.one_stars }})</div>

        </div>
    </div>
</template>

<script>
export default {
    name: "CarRating",
    props: {
        objectOverviewMaster: {
            type: Object,
            default: {}
        }
    },
    components: {

    },
    data() {
        return {
            percentFiveStars: 0,
            percentFourStars: 0,
            percentThreeStars: 0,
            percentTwoStars: 0,
            percentOneStars: 0,
        }
    },
    watch: {
        objectOverviewMaster: {
            handler(newValue) {
                this.calculationPercentStars();
            }, deep: true
        }


    },
    mounted() {
        this.calculationPercentStars();
    },

    methods: {
        calculationPercentStars() {
            try {
                if (this.objectOverviewMaster.total_rating == 0) return; else {

                    this.percentFiveStars = (this.objectOverviewMaster?.five_stars / this.objectOverviewMaster?.total_rating) * 100;
                    this.percentFourStars = (this.objectOverviewMaster?.four_stars / this.objectOverviewMaster?.total_rating) * 100;
                    this.percentThreeStars = (this.objectOverviewMaster?.three_stars / this.objectOverviewMaster?.total_rating) * 100;
                    this.percentTwoStars = (this.objectOverviewMaster?.two_stars / this.objectOverviewMaster?.total_rating) * 100;
                    this.percentOneStars = (this.objectOverviewMaster?.one_stars / this.objectOverviewMaster?.total_rating) * 100;
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
