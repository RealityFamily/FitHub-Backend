using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitHub.WebApp.Models
{
    public class Approach
    {
        public Guid ApproachId { get; set; }
        public int ApproachNum { get; set; }
        public int RepeatCount { get; set; }
        public float WorkingWeight { get; set; }
        public SportExercise SportExercise { get; set; }
    }
}
