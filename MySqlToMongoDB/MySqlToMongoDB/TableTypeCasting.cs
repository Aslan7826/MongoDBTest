using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MySqlToMongoDB
{
   internal class TableTypeCasting
    {
        //將DataTable轉型至T的指定類別
        internal IList<T> GetParm<T>(DataTable data) where T : new()
        {
            IList<PropertyInfo> propertyInfo = typeof(T).GetProperties().ToList();
            IList<T> returnT = new List<T>();
            foreach (var row in data.Rows)
            {
                var item = MappingItem<T>((DataRow)row, propertyInfo);
                returnT.Add(item);
            }
            return returnT;
        }
        private T MappingItem<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            try
            {
                foreach (var property in properties)
                {
                    if (row.Table.Columns.Contains(property.Name))
                    {
                        if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(Nullable<System.DateTime>))
                        {
                            DateTime dt = new DateTime();
                            if (DateTime.TryParse(row[property.Name].ToString(), out dt))
                            {
                                property.SetValue(item, dt, null);
                            }
                            else
                            {
                                property.SetValue(item, null, null);
                            }
                        }
                        else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(Nullable<decimal>))
                        {
                            decimal val = new decimal();
                            decimal.TryParse(row[property.Name].ToString(), out val);
                            property.SetValue(item, val, null);
                        }
                        else if (property.PropertyType == typeof(double))
                        {
                            double val = new double();
                            double.TryParse(row[property.Name].ToString(), out val);
                            property.SetValue(item, val, null);
                        }
                        else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(Nullable<long>) || property.PropertyType == typeof(Int64) || property.PropertyType == typeof(Nullable<Int64>))
                        {
                            long val = new long();
                            long.TryParse(row[property.Name].ToString(), out val);
                            property.SetValue(item, val, null);
                        }
                        else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(Nullable<int>))
                        {
                            int val = new int();
                            int.TryParse(row[property.Name].ToString(), out val);
                            property.SetValue(item, val, null);
                        }
                        else if (property.PropertyType == typeof(short) || property.PropertyType == typeof(Nullable<short>) || property.PropertyType == typeof(Int16) || property.PropertyType == typeof(Nullable<Int16>))
                        {
                            short val = new short();
                            short.TryParse(row[property.Name].ToString(), out val);
                            property.SetValue(item, val, null);
                        }
                        else if (property.PropertyType == typeof(Boolean) || property.PropertyType == typeof(Nullable<Boolean>))
                        {
                            bool val = new bool();
                            bool.TryParse(row[property.Name].ToString(), out val);
                            property.SetValue(item, val, null);
                        }
                        else
                        {
                            if (row[property.Name] != DBNull.Value)
                            {
                                property.SetValue(item, row[property.Name].ToString(), null);
                            }
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }

    }
}
