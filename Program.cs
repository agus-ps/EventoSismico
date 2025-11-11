using System;
using System.Collections.Generic;
using EventoSismicoApp.Controller;
using EventoSismicoApp.Datos; // <-- 1. IMPORTAR EL DBCONTEXT
using EventoSismicoApp.Entities;
using EventoSismicoApp.Forms;
using System.Linq; // <-- 2. IMPORTAR LINQ
using Microsoft.EntityFrameworkCore; // <-- 3. IMPORTAR EF CORE

namespace EventoSismicoApp
{
    static class Program
    {
        // --- INICIO DE CAMBIOS ---
        // 4. BORRAMOS TODAS LAS LISTAS ESTÁTICAS
        // public static List<EventoSismico> EventosSismicos = ...
        // public static List<Estado> Estados = ...
        // public static List<Usuario> Usuarios = ...
        // public static List<Empleado> Empleados { get; set; } = ...

        // 5. CREAMOS EL CONTEXTO DE DB GLOBAL
        public static SismosDbContext Db { get; private set; } = new SismosDbContext();

        // 6. Mantenemos estos
        public static Sesion SesionActual;
        public static PantallaNuevoEventoSismico PantallaPrincipal { get; private set; }
        public static ManejadorNuevoEventoSismico Manejador { get; private set; }
        // --- FIN DE CAMBIOS ---

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // --- INICIO DE CAMBIOS ---
            // 7. Nos aseguramos que la DB esté creada
            Db.Database.EnsureCreated(); // Esto es más simple que Update-Database para SQLite

            // 8. Renombramos y llamamos al método de "siembra"
            CargarDatosSiVacia();
            // --- FIN DE CAMBIOS ---

            ApplicationConfiguration.Initialize();
            PantallaPrincipal = new PantallaNuevoEventoSismico();
            Manejador = new ManejadorNuevoEventoSismico();
            Application.Run(PantallaPrincipal);
        }

        // --- INICIO DE CAMBIOS ---
        // 9. Renombramos "InicializarDatosPrueba" y le agregamos lógica
        static void CargarDatosSiVacia()
        {
            // 10. Si ya hay estados en la DB, no hacemos nada
            if (Db.Estados.Any())
            {
                // 11. Solo cargamos la sesión actual
                SesionActual = Db.Sesiones
                                .Include(s => s.Usuario)
                                .ThenInclude(u => u.Empleado)
                                .FirstOrDefault(); // Asumimos que solo hay 1
                return;
            }

            // 12. Si la DB está vacía, movemos toda tu lógica de "InicializarDatosPrueba"
            //     PERO cambiando "Program.Estados.Add" por "Db.Estados.Add"

            // 1. Crear estados
            var pendienteRevisar = new Estado("EventoSismografico", "PendienteRevisar");
            var bloqueadoEnRevision = new Estado("EventoSismografico", "BloqueadoEnRevision");
            var rechazado = new Estado("EventoSismografico", "Rechazado");
            Db.Estados.AddRange(new[] { pendienteRevisar, bloqueadoEnRevision, rechazado });

            // 2. Crear tipos de dato
            var tipoMagnitud = new TipoDato("Magnitud", 2.5, "Escala Richter");
            var tipoAceleracion = new TipoDato("Aceleración", 50, "cm/s²");
            Db.TiposDeDato.AddRange(new[] { tipoMagnitud, tipoAceleracion });

            // 3. Crear estaciones sismológicas
            var estacionCentral = new EstacionSismologica("EST001", "Estación Central", -34.6037, -58.3816);
            var estacionNorte = new EstacionSismologica("EST002", "Estación Norte", -24.7859, -65.4116);
            Db.EstacionesSismologicas.AddRange(new[] { estacionCentral, estacionNorte });

            // 4. Crear sismógrafos
            var sismografo1 = new Sismografo("SIS001", "SN12345", DateTime.Now.AddYears(-1), estacionCentral);
            var sismografo2 = new Sismografo("SIS002", "SN67890", DateTime.Now.AddYears(-2), estacionNorte);
            Db.Sismografos.AddRange(new[] { sismografo1, sismografo2 });

            // 5. Crear clasificaciones
            var alcanceLocal = new AlcanceSismo("Local", "Sismo de alcance local");
            var origenTectonico = new OrigenDeGeneracion("Tectónico", "Originado por movimiento de placas");
            var clasifSuperficial = new ClasificacionSismo("Superficial", 0, 70);
            Db.AlcancesSismos.Add(alcanceLocal);
            Db.OrigenesDeGeneracion.Add(origenTectonico);
            Db.ClasificacionesSismos.Add(clasifSuperficial);

            // 6. Crear usuario y empleado
            var empleadoAnalista = new Empleado("Juan", "Gomez", "jgomez@institucion.com", "123456789");
            var usuarioAnalista = new Usuario("jgomez", "pass123", empleadoAnalista);
            // (EF Core maneja la relación bidireccional)
            Db.Usuarios.Add(usuarioAnalista);
            Db.Empleados.Add(empleadoAnalista);

            // 7. Crear sesión actual
            SesionActual = new Sesion(DateTime.Now.AddHours(-1), null, usuarioAnalista);
            Db.Sesiones.Add(SesionActual);

            // 8. Crear eventos sísmicos (los mismos que tenías)
            var evento1 = new EventoSismico(
                DateTime.Now.AddHours(-6), -34.6037, -58.3816, -34.7037, -58.4816, 3.5,
                true, pendienteRevisar, alcanceLocal, origenTectonico, clasifSuperficial
            );
            var cambioEstado1 = new CambioEstado(pendienteRevisar, DateTime.Now.AddHours(-5), null, empleadoAnalista);
            evento1.CambiosEstado.Add(cambioEstado1);

            var serie1 = new SerieTemporal(false, DateTime.Now.AddHours(-6), DateTime.Now.AddHours(-5), 0.1, sismografo1);
            // (Tu lógica de Muestras y Detalles... EF Core los guardará en cascada)
            for (int i = 0; i < 10; i++)
            {
                var muestra = new MuestraSismica(DateTime.Now.AddHours(-6).AddSeconds(i));
                muestra.Detalles.Add(new DetalleMuestraSismica(3.5 + (i * 0.1), tipoMagnitud));
                muestra.Detalles.Add(new DetalleMuestraSismica(50 + (i * 2), tipoAceleracion));
                serie1.Muestras.Add(muestra);
            }
            evento1.SeriesTemporales.Add(serie1);


            var evento2 = new EventoSismico(
                DateTime.Now.AddHours(-3), -24.7859, -65.4116, -24.8859, -65.5116, 4.2,
                true, pendienteRevisar, alcanceLocal, origenTectonico, clasifSuperficial
            );
            evento2.CambiosEstado.Add(new CambioEstado(pendienteRevisar, DateTime.Now.AddHours(-2.5), null, empleadoAnalista));
            var serie2 = new SerieTemporal(true, DateTime.Now.AddHours(-3), DateTime.Now.AddHours(-2.5), 0.2, sismografo2);
            // (Lógica de Muestras y Detalles para evento2...)
            for (int i = 0; i < 8; i++)
            {
                var muestra = new MuestraSismica(DateTime.Now.AddHours(-3).AddSeconds(i * 2));
                muestra.Detalles.Add(new DetalleMuestraSismica(4.0 + (i * 0.15), tipoMagnitud));
                muestra.Detalles.Add(new DetalleMuestraSismica(52 + (i * 1.5), tipoAceleracion));
                serie2.Muestras.Add(muestra);
            }
            evento2.SeriesTemporales.Add(serie2);

            // ... (Tus eventos 3 y 4)
            // ... (Asegúrate de agregarlos con Db.EventosSismicos.Add(...))

            Db.EventosSismicos.AddRange(new[] { evento1, evento2 /*, evento3, evento4 */ });

            // 13. ¡EL PASO MÁS IMPORTANTE! Guardar todo en la DB
            Db.SaveChanges();
        }
        // --- FIN DE CAMBIOS ---
    }
}