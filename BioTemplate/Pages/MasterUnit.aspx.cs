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
    public partial class MasterUnit : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand("SELECT *FROM biocash.Masterunit WHERE ENDDA='"+dateMax+"'", con);
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
                
                SqlCommand scmd = new SqlCommand("SELECT *FROM biocash.Masterunit WHERE Unit = @Unit AND kd_unit=@kd_unit AND ENDDA=@ENDDA", con);

                SqlParameter[] prms = new SqlParameter[3];
            
                prms[0] = new SqlParameter("@Unit", SqlDbType.VarChar, 50);
                prms[1] = new SqlParameter("@kd_unit", SqlDbType.VarChar, 50);
                prms[2] = new SqlParameter("@ENDDA", SqlDbType.VarChar, 50);
                prms[0].Value = Unit.Text;
                prms[1].Value = kd_unit.Text;
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
                    SqlCommand cmd = new SqlCommand("insert into biocash.Masterunit (kd_unit,Unit,BEGDA,ENDDA,change_date)values(@kd_unit,@Unit,@BEGDA,@ENDDA,@change_date)", con);
                
                    cmd.Parameters.AddWithValue("@kd_unit", kd_unit.Text);
                    cmd.Parameters.AddWithValue("@Unit", Unit.Text);
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
            SqlCommand cmd = new SqlCommand("UPDATE biocash.Masterunit SET ENDDA=@ENDDA WHERE id='" + id + "'", con);
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
            SqlCommand cmd = new SqlCommand("insert into biocash.Masterunit (kd_unit,Unit,BEGDA,ENDDA,change_date)values(@kd_unit,@Unit,@BEGDA,@ENDDA,@change_date)", con);
            SqlCommand ucmd = new SqlCommand("UPDATE biocash.Masterunit SET ENDDA=@ENDDA, change_date=@change_date WHERE id=@id", con);

            //Insert new Value as Newest Update
            cmd.Parameters.AddWithValue("@kd_unit", KdUnitEdit.Text);
            cmd.Parameters.AddWithValue("@Unit", UnitEdit.Text);
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

            id.Text = (row.FindControl("Idunitlabel") as Label).Text;
            KdUnitEdit.Text = (row.FindControl("Kdunitlabel") as Label).Text;
            UnitEdit.Text = (row.FindControl("Unitlabel") as Label).Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }
    }
}