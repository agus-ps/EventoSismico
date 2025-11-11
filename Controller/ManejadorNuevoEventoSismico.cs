using EventoSismicoApp;
using EventoSismicoApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventoSismicoApp.Controller
{
    public class ManejadorNuevoEventoSismico
    {
        private List<EventoSismico> eventosAutodetectados;
        private EventoSismico eventoSismicoSeleccionado;
        

        private (string, string, string, List<string[]>) detalleEventoSismico;
        private Estado estadoBloqueadoEnRevision;
        private Usuario usuarioLogueado;
        private Empleado empleadoLogueado;
        private Estado estadoPendienteRevisar;
        public void registrarResultadoRevisionManual()
        {
            //eventosAutodetectados = Program.EventosSismicos;
            buscarAutodetectados();
            ordenarFechaHora();
            Program.PantallaPrincipal.presentarEventos(this.eventosAutodetectados);
        }

        private void buscarAutodetectados()
        {
            eventosAutodetectados = new List<EventoSismico>();

            // --- INICIO DE CAMBIOS ---
            // Reemplazamos el foreach sobre la lista estática

            // Antes:
            // foreach (var evento in Program.EventosSismicos)

            // Ahora: (Consultamos la DB)
            // 1. Cargamos todos los eventos que cumplan la condición
            //    Usamos .Include() para cargar el Estado y CambiosEstado, si no, evento.esPendienteRevisar() fallaría
            var eventosDesdeDb = Program.Db.EventosSismicos
                                        .Include(e => e.EstadoActual)
                                        .Include(e => e.CambiosEstado)
                                        .Include(e => e.Alcance)
                                        .Include(e => e.Origen)
                                        .Include(e => e.Clasificacion)
                                        .Include(e => e.SeriesTemporales)
                                            .ThenInclude(s => s.Sismografo)
                                            .ThenInclude(sm => sm.Estacion)
                                        .Include(e => e.SeriesTemporales)
                                            .ThenInclude(s => s.Muestras)
                                            .ThenInclude(m => m.Detalles)
                                            .ThenInclude(d => d.TipoDato)
                                        .ToList(); // Traemos a memoria

            // 2. Filtramos en memoria (ya que tus métodos es...() no son SQL)
            foreach (var evento in eventosDesdeDb)
            {
                if (evento.esPendienteRevisar() && evento.esAutoDetectado())
                // --- FIN DE CAMBIOS ---
                {
                    var hora = evento.getHoraOcurrencia();
                    var ubicacion = evento.getUbicacion();
                    var magnitud = evento.getMagnitud();

                    eventosAutodetectados.Add(evento);
                }
            }

            // Si no hay eventos autodetectados pendientes, generamos más
            if (eventosAutodetectados.Count == 0)
            {
                generarNuevosEventosAutodetectados();
                
                // Volver a buscar después de generar nuevos eventos
                var eventosActualizados = Program.Db.EventosSismicos
                                               .Include(e => e.EstadoActual)
                                               .Include(e => e.CambiosEstado)
                                               .Include(e => e.Alcance)
                                               .Include(e => e.Origen)
                                               .Include(e => e.Clasificacion)
                                               .Include(e => e.SeriesTemporales)
                                                   .ThenInclude(s => s.Sismografo)
                                                   .ThenInclude(sm => sm.Estacion)
                                               .Include(e => e.SeriesTemporales)
                                                   .ThenInclude(s => s.Muestras)
                                                   .ThenInclude(m => m.Detalles)
                                                   .ThenInclude(d => d.TipoDato)
                                               .ToList();
                
                foreach (var evento in eventosActualizados)
                {
                    if (evento.esPendienteRevisar() && evento.esAutoDetectado())
                    {
                        eventosAutodetectados.Add(evento);
                    }
                }
            }
        }

        private void ordenarFechaHora()
        {
            eventosAutodetectados = eventosAutodetectados
                .OrderBy(e => e.FechaHoraOcurrencia)
                .ToList();
        }


        // Paso 8: Tomar selección y buscar estado
        public void tomarSeleccionEventoSismico(int index)
        {
            if (index >= 0 && index < this.eventosAutodetectados.Count)
            {
                this.eventoSismicoSeleccionado = this.eventosAutodetectados[index];
                this.buscarEstadoBloqueadoEnRevision(); // Segundo loop
                this.buscarUsuarioLogueado(); // Paso 11

                string fecha = this.getFecha();
                string hora = this.getHora();
                DateTime fechaHoraActual = DateTime.ParseExact($"{fecha} {hora}", "dd/MM/yyyy HH:mm", null);

                //eventoSismicoSeleccionado.revisar(fechaHoraActual, this.estadoBloqueadoEnRevision); // Pasamos la fecha actual. TODO: Usar los metodos getFecha y getHora
                eventoSismicoSeleccionado.revisar(fechaHoraActual, this.estadoBloqueadoEnRevision, this.empleadoLogueado);

                this.buscarDetalleEventoSismico();
            }
        }

        // --- INICIO DE CAMBIO (Facu4) ---
        // 1. Creamos el nuevo método público
        public void tomarOpcionCancelar()
        {
            if (this.eventoSismicoSeleccionado != null && this.empleadoLogueado != null)
            {
                this.buscarPendienteRevisar();
                this.eventoSismicoSeleccionado.liberarBloqueo(
                    DateTime.Now,
                    this.estadoPendienteRevisar,
                    this.empleadoLogueado
                );

                // --- INICIO DE CAMBIOS ---
                // 3. Guardamos los cambios en la DB
                Program.Db.SaveChanges();
                // --- FIN DE CAMBIOS ---

                this.eventoSismicoSeleccionado = null;
            }
        }

        // 5. Creamos un método helper para buscar el estado "PendienteRevisar"
        private void buscarPendienteRevisar()
        {
            // --- INICIO DE CAMBIOS ---
            // Antes:
            // foreach (var estado in Program.Estados) { ... }

            // Ahora: (Consulta LINQ)
            this.estadoPendienteRevisar = Program.Db.Estados
                .FirstOrDefault(e => e.Ambito == "EventoSismografico" && e.NombreEstado == "PendienteRevisar");
            // --- FIN DE CAMBIOS ---
        }
        // --- FIN DE CAMBIO ---


        private void buscarEstadoBloqueadoEnRevision()
        {
            // --- INICIO DE CAMBIOS ---
            // Antes:
            // foreach (var estado in Program.Estados) { ... }

            // Ahora: (Consulta LINQ)
            this.estadoBloqueadoEnRevision = Program.Db.Estados
                .FirstOrDefault(e => e.Ambito == "EventoSismografico" && e.NombreEstado == "BloqueadoEnRevision");
            // --- FIN DE CAMBIOS ---
        }

        private void buscarUsuarioLogueado()
        {
            if (Program.SesionActual != null)
            {
                // --- INICIO DE CAMBIOS ---
                // El Program.SesionActual ya se cargó en Program.cs
                // con su Usuario y Empleado (gracias al .Include().ThenInclude())
                // Así que este método que corregimos... ¡ya funciona!
                this.usuarioLogueado = Program.SesionActual.Usuario;
                this.empleadoLogueado = this.usuarioLogueado.Empleado;
                // --- FIN DE CAMBIOS ---
            }
        }

        /*
        private void buscarUsuarioLogueado()
        {
            if (Program.SesionActual != null)
            {
                // --- INICIO DE CAMBIO ---
                // 3. Ya no recorremos Program.Empleados (eso estaba mal como dijo Facu)
                //    Obtenemos el Empleado DIRECTAMENTE del Usuario de la Sesión.
                this.usuarioLogueado = Program.SesionActual.Usuario;
                this.empleadoLogueado = this.usuarioLogueado.Empleado; // Asumiendo que Usuario tiene .Empleado
                // --- FIN DE CAMBIO ---
            }
        }
        */

        private void buscarDetalleEventoSismico()
        {
            if (this.eventoSismicoSeleccionado != null)
            {
                // Paso 17: Obtener detalles básicos (alcance, origen, clasificación)
                this.detalleEventoSismico = this.eventoSismicoSeleccionado.getDetalleEventoSismico();

                ordenarPorEstacionSismologica(ref this.detalleEventoSismico.Item4);
                Program.PantallaPrincipal.presentarDetalleEvento(this.detalleEventoSismico.Item1, this.detalleEventoSismico.Item2, this.detalleEventoSismico.Item3, this.detalleEventoSismico.Item4);

            }

            Program.PantallaPrincipal.habilitarOpcionMapaSismico();
            Program.PantallaPrincipal.solicitarOpcionMapaSismico();
            Program.PantallaPrincipal.habilitarOpcionModificarDatos();
            Program.PantallaPrincipal.solicitarOpcionModificarDatos();
            Program.PantallaPrincipal.habilitarGrillaDatos();
            Program.PantallaPrincipal.solicitarOpcionGrilla();

        }

        private void ordenarPorEstacionSismologica(ref List<string[]> datos)
        {
            datos.Sort((fila1, fila2) => string.Compare(fila1[0], fila2[0]));
        }

        private bool ValidarExistencia(string txtAlcance, string txtOrigen, string txtMagnitud, bool rbtConfirmar, bool rbtRechazar, bool rbtSolicitarRev)
        {
            if (string.IsNullOrWhiteSpace(txtAlcance) ||
                string.IsNullOrWhiteSpace(txtOrigen) ||
                string.IsNullOrWhiteSpace(txtMagnitud) ||
                (!rbtConfirmar && !rbtRechazar && !rbtSolicitarRev))
            {
                return false;
            }
            return true;
        }

        public string getFecha()
        {
            return DateTime.Now.ToString("dd/MM/yyyy");
        }

        public string getHora()
        {
            return DateTime.Now.ToString("HH:mm");
        }

        
        public void tomarOpcionMapaSismico()
        {
            MessageBox.Show("Funcionalidad no implementada");
        }

        // Llamado cuando el usuario selecciona "No" en modificar datos
        public void TomarOpcionModificarDatos()
        {
            MessageBox.Show("Flujo alternativo no implementado");
        }

        // Llamado cuando el usuario selecciona "Rechazado"
        public void tomarOpcionGrilla(string txtAlcance, string txtOrigen, string txtMagnitud, bool rbtConfirmar, bool rbtRechazar, bool rbtSolicitarRev)
        {
            if (this.ValidarExistencia(txtAlcance, txtOrigen, txtMagnitud, rbtConfirmar, rbtRechazar, rbtSolicitarRev))
            {
                if (rbtConfirmar)
                {
                    MessageBox.Show("Funcionalidad en implementación", "Confirmar Evento", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (rbtRechazar)
                {
                    Estado estadoRechazado = this.buscarRechazado();

                    if (estadoRechazado != null && this.eventoSismicoSeleccionado != null)
                    {
                        //this.eventoSismicoSeleccionado.rechazar(estadoRechazado, DateTime.Now);
                        this.eventoSismicoSeleccionado.rechazar(estadoRechazado, DateTime.Now, this.empleadoLogueado);
                        Program.Db.SaveChanges();
                    }

                    this.FinCU();
                }
                if (rbtSolicitarRev)
                {
                    MessageBox.Show("Funcionalidad en implementación", "Solicitar Revisión", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private Estado buscarRechazado()
        {
            // --- INICIO DE CAMBIOS ---
            // Antes: Program.Estados.FirstOrDefault(...)
            // Ahora:
            return Program.Db.Estados.FirstOrDefault(e => e.NombreEstado == "Rechazado");
            // --- FIN DE CAMBIOS ---
        }

        private void FinCU()
        {
            MessageBox.Show("Evento rechazado. La aplicación se cerrará.", "Revisión Finalizada", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // Cerrar la aplicación completamente
            Application.Exit();
        }

        private void generarNuevosEventosAutodetectados()
        {
            // Obtener datos necesarios de la base de datos
            var estadoPendienteRevisar = Program.Db.Estados
                .FirstOrDefault(e => e.Ambito == "EventoSismografico" && e.NombreEstado == "PendienteRevisar");
            
            var alcanceLocal = Program.Db.AlcancesSismos.FirstOrDefault();
            var origenTectonico = Program.Db.OrigenesDeGeneracion.FirstOrDefault();
            var clasifSuperficial = Program.Db.ClasificacionesSismos.FirstOrDefault();
            var empleado = Program.Db.Empleados.FirstOrDefault();
            var sismografos = Program.Db.Sismografos.Include(s => s.Estacion).ToList();
            var tiposDato = Program.Db.TiposDeDato.ToList();

            if (estadoPendienteRevisar == null || alcanceLocal == null || origenTectonico == null || 
                clasifSuperficial == null || empleado == null || !sismografos.Any() || !tiposDato.Any())
            {
                return; // No podemos generar eventos sin los datos básicos
            }

            // Generar 3 nuevos eventos autodetectados
            var random = new Random();
            var ubicacionesBase = new[]
            {
                new { Lat = -34.6037, Lon = -58.3816, Nombre = "Buenos Aires" },
                new { Lat = -24.7859, Lon = -65.4116, Nombre = "Salta" },
                new { Lat = -32.9442, Lon = -60.6505, Nombre = "Rosario" },
                new { Lat = -31.4201, Lon = -64.1888, Nombre = "Córdoba" },
                new { Lat = -26.8083, Lon = -65.2176, Nombre = "Tucumán" }
            };

            for (int i = 0; i < 3; i++)
            {
                var ubicacion = ubicacionesBase[random.Next(ubicacionesBase.Length)];
                var variacionLat = (random.NextDouble() - 0.5) * 0.2; // ±0.1 grados
                var variacionLon = (random.NextDouble() - 0.5) * 0.2; // ±0.1 grados
                
                var evento = new EventoSismico(
                    DateTime.Now.AddMinutes(-random.Next(30, 180)), // Entre 30 minutos y 3 horas atrás
                    ubicacion.Lat + variacionLat,
                    ubicacion.Lon + variacionLon,
                    ubicacion.Lat + variacionLat - 0.01,
                    ubicacion.Lon + variacionLon - 0.01,
                    2.5 + (random.NextDouble() * 3.5), // Magnitud entre 2.5 y 6.0
                    true, // Es autodetectado
                    estadoPendienteRevisar,
                    alcanceLocal,
                    origenTectonico,
                    clasifSuperficial
                );

                // Agregar cambio de estado inicial
                var cambioEstado = new CambioEstado(
                    estadoPendienteRevisar, 
                    DateTime.Now.AddMinutes(-random.Next(25, 175)), 
                    null, 
                    empleado
                );
                evento.CambiosEstado.Add(cambioEstado);

                // Generar series temporales con datos de muestra
                var sismografoSeleccionado = sismografos[random.Next(sismografos.Count)];
                var fechaInicio = evento.FechaHoraOcurrencia;
                var fechaFin = fechaInicio.AddMinutes(random.Next(10, 30));
                
                var serieTemporal = new SerieTemporal(
                    random.Next(2) == 0, // booleano aleatorio para algún campo
                    fechaInicio,
                    fechaFin,
                    0.1 + (random.NextDouble() * 0.4), // intervalo entre 0.1 y 0.5
                    sismografoSeleccionado
                );

                // Generar muestras sísmicas
                int numMuestras = random.Next(5, 15);
                for (int j = 0; j < numMuestras; j++)
                {
                    var muestra = new MuestraSismica(fechaInicio.AddSeconds(j * 10));
                    
                    // Agregar detalles para cada tipo de dato disponible
                    foreach (var tipoDato in tiposDato)
                    {
                        var valor = tipoDato.Denominacion.Contains("Magnitud") 
                            ? evento.ValorMagnitud + (random.NextDouble() - 0.5) * 0.5
                            : 30 + (random.NextDouble() * 40); // Para aceleración u otros
                            
                        var detalle = new DetalleMuestraSismica(valor, tipoDato);
                        muestra.Detalles.Add(detalle);
                    }
                    
                    serieTemporal.Muestras.Add(muestra);
                }
                
                evento.SeriesTemporales.Add(serieTemporal);

                Program.Db.EventosSismicos.Add(evento);
            }

            // Guardar en la base de datos
            Program.Db.SaveChanges();
            
            MessageBox.Show("Se han generado nuevos eventos sísmicos autodetectados.", "Nuevos Eventos", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}