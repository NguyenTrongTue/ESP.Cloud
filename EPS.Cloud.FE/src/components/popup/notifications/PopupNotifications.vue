<template>
    <div class="notifications">
        <div class="notifications__header">

            <span class="title ng-font-neutral-80">Thông báo</span>

            <div class="notifications_filter flex-start">
                <div class="notifications_filter__item  flex-center" v-for="(item) in filterType" :key="item.id" :class="{
                    'active': item.id == currentFilterType
                }" @click="currentFilterType = item.id">
                    <span>{{ item.text }}</span>

                </div>


            </div>
            <div class="horizontal-separator"></div>
        </div>
        <div class="notifications__body">
            <div class="notifications_item flex-between" :class="{
                    'unread': item.unread
                }" v-for="(item, index) in computedNotifications" :key="index">
                <div class="notifications_item__right flex-center ">

                    <img v-if="item.type == 0" src="@/assets/img/download.png" alt="icon" />

                    <img v-else src="@/assets/img/promo.png" alt="icon" />

                </div>
                <div class="notifications_item__left">
                    <div class="description">{{ item.description }}</div>
                    <span class="time">{{ $ms.common.formatTime(item.created_time) }}</span>
                </div>

                <div v-if="item.unread" class="notification__unread"></div>
            </div>


        </div>
    </div>

</template>

<script>
export default {
    name: "PopupNotifications",
    props: {
        notificationsProps: {
            type: Array,
            default: []
        }
    },
    data() {
        return {
            // 0 - lọc tất cả
            // 1 - lọc những messege chưa đọc
            filterType: [
                {
                    id: 0,
                    text: 'Tất cả'
                },
                {
                    id: 1,
                    text: 'Chưa đọc'
                },
            ],
            /**
             * mặc định cho lọc tất cả
             */
            currentFilterType: 0,
            datas: this.notificationsProps
        }
    },
    computed: {
        computedNotifications() {

            if (this.currentFilterType == 0) {
                return this.datas
            }
            else if (this.currentFilterType == 1) {
                return this.datas.filter(item => item.type == 0)
            }

        },

    },
    watch: {
        notificationsProps: {
            handler(newNotifications) {
                this.datas = newNotifications

                console.log(this.datas);
            },
            deep: true
        }
    },

    methods: {},
};
</script>

<style lang="scss" scoped>
@import "./popup-notifications.scss";
</style>
