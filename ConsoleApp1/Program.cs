using System;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        public static void testSaveMyToMongo()
        {
            UserDao dao = new UserDao();
            testmysql testmysql = new testmysql();
            bool go = true;
            while (go)
            {
                Console.WriteLine("test MongoDB.... 1.select 2.add 3.update 4.delete 0.exit");
                switch (Console.ReadLine())
                {
                    case "1":
                        string show = string.Empty;
                        dao.FindAll().ToList().ForEach(c => show += $"id:{c.Id},name:{c.Name},Price:{c.Price} \n");
                        Console.Write(show);
                        break;

                    case "2":
                        Console.Write("輸入Name : ");
                        string name = Console.ReadLine();
                        dao.Insert(name);
                        break;

                    case "3":
                        Console.Write("輸入id : ");
                        string id = Console.ReadLine();
                        Console.Write("輸入Name : ");
                        string name3 = Console.ReadLine().ToString();
                        Console.Write("輸入price : ");
                        decimal price = decimal.Parse(Console.ReadLine());
                        bool ans = dao.Update(new user() { Id = id, Name = name3, Price = price });
                        Console.Write(ans);
                        break;

                    case "4":
                        Console.Write("輸入id : ");
                        string id4 = Console.ReadLine();
                        bool ans2 = dao.Delete(id4);
                        Console.Write(ans2);
                        break;

                    case "5":
                        var allevent = testmysql.GetADOAllEvent();
                        foreach (var eve in allevent)
                        {
                            string showstr = $" {eve.MAC}";
                            Console.WriteLine(showstr);
                        }
                        break;

                    case "0":
                        go = false;
                        break;
                }
                Console.WriteLine("---------------------------------------------------------");
            }
        }

        private static void Main(string[] args)
        {
            new mssqldapper().Conn();
        }
    }
}