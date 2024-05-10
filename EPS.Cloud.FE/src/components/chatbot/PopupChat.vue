<template>

    <div class="popup_chat" v-if="showPopup">

        <div class="popup_chat__header">
            <div class="popup_chat__header__left">
                <img src="@/assets/img/chat.png" alt="icon" />
                <span>Chatbot</span>
            </div>

            <div class="popup_chat__header__right flex-center" @click="handleClose"><span>×</span></div>


        </div>
        <div class="popup_chat__body">

            <div class=no_chat v-if="listChats.length == 0">
                <img src="@/assets/img/chatbot.png" alt="icon" />

                <div class="no_chat_title">ChatBot GaraHunt</div>
            </div>

            <div class="popup_chat__body__list" v-else ref="ChatBotList">
                <div class="chat_item" v-for="(item, index) in listChats" :key="index" :class="{
        'is_sender': item.is_sender
    }">
                    <div class="chat_item__top" v-if="!item.is_sender">
                        <img src="@/assets/img/chatbot-small.png" alt="icon" />
                        <span>Chatbot</span>
                    </div>
                    <span class="chat_item__message">{{ item.message }}</span>
                </div>
                <div v-if="isGettingData" class="is_getting_data">
                    <div class="chat_item__top">
                        <img src="@/assets/img/chatbot-small.png" alt="icon" />
                        <span>Chatbot</span>
                    </div>

                    <div class="load-3">
                        <div class="line"></div>
                        <div class="line"></div>
                        <div class="line"></div>
                    </div>
                </div>
            </div>



        </div>

        <div class="popup_chat__input">
            <input ref="question" v-model="question" :disabled="isGettingData" type="text"
                placeholder="Hỏi tôi bất cứ câu hỏi nào về gara" />
            <img src="@/assets/img/send.png" alt="icon" @click="handleSubmit" />

        </div>


    </div>


</template>

<script>
import enterFormMixin from "@/mixins/enterFormMixin.vue";
import axios from "axios";
export default {
    name: "PopupChat",
    emits: ['close'],
    mixins: [enterFormMixin],
    props: {

    },
    data() {
        return {
            showPopup: false,
            isGettingData: false,
            question: "",
            listChats: [

            ]
        }
    },
    watch: {},
    mounted() {

    },
    methods: {
        show() {
            this.showPopup = true;
            this.scrollLastChat();

        },
        scrollLastChat() {
            this.$nextTick(() => {
                if (this.$refs['ChatBotList']) {

                    this.$refs['ChatBotList'].scrollIntoView({ behavior: "smooth", block: "end", inline: "nearest" });
                }
            });
        },
        hide() {
            this.showPopup = false;
        },
        handleClose() {
            this.$emit('close');
            this.hide();
        },

        async handleSubmit() {
            this.listChats.push({
                message: this.question,
                is_sender: true
            });
            let question = this.question;
            this.question = "";
            await this.callDataGetResult(question);
            this.scrollLastChat();

        },
        async callDataGetResult(question) {
            try {

                this.isGettingData = true;
                var response = await axios.post("http://127.0.0.1:5000/get-message", { question });
                if (response.data) {

                    const { message } = response.data;
                    this.listChats.push({
                        message: message,
                        is_sender: false
                    });
                }
                this.isGettingData = false;
            } catch (e) {
                this.isGettingData = false;
            }
        }

    },
};
</script>

<style lang="scss" scoped>
@import "./chatbot.scss";

.is_getting_data {
    .load-3 {
        padding: 8px;
        border-radius: 10px;
        background-color: #f7f7f7;
        width: fit-content;
        padding-top: 0;

        .line {
            width: 5px;
            height: 5px;
        }
    }
}
</style>
