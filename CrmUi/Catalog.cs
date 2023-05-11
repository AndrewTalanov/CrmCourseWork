using CrmBL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class Catalog<T> : Form
        where T : class
    {
        CrmContext db;
        DbSet<T> set;

        public Catalog(DbSet<T> set, CrmContext db)
        {
            InitializeComponent();
            this.db = db;
            this.set = set;
            set.Load();
            dataGridView.DataSource = set.Local.ToBindingList();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // добавление
        private void button1_Click(object sender, EventArgs e)
        {
            if(typeof(T) == typeof(Product))
            {
                var form = new ProductForm();

                if (form.ShowDialog() == DialogResult.OK)
                {
                    db.Products.Add(form.Product);
                    db.SaveChanges();
                    dataGridView.Update();
                }
            } else if (typeof(T) == typeof(Seller))
            {
                var form = new SellerForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    db.Sellers.Add(form.Seller);
                    db.SaveChanges();
                    dataGridView.Update();
                }

            } else if (typeof(T) == typeof(Customer))
            {
                var form = new CustomerForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    db.Customers.Add(form.Customer);
                    db.SaveChanges();
                    dataGridView.Update();
                }
            }
        }

        private void Catalog_Load(object sender, EventArgs e)
        {

        }

        // редактирование
        private void button2_Click(object sender, EventArgs e)
        {
            var id = dataGridView.SelectedRows[0].Cells[0].Value;

            if (typeof(T) == typeof(Product))
            {
                var product = set.Find(id) as Product;
                if (product != null)
                {
                    var form = new ProductForm(product);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        product.Name = form.Product.Name;
                        product.Price = form.Product.Price;
                        product.Count = form.Product.Count;
                        db.SaveChanges();
                        dataGridView.Update();
                    }
                }
            }
            else if (typeof(T) == typeof(Seller))
            {
                var seller = set.Find(id) as Seller;
                if (seller != null)
                {
                    var form = new SellerForm(seller);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        // seller = form.Seller;
                        seller.Name = form.Seller.Name;

                        db.SaveChanges();
                        dataGridView.Update();
                    }
                }
            }
            else if (typeof(T) == typeof(Customer))
            {
                var customer = set.Find(id) as Customer;
                if (customer != null)
                {
                    var form = new CustomerForm(customer);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        // customer = form.Customer;
                        customer.Name = form.Customer.Name;
                        db.SaveChanges();
                        dataGridView.Update();
                    }
                }
            }
        }

        // удаление
        private void button3_Click(object sender, EventArgs e)
        {

            var id = dataGridView.SelectedRows[0].Cells[0].Value;

            if (typeof(T) == typeof(Product))
            {
                var product = set.Find(id) as Product;
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                    dataGridView.Update();
                }
            }
            else if (typeof(T) == typeof(Seller))
            {
                var seller = set.Find(id) as Seller;
                if (seller != null)
                {
                    db.Sellers.Remove(seller);
                    db.SaveChanges();
                    dataGridView.Update();
                    /*var form = new SellerForm(seller);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        seller = form.Seller;
                        db.SaveChanges();
                        dataGridView.Update();
                    }*/
                }
            }
            else if (typeof(T) == typeof(Customer))
            {
                var customer = set.Find(id) as Customer;
                if (customer != null)
                {
                    db.Customers.Remove(customer);
                    db.SaveChanges();
                    dataGridView.Update();
                    /*var form = new CustomerForm(customer);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        customer = form.Customer;
                        db.SaveChanges();
                        dataGridView.Update();
                    }*/
                }
            }
        }
    }
}
