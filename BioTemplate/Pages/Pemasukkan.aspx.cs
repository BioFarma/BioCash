using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BioTemplate.Pages
{
    public partial class Pemasukkan : System.Web.UI.Page
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            con.ConnectionString = "Data Source=LAPTOP-LUGIMV8T;Initial Catalog=BioCash;User ID=sa;Password=@Gtabp1000";
            
            if (!IsPostBack)
            {
                dlunit();
                gvbind();
            }

        }

        protected void dlunit()
        {
            SqlCommand cmd = new SqlCommand("SELECT Unit FROM biocash.Masterunit", con); // table name 
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);  // fill dataset
            kas_kecil.DataTextField = ds.Tables[0].Columns["Unit"].ToString(); // text field name of table dispalyed in dropdown
            kas_kecil.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            kas_kecil.DataBind();  //binding dropdownlist
        }

        protected void gvbind()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT tgl_masuk,thn_periode,jmlh_masuk,kas_kecil FROM biocash.Pemasukkan", con);
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


        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (kas_kecil.Text == "")
            {
                string message = "Kas kecil harus diisi";
                string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            } else if (Masuk.Text == "")
            {
                string message = "Jumlah pemasukkan harus diisi";
                string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            } else if (tgl_masuk.Text == "")
            {
                string message = "Tanggal pemasukkan harus dipilih";
                string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            } else if (periode_masuk.Text == "")
            {
                string message = "Tahun periode harus diisi";
                string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            } else { 

            SqlCommand cmd = new SqlCommand("insert into biocash.pemasukkan" + "(BEGDA,kas_kecil,tgl_masuk,thn_periode,jmlh_masuk,change_date)values(@BEGDA,@kas_kecil,@tgl_masuk,@thn_periode,@jmlh_masuk,@change_date)", con);

            cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            cmd.Parameters.AddWithValue("@kas_kecil",kas_kecil.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@tgl_masuk", tgl_masuk.Text);
            cmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
            cmd.Parameters.AddWithValue("@jmlh_masuk", Masuk.Text);
            cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            cmd.ExecuteNonQuery();
            con.Close();
            Masuk.Text = String.Empty;
            tgl_masuk.Text = String.Empty;
            periode_masuk.Text = String.Empty;
            string message = "Your details have been saved successfully.";
            string script = "window.onload = function(){ alert('";script += message; script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                gvbind();
            }

        }

        protected void RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvBioCash.EditIndex = e.NewEditIndex;
            gvbind();
        }

        protected void RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            //Finding the controls from Gridview for the row which is going to update  
            string tgl_masuk = gvBioCash.DataKeys[e.RowIndex].Value.ToString();
            TextBox tgledit = (TextBox)gvBioCash.Rows[e.RowIndex].FindControl("tgledit");
            TextBox thnedit = (TextBox)gvBioCash.Rows[e.RowIndex].FindControl("thnedit");
            TextBox jmlhedit = (TextBox)gvBioCash.Rows[e.RowIndex].FindControl("jmlhedit");
            string unitedit = (gvBioCash.Rows[e.RowIndex].FindControl("unitedit") as DropDownList).SelectedItem.Value;
            con.Open();
            //updating the record  
            SqlCommand cmd = new SqlCommand("UPDATE biocash.pemasukkan SET tgl_masuk=@tgl_masuk, thn_periode=@thn_periode, jmlh_masuk=@jmlh_masuk, kas_kecil=@kas_kecil, ENDDA=@ENDDA, change_date=@change_date  WHERE tgl_masuk='" + tgl_masuk + "'", con);
            cmd.Parameters.AddWithValue("@tgl_masuk", tgledit.Text);
            cmd.Parameters.AddWithValue("@thn_periode", thnedit.Text);
            cmd.Parameters.AddWithValue("@jmlh_masuk", jmlhedit.Text);
            cmd.Parameters.AddWithValue("@kas_kecil", unitedit);
            cmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            gvBioCash.EditIndex = -1;
            gvbind();
        }

        protected void RowCancelEditing(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvBioCash.EditIndex = -1;
            gvbind();
        }

        protected void RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            string tgl_masuk = gvBioCash.DataKeys[e.RowIndex].Value.ToString();
            Label tgllabel = gvBioCash.Rows[e.RowIndex].FindControl("tgllabel") as Label;
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM biocash.pemasukkan WHERE tgl_masuk='" + tgl_masuk + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            gvbind();
        }

        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvBioCash.EditIndex == e.Row.RowIndex)
            {
                DropDownList unitedit = (DropDownList)e.Row.FindControl("unitedit");
                SqlCommand cmd = new SqlCommand("SELECT Unit FROM biocash.Masterunit", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                unitedit.DataSource = ds;
                unitedit.DataTextField = "Unit";
                unitedit.DataValueField = "Unit";
                unitedit.DataBind();
            }
        }
    }
}