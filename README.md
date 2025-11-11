# TPI-SoftwareDesign

## English Version

### Seismic Event Management System

A Windows Forms application developed in C# .NET 8.0 for managing and reviewing seismic events detected by monitoring stations. The system implements the **Iterator Pattern** and uses **Entity Framework Core** with SQLite for data persistence.

#### 🚀 Features

- **Automatic Event Detection**: The system automatically loads pending seismic events for manual review
- **Event Review Workflow**: Complete workflow for reviewing, accepting, rejecting, or requesting expert revision
- **Dynamic Event Generation**: When all events are processed, the system automatically generates new realistic seismic events
- **Data Persistence**: All events and changes are stored in SQLite database
- **Real-time Data Visualization**: Displays seismic data including magnitude, acceleration, and temporal series
- **State Management**: Tracks event states (Pending Review, Blocked in Review, Rejected)
- **Iterator Pattern Implementation**: Efficient navigation through temporal series and seismic samples

#### 🏗️ Architecture

The application follows a **layered architecture** with:

- **Presentation Layer**: Windows Forms UI (`Forms/`)
- **Business Logic Layer**: Controllers and handlers (`Controller/`)
- **Data Layer**: Entity Framework entities and DbContext (`Entities/`, `Datos/`)
- **Design Patterns**: Iterator pattern implementation (`Iterador/`)

#### 🗄️ Database Schema

The system manages the following entities:
- **EventoSismico**: Main seismic event with location, magnitude, and temporal data
- **SerieTemporal**: Time series data from seismographs
- **MuestraSismica**: Individual seismic samples with measurements
- **EstacionSismologica**: Seismic monitoring stations
- **Sismografo**: Seismograph devices
- **Estado**: Event state management
- **Empleado/Usuario**: User management for event processing

#### 🔧 Technologies Used

- **Framework**: .NET 8.0 Windows Forms
- **Database**: SQLite with Entity Framework Core 9.0.10
- **ORM**: Entity Framework Core
- **Design Pattern**: Iterator Pattern
- **Language**: C# 12.0

#### 📊 Key Functionalities

1. **Event Loading**: Automatically loads autodetected seismic events from database
2. **Event Selection**: Users can select events from a grid for detailed review
3. **Event Details**: Displays comprehensive event information including:
   - Event scope (Local, Regional, etc.)
   - Generation origin (Tectonic, Volcanic, etc.)
   - Classification (Superficial, Intermediate, Deep)
   - Temporal series data with measurements
4. **Event Actions**:
   - **Confirm Event**: Shows "Functionality under development" message
   - **Request Expert Review**: Shows "Functionality under development" message  
   - **Reject Event**: Saves rejection to database and closes application
   - **Cancel Review**: Releases event lock and returns to pending state
5. **Automatic Data Generation**: Creates new realistic events when queue is empty
6. **Data Persistence**: All changes automatically saved to SQLite database

#### 🚀 Getting Started

##### Prerequisites
- .NET 8.0 Runtime or SDK
- Windows OS (Windows Forms dependency)

##### Installation & Running
```bash
# Clone the repository
git clone https://github.com/EliasKarimRaueh/TPI-SoftwareDesign.git
cd TPI-SoftwareDesign

# Restore dependencies
dotnet restore

# Build the application
dotnet build

# Run the application
dotnet run --project EventoSismicoApp.csproj
```

##### First Run
On first execution, the application will:
1. Create the SQLite database (`sismos.db`)
2. Initialize with sample data (stations, seismographs, events)
3. Display the main interface with available events

#### 📱 User Interface

The main interface includes:
- **Event Grid**: List of pending seismic events with time, location, magnitude, and status
- **Event Details Panel**: Shows selected event information
- **Data Series Grid**: Displays temporal series data with measurements
- **Action Buttons**: Confirm, Reject, Request Review, Cancel options
- **Map Integration**: Placeholder for seismic map visualization

#### 🔄 Workflow

1. **Start Application** → Load pending events
2. **Select Event** → View detailed information and lock for review
3. **Review Data** → Analyze seismic measurements and temporal series
4. **Make Decision**:
   - Confirm → Mark as confirmed (under development)
   - Reject → Save rejection and close application
   - Request Review → Forward to expert (under development)
   - Cancel → Release lock and return to pending

#### 🏛️ Design Patterns

##### Iterator Pattern Implementation
The system implements the Iterator pattern for efficient navigation through:
- **Temporal Series**: Navigate through time-based seismic data
- **Seismic Samples**: Iterate through individual measurements
- **Event Collections**: Process multiple events efficiently

**Key Classes**:
- `IIterador`: Iterator interface
- `IAgregado`: Aggregate interface  
- `IteradorDatosSeriesTemporales`: Temporal series iterator
- `IteradorMuestraSismica`: Seismic sample iterator

#### 📝 Recent Improvements

- ✅ **Database Integration**: Migrated from static lists to Entity Framework with SQLite
- ✅ **Automatic Event Generation**: Dynamic creation of new events when queue is empty
- ✅ **State Management**: Proper event state tracking and persistence
- ✅ **Error Handling**: Fixed null reference exceptions and improved data loading
- ✅ **UI Messages**: Added "under development" messages for incomplete features
- ✅ **Application Lifecycle**: Proper application closure after event rejection
- ✅ **Data Relationships**: Complete loading of related entities for proper detail display

#### 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

#### 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## Versión en Español

### Sistema de Gestión de Eventos Sísmicos

Una aplicación Windows Forms desarrollada en C# .NET 8.0 para gestionar y revisar eventos sísmicos detectados por estaciones de monitoreo. El sistema implementa el **Patrón Iterator** y utiliza **Entity Framework Core** con SQLite para la persistencia de datos.

#### 🚀 Características

- **Detección Automática de Eventos**: El sistema carga automáticamente eventos sísmicos pendientes para revisión manual
- **Flujo de Revisión de Eventos**: Flujo completo para revisar, aceptar, rechazar o solicitar revisión de expertos
- **Generación Dinámica de Eventos**: Cuando todos los eventos son procesados, el sistema genera automáticamente nuevos eventos sísmicos realistas
- **Persistencia de Datos**: Todos los eventos y cambios se almacenan en base de datos SQLite
- **Visualización de Datos en Tiempo Real**: Muestra datos sísmicos incluyendo magnitud, aceleración y series temporales
- **Gestión de Estados**: Rastrea estados de eventos (Pendiente Revisión, Bloqueado en Revisión, Rechazado)
- **Implementación del Patrón Iterator**: Navegación eficiente a través de series temporales y muestras sísmicas

#### 🏗️ Arquitectura

La aplicación sigue una **arquitectura en capas** con:

- **Capa de Presentación**: Interfaz Windows Forms (`Forms/`)
- **Capa de Lógica de Negocio**: Controladores y manejadores (`Controller/`)
- **Capa de Datos**: Entidades Entity Framework y DbContext (`Entities/`, `Datos/`)
- **Patrones de Diseño**: Implementación del patrón Iterator (`Iterador/`)

#### 🗄️ Esquema de Base de Datos

El sistema gestiona las siguientes entidades:
- **EventoSismico**: Evento sísmico principal con ubicación, magnitud y datos temporales
- **SerieTemporal**: Datos de series temporales de sismógrafos
- **MuestraSismica**: Muestras sísmicas individuales con mediciones
- **EstacionSismologica**: Estaciones de monitoreo sísmico
- **Sismografo**: Dispositivos sismógrafos
- **Estado**: Gestión de estados de eventos
- **Empleado/Usuario**: Gestión de usuarios para procesamiento de eventos

#### 🔧 Tecnologías Utilizadas

- **Framework**: .NET 8.0 Windows Forms
- **Base de Datos**: SQLite con Entity Framework Core 9.0.10
- **ORM**: Entity Framework Core
- **Patrón de Diseño**: Patrón Iterator
- **Lenguaje**: C# 12.0

#### 📊 Funcionalidades Clave

1. **Carga de Eventos**: Carga automáticamente eventos sísmicos autodetectados desde la base de datos
2. **Selección de Eventos**: Los usuarios pueden seleccionar eventos de una grilla para revisión detallada
3. **Detalles de Eventos**: Muestra información completa del evento incluyendo:
   - Alcance del evento (Local, Regional, etc.)
   - Origen de generación (Tectónico, Volcánico, etc.)
   - Clasificación (Superficial, Intermedio, Profundo)
   - Datos de series temporales con mediciones
4. **Acciones de Eventos**:
   - **Confirmar Evento**: Muestra mensaje "Funcionalidad en desarrollo"
   - **Solicitar Revisión de Experto**: Muestra mensaje "Funcionalidad en desarrollo"
   - **Rechazar Evento**: Guarda rechazo en base de datos y cierra la aplicación
   - **Cancelar Revisión**: Libera el bloqueo del evento y vuelve a estado pendiente
5. **Generación Automática de Datos**: Crea nuevos eventos realistas cuando la cola está vacía
6. **Persistencia de Datos**: Todos los cambios se guardan automáticamente en base de datos SQLite

#### 🚀 Comenzar

##### Prerequisitos
- .NET 8.0 Runtime o SDK
- Sistema Operativo Windows (dependencia de Windows Forms)

##### Instalación y Ejecución
```bash
# Clonar el repositorio
git clone https://github.com/EliasKarimRaueh/TPI-SoftwareDesign.git
cd TPI-SoftwareDesign

# Restaurar dependencias
dotnet restore

# Compilar la aplicación
dotnet build

# Ejecutar la aplicación
dotnet run --project EventoSismicoApp.csproj
```

##### Primera Ejecución
En la primera ejecución, la aplicación:
1. Creará la base de datos SQLite (`sismos.db`)
2. Inicializará con datos de ejemplo (estaciones, sismógrafos, eventos)
3. Mostrará la interfaz principal con eventos disponibles

#### 📱 Interfaz de Usuario

La interfaz principal incluye:
- **Grilla de Eventos**: Lista de eventos sísmicos pendientes con hora, ubicación, magnitud y estado
- **Panel de Detalles de Evento**: Muestra información del evento seleccionado
- **Grilla de Series de Datos**: Muestra datos de series temporales con mediciones
- **Botones de Acción**: Opciones para Confirmar, Rechazar, Solicitar Revisión, Cancelar
- **Integración de Mapa**: Marcador de posición para visualización de mapa sísmico

#### 🔄 Flujo de Trabajo

1. **Iniciar Aplicación** → Cargar eventos pendientes
2. **Seleccionar Evento** → Ver información detallada y bloquear para revisión
3. **Revisar Datos** → Analizar mediciones sísmicas y series temporales
4. **Tomar Decisión**:
   - Confirmar → Marcar como confirmado (en desarrollo)
   - Rechazar → Guardar rechazo y cerrar aplicación
   - Solicitar Revisión → Enviar a experto (en desarrollo)
   - Cancelar → Liberar bloqueo y volver a pendiente

#### 🏛️ Patrones de Diseño

##### Implementación del Patrón Iterator
El sistema implementa el patrón Iterator para navegación eficiente a través de:
- **Series Temporales**: Navegar a través de datos sísmicos basados en tiempo
- **Muestras Sísmicas**: Iterar a través de mediciones individuales
- **Colecciones de Eventos**: Procesar múltiples eventos eficientemente

**Clases Clave**:
- `IIterador`: Interfaz del iterador
- `IAgregado`: Interfaz del agregado
- `IteradorDatosSeriesTemporales`: Iterador de series temporales
- `IteradorMuestraSismica`: Iterador de muestras sísmicas

#### 📝 Mejoras Recientes

- ✅ **Integración de Base de Datos**: Migración de listas estáticas a Entity Framework con SQLite
- ✅ **Generación Automática de Eventos**: Creación dinámica de nuevos eventos cuando la cola está vacía
- ✅ **Gestión de Estados**: Seguimiento y persistencia adecuada de estados de eventos
- ✅ **Manejo de Errores**: Corrección de excepciones de referencia nula y mejora en la carga de datos
- ✅ **Mensajes de UI**: Agregados mensajes "en desarrollo" para características incompletas
- ✅ **Ciclo de Vida de Aplicación**: Cierre adecuado de aplicación después del rechazo de eventos
- ✅ **Relaciones de Datos**: Carga completa de entidades relacionadas para visualización adecuada de detalles

#### 🤝 Contribuir

1. Hacer fork del repositorio
2. Crear rama de característica (`git checkout -b feature/CaracteristicaIncreible`)
3. Hacer commit de cambios (`git commit -m 'Agregar CaracteristicaIncreible'`)
4. Push a la rama (`git push origin feature/CaracteristicaIncreible`)
5. Abrir un Pull Request

#### 📄 Licencia

Este proyecto está licenciado bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para detalles.
