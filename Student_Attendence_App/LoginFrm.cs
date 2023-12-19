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
    public partial class LoginFrm : Form
    {
        public bool logiFlag { get; set; }
        public int UserID { get; set; }
        public LoginFrm()
        {
            InitializeComponent();
            logiFlag = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet1TableAdapters.UsersTblTableAdapter userAda = new DataSet1TableAdapters.UsersTblTableAdapter();
                DataTable dt = userAda.GetDataByUserAndPass(txtUser.Text, txtPassword.Text);

                if (dt.Rows.Count > 0)
                {
                    //loging
                    MessageBox.Show("Login Success!");
                    UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
                    logiFlag = true;


                }
                else
                {
                    //invalid!
                    MessageBox.Show("Invaled Login!");
                    logiFlag = false;

                }
                Close(); //close the form.
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
