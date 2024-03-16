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
            50 điểm sửa chữa chứng nhận RepairPal gần bạn
          </div>
          <div class="estimate-info-wrapper">
            <div class="serrated-top"></div>
            <div class="estimate-info-card">
              <div class="estimate-info-card-content">
                <div class="estimate-info-header">
                  <div class="estimate-info-header-repairname">
                    Chẩn đoán và kiểm tra đèn động cơ
                  </div>
                  <div class="estimate-total-range">$344 to $439</div>
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
                      <mbutton buttonText="Xem 50 Địa điểm" />
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
                2023 Alfa Romeo Giulia
              </div>
              <div class="estimate-info-summary-location">42000 - Nam Định</div>
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
      <div class="testimonials">
        <mrating v-for="i in 3" :key="i" />
      </div>
      <div class="why-choose-section">
        <div class="why-choose-section-content">
          <div class="why-choose-header">
            <div class="why-choose-header-title">
              <div class="why-choose-h1">Tại sao bạn chọn chúng tôi?</div>
              <div class="let-see">Hãy xem so sánh sau nhé!</div>
            </div>
            <div class="why-choose-versus">
              <div class="logo-us">
                <img
                  class="rp-certified-logo"
                  src="https://files.repairpal.com/packs/ba2289af5140b752.png"
                  alt="RepairPal Certified"
                />
              </div>
              <div class="logo-versus">vs</div>
              <div class="logo-competitor">
                <div class="repair-icon">
                  <micon type="Repair" />
                </div>
                <div class="repair-text">Các cửa hàng truyền thống</div>
              </div>
            </div>
          </div>
          <div class="why-comparison" v-for="i in 4" :key="i">
            <div class="comparison-content">
              <div class="why-reason">
                Dịch vụ sửa chữa được trải qua quá trình chứng nhận nghiêm ngặt
              </div>
              <div class="oui-non">
                <div class="com-first">
                  <div class="com-icon-wrapper">
                    <micon type="Yes" />
                  </div>
                </div>
                <div class="com-second">
                  <div class="com-icon-wrapper">
                    <micon type="No" />
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="why-choose-bottom">
            <div class="view-location-btn-wrapper">
              <mbutton buttonText="Xem 50 Địa điểm" />
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="shop-result">
      <div class="custom-locations">
        <div class="custom-location-header">Thay dầu nhớt ở Hà Nội</div>
        <div class="let-see">
          Chúng tôi tìm thấy 50 địa điểm chứng nhận A gần bạn
        </div>
      </div>
      <div class="gara-list">
        <div class="gara-item" v-for="i in 3" :key="i">
          <SideBarItem :location="location" />
        </div>
      </div>
      <div class="view-all-wrapper">
        <button>Xem tất cả</button>
      </div>
    </div>
  </div>
</template>

<script>
import SideBarItem from "@/components/sidebar-item/SideBarItem.vue";
export default {
  name: "Estimator4",
  components: {
    SideBarItem,
  },
  props: {},
  data() {
    return {
      location: {
        garage_name: "Gara 1",
        total_rating: 4.5,
        address: "1234, Đường 1, Quận 1, TP.HCM",
        distance: 1.2,
        garage_website: "https://www.facebook.com",
        phone: "0123456789",
        open_time: "08:00",
        close_time: "17:00",
      },
    };
  },
  watch: {},

  methods: {},
};
</script>

<style lang="scss">
@import "./estimator.scss";
</style>
