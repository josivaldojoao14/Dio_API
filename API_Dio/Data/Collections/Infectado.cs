using MongoDB.Driver.GeoJsonObjectModel;
using System;

namespace API_Dio.Data.Collections
{
    public class Infectado
    {
        public Infectado(DateTime datanasc, string sexo, double latitude, double longitude) 
        {
            this.DataNascimento = datanasc;
            this.Sexo = sexo;
            this.Localizacao = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }

        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public GeoJson2DGeographicCoordinates Localizacao { get; set; }
    }
}
