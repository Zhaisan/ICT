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
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();

            LoadContacts();
        }

        private BLL bll = default(BLL);
        private int curPage = 0;
        

        private void LoadContacts()
        {
            ContactDB contacts2 = new ContactDB();

            bll = new BLL(contacts2);
            UpdateDataGrid();

            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            CreateContactForm createContactForm = new CreateContactForm();
            if (createContactForm.ShowDialog() == DialogResult.OK)
            {
                CreateContactCommand command = new CreateContactCommand();
                command.Name = createContactForm.nameTxtBx.Text;
                command.Phone = createContactForm.phoneTxtBx.Text;
                command.Addr = createContactForm.addressTxtBx.Text;
                bll.CreateContact(command);
                UpdateDataGrid();
            }
        }

        private string searchingName = "";
        private void ShowDetail_Click(object sender, EventArgs e)
        {
            ContactDetail contactDetailForm = new ContactDetail();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                contactDetailForm.nameTxtBx.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                contactDetailForm.phoneTxtBx.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                contactDetailForm.addressTxtBx.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();


                switch (contactDetailForm.ShowDialog())
                {
                    case DialogResult.No:
                        bll.DeleteContact(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        break;
                    case DialogResult.Yes:
                        ContactDTO contact = new ContactDTO();
                        contact.Id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                        contact.Name = contactDetailForm.nameTxtBx.Text;
                        contact.Phone = contactDetailForm.phoneTxtBx.Text;
                        contact.Addr = contactDetailForm.addressTxtBx.Text;

                        bll.UpdateContact(contact);
                        break;
                    default:
                        break;
                }

                UpdateDataGrid();
            }

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            curPage = 0;
            searchingName = textBox1.Text;
            UpdateDataGrid();
        }

        private void PreviousBtn_Click(object sender, EventArgs e)
        {
            curPage = Math.Max(0, curPage - 5);
            UpdateDataGrid();
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            curPage = Math.Max(0, Math.Min(bll.GetContactsInPage(curPage, searchingName).Count, curPage + 5));
            UpdateDataGrid();
        }
        private void UpdateDataGrid()
        {
            bindingSource1.DataSource = bll.GetContactsInPage(curPage, searchingName);
        }




        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            //MessageBox.Show("ok");

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // MessageBox.Show("dataGridView1_RowsAdded");

        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindingSource1.DataSource = bll.sortingContacts();
        }
    }
}
