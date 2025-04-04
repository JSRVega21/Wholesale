using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Wholesale.Models
{
    public class InvoiceSapCLS
    {
        [JsonPropertyName("Número de la Factura:")]
        public string? NumAtCard { get; set; }
        [JsonPropertyName("Fecha de la Factura:")]
        public string? DocDate { get; set; }
        [JsonPropertyName("Tienda:")]
        public string? U_FacSerie { get; set; }
        [JsonPropertyName("Nombre de cliente:")]
        public string? CardName { get; set; }
        [JsonPropertyName("Nombre de cliente en la factura:")]
        public string? U_FacNom { get; set; }
        [JsonPropertyName("Nit del cliente:")]
        public string? U_FacNit { get; set; }
        [JsonPropertyName("Telefono del cliente:")]
        public string? U_Telefonos { get; set; }
        [JsonPropertyName("Dirección de la factura:")]
        public string? Address2 { get; set; }
        [JsonPropertyName("Total de la factura:")]
        public decimal? DocTotal { get; set; }
        [JsonPropertyName("Total de bodega pequeña")]
        public decimal? TotalBodegaGrande { get; set; }
        [JsonPropertyName("Total de bodega grande")]
        public decimal? TotalBodegaPequena { get; set; }
        [JsonPropertyName("Total de productos sin categoria")]
        public decimal? TotalOtros { get; set; }
    }
}
