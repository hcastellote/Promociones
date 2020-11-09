using Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Models
{
    [BsonDiscriminator("Promocion")]
    public class Promocion
    {
        [BsonId]
        public Guid _id { get; set; }

        [JsonProperty("promocion_id")]
        [BsonElement("id")]
        public Guid Id { get; set; }
        [JsonProperty("promocion_medio_de_pago")]
        [BsonElement("medios_de_pago")]
        public IEnumerable<string> MediosDePago { get; set; }
        [JsonProperty("promocion_bancos")]
        [BsonElement("bancos")]
        public IEnumerable<string> Bancos { get; set; }
        [JsonProperty("promocion_categorias_productos")]
        [BsonElement("categorias_productos")]
        public IEnumerable<string> CategoriasProductos { get; set; }
        [JsonProperty("promocion_maxima_cantidad_cuotas")]
        [BsonElement("maxima_cantidad_cuotas")]
        public int? MaximaCantidadDeCuotas { get; set; }
        [JsonProperty("promocion_valor_intereses_cuotas")]
        [BsonElement("valor_intereses_cuotas")]
        public decimal? ValorInteresesCuotas { get; set; }
        [JsonProperty("promocion_porcentaje_de_descuento")]
        [BsonElement("PorcentajeDeDescuento")]
        public decimal? PorcentajeDeDescuento { get; set; }
        [JsonProperty("promocion_fecha_inicio")]
        [BsonElement("fecha_inicio")]
        [BsonDateTimeOptions]
        public DateTime? FechaInicio { get; set; }
        [JsonProperty("promocion_fecha_fin")]
        [BsonElement("fecha_fin")]
        [BsonDateTimeOptions]
        public DateTime? FechaFin { get; set; }
        [JsonProperty("promocion_activo")]
        [BsonElement("activo")]
        public bool Activo { get; set; }
        [JsonProperty("promocion_fecha_creacion")]
        [BsonElement("fecha_creacion")]
        public DateTime? FechaCreacion { get; set; }
        [JsonProperty("promocion_fecha_modificacion")]
        [BsonElement("fecha_modificacion")]
        public DateTime? FechaModificacion { get; set; }


    }
}
