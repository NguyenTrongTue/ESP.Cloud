using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Cloud.BE.Application.Param
{
    public class LikeOrUnLikeParam
    {

        public Guid car_review_id { get; set; }
        public Guid user_id { get; set; }

        public bool liked { get; set; } = false;

    }
}
