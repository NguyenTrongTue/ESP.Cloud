<template>
  <div class="wp-pagenavi">
    <a
      class="previouspostslink changePage"
      @click="previousPage"
      rel="prev"
      href="#"
    ></a>
    <a
      v-for="page in visiblePages"
      :key="page"
      class="changePage"
      data-section="promotion"
      :data-page="page"
      @click="changePage(page)"
      href="#"
      :class="{ current: currentPage === page }"
      >{{ page }}</a
    >
    <a
      class="nextpostslink changePage"
      @click="nextPage"
      rel="next"
      href="#"
    ></a>
  </div>
</template>

<script>
export default {
  name: "Pagination",
  props: {
    totalPage: {
      type: Number,
      required: true,
    },
  },
  data() {
    return {
      currentPage: 1,
    };
  },
  computed: {
    visiblePages() {
      const totalPages = Math.ceil(this.totalPage / 5);
      const startPage = (this.currentPage - 1) * 5 + 1;
      const endPage = Math.min(startPage + 4, totalPages);
      return Array.from(
        { length: endPage - startPage + 1 },
        (_, index) => startPage + index
      );
    },
  },
  methods: {
    previousPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
        this.changePage(this.currentPage);
      }
    },
    nextPage() {
      if (this.currentPage < this.totalPage) {
        this.currentPage++;
        this.changePage(this.currentPage);
      }
    },
    changePage(page) {
      if (page >= 1 && page <= this.totalPage) {
        this.currentPage = page;
        // Gửi sự kiện hoặc thực hiện hành động khác liên quan đến chuyển trang
        // Ví dụ: this.$emit('page-changed', page);
      }
    },
  },
};
</script>

<style lang="scss">
@import "./pagination.scss";
</style>
