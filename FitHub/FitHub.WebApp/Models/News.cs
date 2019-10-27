using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitHub.WebApp.Models
{
    public class News
    {
        public Guid NewsId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string SportTypeTag { get; set; }
        public string GoalTypeTag { get; set; }
        public string DifficultyLevelTag { get; set; }
    }
}
