namespace EventoSismicoApp.Entities
{
    public class DetalleMuestraSismica
    {
        public double Valor { get; set; }
        public TipoDato TipoDato { get; set; }


        public DetalleMuestraSismica(double valor, TipoDato tipoDato)
        {
            Valor = valor;
            TipoDato = tipoDato;
        }

        // Método del diagrama - Versión corregida
        /*
        public object GetDatos()
        {
            return new 
            {
                Valor,
                DatosTipo = TipoDato != null ? TipoDato.GetDatos() : null
            };
        }*/

        public string[] GetDatos()
        {
            var tipo = this.TipoDato.getDatos(); // [denominacion, unidad]
            return new string[] { tipo[0], this.Valor.ToString(), tipo[1] };
        }

    }
}