using ado.netcrud_operation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ado.netcrud_operation.product_dal
{
    public class DAL_product
    {
        String connString = ConfigurationManager.ConnectionStrings["addconnection"].ToString();

        //function to get the all the products

        public List<products> getAllProducts()
        {
            List<products> productList = new List<products>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_get_all_products";
                SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                connection.Open();
                sqlda.Fill(dt);
                connection.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    productList.Add(new products
                    {
                        productid = Convert.ToInt32(dr["productid"]),
                        productName = dr["productName"].ToString(),
                        HSN = Convert.ToInt32(dr["HSN"]),
                        mfg_com = dr["mfg_com"].ToString(),
                        batch = dr["batch"].ToString(),
                        expiry = Convert.ToDateTime(dr["expiry"]),
                        pkg = dr["pkg"].ToString(),
                        qty = Convert.ToInt32(dr["qty"]),
                        rate = Convert.ToInt32(dr["rate"])

                    });
                }

            }
            return productList;
        }

        public bool saveProduct(products product)
        {
            var id = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("sp_saveProducts", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productName", product.productName);
                cmd.Parameters.AddWithValue("@HSN", product.HSN);
                cmd.Parameters.AddWithValue("@mfg_com", product.mfg_com);
                cmd.Parameters.AddWithValue("@batch", product.batch);
                cmd.Parameters.AddWithValue("@expiry", product.expiry);
                cmd.Parameters.AddWithValue("@pkg", product.pkg);
                cmd.Parameters.AddWithValue("@qty", product.qty);
                cmd.Parameters.AddWithValue("@rate", product.rate);
                connection.Open();
                id = cmd.ExecuteNonQuery();
                connection.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public products getProduct(int id)
        {
            products product = new products();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("sp_get_product", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productid", id);
                SqlDataAdapter sqlad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                connection.Open();
                sqlad.Fill(dt);
                connection.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    product.productid = Convert.ToInt32(dr["productid"]);
                    product.productName = dr["productName"].ToString();
                    product.HSN = Convert.ToInt32(dr["HSN"]);
                    product.mfg_com = dr["mfg_com"].ToString();
                    product.batch = dr["batch"].ToString();
                    product.expiry = Convert.ToDateTime(dr["expiry"]);
                    product.pkg = dr["pkg"].ToString();
                    product.qty = Convert.ToInt32(dr["qty"]);
                    product.rate = Convert.ToInt32(dr["rate"]);
                }

            }
            return product;
        }

        public bool deleteProduct(int id)
        {
            var flag = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("sp_delete_pro", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productid", id);
                connection.Open();
                flag = cmd.ExecuteNonQuery();
                connection.Close();
            }
            if (flag > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool saveedited(int id, products product)
        {
            var flag = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("sp_save_edited", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productid", id);
                cmd.Parameters.AddWithValue("@productName", product.productName);
                cmd.Parameters.AddWithValue("@HSN", product.HSN);
                cmd.Parameters.AddWithValue("@mfg_com", product.mfg_com);
                cmd.Parameters.AddWithValue("@batch", product.batch);
                cmd.Parameters.AddWithValue("@expiry", product.expiry);
                cmd.Parameters.AddWithValue("@pkg", product.pkg);
                cmd.Parameters.AddWithValue("@qty", product.qty);
                cmd.Parameters.AddWithValue("@rate", product.rate);
                connection.Open();
                id = cmd.ExecuteNonQuery();
                connection.Close();

                if (id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}