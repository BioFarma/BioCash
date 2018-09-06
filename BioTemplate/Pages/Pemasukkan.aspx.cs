using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
        DateTime dateMax = new DateTime(9999, 12, 31, 00, 00, 00);

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
            SqlCommand cmd = new SqlCommand("SELECT Unit FROM biocash.Masterunit WHERE ENDDA='"+dateMax+"'", con); // table name 
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);  // fill dataset
            kas_kecil.DataTextField = ds.Tables[0].Columns["Unit"].ToString(); // text field name of table dispalyed in dropdown
            kas_kecil.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            kas_kecil.DataBind();  //binding dropdownlist
            kaskeciledit.DataTextField = ds.Tables[0].Columns["Unit"].ToString(); // text field name of table dispalyed in dropdown
            kaskeciledit.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            kaskeciledit.DataBind();  //binding dropdownlist
        }

        protected void gvbind()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Pemasukkan WHERE ENDDA='"+dateMax+"'", con);
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
            SqlCommand cmd = new SqlCommand("INSERT INTO biocash.pemasukkan" + "(BEGDA,kas_kecil,tgl_masuk,thn_periode,jmlh_masuk,change_date,ENDDA)values(@BEGDA,@kas_kecil,@tgl_masuk,@thn_periode,@jmlh_masuk,@change_date,@ENDDA)", con);
            SqlCommand scmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Unit,saldo,change_date,ENDDA)values(@BEGDA,@Unit,@saldo,@change_date,@ENDDA)", con);

            cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            cmd.Parameters.AddWithValue("@kas_kecil",kas_kecil.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@tgl_masuk", tgl_masuk.Text);
            cmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
            cmd.Parameters.AddWithValue("@jmlh_masuk", Masuk.Text);
            cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);

            scmd.Parameters.AddWithValue("@BEGDA",DateTime.Now);
            scmd.Parameters.AddWithValue("@Unit", kas_kecil.SelectedItem.Value);
            scmd.Parameters.AddWithValue("@saldo", Masuk.Text);
            scmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            scmd.Parameters.AddWithValue("@ENDDA", dateMax);

            con.Open();
            scmd.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "Data berhasil disimpan.";
            string script = "window.onload = function(){ alert('";script += message; script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            gvbind();
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvBioCash.Rows[rowIndex];
            id_masuk.Text = (row.FindControl("idmasuklabel") as Label).Text;
            kaskeciledit.Text = (row.FindControl("unitlabel") as Label).Text;
            jmlhmasukedit.Text = (row.FindControl("jmlhlabel") as Label).Text;
            tglmasukedit.Text = (row.FindControl("tgllabel") as Label).Text;
            thnmasukedit.Text = (row.FindControl("thnlabel") as Label).Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }

        protected void RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id_masuk = Convert.ToInt32(gvBioCash.DataKeys[e.RowIndex].Value.ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE biocash.pemasukkan SET ENDDA=@ENDDA WHERE id_masuk='" + id_masuk + "'", con);
            cmd.Parameters.AddWithValue("@ENDDA",DateTime.Now);
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "Data berhasil dihapus.";
            string script = "window.onload = function(){ alert('"; script += message; script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            gvbind();
        }

        protected void update_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO biocash.pemasukkan" + "(BEGDA,kas_kecil,tgl_masuk,thn_periode,jmlh_masuk,change_date,ENDDA)VALUES(@BEGDA,@kas_kecil,@tgl_masuk,@thn_periode,@jmlh_masuk,@change_date,@ENDDA)", con);
            SqlCommand ucmd = new SqlCommand("UPDATE biocash.pemasukkan SET ENDDA=@ENDDA, change_date=@change_date WHERE id_masuk=@id_masuk ", con);

            SqlCommand scmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Unit,saldo,change_date,ENDDA)values(@BEGDA,@Unit,@saldo,@change_date,@ENDDA)", con);
            SqlCommand sucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA='"+dateMax+"' ", con);

            cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            cmd.Parameters.AddWithValue("@kas_kecil", kaskeciledit.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@tgl_masuk", tglmasukedit.Text);
            cmd.Parameters.AddWithValue("@thn_periode", thnmasukedit.Text);
            cmd.Parameters.AddWithValue("@jmlh_masuk", jmlhmasukedit.Text);
            cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);

            ucmd.Parameters.AddWithValue("@id_masuk",id_masuk.Text);
            ucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            ucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

            scmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            scmd.Parameters.AddWithValue("@Unit", kaskeciledit.SelectedItem.Value);
            scmd.Parameters.AddWithValue("@saldo", jmlhmasukedit.Text);
            scmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            scmd.Parameters.AddWithValue("@ENDDA", dateMax);

            sucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            sucmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            con.Open();
            ucmd.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            scmd.ExecuteNonQuery();
            sucmd.ExecuteNonQuery();
            con.Close();
            string message = "Data berhasil diupdate.";
            string script = "window.onload = function(){ alert('"; script += message; script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            gvbind();
        }
    }
}