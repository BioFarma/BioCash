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
                dlunit();
            }
        }

        protected void dlunit()
        {
            SqlCommand cmd = new SqlCommand("SELECT Unit FROM biocash.Masterunit WHERE ENDDA='" + dateMax + "'", con); // table name 
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);  // fill dataset
            unitdrop.DataTextField = ds.Tables[0].Columns["Unit"].ToString(); // text field name of table dispalyed in dropdown
            unitdrop.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            unitdrop.DataBind();  //binding dropdownlist
            //kaskeciledit.DataTextField = ds.Tables[0].Columns["Unit"].ToString(); // text field name of table dispalyed in dropdown
            //kaskeciledit.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            //kaskeciledit.DataBind();  //binding dropdownlist
        }
    }
}