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

public partial class Master_Project : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    //RadAsyncUpload upProjectFile;
    //Label file;
    //string file = string.Empty;
    public string custid = string.Empty;
    //RadComboBox cboCustomer;

    protected void Page_Init(object sender, EventArgs e)
    {
        //if (IsCallback) { LoadViewState(true); }
        try
        {

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

            if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
            {
                SqlConnection connMenu = BusinessTier.getConnection();
                connMenu.Open();
                SqlDataReader readerMenu = BusinessTier.getMenuList(connMenu, Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                dtMenuItems.Load(readerMenu);
                BusinessTier.DisposeReader(readerMenu);
                BusinessTier.DisposeConnection(connMenu);

                btnclikrjt.Visible = false;
                //Insert Mode in First time

            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            //if (!IsPostBack)
            //{
            //    RadGrid1.MasterTableView.IsItemInserted = false;

            //    RadGrid1.Rebind();

            //    if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
            //    {
            //        Response.Redirect("Login.aspx");
            //    }
            //    else
            //    {
            //        lblStatus.Text = "";

            //    }
            //}

        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        lblname.Text = "Hi, " + Session["Name"].ToString();
    }

    /* ==================================== Step 2:Add Purchase Order Grid 1 ===================================== */

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
        sql = "select * FROM Vw_Master_Project WHERE Deleted='" + delval + "' order by Project_ID";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        try
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)//if the item is in edit mode
            {
                GridEditFormItem editItem = (GridEditFormItem)e.Item;
                RadComboBox combo = (RadComboBox)editItem.FindControl("cboCustomer");
                combo.AutoPostBack = true;
                combo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(CboCustomer_SelectedIndexChanged);
            }
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)//if the item is in insert mode
            {
                GridEditFormInsertItem insertItem = (GridEditFormInsertItem)e.Item;
                RadComboBox combo = (RadComboBox)insertItem.FindControl("cboCustomer");
                combo.AutoPostBack = true;
                combo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(CboCustomer_SelectedIndexChanged);
            }
        }
        catch (Exception ex)
        {
            e.Item.OwnerTableView.IsItemInserted = false;

            //ShowMessage(10);
            e.Canceled = true;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "RadGrid1_ItemCreated", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid1_ItemDataBound(object source, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
           
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                upProjectFile = (RadAsyncUpload)e.Item.FindControl("upProjectFile");
                Label lblProjectID = (Label)e.Item.FindControl("lblProjectID");
                LinkButton lblFileName = (LinkButton)e.Item.FindControl("lblFileName");
                RadComboBox cboCustomer = (RadComboBox)e.Item.FindControl("cboCustomer");
                RadComboBox cboProjectManager = (RadComboBox)e.Item.FindControl("cboProjectManager");
                RadComboBox txtProjectName = (RadComboBox)e.Item.FindControl("txtProjectName");
                TextBox txtProjectNo = (TextBox)e.Item.FindControl("txtProjectNo");
                //string strprojectid = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Project_ID"]).ToString();
                HyperLink editLink = (HyperLink)e.Item.FindControl("EditLink");
                RadDatePicker dtFrom = (RadDatePicker)e.Item.FindControl("DtStart");
                RadDatePicker DtEnd = (RadDatePicker)e.Item.FindControl("DtEnd");
                RadDatePicker DtProjectAwardDate = (RadDatePicker)e.Item.FindControl("DtProjectAwardDate");

                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();

                if (!(string.IsNullOrEmpty(lblProjectID.Text.ToString())))
                {
                    //txtProjectNo.Enabled = false;
                    string strpath = WebConfigurationManager.AppSettings["WC_FilePath"].ToString(), FileName = "";
                    string sql1 = "select CUSTOMER_ID,STAFF_ID,Filename,CUSTOMER_NAME,StartDate,EndDate,ProjectAwardDate,Project_NameID from Vw_Master_Project WHERE Deleted = 0 and Project_ID = '" + lblProjectID.Text.ToString().Trim() + "'";
                    SqlCommand command1 = new SqlCommand(sql1, conn);
                    SqlDataReader reader1 = command1.ExecuteReader();
                    if (reader1.Read())
                    {
                        //editLink.Attributes["href"] = "";
                        //editLink.Attributes["onclick"] = String.Format("return ShowEditForm('{0}','{1}');", reader1["CUSTOMER_ID"].ToString().Trim(), reader1["CUSTOMER_NAME"].ToString().Trim());

                        dtFrom.DbSelectedDate = Convert.ToDateTime(reader1["StartDate"].ToString());
                        DtEnd.DbSelectedDate = Convert.ToDateTime(reader1["EndDate"].ToString());
                        DtProjectAwardDate.DbSelectedDate = Convert.ToDateTime(reader1["ProjectAwardDate"].ToString());

                        cboCustomer.SelectedValue = reader1["CUSTOMER_ID"].ToString();
                        cboCustomer.ToolTip = reader1["CUSTOMER_ID"].ToString();
                        cboProjectManager.ToolTip = reader1["CUSTOMER_ID"].ToString();

                        cboProjectManager.SelectedValue = reader1["STAFF_ID"].ToString();
                        txtProjectName.SelectedValue = reader1["Project_NameID"].ToString();
                        txtProjectName.ToolTip = reader1["Project_NameID"].ToString();
                        //upProjectFile.TargetFolder = strpath.ToString() + reader1["Filename"].ToString();
                        //upProjectFile.Localization.Select = strpath.ToString() + reader1["Filename"].ToString();
                        if (string.IsNullOrEmpty(reader1["Filename"].ToString().Trim()))
                        {
                            upProjectFile.Visible = true;
                            lblFileName.Visible = false;
                        }
                        else
                        {
                            lblFileName.Visible = true;
                            lblFileName.Text = reader1["Filename"].ToString();
                            lblFileparameter.Text = reader1["Filename"].ToString();
                            // file = reader1["Filename"].ToString();
                        }
                        //lblFileName.Visible = true;
                        //lblFileName.Text = reader1["Filename"].ToString();
                        //lblFileName.Visible = true;
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

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            RadComboBox txtProjectName = (RadComboBox)editedItem.FindControl("txtProjectName");
            TextBox txtProjectNo = (TextBox)editedItem.FindControl("txtProjectNo");
            RadDatePicker DtStart = (RadDatePicker)editedItem.FindControl("DtStart");
            RadDatePicker DtEnd = (RadDatePicker)editedItem.FindControl("DtEnd");
            RadNumericTextBox txtTenderValue = (RadNumericTextBox)editedItem.FindControl("txtTenderValue");
            RadComboBox cboTenderCurrency = (RadComboBox)editedItem.FindControl("cboTenderCurrency");
            RadComboBox cboSupplyScope = (RadComboBox)editedItem.FindControl("cboSupplyScope");
            RadDatePicker DtProjectAwardDate = (RadDatePicker)editedItem.FindControl("DtProjectAwardDate");
            RadComboBox cboCustomer = (RadComboBox)editedItem.FindControl("cboCustomer");
            RadNumericTextBox txtBank = (RadNumericTextBox)editedItem.FindControl("txtBank");
            RadComboBox cboBankCurrency = (RadComboBox)editedItem.FindControl("cboBankCurrency");
            RadNumericTextBox txtInsurance = (RadNumericTextBox)editedItem.FindControl("txtInsurance");
            RadComboBox cboInsuranceCurrency = (RadComboBox)editedItem.FindControl("cboInsuranceCurrency");
            RadComboBox cboProjectManager = (RadComboBox)editedItem.FindControl("cboProjectManager");

            RadAsyncUpload upProjectFile = (RadAsyncUpload)editedItem.FindControl("upProjectFile");
            TextBox txtDescription = (TextBox)editedItem.FindControl("txtDescription");
            LinkButton lblCertificate = (LinkButton)editedItem.FindControl("lblCertificate");

            RadComboBox txtSupplyScope = (RadComboBox)editedItem.FindControl("txtSupplyScope");

            //if (string.IsNullOrEmpty(Convert.ToString(DtRocregdate.SelectedDate)))
            //{
            //    DtRocregdate.SelectedDate = DateTime.Now;
            // }

            string strCheckflag = "0";
            SqlDataReader reader = BusinessTier.IDCheck(conn, txtProjectNo.Text.ToString().Trim(), "Pro");
            if (reader.Read())
            {
                strCheckflag = reader["IDCheck"].ToString();

            }
            BusinessTier.DisposeReader(reader);

            if (strCheckflag.ToString() == "1")
            {
                lblStatus.Text = "Project Data is already Exist";
                return;
            }
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
            if (string.IsNullOrEmpty(txtBank.Text.ToString().Trim()))
            {
                txtBank.Text = "0";

            }
            if (string.IsNullOrEmpty(txtInsurance.Text.ToString().Trim()))
            {
                txtInsurance.Text = "0";

            }
            if (cboSupplyScope.Text == "--Select Supply Scope--")
            {
                cboSupplyScope.Text = "";
            }
            if (txtSupplyScope.Text == "--Select--")
            {
                txtSupplyScope.Text = "";
            }
            string strpath = WebConfigurationManager.AppSettings["WC_FilePath"].ToString(), FileName = "";

            ////************************>>> FileUpload <<<<************************\\\\

            //if (upProjectFile.UploadedFiles.Count > 0)
            //{
            //    foreach (UploadedFile f in upProjectFile.UploadedFiles)
            //    {
            //        FileName = f.GetName().ToString().Trim();
            //        f.SaveAs(strpath.ToString() + f.GetName(), true);

            //        //upProjectFile.Visible = false;
            //        //lblCertificate.Text = CertificateFileName.ToString();
            //        //lblCertificate.Visible = true;
            //    }
            //}

            int flg = BusinessTier.SaveProjectMaster(conn, 1, txtProjectNo.Text.ToString().Trim(), txtProjectName.Text.ToString().Trim(), txtDescription.Text.ToString().Trim(), cboSupplyScope.Text.ToString().Trim(), txtSupplyScope.Text.ToString().Trim(), StartDate, EndDate, Convert.ToDouble(txtTenderValue.Text.ToString().Trim()), cboTenderCurrency.Text.ToString().Trim(), ProjectAwardDate, Convert.ToInt32(cboCustomer.SelectedValue.ToString().Trim()), Convert.ToDouble(txtBank.Text.ToString().Trim()), cboBankCurrency.Text.ToString().Trim(), Convert.ToDouble(txtInsurance.Text.ToString().Trim()), cboInsuranceCurrency.Text.ToString().Trim(), Convert.ToInt32(cboProjectManager.SelectedValue.ToString().Trim()), FileName.ToString(), Convert.ToInt32(txtProjectName.SelectedValue.ToString().Trim()), Session["sesUserID"].ToString(), "N");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(128);
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
            RadComboBox txtProjectName = (RadComboBox)editedItem.FindControl("txtProjectName");
            TextBox txtProjectNo = (TextBox)editedItem.FindControl("txtProjectNo");
            RadDatePicker DtStart = (RadDatePicker)editedItem.FindControl("DtStart");
            RadDatePicker DtEnd = (RadDatePicker)editedItem.FindControl("DtEnd");
            RadNumericTextBox txtTenderValue = (RadNumericTextBox)editedItem.FindControl("txtTenderValue");
            RadComboBox cboTenderCurrency = (RadComboBox)editedItem.FindControl("cboTenderCurrency");
            RadComboBox cboSupplyScope = (RadComboBox)editedItem.FindControl("cboSupplyScope");
            RadDatePicker DtProjectAwardDate = (RadDatePicker)editedItem.FindControl("DtProjectAwardDate");
            RadComboBox cboCustomer = (RadComboBox)editedItem.FindControl("cboCustomer");
            RadNumericTextBox txtBank = (RadNumericTextBox)editedItem.FindControl("txtBank");
            RadComboBox cboBankCurrency = (RadComboBox)editedItem.FindControl("cboBankCurrency");
            RadNumericTextBox txtInsurance = (RadNumericTextBox)editedItem.FindControl("txtInsurance");
            RadComboBox cboInsuranceCurrency = (RadComboBox)editedItem.FindControl("cboInsuranceCurrency");
            RadComboBox cboProjectManager = (RadComboBox)editedItem.FindControl("cboProjectManager");
            RadAsyncUpload upProjectFile = (RadAsyncUpload)editedItem.FindControl("upProjectFile");
            TextBox txtDescription = (TextBox)editedItem.FindControl("txtDescription");
            LinkButton lblCertificate = (LinkButton)editedItem.FindControl("lblCertificate");

            RadComboBox txtSupplyScope = (RadComboBox)editedItem.FindControl("txtSupplyScope");

            //if (string.IsNullOrEmpty(Convert.ToString(DtRocregdate.SelectedDate)))
            //{
            //    DtRocregdate.SelectedDate = DateTime.Now;
            // }

            //string strCheckflag = "0";
            //SqlDataReader reader = BusinessTier.IDCheck(conn, txtProjectNo.Text.ToString().Trim(), "Pro");
            //if (reader.Read())
            //{
            //    strCheckflag = reader["IDCheck"].ToString();

            //}
            //BusinessTier.DisposeReader(reader);

            //if (strCheckflag.ToString() == "1")
            //{
            //    lblStatus.Text = "Project Data is already Exist";
            //    return;
            //}

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
            if (string.IsNullOrEmpty(txtBank.Text.ToString().Trim()))
            {
                txtBank.Text = "0";

            }
            if (string.IsNullOrEmpty(txtInsurance.Text.ToString().Trim()))
            {
                txtInsurance.Text = "0";

            }
            if (cboSupplyScope.Text == "--Select Supply Scope--")
            {
                cboSupplyScope.Text = "";
            }
            if (txtSupplyScope.Text == "--Select--")
            {
                txtSupplyScope.Text = "";
            }

            string strpath = WebConfigurationManager.AppSettings["WC_FilePath"].ToString(), FileName = "";

            ////************************>>> FileUpload <<<<************************\\\\

            //if (upProjectFile.UploadedFiles.Count > 0)
            //{
            //    foreach (UploadedFile f in upProjectFile.UploadedFiles)
            //    {
            //        FileName = f.GetName().ToString().Trim();
            //        f.SaveAs(strpath.ToString() + f.GetName(), true);

            //        //upProjectFile.Visible = false;
            //        //lblCertificate.Text = CertificateFileName.ToString();
            //        //lblCertificate.Visible = true;
            //    }
            //}

            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Project_ID"].ToString();
            int intCustID = 0;
            if (string.IsNullOrEmpty(cboCustomer.SelectedValue.ToString().Trim()))
            {
                intCustID = Convert.ToInt32(cboCustomer.ToolTip.ToString().Trim());
            }
            else
                intCustID = Convert.ToInt32(cboCustomer.SelectedValue.ToString().Trim());

            int flg = BusinessTier.SaveProjectMaster(conn, Convert.ToInt32(ID.ToString()), txtProjectNo.Text.ToString().Trim(), txtProjectName.Text.ToString().Trim(), txtDescription.Text.ToString().Trim(), cboSupplyScope.Text.ToString().Trim(), txtSupplyScope.Text.ToString().Trim(), StartDate, EndDate, Convert.ToDouble(txtTenderValue.Text.ToString().Trim()), cboTenderCurrency.Text.ToString().Trim(), ProjectAwardDate, intCustID, Convert.ToDouble(txtBank.Text.ToString().Trim()), cboBankCurrency.Text.ToString().Trim(), Convert.ToDouble(txtInsurance.Text.ToString().Trim()), cboInsuranceCurrency.Text.ToString().Trim(), Convert.ToInt32(cboProjectManager.SelectedValue.ToString().Trim()), FileName.ToString(), Convert.ToInt32(txtProjectName.SelectedValue.ToString().Trim()), Session["sesUserID"].ToString(), "U");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(130);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "Update", "Success", "Log");
        }
        catch (Exception ex)
        {
            //ShowMessage(5);
            lblStatus.Text = ex.ToString();
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
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Project_ID"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveProjectMaster(conn, Convert.ToInt32(ID.ToString()), "", "", "", "", "", DateTime.Now, DateTime.Now, 1.00, "", DateTime.Now, 1, 1, "", 1, "", 1, "", 1, Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(129);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "Delete", ex.ToString(), "Audit");
        }
    }    

    //protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    //{
    //    if (e.CommandName == "RowClick" && e.Item is GridDataItem)
    //    {
    //        e.Item.Selected = true;
    //        string strProject_ID = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Project_ID"]).ToString(), strFileName = string.Empty;

    //        SqlConnection conn = BusinessTier.getConnection();
    //        conn.Open();
    //        //txtProjectNo.Enabled = false;
    //        string strpath = WebConfigurationManager.AppSettings["WC_FileGetPath"].ToString(), FileName = "";
    //        string sql1 = "select CUSTOMER_ID,STAFF_ID,Filename from Vw_Master_Project WHERE Deleted = 0 and Project_ID = '" + strProject_ID.ToString().Trim() + "'";
    //        SqlCommand command1 = new SqlCommand(sql1, conn);
    //        SqlDataReader reader1 = command1.ExecuteReader();
    //        if (reader1.Read())
    //        {
    //            strFileName = strpath.ToString().Trim() + reader1["Filename"].ToString().Trim();
    //        }
    //        if (!(string.IsNullOrEmpty(strFileName.ToString().Trim())))
    //        {
    //            // No need to download automatic.
    //            //------------------------------------
    //            //string strPah = WebConfigurationManager.AppSettings["WC_POGetPath"].ToString();
    //            ////string strLink = "http://localhost/SirimNew/PO/" + strFileName.Trim();
    //            //string strLink = strPah + strFileName.Trim();

    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenNewTab", "window.open('" + strFileName + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=800,height=1200');", true);
    //        }
    //    }
    //}

    protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {

        if (e.CommandName == "Download" && e.Item is GridDataItem)
        {
            try
            {
                // GridEditFormItem editedItem = e.Item as GridEditFormItem;
                // GridCommandEventArgs editedItem;
                //GridDataItem item = (GridDataItem)e.Item;
                // GridDataItem databounditem = e.Item as GridDataItem;
                // lblFileName = source as LinkButton;
                //LinkButton lblFileName = editedItem.FindControl("lblFileName") as LinkButton;
                //lblFileName = (LinkButton)editedItem.FindControl("lblFileName");
                string strPah = WebConfigurationManager.AppSettings["WC_FileGetPath"].ToString();
                string strLink = strPah + lblFileparameter.Text.ToString().Trim();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenNewTab", "window.open('" + strLink + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=400,height=200');", true);

            }
            catch (Exception ex)
            {

                //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "RadGrid1_ItemCommand", ex.ToString(), "Audit");

            }
        }
    }

    protected void lnkDownload_OnClick(object sender, EventArgs e)
    {
        try
        {
            string strPah = WebConfigurationManager.AppSettings["WC_FileGetPath"].ToString();
            string strLink = strPah + lnkDownload.Text.ToString().Trim();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenNewTab", "window.open('" + strLink + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=400,height=200');", true);
        }
        catch (Exception Ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

   //----------------------------- OnItemsRequested -------------------------------------------

    protected void CboCustomer_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "select distinct(CUSTOMER_ID) as CUSTOMER_ID,CUSTOMER_CODE,CUSTOMER_NAME from Vw_Master_Project where DELETED=0 and  [CUSTOMER_NAME] LIKE @Text + '%' order by CUSTOMER_NAME";
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

    protected void CboCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //RadComboBox comboBox = (RadComboBox)sender;
            //GridEditFormItem editedItem = (GridEditFormItem)comboBox.NamingContainer;
            //RadComboBox cboCustomer = editedItem.FindControl("cboCustomer") as RadComboBox;
            //custid = cboCustomer.Text.ToString();
        }
        catch (Exception ex)
        {

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "cboProject_SelectedIndexChanged", ex.ToString(), "Audit");
        }

    }

    protected void txtProjectName_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            RadComboBox comboBox = (RadComboBox)sender;
            GridEditFormItem editedItem = (GridEditFormItem)comboBox.NamingContainer;
            RadComboBox cboCustomer = editedItem.FindControl("cboCustomer") as RadComboBox;
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "",cust="";
            cust = cboCustomer.SelectedValue.ToString(); //and CUSTOMER_ID=" + custid.ToString() + "
            sql1 = "select Project_Name,Project_NameID from Master_Project_Name where DELETED=0 and CUSTOMER_ID=" + cust.ToString() + " and [Project_Name] LIKE '%' + @text + '%'  order by Project_Name";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);

            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Project_Name"].ToString().Trim();
                item.Value = row["Project_NameID"].ToString().Trim();
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

    protected void cboContact1_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(cboProject.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please Choose Project";
                return;
            }
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "select CUSTOMER_ID,CONTACT_ID,CONTACT_PERSON,CONTACT_NO from Master_CustomerContact where DELETED=0 and [CONTACT_PERSON] LIKE '%' + @text + '%' and CUSTOMER_ID='" + txtCustomerName.ToolTip.ToString() + "'";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["CONTACT_PERSON"].ToString();
                item.Value = row["CONTACT_ID"].ToString();
                item.Attributes.Add("CUSTOMER_ID", row["CUSTOMER_ID"].ToString());
                item.Attributes.Add("CONTACT_NO", row["CONTACT_NO"].ToString());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
            // Session["rdobutton"] = "";
        }


        catch (Exception ex)
        {

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "cboContact1_OnItemsRequested", ex.ToString(), "Audit");
        }

    }

    //----------------------------- Onclick -------------------------------------------

    protected void btnclikrjt_Onclick(object sender, EventArgs e)
    {
        lnkDownload.Visible = false;
        lnkDownload.Text = string.Empty;
        btnclikrjt.Visible = false;
        upProjectFile.Visible = true;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (string.IsNullOrEmpty(cboProject.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please Choose Project";
                return;
            }
            if (string.IsNullOrEmpty(txtDesignation.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please Enter Designation";
                return;
            }
            if (string.IsNullOrEmpty(txtManpower.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please Enter Manpower";
                return;
            }
            int flg = BusinessTier.SaveProjectMasterContact(conn, 1, Convert.ToInt32(cboProject.SelectedValue.ToString().Trim()), txtDesignation.Text.ToString().ToUpper().Trim(), Convert.ToInt32(txtManpower.Text.ToString().Trim()), Session["sesUserID"].ToString(), "N");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(133);
            }
            BusinessTier.DisposeConnection(conn);
            RadGrid2.DataSource = DataSourceHelper2(cboProject.SelectedValue.ToString().Trim());
            RadGrid2.Rebind();
            Clear1();
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "btnAdd_Click", ex.ToString(), "Audit");
        }


    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (string.IsNullOrEmpty(cboProject.Text.ToString().Trim()))
            {
                lblStatus.Text = "Please Choose Project";
                return;
            }
            if (string.IsNullOrEmpty(cboContact1.SelectedValue.ToString().Trim()))
            {
                cboContact1.SelectedValue = "0";
            }
            if (string.IsNullOrEmpty(cboContact2.SelectedValue.ToString().Trim()))
            {
                cboContact2.SelectedValue = "0";
            }
            if (string.IsNullOrEmpty(cboContact3.SelectedValue.ToString().Trim()))
            {
                cboContact3.SelectedValue = "0";
            }


            string strpath = WebConfigurationManager.AppSettings["WC_FilePath"].ToString(), FileName = "";

            ////************************>>> FileUpload <<<<************************\\\\

            if (upProjectFile.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in upProjectFile.UploadedFiles)
                {
                    FileName = f.GetName().ToString().Trim();
                    f.SaveAs(strpath.ToString() + f.GetName(), true);

                    //upProjectFile.Visible = false;
                    //lblCertificate.Text = CertificateFileName.ToString();
                    //lblCertificate.Visible = true;
                }
            }
            if (string.IsNullOrEmpty(FileName.ToString().Trim()))
            {
                FileName = lnkDownload.Text.ToString();
            }
            int flg = BusinessTier.SaveProjectMasterUpdate(conn, Convert.ToInt32(cboProject.SelectedValue.ToString().Trim()), Convert.ToInt32(cboContact1.SelectedValue.ToString().Trim()), Convert.ToInt32(cboContact2.SelectedValue.ToString().Trim()), Convert.ToInt32(cboContact3.SelectedValue.ToString().Trim()), FileName.ToString().Trim(), Session["sesUserID"].ToString(), "U");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(130);
            }
            BusinessTier.DisposeConnection(conn);
            Clear();
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "btnUpdate_Click", ex.ToString(), "Audit");
        }


    }

    /* ================================ Manpower Loading Grid 3 ===================================== */

    protected void RadGrid2_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(cboProject.Text.ToString().Trim()))
            {
                cboProject.SelectedValue = "0";
            }
            RadGrid2.DataSource = DataSourceHelper2(cboProject.SelectedValue.ToString().Trim());
            //RadGrid2.Rebind();
        }
        catch (Exception ex)
        {
            //ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper2(string projectid)
    {
        int delval = 0;
        string sql = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        sql = "select * FROM Master_Project_Contact WHERE Deleted='" + delval + "' and project_id='" + projectid.ToString() + "'";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void cboProject_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            //if (string.IsNullOrEmpty(cboCompany.SelectedValue))
            sql1 = " select Project_Name,STAFF_NAME,Project_ID,Project_No from Vw_Master_Project where DELETED=0 and [Project_No] LIKE '%' + @text + '%'  order by Project_Name";
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
                item.Text = row["Project_No"].ToString().Trim();
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
        try
        {
            lblStatus.Text = "";
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select CUSTOMER_ID,CUSTOMER_NAME,CustCont_ID1,CustCont_ID2,CustCont_ID3,CONTACT_PERSON1,CONTACT_PERSON2,CONTACT_PERSON3,FileName from Vw_Master_Project where DELETED=0 and [Project_ID] ='" + cboProject.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql1, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                txtCustomerName.Text = rd["CUSTOMER_NAME"].ToString();
                txtCustomerName.ToolTip = rd["CUSTOMER_ID"].ToString();
                cboContact1.Text = rd["CONTACT_PERSON1"].ToString();
                cboContact1.SelectedValue = rd["CustCont_ID1"].ToString();
                cboContact2.Text = rd["CONTACT_PERSON2"].ToString();
                cboContact2.SelectedValue = rd["CustCont_ID2"].ToString();
                cboContact3.Text = rd["CONTACT_PERSON3"].ToString();
                cboContact3.SelectedValue = rd["CustCont_ID3"].ToString();
                if (string.IsNullOrEmpty(rd["FileName"].ToString().Trim()))
                {
                    lnkDownload.Visible = false;
                    btnclikrjt.Visible = false;
                    upProjectFile.Visible = true;
                }
                else
                {
                    lnkDownload.Visible = true;
                    lnkDownload.Text = rd["FileName"].ToString().Trim();
                    btnclikrjt.Visible = true;
                    upProjectFile.Visible = false;
                }
            }

            BusinessTier.DisposeConnection(conn);

            RadGrid2.DataSource = DataSourceHelper2(cboProject.SelectedValue.ToString().Trim());
            RadGrid2.Rebind();
        }


        catch (Exception ex)
        {

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "cboProject_SelectedIndexChanged", ex.ToString(), "Audit");
        }

    }

    protected void RadGrid2_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Project_Contact_id"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveProjectMasterContact(conn, Convert.ToInt32(ID.ToString()), 1, "", 1, Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(134);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "Delete", ex.ToString(), "Audit");
        }
    }

    /* Add Project Grid 3 */

    protected void RadGrid3_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {

            RadGrid3.DataSource = DataSourceHelper3();
            //RadGrid3.Rebind();
        }
        catch (Exception ex)
        {
            //ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "RadGrid3_NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper3()
    {
        int delval = 0;
        string sql = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        sql = "select * FROM Vw_Master_Project_Name order by Project_NameID desc";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void RadGrid3_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Project_NameID"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveProjectName(conn, Convert.ToInt32(ID.ToString()), "", 1, "", Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                lblStatus.Text = "Successfully deleted project name";
                lblStatus.ForeColor = Color.Red;
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "RadGrid3_DeleteCommand", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "RadGrid3_DeleteCommand", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid3_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            TextBox txtProjectName = (TextBox)editedItem.FindControl("txtProjectName");
            RadComboBox cboClient = (RadComboBox)editedItem.FindControl("cboClient");
            TextBox txtContractDuration = (TextBox)editedItem.FindControl("txtContractDuration");

            int flg = BusinessTier.SaveProjectName(conn, 1, txtProjectName.Text.ToString().ToUpper().Trim(), Convert.ToInt32(cboClient.SelectedValue.ToString().Trim()), txtContractDuration.Text.ToString().ToUpper().Trim(), Session["sesUserID"].ToString(), "N");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                lblStatus.Text = "Successfully saved project name";
                lblStatus.ForeColor = Color.Green;
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "RadGrid3_InsertCommand", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "RadGrid3_InsertCommand", ex.ToString(), "Audit");
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

        RadGrid1.Rebind();
    }

    protected void RadGrid3_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            TextBox txtProjectName = (TextBox)editedItem.FindControl("txtProjectName");
            RadComboBox cboClient = (RadComboBox)editedItem.FindControl("cboClient");
            TextBox txtContractDuration = (TextBox)editedItem.FindControl("txtContractDuration");
            Label lblProject_NameID = (Label)editedItem.FindControl("lblProject_NameID");
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Project_NameID"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveProjectName(conn, Convert.ToInt32(lblProject_NameID.Text.ToString()), txtProjectName.Text.ToString().ToUpper().Trim(), Convert.ToInt32(cboClient.SelectedValue.ToString().Trim()), txtContractDuration.Text.ToString().ToUpper().Trim(), Session["sesUserID"].ToString(), "U");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                lblStatus.Text = "Successfully modified project name";
                lblStatus.ForeColor = Color.Blue;
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "RadGrid3_UpdateCommand", "Success", "Log");
        }
        catch (Exception ex)
        {
            //ShowMessage(5);
            //lblStatus.Text = ex.ToString();
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project", "RadGrid3_UpdateCommand", ex.ToString(), "Audit");
        }


        RadGrid1.Rebind();
    }

    protected void RadGrid3_ItemDataBound(object source, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {

                RadComboBox cboClient = (RadComboBox)e.Item.FindControl("cboClient");
                Label lblProject_NameID = (Label)e.Item.FindControl("lblProject_NameID");

                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                string sql1 = "select ClientID from Vw_Master_Project WHERE Deleted = 0 and Project_NameID = '" + lblProject_NameID.Text.ToString().Trim() + "'";
                SqlCommand command1 = new SqlCommand(sql1, conn);
                SqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.Read())
                {
                    cboClient.SelectedValue = reader1["ClientID"].ToString();
                }

                BusinessTier.DisposeReader(reader1);
                BusinessTier.DisposeConnection(conn);
            }
        }
        catch (Exception ex)
        {
        }
    }

    //protected void cboContactName1_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    //{
    //    try
    //    {
    //        SqlConnection conn = BusinessTier.getConnection();
    //        conn.Open();
    //        if (string.IsNullOrEmpty(cboCustomerId.SelectedValue.ToString().Trim()))
    //        {
    //            lblStatus.Text = "Please Select the Customer Name";
    //            return;
    //        }
    //        string sql1 = " select Contact_Id,CONTACT_PERSON,Department from Master_CustomerContact where DELETED=0 and Customer_ID='" + cboCustomerId.SelectedValue + "' and  [CONTACT_PERSON] LIKE '%' + @text + '%' order by CONTACT_PERSON";
    //        SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
    //        adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
    //        DataTable dataTable1 = new DataTable();
    //        adapter1.Fill(dataTable1);
    //        RadComboBox comboBox = (RadComboBox)sender;
    //        comboBox.Items.Clear();
    //        foreach (DataRow row in dataTable1.Rows)
    //        {
    //            RadComboBoxItem item = new RadComboBoxItem();
    //            item.Text = row["CONTACT_PERSON"].ToString();
    //            item.Value = row["Contact_Id"].ToString();
    //            item.Attributes.Add("CONTACT_PERSON", row["CONTACT_PERSON"].ToString());
    //            item.Attributes.Add("Department", row["Department"].ToString());
    //            comboBox.Items.Add(item);
    //            item.DataBind();
    //        }
    //        adapter1.Dispose();
    //        BusinessTier.DisposeConnection(conn);
    //    }
    //    catch (Exception ex)
    //    {
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

    public void Clear()
    {
        cboProject.Text = "";
        cboContact1.Text = "";
        cboContact2.Text = "";
        cboContact3.Text = "";
        txtCustomerName.Text = "";
        lnkDownload.Visible = false;
        lnkDownload.Text = string.Empty;
        btnclikrjt.Visible = false;
        upProjectFile.Visible = true;
    }

    public void Clear1()
    {
        txtDesignation.Text = "";
        txtManpower.Text = "";

    }
}