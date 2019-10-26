using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitHub.WebApp.Data;
using FitHub.WebApp.Models;
using FitHub.WebApp.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitHub.WebApp.Controllers
{
    [Route("api/activity")]
    public class ActivityController : Controller
    {
        private readonly DataBaseContext dataBase;

        public ActivityController(DataBaseContext dataBase)
        {
            this.dataBase = dataBase;
        }

        [HttpPost("create/userId")]
        public async Task<IActionResult> CreateActivity([FromBody]ActivityCreateRequest request, Guid userId)
        {
            var user = await dataBase.Users.Include(i => i.Activities).FirstOrDefaultAsync(u => u.Id == userId);
            Activity activity = new Activity()
            {
                ActivityName = request.ActivityName,
                StartTime = DateTime.Parse(request.StartTime),
                EndTime = DateTime.Parse(request.EndTime),
                Location = request.Location,
                Trains = new List<Train>()
            };

            user.Activities.Add(activity);
            await dataBase.Activities.AddAsync(activity);
            await dataBase.SaveChangesAsync();

            return Ok(activity.ActivityId);
        }

        [HttpPost("addtrain/{activityId}")]
        public async Task<IActionResult> AddTrain([FromBody]List<TrainAddRequest> request, Guid activityId)
        {
            var activity = await dataBase.Activities.Include(i => i.Trains).FirstOrDefaultAsync(id => id.ActivityId == activityId);
            if (activity == null)
            {
                return NotFound();
            }

            List<Guid> ids = new List<Guid>();
            foreach (var train in request)
            {
                Train training = new Train()
                {
                    TrainName = train.TrainName,
                    Activity = activity,
                    SportExercises = new List<SportExercise>()
                };
                
                activity.Trains.Add(training);
                await dataBase.Trains.AddAsync(training);
                await dataBase.SaveChangesAsync();
                ids.Add(training.TrainId);
            }
            return Ok(ids);
        }

        [HttpPost("addexercise/{trainId}")]
        public async Task<IActionResult> AddExercise([FromBody]List<ExerciseAddRequest> request, Guid trainId)
        {
            var activity = await dataBase.Trains.Include(i => i.SportExercises).FirstOrDefaultAsync(id => id.TrainId == trainId);
            if (activity == null)
            {
                return NotFound();
            }

            List<Guid> ids = new List<Guid>();
            foreach (var exercise in request)
            {
                SportExercise exer = new SportExercise()
                {
                    ExerciseName = exercise.ExerciseName,
                    Train = activity,
                    Approaches = new List<Approach>()
                };

                activity.SportExercises.Add(exer);
                await dataBase.SportExercises.AddAsync(exer);
                await dataBase.SaveChangesAsync();
                ids.Add(exer.SportExerciseId);
            }
            return Ok(ids);
        }

        [HttpPost("addapproaches/{exerciseId}")]
        public async Task<IActionResult> AddApproaches([FromBody]List<ApproachesAddRequest> request, Guid exerciseId)
        {
            var activity = await dataBase.SportExercises.Include(i => i.Approaches).FirstOrDefaultAsync(id => id.SportExerciseId == exerciseId);
            if (activity == null)
            {
                return NotFound();
            }

            List<Guid> ids = new List<Guid>();
            foreach (var exercise in request)
            {
                Approach exer = new Approach()
                {
                    ApproachNum = activity.Approaches.Count + 1,
                    RepeatCount = exercise.RepeatCount,
                    WorkingWeight = exercise.WorkingWeight

                };

                activity.Approaches.Add(exer);
                await dataBase.Approaches.AddAsync(exer);
                await dataBase.SaveChangesAsync();
                ids.Add(exer.ApproachId);
            }
            return Ok(ids);
        }
    }
}