using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitHub.WebApp.Models
{
    public class Train
    {
        public Guid TrainId { get; set; }
        public string TrainName { get; set; }
        public Activity Activity { get; set; }
        public List<SportExercise> SportExercises { get; set; }
    }
}
