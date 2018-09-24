using AutoMapper;
using BreakingNews.Application.ViewModels;
using BreakingNews.Domain.Entities;

namespace BreakingNews.Application.AutoMapper
{
    public class ViewModelsToDomain : Profile
    {
        public ViewModelsToDomain()
        {
            CreateMap<NewsViewModel, News>();
            CreateMap<PublicNewsViewModel, News>();
        }
    }
}
