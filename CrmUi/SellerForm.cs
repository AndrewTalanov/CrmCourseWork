using CrmBL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class SellerForm : Form
    {
        public Seller Seller { get; set; }
        public SellerForm()
        {
            InitializeComponent();
        }
        public SellerForm(Seller seller) : this()
        {
            Seller = seller;
            textBox1.Text = seller.Name;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Seller == null)
            {
                Seller = new Seller();
            }

            var s = Seller ?? new Seller();

            s.Name = textBox1.Text;
            
            Close();
        }

        private void SellerForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
