using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BreakingNews.Application.Interfaces;
using BreakingNews.Application.ViewModels;
using BreakingNews.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BreakingNews.Presentation.AspNetCore.Controllers
{
    [Authorize(Roles = "admin")]
    public class NewsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INewsAppService _newsAppService;

        public NewsController(INewsAppService newsAppService, IMapper mapper)
        {
            _newsAppService = newsAppService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _newsAppService.GetAll();

            return View(_mapper.Map<IEnumerable<News>, IEnumerable<NewsViewModel>>(data));
        }

        public IActionResult New()
        {
            return View("FormNews", new NewsViewModel());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _newsAppService.GetById(id);

            if (data == null)
                return NotFound();

            return View("FormNews", _mapper.Map<News, NewsViewModel>(data));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(NewsViewModel model)
        {
            var friendlyName = await _newsAppService.GetByFriendlyName(model.FriendlyUrl);

            if (friendlyName != null && friendlyName.Id != model.Id)
            {
                ModelState.AddModelError("FriendlyUrl", "A URL já está associada com outra notícia.");
            }

            if (!ModelState.IsValid)
            {
                return View("FormNews", model);
            }

            if (model.IsPublished)
                model.PublishDate = DateTime.Now;

            if (model.Id == 0)
            {
                model.CreationDate = DateTime.Now;
                _newsAppService.Add(_mapper.Map<NewsViewModel, News>(model));
            }
            else
            {
                model.LastUpdateDate = DateTime.Now;
                _newsAppService.Update(_mapper.Map<NewsViewModel, News>(model));
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _newsAppService.GetById(id);

            if (data == null)
                return NotFound();

            _newsAppService.Remove(data);

            return RedirectToAction("Index");
        }
    }
}