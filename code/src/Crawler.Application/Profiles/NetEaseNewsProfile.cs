using AutoMapper;
using Crawler.Application.Contracts;
using Crawler.Domain.Entities.NetEaseNews;

namespace Crawler.Application.Profiles
{
    public class NetEaseNewsProfile : Profile
    {
        public NetEaseNewsProfile()
        {
            CreateMap<NetEaseNewsArticleData, NetEaseNewsArticle>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => string.Join(",", src.Images)))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.PublisheTime, opt => opt.MapFrom(src => src.PublisheTime))
                .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId))
                .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => src.UpdatedTime))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));
        }
    }
}