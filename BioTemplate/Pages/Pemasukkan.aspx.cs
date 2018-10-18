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
                dlkas();
                gvBindSaldo();
                gvbind();
            }

        }

        protected void clearTextInput()
        {
            Masuk.Text = string.Empty;
            tgl_masuk.Text = string.Empty;
            periode_masuk.Text = string.Empty;
        }

        protected void gvBindSaldo()
        {
            int vsaldo = 0;

            SqlCommand ucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA='"+DateTime.Now+"' WHERE saldo='"+vsaldo.ToString()+"'", con);
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

        protected void dlkas()
        {
            SqlCommand cmd = new SqlCommand("SELECT Kas FROM biocash.Masterkas WHERE ENDDA='"+dateMax+"'", con); // table name 
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);  // fill dataset
            kasdl.DataTextField = ds.Tables[0].Columns["Kas"].ToString(); // text field name of table dispalyed in dropdown
            kasdl.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            kasdl.DataBind();  //binding dropdownlist
            kasdl.Items.Insert(0, "--Pilih kas--");
            kasdledit.DataTextField = ds.Tables[0].Columns["Kas"].ToString(); // text field name of table dispalyed in dropdown
            kasdledit.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            kasdledit.DataBind();  //binding dropdownlist
            kasdledit.Items.Insert(0, "--Pilih kas--");
        }

        protected void kasdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT plafond FROM biocash.Masterkas WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdl.SelectedItem.Value + "'", con);

            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                string result = myReader.GetValue(0).ToString();
                Masuk.Text = result.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalInput();", true);
            }
            con.Close();
        }

        protected void gvbind()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Pemasukkan WHERE ENDDA='"+dateMax+"'", con);
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
        
        protected void Confirm_Click(object sender, EventArgs e)
        {
            string vsaldo = "0";

            SqlCommand ccmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@kas AND saldo < @saldo", con);
            SqlParameter[] cprms = new SqlParameter[3];

            cprms[0] = new SqlParameter("@Kas", SqlDbType.VarChar, 50);
            cprms[1] = new SqlParameter("@ENDDA", SqlDbType.VarChar, 50);
            cprms[2] = new SqlParameter("@saldo", SqlDbType.VarChar, 50);
            cprms[0].Value = kasdl.Text;
            cprms[1].Value = dateMax;
            cprms[2].Value = vsaldo;
            con.Open();
            ccmd.Parameters.AddRange(cprms);
            object saldomin = ccmd.ExecuteScalar();
            con.Close();
            if (saldomin != null)
            {
                SqlCommand salcmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdl.SelectedItem.Value + "' AND saldo < 0", con);
                con.Open();
                SqlDataReader mysalReader = salcmd.ExecuteReader();
                while (mysalReader.Read())
                {
                    string result = mysalReader.GetValue(0).ToString();
                    jsaldo.Text = result.ToString();
                }
                con.Close();

                if (Convert.ToInt32(jsaldo.Text) >= 0)
                {
                    SqlCommand selectcmd = new SqlCommand("SELECT *FROM biocash.pemasukkan WHERE Kas=@Kas AND ENDDA=@ENDDA AND thn_periode=@thn_periode", con);

                    SqlParameter[] prms = new SqlParameter[3];

                    prms[0] = new SqlParameter("@Kas", SqlDbType.VarChar, 50);
                    prms[1] = new SqlParameter("@ENDDA", SqlDbType.VarChar, 50);
                    prms[2] = new SqlParameter("@thn_periode", SqlDbType.VarChar, 50);
                    prms[0].Value = kasdl.SelectedItem.Value;
                    prms[1].Value = dateMax;
                    prms[2].Value = periode_masuk.Text;
                    con.Open();
                    selectcmd.Parameters.AddRange(prms);
                    object obj = selectcmd.ExecuteScalar();
                    if (obj != null)
                    {
                        SqlCommand cmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdl.SelectedItem.Value + "' AND thn_periode='" + periode_masuk.Text + "'", con);

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



                        SqlCommand icmd = new SqlCommand("INSERT INTO biocash.pemasukkan" + "(BEGDA,Kas,tgl_masuk,thn_periode,jmlh_masuk,change_date,ENDDA)values(@BEGDA,@Kas,@tgl_masuk,@thn_periode,@jmlh_masuk,@change_date,@ENDDA)", con);
                        SqlCommand iscmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
                        SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdl.SelectedItem.Value + "' AND thn_periode='" + periode_masuk.Text + "' ", con);

                        icmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                        icmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
                        icmd.Parameters.AddWithValue("@tgl_masuk", tgl_masuk.Text);
                        icmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
                        icmd.Parameters.AddWithValue("@jmlh_masuk", Masuk.Text);
                        icmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                        icmd.Parameters.AddWithValue("@ENDDA", dateMax);

                        iscmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                        iscmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
                        iscmd.Parameters.AddWithValue("@saldo", jsaldo.Text);
                        iscmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
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
                        clearTextInput();
                        gvbind();
                        gvBindSaldo();
                    }
                    else
                    {
                        SqlCommand ecmd = new SqlCommand("INSERT INTO biocash.pemasukkan" + "(BEGDA,Kas,tgl_masuk,thn_periode,jmlh_masuk,change_date,ENDDA)values(@BEGDA,@Kas,@tgl_masuk,@thn_periode,@jmlh_masuk,@change_date,@ENDDA)", con);
                        SqlCommand scmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);

                        ecmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                        ecmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
                        ecmd.Parameters.AddWithValue("@tgl_masuk", tgl_masuk.Text);
                        ecmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
                        ecmd.Parameters.AddWithValue("@jmlh_masuk", Masuk.Text);
                        ecmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                        ecmd.Parameters.AddWithValue("@ENDDA", dateMax);

                        scmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                        scmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
                        scmd.Parameters.AddWithValue("@saldo", Masuk.Text);
                        scmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
                        scmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                        scmd.Parameters.AddWithValue("@ENDDA", dateMax);
                        scmd.ExecuteNonQuery();
                        ecmd.ExecuteNonQuery();
                        con.Close();
                        string message = "Data berhasil disimpan";
                        string script = "{ alert('" + message + "'); }";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
                        clearTextInput();
                        gvbind();
                        gvBindSaldo();
                    }
                }
                else
                {
                    gvbind();
                    gvBindSaldo();
                    label1.Text = "Terdapat kredit sebesar " + jsaldo.Text + " di kas " + kasdl.SelectedItem.Value + "";
                    label2.Text = "Kredit tersebut akan dilimpahkan pada pemasukkan saat ini. <br /> Lanjutkan ?";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalConfirm();", true);
                }
            }
            else
            {
                SqlCommand selectcmd = new SqlCommand("SELECT *FROM biocash.pemasukkan WHERE Kas=@Kas AND ENDDA=@ENDDA AND thn_periode=@thn_periode", con);

                SqlParameter[] prms = new SqlParameter[3];

                prms[0] = new SqlParameter("@Kas", SqlDbType.VarChar, 50);
                prms[1] = new SqlParameter("@ENDDA", SqlDbType.VarChar, 50);
                prms[2] = new SqlParameter("@thn_periode", SqlDbType.VarChar, 50);
                prms[0].Value = kasdl.Text;
                prms[1].Value = dateMax;
                prms[2].Value = periode_masuk.Text;
                con.Open();
                selectcmd.Parameters.AddRange(prms);
                object obj = selectcmd.ExecuteScalar();
                if (obj != null)
                {
                    SqlCommand cmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdl.SelectedItem.Value + "' AND thn_periode='" + periode_masuk.Text + "'", con);

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



                    SqlCommand icmd = new SqlCommand("INSERT INTO biocash.pemasukkan" + "(BEGDA,Kas,tgl_masuk,thn_periode,jmlh_masuk,change_date,ENDDA)values(@BEGDA,@Kas,@tgl_masuk,@thn_periode,@jmlh_masuk,@change_date,@ENDDA)", con);
                    SqlCommand iscmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
                    SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdl.SelectedItem.Value + "' AND thn_periode='" + periode_masuk.Text + "' ", con);

                    icmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                    icmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
                    icmd.Parameters.AddWithValue("@tgl_masuk", tgl_masuk.Text);
                    icmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
                    icmd.Parameters.AddWithValue("@jmlh_masuk", Masuk.Text);
                    icmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                    icmd.Parameters.AddWithValue("@ENDDA", dateMax);

                    iscmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                    iscmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
                    iscmd.Parameters.AddWithValue("@saldo", jsaldo.Text);
                    iscmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
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
                    clearTextInput();
                    gvbind();
                    gvBindSaldo();
                }
                else
                {
                    SqlCommand ecmd = new SqlCommand("INSERT INTO biocash.pemasukkan" + "(BEGDA,Kas,tgl_masuk,thn_periode,jmlh_masuk,change_date,ENDDA)values(@BEGDA,@Kas,@tgl_masuk,@thn_periode,@jmlh_masuk,@change_date,@ENDDA)", con);
                    SqlCommand scmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);

                    ecmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                    ecmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
                    ecmd.Parameters.AddWithValue("@tgl_masuk", tgl_masuk.Text);
                    ecmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
                    ecmd.Parameters.AddWithValue("@jmlh_masuk", Masuk.Text);
                    ecmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                    ecmd.Parameters.AddWithValue("@ENDDA", dateMax);

                    scmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                    scmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
                    scmd.Parameters.AddWithValue("@saldo", Masuk.Text);
                    scmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
                    scmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                    scmd.Parameters.AddWithValue("@ENDDA", dateMax);
                    scmd.ExecuteNonQuery();
                    ecmd.ExecuteNonQuery();
                    con.Close();
                    string message = "Data berhasil disimpan";
                    string script = "{ alert('" + message + "'); }";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
                    clearTextInput();
                    gvbind();
                    gvBindSaldo();
                }
            }
        }

        protected void confirmpop_Click(object sender, EventArgs e)
        {
            SqlCommand ccmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdl.SelectedItem.Value + "' AND saldo < 0", con);
            con.Open();
            SqlDataReader myCReader = ccmd.ExecuteReader();
            while (myCReader.Read())
            {
                string result = myCReader.GetValue(0).ToString();
                jsaldo.Text = result.ToString();
            }
            con.Close();

            int saldominus = Convert.ToInt32(jsaldo.Text);
            int saldonow = Convert.ToInt32(Masuk.Text);
            int total = saldominus + saldonow;
            string totalsaldo = total.ToString();

            SqlCommand icmd = new SqlCommand("INSERT INTO biocash.pemasukkan" + "(BEGDA,Kas,tgl_masuk,thn_periode,jmlh_masuk,change_date,ENDDA)values(@BEGDA,@Kas,@tgl_masuk,@thn_periode,@jmlh_masuk,@change_date,@ENDDA)", con);
            SqlCommand iscmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
            SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdl.SelectedItem.Value + "' AND thn_periode='" + periode_masuk.Text + "' ", con);
            SqlCommand iuscmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA='" +DateTime.Now+ "', change_date='"+DateTime.Now+"' WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdl.SelectedItem.Value + "' AND saldo < 0", con);

            icmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            icmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
            icmd.Parameters.AddWithValue("@tgl_masuk", tgl_masuk.Text);
            icmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
            icmd.Parameters.AddWithValue("@jmlh_masuk", Masuk.Text);
            icmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            icmd.Parameters.AddWithValue("@ENDDA", dateMax);

            iscmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            iscmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
            iscmd.Parameters.AddWithValue("@saldo", totalsaldo);
            iscmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
            iscmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            iscmd.Parameters.AddWithValue("@ENDDA", dateMax);

            iucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            iucmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            con.Open();
            iucmd.ExecuteNonQuery();
            iuscmd.ExecuteNonQuery();
            iscmd.ExecuteNonQuery();
            icmd.ExecuteNonQuery();
            con.Close();
            string message = "Data berhasil disimpan.";
            string script = "window.onload = function(){ alert('"; script += message; script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            clearTextInput();
            gvbind();
            gvBindSaldo();
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvBioCash.Rows[rowIndex];
            id.Text = (row.FindControl("idmasuklabel") as Label).Text;
            kasdledit.Text = (row.FindControl("kaslabel") as Label).Text;
            jmlhmasukedit.Text = (row.FindControl("jmlhlabel") as Label).Text;
            tglmasukedit.Text = (row.FindControl("tgllabel") as Label).Text;
            thnmasukedit.Text = (row.FindControl("thnlabel") as Label).Text;
            saldoedit.Text = jmlhmasukedit.Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }

        protected void update_Click(object sender, EventArgs e)
        {
            SqlCommand sscmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdl.SelectedItem.Value + "' AND thn_periode='"+thnmasukedit.Text+"'", con);
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

            SqlCommand cmd = new SqlCommand("INSERT INTO biocash.pemasukkan" + "(BEGDA,Kas,tgl_masuk,thn_periode,jmlh_masuk,change_date,ENDDA)VALUES(@BEGDA,@Kas,@tgl_masuk,@thn_periode,@jmlh_masuk,@change_date,@ENDDA)", con);
            SqlCommand ucmd = new SqlCommand("UPDATE biocash.pemasukkan SET ENDDA=@ENDDA, change_date=@change_date WHERE id=@id ", con);

            SqlCommand scmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
            SqlCommand sucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdledit.SelectedItem.Value + "' AND thn_periode='"+thnmasukedit.Text+"' ", con);
     
            cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            cmd.Parameters.AddWithValue("@Kas", kasdledit.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@tgl_masuk", tglmasukedit.Text);
            cmd.Parameters.AddWithValue("@thn_periode", thnmasukedit.Text);
            cmd.Parameters.AddWithValue("@jmlh_masuk", jmlhmasukedit.Text);
            cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);

            ucmd.Parameters.AddWithValue("@id", id.Text);
            ucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            ucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

            scmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            scmd.Parameters.AddWithValue("@Kas", kasdledit.SelectedItem.Value);
            scmd.Parameters.AddWithValue("@saldo", saldoafteredit.Text);
            scmd.Parameters.AddWithValue("@thn_periode", thnmasukedit.Text);
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
        
        protected void RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvBioCash.DataKeys[e.RowIndex].Value.ToString());

            con.Open();
            SqlCommand scmd = new SqlCommand("SELECT Kas,jmlh_masuk,thn_periode FROM biocash.pemasukkan WHERE id='" + id + "'", con);
            SqlDataReader myReader = scmd.ExecuteReader();
            while (myReader.Read())
            {
                string kas = myReader.GetValue(0).ToString();
                string saldo = myReader.GetValue(1).ToString();
                string periode = myReader.GetValue(2).ToString();
                kasdel.Text = kas.ToString();
                saldodel.Text = saldo.ToString();
                thnperiode.Text = periode.ToString();
            }
            con.Close();

            con.Open();
            SqlCommand delcmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdel.Text + "' AND thn_periode='"+thnperiode.Text+"'", con);
            SqlDataReader delReader = delcmd.ExecuteReader();
            while (delReader.Read())
            {
                string result = delReader.GetValue(0).ToString();
                jmlhsaldo.Text = result.ToString();
            }
            con.Close();

            int saldotemp = Convert.ToInt32(saldodel.Text);
            int saldodbtemp = Convert.ToInt32(jmlhsaldo.Text);
            int deletesaldo = saldodbtemp - saldotemp;

            if (deletesaldo < 0 && deletesaldo != saldotemp)
            {
                string messages = "Tidak bisa dihapus, saldo sudah digunakan";
                string scripts = "{ alert('" + messages + "'); }";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", scripts, true);
                return;
            }
            else
            {
                totdelsaldo.Text = deletesaldo.ToString();

                SqlCommand cmd = new SqlCommand("UPDATE biocash.pemasukkan SET ENDDA=@ENDDA WHERE id='" + id + "'", con);
                SqlCommand iscmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
                SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdel.Text + "' AND thn_periode='" + thnperiode.Text + "'", con);

                iscmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                iscmd.Parameters.AddWithValue("@Kas", kasdel.Text);
                iscmd.Parameters.AddWithValue("@saldo", totdelsaldo.Text);
                iscmd.Parameters.AddWithValue("@thn_periode", thnperiode.Text);
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
                gvBindSaldo();
                gvbind();
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand scmd = new SqlCommand("SELECT Kas,jmlh_masuk,thn_periode FROM biocash.pemasukkan WHERE id='" + id.Text + "'", con);
            SqlDataReader myReader = scmd.ExecuteReader();
            while (myReader.Read())
            {
                string kas = myReader.GetValue(0).ToString();
                string saldo = myReader.GetValue(1).ToString();
                string periode = myReader.GetValue(2).ToString();
                kasdel.Text = kas.ToString();
                saldodel.Text = saldo.ToString();
                thnperiode.Text = periode.ToString();
            }
            con.Close();

            con.Open();
            SqlCommand delcmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdel.Text + "' AND thn_periode='"+thnperiode.Text+"'", con);
            SqlDataReader delReader = delcmd.ExecuteReader();
            while (delReader.Read())
            {
                string result = delReader.GetValue(0).ToString();
                jmlhsaldo.Text = result.ToString();
            }
            con.Close();

            int saldotemp = Convert.ToInt32(saldodel.Text);
            int saldodbtemp = Convert.ToInt32(jmlhsaldo.Text);
            int deletesaldo = saldodbtemp - saldotemp;

            if (deletesaldo < 0 && deletesaldo != saldotemp)
            {
                string messages = "Tidak bisa dihapus, saldo sudah digunakan";
                string scripts = "{ alert('" + messages + "'); }";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", scripts, true);
                return;
            }
            else
            {
                totdelsaldo.Text = deletesaldo.ToString();

                SqlCommand cmd = new SqlCommand("UPDATE biocash.pemasukkan SET ENDDA=@ENDDA WHERE id='" + id.Text + "'", con);
                SqlCommand iscmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
                SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA='" + dateMax + "' AND Kas='" + kasdel.Text + "' AND thn_periode='" + thnperiode.Text + "'", con);

                iscmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                iscmd.Parameters.AddWithValue("@Kas", kasdel.Text);
                iscmd.Parameters.AddWithValue("@saldo", totdelsaldo.Text);
                iscmd.Parameters.AddWithValue("@thn_periode", thnperiode.Text);
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
}