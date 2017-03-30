using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Utils;

namespace GameStore.WebUI.Handlers
{
    public class AsyncGameImageLoader : HttpTaskAsyncHandler
    {
        private const int StatusCodeOk = 200;
        private const int StatusCodeBadRequest = 400;

        private readonly IGameService _gameService;

        public AsyncGameImageLoader()
        {
            _gameService = DependencyResolver.Current.GetService<IGameService>();
        }

        public override async Task ProcessRequestAsync(HttpContext context)
        {
            string gameKey = context.Request.Url.Segments[2];
            gameKey = gameKey.Replace("/", string.Empty);
            HttpFileCollection images = context.Request.Files;

            int statusCodeResult = StatusCodeBadRequest;

            if (images.Count > 0)
            {
                HttpPostedFile image = images[0];

                if (ImageManager.IsImage(image))
                {
                    string imagePath = await ImageManager.SaveImageAsync(image, context.Request.MapPath);
                    _gameService.UpdateImage(gameKey, imagePath);

                    statusCodeResult = StatusCodeOk;
                }
            }

            context.Response.StatusCode = statusCodeResult;
        }
    }
}