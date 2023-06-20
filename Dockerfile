FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MailSender.Api/MailSender.Api.csproj", "MailSender.Api/MailSender.Api.csproj"]
RUN dotnet restore "MailSender.Api/MailSender.Api.csproj"
COPY ["MailSender.Api/.", "MailSender.Api/"]
WORKDIR "/src/MailSender.Api"
RUN dotnet build "MailSender.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MailSender.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MailSender.Api.dll"]