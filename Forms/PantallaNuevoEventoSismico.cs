using System.Windows.Forms.VisualStyles;
using EventoSismicoApp.Controller;
using EventoSismicoApp.Entities;
using static System.Windows.Forms.Design.AxImporter;

namespace EventoSismicoApp.Forms
{
    public partial class PantallaNuevoEventoSismico : Form
    {

        public PantallaNuevoEventoSismico()
        {
            InitializeComponent();


            this.btnHabilitarMapa.Enabled = false;
            this.btnModificarDatosEventoSismico.Enabled = false;
            this.groupGrillaOpciones.Enabled = false;
            this.groupDetalles.Enabled = false;
            this.groupGrillaOpciones.Enabled = false;
            this.groupEventos.Enabled = false;

            this.btnSeleccionarEvento.Enabled = false;

            this.groupModificarDatos.Enabled = false;
            this.txtAlcance.Enabled = false;
            this.txtMagnitud.Enabled = false;
            this.txtOrigen.Enabled = false;
            this.label2.Enabled = false;
            this.label3.Enabled = false;
            this.label4.Enabled = false;
            this.btnModificarDatosEventoSismico.Enabled = false;
            this.btnHabilitarMapa.Enabled = false;




            grillaEventos.Columns.Add("colHora", "Hora Ocurrencia");
            grillaEventos.Columns.Add("colUbicacion", "Ubicación");
            grillaEventos.Columns.Add("colMagnitud", "Magnitud");
            grillaEventos.Columns.Add("colEstadoActual", "Estado Actual");

            grillaSeries.Columns.Add("Estacion", "Estación");
            grillaSeries.Columns.Add("FechaHora", "Fecha y hora");
            grillaSeries.Columns.Add("TipoDato", "Tipo de dato");
            grillaSeries.Columns.Add("Valor", "Valor");
            grillaSeries.Columns.Add("Unidad", "Unidad");

            grillaSeries.Columns["Estacion"].Width = 120;
            grillaSeries.Columns["FechaHora"].Width = 120;
            grillaSeries.Columns["TipoDato"].Width = 120;
            grillaSeries.Columns["Valor"].Width = 120;
            grillaSeries.Columns["Unidad"].Width = 120;

        }

        private void opcionRegistrarResultadoRevisionManual(object sender, EventArgs e)
        {
            this.habilitar();
            Program.Manejador.registrarResultadoRevisionManual();
        }

        private void habilitar()
        {
            groupEventos.Enabled = true;
            //grillaEventos.Enabled = true;
            this.btnSeleccionarEvento.Enabled = true;
        }


        private List<EventoSismico> _eventosMostrados; // Lista interna
        public void presentarEventos(List<EventoSismico> eventos)
        {
            // Guardar referencia a los eventos en un campo privado
            _eventosMostrados = eventos;

            grillaEventos.Rows.Clear();

            // Agregar cada evento a la grilla
            foreach (var evento in eventos)
            {
                grillaEventos.Rows.Add(
                    evento.getHoraOcurrencia().ToString("yyyy-MM-dd HH:mm:ss"),
                    evento.getUbicacion(),
                    evento.getMagnitud().ToString("0.0"),
                    evento.EstadoActual?.NombreEstado ?? "N/A"
                );
            }
        }


        private void seleccionaEventoSismico(object sender, EventArgs e)
        {
            if (grillaEventos.SelectedRows.Count > 0)
            {
                int rowIndex = grillaEventos.SelectedRows[0].Index;
                Program.Manejador.tomarSeleccionEventoSismico(rowIndex);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un evento de la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void presentarDetalleEvento(string alcance, string clasificacion, string origen, List<string[]> series)
        {
            // === Paso 9.1: Mostrar datos básicos ===
            lblAlcance.Text = "Alcance: " + alcance;
            lblOrigen.Text = "Origen: " + origen;
            lblClasificacion.Text = "Clasificacion: " + clasificacion;
            txtAlcance.Text = alcance;
            txtOrigen.Text = origen;


            // === Paso 9.2: Mostrar series y datos ===
            grillaSeries.Rows.Clear();

            foreach (var fila in series)
            {
                grillaSeries.Rows.Add(fila[0], fila[1], fila[2], fila[3], fila[4]);
            }
            btnCancelar.Enabled = true;
        }

        public void habilitarOpcionMapaSismico()
        {
            this.groupDetalles.Enabled = true;
            this.groupModificarDatos.Enabled = true;
        }

        public void solicitarOpcionMapaSismico()
        {
            MessageBox.Show("Se habilitó la opción para visualizar mapa. Seleccione Mostrar Mapa para visualizar el evento sismico y las estaciones involucradas.", "Modificar datos del evento", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tomarOpcionMapaSismico(object sender, EventArgs e)
        {
            Program.Manejador.tomarOpcionMapaSismico();
        }

        public void habilitarOpcionModificarDatos()
        {
            this.groupModificarDatos.Enabled = true;
            this.txtAlcance.Enabled = true;
            this.txtMagnitud.Enabled = true;
            this.txtOrigen.Enabled = true;
            this.label2.Enabled = true;
            this.label3.Enabled = true;
            this.label4.Enabled = true;
            this.btnModificarDatosEventoSismico.Enabled = true;
            this.btnHabilitarMapa.Enabled = true;
        }

        public void solicitarOpcionModificarDatos()
        {
            MessageBox.Show("Se habilitó la opción para modificar datos del evento. Seleccione Modificar Datos para realizar cambios.", "Modificar datos del evento", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tomarOpcionModificarDatos(object sender, EventArgs e)
        {
            Program.Manejador.TomarOpcionModificarDatos();
        }

        private void tomarOpcionCancelar(object sender, EventArgs e)
        {
            // 3. Respetamos el patrón: la Pantalla notifica al Manejador
            Program.Manejador.tomarOpcionCancelar();
            btnCancelar.Enabled = false;
            MessageBox.Show("Revisión cancelada. El evento ha sido liberado.");
        }

        public void habilitarGrillaDatos()
        {
            groupGrillaOpciones.Enabled = true;

            btnFinalizar.Enabled = true;
        }


        public void solicitarOpcionGrilla()
        {
            MessageBox.Show("Seleccione una acción para finalizar el evento: Confirmar, Rechazar o Solicitar revisión a experto.", "Acción requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tomarOpcionGrilla(object sender, EventArgs e)
        {
            Program.Manejador.tomarOpcionGrilla(txtAlcance.Text, txtOrigen.Text, txtMagnitud.Text, rbtConfirmar.Checked, rbtRechazar.Checked, rbtSolicitarRev.Checked);
        }
    }
}
