using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitHub.WebApp.Data;
using FitHub.WebApp.Models;
using FitHub.WebApp.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.WebApp.Controllers
{
    [Route("api/News")]
    public class NewsController : Controller
    {
        private readonly DataBaseContext dataBase;

        public NewsController(DataBaseContext dataBase)
        {
            this.dataBase = dataBase;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNews([FromBody]AddNewsRequest request)
        {
            News news = new News()
            {
                Title = request.Title,
                Body = request.Body,
                SportTypeTag = request.SportTypeTag,
                GoalTypeTag = request.GoalTypeTag,
                DifficultyLevelTag = request.DifficultyLevelTag
            };

            await dataBase.News.AddAsync(news);
            await dataBase.SaveChangesAsync();

            return Ok(news.NewsId);
        }

        [HttpGet("get/{page}")]
        public IActionResult GetNews(int page)
        {
            return Json(dataBase.News
               .Skip(page * 10)
               .Take(10)
               .ToList()
               .Select(p => new
               {
                   p.Title,
                   p.Body,
                   p.GoalTypeTag,
                   p.SportTypeTag,
                   p.DifficultyLevelTag
               }));
        }

        [HttpGet("get_recomends/{userId}")]
        public async Task<IActionResult> GetRecomends(Guid userId)
        {
            var user = await dataBase.Users.FindAsync(userId);

            var result = dataBase
                .News
                .Where(p => p.GoalTypeTag == user.UserLevel.ToString())
                .Take(5)
                .Select(n => new
                {
                    n.Title,
                    n.Body,
                    n.GoalTypeTag,
                    n.SportTypeTag,
                    n.DifficultyLevelTag
                });
            return Json(result);
        }
    }
}