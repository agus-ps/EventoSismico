namespace EventoSismicoApp.Entities
{
    public class Sismografo
    {
        public string IdentificadorSismografo { get; set; }
        public string NumeroSerie { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public EstacionSismologica Estacion { get; set; }


        public Sismografo(string identificadorSismografo, string numeroSerie, DateTime fechaAdquisicion, EstacionSismologica estacion)
        {
            IdentificadorSismografo = identificadorSismografo;
            NumeroSerie = numeroSerie;
            FechaAdquisicion = fechaAdquisicion;
            Estacion = estacion;
        }

        // Método del diagrama
        public EstacionSismologica GetEstacion() => Estacion;
    }
}