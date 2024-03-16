import { createRouter, createWebHistory } from "vue-router";

import Home from "@/views/home/Home.vue";
import Booking from "@/views/booking/Booking.vue";
import Promo from "@/views/promo/Promo.vue";
import HistoryBooking from "@/views/history-booking/HistoryBooking.vue";
import Login from "@/views/login/Login.vue";
import SignUp from "@/views/signup/SignUp.vue";
import Estimator from "@/views/estimator/Estimator.vue";
const routes = [
  {
    name: "home",
    component: Home,
    path: "/",
    meta: { requiresAuth: false, title: "Tìm kiếm" },
  },
  {
    name: "booking",
    component: Booking,
    path: "/booking/:id",
    meta: { requiresAuth: false, title: "Đặt lịch", showHeader: false },
  },
  {
    name: "estimator",
    component: Estimator,
    path: "/estimator",
    meta: { requiresAuth: false, title: "Ước tính", showHeader: true },
  },
  {
    name: "promo",
    component: Promo,
    path: "/promo",
    meta: { requiresAuth: false, title: "Chương trình khuyến mãi" },
  },
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
];

export const router = createRouter({
  history: createWebHistory(),
  routes,
});
