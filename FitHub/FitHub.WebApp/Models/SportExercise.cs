using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitHub.WebApp.Models
{
    public class SportExercise
    {
        public Guid SportExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public Train Train { get; set; }

        public List<Approach> Approaches { get; set; } 
    }
}
