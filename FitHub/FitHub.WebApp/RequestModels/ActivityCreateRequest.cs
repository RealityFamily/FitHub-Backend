using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitHub.WebApp.RequestModels
{
    public class ActivityCreateRequest
    {
        public string ActivityName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Location { get; set; }
    }
}
