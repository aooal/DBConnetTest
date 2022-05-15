using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace DBConnetTest.Models
{
    public class DBmanager
    {
        private readonly string ConnStr = "Data Source=.;Initial Catalog=Test;Integrated Security=True";
    
        public List<Card> QueryByKeyWorld(string keyworld)
        {
            List<Card> cards = new List<Card>();
            SqlConnection con = new SqlConnection(ConnStr);
            SqlCommand cmd = new SqlCommand("select * from card where char_name like @keyworld " +
                "or card_name like @keyworld or card_level like @keyworld");
            cmd.Connection = con;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@keyworld", "%"+keyworld+"%"));

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Card card = new Card
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("id")),
                        Char_name = reader.GetString(reader.GetOrdinal("char_name")),
                        Card_name = reader.GetString(reader.GetOrdinal("card_name")),
                        Card_level = reader.GetString(reader.GetOrdinal("card_level")),
                    };
                    cards.Add(card);
                }
            }
            else
            {
                Console.WriteLine("資料不存在");
            }
            con.Close();
            return cards;
        }
        public List<Card> GetCards()
        {
            List<Card> cards = new List<Card>();
            SqlConnection con = new SqlConnection(ConnStr);
            SqlCommand cmd = new SqlCommand("select * from card;");
            cmd.Connection = con;
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Card card = new Card
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("id")),
                        Char_name = reader.GetString(reader.GetOrdinal("char_name")),
                        Card_name = reader.GetString(reader.GetOrdinal("card_name")),
                        Card_level = reader.GetString(reader.GetOrdinal("card_level")),
                    };
                    cards.Add(card);
                }
            }
            else
            {
                Console.WriteLine("資料不存在");
            }
            con.Close();
            return cards;
        }
        public void NewCard(Card card)
        {
            SqlConnection con = new SqlConnection(ConnStr);
            SqlCommand cmd = new SqlCommand("insert into card(char_name,card_name," +
                "card_level) values (@char_name,@card_name,@card_level)");
            cmd.Connection = con;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@char_name", card.Char_name));
            cmd.Parameters.Add(new SqlParameter("@card_name", card.Card_name));
            cmd.Parameters.Add(new SqlParameter("@card_level", card.Card_level));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public Card GetCardById(int id)
        {
            Card card = new Card();
            SqlConnection con = new SqlConnection(ConnStr);
            SqlCommand cmd = new SqlCommand("select * from card where id = @id");
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    card = new Card
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("id")),
                        Char_name = reader.GetString(reader.GetOrdinal("char_name")),
                        Card_name = reader.GetString(reader.GetOrdinal("card_name")),
                        Card_level = reader.GetString(reader.GetOrdinal("card_level")),
                    };
                }
            }
            else
            {
                card.Char_name = "資料不存在";
            }
            con.Close();
            return card;
        }
        public void UpdateCard(Card card)
        {
            SqlConnection con = new SqlConnection(ConnStr);
            SqlCommand cmd = new SqlCommand("update card set char_name= @char_name," +
                "card_name = @card_name,card_level=@card_level where id = @id");
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@char_name", card.Char_name));
            cmd.Parameters.Add(new SqlParameter("@card_name", card.Card_name));
            cmd.Parameters.Add(new SqlParameter("@card_level", card.Card_level));
            cmd.Parameters.Add(new SqlParameter("@id", card.ID));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void DeleteCardById(int id)
        {
            SqlConnection con = new SqlConnection(ConnStr);
            SqlCommand cmd = new SqlCommand("delete from card where id = @id");
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}