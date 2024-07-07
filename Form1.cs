using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace code
{
    public partial class InfoForm : Form
    {
        string id = "";
        string searchInterest;
        public InfoForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.Table1' table. You can move, or remove it, as needed.
            this.table1TableAdapter.Fill(this.appData.Table1);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (firstName.Text != "" && lastName.Text != "" && NationalID.Text != "" &&
                phoneNumber.Text != "" && address.Text != "" && Job.Text != "")
                {
                    OleDbConnection co = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Semmester - 6\DB Lab\Code\code\code\infoDB.accdb");
                    OleDbCommand com = new OleDbCommand("INSERT INTO Table1 (firstName, lastName, NationalID, phoneNumber, address, Job) VALUES (@fn, @ln, @ni, @pn, @ad, @jo)", co);

                    com.Parameters.Add("@fn", firstName.Text);
                    com.Parameters.Add("@ln", lastName.Text);
                    com.Parameters.Add("@ni", NationalID.Text);
                    com.Parameters.Add("@pn", phoneNumber.Text);
                    com.Parameters.Add("@ad", address.Text);
                    com.Parameters.Add("@jo", Job.Text);

                    co.Open();
                    com.ExecuteNonQuery();
                    co.Close();

                    OleDbDataAdapter da = new OleDbDataAdapter("select * from Table1", co);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewN.DataSource = dt;
                    dataGridViewE.DataSource = dt;

                    firstName.Text = "";
                    lastName.Text = "";
                    NationalID.Text = "";
                    phoneNumber.Text = "";
                    address.Text = "";
                    Job.Text = "";
                }
                else
                {
                    MessageBox.Show("Please enter your information completely!");
                }
            }
            catch
            {
                MessageBox.Show("Use correct type of data!");
                return;
            }
        }

        private void editB_Click(object sender, EventArgs e)
        {
            groupBoxN.Visible = false;
            groupBoxE.Visible = true;
            firstName.Text = "";
            lastName.Text = "";
            NationalID.Text = "";
            phoneNumber.Text = "";
            address.Text = "";
            Job.Text = "";

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cancelB_Click(object sender, EventArgs e)
        {
            OleDbConnection co = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Semmester - 6\DB Lab\Code\code\code\infoDB.accdb");
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Table1", co);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridViewE.DataSource = dt;
            groupBoxN.Visible = true;
            groupBoxE.Visible = false;
            firstName.Text = "";
            lastName.Text = "";
            NationalID.Text = "";
            phoneNumber.Text = "";
            address.Text = "";
            Job.Text = "";
            searchBox.Text = "";
        }

        private void dataGridViewE_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void searchB_Click(object sender, EventArgs e)
        {
            OleDbConnection co = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Semmester - 6\DB Lab\Code\code\code\infoDB.accdb");
            if (searchByCombo.Text.ToString() == "National ID") {
                searchInterest = "NationalID";
            }
            else if (searchByCombo.Text.ToString() == "Last Name")
            {
                searchInterest = ""; 
            }
            if (string.IsNullOrEmpty(searchBox.Text.Trim()))
            {
                MessageBox.Show("Please fill the search bar!");
            }
            
            OleDbDataAdapter da = new OleDbDataAdapter($"select * from Table1 where {searchInterest} =  ?", co);
            da.SelectCommand.Parameters.AddWithValue("?", searchBox.Text);
            DataTable dt = new DataTable();

            int BoxCount = searchBox.Text.ToString().Trim().Length;
            if (BoxCount > 0)
            {
                try
                {
                    da.Fill(dt);
                    dataGridViewE.DataSource = dt;
                    id = dataGridViewE.SelectedRows[0].Cells[0].Value.ToString();
                    firstName.Text = dataGridViewE.SelectedRows[0].Cells[1].Value.ToString();
                    lastName.Text = dataGridViewE.SelectedRows[0].Cells[2].Value.ToString();
                    NationalID.Text = dataGridViewE.SelectedRows[0].Cells[3].Value.ToString();
                    phoneNumber.Text = dataGridViewE.SelectedRows[0].Cells[4].Value.ToString();
                    address.Text = dataGridViewE.SelectedRows[0].Cells[5].Value.ToString();
                    Job.Text = dataGridViewE.SelectedRows[0].Cells[6].Value.ToString();
                }
                catch
                {
                    MessageBox.Show("Not found!");
                }
            }
            else
            {
                MessageBox.Show("Please fill the seach bar!");
            }

        }

        private void dataGridViewE_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = dataGridViewE.SelectedRows[0].Cells[0].Value.ToString();
            firstName.Text = dataGridViewE.SelectedRows[0].Cells[1].Value.ToString();
            lastName.Text = dataGridViewE.SelectedRows[0].Cells[2].Value.ToString();
            NationalID.Text = dataGridViewE.SelectedRows[0].Cells[3].Value.ToString();
            phoneNumber.Text = dataGridViewE.SelectedRows[0].Cells[4].Value.ToString();
            address.Text = dataGridViewE.SelectedRows[0].Cells[5].Value.ToString();
            Job.Text = dataGridViewE.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void finalSaveB_Click(object sender, EventArgs e)
        {
            try
            {
                if (firstName.Text != "" && lastName.Text != "" && NationalID.Text != "" &&
                phoneNumber.Text != "" && address.Text != "" && Job.Text != "" && id != "")
                {
                    OleDbConnection co = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Semmester - 6\DB Lab\Code\code\code\infoDB.accdb");
                    OleDbCommand com = new OleDbCommand("update Table1 set firstName = @fn, lastName = @ln, NationalID = @ni, phoneNumber = @pn, address = @ad, Job = @jo where ID = @id", co);

                    com.Parameters.Add("@fn", firstName.Text);
                    com.Parameters.Add("@ln", lastName.Text);
                    com.Parameters.Add("@ni", NationalID.Text);
                    com.Parameters.Add("@pn", phoneNumber.Text);
                    com.Parameters.Add("@ad", address.Text);
                    com.Parameters.Add("@jo", Job.Text);
                    com.Parameters.Add("@id", id);

                    co.Open();
                    com.ExecuteNonQuery();
                    co.Close();

                    OleDbDataAdapter da = new OleDbDataAdapter("select * from Table1", co);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewN.DataSource = dt;
                    dataGridViewE.DataSource = dt;

                    firstName.Text = "";
                    lastName.Text = "";
                    NationalID.Text = "";
                    phoneNumber.Text = "";
                    address.Text = "";
                    Job.Text = "";
                }

                else
                {
                    MessageBox.Show("Please fill all of the fields!");
                }
            }
            catch
            {
                MessageBox.Show("Use correct type of data!");
                return;
            }
        }

        private void deleteB_Click(object sender, EventArgs e)
        {
            if (id != "")
            {
                OleDbConnection co = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Semmester - 6\DB Lab\Code\code\code\infoDB.accdb");
                OleDbCommand com = new OleDbCommand("DELETE FROM Table1 WHERE ID = @id", co);

                com.Parameters.Add("@id", id);

                co.Open();
                com.ExecuteNonQuery();
                co.Close();

                OleDbDataAdapter da = new OleDbDataAdapter("select * from Table1", co);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewN.DataSource = dt;
                dataGridViewE.DataSource = dt;

                firstName.Text = "";
                lastName.Text = "";
                NationalID.Text = "";
                phoneNumber.Text = "";
                address.Text = "";
                Job.Text = "";
                id = "";
            }
            else
            {
                MessageBox.Show("Please select a record to delete!");
            }
        }

        private void groupBoxE_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
