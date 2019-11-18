FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

COPY *.sln .
COPY Token.DataObject/*.csproj ./Token.DataObject/
COPY Token.Domain/*.csproj ./Token.Domain/
COPY Token.Infrastructure/*.csproj ./Token.Infrastructure/
COPY Token.Service.Business/*.csproj ./Token.Service.Business/
COPY Token.Service.Proxy/*.csproj ./Token.Service.Proxy/
RUN dotnet restore

COPY Token.DataObject/. ./Token.DataObject/
COPY Token.Domain/. ./Token.Domain/
COPY Token.Infrastructure/. ./Token.Infrastructure/
COPY Token.Service.Business/. ./Token.Service.Business/
COPY Token.Service.Proxy/. ./Token.Service.Proxy/
WORKDIR /app/Token.Service.Proxy
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/Token.Service.Proxy/out ./
ENTRYPOINT ["dotnet", "Token.Service.Proxy.dll"]