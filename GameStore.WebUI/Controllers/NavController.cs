﻿using GameStore.Domain.Abstract;
using GameStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IGameRepository repository;

        public NavController(IGameRepository repo)
        {
            repository = new EFGameRepository();
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Games
                .Select(game => game.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }

    }
}