using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Турникет
{
    public partial class Информация_о_разработке : Form
    {
        public Информация_о_разработке()
        {
            InitializeComponent();
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
        }


        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрывает текущую форму
        }

    }
}