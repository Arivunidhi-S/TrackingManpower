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

public partial class PMT : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    RadComboBox cboProjectManager;

    RadComboBox cboCompany;

    protected void Page_Init(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["Name"] = "Admin";
        try
        {
            if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblStatus.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        lblname.Text = "Hi, " + Session["Name"].ToString();
    }

    protected void cboCompany_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Company_ID,Company_Name,Company_Code from Master_Company where DELETED=0  and [Company_Name] LIKE '%' + @text + '%' order by Company_Name";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Company_Name"].ToString();
                item.Value = row["Company_ID"].ToString();
                item.Attributes.Add("Company_Name", row["Company_Name"].ToString());
                item.Attributes.Add("Company_Code", row["Company_Code"].ToString());
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

    //protected void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        SqlConnection conn = BusinessTier.getConnection();
    //        conn.Open();
    //        string sql1 = " select STAFF_NO,STAFF_NAME,STAFF_ID from Vw_Staff_Details where DELETED=0 and [Company_ID] ='" + cboCompany.SelectedValue.ToString() + "' order by Company_ID";
    //        SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
    //        //adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
    //        DataTable dataTable1 = new DataTable();
    //        adapter1.Fill(dataTable1);
    //        //RadComboBox cboBranch = (RadComboBox)sender;
    //        cboProjectManager.Items.Clear();
    //        cboProjectManager.Text = string.Empty;
    //        foreach (DataRow row in dataTable1.Rows)
    //        {
    //            RadComboBoxItem item = new RadComboBoxItem();
    //            item.Text = row["STAFF_NAME"].ToString();
    //            item.Value = row["STAFF_ID"].ToString();
    //            item.Attributes.Add("STAFF_NAME", row["STAFF_NAME"].ToString());
    //            item.Attributes.Add("STAFF_NO", row["STAFF_NO"].ToString());
    //            cboProjectManager.Items.Add(item);
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

    protected void cboProjectManager_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select STAFF_NO,STAFF_NAME,STAFF_ID from Vw_Staff_Details where DELETED=0 and [Company_ID] ='" + cboCompany.SelectedValue.ToString() + "' and [STAFF_NAME] LIKE '%' + @text + '%'  order by Company_ID";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox combo = (RadComboBox)sender;
            combo.Items.Clear();
            combo.Text = string.Empty;
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["STAFF_NAME"].ToString();
                item.Value = row["STAFF_ID"].ToString();
                item.Attributes.Add("STAFF_NAME", row["STAFF_NAME"].ToString());
                item.Attributes.Add("STAFF_NO", row["STAFF_NO"].ToString());
                combo.Items.Add(item);
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

    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                cboProjectManager = (RadComboBox)editedItem.FindControl("cboProjectManager");
                cboCompany = (RadComboBox)editedItem.FindControl("cboCompany");
                cboCompany.AutoPostBack = true;
                //cboCompany.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(cboCompany_SelectedIndexChanged);
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
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "PMT", "RadGrid1_ItemCreated", ex.ToString(), "Audit");
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
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Contact", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        int delval = 0;
        string sql = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        sql = "select * FROM Vw_PMT WHERE Deleted='" + delval + "' order by PMT_ID";
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
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PMT_ID"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
           
            int flg = BusinessTier.SavePMTMaster(conn, Convert.ToInt32(ID.ToString()), 1, Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(132);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "PMT", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "PMT", "Delete", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            
            RadComboBox cboProjectManager = (RadComboBox)editedItem.FindControl("cboProjectManager");
            if (string.IsNullOrEmpty(cboProjectManager.Text.ToString()))
            {
                lblStatus.Text = "Select Project Manager";
                return;
            }


            string strCheckflag = "0";
            SqlDataReader reader = BusinessTier.IDCheck(conn, cboProjectManager.SelectedValue.ToString().Trim(), "PMT");
            if (reader.Read())
            {
                strCheckflag = reader["IDCheck"].ToString();

            }
            BusinessTier.DisposeReader(reader);

            if (strCheckflag.ToString() == "1")
            {
                lblStatus.Text = "This Project Manager Data is already Exist";
                return;
            }


            int flg = BusinessTier.SavePMTMaster(conn, 1, Convert.ToInt32(cboProjectManager.SelectedValue), Session["sesUserID"].ToString(), "N");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(131);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "PMT", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            // e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "PMT", "Insert", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

        RadGrid1.Rebind();
    }

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