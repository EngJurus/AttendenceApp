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
    public partial class StudentFrm : Form
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public StudentFrm()
        {
            InitializeComponent();
        }

        private void StudentFrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.StudentTbl' table. You can move, or remove it, as needed.
            this.studentTblTableAdapter.Fill(this.dataSet1.StudentTbl);
            lblClassID.Text = ClassID.ToString();
            lblClassName.Text = ClassName;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //insert or update.
                this.studentTblBindingSource.EndEdit();
                this.studentTblTableAdapter.Update(dataSet1.StudentTbl);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }
    }
}
