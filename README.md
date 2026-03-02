# Conectar 🎬

**Conectar** es una aplicación de escritorio desarrollada en **Windows Presentation Foundation (WPF)** con **C#** y **.NET 8.0**. Su objetivo principal es ofrecer un catálogo interactivo de películas donde los usuarios pueden explorar títulos, interactuar con reseñas de películas y gestionar su propio perfil.

## 🚀 Características Principales

- **Autenticación de Usuarios:** Registro e inicio de sesión seguro para gestionar perfiles individuales.
- **Catálogo de Películas:** Exploración de una amplia biblioteca de películas organizadas y mostradas con sus portadas oficiales.
- **Detalle de Películas y Reseñas:** Información detallada sobre la sinopsis, elenco y una sección dedicada a leer o escribir reseñas.
- **Búsqueda Avanzada:** Funcionalidad para buscar películas específicas rápidamente y ver los resultados filtrados.
- **Gestión de Perfil y Ajustes:** Área personal para ver datos de cuenta y configurar la experiencia dentro de la app.
- **Experiencia Inmersiva:** Reproducción de música de fondo (vía `AudioManager` integrado) que acompaña la exploración del catálogo.

## 🛠️ Tecnologías y Patrones

El proyecto hace uso intensivo del patrón de diseño **MVVM (Model-View-ViewModel)**, garantizando un código limpio, modular y testeable.

- **Framework de UI:** WPF (XAML)
- **Lenguaje / Plataforma:** C# con .NET 8.0
- **Base de Datos:** MySQL (se utiliza la librería oficial `MySql.Data` v9.6.0 para la comunicación).
- **Controladores Multimedia:** Uso de recursos incrustados y gestión de audio local.

## 📂 Estructura del Código (Arquitectura MVVM)

```text
Conectar/
│
├── Assets/         # Recursos pesados (Ej: música de fondo y portadas de películas como Dune, Interstellar, etc.)
├── Helpers/        # Utilidades transversales (AudioManager, conversores para UI)
├── Imagenes/       # Iconos, logos y fondos del layout principal
└── MVVM/           # Núcleo de la aplicación
    ├── Data/       # Capa de acceso a datos (DBConection y AccesoDatos para MySQL)
    ├── Model/      # Entidades de dominio (Pelicula, UsuarioModel)
    ├── View/       # Interfaces gráficas XAML (AjustesPage, CatalogoPeliculas, LogInPage, etc.)
    └── ViewModel/  # Lógica de estados y comandos para enlazar con las Vistas (Bindable/RelayCommands)
```

## ⚙️ Requisitos de Desarrollo

- **Visual Studio 2022** (recomendado para trabajar cómodamente con XAML en .NET 8).
- **.NET 8.0 SDK** o superior instalado en el equipo.
- **Servidor MySQL** ejecutándose local o remotamente con el esquema de base de datos correspondiente (usuarios, películas, reseñas).

## 🚀 Uso y Ejecución

1. Abre el archivo de solución `Conectar.sln` con Visual Studio.
2. Comprueba y actualiza la cadena de conexión de MySQL dentro de las clases de acceso a datos ubicadas en `MVVM/Data/`.
3. Restaura los paquetes NuGet (asegúrate de que `MySql.Data` se instala correctamente).
4. Compila y ejecuta el proyecto pulsando `F5`.
5. Inicia sesión (o regístrate si es tu primera vez) y empieza a disfrutar del catálogo multimedia.
