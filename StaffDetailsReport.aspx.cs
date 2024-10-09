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

public partial class StaffDetailsReport : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblname.Text = "Hi, " + Session["Name"].ToString();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        SqlConnection connMenu = BusinessTier.getConnection();
        connMenu.Open();
        try
        {
            StiWebViewer1.Visible = false;

            if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
            {
               
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
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "StaffDetailsReport", "Page_Init", ex.ToString(), "Audit");          
            Response.Redirect("Login.aspx");
        }
        finally
        {
            connMenu.Close();
        }

    }

    protected void cboBranch_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            sql1 = " select COUNTRY_ID, upper(Location) as Loc from Vw_Staff_Official where DELETED=0 and COUNTRY_ID <>0 and [Location] LIKE '' + @text + '%'  group by COUNTRY_ID,Location order by Location";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            //RadComboBoxItem item1 = new RadComboBoxItem();
            //item1.Text = "---All---";
            //item1.Value = "0";
            //comboBox.Items.Add(item1);
            //item1.DataBind();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Loc"].ToString().Trim();
                item.Value = row["COUNTRY_ID"].ToString().Trim();
                //item.Attributes.Add("Project_Name", row["Project_Name"].ToString().Trim());

                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            //cboProjectName.Text="---All---";
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "StaffDetailsReport", "cboBranch_OnItemsRequested", ex.ToString(), "Audit");        
        }

    }

    protected void cboDept_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            sql1 = " select DEPT_NAME,DEPT_ID from Vw_Staff_Official where DELETED=0 and DEPT_ID <> 0 and [DEPT_NAME] LIKE '' + @text + '%'  group by DEPT_ID,DEPT_NAME order by DEPT_NAME ";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();           
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["DEPT_NAME"].ToString().Trim();
                item.Value = row["DEPT_ID"].ToString().Trim();  
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose(); 
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "StaffDetailsReport", "cboDept_OnItemsRequested", ex.ToString(), "Audit"); 
        }
            }
    
    protected void Onclick_btnSubmit(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        try
        {
            string vinaval = "";
            string con = BusinessTier.getConnection1();

            StiWebViewer1.Visible = true;
            string appPath = Request.PhysicalApplicationPath;
            string kuttalvinaval = string.Empty;
            vinaval = "Select * from Vw_Staff_Official where Deleted=0 ";

            if (string.IsNullOrEmpty(cboBranch.Text.ToString().Trim()))
                kuttalvinaval = "";
            else
            {
                kuttalvinaval = "COUNTRY_ID=" + Convert.ToInt32(cboBranch.SelectedValue.ToString().Trim());
                vinaval = vinaval + "and " + kuttalvinaval;
            }

            if (string.IsNullOrEmpty(cboDept.Text.ToString().Trim()))
                kuttalvinaval = "";
            else
            {
                kuttalvinaval = "DEPT_ID=" + Convert.ToInt32(cboDept.SelectedValue.ToString().Trim());
                vinaval = vinaval + "and " + kuttalvinaval;
            }          

            Stimulsoft.Report.StiReport stiReport1;
            SqlDataAdapter ad1 = new SqlDataAdapter(vinaval, con);
            DataSet ds1 = new DataSet();
            ds1.DataSetName = "DynamicDataSource1";
            ds1.Tables.Add("Vw_Staff_Official");
            ad1.Fill(ds1, "Vw_Staff_Official");

            stiReport1 = new StiReport();
            stiReport1.Dictionary.DataStore.Clear();
            stiReport1.Load(appPath + "\\Reports\\StaffDetails.mrt");
            stiReport1.Dictionary.Databases.Clear();
            stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
            stiReport1.Dictionary.DataSources.Clear();
            stiReport1.RegData("Vw_Staff_Official", ds1);
            stiReport1.Dictionary.Synchronize();
            stiReport1.Compile();
            StiWebViewer1.Report = stiReport1;
            StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
            stiReport1.Dispose();
        }

        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "StaffDetailsReport", "Onclick_btnSubmit", ex.ToString(), "Audit");
            return;
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