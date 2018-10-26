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
            gvbind();
        }

        protected void gvbind()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Pengeluaran WHERE ENDDA=@ENDDA AND pph IS NOT NULL", con);
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
                Label pphtotal = (Label)e.Row.FindControl("pphtotal");
                pphtotal.Text = sumdebit.ToString();
                Label kredittotal = (Label)e.Row.FindControl("kredittotal");
                kredittotal.Text = sumkredit.ToString();

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