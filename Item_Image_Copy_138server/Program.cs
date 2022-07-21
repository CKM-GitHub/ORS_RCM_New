using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Item_Image_Copy_138server
{
    class Program
    {
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static String ItemImage = ConfigurationManager.AppSettings["ItemImage"].ToString();
        static String ItemImageNew = ConfigurationManager.AppSettings["ItemImage_New"].ToString();
        static void Main(string[] args)
        {
            string list = SelectItemImageName();
            //ConsoleWriteLine_Tofile("1.Select Image Name : " + list);
            if (!string.IsNullOrWhiteSpace(list))
            {
                string[] image_names = list.Split(',');
                foreach (string image_name in image_names)
                {
                    if (File.Exists(ItemImage + image_name.Trim()))
                    {
                        if (!File.Exists(ItemImage + image_name))
                            File.Copy(ItemImage + image_name.Trim(), ItemImageNew + image_name.Trim());
                    }
                    UpdateItem_Image_NotExists_Flag(image_name.Trim());
                }
            }
        }
        public static void UpdateItem_Image_NotExists_Flag(string ImageName)
        {
            SqlConnection connectionstring = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Item_Image_NotExists_Flag_Change";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = connectionstring;
                cmd.Parameters.AddWithValue("@ImageName", ImageName);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        static string SelectItemImageName()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SP_Item_Image_NotExists_List_For_ImageCheck", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.CommandTimeout = 0;
                sda.SelectCommand.Connection.Open();
                sda.Fill(dt);
                sda.SelectCommand.Connection.Close();
                return dt.Rows[0]["ImageName"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
