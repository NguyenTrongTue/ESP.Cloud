import { createStore } from "vuex";

import toast from "./modules/toast";
import dialog from "./modules/dialog";
const store = createStore({
  modules: {
    toast,
    dialog,
  },
});

export default store;
