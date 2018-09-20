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

            if (!IsPostBack)
            {
                gvBindSaldo();
                gvbind();
                dlunit();
            }
        }

        protected void clearTextInput()
        {
            jmlhkeluar.Text = string.Empty;
            keperluan.Value = string.Empty;
            tgl_keluar.Text = string.Empty;
            jmlhkeluar.Text = string.Empty;
        }

        protected void gvBindSaldo()
        {
            int vsaldo = 0;

            SqlCommand ucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA='" + DateTime.Now + "' WHERE saldo<='" + vsaldo.ToString() + "'", con);
            con.Open();
            ucmd.ExecuteNonQuery();
            con.Close();

            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Saldo WHERE ENDDA='" + dateMax + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count >= 0)
            {
                gvSaldo.DataSource = ds;
                gvSaldo.DataBind();
            }
        }

        protected void gvbind()
        {
            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Pengeluaran WHERE ENDDA='" + dateMax + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count >= 0)
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
            unitDledit.DataTextField = ds.Tables[0].Columns["Unit"].ToString(); // text field name of table dispalyed in dropdown
            unitDledit.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            unitDledit.DataBind();  //binding dropdownlist
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            SqlCommand scmd = new SqlCommand("SELECT saldo,Unit FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Unit='" + unitDl.SelectedItem.Value + "'", con);

            SqlParameter[] prms = new SqlParameter[2];

            prms[0] = new SqlParameter("@saldo", SqlDbType.VarChar, 50);
            prms[1] = new SqlParameter("@Unit", SqlDbType.VarChar, 50);
            prms[0].Value = jmlhsaldo.Text;
            prms[1].Value = unitdbtext.Text;
            con.Open();
            scmd.Parameters.AddRange(prms);
            object obj = scmd.ExecuteScalar();
            con.Close();
            if (obj == null)
            {
                string message = "Belum ada pemasukkan";
                string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "WarningMessage", script, true);
            }
            else
            {
                SqlCommand lcmd = new SqlCommand("SELECT saldo,Unit FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Unit='" + unitDl.SelectedItem.Value + "'", con);
                con.Open();
                SqlDataReader myReader = lcmd.ExecuteReader();
                while (myReader.Read())
                {
                    string result = myReader.GetValue(0).ToString();
                    jmlhsaldo.Text = result.ToString();
                }
                con.Close();

                int i = Convert.ToInt32(jmlhsaldo.Text);
                int j = Convert.ToInt32(jmlhkeluar.Text);

                if (j > i)
                {
                    string message = "Jumlah uang melebihi saldo yang ada";
                    string script = "{ alert('" + message + "'); }";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
                }
                else
                {
                    int k = i - j;
                    jsaldo.Text = k.ToString();

                    SqlCommand cmd = new SqlCommand("INSERT INTO biocash.Pengeluaran" + "(BEGDA,Unit,tgl_keluar,keterangan,jmlh_keluar,change_date,ENDDA)values(@BEGDA,@Unit,@tgl_keluar,@keterangan,@jmlh_keluar,@change_date,@ENDDA)", con);
                    SqlCommand icmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Unit,saldo,change_date,ENDDA)values(@BEGDA,@Unit,@saldo,@change_date,@ENDDA)", con);
                    SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo set change_date=@change_date, ENDDA=@ENDDA WHERE ENDDA='" + dateMax + "' AND Unit='" + unitDl.SelectedItem.Value + "'", con);


                    cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Unit", unitDl.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@tgl_keluar", tgl_keluar.Text);
                    cmd.Parameters.AddWithValue("@keterangan", keperluan.Value);
                    cmd.Parameters.AddWithValue("@jmlh_keluar", jmlhkeluar.Text);
                    cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ENDDA", dateMax);

                    icmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                    icmd.Parameters.AddWithValue("@Unit", unitDl.SelectedItem.Value);
                    icmd.Parameters.AddWithValue("@saldo", jsaldo.Text);
                    icmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                    icmd.Parameters.AddWithValue("@ENDDA", dateMax);

                    iucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
                    iucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

                    con.Open();
                    iucmd.ExecuteNonQuery();
                    icmd.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    string message = "Data berhasil disimpan";
                    string script = "{ alert('" + message + "'); }";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
                    gvbind();
                    clearTextInput();
                }
                gvBindSaldo();
            }
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvBioCash.Rows[rowIndex];
            id.Text = (row.FindControl("idkeluarlabel") as Label).Text;
            tgledit.Text = (row.FindControl("tgllabel") as Label).Text;
            jmlhkeluaredit.Text = (row.FindControl("jmlhlabel") as Label).Text;
            keteranganedit.InnerText = (row.FindControl("keteranganlabel") as Label).Text;
            unitDledit.Text = (row.FindControl("unitlabel") as Label).Text;
            saldoedit.Text = jmlhkeluaredit.Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            SqlCommand sscmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Unit='" + unitDl.SelectedItem.Value + "'", con);
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
            int saldoAfUp = saldoDb + saldoBef;
            saldotemp.Text = saldoAfUp.ToString();

            int saldotempo = Convert.ToInt32(saldotemp.Text);
            int saldoupdate = Convert.ToInt32(jmlhkeluaredit.Text);

            if (saldoupdate > saldotempo)
            {
                string message = "Jumlah uang melibihi jumlah saldo yang ada";
                string script = "{ alert('" + message + "'); }";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
            }
            else
            {
                int saldoupdating = saldotempo - saldoupdate;
                saldoafteredit.Text = saldoupdating.ToString();

                SqlCommand cmd = new SqlCommand("INSERT INTO biocash.Pengeluaran" + "(BEGDA,Unit,tgl_keluar,keterangan,jmlh_keluar,change_date,ENDDA)VALUES(@BEGDA,@Unit,@tgl_keluar,@keterangan,@jmlh_keluar,@change_date,@ENDDA)", con);
                SqlCommand ucmd = new SqlCommand("UPDATE biocash.Pengeluaran SET ENDDA=@ENDDA, change_date=@change_date WHERE id=@id ", con);

                SqlCommand scmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Unit,saldo,change_date,ENDDA)values(@BEGDA,@Unit,@saldo,@change_date,@ENDDA)", con);
                SqlCommand sucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA='" + dateMax + "' AND Unit='" + unitDledit.SelectedItem.Value + "' ", con);

                cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                cmd.Parameters.AddWithValue("@Unit", unitDledit.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@tgl_keluar", tgledit.Text);
                cmd.Parameters.AddWithValue("@keterangan", keteranganedit.Value);
                cmd.Parameters.AddWithValue("@jmlh_keluar", jmlhkeluaredit.Text);
                cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                cmd.Parameters.AddWithValue("@ENDDA", dateMax);

                ucmd.Parameters.AddWithValue("@id", id.Text);
                ucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
                ucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

                scmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                scmd.Parameters.AddWithValue("@Unit", unitDledit.SelectedItem.Value);
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
                string message = "Data berhasil diubah";
                string script = "{ alert('" + message + "'); }";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
                gvbind();
                gvBindSaldo();
            }
        }

        protected void RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvBioCash.DataKeys[e.RowIndex].Value.ToString());
            con.Open();

            SqlCommand scmd = new SqlCommand("SELECT Unit,jmlh_keluar FROM biocash.Pengeluaran WHERE id='" + id + "'", con);
            SqlDataReader myReader = scmd.ExecuteReader();
            while (myReader.Read())
            {
                string unit = myReader.GetValue(0).ToString();
                string saldo = myReader.GetValue(1).ToString();
                unitdel.Text = unit.ToString();
                saldodel.Text = saldo.ToString();
            }
            con.Close();

            con.Open();
            SqlCommand delcmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Unit='" + unitdel.Text + "'", con);
            SqlDataReader delReader = delcmd.ExecuteReader();
            while (delReader.Read())
            {
                string result = delReader.GetValue(0).ToString();
                jmlhsaldo.Text = result.ToString();
            }
            con.Close();

            int saldotemp = Convert.ToInt32(saldodel.Text);
            int saldodbtemp = Convert.ToInt32(jmlhsaldo.Text);
            int deletesaldo = saldodbtemp + saldotemp;
            totdelsaldo.Text = deletesaldo.ToString();

            SqlCommand cmd = new SqlCommand("UPDATE biocash.Pengeluaran SET ENDDA=@ENDDA WHERE id='" + id + "'", con);
            SqlCommand iscmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Unit,saldo,change_date,ENDDA)values(@BEGDA,@Unit,@saldo,@change_date,@ENDDA)", con);
            SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA='" + dateMax + "' AND Unit='" + unitdel.Text + "' ", con);

            iscmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            iscmd.Parameters.AddWithValue("@Unit", unitdel.Text);
            iscmd.Parameters.AddWithValue("@saldo", totdelsaldo.Text);
            iscmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            iscmd.Parameters.AddWithValue("@ENDDA", dateMax);

            iucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            iucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

            cmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            con.Open();
            iucmd.ExecuteNonQuery();
            iscmd.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "Data berhasil dihapus";
            string script = "{ alert('" + message + "'); }";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
            gvbind();
            gvBindSaldo();
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand scmd = new SqlCommand("SELECT Unit,jmlh_keluar FROM biocash.Pengeluaran WHERE id='" + id.Text + "'", con);
            SqlDataReader myReader = scmd.ExecuteReader();
            while (myReader.Read())
            {
                string unit = myReader.GetValue(0).ToString();
                string saldo = myReader.GetValue(1).ToString();
                unitdel.Text = unit.ToString();
                saldodel.Text = saldo.ToString();
            }
            con.Close();

            con.Open();
            SqlCommand delcmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Unit='" + unitdel.Text + "'", con);
            SqlDataReader delReader = delcmd.ExecuteReader();
            while (delReader.Read())
            {
                string result = delReader.GetValue(0).ToString();
                jmlhsaldo.Text = result.ToString();
            }
            con.Close();

            int saldotemp = Convert.ToInt32(saldodel.Text);
            int saldodbtemp = Convert.ToInt32(jmlhsaldo.Text);
            int deletesaldo = saldodbtemp + saldotemp;
            totdelsaldo.Text = deletesaldo.ToString();

            SqlCommand cmd = new SqlCommand("UPDATE biocash.Pengeluaran SET ENDDA=@ENDDA WHERE id='" + id.Text + "'", con);
            SqlCommand iscmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Unit,saldo,change_date,ENDDA)values(@BEGDA,@Unit,@saldo,@change_date,@ENDDA)", con);
            SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA='" + dateMax + "' AND Unit='" + unitdel.Text + "' ", con);

            iscmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            iscmd.Parameters.AddWithValue("@Unit", unitdel.Text);
            iscmd.Parameters.AddWithValue("@saldo", totdelsaldo.Text);
            iscmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            iscmd.Parameters.AddWithValue("@ENDDA", dateMax);

            iucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            iucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

            cmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            con.Open();
            iucmd.ExecuteNonQuery();
            iscmd.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "Data berhasil dihapus";
            string script = "{ alert('" + message + "'); }";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
            gvbind();
            gvBindSaldo();
        }
    }
}