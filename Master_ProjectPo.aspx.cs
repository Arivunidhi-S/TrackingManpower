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
using System.Web.Configuration;
using System.Globalization;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Drawing;

public partial class Master_ProjectPo : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
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
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                RadGrid1.MasterTableView.IsItemInserted = false;

                RadGrid1.Rebind();

                if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    lblStatus.Text = "";

                }
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        lblname.Text = "Hi, " + Session["Name"].ToString();
    }

    //************************>>> Grid Load <<<************************//

    protected void RadGrid1_ItemDataBound(object source, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                Label lblProjectID = (Label)e.Item.FindControl("lblProjectID");
                LinkButton lblFileName = (LinkButton)e.Item.FindControl("lblFileName");
                RadComboBox cboCustomer = (RadComboBox)e.Item.FindControl("cboCustomer");
                RadComboBox cboProject = (RadComboBox)e.Item.FindControl("cboProject");
                RadComboBox cboBranch = (RadComboBox)e.Item.FindControl("cboBranch");
                RadComboBox cboProjectManager = (RadComboBox)e.Item.FindControl("cboProjectManager");
                TextBox txtProjectNo = (TextBox)e.Item.FindControl("txtProjectNo");
                HyperLink editLink = (HyperLink)e.Item.FindControl("EditLink");
                RadDatePicker dtFrom = (RadDatePicker)e.Item.FindControl("DtStart");
                RadDatePicker DtEnd = (RadDatePicker)e.Item.FindControl("DtEnd");
                RadDatePicker DtProjectAwardDate = (RadDatePicker)e.Item.FindControl("DtProjectAwardDate");

                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();

                if (!(string.IsNullOrEmpty(lblProjectID.Text.ToString())))
                {

                    string strpath = WebConfigurationManager.AppSettings["WC_FilePath"].ToString();
                    string sql1 = "select CUSTOMER_ID,STAFF_ID,Project_ID,COUNTRY_ID,CUSTOMER_NAME,StartDate,EndDate,ProjectAwardDate from Vw_ProjectPo WHERE Deleted = 0 and ProjectPo_ID = '" + lblProjectID.Text.ToString().Trim() + "'";
                    SqlCommand command1 = new SqlCommand(sql1, conn);
                    SqlDataReader reader1 = command1.ExecuteReader();
                    if (reader1.Read())
                    {
                        dtFrom.DbSelectedDate = Convert.ToDateTime(reader1["StartDate"].ToString());
                        DtEnd.DbSelectedDate = Convert.ToDateTime(reader1["EndDate"].ToString());
                        DtProjectAwardDate.DbSelectedDate = Convert.ToDateTime(reader1["ProjectAwardDate"].ToString());

                        cboProject.SelectedValue = reader1["Project_ID"].ToString();
                        cboProject.ToolTip = reader1["Project_ID"].ToString();

                        cboCustomer.SelectedValue = reader1["CUSTOMER_ID"].ToString();
                        cboCustomer.ToolTip = reader1["CUSTOMER_ID"].ToString();

                        cboBranch.SelectedValue = reader1["COUNTRY_ID"].ToString();
                        cboBranch.ToolTip = reader1["COUNTRY_ID"].ToString();

                        cboProjectManager.SelectedValue = reader1["STAFF_ID"].ToString();
                        cboProjectManager.ToolTip = reader1["STAFF_ID"].ToString();

                    }
                    else
                    {
                        lblFileName.Visible = false;
                        txtProjectNo.Enabled = true;
                    }
                    BusinessTier.DisposeReader(reader1);
                    BusinessTier.DisposeConnection(conn);

                }

            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid1.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        int delval = 0;
        string sql = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        sql = "select * FROM Vw_ProjectPo WHERE Deleted='" + delval + "' order by Project_ID";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    //************************>>> Grid Insert,Update and Delete <<<************************//

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            RadComboBox cboProject = (RadComboBox)e.Item.FindControl("cboProject");
            RadComboBox cboBranch = (RadComboBox)e.Item.FindControl("cboBranch");
            RadDatePicker DtStart = (RadDatePicker)editedItem.FindControl("DtStart");
            RadDatePicker DtEnd = (RadDatePicker)editedItem.FindControl("DtEnd");
            RadNumericTextBox txtTenderValue = (RadNumericTextBox)editedItem.FindControl("txtTenderValue");
            RadComboBox cboTenderCurrency = (RadComboBox)editedItem.FindControl("cboTenderCurrency");
            RadComboBox cboSupplyScope = (RadComboBox)editedItem.FindControl("cboSupplyScope");
            RadDatePicker DtProjectAwardDate = (RadDatePicker)editedItem.FindControl("DtProjectAwardDate");
            RadComboBox cboCustomer = (RadComboBox)editedItem.FindControl("cboCustomer");
            RadComboBox cboProjectManager = (RadComboBox)editedItem.FindControl("cboProjectManager");
            TextBox txtDescription = (TextBox)editedItem.FindControl("txtDescription");
            TextBox txtSupplyScope = (TextBox)editedItem.FindControl("txtSupplyScope");

            if (string.IsNullOrEmpty(cboCustomer.Text.ToString().Trim()))
            {
                lblStatus.Text = "Select Customer";
                return;
            }
            if (string.IsNullOrEmpty(cboProjectManager.Text.ToString().Trim()))
            {
                lblStatus.Text = "Select ProjectManager";
                return;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            DateTime ProjectAwardDate = DateTime.Now;
            if (string.IsNullOrEmpty(Convert.ToString(DtStart.SelectedDate)))
            {
                lblStatus.Text = "Please choose Start Date";
                return;
            }
            else
            {
                StartDate = Convert.ToDateTime(DtStart.SelectedDate.ToString());
            }
            if (string.IsNullOrEmpty(Convert.ToString(DtEnd.SelectedDate)))
            {
                lblStatus.Text = "Please choose End Date";
                return;
            }
            else
            {
                EndDate = Convert.ToDateTime(DtEnd.SelectedDate.ToString());
            }

            if (string.IsNullOrEmpty(Convert.ToString(DtProjectAwardDate.SelectedDate)))
            {
                lblStatus.Text = "Please choose project awarded Date";
                return;
            }
            else
            {
                ProjectAwardDate = Convert.ToDateTime(DtProjectAwardDate.SelectedDate.ToString());
            }

            if (string.IsNullOrEmpty(txtTenderValue.Text.ToString().Trim()))
            {
                txtTenderValue.Text = "0";

            }


            string strpath = WebConfigurationManager.AppSettings["WC_FilePath"].ToString();
            int flg = 0;
            flg = BusinessTier.SaveProjectPo(conn, 1, cboProject.SelectedValue.ToString().Trim(), StartDate, EndDate, Convert.ToDouble(txtTenderValue.Text.ToString().Trim()), cboTenderCurrency.Text.ToString().Trim(), cboSupplyScope.Text.ToString().Trim(), txtSupplyScope.Text.ToString().Trim(), ProjectAwardDate, txtDescription.Text.ToString().Trim(), Convert.ToInt32(cboProjectManager.SelectedValue.ToString().Trim()), Convert.ToInt32(cboCustomer.SelectedValue.ToString().Trim()), Convert.ToInt32(cboBranch.SelectedValue.ToString().Trim()), Session["sesUserID"].ToString(), "N");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(141);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "Insert", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

        RadGrid1.Rebind();
    }

    protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            RadComboBox cboProject = (RadComboBox)e.Item.FindControl("cboProject");
            RadComboBox cboBranch = (RadComboBox)e.Item.FindControl("cboBranch");
            RadDatePicker DtStart = (RadDatePicker)editedItem.FindControl("DtStart");
            RadDatePicker DtEnd = (RadDatePicker)editedItem.FindControl("DtEnd");
            RadNumericTextBox txtTenderValue = (RadNumericTextBox)editedItem.FindControl("txtTenderValue");
            RadComboBox cboTenderCurrency = (RadComboBox)editedItem.FindControl("cboTenderCurrency");
            RadComboBox cboSupplyScope = (RadComboBox)editedItem.FindControl("cboSupplyScope");
            RadDatePicker DtProjectAwardDate = (RadDatePicker)editedItem.FindControl("DtProjectAwardDate");
            RadComboBox cboCustomer = (RadComboBox)editedItem.FindControl("cboCustomer");
            RadComboBox cboProjectManager = (RadComboBox)editedItem.FindControl("cboProjectManager");
            TextBox txtDescription = (TextBox)editedItem.FindControl("txtDescription");
            TextBox txtSupplyScope = (TextBox)editedItem.FindControl("txtSupplyScope");

            if (string.IsNullOrEmpty(cboCustomer.Text.ToString().Trim()))
            {
                lblStatus.Text = "Select Customer";
                return;
            }
            if (string.IsNullOrEmpty(cboProjectManager.Text.ToString().Trim()))
            {
                lblStatus.Text = "Select ProjectManager";
                return;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            DateTime ProjectAwardDate = DateTime.Now;
            if (string.IsNullOrEmpty(Convert.ToString(DtStart.SelectedDate)))
            {
                lblStatus.Text = "Please choose Start Date";
                return;
            }
            else
            {
                StartDate = Convert.ToDateTime(DtStart.SelectedDate.ToString());
            }
            if (string.IsNullOrEmpty(Convert.ToString(DtEnd.SelectedDate)))
            {
                lblStatus.Text = "Please choose End Date";
                return;
            }
            else
            {
                EndDate = Convert.ToDateTime(DtEnd.SelectedDate.ToString());
            }

            if (string.IsNullOrEmpty(Convert.ToString(DtProjectAwardDate.SelectedDate)))
            {
                lblStatus.Text = "Please choose project awarded Date";
                return;
            }
            else
            {
                ProjectAwardDate = Convert.ToDateTime(DtProjectAwardDate.SelectedDate.ToString());
            }

            if (string.IsNullOrEmpty(txtTenderValue.Text.ToString().Trim()))
            {
                txtTenderValue.Text = "0";

            }

            string strpath = WebConfigurationManager.AppSettings["WC_FilePath"].ToString(), ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProjectPo_ID"].ToString();
            int flg = 0;

            flg = BusinessTier.SaveProjectPo(conn, Convert.ToInt32(ID.ToString()), cboProject.SelectedValue.ToString().Trim(), StartDate, EndDate, Convert.ToDouble(txtTenderValue.Text.ToString().Trim()), cboTenderCurrency.Text.ToString().Trim(), cboSupplyScope.Text.ToString().Trim(), txtSupplyScope.Text.ToString().Trim(), ProjectAwardDate, txtDescription.Text.ToString().Trim(), Convert.ToInt32(cboProjectManager.SelectedValue.ToString().Trim()), Convert.ToInt32(cboCustomer.SelectedValue.ToString().Trim()), Convert.ToInt32(cboBranch.SelectedValue.ToString().Trim()), Session["sesUserID"].ToString(), "U");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(140);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "Update", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            //lblStatus.Text = ex.ToString();
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "Update", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

        RadGrid1.Rebind();
    }

    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProjectPo_ID"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveProjectPo(conn, Convert.ToInt32(ID.ToString()), "", DateTime.Now, DateTime.Now, 1.00, "", "", "", DateTime.Now, "", 1, 1, 1, Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(139);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            //ShowMessage(7);
            lblStatus.Text = ex.ToString();
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "Delete", ex.ToString(), "Audit");
        }
    }

    //************************>>> OnItemsRequested <<<************************//

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

    protected void cboStaffId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Staff_Id,Staff_Name,Staff_no from Vw_PMT where DELETED=0 and [Staff_Name] LIKE '%' + @text + '%' order by Staff_Name";
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
                item.Attributes.Add("Staff_Name", row["Staff_Name"].ToString());
                item.Attributes.Add("Staff_no", row["Staff_no"].ToString());
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

    protected void CboCustomer_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select CUSTOMER_ID,CUSTOMER_CODE,CUSTOMER_NAME from Master_Customer where DELETED=0 and  [CUSTOMER_NAME] LIKE @Text + '%' order by CUSTOMER_NAME";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["CUSTOMER_NAME"].ToString();
                item.Value = row["CUSTOMER_ID"].ToString();
                item.Attributes.Add("CUSTOMER_NAME", row["CUSTOMER_NAME"].ToString());
                item.Attributes.Add("CUSTOMER_CODE", row["CUSTOMER_CODE"].ToString());
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

    protected void cboBranchId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select COUNTRY_ID,Location,Country_Code from Master_Country where DELETED=0  and [Location] LIKE '%' + @text + '%' order by Location";
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
                item.Value = row["COUNTRY_ID"].ToString();
                item.Attributes.Add("Location", row["Location"].ToString());
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

    //************************>>> Function for Message, Error <<<************************//

    private void ShowMessage(int errorNo)
    {
        lblStatus.Text = BusinessTier.g_ErrorMessagesDataTable.Rows[errorNo - 1]["Message"].ToString();
        System.Drawing.ColorConverter colConvert = new ColorConverter();
        string strColor = BusinessTier.g_ErrorMessagesDataTable.Rows[errorNo - 1]["Color"].ToString();
        lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString(strColor);
    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }



}