using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BioTemplate.Pages
{
    public partial class MasterUnit : System.Web.UI.Page
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            con.ConnectionString = "Data Source=LAPTOP-LUGIMV8T;Initial Catalog=BioCash;User ID=sa;Password=@Gtabp1000";
            if (!IsPostBack)
            {
                gvbind();
            }
        }

        protected void gvbind()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT unit FROM biocash.Masterunit", con);
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
            if (Unit.Text == "")
            {
                string message = "Unit harus diisi dulu";
                string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "WarningMessage", script, true);
                Unit.Text = String.Empty;
            }
            else
            {

                SqlCommand scmd = new SqlCommand("SELECT *FROM biocash.Masterunit WHERE Unit = @Unit", con);

                SqlParameter[] prms = new SqlParameter[1];
                prms[0] = new SqlParameter("@Unit", SqlDbType.VarChar, 50);
                prms[0].Value = Unit.Text;
                con.Open();
                scmd.Parameters.AddRange(prms);
                object obj = scmd.ExecuteScalar();
                if (obj != null)
                {
                    string message = "Unit sudah ada";
                    string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "WarningMessage", script, true);
                    obj.ToString();
                    Unit.Text = String.Empty;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into biocash.Masterunit (Unit,BEGDA,change_date)values(@Unit,@BEGDA,@change_date)", con);

                    cmd.Parameters.AddWithValue("@Unit", Unit.Text);
                    cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                    cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Unit.Text = String.Empty;
                    string message = "Unit berhasil ditambah";
                    string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                    gvbind();
                }
                con.Close();
            }
        }

        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvBioCash.EditIndex = e.NewEditIndex;
            gvbind();
        }

        protected void RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            
            //Finding the controls from Gridview for the row which is going to update  
            string Unit = gvBioCash.DataKeys[e.RowIndex].Value.ToString();
            TextBox edit = (TextBox)gvBioCash.Rows[e.RowIndex].FindControl("Unitedit");
            con.Open();
            //updating the record  
            SqlCommand cmd = new SqlCommand("UPDATE biocash.Masterunit SET Unit=@Unit, ENDDA=@ENDDA, change_date=@change_date WHERE Unit='" + Unit +"'", con);
            cmd.Parameters.AddWithValue("@Unit",edit.Text);
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
            string Unit = gvBioCash.DataKeys[e.RowIndex].Value.ToString();
            Label lbl = gvBioCash.Rows[e.RowIndex].FindControl("Unit_label") as Label;
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM biocash.Masterunit WHERE Unit='"+ Unit +"'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            gvbind();
        }
    }
}