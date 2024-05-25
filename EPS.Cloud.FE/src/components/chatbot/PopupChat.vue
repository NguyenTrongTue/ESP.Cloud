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
        getChatsFromCache() {
            try {
                const chats = this.$ms.cache.getCache('chat');
                if (chats && chats.length > 0) {
                    this.listChats = [...chats];
                }
            } catch (error) {
                console.log(error);
            }
        },
        show() {
            this.showPopup = true;
            this.getChatsFromCache();
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
            try {
                this.listChats.push({
                    message: this.question,
                    is_sender: true
                });
                let question = this.question;
                this.question = "";
                this.scrollLastChat();
                await this.callDataGetResult(question);
                this.$ms.cache.setCache('chat', this.listChats);

            } catch (error) {
                console.log(error);
            }
        },
        async callDataGetResult(question) {
            try {
                // http://150.95.114.153:5022
                this.isGettingData = true;
                let questionText = question.includes('gần') || question.includes('xa') || question.includes('khoảng cách') ? question + ". Vĩ độ hiện tại của tôi là 21.037776 và kinh độ hiện tại của tôi 105.782996" : question;
                var response = await axios.post("http://127.0.0.1:5000/get-message", { question: questionText + ' .Trả lời câu hỏi bằng Tiếng Việt' });
                if (response.data) {

                    const { message } = response.data;
                    this.listChats.push({
                        message: message,
                        is_sender: false
                    });
                    this.$ms.cache.setCache('chat', this.listChats);
                    if (question.toLowerCase().includes("đường đi")) {

                        this.getLatitudeAndLongitude(message);
                    }
                }
                this.isGettingData = false;
                this.scrollLastChat();

            } catch (e) {
                this.isGettingData = false;
            }
        },
        getLatitudeAndLongitude(inputString) {
            const regex = /latitude: ([\d.]+), longitude: ([\d.]+)/;
            const regex1 = /vĩ độ: ([\d.]+), kinh độ: ([\d.]+)/;
            const match = inputString.match(regex);
            const match1 = inputString.match(regex1);
            const regex2 = /vĩ độ ([\d.]+) và kinh độ ([\d.]+)/;
            const match2 = inputString.match(regex2);
            const regex3 = /latitude ([\d.]+) and longitude ([\d.]+)/;
            const match3 = inputString.match(regex3);
            const regex4 = /latitude ([\d.]+), longitude ([\d.]+)/;
            const match4 = inputString.match(regex4);
            if (match) {
                const latitude = parseFloat(match[1]);
                const longitude = parseFloat(match[2]);


                window.open(
                    `https://www.google.com/maps/dir/?api=1&destination=${latitude},${longitude}`,
                    "_blank"
                );

            } else if (match1) {
                const latitude = parseFloat(match1[1]);
                const longitude = parseFloat(match1[2]);


                window.open(
                    `https://www.google.com/maps/dir/?api=1&destination=${latitude},${longitude}`,
                    "_blank"
                );
            } else if (match2) {
                const latitude = parseFloat(match2[1]);
                const longitude = parseFloat(match2[2]);


                window.open(
                    `https://www.google.com/maps/dir/?api=1&destination=${latitude},${longitude}`,
                    "_blank"
                );
            }
            else if (match3) {
                const latitude = parseFloat(match3[1]);
                const longitude = parseFloat(match3[2]);


                window.open(
                    `https://www.google.com/maps/dir/?api=1&destination=${latitude},${longitude}`,
                    "_blank"
                );
            }
            else if (match4) {
                const latitude = parseFloat(match4[1]);
                const longitude = parseFloat(match4[2]);


                window.open(
                    `https://www.google.com/maps/dir/?api=1&destination=${latitude},${longitude}`,
                    "_blank"
                );
            }
            else {
                console.log("Không tìm thấy giá trị vĩ độ và kinh độ trong chuỗi.");
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
