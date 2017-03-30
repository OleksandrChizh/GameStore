using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Models.Publisher;

namespace GameStore.WebUI.Controllers.Api
{
    [ApiErrorHandler]
    [ApiAuthorize(Roles = "Manager")]
    public class PublishersController : BaseApiController
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        public HttpResponseMessage Get()
        {
            IEnumerable<PublisherDto> publishers = _publisherService.GetAllPublishers();

            return Request.CreateResponse(HttpStatusCode.OK, publishers);
        }

        public HttpResponseMessage Get([FromUri] int id)
        {
            PublisherDto publisher = _publisherService.Get(id);

            return Request.CreateResponse(HttpStatusCode.OK, publisher);
        }

        public HttpResponseMessage Post([FromBody] UpdatePublisherModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            IDictionary<string, string> descriptions = new Dictionary<string, string>();

            descriptions.Add("ru", model.Description);

            if (model.IsContainEnglishTranslation)
            {
                descriptions.Add("en", model.EnglishDescription);
            }

            _publisherService.Update(model.Id, descriptions, model.HomePage);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put([FromBody] CreatePublisherModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            IDictionary<string, string> descriptions = new Dictionary<string, string>();

            descriptions.Add("ru", model.Description);

            if (model.IsContainEnglishTranslation)
            {
                descriptions.Add("en", model.EnglishDescription);
            }

            int publisherId = _publisherService.Create(model.CompanyName, descriptions, model.HomePage);

            return Request.CreateResponse(HttpStatusCode.Created, publisherId);
        }

        public HttpResponseMessage Delete([FromUri] int id)
        {
            _publisherService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}