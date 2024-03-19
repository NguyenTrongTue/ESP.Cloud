using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Param
{
    /// <summary>
    /// Param lấy phần ước tính sửa chữa
    /// </summary>
    public class EstimateParam
    {
        public Guid carId { get; set; }

        public List<ServiceCode> serviceCodes { get; set; } = new List<ServiceCode>();
    }
}
