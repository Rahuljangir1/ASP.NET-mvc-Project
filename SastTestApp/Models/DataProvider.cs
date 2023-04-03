using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace SastTestApp.Models
{
    public class DataProvider
    {
        public bool IsUserExist(User user)
        {
            NpgsqlConnection conn = null;
            try
            {
                conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = postgres; User Id = postgres; Password = chi02@; Pooling = true; Maximum Pool Size = 10");
                conn.Open();
                string query = "select * from users where username=@name and password=@pw";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name",user.UserName);
                cmd.Parameters.AddWithValue("@pw", user.Password);
                NpgsqlDataReader rdr = cmd.ExecuteReader();
                if(rdr.HasRows)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception : " + ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close(); ;
                    conn.Dispose();
                    conn = null;
                }
            }
            return false;
        }
        public User getUserDetails(User user)
        {
            NpgsqlConnection conn = null;
            try
            {
                conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = postgres; User Id = postgres; Password = chi02@; Pooling = true; Maximum Pool Size = 10");
                conn.Open();
                string query = "select * from users where username=@name and password=@pw";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", user.UserName);
                cmd.Parameters.AddWithValue("@pw", user.Password);
                NpgsqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    user.Email = rdr.GetString(1);
                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception : " + ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close(); ;
                    conn.Dispose();
                    conn = null;
                }
            }
            return user;
        }
        public User saveUser(User user)
        {
            NpgsqlConnection conn = null;
            try
            {
                conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = postgres; User Id = postgres; Password = chi02@; Pooling = true; Maximum Pool Size = 10");
                conn.Open();
                string query = "insert into users values(@name,@email,@pw)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", user.UserName);
                cmd.Parameters.AddWithValue("@pw", user.Password);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.ExecuteNonQuery();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception : " + ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close(); ;
                    conn.Dispose();
                    conn = null;
                }
            }
            return null;
        }
    }
}