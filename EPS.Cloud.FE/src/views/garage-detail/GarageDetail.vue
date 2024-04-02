<template>
  <div class="garage-detail">

    <div class="garage-detail__header">
      <img :src="garage?.image">
      <img :src="garage?.image">
      <img :src="garage?.image">
    </div>

    <div class="garage-detail__body">
      <div class="navigator">
        <a class="tab_item flex-center  " href="#overview">
          <span class="ds-tab ng-heading-5 ds-medium navigation-tab ">Tổng quan</span>
        </a>
        <a class="tab_item flex-center" href="#about">

          <span class="ds-tab ng-heading-5 ds-medium navigation-tab">Về chúng tôi</span>
        </a>
        <a class="tab_item flex-center" href="#reviews">

          <span class="ds-tab ng-heading-5 ds-medium navigation-tab">Đánh giá ({{
        garage?.total_rating }})</span>
        </a>
      </div>

      <div id="overview">
        <div class="overview__left">
          <div class="rating">
            <micon type="Stars" />
            <span class="rating_avg">{{ garage?.avg_rating?.toFixed(2) }}</span>
            <span> &nbsp;·&nbsp;</span>
            <a href="#reviews" class="total_rating">{{ garage?.total_rating }} đánh giá</a>
          </div>

          <h1 class="shop-name">{{ garage?.garage_name }}</h1>
          <div class="warranty-description">Cửa hàng sửa chữa ô tô đầy đủ dịch vụ cung cấp bảo hành 12 tháng/12.000 dặm
            và mức giá sửa chữa hợp lý.</div>
          <div class="location-container">
            <div class="address">

              <span>{{ garage.address }}</span>
            </div>
            <div class="links">
              <div class="directions"><a target="_blank" rel="noopener noreferrer"
                  :href="`https://www.google.com/maps/dir/?api=1&destination=${garage.latitude},${garage.longitude}`">
                  Đường đi
                </a></div>
              <div class="street-view"><a target="_blank" rel="noopener noreferrer"
                  :href="`https://www.google.com/maps?q&layer=c&cbll=${garage.latitude},${garage.longitude}`">
                  Quang cảnh đường phố</a>
              </div>
            </div>

            <div class="shop-hours ng-flex ng-flex-row pointer ds-medium ng-center-y ng-pt8 ng-pb8">
              <span :class="buildOpenTimeText.split('·')[0].includes('Đang mở')
        ? 'active'
        : 'close'
        ">{{ buildOpenTimeText.split("·")[0] }} </span>
              <span>&nbsp;·&nbsp;</span>
              <span> {{ buildOpenTimeText.split("·")[1] }}</span>
              <div class="icon">
              </div>
            </div>
            <div class="shop-phone ng-pb8">{{ garage.phone }}</div>
            <div class="shop-website">

              <a :href="garage.garage_website" target="_blank">facebook.com</a>
            </div>
          </div>
        </div>
        <div class="overview__right">
          <div class="soonest-availability-container">
            <h1 class="soonest-availability-header">Tình trạng cửa hàng</h1>
            <div class="soonest-availability"><span :class="buildOpenTimeText.split('·')[0].includes('Đang mở')
        ? 'active'
        : 'close'
        ">{{ buildOpenTimeText.split("·")[0] }} </span>
              <span>&nbsp;·&nbsp;</span>
              <span> {{ buildOpenTimeText.split("·")[1] }}</span>
            </div>

            <mbutton button-text="Đặt lịch hẹn" @click="handleBooking" />

            <mbutton button-text="Sẵn sàng kiểm tra" @click="handleBooking" class="none-background mt-1" />
          </div>
          <div class="help-card-container ng-mb24 ng-flex ng-center">
            <div class="header">Không biết có chuyện gì không?</div>

            <div class="ng-font-neutral-60 ng-flex ng-text-center">Cửa hàng này có thể giúp bạn chẩn đoán vấn đề của
              mình, bất kể bạn đang gặp phải âm thanh, khói hay mùi gì.
            </div>
          </div>

        </div>
      </div>
      <div class="horizontal-separator mt-1 mb-2" style="width: 60%"></div>
      <div id="about">

        <div class="amenities-container">
          <div class="amenities-header  mb-2">Tính năng của cửa hàng</div>

          <div class="amenity-item mt-1 mb-1">

            <div class="amenity-icon mr-1">
              <micon type="Certification" />


            </div>

            <div class="amenity-right">
              <div class="amenity-title mb-1">Cửa hàng sửa chữa ô tô được chứng nhận</div>
              <div class="amenity-description">Cửa hàng đáng tin cậy này không bao giờ tính phí quá cao và đáp ứng các
                tiêu chuẩn được chứng nhận của RepairPal về mức giá hợp lý.</div>
            </div>
          </div>
          <div class="amenity-item mt-1 mb-1">

            <div class="amenity-icon mr-1">
              <micon type="CarLarge" />


            </div>

            <div class="amenity-right">
              <div class="amenity-title mb-1">Dịch vụ của {{ garage.total_sevices }} hãng xe</div>
              <div class="amenity-description">Trung tâm ô tô {{ garage.garage_name }} là một cửa hàng đầy đủ dịch vụ
                có
                thể
                thực hiện tất cả các sửa chữa lớn và bảo trì theo lịch trình</div>
            </div>
          </div>
          <div class="amenity-item mt-1 mb-1">

            <div class="amenity-icon mr-1">
              <micon type="Calendar" />


            </div>

            <div class="amenity-right">
              <div class="amenity-title mb-1">Lập kế hoạch linh hoạt</div>
              <div class="amenity-description">Cửa hàng mở cửa vào tất cả các giờ hành chính trong ngày</div>
            </div>
          </div>

          <div class="amenity-item mt-1 mb-1">

            <div class="amenity-icon mr-1">
              <micon type="Certification" />


            </div>

            <div class="amenity-right">
              <div class="amenity-title mb-1">Sự đảm bảo</div>
              <div class="amenity-description">{{ garage.garage_name }} bảo hành công việc của họ trong 12.000 km và
                12
                tháng.</div>
            </div>
          </div>
        </div>
        <div class="horizontal-separator mt-1 mb-2" style="width: 100%" </div>

          <div class="map">

            <iframe width="100%" height="250" style="border:0" loading="lazy" allowfullscreen
              referrerpolicy="no-referrer-when-downgrade" :src="googleMapUrl">
            </iframe>


          </div>

        </div>

      </div>
      <div class="horizontal-separator mt-1 mb-2" style="width: 100%"></div>
      <div id="reviews">

        <div class="amenities-header  mb-2">Đánh giá tổng thể</div>

        <div class="flex-between">
          <div class="summary">
            <div class="icon-summanry">

              <micon type="Stars" :width="16" :height="16" />
              <micon type="Stars" :width="16" :height="16" />
              <micon type="Stars" :width="16" :height="16" />
              <micon type="Stars" :width="16" :height="16" />
              <micon type="Stars" :width="16" :height="16" />
            </div>
            <div class="rating">
              <span class="rating_avg">{{ garage?.avg_rating?.toFixed(2) }}</span>
              <span> &nbsp;·&nbsp;</span>
              <a href="#reviews" class="total_rating">( {{ garage?.total_rating }} đánh giá )</a>
            </div>

          </div>
        </div>
        <div class="horizontal-separator mt-2" style="width: 60%"></div>
        <div>
          <ReviewItem v-for="review in reviews" :key="review.garage_reviews_id" :review="review" />
        </div>

        <Panigation :currentPage="currentPage" @handleChangePage="handleChangePage" :length="garage?.total_rating" />
      </div>
    </div>
</template>

<script>
import GarageAPI from '@/apis/GarageAPI';
import ReviewItem from '@/views/garage-detail/ReviewItem.vue';
import Panigation from '@/views/garage-detail/Panigation.vue';
export default {
  name: "GarageDetail",
  props: {},
  components: {
    ReviewItem,
    Panigation
  },
  data() {
    return {
      garageId: null,
      garage: {},
      reviews: [],
      currentPage: 0,
      pageSize: 5, // mặc định là 5
    }
  },
  computed: {

    buildOpenTimeText() {
      let timeOpen = this.garage.open_time,
        timeClose = this.garage.close_time;
      if (timeOpen && timeClose) {


        const current = new Date();
        const open = new Date(
          current.getFullYear(),
          current.getMonth(),
          current.getDate(),
          timeOpen.split(":")[0],
          timeOpen.split(":")[1]
        );
        const close = new Date(
          current.getFullYear(),
          current.getMonth(),
          current.getDate(),
          timeClose.split(":")[0],
          timeClose.split(":")[1]
        );

        if (current >= open && current <= close) {
          const [house, minutes] = timeClose.split(":");
          return `Đang mở · Đóng cửa lúc ${house}h${minutes}`;
        } else if (current <= open) {
          const [house, minutes] = timeOpen.split(":");
          return `Đóng cửa · Mở cửa lúc ${house}h${minutes}`;
        } else {
          const [house, minutes] = timeOpen.split(":");
          return `Đóng cửa · Mở cửa vào ${house}h${minutes} ${this.$ms.common.convertDate(
            current.getDay()
          )}`;
        }
      } else {
        return ''
      }
    },
    googleMapUrl() {
      const apiKey = window.__congfigGoogleMapAPI; // Hoặc bất kỳ cách nào bạn lưu trữ API key
      const query = `${this.garage.latitude},${this.garage.longitude}`;
      return `https://www.google.com/maps/embed/v1/place?key=${apiKey}&q=${query}`;
    }
  },
  watch: {},
  async mounted() {
    const me = this;
    me.garageId = me.$route.params.id;
    const result = await GarageAPI.getGarageById(me.garageId);
    me.garage = result;

    await me.fetchReviews();
  },


  methods: {


    handleBooking() {
      this.$router.push({ path: `/booking/${this.garage.garage_id}` });
    },
    /**
     * Asynchronously fetches reviews from the garage API.
     *
     * @return {Promise} A promise that resolves with the fetched reviews.
     */
    async fetchReviews() {
      const me = this;
      me.reviews = await GarageAPI.getGarageReviewsById({
        garageId: me.garageId,
        take: me.pageSize,
        skip: me.currentPage
      })
    },

    async handleChangePage(page) {
      this.currentPage = page;

      await this.fetchReviews();
    }
  },
};
</script>

<style lang="scss" scoped>
@import "./garage-detail.scss";
</style>
