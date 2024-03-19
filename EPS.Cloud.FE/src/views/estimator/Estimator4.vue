<template>
  <div class="result-wrapper">
    <div class="estimator-result">
      <div class="estimate-results-background"></div>
      <div class="estimate-info">
        <div class="estimate-info-content">
          <div class="estimate-info-title">
            <div class="icon-price">
              <micon type="PricePlus" />
            </div>
            <h1 class="title-text">Nhận ước tính của bạn</h1>
          </div>
          <div class="estimate-info-big-text">
            {{ resultEstimate.total_locations }} điểm sửa chữa chứng nhận gần
            bạn
          </div>
          <div class="estimate-info-wrapper">
            <div class="serrated-top"></div>
            <div class="estimate-info-card">
              <div class="estimate-info-card-content">
                <div class="estimate-info-header">
                  <div class="estimate-info-header-repairname">
                    Ước tính chi phí
                  </div>
                  <div class="estimate-total-range">
                    {{ estimateAmount }}
                  </div>
                </div>
                <div class="labor-parts">
                  <div class="cost-breakdown">
                    <div class="labor-parts-cost-breakdown">
                      Phân tích chi phí
                    </div>
                  </div>
                  <div class="cost-percent">
                    <div class="percent-left">
                      <div class="labor-dot ng-mr8"></div>
                      <span class="dot-text">Labor (100%)</span>
                    </div>
                    <div class="percent-right">
                      <div class="parts-dot ng-mr8"></div>
                      <span class="dot-text">Parts (0%)</span>
                    </div>
                  </div>
                  <div class="cost-range">
                    <span
                      class="labor-bar round-right"
                      style="width: 100%"
                    ></span>
                    <span class="parts-bar" style="width: 0%"></span>
                  </div>
                </div>
                <div class="estimate-details">
                  <div class="estimate-detail">
                    <span class="estimate-detail-disclaimer-text"
                      >Ước tính này không bao gồm thuế, phí chẩn đoán và các
                      loại phụ phí khác.
                      <button class="learn-more-btn">Tìm hiểu thêm</button>
                    </span>
                  </div>
                  <div class="estimate-info-bottom-actions">
                    <div class="button-bottom-wrapper">
                      <mbutton
                        @click="handleGetResult"
                        :buttonText="`Xem ${resultEstimate.total_locations} Địa điểm`"
                      />
                    </div>
                    <div class="estimate-info-actions">
                      <div class="actions-wrapper">
                        <button class="share-up-btn">
                          <div class="shareup-icon-wrapper">
                            <micon type="ShareUp" />
                          </div>
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="main-vertical-separator"></div>
            <div class="estimate-info-summary">
              <div class="estimate-info-summary-title">Tổng kết dịch vụ</div>
              <div class="estimate-info-summary-vehicle">
                {{ estimateInfo.make }} {{ estimateInfo.model }}
                {{ estimateInfo.year }}
              </div>
              <div class="estimate-info-summary-location">
                {{ this.currentAddress?.formatted_address }}
              </div>
              <div class="dotted-separator"></div>
              <div class="estimate-info-summary-service">
                Thay dầu nhớ &amp; Kiểm tra tổng quá
                <tbody></tbody>
              </div>
              <div class="estimate-info-summary-details">
                <a class="edit-link" href="/estimator/repair-services">Sửa</a>
              </div>
              <div class="estimate-info-repair-notes">
                <a class="estimate-info-repair-notes-link"
                  >Xem nhật ký sửa chữa</a
                >
              </div>
              <div class="dotted-separator"></div>
            </div>
          </div>
        </div>
      </div>
      <div class="middle-info">
        <div class="middle-info-content">
          <div class="info-content-left">
            <div class="advantage-title">
              Không lo tốn quá nhiều chi phí sửa chữa.
              <span class="blue-word">Đã được chứng nhận!</span>
            </div>
          </div>
          <div class="info-content-right">
            <div class="advantage">
              <div class="advantage-icon">
                <micon type="Repair" />
              </div>
              <div class="advantage-text">
                Bạn sẽ nhận được chất lượng công việc cao nhất từ ​​các kỹ thuật
                viên sửa chữa giàu kinh nghiệm.
              </div>
            </div>
            <div class="advantage">
              <div class="advantage-icon">
                <micon type="Price" />
              </div>
              <div class="advantage-text">
                Ước tính được thực hiện tại tất cả các địa điểm trên toàn quốc -
                đảm bảo cho bạn mức giá hợp lý.
              </div>
            </div>
            <div class="advantage">
              <div class="advantage-icon">
                <micon type="Certify" />
              </div>
              <div class="advantage-text">
                Tất cả các địa điểm được RepairPal chứng nhận đều cung cấp bảo
                hành dịch vụ tối thiểu 12 tháng/12 km.
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BookingAPI from "@/apis/BookingAPI";
import { formatAmount } from "@/utils/common";
export default {
  name: "Estimator4",
  emits: ["nextStep"],
  components: {},
  props: {
    estimateInfo: {
      type: Object,
      default: {},
    },
  },
  data() {
    return {
      resultEstimate: [],
      carId: this.estimateInfo.cars_id,
      currentAddress: null,
    };
  },
  watch: {},
  computed: {
    estimateAmount() {
      if (this.resultEstimate.min_price == this.resultEstimate.max_price) {
        return `${this.format(this.resultEstimate.min_price)}`;
      } else {
        return `${this.format(this.resultEstimate.min_price)} đến
                    ${this.format(this.resultEstimate.max_price)}`;
      }
    },
  },
  mounted() {
    this.getEstimateForServices();
    let value = this.$common.cache.getCache("currentAddress");
    this.currentAddress = value.results[0];
  },
  methods: {
    async getEstimateForServices() {
      const result = await BookingAPI.getEstimateService({
        carId: this.carId,
        serviceCodes: this.estimateInfo.selectedServices,
      });
      this.resultEstimate = result[0];
    },
    format(amount) {
      return formatAmount(amount) + " VNĐ";
    },
    handleGetResult() {
      this.$router.push({
        path: `/estimate/${this.resultEstimate.estimate_id}`,
      });
    },
  },
};
</script>

<style lang="scss">
@import "./estimator.scss";
</style>
