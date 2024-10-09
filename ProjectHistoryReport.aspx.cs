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

public partial class ProjectHistoryReport : System.Web.UI.Page
{

    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblname.Text = "Hi, " + Session["Name"].ToString();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            StiWebViewer1.Visible = false;
            //if (!(IsPostBack))
            //{
            //    txtToDate.SelectedDate = DateTime.Now;
            //    int day = DateTime.Now.Day - 1;
            //    txtFromDate.SelectedDate = DateTime.Now.AddDays(-day);
            //}

            if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
            {
                SqlConnection connMenu = BusinessTier.getConnection();
                connMenu.Open();
                SqlDataReader readerMenu = BusinessTier.getMenuList(connMenu, Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                dtMenuItems.Load(readerMenu);
                BusinessTier.DisposeReader(readerMenu);
                BusinessTier.DisposeConnection(connMenu);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }

    }

    protected void cboCustomer_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            sql1 = " select upper(CUSTOMER_NAME) as CUSTOMER_NAME,CUSTOMER_ID from Vw_Master_Project where DELETED=0 and [CUSTOMER_NAME] LIKE '' + @text + '%'   group by CUSTOMER_ID,CUSTOMER_NAME order by CUSTOMER_NAME ";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["CUSTOMER_NAME"].ToString().Trim();
                item.Value = row["CUSTOMER_ID"].ToString().Trim();
                item.Attributes.Add("CUSTOMER_NAME", row["CUSTOMER_NAME"].ToString().Trim());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "ProjectHistoryReport", "cboCustomer_OnItemsRequested", ex.ToString(), "Audit");
        }

    }

    protected void cboProjectName_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            if (string.IsNullOrEmpty(cboCustomer.SelectedValue.ToString()))
                sql1 = " select upper(Project_Name) as Project_Name,Project_ID,Project_No from Vw_Master_Project where DELETED=0 and [Project_Name] LIKE '' + @text + '%' group by Project_ID,Project_Name,Project_No order by Project_Name";
            else
                sql1 = " select upper(Project_Name) as Project_Name,Project_ID,Project_No from Vw_Master_Project where DELETED=0 and [Project_Name] LIKE '' + @text + '%' and CUSTOMER_ID = " + cboCustomer.SelectedValue.ToString() + "   group by Project_ID,Project_Name,Project_No order by Project_Name";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Project_Name"].ToString().Trim();
                item.Value = row["Project_ID"].ToString().Trim();
                item.Attributes.Add("Project_Name", row["Project_Name"].ToString().Trim());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "ProjectHistoryReport", "cboProjectName_OnItemsRequested", ex.ToString(), "Audit");
        }

    }

    protected void cboProjectManager_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            if (string.IsNullOrEmpty(cboProjectName.SelectedValue.ToString()))
                sql1 = " select STAFF_NAME,Staff_ID from Vw_Master_Project where DELETED=0 and [STAFF_NAME] LIKE '' + @text + '%' group by Staff_ID,STAFF_NAME order by STAFF_NAME ";
            else
                sql1 = " select STAFF_NAME,Staff_ID from Vw_Master_Project where DELETED=0 and [STAFF_NAME] LIKE '' + @text + '%' and Project_ID= " + cboProjectName.SelectedValue.ToString() + "   group by Staff_ID,STAFF_NAME order by STAFF_NAME ";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["STAFF_NAME"].ToString().Trim();
                item.Value = row["Staff_ID"].ToString().Trim();
                item.Attributes.Add("STAFF_NAME", row["STAFF_NAME"].ToString().Trim());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "ProjectHistoryReport", "cboProjectManager_OnItemsRequested", ex.ToString(), "Audit");
        }

    }

    protected void Onclick_btnSubmit(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        try
        {
            string sql = "";
            string con = BusinessTier.getConnection1();
            StiWebViewer1.Visible = true;

            string appPath = Request.PhysicalApplicationPath;    
 
            string vinavaltittam = "";
            string vinavalvadikaiyalar = "";
            string vinavaltittammelalar = "";
            string vinavalthethi = "";            

            sql = "Select *,upper(Project_Name) as p_name,upper(CUSTOMER_NAME) as c_name from Vw_Master_Project where Deleted=0 "; //StartDate between '" + FromSalesdate + "' and '" + ToSalesDate + "' and

            if (string.IsNullOrEmpty(cboCustomer.Text.ToString().Trim()))
                vinavalvadikaiyalar = "";
            else
            {
                vinavalvadikaiyalar = "CUSTOMER_ID=" + "" + Convert.ToInt32(cboCustomer.SelectedValue.ToString().Trim()) + "";
                sql = sql + " and " + vinavalvadikaiyalar;
            }
            if (string.IsNullOrEmpty(cboProjectName.Text.ToString().Trim()))
                vinavaltittam = "";
            else
            {
                vinavaltittam = "Project_ID=" + "" + Convert.ToInt32(cboProjectName.SelectedValue.ToString().Trim()) + "";
                sql = sql + "and " + vinavaltittam;
            }

            if (string.IsNullOrEmpty(cboProjectManager.Text.ToString().Trim()))
                vinavaltittammelalar = "";
            else
            {
                vinavaltittammelalar = "STAFF_ID=" + "" + Convert.ToInt32(cboProjectManager.SelectedValue.ToString().Trim()) + "";
                sql = sql + " and " + vinavaltittammelalar;
            }

            string FromSalesdate = string.Empty;

            if (string.IsNullOrEmpty(txtToDate.SelectedDate.ToString().Trim()) && string.IsNullOrEmpty(txtFromDate.SelectedDate.ToString().Trim()))
                vinavalthethi = "";
            else
            {
                DateTime fromDate = Convert.ToDateTime(txtFromDate.SelectedDate.ToString());
                FromSalesdate = fromDate.Month + "/" + fromDate.Day + "/" + fromDate.Year;
                DateTime toDate = Convert.ToDateTime(txtToDate.SelectedDate.ToString());
                string ToSalesDate = toDate.Month + "/" + toDate.Day + "/" + toDate.Year + " " + "23:59:59";
                vinavalthethi = " StartDate between '" + FromSalesdate + "' and  '" + ToSalesDate + "'";
                sql = sql + " and " + vinavalthethi;
            }          

            Stimulsoft.Report.StiReport stiReport1;
            SqlDataAdapter ad1 = new SqlDataAdapter(sql, con);
            DataSet ds1 = new DataSet();
            ds1.DataSetName = "DynamicDataSource1";
            ds1.Tables.Add("Vw_Master_Project");
            ad1.Fill(ds1, "Vw_Master_Project");

            stiReport1 = new StiReport();
            stiReport1.Dictionary.DataStore.Clear();
            stiReport1.Load(appPath + "\\Reports\\ProjectHistory.mrt");
            stiReport1.Dictionary.Databases.Clear();
            stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
            stiReport1.Dictionary.DataSources.Clear();
            stiReport1.RegData("Vw_Master_Project", ds1);
            stiReport1.Dictionary.Synchronize();

            stiReport1.Compile();

            StiWebViewer1.Report = stiReport1;
            StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
            stiReport1.Dispose();

        }

        catch (Exception ex)
        {

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Report", "Onclick_btnSubmit", ex.ToString(), "Audit");
            return;
        }
    }

    protected void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnEmail.ToolTip = "";
        cboGroup.ToolTip = "";
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
                btnEmail.ToolTip = readergetID["Emaillist"].ToString();
                cboGroup.ToolTip = readergetID["Emaillist"].ToString();

            }
            BusinessTier.DisposeReader(readergetID);

            BusinessTier.DisposeConnection(conn);

        }


        catch (Exception ex)
        {

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "cboProject_SelectedIndexChanged", ex.ToString(), "Audit");
        }

    }

    protected void btnEmail_clik(object sender, EventArgs e)
    {
        lblStatus.Text = "";

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
      
        try
        {

            string msg = "Dear All,\n\n" + "Please Find here with Enclosed the Project History Report. " + "\r\n" + "\r\n" + "  " + "\r\n" + "  " + "\r\n" + "\r\n" + "\r\n" + "Note: " + "\r\n" + "*. Do not reply to this mail" + "\r\n" + "*. This is system generated mail " + "\r\n";        
            string strCCMail = "";
            string sql2 = "select EmailList FROM PMT_EmailList WHERE emailgroup ='" + cboGroup.Text.ToString().Trim() + "'";
            SqlCommand command2 = new SqlCommand(sql2, conn);
            SqlDataReader reader2 = command2.ExecuteReader();
            if (reader2.Read())
            {
                strCCMail = reader2["EmailList"].ToString();
            }
            BusinessTier.DisposeReader(reader2);

            if (cboGroup.Text != "None")
            {
                string sql3 = "SELECT  Ltrim(Item) as mailid FROM dbo.SplitString('" + strCCMail.ToString().Trim() + "', ',')";
                SqlCommand command3 = new SqlCommand(sql3, conn);
                SqlDataReader reader3 = command3.ExecuteReader();
                while (reader3.Read())
                {
                    string mailid = reader3["mailid"].ToString();
                    try
                    {
                        BusinessTier.SendMail(mailid, "Project Staff List", msg, StiWebViewer1.Report);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                BusinessTier.DisposeReader(reader3);
            }
            else
            {
                lblStatus.Text = "Please Select 'To Mail'";
                lblStatus.ForeColor = Color.Red;
                return;
            }
            BusinessTier.DisposeConnection(conn);

          
            lblStatus.Text = "Email Send Successfully";
            lblStatus.ForeColor = Color.Green;

        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Report", "btnEmail_clik", ex.ToString(), "Audit");
        }
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery, true);
    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }

}