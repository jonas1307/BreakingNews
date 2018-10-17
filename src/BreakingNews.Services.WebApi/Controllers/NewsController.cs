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
        public async Task<IActionResult> Post([FromBody] News model)
        {
            await _newsService.AddAsync(model);

            var uri = $"{Request.Path}/{model.Id}";

            return Created(uri, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] News model)
        {
            var data = await _newsService.GetById(id);

            if (data == null)
                return NotFound();

            data.Author = model.Author;
            data.IsPublished = model.IsPublished;
            data.FriendlyUrl = model.FriendlyUrl;
            data.Content = model.Content;
            data.CreationDate = model.CreationDate;
            data.PublishDate = model.PublishDate;
            data.LastUpdateDate = model.LastUpdateDate;
            data.Title = model.Title;

            await _newsService.UpdateAsync(data);

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _newsService.GetById(id);

            if (data == null)
                return NotFound();

            await _newsService.RemoveAsync(data);

            return Ok();
        }
    }
}