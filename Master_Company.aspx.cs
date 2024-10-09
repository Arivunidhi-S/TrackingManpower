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


public partial class Master_Company : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    RadComboBox txtCountry;

    RadComboBox cboBranch;

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

                    //DtQaotdt.SelectedDate = DateTime.Now;
                    //Insert Mode in First time

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
                //RadGrid1.MasterTableView.IsItemInserted = true;

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
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Company", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        int delval = 0;
        string sql = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        sql = "select * FROM Vw_Master_Company WHERE Deleted='" + delval + "' order by Company_ID";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Company_ID"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveCompanyMaster(conn, Convert.ToInt32(ID.ToString()), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 1, Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(126);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Company", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Customer", "Delete", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            TextBox txtCompanyName = (TextBox)editedItem.FindControl("txtCompanyName");
            TextBox txtCompanycode = (TextBox)editedItem.FindControl("txtCompanycode");
            TextBox txtRocNo = (TextBox)editedItem.FindControl("txtRocNo");
            RadDatePicker DtRocregdate = (RadDatePicker)editedItem.FindControl("DtRocregdate");
            TextBox txtAddress1 = (TextBox)editedItem.FindControl("txtAddress1");
            TextBox txtAddress2 = (TextBox)editedItem.FindControl("txtAddress2");
            TextBox txtAddress3 = (TextBox)editedItem.FindControl("txtAddress3");
            TextBox txtCity = (TextBox)editedItem.FindControl("txtCity");
            TextBox txtState = (TextBox)editedItem.FindControl("txtState");
            RadComboBox txtCountry = (RadComboBox)editedItem.FindControl("txtCountry");
            RadNumericTextBox txtPostalcode = (RadNumericTextBox)editedItem.FindControl("txtPostalcode");
            RadNumericTextBox txtPhone = (RadNumericTextBox)editedItem.FindControl("txtPhone");
            RadNumericTextBox txtFax = (RadNumericTextBox)editedItem.FindControl("txtFax");
            TextBox txtEmail = (TextBox)editedItem.FindControl("txtEmail");
            TextBox txtWebsite = (TextBox)editedItem.FindControl("txtWebsite");
            RadComboBox cboBranch = (RadComboBox)editedItem.FindControl("cboBranch");

            //if (string.IsNullOrEmpty(Convert.ToString(DtRocregdate.SelectedDate)))
            //{
            //    DtRocregdate.SelectedDate = DateTime.Now;
            // }

            string strCheckflag = "0";
            SqlDataReader reader = BusinessTier.IDCheck(conn, txtCompanycode.Text.ToString().Trim(), "CM");
            if (reader.Read())
            {
                strCheckflag = reader["IDCheck"].ToString();

            }
            BusinessTier.DisposeReader(reader);

            if (strCheckflag.ToString() == "1")
            {
                lblStatus.Text = "Company Data is already Exist";
                return;
            }

            //if (string.IsNullOrEmpty(cboBranch.Text.ToString().Trim()))
            //{
            //   // lblStatus.Text = "Select Branch";
            //    //return;
            //    cboBranch.Text = "0";
            //}
            string RegDT = string.Empty;
            if (string.IsNullOrEmpty(Convert.ToString(DtRocregdate.SelectedDate)))
            {
                RegDT = "01/01/1900 00:00:00";
            }
            else
            {
                RegDT = DtRocregdate.SelectedDate.ToString();
                DateTime idt = DateTime.Parse(RegDT);
                RegDT = idt.Month + "/" + idt.Day + "/" + idt.Year + " 00:00:00";
            }

            int flg = BusinessTier.SaveCompanyMaster(conn, 1, txtCompanyName.Text.ToString().ToUpper().Trim(), txtCompanycode.Text.ToString().ToUpper().Trim(), txtRocNo.Text.ToString().ToUpper().Trim(), RegDT.ToString().ToUpper().Trim(), txtAddress1.Text.ToString().ToUpper().Trim(), txtAddress2.Text.ToString().ToUpper().Trim(), txtAddress3.Text.ToString().ToUpper().Trim(), txtCity.Text.ToString().ToUpper().Trim(), txtState.Text.ToString().ToUpper().Trim(), txtCountry.Text.ToString().ToUpper().Trim(), txtPostalcode.Text.ToString().ToUpper().Trim(), txtPhone.Text.ToString().ToUpper().Trim(), txtFax.Text.ToString().ToUpper().Trim(), txtEmail.Text.ToString().ToLower().Trim(), txtWebsite.Text.ToString().ToUpper().Trim(), 0, Session["sesUserID"].ToString(), "N");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(125);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Company", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Company", "Insert", ex.ToString(), "Audit");
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
            TextBox txtCompanyName = (TextBox)editedItem.FindControl("txtCompanyName");
            TextBox txtCompanycode = (TextBox)editedItem.FindControl("txtCompanycode");
            TextBox txtRocNo = (TextBox)editedItem.FindControl("txtRocNo");
            RadDatePicker DtRocregdate = (RadDatePicker)editedItem.FindControl("DtRocregdate");
            TextBox txtAddress1 = (TextBox)editedItem.FindControl("txtAddress1");
            TextBox txtAddress2 = (TextBox)editedItem.FindControl("txtAddress2");
            TextBox txtAddress3 = (TextBox)editedItem.FindControl("txtAddress3");
            TextBox txtCity = (TextBox)editedItem.FindControl("txtCity");
            TextBox txtState = (TextBox)editedItem.FindControl("txtState");
            RadComboBox txtCountry = (RadComboBox)editedItem.FindControl("txtCountry");
            RadNumericTextBox txtPostalcode = (RadNumericTextBox)editedItem.FindControl("txtPostalcode");
            RadNumericTextBox txtPhone = (RadNumericTextBox)editedItem.FindControl("txtPhone");
            RadNumericTextBox txtFax = (RadNumericTextBox)editedItem.FindControl("txtFax");
            TextBox txtEmail = (TextBox)editedItem.FindControl("txtEmail");
            TextBox txtWebsite = (TextBox)editedItem.FindControl("txtWebsite");
            RadComboBox cboBranch = (RadComboBox)editedItem.FindControl("cboBranch");

            string RegDT = string.Empty;
            if (string.IsNullOrEmpty(Convert.ToString(DtRocregdate.SelectedDate)))
            {
                RegDT = "01/01/1900 00:00:00";
            }
            else
            {
                RegDT = DtRocregdate.SelectedDate.ToString();
                DateTime idt = DateTime.Parse(RegDT);
                RegDT = idt.Month + "/" + idt.Day + "/" + idt.Year + " 00:00:00";
            }

            //string strCheckflag = "0";
            //SqlDataReader reader = BusinessTier.IDCheck(conn, txtCompanycode.Text.ToString().Trim(), "CM");
            //if (reader.Read())
            //{
            //    strCheckflag = reader["IDCheck"].ToString();

            //}
            //BusinessTier.DisposeReader(reader);

            //if (strCheckflag.ToString() == "1")
            //{
            //    lblStatus.Text = "Company Data is already Exist";
            //    return;
            //}

            //if (string.IsNullOrEmpty(cboBranch.Text.ToString().Trim()))
            //{
            //    lblStatus.Text = "Select Branch";
            //    return;
            //}

            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Company_ID"].ToString();

            int flg = BusinessTier.SaveCompanyMaster(conn, Convert.ToInt32(ID.ToString()), txtCompanyName.Text.ToString().ToUpper().Trim(), txtCompanycode.Text.ToString().ToUpper().Trim(), txtRocNo.Text.ToString().ToUpper().Trim(), RegDT.ToString().ToUpper().Trim(), txtAddress1.Text.ToString().ToUpper().Trim(), txtAddress2.Text.ToString().ToUpper().Trim(), txtAddress3.Text.ToString().ToUpper().Trim(), txtCity.Text.ToString().ToUpper().Trim(), txtState.Text.ToString().ToUpper().Trim(), txtCountry.Text.ToString().ToUpper().Trim(), txtPostalcode.Text.ToString().ToUpper().Trim(), txtPhone.Text.ToString().ToUpper().Trim(), txtFax.Text.ToString().ToUpper().Trim(), txtEmail.Text.ToString().ToLower().Trim(), txtWebsite.Text.ToString().ToUpper().Trim(), 0, Session["sesUserID"].ToString(), "U");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(127);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Company", "Update", "Success", "Log");
        }
        catch (Exception ex)
        {
            //ShowMessage(5);
            lblStatus.Text = ex.ToString();
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Company", "Update", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

        RadGrid1.Rebind();
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {

        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                Label lbl = (Label)editedItem.FindControl("lblCustomerID");
                RadComboBox cboBranch = (RadComboBox)editedItem.FindControl("cboBranch");
                RadComboBox txtCountry = (RadComboBox)editedItem.FindControl("txtCountry");
                TextBox txtCompanycode = (TextBox)editedItem.FindControl("txtCompanycode");
                if (!(string.IsNullOrEmpty(lbl.Text.ToString())))
                {
                    SqlConnection conn = BusinessTier.getConnection();
                    conn.Open();
                    txtCompanycode.Enabled = false;
                    string sql1 = "select COUNTRY_ID,COUNTRY from Vw_Master_Company WHERE Deleted = 0 and Company_ID = '" + lbl.Text.ToString() + "'";
                    SqlCommand command1 = new SqlCommand(sql1, conn);
                    SqlDataReader reader1 = command1.ExecuteReader();
                    if (reader1.Read())
                    {
                        cboBranch.SelectedValue = reader1["COUNTRY_ID"].ToString();
                        txtCountry.Text = reader1["COUNTRY"].ToString();
                    }
                    else
                    {
                        cboBranch.SelectedValue = "1";
                        txtCompanycode.Enabled = true;
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

    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                txtCountry = (RadComboBox)editedItem.FindControl("txtCountry");
                cboBranch = (RadComboBox)editedItem.FindControl("cboBranch");
                txtCountry.AutoPostBack = true;
                txtCountry.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(txtCountry_SelectedIndexChanged);
            }

            if (e.Item is GridDataItem)
            {

            }
        }
        catch (Exception ex)
        {
            e.Item.OwnerTableView.IsItemInserted = false;

            ShowMessage(10);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Company", "RadGrid1_ItemCreated", ex.ToString(), "Audit");
        }
    }

    protected void txtCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Country_ID,Country_Code,Country_Name,Location from Master_Country where DELETED=0 and [Country_Name] ='" + txtCountry.Text.ToString() + "' order by Country_Code desc";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            //adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            //RadComboBox cboBranch = (RadComboBox)sender;
            cboBranch.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Location"].ToString();
                item.Value = row["COUNTRY_ID"].ToString();
                item.Attributes.Add("Country_Name", row["Country_Name"].ToString());
                item.Attributes.Add("Country_Code", row["Country_Code"].ToString());
                cboBranch.Items.Add(item);
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

    protected void cboStaffId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Staff_Id,Staff_Name,Staff_no from Master_Staff where DELETED=0 and Branch_Id='" + Session["sesBranchID"].ToString().Trim() + "' and [Staff_Name] LIKE '%' + @text + '%' order by Staff_Name";
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

    protected void txtState_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Distinct(Country_Name) as Country from Master_Country where DELETED=0 and  [Country_Name] LIKE @Text + '%' order by Country_Name desc";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Country"].ToString();
                //item.Value = row["State_Code"].ToString();
                //item.Attributes.Add("State_Name", row["State_Name"].ToString());
                //item.Attributes.Add("State_Code", row["State_Code"].ToString());
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

    //protected void txtCountry_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    //{
    //    try
    //    {
    //        //GridEditableItem item = (GridEditableItem)e.Item;
    //        //RadComboBox resource = (RadComboBox)e.Text("ResourceInput");
    //        //RadComboBox comboBox = (RadComboBox)sender;
    //        //RadComboBox cboStaffId = (RadComboBox)e.FindControl("cboStaffId");

    //        SqlConnection conn = BusinessTier.getConnection();
    //        conn.Open();
    //        string sql1 = " select Country_ID,Country_Code,Country_Name,Location from Master_Country where DELETED=0 and [Location] LIKE '%' + @text + '%' order by Country_Code desc";
    //        SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
    //        adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
    //        DataTable dataTable1 = new DataTable();
    //        adapter1.Fill(dataTable1);
    //        RadComboBox comboBox = (RadComboBox)sender;
    //        comboBox.Items.Clear();
    //        foreach (DataRow row in dataTable1.Rows)
    //        {
    //            RadComboBoxItem item = new RadComboBoxItem();
    //            item.Text = row["Location"].ToString();
    //            item.Value = row["Country_Code"].ToString();
    //            item.Attributes.Add("Country_Name", row["Country_Name"].ToString());
    //            item.Attributes.Add("Country_Code", row["Country_Code"].ToString());
    //            comboBox.Items.Add(item);
    //            item.DataBind();
    //        }
    //        adapter1.Dispose();
    //        BusinessTier.DisposeConnection(conn);
    //        // Session["rdobutton"] = "";
    //    }


    //    catch (Exception ex)
    //    {

    //        //ShowMessage("Please Select the Installation Name", "Red");
    //    }

    //}

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