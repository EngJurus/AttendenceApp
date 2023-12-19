using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Attendence_App
{
    public partial class RegesterFrm : Form
    {
        public RegesterFrm()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPass1.Text != txtPass2.Text)
                {
                    MessageBox.Show("Error! Paasword Mis-match!");
                    return; //continue your code.
                }

                DataSet1TableAdapters.UsersTblTableAdapter ada = new DataSet1TableAdapters.UsersTblTableAdapter();
                ada.InsertQuery(txtUser.Text, txtPass1.Text);
                MessageBox.Show("Regestraion Success!");
                Close(); //close the form.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
