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
        string jasaselected;
        string jasaselectededit;
        string jasapass;
        protected void Page_Load(object sender, EventArgs e)
        {
            con.ConnectionString = "Data Source=MSI;Initial Catalog=BioCash;Persist Security Info=True;User ID=sa;Password=@Gtabp1000";

            if (!IsPostBack)
            {
                gvBindSaldo();
                gvbind();
                dlkas();
                dlbagian();
            }
        }

        protected void clearTextInput()
        {
            kasDl.ClearSelection();
            bagianDl.ClearSelection();
            jmlhkeluar.Text = string.Empty;
            keperluan.Value = string.Empty;
            tgl_keluar.Text = string.Empty;
            vendor.Text = string.Empty;
            satuan.Text = string.Empty;
            quantity.Text = string.Empty;
            harga.Text = string.Empty;
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
            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Pengeluaran WHERE ENDDA=@ENDDA AND pph IS NULL", con);
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
            bagianDl.DataTextField = ds.Tables[0].Columns["nama_bagian"].ToString(); // text field name of table dispalyed in dropdown
            bagianDl.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            bagianDl.DataBind();  //binding dropdownlist
            bagianDl.Items.Insert(0, "--Pilih bagian--");
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
            kasDl.DataTextField = ds.Tables[0].Columns["Kas"].ToString(); // text field name of table dispalyed in dropdown
            kasDl.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            kasDl.DataBind();  //binding dropdownlist
            kasDl.Items.Insert(0, "--Pilih kas--");
            kasDledit.DataTextField = ds.Tables[0].Columns["Kas"].ToString(); // text field name of table dispalyed in dropdown
            kasDledit.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            kasDledit.DataBind();  //binding dropdownlist
            kasDledit.Items.Insert(0, "--Pilih kas--");
        }

        protected void kasDl_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT thn_periode FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas", con);
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);
            cmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);  // fill dataset
                periodeDl.DataTextField = ds.Tables[0].Columns["thn_periode"].ToString(); // text field name of table dispalyed in dropdown
                periodeDl.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                periodeDl.DataBind();  //binding dropdownlist
                periodeDl.Items.Insert(0, "--Pilih periode--");
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalInput();", true);
           
            con.Close();
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            SqlCommand scmd = new SqlCommand("SELECT saldo,Kas FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
            scmd.Parameters.AddWithValue("@ENDDA", dateMax);
            SqlParameter[] prms = new SqlParameter[3];

            prms[0] = new SqlParameter("@saldo", SqlDbType.VarChar, 50);
            prms[1] = new SqlParameter("@Kas", SqlDbType.VarChar, 50);
            prms[2] = new SqlParameter("@thn_periode", SqlDbType.VarChar, 50);
            prms[0].Value = jmlhkeluar.Text;
            prms[1].Value = kasDl.SelectedItem.Value;
            prms[2].Value = periodeDl.SelectedItem.Value;
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
                SqlCommand lcmd = new SqlCommand("SELECT saldo,Kas FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
                lcmd.Parameters.AddWithValue("@ENDDA", dateMax);
                lcmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
                lcmd.Parameters.AddWithValue("@thn_periode", periodeDl.SelectedItem.Value);
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
                int k = i - j;

                if (k < 0)
                {
                    int periode = Convert.ToInt32(periodeDl.SelectedItem.Value);
                    int findperiod = periode + 1;
                    string find = findperiod.ToString();

                    SqlCommand fcmd = new SqlCommand("SELECT saldo,Kas FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
                    fcmd.Parameters.AddWithValue("@ENDDA", dateMax);
                    SqlParameter[] fprms = new SqlParameter[3];

                    fprms[0] = new SqlParameter("@saldo", SqlDbType.VarChar, 50);
                    fprms[1] = new SqlParameter("@Kas", SqlDbType.VarChar, 50);
                    fprms[2] = new SqlParameter("@thn_periode", SqlDbType.VarChar, 50);
                    fprms[0].Value = jmlhsaldo.Text;
                    fprms[1].Value = kasDl.SelectedItem.Value;
                    fprms[2].Value = find;
                    con.Open();
                    fcmd.Parameters.AddRange(fprms);
                    object periodefind = fcmd.ExecuteScalar();
                    con.Close();
                    if (periodefind == null)
                    {
                        warning1.Text = "Jumlah pengeluaran untuk kas " + kasDl.SelectedItem.Value + " melebihi saldo yang ada ("+k.ToString()+"). <br /> Sisa saldo yang kurang akan dilimpahkan ke pemasukkan selanjutnya.";
                        warning2.Text = "Yakin ingin melanjutkan ?";
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalConfirm();", true);
                    }
                    else
                    {
                        Label1.Text = "Jumlah pengeluaran untuk kas " + kasDl.SelectedItem.Value + " melebihi saldo yang ada (" + k.ToString() + "). <br /> Sisa saldo yang kurang akan dilimpahkan kepemasukkan " + kasDl.SelectedItem.Value+" "+find+".";
                        Label2.Text = "Yakin ingin melanjutkan ?";
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalConfirmPeriode();", true);
                    }

                }
                else
                {
                    jsaldo.Text = k.ToString();
                    
                    if (radioya.Checked)
                    {
                        jasaselected = "Ya";
                    }
                    else if (radiotidak.Checked)
                    {
                        jasaselected = "Tidak";
                    }

                    SqlCommand cmd = new SqlCommand("INSERT INTO biocash.Pengeluaran" + "(BEGDA,Kas,tgl_keluar,keterangan,harga,unit,jmlh_keluar,thn_periode,nama_bagian,vendor,satuan,jasa,change_date,ENDDA)values(@BEGDA,@Kas,@tgl_keluar,@keterangan,@harga,@unit,@jmlh_keluar,@thn_periode,@nama_bagian,@vendor,@satuan,@jasa,@change_date,@ENDDA)", con);
                    SqlCommand icmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
                    SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo set change_date=@change_date, ENDDA=@ENDDA WHERE ENDDA=@ENDDAS AND Kas=@Kas AND thn_periode=@thn_periode", con);


                    cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@tgl_keluar", tgl_keluar.Text);
                    cmd.Parameters.AddWithValue("@keterangan", keperluan.Value);
                    cmd.Parameters.AddWithValue("@harga", harga.Text);
                    cmd.Parameters.AddWithValue("@unit", quantity.Text);
                    cmd.Parameters.AddWithValue("@jmlh_keluar", jmlhkeluar.Text);
                    cmd.Parameters.AddWithValue("@thn_periode", periodeDl.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@nama_bagian", bagianDl.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@vendor", vendor.Text);
                    cmd.Parameters.AddWithValue("@satuan", satuan.Text);
                    cmd.Parameters.AddWithValue("@jasa", jasaselected);
                    cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ENDDA", dateMax);

                    icmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                    icmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
                    icmd.Parameters.AddWithValue("@saldo", jsaldo.Text);
                    icmd.Parameters.AddWithValue("@thn_periode", periodeDl.SelectedItem.Value);
                    icmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                    icmd.Parameters.AddWithValue("@ENDDA", dateMax);

                    iucmd.Parameters.AddWithValue("@ENDDAS", dateMax);
                    iucmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
                    iucmd.Parameters.AddWithValue("@thn_periode", periodeDl.SelectedItem.Value);
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
                    gvBindSaldo();
                    clearTextInput();
                }
            }
        }

        protected void btnperiode_Click(object sender, EventArgs e)
        {
            SqlCommand lcmd = new SqlCommand("SELECT saldo,Kas FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
            lcmd.Parameters.AddWithValue("@ENDDA", dateMax);
            lcmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
            lcmd.Parameters.AddWithValue("@thn_periode", periodeDl.SelectedItem.Value);
            con.Open();
            SqlDataReader myReader = lcmd.ExecuteReader();
            while (myReader.Read())
            {
                string result = myReader.GetValue(0).ToString();
                jmlhsaldo.Text = result.ToString();
            }
            con.Close();

            int getperiode = Convert.ToInt32(periodeDl.SelectedItem.Value);
            int periodesum = getperiode + 1;
            string nextperiode = periodesum.ToString();

            SqlCommand pcmd = new SqlCommand("SELECT saldo,Kas FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
            pcmd.Parameters.AddWithValue("@ENDDA", dateMax);
            pcmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
            pcmd.Parameters.AddWithValue("@thn_periode", nextperiode);
            con.Open();
            SqlDataReader PReader = pcmd.ExecuteReader();
            while (PReader.Read())
            {
                string result = PReader.GetValue(0).ToString();
                jsaldo.Text = result.ToString();
            }
            con.Close();

            int saldonext = Convert.ToInt32(jsaldo.Text);
            int saldolama = Convert.ToInt32(jmlhsaldo.Text);
            int saldobaru = Convert.ToInt32(jmlhkeluar.Text);
            int subtotal = saldolama - saldobaru;
            int total = subtotal + saldonext;
            string totalperiode = total.ToString();

            if (radioya.Checked)
            {
                jasaselected = "Ya";
            }
            else if (radiotidak.Checked)
            {
                jasaselected = "Tidak";
            }

            SqlCommand cmd = new SqlCommand("INSERT INTO biocash.Pengeluaran" + "(BEGDA,Kas,tgl_keluar,keterangan,harga,unit,jmlh_keluar,thn_periode,nama_bagian,vendor,satuan,jasa,change_date,ENDDA)values(@BEGDA,@Kas,@tgl_keluar,@keterangan,@harga,@unit,@jmlh_keluar,@thn_periode,@nama_bagian,@vendor,@satuan,@jasa,@change_date,@ENDDA)", con);
            SqlCommand incmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
            SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo set change_date=@change_date, ENDDA=@ENDDA WHERE ENDDA=@ENDDAS AND Kas=@Kas AND thn_periode=@thn_periode", con);
            SqlCommand iuncmd = new SqlCommand("UPDATE biocash.Saldo set change_date=@change_date, ENDDA=@ENDDA WHERE ENDDA=@ENDDAS AND Kas=@Kas AND thn_periode=@thn_periode", con);

            cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            cmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@tgl_keluar", tgl_keluar.Text);
            cmd.Parameters.AddWithValue("@keterangan", keperluan.Value);
            cmd.Parameters.AddWithValue("@harga", harga.Text);
            cmd.Parameters.AddWithValue("@unit", quantity.Text);
            cmd.Parameters.AddWithValue("@jmlh_keluar", jmlhkeluar.Text);
            cmd.Parameters.AddWithValue("@thn_periode", periodeDl.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@nama_bagian", bagianDl.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@vendor", vendor.Text);
            cmd.Parameters.AddWithValue("@satuan", satuan.Text);
            cmd.Parameters.AddWithValue("@jasa", jasaselected);
            cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);

            incmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            incmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
            incmd.Parameters.AddWithValue("@saldo", totalperiode);
            incmd.Parameters.AddWithValue("@thn_periode", nextperiode);
            incmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            incmd.Parameters.AddWithValue("@ENDDA", dateMax);

            iucmd.Parameters.AddWithValue("@ENDDAS", dateMax);
            iucmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
            iucmd.Parameters.AddWithValue("@thn_periode", periodeDl.SelectedItem.Value);
            iucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            iucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

            iuncmd.Parameters.AddWithValue("@ENDDAS", dateMax);
            iuncmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
            iuncmd.Parameters.AddWithValue("@thn_periode", nextperiode);
            iuncmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            iuncmd.Parameters.AddWithValue("@change_date", DateTime.Now);

            con.Open();
            iucmd.ExecuteNonQuery();
            iuncmd.ExecuteNonQuery();
            incmd.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "Data berhasil disimpan";
            string script = "{ alert('" + message + "'); }";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
            gvbind();
            gvBindSaldo();
            clearTextInput();
        }

        protected void confirmmin_Click(object sender, EventArgs e)
        {
            SqlCommand scmd = new SqlCommand("SELECT saldo,Kas FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
            scmd.Parameters.AddWithValue("@ENDDA", dateMax);
            SqlParameter[] prms = new SqlParameter[3];

            prms[0] = new SqlParameter("@saldo", SqlDbType.VarChar, 50);
            prms[1] = new SqlParameter("@Kas", SqlDbType.VarChar, 50);
            prms[2] = new SqlParameter("@thn_periode", SqlDbType.VarChar, 50);
            prms[0].Value = jmlhkeluar.Text;
            prms[1].Value = kasDl.SelectedItem.Value;
            prms[2].Value = periodeDl.SelectedItem.Value;
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
                SqlCommand lcmd = new SqlCommand("SELECT saldo,Kas FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
                lcmd.Parameters.AddWithValue("@ENDDA", dateMax);
                lcmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
                lcmd.Parameters.AddWithValue("@thn_periode", periodeDl.SelectedItem.Value);
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
                int k = i - j;
                jsaldo.Text = k.ToString();

                if (radioya.Checked)
                {
                    jasaselected = "Ya";
                }
                else if (radiotidak.Checked)
                {
                    jasaselected = "Tidak";
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO biocash.Pengeluaran" + "(BEGDA,Kas,tgl_keluar,keterangan,harga,unit,jmlh_keluar,thn_periode,nama_bagian,vendor,satuan,jasa,change_date,ENDDA)values(@BEGDA,@Kas,@tgl_keluar,@keterangan,@harga,@unit,@jmlh_keluar,@thn_periode,@nama_bagian,@vendor,@satuan,@jasa,@change_date,@ENDDA)", con);
                SqlCommand icmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
                SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo set change_date=@change_date, ENDDA=@ENDDA WHERE ENDDA=@ENDDAS AND Kas=@Kas AND thn_periode=@thn_periode", con);


                cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                cmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@tgl_keluar", tgl_keluar.Text);
                cmd.Parameters.AddWithValue("@keterangan", keperluan.Value);
                cmd.Parameters.AddWithValue("@harga", harga.Text);
                cmd.Parameters.AddWithValue("@unit", quantity.Text);
                cmd.Parameters.AddWithValue("@jmlh_keluar", jmlhkeluar.Text);
                cmd.Parameters.AddWithValue("@thn_periode", periodeDl.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@nama_bagian", bagianDl.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@vendor", vendor.Text);
                cmd.Parameters.AddWithValue("@satuan", satuan.Text);
                cmd.Parameters.AddWithValue("@jasa", jasaselected);
                cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                cmd.Parameters.AddWithValue("@ENDDA", dateMax);

                icmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                icmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
                icmd.Parameters.AddWithValue("@saldo", jsaldo.Text);
                icmd.Parameters.AddWithValue("@thn_periode", periodeDl.SelectedItem.Value);
                icmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                icmd.Parameters.AddWithValue("@ENDDA", dateMax);

                iucmd.Parameters.AddWithValue("@ENDDAS", dateMax);
                iucmd.Parameters.AddWithValue("@Kas", kasDl.SelectedItem.Value);
                iucmd.Parameters.AddWithValue("@thn_periode", periodeDl.SelectedItem.Value);
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
                gvBindSaldo();
                clearTextInput();
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
            kasDledit.Text = (row.FindControl("kaslabel") as Label).Text;
            periodeDledit.Text = (row.FindControl("periodelabel") as Label).Text;
            bagianDledit.Text = (row.FindControl("bagianlabel") as Label).Text;
            vendoredit.Text = (row.FindControl("vendorlabel") as Label).Text;
            satuanedit.Text = (row.FindControl("satuanlabel") as Label).Text;
            hargaedit.Text = (row.FindControl("hargalabel") as Label).Text;
            quantityedit.Text = (row.FindControl("quantitylabel") as Label).Text;
            saldoedit.Text = jmlhkeluaredit.Text;
            string jasacheck = (row.FindControl("jasalabel") as Label).Text;
            //if (jasacheck != "Ya")
            //{
            //    radiotidakedit.Checked = true;
            //    radioyaedit.Checked = false;
            //}
            //else
            //{
            //    radioyaedit.Checked = true;
            //    radiotidakedit.Checked = false;
            //}

            SqlCommand sscmd = new SqlCommand("SELECT pph FROM biocash.Pengeluaran WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode AND tgl_keluar=@tgl_keluar AND jmlh_keluar=@jmlh_keluar AND unit=@unit AND harga=@harga AND nama_bagian=@nama_bagian AND satuan=@satuan AND keterangan=@keterangan", con);
            sscmd.Parameters.AddWithValue("@ENDDA", dateMax);
            sscmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
            sscmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
            sscmd.Parameters.AddWithValue("@tgl_keluar", tgledit.Text);
            sscmd.Parameters.AddWithValue("@jmlh_keluar", jmlhkeluaredit.Text);
            sscmd.Parameters.AddWithValue("@unit", quantityedit.Text);
            sscmd.Parameters.AddWithValue("@harga", hargaedit.Text);
            sscmd.Parameters.AddWithValue("@nama_bagian", bagianDledit.SelectedItem.Value);
            sscmd.Parameters.AddWithValue("@satuan", satuanedit.Text);
            sscmd.Parameters.AddWithValue("@keterangan", keteranganedit.Value);
            con.Open();
            SqlDataReader myReader = sscmd.ExecuteReader();
            while (myReader.Read())
            {
                string result = myReader.GetValue(0).ToString();
                jmlhsaldo.Text = result.ToString();
            }
            con.Close();
            if(jmlhsaldo.Text != string.Empty)
            { 
                string message = "Terdapat pph dan nomor pada data ini. Silahkan hapus pph dan nomor pada menu Pengeluaran khusus Jasa";
                string script = "{ alert('" + message + "'); }";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
            }
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
                string message = "Data sudah tidak dapat diubah <br /> Karena saldo sudah habis dan akan mengubah data yang ada";
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
                    jmlhsaldo.Text = result.ToString();
                }
                con.Close();

                int saldoDb = Convert.ToInt32(jmlhsaldo.Text);
                int saldoBef = Convert.ToInt32(saldoedit.Text);
                int saldoAfUp = saldoDb + saldoBef;
                saldotemp.Text = saldoAfUp.ToString();

                int saldotempo = Convert.ToInt32(saldotemp.Text);
                int saldoupdate = Convert.ToInt32(jmlhkeluaredit.Text);
                int saldoupdating = saldotempo - saldoupdate;

                if (saldoupdating < 0)
                    {
                        int periode = Convert.ToInt32(periodeDledit.Text);
                        int findperiod = periode + 1;
                        string find = findperiod.ToString();

                        SqlCommand fcmd = new SqlCommand("SELECT saldo,Kas FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode>=@thn_periode", con);
                        fcmd.Parameters.AddWithValue("@ENDDA", dateMax);
                        SqlParameter[] fprms = new SqlParameter[3];

                        fprms[0] = new SqlParameter("@saldo", SqlDbType.VarChar, 50);
                        fprms[1] = new SqlParameter("@Kas", SqlDbType.VarChar, 50);
                        fprms[2] = new SqlParameter("@thn_periode", SqlDbType.VarChar, 50);
                        fprms[0].Value = jmlhkeluaredit.Text;
                        fprms[1].Value = kasDledit.SelectedItem.Value;
                        fprms[2].Value = find;
                        con.Open();
                        fcmd.Parameters.AddRange(fprms);
                        object periodefind = fcmd.ExecuteScalar();
                        con.Close();
                        if (periodefind == null)
                        {
                            updatelabel1.Text = "Jumlah pengeluaran untuk kas " + kasDledit.SelectedItem.Value + " melebihi saldo yang ada (" + saldoupdating.ToString() + "). <br /> Sisa saldo yang kurang akan dilimpahkan ke pemasukkan selanjutnya.";
                            updatelabel2.Text = "Lanjutkan ?";
                            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalUpdateMin();", true);
                        }
                        else
                        {
                            updatelabel3.Text = "Jumlah pengeluaran untuk kas " + kasDledit.SelectedItem.Value + " melebihi saldo yang ada (" + saldoupdating.ToString() + "). <br /> Sisa saldo yang kurang akan dilimpahkan kepemasukkan " + kasDledit.SelectedItem.Value + " " + find + ".";
                            updatelabel4.Text = "Yakin ingin melanjutkan ?";
                            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalUpdatePeriode();", true);
                        }
                    }
                    else
                    {
                        
                        saldoafteredit.Text = saldoupdating.ToString();

                        if (radioyaedit.Checked)
                        {
                            jasaselectededit = "Ya";
                        }
                        else if (radiotidakedit.Checked)
                        {
                            jasaselectededit = "Tidak";
                        }

                        SqlCommand cmd = new SqlCommand("INSERT INTO biocash.Pengeluaran" + "(BEGDA,Kas,tgl_keluar,keterangan,harga,unit,jmlh_keluar,thn_periode,nama_bagian,vendor,satuan,jasa,change_date,ENDDA)VALUES(@BEGDA,@Kas,@tgl_keluar,@keterangan,@harga,@unit,@jmlh_keluar,@thn_periode,@nama_bagian,@vendor,@satuan,@jasa,@change_date,@ENDDA)", con);
                        SqlCommand ucmd = new SqlCommand("UPDATE biocash.Pengeluaran SET ENDDA=@ENDDA, change_date=@change_date WHERE id=@id ", con);

                        SqlCommand scmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
                        SqlCommand sucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA=@ENDDAS AND Kas=@Kas AND thn_periode=@thn_periode ", con);

                        cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@tgl_keluar", tgledit.Text);
                        cmd.Parameters.AddWithValue("@keterangan", keteranganedit.Value);
                        cmd.Parameters.AddWithValue("@harga", hargaedit.Text);
                        cmd.Parameters.AddWithValue("@unit", quantityedit.Text);
                        cmd.Parameters.AddWithValue("@jmlh_keluar", jmlhkeluaredit.Text);
                        cmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
                        cmd.Parameters.AddWithValue("@nama_bagian", bagianDledit.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@vendor", vendoredit.Text);
                        cmd.Parameters.AddWithValue("@satuan", satuanedit.Text);
                        cmd.Parameters.AddWithValue("@jasa", jasaselectededit);
                        cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ENDDA", dateMax);

                        ucmd.Parameters.AddWithValue("@id", id.Text);
                        ucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
                        ucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

                        scmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                        scmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
                        scmd.Parameters.AddWithValue("@saldo", saldoafteredit.Text);
                        scmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
                        scmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                        scmd.Parameters.AddWithValue("@ENDDA", dateMax);

                        sucmd.Parameters.AddWithValue("@ENDDAS", dateMax);
                        sucmd.Parameters.AddWithValue("@Kas", kasDledit.Text);
                        sucmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
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
        }

        protected void updateminus_Click(object sender, EventArgs e)
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
                string message = "Data sudah tidak dapat diubah <br /> Karena saldo sudah habis dan akan mengubah data yang ada";
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
                    jmlhsaldo.Text = result.ToString();
                }
                con.Close();

                int saldoDb = Convert.ToInt32(jmlhsaldo.Text);
                int saldoBef = Convert.ToInt32(saldoedit.Text);
                int saldoAfUp = saldoDb + saldoBef;
                saldotemp.Text = saldoAfUp.ToString();

                int saldotempo = Convert.ToInt32(saldotemp.Text);
                int saldoupdate = Convert.ToInt32(jmlhkeluaredit.Text);
                int saldoupdating = saldotempo - saldoupdate;
                string saldoafteredit = saldoupdating.ToString();

                if (radioyaedit.Checked)
                {
                    jasaselectededit = "Ya";
                }
                else if (radiotidakedit.Checked)
                {
                    jasaselectededit = "Tidak";
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO biocash.Pengeluaran" + "(BEGDA,Kas,tgl_keluar,keterangan,harga,unit,jmlh_keluar,thn_periode,nama_bagian,vendor,satuan,jasa,change_date,ENDDA)VALUES(@BEGDA,@Kas,@tgl_keluar,@keterangan,@harga,@unit,@jmlh_keluar,@thn_periode,@nama_bagian,@vendor,@satuan,@jasa,@change_date,@ENDDA)", con);
                SqlCommand ucmd = new SqlCommand("UPDATE biocash.Pengeluaran SET ENDDA=@ENDDA, change_date=@change_date WHERE id=@id ", con);

                SqlCommand scmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
                SqlCommand sucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA=@ENDDAS AND Kas=@Kas AND thn_periode=@thn_periode ", con);

                cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                cmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@tgl_keluar", tgledit.Text);
                cmd.Parameters.AddWithValue("@keterangan", keteranganedit.Value);
                cmd.Parameters.AddWithValue("@harga", hargaedit.Text);
                cmd.Parameters.AddWithValue("@unit", quantityedit.Text);
                cmd.Parameters.AddWithValue("@jmlh_keluar", jmlhkeluaredit.Text);
                cmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
                cmd.Parameters.AddWithValue("@nama_bagian", bagianDledit.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@vendor", vendoredit.Text);
                cmd.Parameters.AddWithValue("@satuan", satuanedit.Text);
                cmd.Parameters.AddWithValue("@jasa", jasaselectededit);
                cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                cmd.Parameters.AddWithValue("@ENDDA", dateMax);

                ucmd.Parameters.AddWithValue("@id", id.Text);
                ucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
                ucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

                scmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                scmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
                scmd.Parameters.AddWithValue("@saldo", saldoafteredit);
                scmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
                scmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                scmd.Parameters.AddWithValue("@ENDDA", dateMax);

                sucmd.Parameters.AddWithValue("@ENDDAS", dateMax);
                sucmd.Parameters.AddWithValue("@Kas", kasDledit.Text);
                sucmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
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

        protected void updateperiode_Click(object sender, EventArgs e)
        {
            SqlCommand lcmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
            lcmd.Parameters.AddWithValue("@ENDDA", dateMax);
            lcmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
            lcmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
            con.Open();
            SqlDataReader myReader = lcmd.ExecuteReader();
            while (myReader.Read())
            {
                string result = myReader.GetValue(0).ToString();
                jmlhsaldo.Text = result.ToString();
            }
            con.Close();

            int getperiode = Convert.ToInt32(periodeDledit.Text);
            int periodesum = getperiode + 1;
            string nextperiode = periodesum.ToString();

            SqlCommand pcmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
            pcmd.Parameters.AddWithValue("@ENDDA", dateMax);
            pcmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
            pcmd.Parameters.AddWithValue("@thn_periode", nextperiode);
            con.Open();
            SqlDataReader PReader = pcmd.ExecuteReader();
            while (PReader.Read())
            {
                string result = PReader.GetValue(0).ToString();
                jsaldo.Text = result.ToString();
            }
            con.Close();

            int saldonext = Convert.ToInt32(jsaldo.Text); //25
            int saldolama = Convert.ToInt32(jmlhsaldo.Text); //1     
            int saldobaru = Convert.ToInt32(jmlhkeluaredit.Text);//26
            int subtotal = saldonext - saldobaru;

            int total = subtotal + saldonext;
            string totalperiode = total.ToString();

            if (radioyaedit.Checked)
            {
                jasaselectededit = "Ya";
            }
            else if (radiotidakedit.Checked)
            {
                jasaselectededit = "Tidak";
            }

            SqlCommand cmd = new SqlCommand("INSERT INTO biocash.Pengeluaran" + "(BEGDA,Kas,tgl_keluar,keterangan,harga,unit,jmlh_keluar,thn_periode,nama_bagian,vendor,satuan,jasa,change_date,ENDDA)values(@BEGDA,@Kas,@tgl_keluar,@keterangan,@harga,@unit,@jmlh_keluar,@thn_periode,@nama_bagian,@vendor,@satuan,@jasa,@change_date,@ENDDA)", con);
            SqlCommand incmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
            SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo set change_date=@change_date, ENDDA=@ENDDA WHERE ENDDA=@ENDDAS AND Kas=@Kas AND thn_periode=@thn_periode", con);
            SqlCommand iuncmd = new SqlCommand("UPDATE biocash.Saldo set change_date=@change_date, ENDDA=@ENDDA WHERE ENDDA=@ENDDAS AND Kas=@Kas AND thn_periode=@thn_periode", con);
            SqlCommand ucmd = new SqlCommand("UPDATE biocash.Pengeluaran SET ENDDA=@ENDDA, change_date=@change_date WHERE id=@id ", con);

            cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            cmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@tgl_keluar", tgledit.Text);
            cmd.Parameters.AddWithValue("@keterangan", keteranganedit.Value);
            cmd.Parameters.AddWithValue("@harga", hargaedit.Text);
            cmd.Parameters.AddWithValue("@unit", quantityedit.Text);
            cmd.Parameters.AddWithValue("@jmlh_keluar", jmlhkeluaredit.Text);
            cmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
            cmd.Parameters.AddWithValue("@nama_bagian", bagianDledit.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@vendor", vendoredit.Text);
            cmd.Parameters.AddWithValue("@satuan", satuanedit.Text);
            cmd.Parameters.AddWithValue("@jasa", jasaselectededit);
            cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);

            ucmd.Parameters.AddWithValue("@id", id.Text);
            ucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            ucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

            incmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            incmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
            incmd.Parameters.AddWithValue("@saldo", totalperiode);
            incmd.Parameters.AddWithValue("@thn_periode", nextperiode);
            incmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            incmd.Parameters.AddWithValue("@ENDDA", dateMax);

            iucmd.Parameters.AddWithValue("@ENDDAS", dateMax);
            iucmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
            iucmd.Parameters.AddWithValue("@thn_periode", periodeDledit.Text);
            iucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            iucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

            iuncmd.Parameters.AddWithValue("@ENDDAS", dateMax);
            iuncmd.Parameters.AddWithValue("@Kas", kasDledit.SelectedItem.Value);
            iuncmd.Parameters.AddWithValue("@thn_periode", nextperiode);
            iuncmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            iuncmd.Parameters.AddWithValue("@change_date", DateTime.Now);

            con.Open();
            iucmd.ExecuteNonQuery();
            iuncmd.ExecuteNonQuery();
            ucmd.ExecuteNonQuery();
            incmd.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "Data berhasil disimpan";
            string script = "{ alert('" + message + "'); }";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
            gvbind();
            gvBindSaldo();
            clearTextInput();
        }

        protected void RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvBioCash.DataKeys[e.RowIndex].Value.ToString());
            con.Open();

            SqlCommand scmd = new SqlCommand("SELECT Kas,jmlh_keluar,thn_periode,unit,harga,nama_bagian,vendor,satuan,keterangan,tgl_keluar FROM biocash.Pengeluaran WHERE id=@id", con);
            scmd.Parameters.AddWithValue("@id", id);
            SqlDataReader myReader = scmd.ExecuteReader();
            while (myReader.Read())
            {
                string kas = myReader.GetValue(0).ToString();
                string saldo = myReader.GetValue(1).ToString();
                string periode = myReader.GetValue(2).ToString();
                string unit = myReader.GetValue(3).ToString();
                string harga = myReader.GetValue(4).ToString();
                string bagian = myReader.GetValue(5).ToString();
                string vendor = myReader.GetValue(6).ToString();
                string satuan = myReader.GetValue(7).ToString();
                string keterangan = myReader.GetValue(8).ToString();
                string tglkeluar = myReader.GetValue(9).ToString();
                kasdel.Text = kas.ToString();
                saldodel.Text = saldo.ToString();
                periodedel.Text = periode.ToString();
                unitdel.Text = unit.ToString();
                hargadel.Text = harga.ToString();
                bagiandel.Text = bagian.ToString();
                vendordel.Text = vendor.ToString();
                satuandel.Text = satuan.ToString();
                keterangandel.Text = keterangan.ToString();
                tgldel.Text = tglkeluar.ToString();
            }
            con.Close();

            SqlCommand sscmd = new SqlCommand("SELECT pph FROM biocash.Pengeluaran WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode AND tgl_keluar=@tgl_keluar AND jmlh_keluar=@jmlh_keluar AND unit=@unit AND harga=@harga AND nama_bagian=@nama_bagian AND satuan=@satuan AND keterangan=@keterangan", con);
            sscmd.Parameters.AddWithValue("@ENDDA", dateMax);
            sscmd.Parameters.AddWithValue("@Kas", kasdel.Text);
            sscmd.Parameters.AddWithValue("@thn_periode", periodedel.Text);
            sscmd.Parameters.AddWithValue("@tgl_keluar", tgldel.Text);
            sscmd.Parameters.AddWithValue("@jmlh_keluar", saldodel.Text);
            sscmd.Parameters.AddWithValue("@unit", unitdel.Text);
            sscmd.Parameters.AddWithValue("@harga", hargadel.Text);
            sscmd.Parameters.AddWithValue("@nama_bagian", bagiandel.Text);
            sscmd.Parameters.AddWithValue("@satuan", satuandel.Text);
            sscmd.Parameters.AddWithValue("@keterangan", keterangandel.Text);
            con.Open();
            SqlDataReader mysReader = sscmd.ExecuteReader();
            while (mysReader.Read())
            {
                string result = mysReader.GetValue(0).ToString();
                jmlhsaldo.Text = result.ToString();
            }
            con.Close();
            if (jmlhsaldo.Text != string.Empty)
            {
                string message = "Terdapat pph dan nomor pada data ini. Silahkan hapus pph dan nomor pada menu Pengeluaran khusus Jasa";
                string script = "{ alert('" + message + "'); }";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
            }
            else
            {
            SqlCommand ccmd = new SqlCommand("SELECT saldo,Kas,thn_periode FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
            ccmd.Parameters.AddWithValue("@ENDDA", dateMax);
            SqlParameter[] cprms = new SqlParameter[3];

            cprms[0] = new SqlParameter("@saldo", SqlDbType.VarChar, 50);
            cprms[1] = new SqlParameter("@Kas", SqlDbType.VarChar, 50);
            cprms[2] = new SqlParameter("@thn_periode", SqlDbType.VarChar, 50);
            cprms[0].Value = jmlhsaldo.Text;
            cprms[1].Value = kasdel.Text;
            cprms[2].Value = periodedel.Text;
            con.Open();
            ccmd.Parameters.AddRange(cprms);
            object saldokas = ccmd.ExecuteScalar();
            con.Close();

                if (saldokas == null)
                {
                    string message = "Data sudah tidak bisa dihapus karena saldo sudah habis";
                    string script = "{ alert('" + message + "'); }";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
                }
                else
                {

                    con.Open();
                    SqlCommand delcmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
                    delcmd.Parameters.AddWithValue("@ENDDA", dateMax);
                    delcmd.Parameters.AddWithValue("@Kas", kasdel.Text);
                    delcmd.Parameters.AddWithValue("@thn_periode", periodedel.Text);
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

                    SqlCommand cmd = new SqlCommand("UPDATE biocash.Pengeluaran SET ENDDA=@ENDDA WHERE id=@id", con);
                    SqlCommand iscmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
                    SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA=@ENDDAS AND Kas=@Kas AND thn_periode=@thn_periode ", con);

                    iscmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                    iscmd.Parameters.AddWithValue("@Kas", kasdel.Text);
                    iscmd.Parameters.AddWithValue("@saldo", totdelsaldo.Text);
                    iscmd.Parameters.AddWithValue("@thn_periode", periodedel.Text);
                    iscmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                    iscmd.Parameters.AddWithValue("@ENDDA", dateMax);

                    iucmd.Parameters.AddWithValue("@ENDDAS", dateMax);
                    iucmd.Parameters.AddWithValue("@Kas", kasdel.Text);
                    iucmd.Parameters.AddWithValue("@thn_periode", periodedel.Text);
                    iucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
                    iucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

                    cmd.Parameters.AddWithValue("@id", id);
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

        protected void delete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand scmd = new SqlCommand("SELECT Kas,jmlh_keluar,thn_periode,unit,harga,nama_bagian,vendor,satuan,keterangan,tgl_keluar FROM biocash.Pengeluaran WHERE id=@id", con);
            scmd.Parameters.AddWithValue("@id", id.Text);
            SqlDataReader myReader = scmd.ExecuteReader();
            while (myReader.Read())
            {
                string kas = myReader.GetValue(0).ToString();
                string saldo = myReader.GetValue(1).ToString();
                string periode = myReader.GetValue(2).ToString();
                string unit = myReader.GetValue(3).ToString();
                string harga = myReader.GetValue(4).ToString();
                string bagian = myReader.GetValue(5).ToString();
                string vendor = myReader.GetValue(6).ToString();
                string satuan = myReader.GetValue(7).ToString();
                string keterangan = myReader.GetValue(8).ToString();
                string tglkeluar = myReader.GetValue(9).ToString();
                kasdel.Text = kas.ToString();
                saldodel.Text = saldo.ToString();
                periodedel.Text = periode.ToString();
                unitdel.Text = unit.ToString();
                hargadel.Text = harga.ToString();
                bagiandel.Text = bagian.ToString();
                vendordel.Text = vendor.ToString();
                satuandel.Text = satuan.ToString();
                keterangandel.Text = keterangan.ToString();
                tgldel.Text = tglkeluar.ToString();
            }
            con.Close();

            SqlCommand sscmd = new SqlCommand("SELECT pph FROM biocash.Pengeluaran WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode AND tgl_keluar=@tgl_keluar AND jmlh_keluar=@jmlh_keluar AND unit=@unit AND harga=@harga AND nama_bagian=@nama_bagian AND satuan=@satuan AND keterangan=@keterangan", con);
            sscmd.Parameters.AddWithValue("@ENDDA", dateMax);
            sscmd.Parameters.AddWithValue("@Kas", kasdel.Text);
            sscmd.Parameters.AddWithValue("@thn_periode", periodedel.Text);
            sscmd.Parameters.AddWithValue("@tgl_keluar", tgldel.Text);
            sscmd.Parameters.AddWithValue("@jmlh_keluar", saldodel.Text);
            sscmd.Parameters.AddWithValue("@unit", unitdel.Text);
            sscmd.Parameters.AddWithValue("@harga", hargadel.Text);
            sscmd.Parameters.AddWithValue("@nama_bagian", bagiandel.Text);
            sscmd.Parameters.AddWithValue("@satuan", satuandel.Text);
            sscmd.Parameters.AddWithValue("@keterangan", keterangandel.Text);
            con.Open();
            SqlDataReader mysReader = sscmd.ExecuteReader();
            while (mysReader.Read())
            {
                string result = mysReader.GetValue(0).ToString();
                jmlhsaldo.Text = result.ToString();
            }
            con.Close();
            if (jmlhsaldo.Text != string.Empty)
            {
                string message = "Terdapat pph dan nomor pada data ini. Silahkan hapus pph dan nomor pada menu Pengeluaran khusus Jasa";
                string script = "{ alert('" + message + "'); }";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
            }
            else
            {
                SqlCommand ccmd = new SqlCommand("SELECT saldo,Kas,thn_periode FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
            ccmd.Parameters.AddWithValue("@ENDDA", dateMax);
            SqlParameter[] cprms = new SqlParameter[3];

            cprms[0] = new SqlParameter("@saldo", SqlDbType.VarChar, 50);
            cprms[1] = new SqlParameter("@Kas", SqlDbType.VarChar, 50);
            cprms[2] = new SqlParameter("@thn_periode", SqlDbType.VarChar, 50);
            cprms[0].Value = jmlhsaldo.Text;
            cprms[1].Value = kasdbtext.Text;
            cprms[2].Value = periodedel.Text;
            con.Open();
            ccmd.Parameters.AddRange(cprms);
            object saldokas = ccmd.ExecuteScalar();
            con.Close();

                if (saldokas == null)
                {
                    string message = "Data sudah tidak bisa dihapus karena saldo sudah habis";
                    string script = "{ alert('" + message + "'); }";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
                }
                else
                {

                    con.Open();
                    SqlCommand delcmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
                    delcmd.Parameters.AddWithValue("@ENDDA", dateMax);
                    delcmd.Parameters.AddWithValue("@Kas", kasdel.Text);
                    delcmd.Parameters.AddWithValue("@thn_periode", periodedel.Text);
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

                    SqlCommand cmd = new SqlCommand("UPDATE biocash.Pengeluaran SET ENDDA=@ENDDA WHERE id=@id", con);
                    SqlCommand iscmd = new SqlCommand("INSERT INTO biocash.Saldo" + "(BEGDA,Kas,saldo,thn_periode,change_date,ENDDA)values(@BEGDA,@Kas,@saldo,@thn_periode,@change_date,@ENDDA)", con);
                    SqlCommand iucmd = new SqlCommand("UPDATE biocash.Saldo SET ENDDA=@ENDDA, change_date=@change_date WHERE ENDDA=@ENDDAS AND Kas=@Kas AND thn_periode=@thn_periode ", con);

                    iscmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                    iscmd.Parameters.AddWithValue("@Kas", kasdel.Text);
                    iscmd.Parameters.AddWithValue("@saldo", totdelsaldo.Text);
                    iscmd.Parameters.AddWithValue("@thn_periode", periodedel.Text);
                    iscmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                    iscmd.Parameters.AddWithValue("@ENDDA", dateMax);

                    iucmd.Parameters.AddWithValue("@ENDDAS", dateMax);
                    iucmd.Parameters.AddWithValue("@Kas", kasdel.Text);
                    iucmd.Parameters.AddWithValue("@thn_periode", periodedel.Text);
                    iucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
                    iucmd.Parameters.AddWithValue("@change_date", DateTime.Now);

                    cmd.Parameters.AddWithValue("@id", id.Text);
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
}