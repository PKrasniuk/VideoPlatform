using System;
using AutoMapper;
using Microsoft.ML.Data;
using MongoDB.Bson;
using VideoPlatform.AIL.Models.SearchResultModels;
using VideoPlatform.AIL.Models.TripModels;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.Api.Models.ResponseModels;
using VideoPlatform.DAL.DataModels;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.Api.Models
{
    internal class MappingProfile : Profile
    {
        internal MappingProfile()
        {
            CreateMap<PartnerTypes, PartnerTypesModel>();
            CreateMap<AddPartnerModel, Partner>();
            CreateMap<UpdatePartnerModel, Partner>();
            CreateMap<Partner, PartnerModel>();
            CreateMap<Setting, SettingModel>();
            CreateMap<AddSettingModel, Setting>();
            CreateMap<UpdateSettingModel, Setting>();
            CreateMap<Tag, TagModel>();
            CreateMap<TagModel, Tag>();
            CreateMap<AddTagModel, Tag>();
            CreateMap<UpdateTagModel, Tag>();
            CreateMap<UserRoleDataModel, UserRoleModel>();

            CreateMap<ObjectId, string>().ConvertUsing(o => o.ToString());
            CreateMap<string, ObjectId>().ConvertUsing(s => ObjectId.Parse(s));
            CreateMap<BsonString, string>().ConvertUsing(o => o.ToString());
            CreateMap<string, BsonString>().ConvertUsing(s => new BsonString(s));
            CreateMap<BsonDateTime, DateTime>().ConvertUsing(o => o.ToUniversalTime());
            CreateMap<DateTime, BsonDateTime>().ConvertUsing(t => new BsonDateTime(t));

            CreateMap<Guid, string>().ConvertUsing(o => o.ToString());
            CreateMap<string, Guid>().ConvertUsing(s => Guid.Parse(s));

            CreateMap<AddMetaDataModel, MetaData>();
            CreateMap<UpdateMetaDataModel, MetaData>();
            CreateMap<MetaData, MetaDataModel>();

            CreateMap<AddInfoDataModel, InfoData>();
            CreateMap<UpdateInfoDataModel, InfoData>();
            CreateMap<InfoData, InfoDataModel>();

            CreateMap<InputTripModel, TripModel>();
            CreateMap<TripFarePredictionModel, OutputTripFarePredictionModel>();
            CreateMap<SearchResultPredictionModel, OutputSearchResultPredictionModel>();
            CreateMap<RegressionMetrics, RegressionMetricsModel>();
            CreateMap<RankingMetrics, RankingMetricsModel>();
        }
    }
}