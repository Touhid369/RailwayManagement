using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailwayManagement
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(this.txtUserId.Text) || String.IsNullOrEmpty(this.txtPassword.Text))
                {
                    MessageBox.Show("Please fill User Id and Password to continue");
                    return;
                }

                string sql = "select * from UserInfo where Id = '" + this.txtUserId.Text + "' and Password = '" + this.txtPassword.Text + "';";
                SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-1AAI0BE;Initial Catalog=RailwayManagementDB;Persist Security Info=True;User ID=sa;Password=touhid890;Encrypt=False");
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
                SqlDataAdapter sda = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                var name = ds.Tables[0].Rows[0][1].ToString();

                if (ds.Tables[0].Rows.Count == 1)
                {
                    this.Hide();
                    MessageBox.Show("Valid User");

                    if (ds.Tables[0].Rows[0][3].ToString().Equals("admin"))
                        new FormAdmin(name, this).Show();
                    else if (ds.Tables[0].Rows[0][3].ToString().Equals("member"))
                        new FormMember(name, this).Show();
                }
                else
                {
                    MessageBox.Show("Invalid User");
                }

                sqlcon.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("An error has occured");
            }

            //bool notFound = true;
            //int index = 0;
            //while(index < ds.Tables[0].Rows.Count)
            //{
            //    if (this.txtUserId.Text == ds.Tables[0].Rows[index][0].ToString() && this.txtPassword.Text == ds.Tables[0].Rows[index][2].ToString())
            //    {
            //        notFound = false;
            //        MessageBox.Show("Valid User");
            //        break;
            //    }
            //    //else
            //    //{
            //    //    MessageBox.Show("Invalid User");
            //    //}
            //    index++;
            //}
            //if(notFound)
            //    MessageBox.Show("Invalid User");


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtUserId.Clear();
            this.txtPassword.Text = "";
        }
    }
}
    

