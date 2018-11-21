using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BioTemplate.Pages
{
    public partial class Laporan : System.Web.UI.Page
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        DateTime dateMax = new DateTime(9999, 12, 31, 00, 00, 00);
        int sumdebit, sumkredit = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            con.ConnectionString = "Data Source=MSI;Initial Catalog=BioCash;Persist Security Info=True;User ID=sa;Password=@Gtabp1000";

            if (!IsPostBack)
            {
                gvbind();
                dlkas();
            }
        }

        protected void gvbind()
        {
            con.Open();
            //SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Pengeluaran WHERE ENDDA=@ENDDA AND pph IS NOT NULL", con);
            SqlCommand cmd = new SqlCommand("SELECT biocash.Pengeluaran.tgl_keluar,biocash.Pengeluaran.Kas,CASE WHEN biocash.Pengeluaran.pph IS NULL THEN biocash.Pengeluaran.keterangan ELSE 'Jasa ' +biocash.Pengeluaran.keterangan+ ' PPh (Pasal 23)' END AS keterangan ,biocash.Pengeluaran.satuan,biocash.Pengeluaran.unit,biocash.Pengeluaran.nama_bagian,biocash.Pengeluaran.nomor,biocash.Pengeluaran.vendor,ISNULL(biocash.Pengeluaran.pph, 0) AS pph, CASE WHEN biocash.pengeluaran.pph is not null  THEN '0' ELSE biocash.Pengeluaran.jmlh_keluar END AS jmlh_keluar,biocash.Pengeluaran.jasa, biocash.Saldo.saldo FROM biocash.Pengeluaran INNER JOIN biocash.Saldo ON biocash.Pengeluaran.Kas = biocash.Saldo.Kas AND biocash.Pengeluaran.ENDDA = @ENDDA WHERE biocash.Pengeluaran.BEGDA = biocash.Saldo.BEGDA AND biocash.Pengeluaran.Kas = biocash.Saldo.Kas ORDER BY biocash.Pengeluaran.tgl_keluar ASC", con);
        
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Close();
            using (DataTable dt = new DataTable())
            {
                da.Fill(dt);
                gvBioCash.DataSource = dt;
                gvBioCash.DataBind();
            }
        }

        protected void dlkas()
        {
            SqlCommand cmd = new SqlCommand("SELECT Kas FROM biocash.Masterkas WHERE ENDDA=@ENDDA", con); // table name 
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);  // fill dataset
            kasdl.DataTextField = ds.Tables[0].Columns["Kas"].ToString(); // text field name of table dispalyed in dropdown
            kasdl.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            kasdl.DataBind();  //binding dropdownlist
            kasdl.Items.Insert(0, "--Pilih kas--");
        }

        protected void kasdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT thn_periode FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas", con);
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);
            cmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);  // fill dataset
            periodedl.DataTextField = ds.Tables[0].Columns["thn_periode"].ToString(); // text field name of table dispalyed in dropdown
            periodedl.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            periodedl.DataBind();  //binding dropdownlist
            periodedl.Items.Insert(0, "--Pilih periode--");
            con.Close();

            con.Open();
            //SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Pengeluaran WHERE ENDDA=@ENDDA AND pph IS NOT NULL", con);
            SqlCommand gcmd = new SqlCommand("SELECT biocash.Pengeluaran.tgl_keluar,biocash.Pengeluaran.Kas,CASE WHEN biocash.Pengeluaran.pph IS NULL THEN biocash.Pengeluaran.keterangan ELSE 'Jasa ' +biocash.Pengeluaran.keterangan+ ' PPh (Pasal 23)' END AS keterangan ,biocash.Pengeluaran.satuan,biocash.Pengeluaran.unit,biocash.Pengeluaran.nama_bagian,biocash.Pengeluaran.nomor,biocash.Pengeluaran.vendor,ISNULL(biocash.Pengeluaran.pph, 0) AS pph, CASE WHEN biocash.pengeluaran.pph is not null  THEN '0' ELSE biocash.Pengeluaran.jmlh_keluar END AS jmlh_keluar ,biocash.Pengeluaran.jasa, biocash.Saldo.saldo FROM biocash.Pengeluaran INNER JOIN biocash.Saldo ON biocash.Pengeluaran.Kas = biocash.Saldo.Kas AND biocash.Pengeluaran.Kas = @Kas AND biocash.Pengeluaran.ENDDA = @ENDDA WHERE biocash.Pengeluaran.BEGDA = biocash.Saldo.BEGDA AND biocash.Pengeluaran.Kas = biocash.Saldo.Kas ORDER BY biocash.Pengeluaran.tgl_keluar ASC", con);
            gcmd.Parameters.AddWithValue("@ENDDA", dateMax);
            gcmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
            SqlDataAdapter da = new SqlDataAdapter (gcmd);
            con.Close();
            using (DataTable dt = new DataTable())
            {
                da.Fill(dt);
                gvBioCash.DataSource = dt;
                gvBioCash.DataBind();
            }

            SqlCommand lcmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
            lcmd.Parameters.AddWithValue("@ENDDA", dateMax);
            lcmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
            lcmd.Parameters.AddWithValue("@thn_periode", periodedl.SelectedItem.Value);
            con.Open();
            SqlDataReader myReader = lcmd.ExecuteReader();
            while (myReader.Read())
            {
                string result = myReader.GetValue(0).ToString();
                jsaldo.Text = result.ToString();
            }
            con.Close();

            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalFilter();", true);
        }

        protected void periodedl_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            //SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Pengeluaran WHERE ENDDA=@ENDDA AND pph IS NOT NULL", con);
            SqlCommand gcmd = new SqlCommand("SELECT biocash.Pengeluaran.tgl_keluar,biocash.Pengeluaran.Kas,CASE WHEN biocash.Pengeluaran.pph IS NULL THEN biocash.Pengeluaran.keterangan ELSE 'Jasa ' +biocash.Pengeluaran.keterangan+ ' PPh (Pasal 23)' END AS keterangan ,biocash.Pengeluaran.satuan,biocash.Pengeluaran.unit,biocash.Pengeluaran.nama_bagian,biocash.Pengeluaran.nomor,biocash.Pengeluaran.vendor,ISNULL(biocash.Pengeluaran.pph, 0) AS pph, CASE WHEN biocash.pengeluaran.pph is not null  THEN '0' ELSE biocash.Pengeluaran.jmlh_keluar END AS jmlh_keluar ,biocash.Pengeluaran.jasa, biocash.Saldo.saldo FROM biocash.Pengeluaran INNER JOIN biocash.Saldo ON biocash.Pengeluaran.Kas = biocash.Saldo.Kas AND biocash.Pengeluaran.Kas = @Kas AND biocash.Pengeluaran.thn_periode = @thn_periode AND biocash.Pengeluaran.ENDDA = @ENDDA WHERE biocash.Pengeluaran.BEGDA = biocash.Saldo.BEGDA AND biocash.Pengeluaran.Kas = biocash.Saldo.Kas ORDER BY biocash.Pengeluaran.tgl_keluar ASC", con);
            gcmd.Parameters.AddWithValue("@ENDDA", dateMax);
            gcmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
            gcmd.Parameters.AddWithValue("@thn_periode", periodedl.SelectedItem.Value);
            SqlDataAdapter da = new SqlDataAdapter(gcmd);
            con.Close();
            using (DataTable dt = new DataTable())
            {
                da.Fill(dt);
                gvBioCash.DataSource = dt;
                gvBioCash.DataBind();
            }

            SqlCommand lcmd = new SqlCommand("SELECT saldo FROM biocash.Saldo WHERE ENDDA=@ENDDA AND Kas=@Kas AND thn_periode=@thn_periode", con);
            lcmd.Parameters.AddWithValue("@ENDDA", dateMax);
            lcmd.Parameters.AddWithValue("@Kas", kasdl.SelectedItem.Value);
            lcmd.Parameters.AddWithValue("@thn_periode", periodedl.SelectedItem.Value);
            con.Open();
            SqlDataReader myReader = lcmd.ExecuteReader();
            while (myReader.Read())
            {
                string result = myReader.GetValue(0).ToString();
                jsaldo.Text = result.ToString();
            }
            con.Close();

            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalFilter();", true);
        }

        protected void gvBioCash_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBioCash.PageIndex = e.NewPageIndex;
            this.gvbind();
        }

        protected void gvBioCash_PageIndexChanged(object sender, EventArgs e)
        {
            this.gvbind();
        }

        protected void export_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvBioCash.AllowPaging = false;
                this.gvbind();

                gvBioCash.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvBioCash.HeaderRow.Cells)
                {
                    cell.BackColor = gvBioCash.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvBioCash.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvBioCash.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvBioCash.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvBioCash.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void gvBioCash_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                    sumdebit += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "pph"));
                    sumkredit += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "jmlh_keluar"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (jsaldo.Text == string.Empty)
                {
                    jsaldo.Text = "0";

                    Label pphtotal = (Label)e.Row.FindControl("pphtotal");
                    pphtotal.Text = sumdebit.ToString();
                    Label kredittotal = (Label)e.Row.FindControl("kredittotal");
                    kredittotal.Text = sumkredit.ToString();
                    Label saldototal = (Label)e.Row.FindControl("saldototal");
                    saldototal.Text = jsaldo.Text;

                    int kredit = Convert.ToInt32(kredittotal.Text);
                    int debit = Convert.ToInt32(pphtotal.Text);
                    int saldo = kredit - debit;

                    biaya.Text = sumkredit.ToString();
                    pphh.Text = sumdebit.ToString();
                    total.Text = saldo.ToString();
                }
                else
                {
                    int saldop = Convert.ToInt32(jsaldo.Text);
                    
                    Label pphtotal = (Label)e.Row.FindControl("pphtotal");
                    pphtotal.Text = sumdebit.ToString();
                    Label kredittotal = (Label)e.Row.FindControl("kredittotal");
                    kredittotal.Text = sumkredit.ToString();
                    Label saldototal = (Label)e.Row.FindControl("saldototal");
                    saldototal.Text = saldop.ToString();

                    int kredit = Convert.ToInt32(kredittotal.Text);
                    int debit = Convert.ToInt32(pphtotal.Text);
                    int saldo = kredit - debit;

                    biaya.Text = sumkredit.ToString();
                    pphh.Text = sumdebit.ToString();
                    total.Text = saldo.ToString();
                }
            }
        }
    }
}