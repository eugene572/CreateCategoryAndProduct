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
    public partial class SalesHistory : Form
    {
        public SalesHistory()
        {
            InitializeComponent();
            styleDategridview();
        }

        private void SalesHistory_Load(object sender, EventArgs e)
        {
            DataConnection DB = new DataConnection();

            foreach (Data SaleDetails in DB.RetreiveAllSales())
            {
                dataGridView1.Rows.Add(SaleDetails.SaleID, SaleDetails.SaleTime, Math.Truncate(SaleDetails.Retrun*100)/100, Math.Truncate(SaleDetails.Given * 100) / 100, Math.Truncate(SaleDetails.Total * 100) / 100);
            }
        }

        void styleDategridview()
        {
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
