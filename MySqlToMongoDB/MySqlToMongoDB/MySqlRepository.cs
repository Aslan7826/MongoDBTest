using MaxP.Arpro.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace MySqlToMongoDB
{
    internal class MySqlRepository
    {
        internal MySqlRepository()
        {
        }

        internal int GetTabelCount()
        {
            int i = 0;
            string sqlconn = System.Configuration.ConfigurationManager.ConnectionStrings["ArproContext2"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(sqlconn))
            {
                string strsql = "select count(*) count from Event";
                using (MySqlCommand command = new MySqlCommand(strsql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["count"] != null)
                        {
                            int.TryParse(reader["count"].ToString(), out i);
                        }
                    }
                    connection.Close();
                }
            }
            return i;
        }

        internal IEnumerable<Event> GetEvents(int page, int pagenum)
        {
            string sqlconn = System.Configuration.ConfigurationManager.ConnectionStrings["ArproContext2"].ConnectionString;
            string strsql = "select * from Event LIMIT @page,@pagenum";
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(sqlconn))
                {
                    using (MySqlCommand command = new MySqlCommand(strsql, connection))
                    {
                        command.CommandTimeout = 180;
                        command.Parameters.AddWithValue("@page", page);
                        command.Parameters.AddWithValue("@pagenum", pagenum);
                        connection.Open();
                        var reader = command.ExecuteReader();
                        dt.Load(reader);
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new TableTypeCasting().GetParm<Event>(dt);
        }

        //internal async Task<IEnumerable<Event>> GetEvents_async(int page,int pagenum) {
        //    try
        //    {
        //        await connection.OpenAsync();
        //        DataTable dt = new DataTable();
        //        string strsql = "select * from Event LIMIT @page,@pagenum";
        //        using (MySqlCommand command = new MySqlCommand(strsql, connection))
        //        {
        //            command.Parameters.AddWithValue("@page", page);
        //            command.Parameters.AddWithValue("@pagenum", pagenum);
        //            var reader = await command.ExecuteReaderAsync();
        //            dt.Load(reader);
        //        }
        //        await connection.CloseAsync();
        //        var returnlist = new TableTypeCasting().GetParm<Event>(dt).ToList();
        //        return returnlist;
        //    }
        //    catch (Exception ex) {
        //        throw ex;
        //    }
        //}
    }
}