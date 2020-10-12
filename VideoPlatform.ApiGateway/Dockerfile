FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["VideoPlatform.ApiGateway/VideoPlatform.ApiGateway.csproj", "VideoPlatform.ApiGateway/"]
RUN dotnet restore "VideoPlatform.ApiGateway/VideoPlatform.ApiGateway.csproj"
COPY . .
WORKDIR "/src/VideoPlatform.ApiGateway"
RUN dotnet build "VideoPlatform.ApiGateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "VideoPlatform.ApiGateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "VideoPlatform.ApiGateway.dll"]