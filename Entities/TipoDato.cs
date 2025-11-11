namespace EventoSismicoApp.Entities
{
    public class TipoDato
    {
        public int Id { get; set; }

        public string Denominacion { get; set; }
        public double ValorUmbral { get; set; }
        public string NombreUnidadMedida { get; set; }

        public TipoDato() { }

        public TipoDato(string denominacion, double valorUmbral, string nombreUnidadMedida)
        {
            Denominacion = denominacion;
            ValorUmbral = valorUmbral;
            NombreUnidadMedida = nombreUnidadMedida;
        }

        // Método del diagrama
        //public object GetDatos() => new { Denominacion, ValorUmbral, NombreUnidadMedida };

        public string[] getDatos()
        {
            return new string[] { this.Denominacion, this.NombreUnidadMedida };
        }

    }
}