using AutoMapper;
using BreakingNews.Application.ViewModels;
using BreakingNews.Domain.Entities;

namespace BreakingNews.Application.AutoMapper
{
    public class DomainToViewModels : Profile
    {
        public DomainToViewModels()
        {
            CreateMap<News, NewsViewModel>();
            CreateMap<News, PublicNewsViewModel>();
        }
    }
}
