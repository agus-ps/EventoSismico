namespace EventoSismicoApp.Entities
{
    public class Estado
    {
        public int Id { get; set; }
        public string Ambito { get; set; }
        public string NombreEstado { get; set; }

        public Estado() { }
        public Estado(string ambito, string nombreEstado)
        {
            this.Ambito = ambito;
            this.NombreEstado = nombreEstado;
        }

        public bool esAmbitoEventoSismografico()
        {
            return Ambito == "EventoSismografico";
        }

        public bool esBloqueado()
        {
            return NombreEstado == "BloqueadoEnRevision";
        }

        public bool EsRechazado()
        {
            return NombreEstado == "Rechazado";
        }

        public bool esPendienteRevisar()
        {
            return NombreEstado == "PendienteRevisar";
        }
    }
}