using Sap.Data.Hana;
using System;
using Wholesale.Models;

namespace Wholesale.server.Repository
{
    public class InvoiceRepository
    {
        private readonly HanaSettings _hanaSettings;

        public InvoiceRepository()
        {
            _hanaSettings = new HanaSettings();
        }

        public InvoiceSapCLS GetInvoice(string NumAtCard)
        {
            string connectionString = _hanaSettings.HanaConexion;
            InvoiceSapCLS invoice = null;

            try
            {
                using (HanaConnection connection = new HanaConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                    SELECT 
                      T1.""NumAtCard"",
                      T1.""U_FacSerie"",
                      T1.""CardName"",
                      T1.""U_FacNom"",
                      T1.""U_FacNit"",
                      T1.""U_Telefonos"",
                      T1.""Address2"",
                      T1.""DocTotal"",
                      ROUND(SUM(CASE WHEN T3.""U_CLASIFICACION"" = 'Bodega Grande' THEN T2.""LineTotal"" ELSE 0 END) * 1.12, 2) AS ""TotalBodegaGrande"",
                      ROUND(SUM(CASE WHEN T3.""U_CLASIFICACION"" = 'Bodega Pequeña' THEN T2.""LineTotal"" ELSE 0 END) * 1.12, 2) AS ""TotalBodegaPequena"",
                      ROUND(SUM(CASE WHEN T3.""U_CLASIFICACION"" NOT IN ('Bodega Grande', 'Bodega Pequeña') THEN T2.""LineTotal"" ELSE 0 END) * 1.12, 2) AS ""TotalOtros""
                    FROM ""SBO_GT_FFACSA"".""OINV"" AS T1
                    INNER JOIN ""SBO_GT_FFACSA"".""INV1"" AS T2
                      ON T1.""DocEntry"" = T2.""DocEntry""
                    INNER JOIN ""SBO_GT_FFACSA"".""OITM"" AS T3
                      ON T2.""ItemCode"" = T3.""ItemCode""
                    WHERE T1.""NumAtCard"" = ?
                    GROUP BY 
                      T1.""U_FacSerie"", 
                      T1.""CardName"", 
                      T1.""U_FacNom"", 
                      T1.""U_FacNit"", 
                      T1.""U_Telefonos"", 
                      T1.""DocTotal"",  
                      T1.""Address2"", 
                      T1.""NumAtCard""";

                    using (HanaCommand command = new HanaCommand(query, connection))
                    {
                        command.Parameters.Add(new HanaParameter
                        {
                            HanaDbType = HanaDbType.VarChar,
                            Value = NumAtCard
                        });

                        using (HanaDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                invoice = new InvoiceSapCLS
                                {
                                    NumAtCard = reader.IsDBNull(0) ? null : reader.GetString(0),
                                    U_FacSerie = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    CardName = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    U_FacNom = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    U_FacNit = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    U_Telefonos = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    Address2 = reader.IsDBNull(6) ? null : reader.GetString(6),
                                    DocTotal = reader.IsDBNull(7) ? null : reader.GetDecimal(7),
                                    TotalBodegaGrande = reader.IsDBNull(8) ? null : reader.GetDecimal(8),
                                    TotalBodegaPequena = reader.IsDBNull(9) ? null : reader.GetDecimal(9),
                                    TotalOtros = reader.IsDBNull(10) ? null : reader.GetDecimal(10)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetInvoice: {ex}");
                invoice = null;
            }

            return invoice;
        }
    }
}
