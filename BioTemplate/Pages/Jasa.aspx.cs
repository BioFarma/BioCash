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
    public partial class Jasa : System.Web.UI.Page
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        DateTime dateMax = new DateTime(9999, 12, 31, 00, 00, 00);
        string jasaselectededit;
        protected void Page_Load(object sender, EventArgs e)
        {
            con.ConnectionString = "Data Source=MSI;Initial Catalog=BioCash;Persist Security Info=True;User ID=sa;Password=@Gtabp1000";
            if (!IsPostBack)
            {
                gvbind();
                dlkas();
                dlbagian();
                gvBindSaldo();
                
                if (pphedit.Text == string.Empty)
                {
                    pphedit.Text = "0";
                }
            }
        }

        protected void gvBindSaldo()
        {
            int vsaldo = 0;

            //SqlCommand ucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA WHERE saldo=@saldo", con);
            //ucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            //ucmd.Parameters.AddWithValue("@saldo", vsaldo.ToString());
            //con.Open();
            //ucmd.ExecuteNonQuery();
            //con.Close();

            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Saldo WHERE ENDDA=@ENDDA", con);
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);
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
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Pengeluaran WHERE ENDDA=@ENDDA AND jasa='Ya'", con);
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);
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

        protected void dlbagian()
        {
            SqlCommand cmd = new SqlCommand("SELECT nama_bagian FROM biocash.Bagian", con); // table name 
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);  // fill dataset
            bagianDledit.DataTextField = ds.Tables[0].Columns["nama_bagian"].ToString(); // text field name of table dispalyed in dropdown
            bagianDledit.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            bagianDledit.DataBind();  //binding dropdownlist
            bagianDledit.Items.Insert(0, "--Pilih bagian--");
        }

        protected void dlkas()
        {
            SqlCommand cmd = new SqlCommand("SELECT Kas FROM biocash.Masterkas WHERE ENDDA=@ENDDA", con); // table name 
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);  // fill dataset
            kasDledit.DataTextField = ds.Tables[0].Columns["Kas"].ToString(); // text field name of table dispalyed in dropdown
            kasDledit.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            kasDledit.DataBind();  //binding dropdownlist
            kasDledit.Items.Insert(0, "--Pilih kas--");
        }
        

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvBioCash.Rows[rowIndex];
            id.Text = (row.FindControl("idkeluarlabel") as Label).Text;
            tgledit.Text = (row.FindControl("tgllabel") as Label).Text;
            jmlhkeluaredit.Text = (row.FindControl("jmlhlabel") as Label).Text;
            keteranganedit.InnerText = (row.FindControl("keteranganlabel") as Label).Text;
            kasDledit.Text = (row.FindControl("kaslabel") as Label).Text;
            periodeDledit.Text = (row.FindControl("periodelabel") as Label).Text;
            bagianDledit.Text = (row.FindControl("bagianlabel") as Label).Text;
            vendoredit.Text = (row.FindControl("vendorlabel") as Label).Text;
            hargaedit.Text = (row.FindControl("hargalabel") as Label).Text;
            quantityedit.Text = (row.FindControl("quantitylabel") as Label).Text;
            satuanedit.Text = (row.FindControl("satuanlabel") as Label).Text;
            pphedit.Text = (row.FindControl("pphlabel") as Label).Text;
            nomoredit.Text = (row.FindControl("nomorlabel") as Label).Text;
            saldoedit.Text = jmlhkeluaredit.Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalEdit();", true);
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            SqlCommand ccmd = new SqlCommand("SELECT saldo,Kas,thn_periode FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
            ccmd.Parameters.AddWithValue("@ENDDA", dateMax);
            SqlParameter[] cprms = new SqlParameter[3];

            cprms[0] = new SqlParameter("@saldo", SqlDbType.VarChar, 50);
            cprms[1] = new SqlParameter("@Kas", SqlDbType.VarChar, 50);
            cprms[2] = new SqlParameter("@thn_periode", SqlDbType.VarChar, 50);
            cprms[0].Value = jmlhkeluaredit.Text;
            cprms[1].Value = kasDledit.Text;
            cprms[2].Value = periodeDledit.Text;
            con.Open();
            ccmd.Parameters.AddRange(cprms);
            object saldokas = ccmd.ExecuteScalar();
            con.Close();
            if (saldokas == null)
            {
                string message = "Tidak ada saldo pada " + kasDledit.SelectedItem.Value + " " + periodeDledit.Text + "";
                string script = "{ alert('" + message + "'); }";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
            }
            else
            {
                SqlCommand sscmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
                sscmd.Parameters.AddWithValue("@ENDDA", dateMax);
                sscmd.Parameters.AddWithValue("@Kas", kasDledit.Text);
                sscmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
                con.Open();
                SqlDataReader myReader = sscmd.ExecuteReader();
                while (myReader.Read())
                {
                    string result = myReader.GetValue(0).ToString();
                    saldotemp.Text = result.ToString();
                }
                con.Close();

                SqlCommand pcmd = new SqlCommand("SELECT jmlh_masuk FROM biocash.Pemasukkan WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
                pcmd.Parameters.AddWithValue("@ENDDA", dateMax);
                pcmd.Parameters.AddWithValue("@Kas", kasDledit.Text);
                pcmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
                con.Open();
                SqlDataReader PReader = pcmd.ExecuteReader();
                while (PReader.Read())
                {
                    string masuk = PReader.GetValue(0).ToString();
                    saldoedit.Text = masuk.ToString();
                }
                con.Close();

                SqlCommand pphcmd = new SqlCommand("SELECT pph FROM biocash.Pengeluaran WHERE id=@id", con);
                pphcmd.Parameters.AddWithValue("@id", id.Text);
                con.Open();
                SqlDataReader PhReader = pphcmd.ExecuteReader();
                while (PhReader.Read())
                {
                    string keluarid = PhReader.GetValue(0).ToString();
                    saldoid.Text = keluarid.ToString();
                }
                con.Close();

                if (saldoid.Text == string.Empty)
                {
                    saldoid.Text = "0";
                }

                int pphnow = Convert.ToInt32(saldoid.Text);
                int saldomasuk = Convert.ToInt32(saldoedit.Text);
                int saldodb = Convert.ToInt32(saldotemp.Text);
                int uangpph = Convert.ToInt32(pphedit.Text);
                int sub = saldodb - pphnow;
                int subtotal = sub + uangpph;
                string totalsaldo = subtotal.ToString();

                if (subtotal > saldomasuk)
                {
                    string mmessage = "Jumlah uang melebihi saldo yang diberikan";
                    string sscript = "{ alert('" + mmessage + "'); }";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", sscript, true);
                }
                else
                {

                    SqlCommand cpcmd = new SqlCommand("SELECT pph FROM biocash.Pengeluaran WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
                    SqlParameter[] cpprms = new SqlParameter[3];

                    cpprms[0] = new SqlParameter("@ENDDA", SqlDbType.DateTime);
                    cpprms[1] = new SqlParameter("@Kas", SqlDbType.VarChar, 50);
                    cpprms[2] = new SqlParameter("@thn_periode", SqlDbType.VarChar, 50);
                    cpprms[0].Value = dateMax;
                    cpprms[1].Value = kasDledit.SelectedItem.Value;
                    cpprms[2].Value = periodeDledit.Text;
                    con.Open();
                    cpcmd.Parameters.AddRange(cpprms);
                    object pphcek = cpcmd.ExecuteScalar();
                    con.Close();

                    if (radioyaedit.Checked)
                    {
                        jasaselectededit = "Ya";
                    }
                    else if (radiotidakedit.Checked)
                    {
                        jasaselectededit = "Tidak";
                    }

                    if (pphcek == null)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO biocash.Pengeluaran" + "(BEGDA,Kas,tgl_keluar,keterangan,harga,unit,jmlh_keluar,pph,nomor,thn_periode,nama_bagian,vendor,satuan,jasa,change_date,ENDDA)VALUES(@BEGDA,@Kas,@tgl_keluar,@keterangan,@harga,@unit,@jmlh_keluar,@pph,@nomor,@thn_periode,@nama_bagian,@vendor,@satuan,@jasa,@change_date,@ENDDA)", con);
                        SqlCommand iscmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
                        SqlCommand iuscmd = new SqlCommand("UPDATE biocash.Saldo set change_date=@change_date, ENDDA=@ENDDA WHERE ENDDA=@ENDDAS AND Kas=@Kas AND thn_periode=@thn_periode", con);

                        cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@tgl_keluar", tgledit.Text);
                        cmd.Parameters.AddWithValue("@keterangan", keteranganedit.Value);
                        cmd.Parameters.AddWithValue("@harga", hargaedit.Text);
                        cmd.Parameters.AddWithValue("@unit", quantityedit.Text);
                        cmd.Parameters.AddWithValue("@jmlh_keluar", jmlhkeluaredit.Text);
                        cmd.Parameters.AddWithValue("@pph", pphedit.Text);
                        cmd.Parameters.AddWithValue("@nomor", nomoredit.Text);
                        cmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
                        cmd.Parameters.AddWithValue("@nama_bagian", bagianDledit.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@vendor", vendoredit.Text);
                        cmd.Parameters.AddWithValue("@satuan", satuanedit.Text);
                        cmd.Parameters.AddWithValue("@jasa", jasaselectededit);
                        cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ENDDA", dateMax);

                        iscmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                        iscmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
                        iscmd.Parameters.AddWithValue("@saldo", totalsaldo);
                        iscmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
                        iscmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                        iscmd.Parameters.AddWithValue("@ENDDA", dateMax);

                        iuscmd.Parameters.AddWithValue("@ENDDAS", dateMax);
                        iuscmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
                        iuscmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
                        iuscmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
                        iuscmd.Parameters.AddWithValue("@change_date", DateTime.Now);

                        con.Open();
                        iuscmd.ExecuteNonQuery();
                        iscmd.ExecuteNonQuery();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        string message = "Data berhasil disimpan";
                        string script = "{ alert('" + message + "'); }";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
                        gvBindSaldo();
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO biocash.Pengeluaran" + "(BEGDA,Kas,tgl_keluar,keterangan,harga,unit,jmlh_keluar,pph,nomor,thn_periode,nama_bagian,vendor,satuan,jasa,change_date,ENDDA)VALUES(@BEGDA,@Kas,@tgl_keluar,@keterangan,@harga,@unit,@jmlh_keluar,@pph,@nomor,@thn_periode,@nama_bagian,@vendor,@satuan,@jasa,@change_date,@ENDDA)", con);
                        SqlCommand iscmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
                        SqlCommand iucmd = new SqlCommand("UPDATE biocash.Pengeluaran set change_date=@change_date, ENDDA=@ENDDA WHERE id=@id", con);
                        SqlCommand iuscmd = new SqlCommand("UPDATE biocash.Saldo set change_date=@change_date, ENDDA=@ENDDA WHERE ENDDA=@ENDDAS AND Kas=@Kas AND thn_periode=@thn_periode", con);

                        cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@tgl_keluar", tgledit.Text);
                        cmd.Parameters.AddWithValue("@keterangan", keteranganedit.Value);
                        cmd.Parameters.AddWithValue("@harga", hargaedit.Text);
                        cmd.Parameters.AddWithValue("@unit", quantityedit.Text);
                        cmd.Parameters.AddWithValue("@jmlh_keluar", jmlhkeluaredit.Text);
                        cmd.Parameters.AddWithValue("@pph", pphedit.Text);
                        cmd.Parameters.AddWithValue("@nomor", nomoredit.Text);
                        cmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
                        cmd.Parameters.AddWithValue("@nama_bagian", bagianDledit.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@vendor", vendoredit.Text);
                        cmd.Parameters.AddWithValue("@satuan", satuanedit.Text);
                        cmd.Parameters.AddWithValue("@jasa", jasaselectededit);
                        cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ENDDA", dateMax);

                        iscmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                        iscmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
                        iscmd.Parameters.AddWithValue("@saldo", totalsaldo);
                        iscmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
                        iscmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                        iscmd.Parameters.AddWithValue("@ENDDA", dateMax);

                        iuscmd.Parameters.AddWithValue("@ENDDAS", dateMax);
                        iuscmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
                        iuscmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
                        iuscmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
                        iuscmd.Parameters.AddWithValue("@change_date", DateTime.Now);

                        iucmd.Parameters.AddWithValue("@id", id.Text);
                        iucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
                        iucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

                        con.Open();
                        iucmd.ExecuteNonQuery();
                        iuscmd.ExecuteNonQuery();
                        iscmd.ExecuteNonQuery();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        string message = "Data berhasil disimpan";
                        string script = "{ alert('" + message + "'); }";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
                        gvBindSaldo();
                    }
                }
            } 
        }

        protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void delete_Click(object sender, EventArgs e)
        {

        }
    }
}