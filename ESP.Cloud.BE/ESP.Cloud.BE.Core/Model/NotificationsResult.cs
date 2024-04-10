using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Cloud.BE.Core.Model
{
    public class NotificationsResult
    {
        public List<BookingDetail> bookingDetails { get; set; } = new List<BookingDetail>();
        public List<PromoNofications> promoNofications { get; set; } = new List<PromoNofications>();
    }
}
