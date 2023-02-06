using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using Main.Views;

namespace Main
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
            
            Timer t = new Timer();
            t.Tick += new EventHandler(timer_Tick);
            t.Start();
            styleDategridview();
            DataConnection DB = new DataConnection();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string datepart = dt.ToString("yyyy-MM-dd  hh:mm:ss");
            label7.Text = datepart;
        }
        public int RowIndex = 0;
        public int number = 0;
        private void main_Load(object sender, EventArgs e)
        {
            DataConnection DB = new DataConnection();

            ArrayList AllCategories = DB.RetreiveAllCategoriesFromDatabase();

            foreach (Data Category in AllCategories)
            {
                Button btn = new Button();
                btn.Text = Category.Name;
                btn.Size = new System.Drawing.Size(80,80);
                btn.ForeColor = Color.Black;
                btn.FlatStyle = FlatStyle.Flat;

                btn.Padding = new System.Windows.Forms.Padding(10);
                btn.Tag = Category.ID;

                CategoriesFlowPanel.Controls.Add(btn);

                btn.Click += CategoryButtonClick;
            }
        }

        void CategoryButtonClick(object sender, EventArgs e)
        {
            ProductsFlowPanel.Controls.Clear();

            Button btn = (Button)sender;

            int CategoryID = Convert.ToInt32(btn.Tag);

            DataConnection DB = new DataConnection();

            foreach (Data Product in DB.RetreiveProductsFromCategory(CategoryID))
            {
                Button ProductButton = new Button();

                ProductButton.Size = new System.Drawing.Size(100,100);
                ProductButton.ForeColor = Color.White;
                
                ProductButton.FlatStyle = FlatStyle.Flat;
                MemoryStream ms = new MemoryStream(Product.Picture);
                ProductButton.Image = Image.FromStream(ms);
                ProductButton.Image = new Bitmap(ProductButton.Image, ProductButton.Size);

                ProductButton.Tag = Product.ID;

                ProductsFlowPanel.Controls.Add(ProductButton);

                ProductButton.Click += ProductButton_Click;

                
            }
        }



        void ProductButton_Click(object sender, EventArgs e)
        {
            Button ProductButton = sender as Button;

            DataConnection DB = new DataConnection();

            int ProductID = Convert.ToInt32(ProductButton.Tag);

            Data ProductDetails = DB.RetreiveProductDetails(ProductID);

            if (CheckProductAlreadyAdded(ProductID))
            {
                int Quantity = Convert.ToInt32(ProductsGridView.Rows[RowIndex].Cells["ProductQuantityColumn"].Value);
                decimal Price = Convert.ToInt32(ProductsGridView.Rows[RowIndex].Cells["ProductPriceColumn"].Value);


                Quantity++;

                double TotalPrice = Convert.ToDouble(Quantity * Price);


                ProductsGridView.Rows[RowIndex].Cells["ProductQuantityColumn"].Value = Quantity;
                ProductsGridView.Rows[RowIndex].Cells["TotalPriceColumn"].Value = TotalPrice;

                TotalBillBox.Text = CalculateTotalBill(ProductsGridView).ToString();
            }
            else
            {
                ProductsGridView.Rows.Add(ProductID, ProductDetails.Name, ProductDetails.Price, 1, ProductDetails.Price * 1);

                TotalBillBox.Text = CalculateTotalBill(ProductsGridView).ToString();
            }
        }

        public bool CheckProductAlreadyAdded(int ProductID)
        {
            foreach (DataGridViewRow Row in ProductsGridView.Rows)
            {
                if (Convert.ToInt32(Row.Cells["ProductIDColumn"].Value) == ProductID)
                {
                    RowIndex = Row.Index;
                    return true;
                }
            }
            return false;
        }

        private void addCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCategory AC = new AddCategory();


            if (AC.ShowDialog() == DialogResult.OK)
            {
                CategoriesFlowPanel.Controls.Clear();

                DataConnection DB = new DataConnection();

                ArrayList AllCategories = DB.RetreiveAllCategoriesFromDatabase();

                foreach (Data Category in AllCategories)
                {
                    Button btn = new Button();
                    btn.Text = Category.Name;
                    btn.Size = new System.Drawing.Size(80, 80);
                    btn.ForeColor = Color.Black;


                    btn.Tag = Category.ID;

                    CategoriesFlowPanel.Controls.Add(btn);

                    btn.Click += CategoryButtonClick;
                }
            }
        }

        public decimal CalculateTotalBill(DataGridView ProductsGridView)
        {
            decimal TotalBill = 0;

            foreach (DataGridViewRow Row in ProductsGridView.Rows)
            {
                decimal ProductTotal = Convert.ToDecimal(Row.Cells["TotalPriceColumn"].Value);

                TotalBill = TotalBill + ProductTotal;
            }

            return TotalBill;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            AddCategory AC = new AddCategory();



            if (AC.ShowDialog() == DialogResult.OK)
            {

                CategoriesFlowPanel.Controls.Clear();

                DataConnection DB = new DataConnection();

                ArrayList AllCategories = DB.RetreiveAllCategoriesFromDatabase();

                foreach (Data Category in AllCategories)
                {
                    Button btn = new Button();
                    btn.Text = Category.Name;
                    btn.Size = new System.Drawing.Size(80, 80);
                    btn.ForeColor = Color.Black;


                    btn.Tag = Category.ID;

                    CategoriesFlowPanel.Controls.Add(btn);

                    btn.Click += CategoryButtonClick;

                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            AddProduct a = new AddProduct();
            a.ShowDialog();
        }

        private void CheckOutButton_Click(object sender, EventArgs e)
        {
            Buy buy = new Buy();

            buy.TotalBillBox.Text = TotalBillBox.Text;

            if (buy.ShowDialog() == DialogResult.OK)
            {
  
                DataConnection DB = new DataConnection();

                if (DB.AddSaleDB(DateTime.Now, Convert.ToDecimal(buy.CashGivenBox.Text), Convert.ToDecimal(buy.TotalBillBox.Text), Convert.ToDecimal(buy.CashReturnBox.Text)))
                {
                    
                    MessageBox.Show("결제를 완료하였습니다.");
                    number++;
                    label5.Text = "" + number;
                }
                else MessageBox.Show("결제를 할 수 없습니다.");
            }
        
    }

        private void label3_Click(object sender, EventArgs e)
        {
            SalesHistory sh = new SalesHistory();
            sh.Show();
        }

        

        private void label6_Click(object sender, EventArgs e)
        {
            AllProduct al = new AllProduct();
            al.Show();
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

        private void button1_Click(object sender, EventArgs e)
        {
            ProductsGridView.Rows.Clear();
            TotalBillBox.Text = "";
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
