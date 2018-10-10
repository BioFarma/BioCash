using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BioTemplate.Pages
{
    public partial class Masterkas : System.Web.UI.Page
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        DateTime dateMax = new DateTime(9999, 12, 31, 00, 00, 00);

        protected void Page_Load(object sender, EventArgs e)
        {
            con.ConnectionString = "Data Source=LAPTOP-LUGIMV8T;Initial Catalog=BioCash;User ID=sa;Password=@Gtabp1000";
            if (!IsPostBack)
            {
                gvbind();
            }
        }

        protected void gvbind()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Masterkas WHERE ENDDA='"+dateMax+"'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBioCash.DataSource = ds;
                gvBioCash.DataBind();
            }
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
                
                SqlCommand scmd = new SqlCommand("SELECT *FROM biocash.Masterkas WHERE Kas = @Kas AND kd_kas=@kd_kas AND ENDDA=@ENDDA", con);

                SqlParameter[] prms = new SqlParameter[3];
            
                prms[0] = new SqlParameter("@Kas", SqlDbType.VarChar, 50);
                prms[1] = new SqlParameter("@kd_kas", SqlDbType.VarChar, 50);
                prms[2] = new SqlParameter("@ENDDA", SqlDbType.VarChar, 50);
                prms[0].Value = Kas.Text;
                prms[1].Value = kd_kas.Text;
                prms[2].Value = dateMax;
                con.Open();
                scmd.Parameters.AddRange(prms);
                object obj = scmd.ExecuteScalar();
                if (obj != null)
                {
                    string message = "Data sudah ada";
                    string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "WarningMessage", script, true);
                    obj.ToString();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into biocash.Masterkas (kd_kas,Kas,Plafond,nosk,BEGDA,ENDDA,change_date)values(@kd_kas,@Kas,@plafond,@nosk,@BEGDA,@ENDDA,@change_date)", con);
                
                    cmd.Parameters.AddWithValue("@kd_kas", kd_kas.Text);
                    cmd.Parameters.AddWithValue("@Kas", Kas.Text);
                    cmd.Parameters.AddWithValue("@Plafond", plafond.Text);
                    cmd.Parameters.AddWithValue("@nosk", nosk.Text);
                    cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ENDDA", dateMax);
                    cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    string message = "Data berhasil ditambah";
                    string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                    gvbind();
                }
                con.Close();
        }

        protected void RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvBioCash.DataKeys[e.RowIndex].Value.ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE biocash.Masterkas SET ENDDA=@ENDDA WHERE id='" + id + "'", con);
            cmd.Parameters.AddWithValue("@ENDDA",DateTime.Now);
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "Data berhasil dihapus";
            string script = "window.onload = function(){ alert('"; script += message; script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            gvbind();
        }
        

        protected void Update_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into biocash.Masterkas (kd_kas,Kas,Plafond,nosk,BEGDA,ENDDA,change_date)values(@kd_kas,@Kas,@Plafond,@nosk,@BEGDA,@ENDDA,@change_date)", con);
            SqlCommand ucmd = new SqlCommand("UPDATE biocash.Masterkas SET ENDDA=@ENDDA, change_date=@change_date WHERE id=@id", con);

            //Insert new Value as Newest Update
            cmd.Parameters.AddWithValue("@kd_kas", KdKasEdit.Text);
            cmd.Parameters.AddWithValue("@Kas", KasEdit.Text);
            cmd.Parameters.AddWithValue("@Plafond", plafondEdit.Text);
            cmd.Parameters.AddWithValue("@nosk", noskEdit.Text);
            cmd.Parameters.AddWithValue("@BEGDA", DateTime.Now);
            cmd.Parameters.AddWithValue("@ENDDA", dateMax);
            cmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            //Update Value
            ucmd.Parameters.AddWithValue("@id", id.Text);
            ucmd.Parameters.AddWithValue("@ENDDA", DateTime.Now);
            ucmd.Parameters.AddWithValue("@change_date", DateTime.Now);
            con.Open();
            cmd.ExecuteNonQuery();
            ucmd.ExecuteNonQuery();
            con.Close();
            string message = "Data berhasil diupdate";
            string script = "window.onload = function(){ alert('"; script += message; script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            gvbind();
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvBioCash.Rows[rowIndex];

            id.Text = (row.FindControl("Idkaslabel") as Label).Text;
            KdKasEdit.Text = (row.FindControl("Kdkaslabel") as Label).Text;
            KasEdit.Text = (row.FindControl("Kaslabel") as Label).Text;
            plafondEdit.Text = (row.FindControl("plafondlabel") as Label).Text;
            noskEdit.Text = (row.FindControl("nosklabel") as Label).Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }
    }
}