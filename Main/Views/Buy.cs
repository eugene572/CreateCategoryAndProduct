﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.Views
{
    public partial class Buy : Form
    {
        public Buy()
        {
            InitializeComponent();
        }

        private void CashGivenBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CashReturnBox.Text = (Convert.ToDecimal(CashGivenBox.Text) - Convert.ToDecimal(TotalBillBox.Text)).ToString();
            }
            catch
            {
                CashReturnBox.Text = string.Empty;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

   
}
