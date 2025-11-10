namespace EventoSismicoApp.Forms
{
    partial class PantallaNuevoEventoSismico
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnRegResRevManual = new Button();
            btnHabilitarMapa = new Button();
            btnFinalizar = new Button();
            btnModificarDatosEventoSismico = new Button();
            grillaEventos = new DataGridView();
            txtMagnitud = new TextBox();
            txtAlcance = new TextBox();
            txtOrigen = new TextBox();
            btnSeleccionarEvento = new Button();
            groupEventos = new GroupBox();
            lblAlcance = new Label();
            lblOrigen = new Label();
            lblClasificacion = new Label();
            groupModificarDatos = new GroupBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            groupGrillaOpciones = new GroupBox();
            rbtSolicitarRev = new RadioButton();
            rbtRechazar = new RadioButton();
            rbtConfirmar = new RadioButton();
            groupDetalles = new GroupBox();
            grillaSeries = new DataGridView();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)grillaEventos).BeginInit();
            groupEventos.SuspendLayout();
            groupModificarDatos.SuspendLayout();
            groupGrillaOpciones.SuspendLayout();
            groupDetalles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grillaSeries).BeginInit();
            SuspendLayout();
            // 
            // btnRegResRevManual
            // 
            btnRegResRevManual.Location = new Point(12, 12);
            btnRegResRevManual.Name = "btnRegResRevManual";
            btnRegResRevManual.Size = new Size(244, 23);
            btnRegResRevManual.TabIndex = 0;
            btnRegResRevManual.Text = "Registrar resultado revisión manual";
            btnRegResRevManual.UseVisualStyleBackColor = true;
            btnRegResRevManual.Click += opcionRegistrarResultadoRevisionManual;
            // 
            // btnHabilitarMapa
            // 
            btnHabilitarMapa.Location = new Point(553, 18);
            btnHabilitarMapa.Name = "btnHabilitarMapa";
            btnHabilitarMapa.Size = new Size(162, 23);
            btnHabilitarMapa.TabIndex = 1;
            btnHabilitarMapa.Text = "Mostrar Mapa";
            btnHabilitarMapa.UseVisualStyleBackColor = true;
            btnHabilitarMapa.Click += tomarOpcionMapaSismico;
            // 
            // btnFinalizar
            // 
            btnFinalizar.Location = new Point(1352, 384);
            btnFinalizar.Name = "btnFinalizar";
            btnFinalizar.Size = new Size(163, 23);
            btnFinalizar.TabIndex = 3;
            btnFinalizar.Text = "Finalizar Revisión";
            btnFinalizar.UseVisualStyleBackColor = true;
            btnFinalizar.Click += tomarOpcionGrilla;
            // 
            // btnModificarDatosEventoSismico
            // 
            btnModificarDatosEventoSismico.Location = new Point(74, 120);
            btnModificarDatosEventoSismico.Name = "btnModificarDatosEventoSismico";
            btnModificarDatosEventoSismico.Size = new Size(167, 23);
            btnModificarDatosEventoSismico.TabIndex = 4;
            btnModificarDatosEventoSismico.Text = "Modificar Datos";
            btnModificarDatosEventoSismico.UseVisualStyleBackColor = true;
            btnModificarDatosEventoSismico.Click += tomarOpcionModificarDatos;
            // 
            // grillaEventos
            // 
            grillaEventos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaEventos.Dock = DockStyle.Fill;
            grillaEventos.Location = new Point(3, 19);
            grillaEventos.Name = "grillaEventos";
            grillaEventos.Size = new Size(718, 189);
            grillaEventos.TabIndex = 5;
            // 
            // txtMagnitud
            // 
            txtMagnitud.Location = new Point(74, 22);
            txtMagnitud.Name = "txtMagnitud";
            txtMagnitud.Size = new Size(167, 23);
            txtMagnitud.TabIndex = 8;
            // 
            // txtAlcance
            // 
            txtAlcance.Location = new Point(74, 51);
            txtAlcance.Name = "txtAlcance";
            txtAlcance.Size = new Size(167, 23);
            txtAlcance.TabIndex = 9;
            // 
            // txtOrigen
            // 
            txtOrigen.Location = new Point(74, 80);
            txtOrigen.Name = "txtOrigen";
            txtOrigen.Size = new Size(167, 23);
            txtOrigen.TabIndex = 10;
            // 
            // btnSeleccionarEvento
            // 
            btnSeleccionarEvento.Location = new Point(15, 255);
            btnSeleccionarEvento.Name = "btnSeleccionarEvento";
            btnSeleccionarEvento.Size = new Size(130, 23);
            btnSeleccionarEvento.TabIndex = 11;
            btnSeleccionarEvento.Text = "Seleccionar Evento";
            btnSeleccionarEvento.UseVisualStyleBackColor = true;
            btnSeleccionarEvento.Click += seleccionaEventoSismico;
            // 
            // groupEventos
            // 
            groupEventos.Controls.Add(grillaEventos);
            groupEventos.Location = new Point(12, 41);
            groupEventos.Name = "groupEventos";
            groupEventos.Size = new Size(724, 211);
            groupEventos.TabIndex = 12;
            groupEventos.TabStop = false;
            groupEventos.Text = "Lista de eventos detectados";
            // 
            // lblAlcance
            // 
            lblAlcance.AutoSize = true;
            lblAlcance.Location = new Point(6, 19);
            lblAlcance.Name = "lblAlcance";
            lblAlcance.Size = new Size(52, 15);
            lblAlcance.TabIndex = 14;
            lblAlcance.Text = "Alcance:";
            // 
            // lblOrigen
            // 
            lblOrigen.AutoSize = true;
            lblOrigen.Location = new Point(171, 19);
            lblOrigen.Name = "lblOrigen";
            lblOrigen.Size = new Size(46, 15);
            lblOrigen.TabIndex = 15;
            lblOrigen.Text = "Origen:";
            // 
            // lblClasificacion
            // 
            lblClasificacion.AutoSize = true;
            lblClasificacion.Location = new Point(305, 19);
            lblClasificacion.Name = "lblClasificacion";
            lblClasificacion.Size = new Size(77, 15);
            lblClasificacion.TabIndex = 16;
            lblClasificacion.Text = "Clasificación:";
            // 
            // groupModificarDatos
            // 
            groupModificarDatos.Controls.Add(label2);
            groupModificarDatos.Controls.Add(label3);
            groupModificarDatos.Controls.Add(btnHabilitarMapa);
            groupModificarDatos.Controls.Add(label4);
            groupModificarDatos.Controls.Add(txtAlcance);
            groupModificarDatos.Controls.Add(txtMagnitud);
            groupModificarDatos.Controls.Add(txtOrigen);
            groupModificarDatos.Controls.Add(btnModificarDatosEventoSismico);
            groupModificarDatos.Location = new Point(794, 55);
            groupModificarDatos.Name = "groupModificarDatos";
            groupModificarDatos.Size = new Size(721, 194);
            groupModificarDatos.TabIndex = 17;
            groupModificarDatos.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 83);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 20;
            label2.Text = "Origen:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 54);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 19;
            label3.Text = "Alcance:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 26);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 18;
            label4.Text = "Magnitud:";
            // 
            // groupGrillaOpciones
            // 
            groupGrillaOpciones.Controls.Add(rbtSolicitarRev);
            groupGrillaOpciones.Controls.Add(rbtRechazar);
            groupGrillaOpciones.Controls.Add(rbtConfirmar);
            groupGrillaOpciones.Location = new Point(800, 316);
            groupGrillaOpciones.Name = "groupGrillaOpciones";
            groupGrillaOpciones.Size = new Size(275, 97);
            groupGrillaOpciones.TabIndex = 18;
            groupGrillaOpciones.TabStop = false;
            groupGrillaOpciones.Text = "Seleccione acción";
            // 
            // rbtSolicitarRev
            // 
            rbtSolicitarRev.AutoSize = true;
            rbtSolicitarRev.Location = new Point(6, 72);
            rbtSolicitarRev.Name = "rbtSolicitarRev";
            rbtSolicitarRev.Size = new Size(180, 19);
            rbtSolicitarRev.TabIndex = 2;
            rbtSolicitarRev.TabStop = true;
            rbtSolicitarRev.Text = "Solicitar revisión a un experto";
            rbtSolicitarRev.UseVisualStyleBackColor = true;
            // 
            // rbtRechazar
            // 
            rbtRechazar.AutoSize = true;
            rbtRechazar.Location = new Point(6, 47);
            rbtRechazar.Name = "rbtRechazar";
            rbtRechazar.Size = new Size(111, 19);
            rbtRechazar.TabIndex = 1;
            rbtRechazar.TabStop = true;
            rbtRechazar.Text = "Rechazar evento";
            rbtRechazar.UseVisualStyleBackColor = true;
            // 
            // rbtConfirmar
            // 
            rbtConfirmar.AutoSize = true;
            rbtConfirmar.Location = new Point(6, 22);
            rbtConfirmar.Name = "rbtConfirmar";
            rbtConfirmar.Size = new Size(118, 19);
            rbtConfirmar.TabIndex = 0;
            rbtConfirmar.TabStop = true;
            rbtConfirmar.Text = "Confirmar evento";
            rbtConfirmar.UseVisualStyleBackColor = true;
            // 
            // groupDetalles
            // 
            groupDetalles.Controls.Add(grillaSeries);
            groupDetalles.Controls.Add(lblClasificacion);
            groupDetalles.Controls.Add(lblAlcance);
            groupDetalles.Controls.Add(lblOrigen);
            groupDetalles.Location = new Point(12, 297);
            groupDetalles.Name = "groupDetalles";
            groupDetalles.Size = new Size(721, 211);
            groupDetalles.TabIndex = 19;
            groupDetalles.TabStop = false;
            groupDetalles.Text = "Detalles del evento seleccionado";
            // 
            // grillaSeries
            // 
            grillaSeries.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaSeries.Location = new Point(6, 37);
            grillaSeries.Name = "grillaSeries";
            grillaSeries.Size = new Size(709, 170);
            grillaSeries.TabIndex = 17;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(1352, 481);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(163, 23);
            btnCancelar.TabIndex = 20;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += tomarOpcionCancelar;
            // 
            // PantallaNuevoEventoSismico
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1552, 535);
            Controls.Add(btnCancelar);
            Controls.Add(groupDetalles);
            Controls.Add(groupGrillaOpciones);
            Controls.Add(groupModificarDatos);
            Controls.Add(groupEventos);
            Controls.Add(btnSeleccionarEvento);
            Controls.Add(btnFinalizar);
            Controls.Add(btnRegResRevManual);
            Name = "PantallaNuevoEventoSismico";
            Text = "Registrar resultado de revisión manual";
            ((System.ComponentModel.ISupportInitialize)grillaEventos).EndInit();
            groupEventos.ResumeLayout(false);
            groupModificarDatos.ResumeLayout(false);
            groupModificarDatos.PerformLayout();
            groupGrillaOpciones.ResumeLayout(false);
            groupGrillaOpciones.PerformLayout();
            groupDetalles.ResumeLayout(false);
            groupDetalles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grillaSeries).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnRegResRevManual;
        private Button btnHabilitarMapa;
        private Button btnFinalizar;
        private Button btnModificarDatosEventoSismico;
        private DataGridView grillaEventos;
        private TextBox txtMagnitud;
        private TextBox txtAlcance;
        private TextBox txtOrigen;
        private Button btnSeleccionarEvento;
        private GroupBox groupEventos;
        private Label lblAlcance;
        private Label lblOrigen;
        private Label lblClasificacion;
        private GroupBox groupModificarDatos;
        private Label label2;
        private Label label3;
        private Label label4;
        private GroupBox groupGrillaOpciones;
        private RadioButton rbtSolicitarRev;
        private RadioButton rbtRechazar;
        private RadioButton rbtConfirmar;
        private GroupBox groupDetalles;
        private DataGridView grillaSeries;
        private Button btnCancelar;
    }
}
