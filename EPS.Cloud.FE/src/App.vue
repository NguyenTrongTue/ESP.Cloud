<template>
  <DefaultLayout :show-header="showHeader"
    ><router-view></router-view
  ></DefaultLayout>
  <MToast></MToast>
  <MDialog />
  <mloading />
</template>

<script>
import DefaultLayout from "@/layouts/DefaultLayout.vue";
import { RouterLink } from "vue-router";
import MToast from "@/components/toast/MToast.vue";
import MDialog from "@/components/dialog/MDialog.vue";
import MLoading from "@/components/loading/MLoading.vue";
export default {
  props: { ...RouterLink.props },
  components: {
    DefaultLayout,
    MToast,
    MDialog,
    MLoading,
  },

  mounted() {},
  computed: {
    showHeader() {
      const routeMeta = this.$route.meta;

      return !(routeMeta && routeMeta.showHeader === false);
    },
  },
};
</script>
