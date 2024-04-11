<template>
  <div class="promo-page-wrapper">
    <div class="promo-filter-left">
      <promofilter />
      <promofilter />
      <promofilter />
    </div>
    <div class="promo-list-right">
      <div class="article-list">
        <div class="article-large">
          <!-- Hiển thị bài viết lớn (2x2) - Tin mới nhất -->
          <div class="article-item"><promoitem /></div>
        </div>
        <div class="article-small">
          <!-- Hiển thị 2 bài viết nhỏ (1x1) bên phải -->
          <div class="article-item"><promoitem /></div>
          <div class="article-item"><promoitem /></div>
        </div>
        <div class="article-grid">
          <!-- Hiển thị danh sách các dòng bài viết (mỗi dòng gồm 3 bài 1x1) -->
          <div
            v-for="(row, index) in articleRows"
            :key="index"
            class="article-row"
          >
            <div v-for="article in row" :key="article" class="article-item">
              <promoitem />
            </div>
          </div>
        </div>
      </div>
      <div class="pagination-article">
        <pagination :totalPage="90" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Promo",
  props: {},
  data() {
    return {
      articles: [
        // Danh sách các bài viết
        "Bài viết 1",
        "Bài viết 2",
        "Bài viết 3",
        "Bài viết 4",
        "Bài viết 5",
        "Bài viết 6",
        "Bài viết 7",
        "Bài viết 8",
        "Bài viết 9",
        "Bài viết 10",
        "Bài viết 11",
        "Bài viết 12",
        // Thêm các bài viết khác vào đây
      ],
    };
  },
  watch: {},

  methods: {},
  computed: {
    articleRows() {
      // Tạo danh sách các dòng bài viết (mỗi dòng gồm 3 bài 1x1)
      const rows = [];
      for (let i = 3; i < this.articles.length; i += 3) {
        rows.push(this.articles.slice(i, i + 3));
      }
      return rows;
    },
  },
};
</script>

<style lang="scss">
@import "./promo.scss";
</style>
