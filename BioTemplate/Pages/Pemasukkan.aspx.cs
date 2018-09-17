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
                gvBindSaldo();
            }

        }

        protected void gvBindSaldo()
        {
            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Saldo WHERE ENDDA='" + dateMax + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSaldo.DataSource = ds;
                gvSaldo.DataBind();
            }
        }

        protected void dlunit()
        {
            SqlCommand cmd = new SqlCommand("SELECT Unit FROM biocash.Masterunit WHERE ENDDA='"+dateMax+"'", con); // table name 
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);  // fill dataset
            unitdl.DataTextField = ds.Tables[0].Columns["Unit"].ToString(); // text field name of table dispalyed in dropdown
            unitdl.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            unitdl.DataBind();  //binding dropdownlist
            unitdledit.DataTextField = ds.Tables[0].Columns["Unit"].ToString(); // text field name of table dispalyed in dropdown
            unitdledit.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            unitdledit.DataBind();  //binding dropdownlist
        }

        protected void gvbind()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Pemasukkan WHERE ENDDA='"+dateMax+"'", con);
            //SqlCommand cmd = new SqlCommand("SELECT biocash.pemasukkan.id , biocash.pemasukkan.tgl_masuk, biocash.pemasukkan.thn_periode, biocash.pemasukkan.Unit, biocash.pemasukkan.jmlh_masuk FROM biocash.pemasukkan INNER JOIN biocash.Saldo ON biocash.pemasukkan.Unit=biocash.Saldo.Unit WHERE biocash.pemasukkan.ENDDA='"+dateMax+"' AND biocash.pemasukkan.Unit=biocash.Saldo.Unit", con);
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
            SqlCommand selectcmd = new SqlCommand("SELECT *FROM biocash.pemasukkan WHERE Unit = @Unit AND ENDDA=@ENDDA", con);

            SqlParameter[] prms = new SqlParameter[2];

            prms[0] = new SqlParameter("@Unit", SqlDbType.VarChar, 50);
            prms[1] = new SqlParameter("@ENDDA", SqlDbType.VarChar, 50);
            prms[0].Value = unitdl.Text;
            prms[1].Value = dateMax;
            con.Open();
            selectcmd.Parameters.AddRange(prms);
            object obj = selectcmd.ExecuteScalar();
            if (obj != null)
            {
                SqlCommand cmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Unit='"+ unitdl.SelectedItem.Value +"'", con);

                SqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string result = myReader.GetValue(0).ToString();
                    jmlhsaldo.Text = result.ToString();
                }
                con.Close();

                int i = Convert.ToInt32(jmlhsaldo.Text);
                int j = Convert.ToInt32(Masuk.Text);
                int k = i + j;
                jsaldo.Text = k.ToString();

                SqlCommand icmd = new SqlCommand("INSERT INTO biocash.pemasukkan" + "(BEGDA,Unit,tgl_masuk,thn_periode,jmlh_masuk,change_date,ENDDA)values(@BEGDA,@Unit,@tgl_masuk,@thn_periode,@jmlh_masuk,@change_date,@ENDDA)", con);
                SqlCommand iscmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Unit,saldo,change_date,ENDDA)values(@BEGDA,@Unit,@saldo,@change_date,@ENDDA)", con);
                SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA='" + dateMax + "' AND Unit='" + unitdl.SelectedItem.Value + "' ", con);

                icmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                icmd.Parameters.AddWithValue("@Unit", unitdl.SelectedItem.Value);
                icmd.Parameters.AddWithValue("@tgl_masuk", tgl_masuk.Text);
                icmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
                icmd.Parameters.AddWithValue("@jmlh_masuk", Masuk.Text);
                icmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                icmd.Parameters.AddWithValue("@ENDDA", dateMax);

                iscmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                iscmd.Parameters.AddWithValue("@Unit", unitdl.SelectedItem.Value);
                iscmd.Parameters.AddWithValue("@saldo", jsaldo.Text);
                iscmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                iscmd.Parameters.AddWithValue("@ENDDA", dateMax);

                iucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
                iucmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                con.Open();
                iucmd.ExecuteNonQuery();
                iscmd.ExecuteNonQuery();
                icmd.ExecuteNonQuery();
                con.Close();
                string message = "Data berhasil disimpan.";
                string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                gvbind();
                gvBindSaldo();
            }
            else
            {
                SqlCommand ecmd = new SqlCommand("INSERT INTO biocash.pemasukkan" + "(BEGDA,Unit,tgl_masuk,thn_periode,jmlh_masuk,change_date,ENDDA)values(@BEGDA,@Unit,@tgl_masuk,@thn_periode,@jmlh_masuk,@change_date,@ENDDA)", con);
                SqlCommand scmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Unit,saldo,change_date,ENDDA)values(@BEGDA,@Unit,@saldo,@change_date,@ENDDA)", con);

                ecmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                ecmd.Parameters.AddWithValue("@Unit", unitdl.SelectedItem.Value);
                ecmd.Parameters.AddWithValue("@tgl_masuk", tgl_masuk.Text);
                ecmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
                ecmd.Parameters.AddWithValue("@jmlh_masuk", Masuk.Text);
                ecmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                ecmd.Parameters.AddWithValue("@ENDDA", dateMax);

                scmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                scmd.Parameters.AddWithValue("@Unit", unitdl.SelectedItem.Value);
                scmd.Parameters.AddWithValue("@saldo", Masuk.Text);
                scmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                scmd.Parameters.AddWithValue("@ENDDA", dateMax);
                scmd.ExecuteNonQuery();
                ecmd.ExecuteNonQuery();
                con.Close();
                string message = "Data berhasil disimpan.";
                string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                gvbind();
                gvBindSaldo();
            }
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvBioCash.Rows[rowIndex];
            id.Text = (row.FindControl("idmasuklabel") as Label).Text;
            unitdledit.Text = (row.FindControl("unitlabel") as Label).Text;
            jmlhmasukedit.Text = (row.FindControl("jmlhlabel") as Label).Text;
            tglmasukedit.Text = (row.FindControl("tgllabel") as Label).Text;
            thnmasukedit.Text = (row.FindControl("thnlabel") as Label).Text;
            saldoedit.Text = jmlhmasukedit.Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }

        protected void RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvBioCash.DataKeys[e.RowIndex].Value.ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE biocash.pemasukkan SET ENDDA=@ENDDA WHERE id='" + id + "'", con);
            cmd.Parameters.AddWithValue("@ENDDA",DateTime.Now);
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "Data berhasil dihapus.";
            string script = "window.onload = function(){ alert('"; script += message; script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            gvbind();
            gvBindSaldo();
        }

        protected void update_Click(object sender, EventArgs e)
        {
            SqlCommand sscmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Unit='" + unitdl.SelectedItem.Value + "'", con);
            con.Open();
            SqlDataReader myReader = sscmd.ExecuteReader();
            while (myReader.Read())
            {
                string result = myReader.GetValue(0).ToString();
                jmlhsaldo.Text = result.ToString();
            }
            con.Close();

            int saldoDb = Convert.ToInt32(jmlhsaldo.Text);
            int saldoBef = Convert.ToInt32(saldoedit.Text);
            int saldoAfUp = saldoDb - saldoBef;
            saldotemp.Text = saldoAfUp.ToString();

            int saldotempo = Convert.ToInt32(saldotemp.Text);
            int saldoupdate = Convert.ToInt32(jmlhmasukedit.Text);
            int saldoupdating = saldotempo + saldoupdate;
            saldoafteredit.Text = saldoupdating.ToString();



            SqlCommand cmd = new SqlCommand("INSERT INTO biocash.pemasukkan" + "(BEGDA,Unit,tgl_masuk,thn_periode,jmlh_masuk,change_date,ENDDA)VALUES(@BEGDA,@Unit,@tgl_masuk,@thn_periode,@jmlh_masuk,@change_date,@ENDDA)", con);
            SqlCommand ucmd = new SqlCommand("UPDATE biocash.pemasukkan SET ENDDA=@ENDDA, change_date=@change_date WHERE id=@id ", con);

            SqlCommand scmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Unit,saldo,change_date,ENDDA)values(@BEGDA,@Unit,@saldo,@change_date,@ENDDA)", con);
            SqlCommand sucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA='" + dateMax + "' AND Unit='" + unitdledit.SelectedItem.Value + "' ", con);
     
            cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            cmd.Parameters.AddWithValue("@Unit", unitdledit.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@tgl_masuk", tglmasukedit.Text);
            cmd.Parameters.AddWithValue("@thn_periode", thnmasukedit.Text);
            cmd.Parameters.AddWithValue("@jmlh_masuk", jmlhmasukedit.Text);
            cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);

            ucmd.Parameters.AddWithValue("@id", id.Text);
                ucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
                ucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

                scmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                scmd.Parameters.AddWithValue("@Unit", unitdledit.SelectedItem.Value);
                scmd.Parameters.AddWithValue("@saldo", saldoafteredit.Text);
                scmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                scmd.Parameters.AddWithValue("@ENDDA", dateMax);

                sucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
                sucmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                con.Open();

                sucmd.ExecuteNonQuery();
                scmd.ExecuteNonQuery();
                ucmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                
                con.Close();
                string message = "Data berhasil diupdate.";
                string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                gvbind();
                gvBindSaldo();
        }
    }
}