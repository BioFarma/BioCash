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
    public partial class Laporan : System.Web.UI.Page
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        DateTime dateMax = new DateTime(9999, 12, 31, 00, 00, 00);
        protected void Page_Load(object sender, EventArgs e)
        {
            con.ConnectionString = "Data Source=MSI;Initial Catalog=BioCash;Persist Security Info=True;User ID=sa;Password=@Gtabp1000";
            gvbind();
        }

        protected void gvbind()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Pengeluaran WHERE ENDDA=@ENDDA", con);
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);
            //SqlCommand cmd = new SqlCommand("SELECT biocash.pemasukkan.id , biocash.pemasukkan.tgl_masuk, biocash.pemasukkan.thn_periode, biocash.pemasukkan.Unit, biocash.pemasukkan.jmlh_masuk FROM biocash.pemasukkan INNER JOIN biocash.Saldo ON biocash.pemasukkan.Unit=biocash.Saldo.Unit WHERE biocash.pemasukkan.ENDDA='"+dateMax+"' AND biocash.pemasukkan.Unit=biocash.Saldo.Unit", con);
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
    }
}