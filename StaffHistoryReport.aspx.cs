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

public partial class StaffHistoryReport : System.Web.UI.Page
{

    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //Session["sesUserID"] = 6;
            //if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
            if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
            {
                //if (!(IsPostBack))
                //{
                //    txtToDate.SelectedDate = DateTime.Now;
                //    int day = DateTime.Now.Day - 1;
                //    txtFromDate.SelectedDate = DateTime.Now.AddDays(-day);
                //}
                SqlConnection connSave = BusinessTier.getConnection();
                connSave.Open();
                //int iflg = BusinessTier.InsertLogTable(connSave, Session["sesUserID"].ToString(), "UpdateVal");
                connSave.Close();
                Response.Redirect("Login.aspx");
            }
            else
            {
                //lblStatus.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        lblname.Text = "Hi, " + Session["Name"].ToString();

    }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            StiWebViewer1.Visible = false;

            if (!(IsPostBack))
            {
                txtToDate.SelectedDate = DateTime.Now;
                int day = DateTime.Now.Day - 1;
                txtFromDate.SelectedDate = DateTime.Now.AddDays(-day);
            }
        

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
                SqlConnection connSave = BusinessTier.getConnection();
                connSave.Open();
                //int iflg = BusinessTier.InsertLogTable(connSave, Session["sesUserID"].ToString(), "UpdateVal");
                connSave.Close();
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
            //SqlConnection connSave = BusinessTier.getConnection();
            //connSave.Open();
            //int iflg = BusinessTier.InsertLogTable(connSave, Session["sesUserID"].ToString(), "UpdateVal");
            //connSave.Close();
            Response.Redirect("Login.aspx");
        }

    }

    protected void cboStaffName_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();

            string sql1 = "";
            sql1 = " select STAFF_NAME,Staff_ID from Vw_AssignStaff where DELETED=0  order by STAFF_NAME";
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
                item.Attributes.Add("Project_Name", row["STAFF_NAME"].ToString().Trim());
              
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);

        }


        catch (Exception ex)
        {
            //InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "cboProject_ItemrequestedChanged", ex.ToString(), "Audit");
            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery, true);
    }

    protected void Onclick_btnSubmit(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        try
        {


           if (!(string.IsNullOrEmpty(cboStaffName.SelectedValue.ToString().Trim())))
           {
                
           string con = BusinessTier.getConnection1();
           StiWebViewer1.Visible = true;
           string appPath = Request.PhysicalApplicationPath;

           if ((string.IsNullOrEmpty(txtFromDate.ToString().Trim())))
           {
               lblStatus.Text = "Select From date";
               return;
           }

           DateTime fromDate = Convert.ToDateTime(txtFromDate.SelectedDate.ToString());
           string FromSalesdate = fromDate.Month + "/" + fromDate.Day + "/" + fromDate.Year;

           if ((string.IsNullOrEmpty(txtToDate.ToString())))
           {
               lblStatus.Text = "Select To date";
               return;
           }
          
           DateTime toDate = Convert.ToDateTime(txtToDate.SelectedDate.ToString());
           string ToSalesDate = toDate.Month + "/" + toDate.Day + "/" + toDate.Year + "";
           //string ToSalesDate = toDate.Month + "/" + toDate.Day + "/" + toDate.Year + " " + "23:59:59";

        

           string sql = "";

           //sql = "Select * from Vw_AssignStaff where ((AssignStartDt between '" + FromSalesdate + "' and '" + ToSalesDate + "') or (AssignEndtDt between '" + FromSalesdate + "' and '" + ToSalesDate + "')) and Staff_ID='" + Convert.ToInt32(cboStaffName.SelectedValue.ToString().Trim()) + "' ";

           sql = "Select  '" + FromSalesdate + "' as fromdt,'" + ToSalesDate + "' as todt,STAFF_NAME,DEPT_NAME,DateOfBirth,CONTACT_NO,EMAIL,Designation,Project_Name,Project_Manager,Location,StartDate,EndDate,AssignStartDt,AssignEndtDt from Vw_AssignStaff  where ((AssignStartDt between '" + FromSalesdate + "' and '" + ToSalesDate + "') or (AssignEndtDt between '" + FromSalesdate + "' and '" + ToSalesDate + "')) and Staff_ID='" + Convert.ToInt32(cboStaffName.SelectedValue.ToString().Trim()) + "' ";


           Stimulsoft.Report.StiReport stiReport1;
           SqlDataAdapter ad1 = new SqlDataAdapter(sql, con);
           DataSet ds1 = new DataSet();
           ds1.DataSetName = "DynamicDataSource1";
           ds1.Tables.Add("Vw_AssignStaff");
           ad1.Fill(ds1, "Vw_AssignStaff");
        
           stiReport1 = new StiReport();
           stiReport1.Dictionary.DataStore.Clear();
           stiReport1.Load(appPath + "\\Reports\\StaffHistory.mrt");
           stiReport1.Dictionary.Databases.Clear();
           stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
           stiReport1.Dictionary.DataSources.Clear();
           stiReport1.RegData("Vw_AssignStaff", ds1);
           stiReport1.Dictionary.Synchronize();

           //Stimulsoft.Report.Dictionary.StiDataParameter Parameters = new Stimulsoft.Report.Dictionary.StiDataParameter();

           //stiReport1.CompiledReport.DataSources["Vw_AssignStaff"].Parameters["@From"].ParameterValue = FromSalesdate;
           //stiReport1.CompiledReport.DataSources["Vw_AssignStaff"].Parameters["@To"].ParameterValue = ToSalesDate;

           //stiReport1.Dictionary.Variables["From"].Value = FromSalesdate;
           //stiReport1.Dictionary.Variables["To"].Value = ToSalesDate;
                               
           stiReport1.Compile();
           
           StiWebViewer1.Report = stiReport1;
           StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
            }
            else
            {
                lblStatus.Text = "Select Staff Name";


            }
        }
        catch (Exception ex)
        {

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Report", "Onclick_btnSubmit", ex.ToString(), "Audit");
            return;
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