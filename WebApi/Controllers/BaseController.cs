using NLog;
using Repositories.Interfaces;
using System.Web.Http;

namespace WebClient.Controllers
{
    public class BaseController : ApiController
    {
        protected readonly IUnitOfWork _db;
        protected readonly ILogger _logger;

        protected BaseController(IUnitOfWork unitOfWork, ILogger logger)
        {
            _db = unitOfWork;
            _logger = logger;
        }
    }
}
