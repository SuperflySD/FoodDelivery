using System.Web.Mvc;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Linq;

namespace GameStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IGameRepository repository;

        public AdminController(IGameRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Games);
        }


        public ViewResult Edit(int gameId)
        {
            Game game = repository.Games
                .FirstOrDefault(g => g.GameId == gameId);
            return View(game);
        }

    }
}