<template>
  <div class="promo-page-wrapper">
    <div class="promo-filter-left">
      <promofilter
        title="Lọc theo thời gian"
        @selected-filter="handleFilterByTime"
        :filterItems="timeFilterType"
        
      />
      <promofilter
        title="Lọc theo mẫu xe"
        @selected-filter="handleFilterByCarName"
        :filterItems="carFilterType"
      />
    </div>
    <div class="promo-list-right">
      <div class="article-list">
        <div class="article-large" v-if="pagiArticles.length > 0">
          <!-- Hiển thị bài viết lớn (2x2) - Tin mới nhất -->
          <div class="article-item">
            <promoitem
              :src="pagiArticles[0]?.image_link"
              :date="pagiArticles[0]?.start_date"
              :title="pagiArticles[0]?.title"
              @click-item="handlClickItem(pagiArticles[0]?.promotions_id)"
            />
          </div>
        </div>
        <div class="article-small">
          <!-- Hiển thị 2 bài viết nhỏ (1x1) bên phải -->
          <div class="article-item" v-if="pagiArticles.length > 1">
            <promoitem
              :src="pagiArticles[1]?.image_link"
              :date="pagiArticles[1]?.start_date"
              :title="pagiArticles[1]?.title"
              @click-item="handlClickItem(pagiArticles[1]?.promotions_id)"
            />
          </div>
          <div class="article-item" v-if="pagiArticles.length > 2">
            <promoitem
              :src="pagiArticles[2]?.image_link"
              :date="pagiArticles[2]?.start_date"
              :title="pagiArticles[2]?.title"
              @click-item="handlClickItem(pagiArticles[2]?.promotions_id)"
            />
          </div>
        </div>
        <div class="article-grid">
          <!-- Hiển thị danh sách các dòng bài viết (mỗi dòng gồm 3 bài 1x1) -->
          <div
            v-for="(row, index) in articleRows"
            :key="index"
            class="article-row"
          >
            <div v-for="article in row" :key="article" class="article-item">
              <promoitem
                :src="article?.image_link"
                :date="article?.start_date"
                :title="article?.title"
                @click-item="handlClickItem(article?.promotions_id)"
              />
            </div>
          </div>
        </div>
      </div>
      <div class="pagination-article">
        <Pagination
          :currentPage="currentPage"
          @handleChangePage="handleChangePage"
          :pageSize="pageSize"
          :length="articles.length"
        />
      </div>
    </div>
  </div>
</template>

<script>
import Pagination from "@/components/pagination/Pagination.vue";
import PromoAPI from "@/apis/PromoAPI";
export default {
  name: "Promo",
  components: {
    Pagination,
  },
  props: {},
  data() {
    return {
      articles: {},
      pagiArticles: {},
      currentPage: 0,
      pageSize: 12,
      filterByTime: "",
      filterByCarName: "",
      timeFilterType: [
        { title: "Hôm nay" },
        { title: "Tháng này" },
        { title: "Năm nay" },
      ],
      carFilterType: [
        { title: "Toyota" },
        { title: "Honda" },
        { title: "Mazda" },
        { title: "Hyundai" },
        { title: "Kia" },
        { title: "Ford" },
        { title: "Chevrolet" },
        { title: "Nissan" },
        { title: "Suzuki" },
        { title: "Mercedes" },
        { title: "BMW" },
        { title: "Audi" },
        { title: "Lexus" },
      ],
    };
  },
  watch: {
    articles: {
      handler: function (val) {
        this.handleChangePage(0);
      },
      deep: true,
    },
  },
  async mounted() {
    const me = this;
    const result = await PromoAPI.getPromoFilter(
      me.filterByTime,
      me.filterByCarName
    );
    me.articles = result;
    me.handleChangePage(0);
  },
  methods: {
    async fetchPromo() {
      const me = this;
      const result = await PromoAPI.getPromoFilter(
        me.filterByTime,
        me.filterByCarName
      );
      me.articles = result;
  
    },
    handleChangePage(page) {
      this.currentPage = page;
      if (page > this.articles.length / this.pageSize - 1) {
        this.pagiArticles = this.articles.slice(
          this.currentPage * this.pageSize,
          this.articles.length
        );
      } else {
        this.pagiArticles = this.articles.slice(
          this.currentPage * this.pageSize,
          (this.currentPage + 1) * this.pageSize
        );
      }
    },
    async handleFilterByTime(value) {
      const me = this;
      if (value === "Hôm nay") {
        me.filterByTime = 1;
      } else if (value === "Tháng này") {
        me.filterByTime = 2;
      } else if (value === "Năm nay") {
        me.filterByTime = 3;
      } else {
        me.filterByTime = null;
      }
      await me.fetchPromo();
    },
    async handleFilterByCarName(value) {
      const me = this;
      me.filterByCarName = value;
      await me.fetchPromo();
    },
    handlClickItem(promoId) {
      this.$router.push({path: `/promo-detail/${promoId}`});
    },
  },
  computed: {
    articleRows() {
      // Tạo danh sách các dòng bài viết (mỗi dòng gồm 3 bài 1x1)
      const rows = [];
      for (let i = 3; i < this.pagiArticles.length; i += 3) {
        rows.push(this.pagiArticles.slice(i, i + 3));
      }
      return rows;
    },
  },
};
</script>

<style lang="scss">
@import "./promo.scss";
</style>
