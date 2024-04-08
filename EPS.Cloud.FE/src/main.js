import "@/styles/main.scss";
import i18n from "@/i18n";
import { createApp } from "vue";
import App from "./App.vue";
import router from "@/router";
import { clickOutsideDirective } from "@/utils/clickOutsideDirective.js";
import MyEmitter from "tiny-emitter/instance";
import MResources from "@/helper/resources";
import store from "@/store";
import { moudle } from "@/utils/index";
import VueTippy from "vue-tippy";
import { Tippy } from "vue-tippy";
import "tippy.js/dist/tippy.css";

// components
import MInputPri from "@/components/input/MInputPri.vue";
import MButton from "@/components/button/MButton.vue";
import MModal from "@/components/modal/MModal.vue";
import MLoading from "@/components/loading/MLoading.vue";
import MCombobox from "@/components/combobox/MCombobox.vue";
import Icon from "@/components/icons/Icon.vue";
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import MDatePicker from "@/components/date-picker/MDatePicker.vue";
import MSearch from "./components/search/MSearch.vue";
import MCardSelect from "./components/cardselect/MCardSelect.vue";
import InputRadio from "./components/radio/MRadio.vue";
import MRating from "./components/rating/MRating.vue";
import StarRating from "@/components/star-rating/StarRating.vue";

const app = createApp(App);

// config
app.config.globalProperties.$emitter = MyEmitter;
app.config.globalProperties.$route = router;
app.config.globalProperties.$ms = moudle;
app.config.globalProperties.$MResources = MResources;
// conponents
app.component("minput", MInputPri);
app.component("mbutton", MButton);
app.component("mmodal", MModal);
app.component("mloading", MLoading);
app.component("mtooltip", Tippy);
app.component("micon", Icon);
app.component("mdate-picker", VueDatePicker);
app.component("date-picker", MDatePicker);
app.component("mcombobox", MCombobox);
app.component("msearch", MSearch);
app.component("mcardselect", MCardSelect);
app.component("mradio", InputRadio);
app.component("mrating", MRating);
app.component("starrating", StarRating);
app.directive("click-outside", clickOutsideDirective);

app.use(i18n).use(router).use(store).mount("#app");
