const state = {
  isShowMessage: false,
  errorMessage: "",
};

const mutations = {
  /**
   * Hàm thực hiện nhận nội dung thông báo và hiển thị lỗi lên dialog.
   * @param {Object} state
   * @param {String} message Thông báo lỗi
   */
  setErrorMessage(state, message) {
    console.log(message);

    state.isShowMessage = true;
    state.errorMessage = message;
  },
  /**
   * Hàm thực hiện ẩn dialog khi người dùng đóng dialog.
   */
  hideErrorMessage(state) {
    state.isShowMessage = false;
  },
};

const actions = {};

const getters = {};

export default {
  namespaced: true,
  state,
  mutations,
  actions,
  getters,
};
