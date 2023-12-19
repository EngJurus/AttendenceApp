using Student_Attendence_App.DataSet1TableAdapters;
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
    public partial class MainFrm : Form
    {
        public int LogedIn { get; set; }
        public int UserID { get; set; }
        public MainFrm()
        {
            InitializeComponent();
            LogedIn = 0;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.AttendenceTbl' table. You can move, or remove it, as needed.
           // this.attendenceTblTableAdapter.Fill(this.dataSet1.AttendenceTbl);
            // TODO: This line of code loads data into the 'dataSet1.ClassTbl' table.

            // You can move, or remove it, as needed.
            //this.classTblTableAdapter.Fill(this.dataSet1.ClassTbl);
            //classTblBindingSource.Filter = "UserID = '" + UserID.ToString() + "'";  //Filter the classes.
            ////open the login form.
            //LoginFrm newLogin = new LoginFrm();
            //newLogin.ShowDialog();

            //if (newLogin.logiFlag == false)
            //{
            //    Close(); //also colose the main form and exit.
            //}
        }

        private void MainFrm_Activated(object sender, EventArgs e)
        {
            if (LogedIn == 0)
            {
                //open the login form.
                LoginFrm newLogin = new LoginFrm();
                newLogin.ShowDialog();

                if (newLogin.logiFlag == false)
                {

                    Close(); //also colose the main form and exit.
                }
                else
                {
                    UserID = newLogin.UserID;
                    stsLblUser.Text = UserID.ToString();
                    LogedIn = 1;
                    //filter the clasess related to this user!
                    this.classTblTableAdapter.Fill(this.dataSet1.ClassTbl);
                    classTblBindingSource.Filter = "UserID = '" + UserID.ToString() + "'";  //Filter the classes.

                }
            }
        }
        private void btnAddClass_Click(object sender, EventArgs e)
        {
            try
            {
                AddClassFrm addClass = new AddClassFrm();
                addClass.UserID = this.UserID;
                addClass.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                StudentFrm student = new StudentFrm();
                student.ClassName = comboBox1.Text;
                student.ClassID = (int)comboBox1.SelectedValue;
                student.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }


        private void GetRecords_Click(object sender, EventArgs e)
        {
            try
            {
                //Check if records exists, if yes load them for edit ...
                AttendenceTblTableAdapter ada = new AttendenceTblTableAdapter();
                DataTable dt = ada.GetDataBy((int)comboBox1.SelectedValue, dateTimePicker1.Text);

                if (dt.Rows.Count > 0)
                {
                    //we have recods , so we can edit them
                    // TODO: This line of code loads data into the 'dataSet1.AttendenceTbl' table. You can move, or remove it, as needed.
                    this.attendenceTblTableAdapter.Fill(this.dataSet1.AttendenceTbl);
                    // TODO: This line of code loads data into the 'dataSet1.ClassTbl' table.
                }
                else
                {
                    //create record for each student.
                    //get the class student list.
                    StudentTblTableAdapter std_adapter = new StudentTblTableAdapter();
                    DataTable std_da = std_adapter.GetDataByClassID((int)comboBox1.SelectedValue);

                    //go thorught by looping them.
                    foreach (DataRow row in std_da.Rows)
                    {
                        //insert a record fot this student.
                        ada.InsertQuery((int)row[0], row[1].ToString(), (int)(comboBox1.SelectedValue), comboBox1.Text, dateTimePicker1.Text, "");

                    }
                    DataTable dt_new = ada.GetDataBy((int)comboBox1.SelectedValue, dateTimePicker1.Text);
                    dataGridView1.DataSource = dt_new;
                }
            }
            catch(Exception ex)
            {

            }
           

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                AttendenceTblTableAdapter ada = new AttendenceTblTableAdapter();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[2].Value != null)
                    {
                        ada.UpdateQuery(row.Cells[2].Value.ToString(), row.Cells[1].Value.ToString(), (int)comboBox1.SelectedValue, dateTimePicker1.Text);
                    }


                }

                DataTable dt_new = ada.GetDataBy((int)comboBox1.SelectedValue, dateTimePicker1.Text);
                dataGridView1.DataSource = dt_new;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                AttendenceTblTableAdapter ada = new AttendenceTblTableAdapter();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[2].Value != null)
                    {
                        ada.UpdateQuery("", row.Cells[1].Value.ToString(), (int)comboBox1.SelectedValue, dateTimePicker1.Text);
                    }


                }

                DataTable dt_new = ada.GetDataBy((int)comboBox1.SelectedValue, dateTimePicker1.Text);
                dataGridView1.DataSource = dt_new;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRecods_Click(object sender, EventArgs e)
        {
            try
            {
                //get studens
                StudentTblTableAdapter std_adapter = new StudentTblTableAdapter();
                DataTable std_da = std_adapter.GetDataByClassID((int)comboBox2.SelectedValue);
                AttendenceTblTableAdapter ada = new AttendenceTblTableAdapter();

                int P = 0; //for perecent
                int A = 0; //for absent

                //loop throught students and...
                foreach (DataRow row in std_da.Rows)
                {
                    //precent
                    P = (int)ada.GetDataByReport(dateTimePicker2.Value.Month, row[1].ToString(), "Presnet").Rows[0][6];
                    //Absent
                    A = (int)ada.GetDataByReport(dateTimePicker2.Value.Month, row[1].ToString(), "Absent").Rows[0][6];

                    //add to listview item.
                    ListViewItem item = new ListViewItem();
                    item.Text = row[1].ToString();
                    item.SubItems.Add(P.ToString());
                    item.SubItems.Add(A.ToString());
                    listView1.Items.Add(item);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

            }

            //add to the lsitview
        

        private void btnReges_Click(object sender, EventArgs e)
        {
            try
            {
                RegesterFrm new_reg = new RegesterFrm();
                new_reg.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}

