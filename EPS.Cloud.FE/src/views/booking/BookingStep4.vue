<template>
  <div class="summary-wrapper">
    <div class="summary-drop-off">
      <div class="appointment-details" style="padding: 0">
        <div class="sub-h1">Đặt lịch tại</div>

        <div class="main-info">
          <div class="main-info__center">
            <div class="sidebar-item__left">
              <img
                src="https://storage.googleapis.com/rp-production-public-content/puz5muru666yt3hfcbrs86b222f8"
                alt="Ảnh của gara"
              />
            </div>
            <div class="sidebar-item__right">
              <div class="gara-name__wrapper">
                <div class="ds-body-regular ds-medium ng-font-neutral-80">
                  {{ garage?.garage_name }}
                </div>
                <div class="rating">
                  <micon type="Stars" />
                  <div>{{ garage?.avg_rating }}</div>
                  <span>({{ garage?.total_reviews }})</span>
                </div>
              </div>
              <div class="address">
                <span class="address-text">{{ garage?.address }} </span
                ><span class="fz-12">(2.8 km)</span>
              </div>

              <div class="facebook">
                <a :href="garage?.garage_website" target="_blank"
                  >facebook.com</a
                >
              </div>
              <div class="phone">
                <span>{{ garage?.phone }}</span>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="drop-off-time">
        <div class="time-icon">
          <micon type="Clock" />
        </div>
        <div class="time-text">
          <span class="ds-body-regular ds-medium ng-font-neutral-80 ml-1 mr-1"
            >08:30:00 Ngày 30/05/2002</span
          ><span>Sửa</span>
        </div>
      </div>
    </div>
    <div class="summary-vehicle">
      <div class="sub-h1">
        NỘI DUNG SỬA CHỮA <span><a href="">Sửa</a></span>
      </div>
      <div class="car-detail">
        <div class="car-icon">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="18"
            height="18"
            fill="currentColor"
            viewBox="0 0 256 256"
          >
            <path
              d="M240,112H229.2L201.42,49.5A16,16,0,0,0,186.8,40H69.2a16,16,0,0,0-14.62,9.5L26.8,112H16a8,8,0,0,0,0,16h8v80a16,16,0,0,0,16,16H64a16,16,0,0,0,16-16V192h96v16a16,16,0,0,0,16,16h24a16,16,0,0,0,16-16V128h8a8,8,0,0,0,0-16ZM69.2,56H186.8l24.89,56H44.31ZM64,208H40V192H64Zm128,0V192h24v16Zm24-32H40V128H216ZM56,152a8,8,0,0,1,8-8H80a8,8,0,0,1,0,16H64A8,8,0,0,1,56,152Zm112,0a8,8,0,0,1,8-8h16a8,8,0,0,1,0,16H176A8,8,0,0,1,168,152Z"
            ></path>
          </svg>
        </div>
        <div class="car-text">2023 Audi A3 Quattro</div>
      </div>
      <div class="car-problem">
        <div class="problem-icon">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="18"
            height="18"
            fill="currentColor"
            viewBox="0 0 256 256"
          >
            <path
              d="M88,96a8,8,0,0,1,8-8h64a8,8,0,0,1,0,16H96A8,8,0,0,1,88,96Zm8,40h64a8,8,0,0,0,0-16H96a8,8,0,0,0,0,16Zm32,16H96a8,8,0,0,0,0,16h32a8,8,0,0,0,0-16ZM224,48V156.69A15.86,15.86,0,0,1,219.31,168L168,219.31A15.86,15.86,0,0,1,156.69,224H48a16,16,0,0,1-16-16V48A16,16,0,0,1,48,32H208A16,16,0,0,1,224,48ZM48,208H152V160a8,8,0,0,1,8-8h48V48H48Zm120-40v28.7L196.69,168Z"
            ></path>
          </svg>
        </div>
        <div class="problem-text">Vấn đề về xe của bạn</div>
      </div>
      <div class="garage-detail">
        <p>Hỏng đèn pha</p>
      </div>
    </div>
    <div class="summary-contact">
      <div class="sub-h1">
        THÔNG TIN LIÊN LẠC <span><a href="">Sửa</a></span>
      </div>
      <div class="custom-name">Pham Quang Nam</div>
      <div class="custom-email">awildquangtien@gmail.com</div>
      <div class="custom-phone">0999999999</div>
    </div>
    <div class="next-steps-footer flex-center">
      <button class="next-step pointer" @click="handleNextStep">
        <span>Đặt lịch</span>
        <micon type="ArrowRight" />
      </button>
    </div>
    <p
      class="flex-center ds-body-small ds-medium ng-center ng-full-width ng-font-neutral-60 mb-4"
    >
      Bạn sẽ nhận được thống báo về thông tin đã chọn.
    </p>
  </div>
</template>

<script>
export default {
  name: "BookingStep4",
  emits: ["nextStep"],
  props: {
    garageProps: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {
      garage: this.garageProps,
      BookingInfo: {},
    };
  },
  computed: {},
  watch: {
    garageProps: {
      /**
       * A description of the entire function.
       *
       * @param {type} newGarage - description of parameter
       * @return {type}
       */
      handler(newGarage) {
        if (newGarage) {
          this.garage = newGarage;
        }
      },
      deep: true,
    },
  },
  methods: {
    handleNextStep() {
      this.$emit("nextStep");
    },
  },
};
</script>

<style scoped>
@import "./booking.scss";
.next-steps-footer {
  padding: 12px 0px 0px;
  button {
    height: 48px;
  }
}
</style>
