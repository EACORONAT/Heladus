#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Define la imagen base para la etapa "base". Esta imagen contiene ASP.NET 6.0.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

# Expone los puertos 80 y 443 para que la aplicación pueda escuchar solicitudes HTTP y HTTPS.
EXPOSE 80
EXPOSE 443

# Define la imagen base para la etapa "build". Esta imagen contiene el SDK de .NET 6.0.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia el archivo de proyecto de la aplicación a la imagen y restaura las dependencias.
COPY ["Heladus/Heladus.csproj", "Heladus/"]
RUN dotnet restore "Heladus/Heladus.csproj"

# Copia todo el contenido del directorio actual al directorio de trabajo en la imagen.
COPY . .

# Cambia el directorio de trabajo a la ubicación del proyecto de la aplicación y compila la aplicación en modo Release.
WORKDIR "/src/Heladus"
RUN dotnet build "Heladus.csproj" -c Release -o /app/build

# Define la imagen "publish" como una capa basada en la etapa "build". Publica la aplicación en modo Release.
FROM build AS publish
RUN dotnet publish "Heladus.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Define la imagen final basada en la etapa "base". Copia la aplicación publicada desde la etapa "publish" a esta imagen.
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Establece el punto de entrada para la aplicación cuando se ejecute el contenedor.
ENTRYPOINT ["dotnet", "Heladus.dll"]