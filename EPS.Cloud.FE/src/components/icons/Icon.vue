<template>
  <Clock v-if="type == 'Clock'" />
  <Close v-if="type == 'Close'" />
  <Direction v-if="type == 'Direction'" />
  <Done v-if="type == 'Done'" />
  <Eye v-if="type == 'Eye'" />
  <HideEye v-if="type == 'HideEye'" />
  <Set v-if="type == 'Set'" />
  <ChevronLeft v-if="type == 'ChevronLeft'" />
  <ChevronRight v-if="type == 'ChevronRight'" />
  <ChevronDown v-if="type == 'ChevronDown'" />
  <ChevronTop v-if="type == 'ChevronTop'" />
  <ArrowLeft v-if="type == 'ArrowLeft'" />
  <Books v-if="type == 'Books'" />
  <Car v-if="type == 'Car'" />
  <Logo v-if="type == 'Logo'" />
  <Previous v-if="type == 'Previous'" />
  <Next v-if="type == 'Next'" />
  <ArrowRight v-if="type == 'ArrowRight'" />
  <Stars v-if="type == 'Stars'" />
  <Note v-if="type == 'Note'" />
  <Avatar v-if="type == 'Avatar'" />
  <Search v-if="type == 'Search'" />
</template>

<script>
import Clock from "./Clock.vue";
import Close from "./Close.vue";
import Direction from "./Direction.vue";
import Done from "./Done.vue";
import Eye from "./Eye.vue";
import HideEye from "./HideEye.vue";
import ChevronLeft from "./ChevronLeft.vue";
import ChevronRight from "./ChevronRight.vue";
import ChevronDown from "./ChevronDown.vue";
import ChevronTop from "./ChevronTop.vue";
import ArrowLeft from "./ArrowLeft.vue";
import ArrowRight from "./ArrowRight.vue";
import Books from "./Books.vue";
import Car from "./Car.vue";
import Logo from "./Logo.vue";
import Previous from "./Previous.vue";
import Next from "./Next.vue";
import Stars from "./Stars.vue";
import Note from "./Note.vue";
import Avatar from "./Avatar.vue";
import Search from "./Search.vue";
export default {
  name: "Icon",
  props: {
    type: {
      type: String,
      default: "",
    },
  },
  components: {
    Clock,
    Close,
    Direction,
    Done,
    Eye,
    HideEye,
    Set,
    ChevronLeft,
    ChevronRight,
    ChevronDown,
    ArrowLeft,
    Books,
    Car,
    Logo,
    Previous,
    Next,
    ArrowRight,
    ChevronTop,
    Stars,
    Note,
    Avatar,
    Search,
  },
};
</script>
./ArrowLeft.vue./ChevronLeft.vue
