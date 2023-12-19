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
    public partial class AddClassFrm : Form
    {
        public int UserID { get; set; }
        public AddClassFrm()
        {
            InitializeComponent();
        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            DataSet1TableAdapters.ClassTblTableAdapter ada = new DataSet1TableAdapters.ClassTblTableAdapter();
            ada.AddClass(txtClassName.Text, UserID);
            Close();
        }
    }
}
