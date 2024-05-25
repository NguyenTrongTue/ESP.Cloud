import { createRouter, createWebHistory } from "vue-router";
import Cache from "@/utils/cache";
import store from "@/store";
import Home from "@/views/home/Home.vue";
import Estimate from "@/views/estimate/Estimate.vue";
import Booking from "@/views/booking/Booking.vue";
import Promo from "@/views/promo/Promo.vue";
import Questions from "@/views/questions/Questions.vue";
import CarReview from "@/views/car-review/CarReview.vue";
import Answer from "@/views/questions/Answer.vue";
import HistoryBooking from "@/views/history-booking/HistoryBooking.vue";
import Login from "@/views/login/Login.vue";
import SignUp from "@/views/signup/SignUp.vue";
import Estimator from "@/views/estimator/Estimator.vue";
import GarageDetail from "@/views/garage-detail/GarageDetail.vue";
import PromoDetail from "@/views/promo-detail/PromoDetail.vue";
const routes = [
  {
    name: "home",
    component: Home,
    path: "/",
    meta: { requiresAuth: false, title: "Tìm kiếm" },
  },
  {
    name: "estimate",
    component: Estimate,
    path: "/estimate/:id",
    meta: { requiresAuth: true, title: "Ước tính gara" },
  },
  {
    name: "garage-detail",
    component: GarageDetail,
    path: "/garage-detail/:id",
    meta: { requiresAuth: true, title: "Chi tiết" },
  },
  {
    name: "booking",
    component: Booking,
    path: "/booking/:id",
    meta: { requiresAuth: true, title: "Đặt lịch", showHeader: false },
  },
  {
    name: "estimator",
    component: Estimator,
    path: "/estimator",
    meta: { requiresAuth: false, title: "Ước tính", showHeader: false },
  },
  {
    name: "promo",
    component: Promo,
    path: "/promo",
    meta: { requiresAuth: false, title: "Chương trình khuyến mãi" },
  },
{
    name: "answer",
    path: "/answer/:id",
    component: Answer,
    meta: { requiresAuth: true, title: "Thảo luận" },
   
  },
  {
    path: "/questions",
    component: Questions,
    name: "Questions",
    meta: { requiresAuth: true, title: "Thảo luận" },
    // children: [
    //   { path: ":make", name: "Make", component: Questions },
    //   { path: ":make/:model", name: "Model", component: Questions },
    //   { path: ":make/:model/:year", name: "Year", component: Questions },
    // ],
  },
        { path: "/questions/:make", name: "Make", component: Questions },
      { path: "/questions/:make/:model", name: "Model", component: Questions },
      { path: "/questions/:make/:model/:year", name: "Year", component: Questions },
  {
    path: "/car_review",
    name: "CarReview",
    component: CarReview,
    meta: { requiresAuth: true, title: "Đáng giá" },
    // children: [
    //   { path: ":make", name: "CarMake", component: CarReview },
    //   { path: ":make/:model", name: "CarModel", component: CarReview },
    //   { path: ":make/:model/:year", name: "CarYear", component: CarReview },
    // ],
  },
  { path: "/car_review/:make", name: "CarMake", component: CarReview },
  { path: "/car_review/:make/:model", name: "CarModel", component: CarReview },
  { path: "/car_review/:make/:model/:year", name: "CarYear", component: CarReview },
  {
    name: "history-booking",
    component: HistoryBooking,
    path: "/history-booking",
    meta: { requiresAuth: false, title: "Lịch sử đặt lịch" },
  },
  {
    name: "login",
    component: Login,
    path: "/login",
    meta: { requiresAuth: false, title: "Đăng nhập", showHeader: false },
  },
  {
    name: "signup",
    component: SignUp,
    path: "/signup",
    meta: { requiresAuth: false, title: "Đăng ký", showHeader: false },
  },
  {
    name: "promo-detail",
    component: PromoDetail,
    path: "/promo-detail/:id",
    meta: { requiresAuth: false, title: "Chi tiết khuyến mãi", showHeader: true },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  let value = Cache.getCache("user");

  function changeTitle(to) {
    if (to.meta.title) {
      document.title = to.meta.title;
    } else {
      document.title = "Hệ thống";
    }
  }
  let auth = false;
  if (value && value.user_id) {
    auth = true;
  }
  if (to.meta.requiresAuth) {
    if (auth) {
      changeTitle(to);
    } else {

      next({ name: "login" });

      store.commit("showToast", {
        label:
          "Vui lòng đăng nhập để sử dụng tính năng!",
        type: "error",
      });
    }
  } else {
    changeTitle(to);
  }

  next(); // Chuyển tiếp để thực hiện điều hướng
});

export default router;
