using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BioTemplate.Pages
{
    public partial class Pengeluaran : System.Web.UI.Page
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        DateTime dateMax = new DateTime(9999, 12, 31, 00, 00, 00);

        protected void Page_Load(object sender, EventArgs e)
        {
            con.ConnectionString = "Data Source=LAPTOP-LUGIMV8T;Initial Catalog=BioCash;User ID=sa;Password=@Gtabp1000";

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA='" + dateMax + "'", con);

            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                string result = myReader.GetValue(0).ToString();
                jmlhsaldo.Text = result.ToString();
            }
            con.Close();
            

            if (!IsPostBack)
            {
                gvbind();
                dlunit();
            }
        }

        protected void gvbind()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Pengeluaran WHERE ENDDA='" + dateMax + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBioCash.DataSource = ds;
                gvBioCash.DataBind();
            }
        }

        protected void dlunit()
        {
            SqlCommand cmd = new SqlCommand("SELECT Unit FROM biocash.Masterunit WHERE ENDDA='" + dateMax + "'", con); // table name 
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);  // fill dataset
            unitDl.DataTextField = ds.Tables[0].Columns["Unit"].ToString(); // text field name of table dispalyed in dropdown
            unitDl.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            unitDl.DataBind();  //binding dropdownlist
            //kaskeciledit.DataTextField = ds.Tables[0].Columns["Unit"].ToString(); // text field name of table dispalyed in dropdown
            //kaskeciledit.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            //kaskeciledit.DataBind();  //binding dropdownlist
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO biocash.Pengeluaran" + "(BEGDA,Unit,tgl_keluar,keterangan,jmlh_keluar,change_date,ENDDA)values(@BEGDA,@Unit,@tgl_keluar,@keterangan,@jmlh_keluar,@change_date,@ENDDA)", con);

            cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            cmd.Parameters.AddWithValue("@Unit", unitDl.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@tgl_keluar", tgl_keluar.Text);
            cmd.Parameters.AddWithValue("@keterangan", keperluan.Value);
            cmd.Parameters.AddWithValue("@jmlh_keluar", jmlhkeluar.Text);
            cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);


            con.Open();
            int check = cmd.ExecuteNonQuery();
            if (check > 0)
            {
                cmd.ExecuteNonQuery();
                string message = "Data berhasil disimpan.";
                string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                gvbind();
            }
            else if (check <= 0)
            {
                string message = "Data gagal disimpan.";
                string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "WarningMessage", script, true);
                return;
            }
            con.Close();
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {

        }

        protected void RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id_keluar = Convert.ToInt32(gvBioCash.DataKeys[e.RowIndex].Value.ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE biocash.Pengeluaran SET ENDDA=@ENDDA WHERE id_keluar='" + id_keluar + "'", con);
            cmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "Data berhasil dihapus.";
            string script = "window.onload = function(){ alert('"; script += message; script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            gvbind();
        }
    }
}