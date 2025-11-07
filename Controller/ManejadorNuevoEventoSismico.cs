using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using EventoSismicoApp;
using EventoSismicoApp.Entities;
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

        public void registrarResultadoRevisionManual()
        {
            //eventosAutodetectados = Program.EventosSismicos;
            buscarAutodetectados();
            ordenarFechaHora();
            Program.PantallaPrincipal.presentarEventos(this.eventosAutodetectados);
        }

        private void buscarAutodetectados()
        {
            eventosAutodetectados = new List<EventoSismico>();// TODO: no deebría guardar la lista de eventos sismicos.
            //Primer loop
            foreach (var evento in Program.EventosSismicos)// Mientras haya eventos
            {
                if (evento.esPendienteRevisar())
                {
                    var hora = evento.getHoraOcurrencia();
                    var ubicacion = evento.getUbicacion(); // Internamente llama a getCoordEpicentro/Hipocentro
                    var magnitud = evento.getMagnitud();

                    eventosAutodetectados.Add(evento);
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

                eventoSismicoSeleccionado.revisar(fechaHoraActual, this.estadoBloqueadoEnRevision); // Pasamos la fecha actual. TODO: Usar los metodos getFecha y getHora


                this.buscarDetalleEventoSismico();
            }
        }

        
        private void buscarEstadoBloqueadoEnRevision()
        {
            foreach (var estado in Program.Estados)
            {
                if (estado.esAmbitoEventoSismografico() && estado.esBloqueado())
                {
                    this.estadoBloqueadoEnRevision = estado;
                    break;
                }
            }
        }

        
        private void buscarUsuarioLogueado()
        {
            if (Program.SesionActual != null)
            {
                var empleadoDeSesion = Program.SesionActual.GetEmpleadoEnSesion();

                
                foreach (var empleado in Program.Empleados)
                {
                    if (empleado.EsTuUsuario(Program.SesionActual.Usuario))
                    {
                        this.usuarioLogueado = Program.SesionActual.Usuario;
                        break;
                    }
                }
            }
        }

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

                }
                if (rbtRechazar)
                {
                    Estado estadoRechazado = this.buscarRechazado();

                    if (estadoRechazado != null && this.eventoSismicoSeleccionado != null)
                    {
                        this.eventoSismicoSeleccionado.rechazar(estadoRechazado, DateTime.Now);
                    }

                    this.FinCU();
                }
                if (rbtSolicitarRev)
                {

                }
            }
        }

        private Estado buscarRechazado()
            => Program.Estados.FirstOrDefault(e => e.EsRechazado());

        private void FinCU()
        {
            MessageBox.Show("Evento finalizado");
        }



    }
}