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
using System.Web.Configuration;
using System.IO;
using ClosedXML.Excel;
using Excel = Microsoft.Office.Interop.Excel;

public partial class Master_Staff : System.Web.UI.Page
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
            lblname.Text = "Hi, " + Session["Name"].ToString();

        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void cboDeptId_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Dept_ID,Dept_Name,Dept_Code from Master_Department where DELETED=0  and [Dept_Name] LIKE '%' + @text + '%' order by Dept_Name";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Dept_Name"].ToString();
                item.Value = row["Dept_ID"].ToString();
                item.Attributes.Add("Dept_Name", row["Dept_Name"].ToString());
                item.Attributes.Add("Dept_Code", row["Dept_Code"].ToString());
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
            string sql1 = " select Branch_ID,Branch_Name,Branch_Code from Master_Branch where DELETED=0  and [Branch_Name] LIKE '%' + @text + '%' order by Branch_Name";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Branch_Name"].ToString();
                item.Value = row["Branch_ID"].ToString();
                item.Attributes.Add("Branch_Name", row["Branch_Name"].ToString());
                item.Attributes.Add("Branch_Code", row["Branch_Code"].ToString());
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

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {

        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                Label lbl = (Label)editedItem.FindControl("lblStaffID");
                //Label lblStatus = (Label)editedItem.FindControl("lblStatus");
                RadComboBox cboDeptId = (RadComboBox)editedItem.FindControl("cboDeptId");
                RadDatePicker DtBirth = (RadDatePicker)editedItem.FindControl("DtBirth");
                RadDatePicker DtPassportExpiry = (RadDatePicker)editedItem.FindControl("DtPassportExpiry");
                RadDatePicker DtVisaEntry = (RadDatePicker)editedItem.FindControl("DtVisaEntry");
                RadDatePicker DtVisaExpiry = (RadDatePicker)editedItem.FindControl("DtVisaExpiry");

                LinkButton lnkDownload = (LinkButton)editedItem.FindControl("lnkDownload");
                Button btnclikrjt = (Button)editedItem.FindControl("btnclikrjt");

                //GridDataItem editedItem = e.Item as GridDataItem;
                RadAsyncUpload fupPhoto = (RadAsyncUpload)editedItem.FindControl("fupPhoto");
                LinkButton lnkPhotodownload = (LinkButton)editedItem.FindControl("lnkPhotodownload");
                Button btnphotoreject = (Button)editedItem.FindControl("btnphotoreject");

                RadAsyncUpload fupResume = (RadAsyncUpload)editedItem.FindControl("fupResume");
                LinkButton lnkResumeDownload = (LinkButton)editedItem.FindControl("lnkResumeDownload");
                Button btnResumeReject = (Button)editedItem.FindControl("btnResumeReject");

                RadAsyncUpload fupCertificate = (RadAsyncUpload)editedItem.FindControl("fupCertificate");
                LinkButton lnkCertificateDownload = (LinkButton)editedItem.FindControl("lnkCertificateDownload");
                Button btnCertificateReject = (Button)editedItem.FindControl("btnCertificateReject");

                lnkPhotodownload.Visible = false;
                btnphotoreject.Visible = false;

                lnkResumeDownload.Visible = false;
                btnResumeReject.Visible = false;

                lnkCertificateDownload.Visible = false;
                btnCertificateReject.Visible = false;

                if (!(string.IsNullOrEmpty(lbl.Text.ToString())))
                {
                    //string strDeptName = "";
                    //int intdeptvalue = 0;
                    SqlConnection conn = BusinessTier.getConnection();
                    conn.Open();
                    string sql = "select * FROM Master_Staff WHERE Deleted = 0 and Staff_id = '" + lbl.Text.ToString() + "'";
                    SqlCommand command = new SqlCommand(sql, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {

                        if (!(string.IsNullOrEmpty(reader["Photo"].ToString())))
                        {
                            fupPhoto.Visible = false;
                            lnkPhotodownload.Text = reader["Photo"].ToString();
                            lnkPhotodownload.Visible = true;
                            btnphotoreject.Visible = true;
                        }
                        if (!(string.IsNullOrEmpty(reader["Resume"].ToString())))
                        {
                            fupResume.Visible = false;
                            lnkResumeDownload.Text = reader["Resume"].ToString();
                            lnkResumeDownload.Visible = true;
                            btnResumeReject.Visible = true;
                        }
                        if (!(string.IsNullOrEmpty(reader["Certificate"].ToString())))
                        {
                            fupCertificate.Visible = false;
                            lnkCertificateDownload.Text = reader["Certificate"].ToString();
                            lnkCertificateDownload.Visible = true;
                            btnCertificateReject.Visible = true;
                        }

                    }
                    BusinessTier.DisposeReader(reader);
                    BusinessTier.DisposeConnection(conn);

                }

            }
            if (e.Item is GridDataItem)
            {

            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.ToString();
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
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        int delval = 0;
        string sql = "";
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        sql = "select * FROM Master_Staff WHERE Deleted='" + delval + "' order by Staff_ID";
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
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Staff_Id"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveStaffMaster(conn, Convert.ToInt32(ID.ToString()), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(41);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "Delete", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            TextBox txtStaffNo = (TextBox)editedItem.FindControl("txtStaffNo");
            TextBox txtStaffame = (TextBox)editedItem.FindControl("txtStaffame");
            RadDatePicker DtBirth = (RadDatePicker)editedItem.FindControl("DtBirth");
            TextBox txtNationality = (TextBox)editedItem.FindControl("txtNationality");
            TextBox txtPhone = (TextBox)editedItem.FindControl("txtPhone");
            TextBox txtPhone2 = (TextBox)editedItem.FindControl("txtPhone2");
            TextBox txtEmail = (TextBox)editedItem.FindControl("txtEmail");
            TextBox txtIC = (TextBox)editedItem.FindControl("txtIC");
            TextBox txtPassport = (TextBox)editedItem.FindControl("txtPassport");
            RadDatePicker DtPassportExpiry = (RadDatePicker)editedItem.FindControl("DtPassportExpiry");
            RadDatePicker DtVisaEntry = (RadDatePicker)editedItem.FindControl("DtVisaEntry");
            RadDatePicker DtVisaExpiry = (RadDatePicker)editedItem.FindControl("DtVisaExpiry");
            TextBox txtPermanentAddress = (TextBox)editedItem.FindControl("txtPermanentAddress");
            TextBox txtTemporaryAddress = (TextBox)editedItem.FindControl("txtTemporaryAddress");
            RadAsyncUpload fupPhoto = (RadAsyncUpload)editedItem.FindControl("fupPhoto");
            RadAsyncUpload fupResume = (RadAsyncUpload)editedItem.FindControl("fupResume");
            RadAsyncUpload fupCertificate = (RadAsyncUpload)editedItem.FindControl("fupCertificate");
            string strCheckflag = "0";
            SqlDataReader reader = BusinessTier.IDCheck(conn, txtStaffNo.Text.ToString().Trim(), "S");
            if (reader.Read())
            {
                strCheckflag = reader["IDCheck"].ToString();

            }
            BusinessTier.DisposeReader(reader);

            if (strCheckflag.ToString() == "1")
            {
                ShowMessage(39);
                return;
            }
            String DB = string.Empty, PEDT = string.Empty, VEntdt = string.Empty, VExpdt = string.Empty;

            if (string.IsNullOrEmpty(Convert.ToString(DtBirth.SelectedDate)))
            {
                DB = "01/01/1900 00:00:00";
            }
            else
            {
                DB = DtBirth.SelectedDate.ToString();
                DateTime idt = DateTime.Parse(DB);
                DB = idt.Month + "/" + idt.Day + "/" + idt.Year + " 00:00:00";
            }

            if (string.IsNullOrEmpty(Convert.ToString(DtPassportExpiry.SelectedDate)))
            {
                PEDT = "01/01/1900 00:00:00";

            }
            else
            {
                PEDT = DtPassportExpiry.SelectedDate.ToString();
                DateTime idt1 = DateTime.Parse(PEDT);
                PEDT = idt1.Month + "/" + idt1.Day + "/" + idt1.Year + " 00:00:00";
            }

            if (string.IsNullOrEmpty(Convert.ToString(DtVisaEntry.SelectedDate)))
            {
                VEntdt = "01/01/1900 00:00:00";

            }
            else
            {
                VEntdt = DtVisaEntry.SelectedDate.ToString();
                DateTime idt2 = DateTime.Parse(VEntdt);
                VEntdt = idt2.Month + "/" + idt2.Day + "/" + idt2.Year + " 00:00:00";
            }

            if (string.IsNullOrEmpty(Convert.ToString(DtVisaExpiry.SelectedDate)))
            {
                VExpdt = "01/01/1900 00:00:00";

            }
            else
            {
                VExpdt = DtBirth.SelectedDate.ToString();
                DateTime idt3 = DateTime.Parse(VExpdt);
                VExpdt = idt3.Month + "/" + idt3.Day + "/" + idt3.Year + " 00:00:00";
            }

            //************************>>> FileUpload <<<<************************\\\\
            string strpath = WebConfigurationManager.AppSettings["WC_FilePath1"].ToString(), PhotoFileName = "", ResumeFileName = "", CertificateFileName = "";
            if (fupPhoto.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in fupPhoto.UploadedFiles)
                {
                    string[] extension = f.GetName().ToString().Split('.');
                    PhotoFileName = txtStaffame.Text.ToString().Trim() + txtIC.Text.ToString().Trim() + txtPassport.Text.ToString().Trim() + "." + extension[1].ToString();
                    f.SaveAs(strpath.ToString() + PhotoFileName.ToString(), true);
                }
            }

            if (fupResume.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in fupResume.UploadedFiles)
                {
                    string[] extension = f.GetName().ToString().Split('.');
                    ResumeFileName = txtStaffame.Text.ToString().Trim() + txtIC.Text.ToString().Trim() + txtPassport.Text.ToString().Trim() + "." + extension[1].ToString();
                    f.SaveAs(strpath.ToString() + ResumeFileName.ToString(), true);
                }
            }

            if (fupCertificate.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in fupCertificate.UploadedFiles)
                {
                    string[] extension = f.GetName().ToString().Split('.');
                    CertificateFileName = txtStaffame.Text.ToString().Trim() + txtIC.Text.ToString().Trim() + txtPassport.Text.ToString().Trim() + "." + extension[1].ToString();
                    f.SaveAs(strpath.ToString() + CertificateFileName.ToString(), true);
                }
            }

            int flg = BusinessTier.SaveStaffMaster(conn, 1, txtStaffNo.Text.ToString().Trim().ToUpper(), txtStaffame.Text.ToString().Trim().ToUpper(), DB.ToString(), txtNationality.Text.ToString().Trim().ToUpper(), txtPhone.Text.ToString().Trim(), txtPhone2.Text.ToString().Trim(), txtEmail.Text.ToString().ToLower().Trim(), txtIC.Text.ToString().Trim(), txtPassport.Text.ToString().Trim().ToUpper(), PEDT.ToString(), VEntdt.ToString(), VExpdt.ToString(), txtPermanentAddress.Text.ToString().Trim().ToUpper(), txtTemporaryAddress.Text.ToString().Trim().ToUpper(), PhotoFileName.ToString(), ResumeFileName.ToString(), CertificateFileName.ToString(), Session["sesUserID"].ToString(), "N");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(40);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            // lblStatus.Text = ex.ToString();
            // e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "Insert", ex.ToString(), "Audit");
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
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Staff_Id"].ToString();
            TextBox txtStaffNo = (TextBox)editedItem.FindControl("txtStaffNo");
            TextBox txtStaffame = (TextBox)editedItem.FindControl("txtStaffame");
            RadDatePicker DtBirth = (RadDatePicker)editedItem.FindControl("DtBirth");
            TextBox txtNationality = (TextBox)editedItem.FindControl("txtNationality");
            TextBox txtPhone = (TextBox)editedItem.FindControl("txtPhone");
            TextBox txtPhone2 = (TextBox)editedItem.FindControl("txtPhone2");
            TextBox txtEmail = (TextBox)editedItem.FindControl("txtEmail");
            TextBox txtIC = (TextBox)editedItem.FindControl("txtIC");
            TextBox txtPassport = (TextBox)editedItem.FindControl("txtPassport");
            RadDatePicker DtPassportExpiry = (RadDatePicker)editedItem.FindControl("DtPassportExpiry");
            RadDatePicker DtVisaEntry = (RadDatePicker)editedItem.FindControl("DtVisaEntry");
            RadDatePicker DtVisaExpiry = (RadDatePicker)editedItem.FindControl("DtVisaExpiry");
            TextBox txtPermanentAddress = (TextBox)editedItem.FindControl("txtPermanentAddress");
            TextBox txtTemporaryAddress = (TextBox)editedItem.FindControl("txtTemporaryAddress");
            Label lblExistingname = (Label)editedItem.FindControl("lblExistingname");
            RadAsyncUpload fupPhoto = (RadAsyncUpload)editedItem.FindControl("fupPhoto");
            RadAsyncUpload fupResume = (RadAsyncUpload)editedItem.FindControl("fupResume");
            RadAsyncUpload fupCertificate = (RadAsyncUpload)editedItem.FindControl("fupCertificate");
            string strCheckflag = "0";
            SqlDataReader reader = BusinessTier.IDCheck(conn, txtStaffNo.Text.ToString().Trim(), "S");
            if (reader.Read())
            {
                strCheckflag = reader["IDCheck"].ToString();

            }
            BusinessTier.DisposeReader(reader);

            if (strCheckflag.ToString() == "1")
            {
                if (!(lblExistingname.Text.ToString().Trim() == txtStaffNo.Text.ToString().Trim()))
                {
                    ShowMessage(46);
                    return;
                }
            }

            String DB = string.Empty, PEDT = string.Empty, VEntdt = string.Empty, VExpdt = string.Empty;

            if (string.IsNullOrEmpty(Convert.ToString(DtBirth.SelectedDate)))
            {
                DB = "01/01/1900 00:00:00";
            }
            else
            {
                DB = DtBirth.SelectedDate.ToString();
                DateTime idt = DateTime.Parse(DB);
                DB = idt.Month + "/" + idt.Day + "/" + idt.Year + " 00:00:00";
            }

            if (string.IsNullOrEmpty(Convert.ToString(DtPassportExpiry.SelectedDate)))
            {
                PEDT = "01/01/1900 00:00:00";

            }
            else
            {
                PEDT = DtPassportExpiry.SelectedDate.ToString();
                DateTime idt1 = DateTime.Parse(PEDT);
                PEDT = idt1.Month + "/" + idt1.Day + "/" + idt1.Year + " 00:00:00";
            }

            if (string.IsNullOrEmpty(Convert.ToString(DtVisaEntry.SelectedDate)))
            {
                VEntdt = "01/01/1900 00:00:00";

            }
            else
            {
                VEntdt = DtVisaEntry.SelectedDate.ToString();
                DateTime idt2 = DateTime.Parse(VEntdt);
                VEntdt = idt2.Month + "/" + idt2.Day + "/" + idt2.Year + " 00:00:00";
            }

            if (string.IsNullOrEmpty(Convert.ToString(DtVisaExpiry.SelectedDate)))
            {
                VExpdt = "01/01/1900 00:00:00";

            }
            else
            {
                VExpdt = DtBirth.SelectedDate.ToString();
                DateTime idt3 = DateTime.Parse(VExpdt);
                VExpdt = idt3.Month + "/" + idt3.Day + "/" + idt3.Year + " 00:00:00";
            }


            //************************>>> FileUpload <<<<************************\\\\
            string strpath = WebConfigurationManager.AppSettings["WC_FilePath1"].ToString(), PhotoFileName = "", ResumeFileName = "", CertificateFileName = "";
            if (fupPhoto.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in fupPhoto.UploadedFiles)
                {
                    string[] extension = f.GetName().ToString().Split('.');
                    PhotoFileName = txtStaffame.Text.ToString().Trim() + txtIC.Text.ToString().Trim() + txtPassport.Text.ToString().Trim() + "." + extension[1].ToString();
                    f.SaveAs(strpath.ToString() + PhotoFileName.ToString(), true);
                }
            }

            if (fupResume.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in fupResume.UploadedFiles)
                {
                    string[] extension = f.GetName().ToString().Split('.');
                    ResumeFileName = txtStaffame.Text.ToString().Trim() + txtIC.Text.ToString().Trim() + txtPassport.Text.ToString().Trim() + "." + extension[1].ToString();
                    f.SaveAs(strpath.ToString() + ResumeFileName.ToString(), true);
                }
            }

            if (fupCertificate.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in fupCertificate.UploadedFiles)
                {
                    string[] extension = f.GetName().ToString().Split('.');
                    CertificateFileName = txtStaffame.Text.ToString().Trim() + txtIC.Text.ToString().Trim() + txtPassport.Text.ToString().Trim() + "." + extension[1].ToString();
                    f.SaveAs(strpath.ToString() + CertificateFileName.ToString(), true);
                }
            }

            int flg = BusinessTier.SaveStaffMaster(conn, Convert.ToInt32(ID.ToString()), txtStaffNo.Text.ToString().Trim().ToUpper(), txtStaffame.Text.ToString().Trim().ToUpper(), DB.ToString(), txtNationality.Text.ToString().Trim().ToUpper(), txtPhone.Text.ToString().Trim(), txtPhone2.Text.ToString().Trim(), txtEmail.Text.ToString().Trim(), txtIC.Text.ToString().Trim(), txtPassport.Text.ToString().Trim().ToUpper(), PEDT.ToString(), VEntdt.ToString(), VExpdt.ToString(), txtPermanentAddress.Text.ToString().Trim().ToUpper(), txtTemporaryAddress.Text.ToString().Trim().ToUpper(), PhotoFileName.ToString(), ResumeFileName.ToString(), CertificateFileName.ToString(), Session["sesUserID"].ToString(), "U");

            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(42);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "Update", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            lblStatus.Text = ex.ToString();
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff", "Update", ex.ToString(), "Audit");
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

    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            RadGrid1.MasterTableView.ExportToExcel();
        }
    }

    //protected void RadGrid1_ExcelMLWorkBookCreated(object sender, GridExcelMLWorkBookCreatedEventArgs e)
    //{
    //    if (CheckBox2.Checked)
    //    {
    //        foreach (RowElement row in e.WorkBook.Worksheets[0].Table.Rows)
    //        {
    //            row.Cells[0].StyleValue = "Style1";
    //        }

    //        StyleElement style = new StyleElement("Style1");
    //        style.InteriorStyle.Pattern = InteriorPatternType.Solid;
    //        style.InteriorStyle.Color = System.Drawing.Color.LightGray;
    //        e.WorkBook.Styles.Add(style);
    //    }
    //}

    protected void Onclick_btnExport(object sender, EventArgs e)
    {        
        SqlConnection conn = BusinessTier.getConnection();
        try
        {   
            SqlCommand cmd = new SqlCommand("SELECT STAFF_NAME,ICNO as MYCARD,DATEDIFF(DAY, DateOfBirth, GetDate()) / 365 as AGE,PermanentAddress as ADDRESS,CONTACT_NO as MOBILENO,Nationality as NATIONALITY,CONVERT(varchar, DOJ, 103)  as HIRED_DATE,Designation as DESIGNATION,DEPT_NAME as DEPARTMENT,REPORTING_TO,BRANCH,COMPANY,OGSPNO,ogsp as OGSP,medi as MEDICAL,BOSIET,Cspace as CONFINE_SPACE,cid as CIDB,PT as PTW,permit as WORKING_PERMIT,Bording AS BORDING_PASS from VW_Staff_Personal");
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = conn;
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();           
            sda.Fill(dt);         
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "StaffDetails");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=StaffDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }         
            conn.Close();
        }
        catch (Exception ex) 
        { 
           // lblStatus.Text = ex.ToString();
        }
        finally 
        {
            conn.Close(); 
        }
    }

    protected void btnResumeReject_Onclick(object sender, EventArgs e)
    {
        Button btn = ((Button)(sender));
        GridEditFormItem editedItem = ((GridEditFormItem)(btn.NamingContainer));
        RadAsyncUpload fupResume = (RadAsyncUpload)editedItem.FindControl("fupResume");
        LinkButton lnkResumeDownload = (LinkButton)editedItem.FindControl("lnkResumeDownload");
        Button btnResumeReject = (Button)editedItem.FindControl("btnResumeReject");

        lnkResumeDownload.Visible = false;
        lnkResumeDownload.Text = string.Empty;
        btnResumeReject.Visible = false;
        fupResume.Visible = true;
    }

    protected void btnCertificateReject_Onclick(object sender, EventArgs e)
    {
        Button btn = ((Button)(sender));
        GridEditFormItem editedItem = ((GridEditFormItem)(btn.NamingContainer));
        RadAsyncUpload fupCertificate = (RadAsyncUpload)editedItem.FindControl("fupCertificate");
        LinkButton lnkCertificateDownload = (LinkButton)editedItem.FindControl("lnkCertificateDownload");
        Button btnCertificateReject = (Button)editedItem.FindControl("btnCertificateReject");

        lnkCertificateDownload.Visible = false;
        lnkCertificateDownload.Text = string.Empty;
        btnCertificateReject.Visible = false;
        fupCertificate.Visible = true;
    }

    protected void btnphotoreject_Onclick(object sender, EventArgs e)
    {
        Button btn = ((Button)(sender));
        GridEditFormItem editedItem = ((GridEditFormItem)(btn.NamingContainer));
        RadAsyncUpload fupPhoto = (RadAsyncUpload)editedItem.FindControl("fupPhoto");
        LinkButton lnkPhotodownload = (LinkButton)editedItem.FindControl("lnkPhotodownload");
        Button btnphotoreject = (Button)editedItem.FindControl("btnphotoreject");

        lnkPhotodownload.Visible = false;
        lnkPhotodownload.Text = string.Empty;
        btnphotoreject.Visible = false;
        fupPhoto.Visible = true;
    }

    protected void lnkPhotodownload_OnClick(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            GridEditFormItem editedItem = ((GridEditFormItem)(btn.NamingContainer));
            LinkButton lnkPhotodownload = (LinkButton)editedItem.FindControl("lnkPhotodownload");
            string strPah = WebConfigurationManager.AppSettings["WC_FileGetPath1"].ToString();
            string strLink = strPah + lnkPhotodownload.Text.ToString().Trim();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenNewTab", "window.open('" + strLink + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=400,height=200');", true);
        }
        catch (Exception Ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void lnkResumeDownload_OnClick(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            GridEditFormItem editedItem = ((GridEditFormItem)(btn.NamingContainer));
            LinkButton lnkResumeDownload = (LinkButton)editedItem.FindControl("lnkResumeDownload");
            string strPah = WebConfigurationManager.AppSettings["WC_FileGetPath1"].ToString();
            string strLink = strPah + lnkResumeDownload.Text.ToString().Trim();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenNewTab", "window.open('" + strLink + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=400,height=200');", true);
        }
        catch (Exception Ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void lnkCertificateDownload_OnClick(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            GridEditFormItem editedItem = ((GridEditFormItem)(btn.NamingContainer));
            LinkButton lnkCertificateDownload = (LinkButton)editedItem.FindControl("lnkCertificateDownload");
            string strPah = WebConfigurationManager.AppSettings["WC_FileGetPath1"].ToString();
            string strLink = strPah + lnkCertificateDownload.Text.ToString().Trim();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenNewTab", "window.open('" + strLink + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=400,height=200');", true);
        }
        catch (Exception Ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

}