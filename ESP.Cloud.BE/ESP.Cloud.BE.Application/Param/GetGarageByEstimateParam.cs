using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Cloud.BE.Application.Param
{
    public class GetGarageByEstimateParam
    {

        /// <summary>
        /// Kinh độ
        /// </summary>
        public double? latitude { get; set; }
        /// <summary>
        /// Vĩ độ
        /// </summary>
        public double? longitude { get; set; }

        public Guid p_estimate_id { get; set; }
    }
}
