using System.Threading.Tasks;
using BreakingNews.Domain.Entities;
using BreakingNews.Repositories.App;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreakingNews.Services.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/News")]
    public class NewsController : Controller
    {
        private readonly BreakingNewsContext _context = new BreakingNewsContext();

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _context.News.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _context.FindAsync<News>(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var data = await _context.News.FirstOrDefaultAsync(f => f.FriendlyUrl == id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(News value)
        {
            await _context.News.AddAsync(value);

            var uri = $"{Request.GetUri().AbsoluteUri}/{value.Id}";

            return Created(uri, value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, News value)
        {
            var data = await _context.FindAsync<News>(id);

            if (data == null)
                return NotFound();

            _context.Entry(value).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.FindAsync<News>(id);

            if (data == null)
                return NotFound();

            _context.Remove(data);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}