using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using BreakingNews.Application.Interfaces;
using BreakingNews.Application.ViewModels;
using BreakingNews.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BreakingNews.Presentation.AspNetCore.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INewsAppService _newsAppService;

        public HomeController(IMapper mapper, INewsAppService newsAppService)
        {
            _mapper = mapper;
            _newsAppService = newsAppService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _newsAppService.GetPublicNews();

            return View(_mapper.Map<IEnumerable<News>, IEnumerable<PublicNewsViewModel>>(data));
        }

        [HttpGet("more/{id}")]
        public async Task<IActionResult> News(string id)
        {
            var model = await _newsAppService.GetByFriendlyName(id);

            if (model == null)
                return NotFound();

            return View("Content", _mapper.Map<News, PublicNewsViewModel>(model));
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
