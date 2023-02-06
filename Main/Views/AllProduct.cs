using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class AllProduct : Form
    {
        public AllProduct()
        {
            InitializeComponent();
            styleDategridview();
        }

        private void AllProduct_Load(object sender, EventArgs e)
        {
            DataConnection DB = new DataConnection();

            ProductCategoryComboBox.Items.Add("전체");

            foreach (Data CategoryDetail in DB.RetreiveAllCategoriesFromDatabase())
            {
                ProductCategoryComboBox.Items.Add(CategoryDetail.Name);
            }

            ProductCategoryComboBox.SelectedIndex = 0;
        }

        private void ProductCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductsGridView.Rows.Clear();

            if (ProductCategoryComboBox.SelectedIndex == 0)
            {
                DataConnection DB = new DataConnection();

                foreach (Data ProductDetail in DB.RetreiveAllProducts())
                {
                    ProductsGridView.Rows.Add(ProductDetail.ID, ProductDetail.Name, ProductDetail.Price, ProductDetail.Category, ProductDetail.Description, ProductDetail.Picture);
                }
            }
            else if (ProductCategoryComboBox.SelectedIndex > 0)
            {
                string CategoryName = ProductCategoryComboBox.SelectedItem.ToString();

                DataConnection DB = new DataConnection();

                int CategoryID = DB.ReturnCategoryID(CategoryName);

                foreach (Data ProductDetail in DB.RetreiveProductsFromCategory(CategoryID))
                {
                    ProductsGridView.Rows.Add(ProductDetail.ID, ProductDetail.Name, ProductDetail.Price, CategoryName, ProductDetail.Description, ProductDetail.Picture);
                }
            }
        }


        void styleDategridview()
        {
            ProductsGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            ProductsGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            ProductsGridView.DefaultCellStyle.SelectionBackColor = Color.White;
            ProductsGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            ProductsGridView.BackgroundColor = Color.White;
            ProductsGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            ProductsGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            ProductsGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            ProductsGridView.EnableHeadersVisualStyles = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
