namespace EventoSismicoApp.Entities
{
    public class AlcanceSismo
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public AlcanceSismo(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }

        // Método del diagrama
        public string getNombre() => Nombre;
    }
}