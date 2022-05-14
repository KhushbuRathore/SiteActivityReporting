using AutoMapper;
using SiteActivityReporting.Common;
using SiteActivityReporting.Domain;
using SiteActivityReporting.DTO;
using System;
using System.Collections.Generic;

namespace SiteActivityReporting.Helper.Mapper
{
    public static partial class MappingExtensions
    {
        public static IList<ActivityEventDTO> ToModel(this IList<ActivityEvent> entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ActivityEvent, ActivityEventDTO>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<IList<ActivityEvent>, IList<ActivityEventDTO>>(entity);
        }

        public static ActivityEventDTO ToModel(this ActivityEvent entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ActivityEvent, ActivityEventDTO>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<ActivityEvent, ActivityEventDTO>(entity);
        }
        public static ActivityEvent ToEntity(this ActivityEventDTO model, string key)
        {

            model.CreatedOn = DateTime.Now;
            model.ModifiedOn = DateTime.Now;
            model.IsDeleted = false;
            model.Key = key;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ActivityEventDTO, ActivityEvent>()
                .ForMember(m => m.Value, opt => opt.MapFrom(src => CommonHelper.RoundToNearestNumber(src.Value)));
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<ActivityEventDTO, ActivityEvent>(model);
        }

    }
}
