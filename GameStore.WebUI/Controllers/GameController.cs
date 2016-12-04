using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Models;
using GameStore.Domain.Concrete;

namespace GameStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        private IGameRepository repository;
        public int pageSize = 4;

        public GameController(IGameRepository repo)
        {
            repository = new EFGameRepository();
        }

        
        public ViewResult List(string category, int page = 1)
        {
            GamesListViewModel model = new GamesListViewModel
            {
                Games = repository.Games
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(game => game.GameId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? repository.Games.Count() : repository.Games.Where(game => game.Category == category).Count()
                },
                CurrentCategory = category
            };
            ViewBag.i = RouteData.Values["category"];
            return View(model);
        }


        public FileContentResult GetImage(int gameId)
        {
            Game game = repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                return File(game.ImageData, game.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ViewResult Test()
        {

            ViewBag.h = HttpContext;

            int i = HttpContext.Request.Cookies.Count;
               HttpContext.Response.AppendCookie(new HttpCookie((i+1).ToString(), "mycoo"));

            return View();
        }

    }
}