using System.Collections.Generic;

namespace EventoSismicoApp.Entities
{
    public class MuestraSismica
    {
        public DateTime FechaHoraMuestra { get; set; }
        public List<DetalleMuestraSismica> Detalles { get; set; }

        public MuestraSismica(DateTime fechaHoraMuestra)
        {
            FechaHoraMuestra = fechaHoraMuestra;
            Detalles = new List<DetalleMuestraSismica>();
        }

        // Método del diagrama
        //public List<DetalleMuestraSismica> GetDatos() => Detalles;

        public List<string[]> GetDatos(string estacion)
        {
            var datos = new List<string[]>();

            foreach (var detalle in this.Detalles) // ahora es una lista
            {
                var tipo = detalle.GetDatos(); // [tipoDato, valor, unidad]

                datos.Add(new string[]
                {
            estacion,
            this.FechaHoraMuestra.ToString("g"),
            tipo[0], // tipoDato.denominacion
            tipo[1], // valor
            tipo[2]  // unidad
                });
            }

            return datos;
        }


    }
}