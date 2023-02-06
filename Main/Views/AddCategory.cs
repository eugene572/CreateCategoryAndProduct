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
    public partial class AddCategory : Form
    {
        public AddCategory()
        {
            InitializeComponent();
        }

        private void AddCategory_Load(object sender, EventArgs e)
        {

        }




        private void AddCategoryButton_Click_1(object sender, EventArgs e)
        {
            DataConnection DB = new DataConnection();

            bool CategoryAddedOrNot = DB.AddCategoryDB(CategoryNameBox.Text, CategoryDescriptionRBox.Text);

            if (CategoryAddedOrNot)
            {
                MessageBox.Show("카테고리를 추가하였습니다.");

                this.DialogResult = DialogResult.OK;
            }
            else MessageBox.Show("카테고리를 추가할 수 없습니다.");
        }


        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
