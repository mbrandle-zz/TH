using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TH.Classes
{
    public class ActivityResponse
    {
        public int ID { get; set; }
        public DateTime schedule { get; set; }
        public string title { get; set; }
        public DateTime created_at { get; set; }
        public string status { get; set; }
        public string condition { get; set; }
        public PropertyResponse property { get; set; }
        public string survey { get; set; }
    }
}
