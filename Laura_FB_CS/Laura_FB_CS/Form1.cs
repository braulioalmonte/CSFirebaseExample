using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.IO;


namespace Laura_FB_CS
{
    public partial class Form1 : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "0EBtjy1wAOJKF1oyXx6c5WFXGDCxYPIV193YCRAE",
            BasePath = "https://fir-cs-75c53.firebaseio.com/"

        };

        IFirebaseClient client;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            if(client != null)
            {
                MessageBox.Show("Connected to database");
            }


        }

        private async void insert_btn_Click(object sender, EventArgs e)
        {
            var car = new car
            {
                carID = Int32.Parse(textBox1.Text),
                plateName = textBox2.Text,
                brand = textBox3.Text,
                model = textBox4.Text,
                ownerName = textBox5.Text

            };

            SetResponse response = await client.SetAsync("Cars/" + textBox1.Text, car);
            car result = response.ResultAs<car>();
            MessageBox.Show("Record Inserted:" + result.carID);

        }

        private async void update_btn_Click(object sender, EventArgs e)
        {
            var car = new car
            {
                carID = Int32.Parse(textBox1.Text),
                plateName = textBox2.Text,
                brand = textBox3.Text,
                model = textBox4.Text,
                ownerName = textBox5.Text

            };

            FirebaseResponse response = await client.UpdateAsync("Cars/" + textBox1.Text, car);
            car result = response.ResultAs<car>();
            MessageBox.Show("Record Updated:" + result.carID);

        }

        private async void select_btn_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetAsync("Cars/" + textBox1.Text);
            car result = response.ResultAs<car>();
            textBox2.Text = result.plateName;
            textBox3.Text = result.brand;
            textBox4.Text = result.model;
            textBox5.Text = result.ownerName;


        }

        private async  void delete_btn_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.DeleteAsync("Cars/" + textBox1.Text);
            MessageBox.Show("Record Deleted");

        }
    }
}
