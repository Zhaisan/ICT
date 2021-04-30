using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example2
{
    public partial class ContactDetail : Form
    {
        public ContactDetail()
        {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void EditBtn_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }
    }
}
