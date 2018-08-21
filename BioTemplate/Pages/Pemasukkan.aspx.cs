using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BioTemplate.Pages
{
    public partial class Pemasukkan : System.Web.UI.Page
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        private object tgl_masuk_label;

        protected void Page_Load(object sender, EventArgs e)
        {
          
            con.ConnectionString = "Data Source=LAPTOP-LUGIMV8T;Initial Catalog=BioCash;User ID=sa;Password=@Gtabp1000";
            con.Open();
           
        }


        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Masuk.Text == String.Empty)
            {
                tgl_masuk_label = "bla bla bla"; 
            } else { 
             
            SqlCommand cmd = new SqlCommand("insert into biocash.pemasukkan" + "(BEGDA,kas_kecil,tgl_masuk,thn_periode,jmlh_masuk)values(@BEGDA,@kas_kecil,@tgl_masuk,@thn_periode,@jmlh_masuk)", con);

            cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            cmd.Parameters.AddWithValue("@kas_kecil",kas_kecil.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@tgl_masuk", tgl_masuk.Text);
            cmd.Parameters.AddWithValue("@thn_periode", periode_masuk.Text);
            cmd.Parameters.AddWithValue("@jmlh_masuk", Masuk.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            Masuk.Text = String.Empty;
            tgl_masuk.Text = String.Empty;
            periode_masuk.Text = String.Empty;
            string message = "Your details have been saved successfully.";
            string script = "window.onload = function(){ alert('";script += message; script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

            }
        }
        
    }
}