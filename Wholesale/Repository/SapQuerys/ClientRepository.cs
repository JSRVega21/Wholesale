using Sap.Data.Hana;
using System;
using System.Collections.Generic;
using Wholesale.Models;

namespace Wholesale.server.Repository
{
    public class ClientRepository
    {
        private readonly HanaSettings _hanaSettings;

        public ClientRepository()
        {
            _hanaSettings = new HanaSettings();
        }

        public ClientHeaderCLS GetClients(int? slpCode, string uCodigoPOS)
        {
            string connectionString = _hanaSettings.HanaConexion;
            var header = new ClientHeaderCLS();
            header.Clients = new List<ClientDetailCLS>();

            try
            {
                using (HanaConnection connection = new HanaConnection(connectionString))
                {
                    connection.Open();

                    string queryBase = @"SELECT T0.""CardCode"", T0.""CardName"", T1.""SlpCode"", 
                                    T1.""U_CodigoPOS"", T1.""SlpName"", T1.""U_Region"", T1.""U_RegionMayoreo"", 
                                    T0.""LicTradNum"", T0.""Phone1"", T0.""Address"", T0.""City"", T0.""Country""
                                FROM ""SBO_GT_FFACSA"".""OCRD"" T0
                                INNER JOIN ""SBO_GT_FFACSA"".""OSLP"" T1 ON T0.""SlpCode"" = T1.""SlpCode"" ";

                    if (slpCode.HasValue)
                    {
                        queryBase += @"WHERE T1.""SlpCode"" = ?";
                    }
                    else if (!string.IsNullOrEmpty(uCodigoPOS))
                    {
                        queryBase += @"WHERE T1.""U_CodigoPOS"" = ?";
                    }
                    else
                    {
                        return null; // Si no viene ningún filtro, no buscamos nada.
                    }

                    using (HanaCommand command = new HanaCommand(queryBase, connection))
                    {
                        if (slpCode.HasValue)
                        {
                            command.Parameters.Add(new HanaParameter
                            {
                                HanaDbType = HanaDbType.Integer,
                                Value = slpCode.Value
                            });
                        }
                        else
                        {
                            command.Parameters.Add(new HanaParameter
                            {
                                HanaDbType = HanaDbType.VarChar,
                                Value = uCodigoPOS
                            });
                        }

                        using (HanaDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (header.SlpCode == 0)
                                {
                                    header.SlpCode = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                    header.U_CodigoPOS = reader.IsDBNull(3) ? "Sin código POS" : reader.GetString(3);
                                    header.SlpName = reader.IsDBNull(4) ? "Sin nombre de vendedor" : reader.GetString(4);
                                    header.U_Region = reader.IsDBNull(5) ? "Sin región asignada" : reader.GetString(5);
                                    header.U_RegionMayoreo = reader.IsDBNull(6) ? "Sin región mayoreo asignada" : reader.GetString(6);
                                }

                                var clientDetail = new ClientDetailCLS
                                {
                                    CardCode = reader.IsDBNull(0) ? "Sin código de cliente" : reader.GetString(0),
                                    CardName = reader.IsDBNull(1) ? "Sin nombre de cliente" : reader.GetString(1),
                                    LicTradNum = reader.IsDBNull(7) ? "Sin NIT" : reader.GetString(7),
                                    Phone1 = reader.IsDBNull(8) ? "Sin teléfono" : reader.GetString(8),
                                    Address = reader.IsDBNull(9) ? "Sin dirección" : reader.GetString(9),
                                    City = reader.IsDBNull(10) ? "No tiene asignada una ciudad" : reader.GetString(10),
                                    Country = reader.IsDBNull(11) ? "Sin país asignado" : reader.GetString(11)
                                };
                                header.Clients.Add(clientDetail);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ClientRepository: {ex.Message}");
                header = null;
            }

            return header;
        }


        public List<UserSapCLS> GetUsersSap()
        {
            string connectionString = _hanaSettings.HanaConexion;
            var users = new List<UserSapCLS>();

            try
            {
                using (HanaConnection connection = new HanaConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT ""SlpCode"", ""U_CodigoPOS"", ""SlpName"", ""U_Region"", ""U_RegionMayoreo""
                             FROM ""SBO_GT_FFACSA"".""OSLP""
                             WHERE ""Active"" = 'Y'
                               AND ""Memo"" = 'Mayoreo'
                             ORDER BY ""SlpCode"" ASC";

                    using (HanaCommand command = new HanaCommand(query, connection))
                    using (HanaDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new UserSapCLS
                            {
                                SlpCode = reader.IsDBNull(0) ? "Sin código" : reader.GetString(0),
                                U_CodigoPOS = reader.IsDBNull(1) ? "Sin código POS" : reader.GetString(1),
                                SlpName = reader.IsDBNull(2) ? "Sin nombre de vendedor" : reader.GetString(2),
                                U_Region = reader.IsDBNull(3) ? "Sin región asignada" : reader.GetString(3),
                                U_RegionMayoreo = reader.IsDBNull(4) ? "Sin región mayoreo asignada" : reader.GetString(4)
                            };

                            users.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetUsersSap: {ex.Message}");
                users = null;
            }

            return users;
        }


    }
}
