using System;
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

using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI.Calendar;
//using NCalc;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Web.Configuration;
using System.Net;
using System.Text;
using System.Net.Mail;

public partial class AssignStaff : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    //DataSet ds;

    //DataTable Dt;

    //--------------------------< Page >--------------------------------------

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            // if (!IsPostBack)
            //{
            if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
            {
                SqlConnection connMenu = BusinessTier.getConnection();
                connMenu.Open();
                SqlDataReader readerMenu = BusinessTier.getMenuList(connMenu, Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                dtMenuItems.Load(readerMenu);
                BusinessTier.DisposeReader(readerMenu);
                BusinessTier.DisposeConnection(connMenu);
                //DtInvoicedt.SelectedDate = DateTime.Now;
                DateTime dt = DateTime.Now;
                DateTime dt1 = dt.AddDays(30);

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            btnclikrjt.Visible = false;

        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e1)
    {
        try
        {
            if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
            {
                Response.Redirect("Login.aspx");
            }

            lblname.Text = "Hi, " + Session["Name"].ToString();

        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    //--------------------------< Combobox >--------------------------------------

    protected void cboCompany_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Country_Code,COUNTRY_NAME,Location from Master_Country where DELETED=0  and [COUNTRY_NAME] LIKE '%' + @text + '%' order by COUNTRY_NAME";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Location"].ToString();
                item.Value = row["Country_Code"].ToString();
                item.Attributes.Add("COUNTRY_NAME", row["COUNTRY_NAME"].ToString());
                item.Attributes.Add("Country_Code", row["Country_Code"].ToString());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
            // Session["rdobutton"] = "";
        }
        catch (Exception ex)
        {

            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    protected void cboProject_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            //if (string.IsNullOrEmpty(cboCompany.SelectedValue))
            sql1 = " select Project_Name,STAFF_NAME,Project_ID,Project_No from Vw_Master_Project where DELETED=0 and [Project_Name] LIKE '%' + @text + '%'  order by Project_Name";
            //else
            //    sql1 = " select Project_Name,STAFF_NAME,Project_ID,Project_No from Vw_Master_Project where Company_Id=" + Convert.ToInt32(cboCompany.SelectedValue) + " and  DELETED=0 and [Project_Name] LIKE '%' + @text + '%'  order by Project_Name";
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
                item.Attributes.Add("Project_No", row["Project_No"].ToString().Trim());
                item.Attributes.Add("STAFF_NAME", row["STAFF_NAME"].ToString().Trim());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);

        }


        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "AssignStaff", "cboProject_ItemrequestedChanged", ex.ToString(), "Audit");
            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    protected void cboProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        cboDesignation.Items.Clear();
        lblStatus.Text = string.Empty;
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "select StartDate,EndDate,Project_Name,CUSTOMER_NAME,Project_No,STAFF_NAME from Vw_Master_Project where DELETED=0 and [Project_ID] ='" + cboProject.SelectedValue.ToString() + "'";
            SqlCommand command = new SqlCommand(sql1, conn);
            SqlDataReader readergetID = command.ExecuteReader();
            if (readergetID.Read())
            {
                DtStart.SelectedDate = Convert.ToDateTime(readergetID["StartDate"].ToString().Trim());
                DtEnd.SelectedDate = Convert.ToDateTime(readergetID["EndDate"].ToString().Trim());

                string[] strstart = (DtStart.SelectedDate.ToString().Trim()).Split(' ');
                string[] strEnd = (DtEnd.SelectedDate.ToString().Trim()).Split(' ');
                txtProjectInfo.Text = "Project Manager: " + readergetID["STAFF_NAME"].ToString().Trim() + Environment.NewLine + "StartDate: " + Convert.ToDateTime(strstart[0]).ToString("dd/MMM/yyyy") + "   EndDate :" + Convert.ToDateTime(strEnd[0]).ToString("dd/MMM/yyyy") + Environment.NewLine + "Project No. : " + readergetID["Project_No"].ToString().Trim() + Environment.NewLine + "Customer : " + readergetID["CUSTOMER_NAME"].ToString().Trim();

            }
            BusinessTier.DisposeReader(readergetID);
            BusinessTier.DisposeConnection(conn);
            RadGridAssignStaff.DataSource = DataSourceHelper(cboProject.SelectedValue.ToString());
            RadGridAssignStaff.Rebind();
            RadGridFreelancer.DataSource = DataSourceHelper2(cboProject.SelectedValue.ToString());
            RadGridFreelancer.Rebind();
        }


        catch (Exception ex)
        {

            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    protected void cboStaffId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            if (string.IsNullOrEmpty(cboCompany.SelectedValue))
                sql1 = "select Staff_Id,Staff_Name,Staff_no,Designation,EMAIL from Vw_Staff_Official where DELETED=0 and EMPType<>'FreeLancer' and [Staff_Name] LIKE '%' + @text + '%' order by Staff_Name";
            else
                sql1 = "select Staff_Id,Staff_Name,Staff_no,Designation,EMAIL from Vw_Staff_Official where Country_Code='" + cboCompany.SelectedValue + "' and DELETED=0 and EMPType<>'FreeLancer' and [Staff_Name] LIKE '%' + @text + '%' and STAFF_ID NOT IN (SELECT STAFF_ID FROM AssignStaff where project_id = '" + cboProject.SelectedValue + "') order by Staff_Name";

            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Staff_Name"].ToString();
                item.Value = row["Staff_Id"].ToString();
                item.Attributes.Add("Designation", row["Designation"].ToString());
                item.Attributes.Add("Staff_no", row["Staff_no"].ToString());
                item.Attributes.Add("EMAIL", row["EMAIL"].ToString());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
            // Session["rdobutton"] = "";
        }


        catch (Exception ex)
        {

            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    protected void cboFreeLanceStaffId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            //if (string.IsNullOrEmpty(cboProject.SelectedValue))
            //{
            //    lblStatus.Text = "** Please Select Project / Contract **";
            //}
               // sql1 = "select Staff_Id,Staff_Name,Staff_no,Designation,EMAIL from Vw_Staff_Official where EMPType='FreeLancer' and DELETED=0 and [Staff_Name] LIKE '%' + @text + '%' order by Staff_Name";
            //else
            sql1 = "select Staff_Id,Staff_Name,Staff_no,Designation,EMAIL from Vw_Staff_Official where EMPType='FreeLancer' and DELETED=0 and [Staff_Name] LIKE '%' + @text + '%' and STAFF_ID NOT IN (SELECT STAFF_ID FROM FreeLancer where project_id = '" + cboProject.SelectedValue + "' and DELETED=0) order by Staff_Name";

            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Staff_Name"].ToString();
                item.Value = row["Staff_Id"].ToString();
                item.Attributes.Add("Designation", row["Designation"].ToString());
                item.Attributes.Add("Staff_no", row["Staff_no"].ToString());
                item.Attributes.Add("EMAIL", row["EMAIL"].ToString());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
            // Session["rdobutton"] = "";
        }


        catch (Exception ex)
        {

            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    protected void cboDesignation_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            //if (string.IsNullOrEmpty(cboCompany.SelectedValue))
            //    sql1 = "select Staff_Id,Staff_Name,Staff_no,Skill from Vw_Staff_Details where DELETED=0 and [Staff_Name] LIKE '%' + @text + '%' order by Staff_Name";
            //else
            sql1 = "select Project_Contact_id,Designation,Manpower from Master_Project_Contact where Project_Id=" + Convert.ToInt32(cboProject.SelectedValue) + " and DELETED=0 and [Designation] LIKE '%' + @text + '%' order by Designation";

            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Designation"].ToString();
                item.Value = row["Project_Contact_id"].ToString();
                item.Attributes.Add("Manpower", row["Manpower"].ToString());
                //item.Attributes.Add("Staff_no", row["Staff_no"].ToString());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
            // Session["rdobutton"] = "";
        }


        catch (Exception ex)
        {

            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    protected void cboStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(cboProject.Text))
            {
                lblStatus.Text = "Project Field Cannot be Empty";
                return;
            }

            if (string.IsNullOrEmpty(cboStaff.Text))
            {
                lblStatus.Text = "Staff Field Cannot be Empty";
                return;
            }
            imgRed.Visible = false;
            imgGreen.Visible = true;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql11 = "select AssignEndtDt from Vw_AssignStaff where DELETED=0 and [Staff_ID] ='" + cboStaff.SelectedValue.ToString() + "'";
            SqlCommand command1 = new SqlCommand(sql11, conn);
            SqlDataReader readergetID1 = command1.ExecuteReader();
            if (readergetID1.Read())
            {
                DateTime eDate, sDate;
                eDate = Convert.ToDateTime(readergetID1["AssignEndtDt"].ToString().Trim());
                sDate = DtStart.SelectedDate.Value;
                if (eDate >= sDate)
                {
                    imgRed.Visible = true;
                    imgGreen.Visible = false;
                }
            }
            BusinessTier.DisposeReader(readergetID1);


            string sql1 = "select Skill from Vw_Staff_Details where DELETED=0 and [Staff_ID] ='" + cboStaff.SelectedValue.ToString() + "'";
            SqlCommand command = new SqlCommand(sql1, conn);
            SqlDataReader readergetID = command.ExecuteReader();
            if (readergetID.Read())
            {
                txtStaffHistory.Text = "Skill: " + readergetID["Skill"].ToString().Trim() + Environment.NewLine + "----------------------------";
            }
            BusinessTier.DisposeReader(readergetID);


            string sql2 = "select Project_Name,AssignStartDt,AssignEndtDt from Vw_AssignStaff where DELETED=0 and [Staff_ID] ='" + cboStaff.SelectedValue.ToString() + "' and Assign_id=" + "(select max(assign_id) as assignID from AssignStaff where staff_id='" + cboStaff.SelectedValue.ToString() + "')";
            SqlCommand command2 = new SqlCommand(sql2, conn);
            SqlDataReader readergetID2 = command2.ExecuteReader();
            if (readergetID2.Read())
            {
                string[] strstart = readergetID2["AssignStartDt"].ToString().Trim().Split(' ');
                string[] strEnd = readergetID2["AssignEndtDt"].ToString().Trim().Split(' ');

                txtStaffHistory.Text = txtStaffHistory.Text + Environment.NewLine + "Latest Project : " + readergetID2["Project_Name"].ToString().Trim() + Environment.NewLine + "Duration : " + strstart[0].ToString().Trim() + " - " + strEnd[0].ToString().Trim();
            }
            BusinessTier.DisposeReader(readergetID2);
            BusinessTier.DisposeConnection(conn);
        }


        catch (Exception ex)
        {

            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    //--------------------------< RadGridAssignStaff >--------------------------------------

    protected void RadGridAssignStaff_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(cboProject.Text.ToString()))
            {
                RadGridAssignStaff.DataSource = DataSourceHelper("0");
            }
            else { RadGridAssignStaff.DataSource = DataSourceHelper(cboProject.SelectedValue.ToString()); }
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "AssignStaff", "RadGridAssignStaff_NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper(string pid)
    {
        //   TabContainer1.ActiveTab = TabContainer1.Tabs[1];
        lblStatus.Text = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";
        sql = "select * FROM Vw_AssignStaff WHERE Deleted=0 and Project_ID='" + pid.ToString() + "'";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void RadGridAssignStaff_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "RowClick" && e.Item is GridDataItem)
        {
            e.Item.Selected = true;
            string strTakenId = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Assign_ID"]).ToString();
            try
            {
                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                string sql1 = "select * from Vw_AssignStaff where DELETED=0 and [Assign_ID] ='" + strTakenId.ToString() + "'";
                SqlCommand command = new SqlCommand(sql1, conn);
                SqlDataReader readergetID = command.ExecuteReader();
                if (readergetID.Read())
                {
                    DtStart.SelectedDate = Convert.ToDateTime(readergetID["AssignStartDt"].ToString().Trim());
                    DtEnd.SelectedDate = Convert.ToDateTime(readergetID["AssignEndtDt"].ToString().Trim());
                    cboProject.Text = (readergetID["Project_Name"].ToString());
                    cboProject.SelectedValue = (readergetID["Project_ID"].ToString());
                    cboProject.ToolTip = (readergetID["Assign_ID"].ToString());
                    cboStaff.Text = (readergetID["STAFF_NAME"].ToString());
                    cboStaff.SelectedValue = (readergetID["Staff_ID"].ToString());
                }
                BusinessTier.DisposeReader(readergetID);
                BusinessTier.DisposeConnection(conn);
            }
            catch (Exception ex)
            {
                //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "AssignStaff", "RadGridAssignStaff_ItemCommand", ex.ToString(), "Audit");
                ShowMessage(8);
            }
        }
    }

    protected void RadGridAssignStaff_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();

            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Assign_ID"].ToString();
            BusinessTier.SaveAssignStaff(conn, Convert.ToInt32(ID.ToString()), "0", "0", "0", "", "", Session["sesUserID"].ToString().Trim(), "D");

            BusinessTier.DisposeConnection(conn);
            lblStatus.Text = "Successfully Deleted Staff to Project";
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "AssignStaff", "RadGridAssignStaff_DeleteCommand", "Success", "Log");

        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "AssignStaff", "RadGridAssignStaff_DeleteCommand", ex.ToString(), "Audit");
        }
    }

    //--------------------------< RadGridFreelancer >--------------------------------------

    protected void RadGridFreelancer_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(cboProject.Text.ToString()))
            {
                RadGridFreelancer.DataSource = DataSourceHelper2("0");
            }
            else { RadGridFreelancer.DataSource = DataSourceHelper2(cboProject.SelectedValue.ToString()); }           
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "AssignStaff", "RadGridFreelancer_NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper2(string pid)
    {
        //   TabContainer1.ActiveTab = TabContainer1.Tabs[1];
        lblStatus.Text = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "";
        sql = "select * FROM Vw_FreeLancer WHERE Deleted=0 and Project_ID='" + pid.ToString() + "'";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }   

    protected void RadGridFreelancer_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "RowClick" && e.Item is GridDataItem)
        {
            e.Item.Selected = true;
            string strTakenId = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FreeLancer_ID"]).ToString();
            try
            {
                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                string sql1 = "select * from Vw_FreeLancer where DELETED=0 and [FreeLancer_ID] ='" + strTakenId.ToString() + "'";
                SqlCommand command = new SqlCommand(sql1, conn);
                SqlDataReader readergetID = command.ExecuteReader();
                if (readergetID.Read())
                {
                    txtNameFreelancer.Text = readergetID["Staff_Name"].ToString().Trim();
                    txtNameFreelancer.ToolTip = readergetID["FreeLancer_ID"].ToString().Trim();
                    txtICNo.Text = readergetID["ICNo"].ToString().Trim();
                    txtSalary.Text = (readergetID["Salary"].ToString());
                    cboSalaryBy.Text = (readergetID["BasisSalary"].ToString());
                    DtStart.SelectedDate = Convert.ToDateTime(readergetID["AssignStartDt"].ToString().Trim());
                    DtEnd.SelectedDate = Convert.ToDateTime(readergetID["AssignEndtDt"].ToString().Trim());
                    cboProject.Text = (readergetID["Project_Name"].ToString());
                    cboProject.SelectedValue = (readergetID["Project_ID"].ToString());
                    if (string.IsNullOrEmpty(readergetID["FileName"].ToString().Trim()))
                    {
                        lnkDownload.Visible = false;
                        btnclikrjt.Visible = false;
                        upProjectFile.Visible = true;
                    }
                    else
                    {
                        lnkDownload.Visible = true;
                        lnkDownload.Text = readergetID["FileName"].ToString().Trim();
                        btnclikrjt.Visible = true;
                        upProjectFile.Visible = false;
                    }
                    btnSave.Text = " Update ";
                }
                BusinessTier.DisposeReader(readergetID);
                BusinessTier.DisposeConnection(conn);
            }
            catch (Exception ex)
            {
                //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "AssignStaff", "RadGridFreelancer_ItemCommand", ex.ToString(), "Audit");
                ShowMessage(8);
            }
        }
    }

    protected void RadGridFreelancer_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();

            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FreeLancer_ID"].ToString();
            BusinessTier.SaveFreeLancer(conn, Convert.ToInt32(ID.ToString().Trim()), 0, 0, 0, "", "", 0.00, "", "", "", "", Session["sesUserID"].ToString(), "D");

            BusinessTier.DisposeConnection(conn);
            lblStatus.Text = "Successfully Deleted Freelancer";
            //InsertLogAuditTrail(Session["sesUserID"].ToString(), "AssignStaff", "RadGridFreelancer_DeleteCommand", "Success", "Log");
            RadGridFreelancer.DataSource = DataSourceHelper2(cboProject.SelectedValue.ToString());
            RadGridFreelancer.Rebind();           
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "AssignStaff", "RadGridFreelancer_DeleteCommand", ex.ToString(), "Audit");
        }
    }   

    private void ShowMessage(int errorNo)
    {
        lblStatus.Text = BusinessTier.g_ErrorMessagesDataTable.Rows[errorNo - 1]["Message"].ToString();
        System.Drawing.ColorConverter colConvert = new ColorConverter();
        string strColor = BusinessTier.g_ErrorMessagesDataTable.Rows[errorNo - 1]["Color"].ToString();
        lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString(strColor);
    }

    private void ShowMessage(string strMessage)
    {
        lblStatus.Text = strMessage.ToString().Trim(); // BusinessTier.g_ErrorMessagesDataTable.Rows[errorNo - 1]["Message"].ToString() + " -  Completed : " + intSuccess + " Failure : " + intAll;
        System.Drawing.ColorConverter colConvert = new ColorConverter();
        string strColor = "Red";
        lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString(strColor);
    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }

    protected void btnAssignStaff_OnClick(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (string.IsNullOrEmpty(cboProject.Text))
            {
                lblStatus.Text = "** Project Field Cannot be Empty **";
                return;
            }

            if (string.IsNullOrEmpty(cboDesignation.Text))
            {
                lblStatus.Text = "** Assign Designation Field Cannot be Empty **";
                return;
            }
           
            if (string.IsNullOrEmpty(DtStart.SelectedDate.ToString()))
            {
                lblStatus.Text = "** Start Date Field Cannot be Empty **";
                return;
            }

            if (string.IsNullOrEmpty(DtEnd.SelectedDate.ToString()))
            {
                lblStatus.Text = "** End Date Field Cannot be Empty **";
                return;
            }

            if (DtStart.SelectedDate.Value >= DtEnd.SelectedDate.Value)
            {
                lblStatus.Text = "** Check End Date,Not Less than to Start Date **";
                return;
            }

            if (string.IsNullOrEmpty(cboCompany.Text))
            {
                lblStatus.Text = "** Branch Field Cannot be Empty **";
                return;
            }

            if (string.IsNullOrEmpty(cboStaff.Text))
            {
                lblStatus.Text = "** Staff Field Cannot be Empty **";
                return;
            }

            //ShowMessage(12, Convert.ToInt32(Session["SaveFlag"]), companyTable.RowCount - Convert.ToInt32(Session["SaveFlag"]));

            String StartDate = DtStart.SelectedDate.ToString();
            DateTime sdt = DateTime.Parse(StartDate);
            StartDate = sdt.Month + "/" + sdt.Day + "/" + sdt.Year + " 00:00:00";

            String EndDate = DtEnd.SelectedDate.ToString();
            DateTime edt = DateTime.Parse(EndDate);
            EndDate = edt.Month + "/" + edt.Day + "/" + edt.Year + " 00:00:00";

            string sql1 = "select AssignEndtDt from Vw_AssignStaff where DELETED=0 and [Staff_ID] ='" + cboStaff.SelectedValue.ToString() + "'";
            SqlCommand command = new SqlCommand(sql1, conn);
            SqlDataReader readergetID = command.ExecuteReader();
            if (readergetID.Read())
            {
                DateTime eDate, sDate;
                eDate = Convert.ToDateTime(readergetID["AssignEndtDt"].ToString().Trim());
                sDate = DtStart.SelectedDate.Value;
                if (eDate >= sDate)
                {
                    lblStatus.Text = "Access Denied, This staff already has been assigned in another project on this date";
                    lblStatus.ForeColor = Color.Red;
                    return;
                }

            }
            BusinessTier.DisposeReader(readergetID);

            if (cboProject.ToolTip == "")
            {
                BusinessTier.SaveAssignStaff(conn, 1, cboProject.SelectedValue.ToString(), cboStaff.SelectedValue.ToString(), cboDesignation.SelectedValue.ToString(), StartDate.ToString(), EndDate.ToString(), Session["sesUserID"].ToString().Trim(), "N");
                               lblStatus.Text = "Successfully Staff Assigned to Project";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                BusinessTier.SaveAssignStaff(conn, Convert.ToInt32(cboProject.ToolTip.ToString()), cboProject.SelectedValue.ToString(), cboStaff.SelectedValue.ToString(), cboDesignation.SelectedValue.ToString(), StartDate.ToString(), EndDate.ToString(), Session["sesUserID"].ToString().Trim(), "U");
                              lblStatus.Text = "Successfully Updated";
                lblStatus.ForeColor = Color.Maroon;
            }
            BusinessTier.DisposeConnection(conn);
            RadGridAssignStaff.DataSource = DataSourceHelper(cboProject.SelectedValue.ToString());
            RadGridAssignStaff.Rebind();
            //if (cboStaff.SelectedItem.Attributes.ToString().Trim() != "")
            //{
            //    MailMessage message1 = new MailMessage();
            //    message1.From = new MailAddress(ConfigurationManager.AppSettings["MailAddress"].ToString());
            //    //message1.From = new MailAddress(Session["Email"].ToString().Trim());
            //    message1.To.Add(new MailAddress(cboStaff.SelectedItem.Attributes.ToString().Trim()));

            //    message1.Subject = "Assign Project";
            //    string msg = string.Empty;
            //    msg = "Dear " + cboStaff.Text.ToString() + ",\n\n" + "You have Assign Project " + cboProject.Text.ToString().Trim() + "\n\nStart Date:" + DtStart.SelectedDate.ToString() + "\n\nEnd Date:" + DtEnd.SelectedDate.ToString() + " \nThank you\n\nby, \nAdmin" + "\r\n" + "\r\n" + "Note: " + "\r\n" + "*. Do not reply to this mail" + "\r\n" + "*. This is system generated mail " + "\r\n";
            //    message1.Body = msg;
            //    SmtpClient client1 = new SmtpClient(ConfigurationManager.AppSettings["Webserver"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString()));
            //    client1.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailAddress"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
            //    //client1.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    //client1.EnableSsl = true;
            //    client1.Send(message1);

            //    //lblStatus.Text = "You have Sucessfully Register!";
            //    //lblStatus.ForeColor = Color.Green;
            //    //Response.Redirect("index.aspx");
            //}
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message.ToString();
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "AssignStaff", "btnAssignStaff_OnClick", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }


    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(cboJobNo.Text))
        //{
        //    lblStatus.Text = "JobNo Field Cannot be Empty";
        //    return;
        //}

        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Report_Invoice.aspx?param1=" + cboQuotationNo.Text.ToString().Trim() + "&param2=" + lblinvoiceno.Text.ToString().Trim() + "');", true);
    }

    protected void lnkDownload_OnClick(object sender, EventArgs e)
    {
        try
        {
            string strPah = WebConfigurationManager.AppSettings["WC_FileGetPath1"].ToString();
            string strLink = strPah + lnkDownload.Text.ToString().Trim();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenNewTab", "window.open('" + strLink + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=400,height=200');", true);
        }
        catch (Exception Ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnclikrjt_Onclick(object sender, EventArgs e)
    {
        lnkDownload.Visible = false;
        lnkDownload.Text = string.Empty;
        btnclikrjt.Visible = false;
        upProjectFile.Visible = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (string.IsNullOrEmpty(cboProject.Text.ToString().Trim()))
            {
                lblStatus.Text = "** Please Choose Project **";
                return;
            }
            if (string.IsNullOrEmpty(cboDesignation.Text))
            {
                lblStatus.Text = "** Assign Designation Field Cannot be Empty **";
                return;
            }

            if (string.IsNullOrEmpty(DtStart.SelectedDate.ToString()))
            {
                lblStatus.Text = "** Start Date Field Cannot be Empty **";
                return;
            }

            if (string.IsNullOrEmpty(DtEnd.SelectedDate.ToString()))
            {
                lblStatus.Text = "** End Date Field Cannot be Empty **";
                return;
            }

            if (DtStart.SelectedDate.Value >= DtEnd.SelectedDate.Value)
            {
                lblStatus.Text = "** Check End Date,Not Less than to Start Date **";
                return;
            }
            if (string.IsNullOrEmpty(cboFreeLanceStaffId.Text))
            {
                lblStatus.Text = "** FreeLancer Field Cannot be Empty **";
                return;
            }
            if (string.IsNullOrEmpty(txtSalary.Text))
            {
                lblStatus.Text = "** Salary Field Cannot be Empty **";
                return;
            }

            string sql1 = "select * from FreeLancer where DELETED=0 and Staff_ID ='" + cboFreeLanceStaffId.SelectedValue.ToString() + "'";
            SqlCommand command = new SqlCommand(sql1, conn);
            SqlDataReader readergetID = command.ExecuteReader();
            if (readergetID.Read())
                {
                    lblStatus.Text = "** This staff is already assigned **";
                    lblStatus.ForeColor = Color.Red;
                    return;
                }           
            BusinessTier.DisposeReader(readergetID);

            String StartDate = DtStart.SelectedDate.ToString();
            DateTime sdt = DateTime.Parse(StartDate);
            StartDate = sdt.Month + "/" + sdt.Day + "/" + sdt.Year + " 00:00:00";

            String EndDate = DtEnd.SelectedDate.ToString();
            DateTime edt = DateTime.Parse(EndDate);
            EndDate = edt.Month + "/" + edt.Day + "/" + edt.Year + " 00:00:00";

            string strpath = WebConfigurationManager.AppSettings["WC_FilePath1"].ToString(), FileName = "";

            ////************************>>> FileUpload <<<<************************\\\\

            if (upProjectFile.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in upProjectFile.UploadedFiles)
                {
                    FileName = f.GetName().ToString().Trim();
                    f.SaveAs(strpath.ToString() + f.GetName(), true);
                }
            }
            if (string.IsNullOrEmpty(FileName.ToString().Trim()))
            {
                FileName = lnkDownload.Text.ToString();
            }
            int flg = 0;

            if (btnSave.Text == " Update ")
            {
                flg = BusinessTier.SaveFreeLancer(conn, Convert.ToInt32(txtNameFreelancer.ToolTip.ToString().Trim()), Convert.ToInt32(cboProject.SelectedValue.ToString().Trim()), Convert.ToInt32(cboFreeLanceStaffId.SelectedValue.ToString()), Convert.ToInt32(cboDesignation.SelectedValue.ToString()), txtNameFreelancer.Text.ToString().Trim(), txtICNo.Text.ToString().Trim(), Convert.ToDouble(txtSalary.Text.ToString().Trim()), cboSalaryBy.Text.ToString(), FileName.ToString(), StartDate.ToString(), EndDate.ToString().Trim(), Session["sesUserID"].ToString(), "U");
                flg += 1;
            }
            else
            {
                flg = BusinessTier.SaveFreeLancer(conn, 1, Convert.ToInt32(cboProject.SelectedValue.ToString().Trim()), Convert.ToInt32(cboFreeLanceStaffId.SelectedValue.ToString()), Convert.ToInt32(cboDesignation.SelectedValue.ToString()), txtNameFreelancer.Text.ToString().Trim(), txtICNo.Text.ToString().Trim(), Convert.ToDouble(txtSalary.Text.ToString().Trim()), cboSalaryBy.Text.ToString(), FileName.ToString(), StartDate.ToString(), EndDate.ToString().Trim(), Session["sesUserID"].ToString(), "N");
               }
            BusinessTier.DisposeConnection(conn);
            if (flg == 1)
            {
                lblStatus.Text = "** Successfully Add FreeLancer **";
                lblStatus.ForeColor = Color.Green;
            }
            if (flg == 2)
            {
                ShowMessage(138);
            }
            BusinessTier.DisposeConnection(conn);
            btnSave.Text = " Save ";
            RadGridFreelancer.DataSource = DataSourceHelper2(cboProject.SelectedValue.ToString());
            RadGridFreelancer.Rebind();
            Clear();
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "btnSave_Click", ex.ToString(), "Audit");
        }


    }

    public void Clear()
    {

        cboProject.Text = "";
        txtNameFreelancer.Text = "";
        txtICNo.Text = "";
        txtSalary.Text = "";

        cboSalaryBy.Text = "";
        DtStart.SelectedDate = null;
        DtEnd.SelectedDate = null;

        lnkDownload.Visible = false;
        lnkDownload.Text = string.Empty;
        btnclikrjt.Visible = false;
        upProjectFile.Visible = true;
    }
}