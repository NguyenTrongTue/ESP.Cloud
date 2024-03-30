<template>
    <div class="garage-review">
        <div class="top flex-start">
            <div class="icon-summanry flex-center">

                <micon type="Stars" :width="16" :height="16" />
                <micon type="Stars" :width="16" :height="16" />
                <micon type="Stars" :width="16" :height="16" />
                <micon type="Stars" :width="16" :height="16" />
                <micon type="Stars" :width="16" :height="16" />
            </div>

            <div class="reviewer">
                {{ review.fullname }}
            </div>
        </div>

        <div class="bottom mb-2 mt-1">
            {{ review.comment }}
        </div>

    </div>

</template>

<script>
export default {
    name: "ReviewItem",
    props: {
        review: {
            type: Object,
            default: {}
        }
    },
    data() { },
    watch: {},

    methods: {},
};
</script>

<style lang="scss" scoped>
@import "./garage-detail.scss";
</style>
