FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8082

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SocialMediaService/SocialMediaService.csproj", "SocialMediaService/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["DatabaseService/DatabaseService.csproj", "DatabaseService/"]
COPY ["GoogleDriveService/GoogleDriveService.csproj", "GoogleDriveService/"]
COPY ["MailService/MailService.csproj", "MailService/"]
RUN dotnet restore "SocialMediaService/SocialMediaService.csproj"
COPY . .
WORKDIR "/src/SocialMediaService"
RUN dotnet build "SocialMediaService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SocialMediaService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialMediaService.dll"]
