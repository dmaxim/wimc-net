FROM mcr.microsoft.com/dotnet/aspnet:5.0.8-focal-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM  mcr.microsoft.com/dotnet/sdk:5.0 AS build 
WORKDIR /src
COPY Mx.EntityFramework/Mx.EntityFramework.csproj Mx.EntityFramework/
COPY Mx.EntityFramework.Contracts/Mx.EntityFramework.Contracts.csproj Mx.EntityFramework.Contracts/
COPY Mx.Library/Mx.Library.csproj Mx.Library/
COPY Wimc.Business/Wimc.Business.csproj Wimc.Business/
COPY Wimc.Data/Wimc.Data.csproj Wimc.Data/
COPY Wimc.Domain/Wimc.Domain.csproj Wimc.Domain/
COPY Wimc.Infrastructure/Wimc.Infrastructure.csproj Wimc.Infrastructure/
COPY Wimc/Wimc.csproj Wimc/


RUN dotnet restore Wimc/Wimc.csproj
COPY . .
WORKDIR /src/Wimc

RUN dotnet build Wimc.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Wimc.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Wimc.dll"]


