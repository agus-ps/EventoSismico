using System;
using System.Collections.Generic;
using EventoSismicoApp.Controller;
using EventoSismicoApp.Entities;
using EventoSismicoApp.Forms;

namespace EventoSismicoApp
{
    static class Program
    {
        // Variables globales para simular "base de datos"
        public static List<EventoSismico> EventosSismicos = new List<EventoSismico>();
        public static List<Estado> Estados = new List<Estado>();
        public static List<Usuario> Usuarios = new List<Usuario>();
        public static Sesion SesionActual;
        public static List<Empleado> Empleados { get; set; } = new List<Empleado>();
        public static PantallaNuevoEventoSismico PantallaPrincipal { get; private set; }
        public static ManejadorNuevoEventoSismico Manejador { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicializar datos de prueba
            InicializarDatosPrueba();

            ApplicationConfiguration.Initialize();
            // Crear y mostrar pantalla principal
            PantallaPrincipal = new PantallaNuevoEventoSismico();
            Manejador = new ManejadorNuevoEventoSismico();
            Application.Run(PantallaPrincipal);
        }

        static void InicializarDatosPrueba()
        {
            // 1. Crear estados (usando constructor)
            var pendienteRevisar = new Estado("EventoSismografico", "PendienteRevisar");
            var bloqueadoEnRevision = new Estado("EventoSismografico", "BloqueadoEnRevision");
            var rechazado = new Estado("EventoSismografico", "Rechazado");

            Program.Estados.AddRange(new[] { pendienteRevisar, bloqueadoEnRevision, rechazado });

            // 2. Crear tipos de dato
            var tipoMagnitud = new TipoDato("Magnitud", 2.5, "Escala Richter");
            var tipoAceleracion = new TipoDato("Aceleración", 50, "cm/s²");

            // 3. Crear estaciones sismológicas
            var estacionCentral = new EstacionSismologica("EST001", "Estación Central", -34.6037, -58.3816);
            var estacionNorte = new EstacionSismologica("EST002", "Estación Norte", -24.7859, -65.4116);

            // 4. Crear sismógrafos
            var sismografo1 = new Sismografo("SIS001", "SN12345", DateTime.Now.AddYears(-1), estacionCentral);
            var sismografo2 = new Sismografo("SIS002", "SN67890", DateTime.Now.AddYears(-2), estacionNorte);

            // 5. Crear clasificaciones
            var alcanceLocal = new AlcanceSismo("Local", "Sismo de alcance local");
            var origenTectonico = new OrigenDeGeneracion("Tectónico", "Originado por movimiento de placas");
            var clasifSuperficial = new ClasificacionSismo("Superficial", 0, 70);

            // 6. Crear usuario y empleado
            var empleadoAnalista = new Empleado("Juan", "Gomez", "jgomez@institucion.com", "123456789");
            var usuarioAnalista = new Usuario("jgomez", "pass123", empleadoAnalista);
            empleadoAnalista.Usuario = usuarioAnalista; // Relación bidireccional

            Program.Usuarios.Add(usuarioAnalista);

            Empleados.Add(empleadoAnalista);

            // 7. Crear sesión actual
            Program.SesionActual = new Sesion(DateTime.Now.AddHours(-1), null, usuarioAnalista);

            // 8. Crear eventos sísmicos

            // --- INICIO DE CORRECCIÓN ---
            // Agregamos el bool 'esAutoDetectado' al constructor
            var evento1 = new EventoSismico(
                fechaHoraOcurrencia: DateTime.Now.AddHours(-6),
                latEpicentro: -34.6037,
                longEpicentro: -58.3816,
                latHipocentro: -34.7037,
                longHipocentro: -58.4816,
                magnitud: 3.5,
                esAutoDetectado: true, // <-- CAMBIO 1
                estado: pendienteRevisar,
                alcance: alcanceLocal,
                origen: origenTectonico,
                clasificacion: clasifSuperficial
            );
            // --- FIN DE CORRECCIÓN ---

            // --- INICIO DE CORRECCIÓN ---
            // Agregamos el 'empleadoAnalista' al constructor de CambioEstado
            var cambioEstado1 = new CambioEstado(pendienteRevisar, DateTime.Now.AddHours(-5), null, empleadoAnalista); // <-- CAMBIO 2
            // --- FIN DE CORRECCIÓN ---
            evento1.CambiosEstado.Add(cambioEstado1);

            // Crear serie temporal para evento1
            var serie1 = new SerieTemporal(
                condicionAlarma: false,
                fechaHoraInicioRegistroMuestras: DateTime.Now.AddHours(-6),
                fechaHoraRegistro: DateTime.Now.AddHours(-5),
                frecuenciaMuestreo: 0.1,
                sismografo: sismografo1
            );

            // Agregar muestras a la serie1
            for (int i = 0; i < 10; i++)
            {
                var muestra = new MuestraSismica(DateTime.Now.AddHours(-6).AddSeconds(i));

                muestra.Detalles.Add(new DetalleMuestraSismica(3.5 + (i * 0.1), tipoMagnitud));
                muestra.Detalles.Add(new DetalleMuestraSismica(50 + (i * 2), tipoAceleracion));

                serie1.Muestras.Add(muestra);
            }

            evento1.SeriesTemporales.Add(serie1);

            // Evento 2
            // --- INICIO DE CORRECCIÓN ---
            var evento2 = new EventoSismico(
                DateTime.Now.AddHours(-3),
                -24.7859, -65.4116, -24.8859, -65.5116,
                4.2,
                true, // <-- CAMBIO 3
                pendienteRevisar,
                alcanceLocal, origenTectonico, clasifSuperficial
            );
            // --- FIN DE CORRECCIÓN ---

            // --- INICIO DE CORRECCIÓN ---
            evento2.CambiosEstado.Add(new CambioEstado(pendienteRevisar, DateTime.Now.AddHours(-2.5), null, empleadoAnalista)); // <-- CAMBIO 4
            // --- FIN DE CORRECCIÓN ---

            // Crear nueva serie para evento2
            var serie2 = new SerieTemporal(
                condicionAlarma: true,
                fechaHoraInicioRegistroMuestras: DateTime.Now.AddHours(-3),
                fechaHoraRegistro: DateTime.Now.AddHours(-2.5),
                frecuenciaMuestreo: 0.2,
                sismografo: sismografo2
            );

            // Agregar muestras a la serie2
            for (int i = 0; i < 8; i++)
            {
                var muestra = new MuestraSismica(DateTime.Now.AddHours(-3).AddSeconds(i * 2));

                muestra.Detalles.Add(new DetalleMuestraSismica(4.0 + (i * 0.15), tipoMagnitud));
                muestra.Detalles.Add(new DetalleMuestraSismica(52 + (i * 1.5), tipoAceleracion));

                serie2.Muestras.Add(muestra);
            }

            evento2.SeriesTemporales.Add(serie2);


            // Evento 3 (ej. uno que no es autodetectado y ya está rechazado)
            // --- INICIO DE CORRECCIÓN ---
            var evento3 = new EventoSismico(
                DateTime.Now.AddHours(-10),
                -32.8895, -68.8458,
                -32.9895, -68.9458,
                5.1,
                false, // <-- CAMBIO 5
                rechazado,
                alcanceLocal,
                origenTectonico,
                clasifSuperficial
            );
            // --- FIN DE CORRECCIÓN ---

            // --- INICIO DE CORRECCIÓN ---
            evento3.CambiosEstado.Add(new CambioEstado(rechazado, DateTime.Now.AddHours(-9), DateTime.Now.AddHours(-8), empleadoAnalista)); // <-- CAMBIO 6
            // --- FIN DE CORRECCIÓN ---
            Program.EventosSismicos.Add(evento3);


            // Evento 4 (ej. uno no autodetectado que quedó bloqueado)
            // --- INICIO DE CORRECCIÓN ---
            var evento4 = new EventoSismico(
                DateTime.Now.AddHours(-8),
                -31.4201, -64.1888,
                -31.5201, -64.2888,
                2.9,
                false, // <-- CAMBIO 7
                bloqueadoEnRevision,
                alcanceLocal,
                origenTectonico,
                clasifSuperficial
            );
            // --- FIN DE CORRECCIÓN ---

            // --- INICIO DE CORRECCIÓN ---
            evento4.CambiosEstado.Add(new CambioEstado(bloqueadoEnRevision, DateTime.Now.AddHours(-7), null, empleadoAnalista)); // <-- CAMBIO 8
            // --- FIN DE CORRECCIÓN ---


            Program.EventosSismicos.AddRange(new[] { evento1, evento2, evento3, evento4 });
        }
    }
}