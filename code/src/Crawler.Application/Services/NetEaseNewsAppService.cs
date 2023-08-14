using AutoMapper;
using Crawler.Application.Contracts;
using Crawler.Application.Dependency;
using Crawler.Domain.Entities.NetEaseNews;
using Crawler.Domain.Repository;
using Yitter.IdGenerator;

namespace Crawler.Application.Services
{
    public class NetEaseNewsAppService : ITransientSelfDependency
    {
        public readonly IMapper _mapper;
        private readonly ICmdRepository _cmdRepository;

        public NetEaseNewsAppService(IMapper mapper, ICmdRepository cmdRepository)
        {
            _mapper = mapper;
            _cmdRepository = cmdRepository;
        }

        public async Task SaveParseDataAsync(NetEaseNewsArticleData data)
        {
            var entity = _mapper.Map<NetEaseNewsArticle>(data);

            entity.Id = YitIdHelper.NextId();
            entity.TenantId = 0L;
            entity.CreatedBy = 0L;
            entity.UpdatedBy = 0L;
            entity.CreatedTime = DateTime.UtcNow;
            entity.UpdatedTime = DateTime.UtcNow;

            await _cmdRepository.InsertAsync(entity);
        }
    }
}