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
        protected void Page_Load(object sender, EventArgs e)
        {
            con.ConnectionString = "Data Source=MSI;Initial Catalog=BioCash;Persist Security Info=True;User ID=sa;Password=@Gtabp1000";
            if (!IsPostBack)
            {
                gvbind();
                dlkas();
                dlbagian();
                
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
            satuanedit.Text = (row.FindControl("satuanlabel") as Label).Text;
            pphedit.Text = (row.FindControl("pphlabel") as Label).Text;
            saldoedit.Text = jmlhkeluaredit.Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalEdit();", true);
        }

        protected void Update_Click(object sender, EventArgs e)
        {

        }

        protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void delete_Click(object sender, EventArgs e)
        {

        }
    }
}