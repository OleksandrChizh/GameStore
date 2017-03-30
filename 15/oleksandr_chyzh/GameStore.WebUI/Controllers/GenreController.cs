using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Utils;
using GameStore.WebUI.ViewModels.Genre;
using Resources;

namespace GameStore.WebUI.Controllers
{
    [ErrorLogger]
    [PerfomanceCalculator]
    [MvcAuthorise(Roles = "Manager")]
    public class GenreController : BaseController
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public PartialViewResult GetSubgenresModal(int id)
        {
            return PartialView(_genreService.GetDirectSubGenres(id).Select(g => g.ToViewModel()).ToList());
        }

        [HttpGet]
        public ViewResult GetAll()
        {
            return View(_genreService.GetAllGenres().Select(g => g.ToViewModel()).ToList());
        }

        [HttpGet]
        public ViewResult New()
        {
            List<GenreDto> genres = _genreService.GetAllGenres().ToList();
            genres.Insert(0, new GenreDto { Id = default(int), Name = Resource.Default });

            return View(new CreateGenreViewModel { Genres = new SelectList(genres, "Id", "Name", genres.First().Id) });
        }

        [HttpPost]
        public ActionResult New(CreateGenreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                List<GenreDto> genres = _genreService.GetAllGenres().ToList();
                genres.Insert(0, new GenreDto { Id = 0, Name = Resource.Default });
                model.Genres = new SelectList(genres, "Id", "Name", genres.First().Id);

                return View(model);
            }

            if (model.ParentGenreId != null && model.ParentGenreId == default(int))
            {
                model.ParentGenreId = null;
            }

            _genreService.Create(model.Name, model.ParentGenreId);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            IEnumerable<string> subGenres = _genreService.GetDirectSubGenres(id).Select(g => g.Name);

            return View(_genreService.Get(id).ToDeleteGenreViewModel(subGenres));
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(DeleteGenreViewModel model)
        {
            _genreService.Delete(model.Id);

            return RedirectToAction("GetAll");
        }
    }
}