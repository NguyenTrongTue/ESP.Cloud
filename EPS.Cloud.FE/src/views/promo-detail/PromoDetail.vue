<template>
  <div class="detail-overlay-wrapper">
    <div class="detail-title">
      <h1 class="title">Chương trình khuyến mãi</h1>
    </div>
    <div class="detail-date">
      <span class="date">
        <micon type="Calendar"> </micon>{{ promodetail.start_date }}
      </span>
      <span class="date">
        <micon type="ShareUp"> </micon>Chia sẻ
      </span>
    </div>
    <div class="detail-summary">
      <p>
        {{ promodetail.summary }}
      </p>
      <div class="detail-summary-img">
        <img class="alignnone size-full wp-image-30793 webpexpress-processed lazyloaded" :src="promodetail.image_link"
          data-src="https://yamaha-motor.com.vn/wp/wp-content/uploads/2023/10/BDCN-RinhNgayQuaTang-1200x900-1.jpg"
          alt="" width="1200" height="900"
          data-srcset="https://yamaha-motor.com.vn/wp/wp-content/uploads/2023/10/BDCN-RinhNgayQuaTang-1200x900-1.jpg 1200w, https://yamaha-motor.com.vn/wp/wp-content/uploads/2023/10/BDCN-RinhNgayQuaTang-1200x900-1-300x225.jpg 300w, https://yamaha-motor.com.vn/wp/wp-content/uploads/2023/10/BDCN-RinhNgayQuaTang-1200x900-1-1024x768.jpg 1024w, https://yamaha-motor.com.vn/wp/wp-content/uploads/2023/10/BDCN-RinhNgayQuaTang-1200x900-1-150x113.jpg 150w, https://yamaha-motor.com.vn/wp/wp-content/uploads/2023/10/BDCN-RinhNgayQuaTang-1200x900-1-768x576.jpg 768w"
          data-sizes="(max-width: 1200px) 100vw, 1200px" sizes="(max-width: 1200px) 100vw, 1200px" />
      </div>
    </div>
    <div class="detail-content" v-html="promodetail.description" </div>
      <div class="detail-rule">

      </div>
      <div class="promo-footer">
        <button @click="goBack">
          < Quay Lại</button>
      </div>
    </div>
</template>
<script>
import PromoAPI from "@/apis/PromoAPI";
export default {
  name: "PromoDetail",
  props: {},
  data() {
    return {
      promoId: "",
      promodetail: {
        promotions_id: "eac70170-24a4-4191-bee0-ead5fe0fa197",
        image_link:
          "https://hyundai-api.thanhcong.vn/storage/uploads/editors/2024/04/02/KV-KMT04%20(1200x628).jpg",
        summary:
          "Từ nay cho đến hết 30/04/2024, Liên doanh ô tô Hyundai Thành Công Việt Nam (HTV) chính thức triển khai chương trình ưu đãi đặc biệt “Mừng Đại lễ - Rước xế Hyundai” áp dụng tại tất cả các Đại lý trên toàn quốc.",
        title:
          "Hyundai Thành Công triển khai chương trình ưu đãi tháng 4 cho khách hàng",
        description:
          '<p>Toyota Việt Nam c&ugrave;ng với c&aacute;c c&ocirc;ng ty đối t&aacute;c (Toyota Ch&acirc;u &Aacute; &ndash; Th&aacute;i B&igrave;nh Dương &amp; Toyota Tsusho) c&ugrave;ng th&agrave;nh lập trang web&nbsp;<a href="https://sanxe.toyotasure.vn/" target="_blank" rel="noopener noreferrer">https://sanxe.toyotasure.vn/</a>&nbsp;với hoạt động mua b&aacute;n xe cũ Toyota &amp; Lexus th&ocirc;ng qua h&igrave;nh thức trả gi&aacute;. Th&ocirc;ng qua webite, xe của kh&aacute;ch h&agrave;ng sẽ được c&aacute;c đại l&yacute; trả gi&aacute; trực tuyến. Với thủ tục nhanh ch&oacute;ng v&agrave; dễ d&agrave;ng, quy tr&igrave;nh xem xe v&agrave; trả gi&aacute; chỉ diễn ra trong 1 ng&agrave;y, Sanxe sẽ hỗ trợ kh&aacute;ch h&agrave;ng từ l&uacute;c đấu gi&aacute; th&agrave;nh c&ocirc;ng cho tới khi ho&agrave;n tất thủ tục thanh to&aacute;n.</p>\r\n<p>Trong th&aacute;ng 3, Sanxe triển khai nhiều ưu đ&atilde;i đặc biệt d&agrave;nh cho kh&aacute;ch h&agrave;ng b&aacute;n xe cũ v&agrave; người giới thiệu.</p>\r\n<p><strong>1. Ưu đ&atilde;i d&agrave;nh cho người giới thiệu kh&aacute;ch h&agrave;ng b&aacute;n xe &ocirc; t&ocirc; cũ</strong></p>\r\n<p>Từ 01/03 đến 31/03/2024, Sanxe.toyotasure.vn&nbsp;<strong>d&agrave;nh tặng 5.000.000 VNĐ</strong>&nbsp;cho mỗi&nbsp;<strong>kh&aacute;ch h&agrave;ng b&aacute;n xe th&agrave;nh c&ocirc;ng hoặc cho người giới thiệu th&agrave;nh c&ocirc;ng&nbsp;</strong>kh&aacute;ch b&aacute;n xe. Chương tr&igrave;nh được &aacute;p dụng cho những người b&aacute;n xe v&agrave; người giới thiệu tại TP.HCM v&agrave; B&igrave;nh Dương.</p>\r\n<p><strong>C&aacute;ch thức tham gia</strong></p>\r\n<p>-&nbsp; Đăng k&yacute; t&agrave;i khoản: Truy cập&nbsp;<a href="https://sanxe.toyotasure.vn/" target="_blank" rel="noopener noreferrer">https://sanxe.toyotasure.vn/</a>&nbsp;để tạo t&agrave;i khoản c&aacute; nh&acirc;n.</p>\r\n<p>-&nbsp; Li&ecirc;n hệ hỗ trợ: Sau khi đăng k&yacute;, li&ecirc;n hệ với đội ngũ hỗ trợ của Sanxe để tiến h&agrave;nh giới thiệu kh&aacute;ch h&agrave;ng.</p>\r\n<p>-&nbsp; Cung cấp th&ocirc;ng tin: Chia sẻ th&ocirc;ng tin chi tiết về người b&aacute;n xe v&agrave; th&ocirc;ng tin xe. Lưu &yacute;: th&ocirc;ng tin cần phải đầy đủ v&agrave; ch&iacute;nh x&aacute;c.</p>\r\n<p>-&nbsp; Nhận thưởng: Sau khi giao dịch b&aacute;n xe th&agrave;nh c&ocirc;ng, người giới thiệu sẽ nhận được phần thưởng 5.000.000 VNĐ.</p>\r\n<p><strong>2. Ưu đ&atilde;i d&agrave;nh cho kh&aacute;ch h&agrave;ng b&aacute;n &ocirc; t&ocirc; cũ v&agrave; mua xe mới tại đại l&yacute; Toyota</strong></p>\r\n<p>Từ 01/03 đến 31/03/2024, Sanxe.toyota.vn&nbsp;<strong>d&agrave;nh tặng 10.000.000 VNĐ</strong>&nbsp;d&agrave;nh cho kh&aacute;ch h&agrave;ng b&aacute;n &ocirc; t&ocirc; cũ v&agrave; mua &ocirc; t&ocirc; Toyota mới. Chương tr&igrave;nh &aacute;p dụng cho kh&aacute;ch h&agrave;ng tại TP. Hồ Ch&iacute; Minh v&agrave; B&igrave;nh Dương.</p>\r\n<p><strong>Điều kiện v&agrave; điều khoản</strong></p>\r\n<p>Trước khi tham gia, vui l&ograve;ng đọc kỹ điều khoản v&agrave; điều kiện tại&nbsp;<a href="https://sanxe.toyotasure.vn/terms/" target="_blank" rel="noopener noreferrer">https://sanxe.toyotasure.vn/terms/</a>. &nbsp;Mọi quyết định cuối c&ugrave;ng thuộc về ban tổ chức.</p>\r\n<p><strong>Li&ecirc;n hệ</strong></p>\r\n<p>Nếu c&oacute; bất kỳ thắc mắc n&agrave;o hoặc cần hỗ trợ, li&ecirc;n hệ hoặc nhắn tin với ch&uacute;ng t&ocirc;i qua hotline: 0707 700 086.</p>',
        start_date: "2024-04-16T00:00:00",
        end_date: "2024-05-01T00:00:00",
        garage_id: "ab8c7c6c-9277-4b8a-b846-32f8dbd6c2e8",
      },
    };
  },
  watch: {},
  async mounted() {
    const me = this;
    me.promoId = me.$route.params.id;
    const result = await PromoAPI.getPromoById(me.promoId);
    me.promodetail = result;
  },

  methods: {

    goBack() {
      this.$router.go(-1);
    },
  },
};
</script>
<style lang="scss">
@import "./promodetail.scss";
</style>
