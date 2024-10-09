using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

using System.Globalization;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Web;
using Stimulsoft.Report.Viewer;
using Stimulsoft.Report.SaveLoad;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Print;
using Stimulsoft.Base;
using Stimulsoft.Controls;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Controls;
using System.IO;
using System.Web.SessionState;
using System.Runtime;
using System.Drawing.Printing;

using System.Net.Mail;
using System.IO.MemoryMappedFiles;
using System.Net;

public partial class CCMailMaster : System.Web.UI.Page
{

    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Init(object sender, EventArgs e)
    {

        try
        {
            if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
            {

                Response.Redirect("Login.aspx");
            }
            else
            {
                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                SqlDataReader readerMenu = BusinessTier.getMenuList(conn, Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                dtMenuItems.Load(readerMenu);
                BusinessTier.DisposeReader(readerMenu);

                BusinessTier.DisposeConnection(conn);
                //lblStatus.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        lblname.Text = "Hi, " + Session["Name"].ToString();

    }

    protected void btnSave_click(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (!(string.IsNullOrEmpty(txtCCMailList.Text.ToString())))
            {
                string sql1 = "update PMT_EmailList set  Emaillist = '" + txtCCMailList.Text.ToString().Trim() + "' where emailgroup ='" + cboGroup.Text.ToString().Trim() + "'";
                SqlCommand command = new SqlCommand(sql1, conn);
                command.ExecuteNonQuery();
                lblStatus.Text = "Successfully Updated";
            }
        }
        catch (Exception ex)
        {
            //Response.Redirect("Login.aspx");
        }
    }

    protected void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblStatus.Text = "";
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "select Emaillist from PMT_EmailList where emailgroup ='" + cboGroup.Text.ToString().Trim() + "'";
            SqlCommand command = new SqlCommand(sql1, conn);
            SqlDataReader readergetID = command.ExecuteReader();
            if (readergetID.Read())
            {
                txtCCMailList.Text = readergetID["Emaillist"].ToString();

            }
            BusinessTier.DisposeReader(readergetID);

            BusinessTier.DisposeConnection(conn);

        }


        catch (Exception ex)
        {

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "cboProject_SelectedIndexChanged", ex.ToString(), "Audit");
        }

    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }

}