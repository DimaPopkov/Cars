using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Formula1
{
    public partial class Form1 : Form
    {

        public static List<cars> carsList = new List<cars>();


        public Form1()
        {

            List<string> products = Program.Select("SELECT * FROM Products");


            for (int i = 0; i < products.Count; i += 5)
                carsList.Add(new Product(products[i], DateTime.Parse(products[i + 1]), int.Parse(products[i + 2]), products[i + 3], int.Parse(products[i + 4])));

            InitializeComponent();



            
        }

        public static MySqlConnection conn;
        public static List<string> Select(string text)
        {
            conn.Open();

            List<string> results = new List<string>();
            MySqlCommand command = new MySqlCommand(text, conn);

            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    results.Add(reader.GetValue(i).ToString());
            }
            reader.Close();

            conn.Close();

            return results;
        }



        String connString =
            "Server = VH287.spaceweb.ru; Database = beavisabra_cars;" +
            "port = 3306; User Id = beavisabra_cars; password = Beavis1989";

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM Cars", conn);

            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                carsList.Add(new Car(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetDateTime(3)));
            }


            conn.Close();

            List<string> dead = Program.Select("SELECT * FROM Products" +
                " WHERE DATEDIFF(CURDATE(), DateBegin) > LifeTime");







        }

    }
}

