FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["VideoPlatform.AuthenticationService/VideoPlatform.AuthenticationService.csproj", "VideoPlatform.AuthenticationService/"]
RUN dotnet restore "VideoPlatform.AuthenticationService/VideoPlatform.AuthenticationService.csproj"
COPY . .
WORKDIR "/src/VideoPlatform.AuthenticationService"
RUN dotnet build "VideoPlatform.AuthenticationService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "VideoPlatform.AuthenticationService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "VideoPlatform.AuthenticationService.dll"]