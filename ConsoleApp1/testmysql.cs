using MaxP.Arpro.Models.MySqlDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxP.Arpro.Models;
using MySql.Data.MySqlClient;

namespace ConsoleApp1
{
    public class testmysql
    {
        ArproContext context;
        string sqlconn;
        public testmysql(){
           sqlconn = System.Configuration.ConfigurationManager.ConnectionStrings["ArproContext"].ConnectionString;
           context = ArproContextFactory.GetArproContext(sqlconn); 
        }

        public IQueryable<Event> GetAllEvent() {
            try
            {
                return context.Event.AsQueryable().Skip(0).Take(10);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public List<Event> GetADOAllEvent()
        {
            List<Event> events = new List<Event>();
            using (MySqlConnection connection = new MySqlConnection(sqlconn)) {
                string strsql = "select * from Event";
                using (MySqlCommand command = new MySqlCommand(strsql,connection)) {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Event eve = new Event
                        {
                            MAC = reader["MAC"].ToStringOrNull(),
                        };
                        events.Add(eve);
                    }
                }
            }
            return events;
        }

    }
}
