using MVCexample.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCexample.Services
{
    public class GuestbookService
    {
        private readonly string cnstr = ConfigurationManager.ConnectionStrings["Guestbook"].ConnectionString;

        public List<Gbook> GetAllGuestbooks()
        {
            List<Gbook> result = new List<Gbook>();
            string sql = "SELECT * FROM Guestbook";
            using (SqlConnection conn = new SqlConnection(cnstr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Gbook gbook = new Gbook();
                        gbook.Id = Convert.ToInt32(rd[0]);
                        gbook.Name = rd[1].ToString();
                        gbook.Content = rd[2].ToString();
                        gbook.CreateTime = Convert.ToDateTime(rd[3]);
                        gbook.Reply = rd[4].ToString();
                        gbook.ReplyTime = Convert.ToDateTime((string.IsNullOrEmpty(rd[5].ToString())) ? DateTime.Now : rd[5]);
                        result.Add(gbook);
                    }
                }
                conn.Close();
            }
            return result;
        }

        public void InsertNewGuestbook(Gbook Data)
        {
            Data.CreateTime = DateTime.Now;
            string sql = "INSERT INTO Guestbook(Name,Content,CreateTime) VALUES ('" + Data.Name + "','" + Data.Content + "','" + Data.CreateTime.ToString("yyyy/MM/dd HH:mm:ss") + "')";
            using (SqlConnection conn = new SqlConnection(cnstr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public Gbook GetDataById(int Id)
        {
            Gbook SearchData = new Gbook();
            string sql = "SELECT * FROM Guestbook WHERE Id=" + Id;
            using (SqlConnection conn = new SqlConnection(cnstr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        SearchData.Id = Convert.ToInt32(rd[0]);
                        SearchData.Name = rd[1].ToString();
                        SearchData.Content = rd[2].ToString();
                        SearchData.CreateTime = Convert.ToDateTime(rd[3]);
                        SearchData.Reply = rd[4].ToString();
                        SearchData.ReplyTime = Convert.ToDateTime((string.IsNullOrEmpty(rd[5].ToString())) ? DateTime.Now : rd[5]);
                    }
                }
            }

            return SearchData;
        }

        public void EditGuestbook(Gbook EditData)
        {
            if (string.IsNullOrEmpty(EditData.ReplyTime.ToString()))
            {
                string sql = "UPDATE Guestbook SET Name='" + EditData.Name + "',Content='" + EditData.Content + "' WHERE Id=" + EditData.Id;
                using (SqlConnection conn = new SqlConnection(cnstr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            else
            {
                return;
            }
        }

        public void ReplyGuestbook(Gbook ReplyData)
        {
            if (string.IsNullOrEmpty(ReplyData.ReplyTime.ToString()))
            {
                ReplyData.ReplyTime = DateTime.Now;
                string sql = "UPDATE Guestbook SET Reply='" + ReplyData.Reply + "',ReplyTime ='" + ReplyData.ReplyTime.ToString("yyyy/MM/dd HH:mm:ss") + "' WHERE Id=" + ReplyData.Id;
                using (SqlConnection conn = new SqlConnection(cnstr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            else
            {
                return;
            }
        }


        public void DeleteGuestbook(int id)
        {
            string sql = "DELETE FROM Guestbook WHERE Id=" + id;
            using (SqlConnection conn = new SqlConnection(cnstr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}