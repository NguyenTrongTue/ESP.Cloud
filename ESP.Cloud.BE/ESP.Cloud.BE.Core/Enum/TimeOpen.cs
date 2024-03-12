using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Cloud.BE.Core.Enum
{
    /// <summary>
    /// Enum định nghĩa thời gian mở cửa của gara
    /// </summary>
    public enum TimeOpenEnum
    {   
        /// <summary>
        /// Ngay bây giờ
        /// </summary>
        Now = 0,
        /// <summary>
        /// Trước giờ làm việc
        /// </summary>
        BeforeWork = 1,
        /// <summary>
        /// Sau giờ làm việc
        /// </summary>
        AfterWork = 2,
        /// <summary>
        /// Tất cả các khoảng thời gian
        /// </summary>
        AllTime = 3
    }
}
