using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackingAssistance.Models;

namespace TrackingAssistance.LoadMappers
{
    public class DBmapper
    {
        public static MapperConfiguration sentToDisplay = new MapperConfiguration(cfg =>
               cfg.CreateMap<FourkitesTrackingSentLeg, LoadDisplay>()
                   .ForMember(dest => dest.BolNum, act => act.MapFrom(src => src.bol_number))
                   .ForMember(dest => dest.Status, act => act.MapFrom(src => src.current_status))
                   .ForMember(dest => dest.StatusTime, act => act.MapFrom(src => src.pickedup_time))
                   .ForMember(dest => dest.IsChecked, act => act.MapFrom(src => src.is_checked))
         );

        public static MapperConfiguration backupToDisplay = new MapperConfiguration(cfg =>
           cfg.CreateMap<backupLoad, LoadDisplay>()
               .ForMember(dest => dest.BolNum, act => act.MapFrom(src => src.bol_number))
               .ForMember(dest => dest.Status, act => act.MapFrom(src => "DELIVERED"))
               .ForMember(dest => dest.StatusTime, act => act.MapFrom(src => src.delete_data))
               .ForMember(dest => dest.IsChecked, act => act.MapFrom(src => src.is_checked))
     );


    }
}