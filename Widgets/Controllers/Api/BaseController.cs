using System.Web.Http;
using Widgets.Data;
using Widgets.Models;

namespace Widgets.Controllers.Api
{
    public class BaseController : ApiController
    {
        IRepository _repository;
        ModelFactory _factory;

        public BaseController(IRepository repository)
        {
            _repository = repository; // getting the repository container using dependency injection
        }

        protected IRepository Repository
        {
            get
            {
                return _repository;
            }
        }

        protected ModelFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new ModelFactory(_repository, Request);
                }
                return _factory;
            }
        }
    }
}
