using BreakingNews.Domain.Entities;
using BreakingNews.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BreakingNews.Services.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/News")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _newsService.GetAll();
            return Ok(data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _newsService.GetById(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var data = await _newsService.QuerySingle(f => f.FriendlyUrl == id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post(News model)
        {
            _newsService.Add(model);

            var uri = $"{Request.Path}/{model.Id}";

            return Created(uri, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, News model)
        {
            var data = await _newsService.GetById(id);

            if (data == null)
                return NotFound();

            _newsService.Update(model);

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _newsService.GetById(id);

            if (data == null)
                return NotFound();

            _newsService.Remove(data);

            return Ok();
        }
    }
}