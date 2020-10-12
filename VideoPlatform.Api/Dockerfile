FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["VideoPlatform.AIL/VideoPlatform.AIL.csproj", "VideoPlatform.AIL/"]
COPY ["VideoPlatform.Api/VideoPlatform.Api.csproj", "VideoPlatform.Api/"]
COPY ["VideoPlatform.BLL/VideoPlatform.BLL.csproj", "VideoPlatform.BLL/"]
COPY ["VideoPlatform.CacheService/VideoPlatform.CacheService.csproj", "VideoPlatform.CacheService/"]
COPY ["VideoPlatform.Common/VideoPlatform.Common.csproj", "VideoPlatform.Common/"]
COPY ["VideoPlatform.CQRS/VideoPlatform.CQRS.csproj", "VideoPlatform.CQRS/"]
COPY ["VideoPlatform.DAL/VideoPlatform.DAL.csproj", "VideoPlatform.DAL/"]
COPY ["VideoPlatform.Domain/VideoPlatform.Domain.csproj", "VideoPlatform.Domain/"]
COPY ["VideoPlatform.ElasticSearchService/VideoPlatform.ElasticSearchService.csproj", "VideoPlatform.ElasticSearchService/"]
COPY ["VideoPlatform.ExternalService/VideoPlatform.ExternalService.csproj", "VideoPlatform.ExternalService/"]
COPY ["VideoPlatform.MessageService/VideoPlatform.MessageService.csproj", "VideoPlatform.MessageService/"]
COPY ["VideoPlatform.SchedulerService/VideoPlatform.SchedulerService.csproj", "VideoPlatform.SchedulerService/"]
RUN dotnet restore "VideoPlatform.Api/VideoPlatform.Api.csproj"
COPY . .
WORKDIR "/src/VideoPlatform.Api"
RUN dotnet build "VideoPlatform.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "VideoPlatform.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "VideoPlatform.Api.dll"]
