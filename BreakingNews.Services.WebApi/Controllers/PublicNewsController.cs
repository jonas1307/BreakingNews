using BreakingNews.Domain.Entities;
using BreakingNews.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BreakingNews.Services.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/PublicNews")]
    public class PublicNewsController : Controller
    {
        private readonly INewsService _newsService;

        public PublicNewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _newsService.QueryMultiple(q => q.IsPublished);
            return Ok(data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _newsService.QueryMultiple(q => q.IsPublished && q.Id == id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var data = await _newsService.QueryMultiple(q => q.IsPublished && q.FriendlyUrl == id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post(News model)
        {
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, News model)
        {
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return BadRequest();
        }
    }
}