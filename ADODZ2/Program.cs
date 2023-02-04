using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ADODZ2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DataSet setSuppliers = new DataSet();
                DataSet setProduct = new DataSet();
                SqlDataAdapter adapterSuppliers = new SqlDataAdapter("SELECT * FROM Suppliers;", "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Storage;Integrated Security=True;");
                SqlDataAdapter adapterProduct = new SqlDataAdapter("SELECT * FROM Product;", "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Storage;Integrated Security=True;");
                adapterSuppliers.Fill(setSuppliers);
                adapterProduct.Fill(setProduct);
                SqlCommandBuilder builderSuppliers = new SqlCommandBuilder(adapterSuppliers);
                SqlCommandBuilder builderProduct = new SqlCommandBuilder(adapterProduct);

                adapterSuppliers.InsertCommand = builderSuppliers.GetInsertCommand();
                adapterSuppliers.DeleteCommand = builderSuppliers.GetDeleteCommand();
                adapterSuppliers.UpdateCommand = builderSuppliers.GetUpdateCommand();

                adapterProduct.InsertCommand = builderProduct.GetInsertCommand();
                adapterProduct.DeleteCommand = builderProduct.GetDeleteCommand();
                adapterProduct.UpdateCommand = builderProduct.GetUpdateCommand();

               
             

                int maxcount = 0;
                int mincount = Int32.MaxValue;
                string counterName = "";
                string counterName2 = "";
                foreach (DataRow dr in setSuppliers.Tables[0].Rows)
                {
                    if (dr.Field<int>("Count") > maxcount)
                    {
                        maxcount = dr.Field<int>("Count");
                        counterName = dr.Field<string>("SuppliersName");
                    }
                    if (dr.Field<int>("Count") < mincount)
                    {
                        mincount = dr.Field<int>("Count");
                        counterName2 = dr.Field<string>("SuppliersName");
                    }
                }
                Console.WriteLine(counterName + " : " + maxcount + "tovarov");
                Console.WriteLine(counterName2 + " : " + mincount + "tovarov");

                DataViewManager dvm = new DataViewManager(setSuppliers);
                DataView dv = dvm.CreateDataView(setSuppliers.Tables[0]);
                dv.RowFilter = "Count = MAX(Count)";
                Console.WriteLine(dv.Count);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
      
       
    }
}
 

