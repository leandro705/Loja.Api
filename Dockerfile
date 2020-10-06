### Estágio 1 - Obter o source e gerar o Build ###
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS dotnet-builder
    
WORKDIR /app
COPY . /app

RUN dotnet restore "src/Loja.Api/Loja.Api.csproj" 
RUN dotnet build "src/Loja.Api/Loja.Api.csproj" -c Release -o /app/publish
RUN dotnet publish "src/Loja.Api/Loja.Api.csproj" -c Release -o /app/publish

### Estágio 2 - Subir a aplicação através dos binários ###
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
ENV ASPNETCORE_ENVIRONMENT=Prodution \
    LC_ALL=pt_BR.UTF-8 \
    LANG=pt_BR.UTF-8 \
    ASPNETCORE_URLS=http://*:80 \
	DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
	TZ='America/Campo_Grande'

    RUN apk update && \
		apk upgrade && \
		apk add tzdata &&\
		cp /usr/share/zoneinfo/America/Campo_Grande /etc/localtime && \
		echo "America/Campo_Grande" >  /etc/timezone && \
		apk add --no-cache icu-libs
    
WORKDIR /app
EXPOSE 80
COPY --from=dotnet-builder /app/publish .
ENTRYPOINT ["dotnet", "Loja.Api.dll"]