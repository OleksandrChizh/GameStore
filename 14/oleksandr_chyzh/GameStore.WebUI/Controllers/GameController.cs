using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Enums;
using GameStore.Services.Interfaces.Utils;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Exceptions;
using GameStore.WebUI.Models.Game;
using GameStore.WebUI.Utils;
using GameStore.WebUI.ViewModels.Game;
using Resources;

namespace GameStore.WebUI.Controllers
{
    [ErrorLogger]
    [PerfomanceCalculator]
    public class GameController : BaseController
    {
        private readonly IGameService _gameService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IPublisherService _publisherService;
        private readonly IGenreService _genreService;

        public GameController(
            IGameService gameService,
            IPlatformTypeService platformTypeService,
            IPublisherService publisherService,
            IGenreService genreService)
        {
            _gameService = gameService;
            _platformTypeService = platformTypeService;
            _publisherService = publisherService;
            _genreService = genreService;
        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public int GetAmountOfGames()
        {
            return _gameService.GetAmountOfGames();
        }

        [HttpGet]
        [ActionName("GetAll")]
        public ViewResult GetAllGames(GamesViewModel model)
        {
            SetUpModelForGamesFilter(model);

            if (!ModelState.IsValid)
            {
                model.Games = _gameService.GetGames(null).Select(g => g.ToShortViewModel()).ToList();
                return View(model);
            }

            GamesFilterAttributes attributes = GetAttributesFromModel(model);
            IEnumerable<GameDto> gameDtos = _gameService.GetGames(attributes);           
            model.Games = gameDtos.Select(g => g.ToShortViewModel()).ToList();

            return View(model);
        }

        [HttpGet]
        public ViewResult GetGameByKey(string gamekey)
        {
            GameDto game = _gameService.GetGameByKey(gamekey);
            _gameService.AddView(game.Id);
            game.ViewCount++;

            GameViewModel model = game.ToViewModel();
            return View(model);
        }

        [HttpGet]
        [EventLogger]
        [ActionName("Download")]
        [OutputCache(Duration = 60)]
        public FileResult DownloadGame(string gamekey)
        {
            const string gamePath = "~/Content/game.txt";
            const string fileType = "application/txt";
            const string fileName = "Game.txt";

            string filePath = Server.MapPath(gamePath);
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            return File(filePath, fileType, fileName);
        }

        [HttpGet]
        [ActionName("Remove")]
        [MvcAuthorise(Roles = "Manager")]
        public ViewResult RemoveGame(string gamekey)
        {
            GameDto game = _gameService.GetGameByKey(gamekey);

            string currentCulture = Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);
            string gameName = game.LanguagesNames.ContainsKey(currentCulture) ? game.LanguagesNames[currentCulture] : game.LanguagesNames["ru"];

            return View("RemoveGame", new DeleteGameModel { Id = game.Id, Name = gameName });
        }

        [HttpPost]
        [EventLogger]
        [ActionName("Remove")]
        [MvcAuthorise(Roles = "Manager")]
        public ActionResult RemoveGame(DeleteGameModel model)
        {
            _gameService.Delete(model.Id);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        [ActionName("New")]
        [MvcAuthorise(Roles = "Manager")]
        public ViewResult NewGame()
        {
            var model = new CreateGameViewModel
            {
                GenresIds = new List<int>(),
                PlatformTypesIds = new List<int>(),
            };
            SetMultiSelectListsForCreateGameViewModel(model);

            return View(model);
        }

        [HttpPost]
        [EventLogger]
        [ActionName("New")]
        [MvcAuthorise(Roles = "Manager")]
        public ActionResult NewGame(CreateGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetMultiSelectListsForCreateGameViewModel(model);
                return View(model);
            }

            IDictionary<string, string> languagesNames = new Dictionary<string, string>();
            IDictionary<string, string> languagesDescriptions = new Dictionary<string, string>();

            languagesNames.Add("ru", model.Name);
            languagesDescriptions.Add("ru", model.Description);

            if (model.IsContainEnglishTranslation)
            {
                languagesNames.Add("en", model.EnglishName);
                languagesDescriptions.Add("en", model.EnglishDescription);
            }

            _gameService.Create(
                model.Key,
                languagesNames,
                languagesDescriptions,
                model.Price,
                model.UnitsInStock,
                model.Discounted,
                model.PublishingDate,
                model.GenresIds,
                model.PlatformTypesIds,
                model.PublishersIds);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        [ActionName("Update")]
        [MvcAuthorise(Roles = "Manager")]
        public ActionResult UpdateGame(string gameKey)
        {
            GameDto game = _gameService.GetGameByKey(gameKey);
            if (game.Deleted)
            {
                throw new GameDeletedException();
            }

            IEnumerable<PlatformTypeDto> platformTypes = _platformTypeService.GetAllPlatformTypes();
            var platformTypesMultiselect = new MultiSelectList(platformTypes, "Id", "Type");

            IEnumerable<PublisherDto> publishers = _publisherService.GetAllPublishers();
            var publishersMultiselect = new MultiSelectList(publishers, "Id", "CompanyName");

            IEnumerable<GenreDto> genres = _genreService.GetAllGenres();
            var genresMultiselect = new MultiSelectList(genres, "Id", "Name");

            var model = game.ToUpdateViewModel();
            model.AllPlatformTypes = platformTypesMultiselect;
            model.AllGenres = genresMultiselect;
            model.AllPublishers = publishersMultiselect;

            return View(model);
        }

        [HttpPost]
        [EventLogger]
        [ActionName("Update")]
        [MvcAuthorise(Roles = "Manager")]
        public ActionResult UpdateGame(UpdateGameViewModel model)
        {
            IDictionary<string, string> languagesNames = new Dictionary<string, string>();
            IDictionary<string, string> languagesDescriptions = new Dictionary<string, string>();

            languagesNames.Add("ru", model.Name);
            languagesDescriptions.Add("ru", model.Description);

            if (model.IsContainEnglishTranslation)
            {
                languagesNames.Add("en", model.EnglishName);
                languagesDescriptions.Add("en", model.EnglishDescription);
            }

            _gameService.Edit(
                model.GameId,
                languagesNames,
                languagesDescriptions,
                model.Price,
                model.UnitsInStock,
                model.Discounted,
                model.PlatformTypeIds,
                model.PublisherIds,
                model.GenreIds);

            return RedirectToAction("GetAll");
        }

        private void SetMultiSelectListsForCreateGameViewModel(CreateGameViewModel model)
        {
            IEnumerable<GenreDto> genres = _genreService.GetAllGenres();
            IEnumerable<PlatformTypeDto> platformTypes = _platformTypeService.GetAllPlatformTypes();
            IEnumerable<PublisherDto> publishers = _publisherService.GetAllPublishers();

            model.Genres = new MultiSelectList(genres, "Id", "Name");
            model.PlatformTypes = new MultiSelectList(platformTypes, "Id", "Type");
            model.Publishers = new MultiSelectList(publishers, "Id", "CompanyName");
        }

        private GamesFilterAttributes GetAttributesFromModel(GamesViewModel model)
        {
            var attributes = new GamesFilterAttributes
            {
                Genres = model.SelectedGenresIds.ToList(),
                PlatformTypes = model.SelectedPlatformTypesIds.ToList(),
                Publishers = model.SelectedPublishersIds.ToList(),
                SortingObject = model.SortingObject,
                PublishingDatePeriod = model.PublishingDatePeriod,
                MinPrice = model.MinPrice,
                MaxPrice = model.MaxPrice,
                GameNameSearchingString = model.GameName,
                PageInfo = model.PageInfo 
            };

            return attributes;
        }

        private void SetUpModelForGamesFilter(GamesViewModel model)
        {
            const int startMaxPrice = 1000000;

            model.SelectedGenresIds = model.SelectedGenresIds ?? new int[] { };
            model.SelectedPlatformTypesIds = model.SelectedPlatformTypesIds ?? new int[] { };
            model.SelectedPublishersIds = model.SelectedPublishersIds ?? new int[] { };

            model.Genres = _genreService.GetAllGenres().Select(g => g.ToViewModel()).ToList();
            model.PlatformTypes = _platformTypeService.GetAllPlatformTypes().Select(pt => pt.ToViewModel()).ToList();
            model.Publishers = _publisherService.GetAllPublishers().Select(p => p.ToShortViewModel()).ToList();

            if (model.MaxPrice == default(decimal))
            {
                model.MaxPrice = startMaxPrice;
            }
            
            model.PageInfo = model.PageInfo ?? new PageInfo
                             {
                                 PageNumber = 1,
                                 PageSize = PageSize.AllItems,
                                 TotalItems = _gameService.GetAmountOfGames()
                             };

            model.Resource = model.Resource ?? new GamesResource();

            model.Resource.Name = Resource.Name;
            model.Resource.Price = Resource.Price;
            model.Resource.Details = Resource.Details;
            model.Resource.Edit = Resource.Edit;
            model.Resource.Remove = Resource.Remove;
            model.Resource.Submit = Resource.Submit;
            model.Resource.Hide = Resource.Hide;
            model.Resource.Show = Resource.Show;
        }
    }
}