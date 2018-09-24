using System.Threading.Tasks;
using BreakingNews.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreakingNews.Services.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/PublicNews")]
    public class PublicNewsController : Controller
    {
        private readonly INewsService _newsService;

        public PublicNewsController(INewsService newsNewsService)
        {
            _newsService = newsNewsService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var data = await _newsService.GetPublicNews();

        //    return Ok(data);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    var data = await _newsService.GetPublicNews();

        //    return Ok(data);
        //}
    }
}