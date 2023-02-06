using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Main
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
            DataConnection DB = new DataConnection();
            ProductCategoryComboBox.Text = "카테고리 선택";

        }

        
        private void AddProduct_Load(object sender, EventArgs e)
        {
            DataConnection DB = new DataConnection();

            foreach (Data Category in DB.RetreiveAllCategoriesFromDatabase())
            {
                ProductCategoryComboBox.Items.Add(Category.Name);
            }

            

        }

        

        private void UploadPictureButton_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select Image file..";
            ofd.DefaultExt = ".jpg";
            ofd.Filter = "Media Files|*.jpg;*.png;*.gif;*.bmp;*.jpeg|All Files|*.*";

            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                ProductPictureBox.Load(ofd.FileName);
            }
        }

        private void AddProductButton_Click_1(object sender, EventArgs e)
        {
            DataConnection DB = new DataConnection();

            MemoryStream ms = new MemoryStream();

            ProductPictureBox.Image.Save(ms, ProductPictureBox.Image.RawFormat);

            byte[] ProductPicture = ms.GetBuffer();

            ms.Close();



            if (DB.AddProductDB(ProductNameBox.Text, Convert.ToDecimal(ProductPriceBox.Text), DB.ReturnCategoryID(ProductCategoryComboBox.SelectedItem.ToString()), ProductDescriptionRBox.Text, ProductPicture))
            {
                MessageBox.Show("상품을 추가하였습니다.");
                

                
            }
            else MessageBox.Show("상품을 추가할 수 없습니다.");
        }

      


        private void AddProduct_Load_1(object sender, EventArgs e)
        {
            DataConnection DB = new DataConnection();

            foreach (Data Category in DB.RetreiveAllCategoriesFromDatabase())
            {
                ProductCategoryComboBox.Items.Add(Category.Name);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
