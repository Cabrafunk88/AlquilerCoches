using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Conectar.MVVM.Data
{
    public class AccesoDatos
    {
        private readonly string connectionString;

        #region Constructores

        public AccesoDatos()
        {
            connectionString =
                "datasource=localhost;port=3309;username=root;password=1234;database=filmbrosdb;";
        }

        public AccesoDatos(string servidor, string puerto, string usuario, string password, string bbdd)
        {
            connectionString =
                $"datasource={servidor};port={puerto};username={usuario};password={password};database={bbdd};";
        }

        #endregion

        #region PROCEDIMIENTOS QUE DEVUELVEN DATOS (SELECT)

        // 1️⃣ Procedimiento SIN parámetros
        public async Task<DataTable> EjecutarProcedimientoAsync(string nombrePA)
        {
            return await EjecutarProcedimientoAsync(nombrePA, null, null);
        }

        // 2️⃣ Procedimiento CON parámetros IN
        public async Task<DataTable> EjecutarProcedimientoAsync(
            string nombrePA,
            List<string> nombresParametros,
            List<object> valoresParametros)
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand(nombrePA, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (nombresParametros != null && valoresParametros != null)
                    {
                        for (int i = 0; i < nombresParametros.Count; i++)
                        {
                            cmd.Parameters.AddWithValue(
                                "@" + nombresParametros[i],
                                valoresParametros[i]);
                        }
                    }

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        // DataAdapter no tiene FillAsync → se ejecuta en hilo de fondo
                        await Task.Run(() => da.Fill(tabla));
                    }
                }
            }

            return tabla;
        }

        #endregion

        #region PROCEDIMIENTOS SIN RESULTADO (INSERT / UPDATE / DELETE)

        // 3️⃣ Procedimiento que NO devuelve datos
        public async Task<int> EjecutarProcedimientoNonQueryAsync(
            string nombrePA,
            List<string> nombresParametros,
            List<object> valoresParametros)
        {
            int filasAfectadas;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand(nombrePA, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (nombresParametros != null && valoresParametros != null)
                    {
                        for (int i = 0; i < nombresParametros.Count; i++)
                        {
                            cmd.Parameters.AddWithValue(
                                "@" + nombresParametros[i],
                                valoresParametros[i]);
                        }
                    }

                    filasAfectadas = await cmd.ExecuteNonQueryAsync();
                }
            }

            return filasAfectadas;
        }

        #endregion

        #region PROCEDIMIENTOS CON PARÁMETROS OUT

        // 4️⃣ Procedimiento con parámetros OUT
        public async Task<Dictionary<string, object>> EjecutarProcedimientoConOutAsync(
            string nombrePA,
            List<string> nombresIN,
            List<object> valoresIN,
            List<string> nombresOUT)
        {
            Dictionary<string, object> valoresSalida = new Dictionary<string, object>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand(nombrePA, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros IN
                    if (nombresIN != null && valoresIN != null)
                    {
                        for (int i = 0; i < nombresIN.Count; i++)
                        {
                            cmd.Parameters.AddWithValue(
                                "@" + nombresIN[i],
                                valoresIN[i]);
                        }
                    }

                    // Parámetros OUT
                    foreach (string nombreOut in nombresOUT)
                    {
                        MySqlParameter pOut = new MySqlParameter(
                            "@" + nombreOut,
                            MySqlDbType.VarChar);
                        pOut.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pOut);
                    }

                    await cmd.ExecuteNonQueryAsync();

                    foreach (string nombreOut in nombresOUT)
                    {
                        valoresSalida[nombreOut] =
                            cmd.Parameters["@" + nombreOut].Value;
                    }
                }
            }

            return valoresSalida;
        }

        #endregion
    }
}
