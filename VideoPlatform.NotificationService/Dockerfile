FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["VideoPlatform.NotificationService/VideoPlatform.NotificationService.csproj", "VideoPlatform.NotificationService/"]
RUN dotnet restore "VideoPlatform.NotificationService/VideoPlatform.NotificationService.csproj"
COPY . .
WORKDIR "/src/VideoPlatform.NotificationService"
RUN dotnet build "VideoPlatform.NotificationService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "VideoPlatform.NotificationService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "VideoPlatform.NotificationService.dll"]