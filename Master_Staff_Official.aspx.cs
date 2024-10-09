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
using ClosedXML.Excel;
using System.IO;

public partial class Master_Staff_Official : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    //----------------- Page -------------------------------------------------------------------//

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

    //----------------- Combobox -------------------------------------------------------------------//

    protected void cboSTAFF_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "SELECT STAFF_ID,STAFF_NO,STAFF_NAME FROM Master_Staff where DELETED=0  and [STAFF_NAME] LIKE '%' + @text + '%' and  STAFF_ID NOT IN (SELECT STAFF_ID FROM Master_Staff_Official)";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["STAFF_NAME"].ToString();
                item.Value = row["STAFF_ID"].ToString();
                item.Attributes.Add("STAFF_NAME", row["STAFF_NAME"].ToString());
                item.Attributes.Add("STAFF_NO", row["STAFF_NO"].ToString());
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

    protected void cboSTAFFReporting_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select STAFF_ID,STAFF_NO,STAFF_NAME from Vw_Staff_Details where DELETED=0  and [STAFF_NAME] LIKE '%' + @text + '%' order by STAFF_NAME";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["STAFF_NAME"].ToString();
                item.Value = row["STAFF_ID"].ToString();
                item.Attributes.Add("STAFF_NAME", row["STAFF_NAME"].ToString());
                item.Attributes.Add("STAFF_NO", row["STAFF_NO"].ToString());
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

    protected void cboCompany_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = " select Company_ID,Company_Name,Company_CODE from Master_Company where DELETED=0  and [Company_Name] LIKE '%' + @text + '%' order by Company_Name";
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
                item.Attributes.Add("Company_CODE", row["Company_CODE"].ToString());
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

    protected void cboEMPtype_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GridEditableItem editedItem = (sender as RadComboBox).NamingContainer as GridEditableItem;
        RadComboBox RdcmboBx = (sender as RadComboBox);
        RadComboBox cboBranch = (RadComboBox)editedItem.FindControl("cboBranch");
        RadComboBox cboDeptId = (RadComboBox)editedItem.FindControl("cboDeptId");       
        RadComboBox cboReporting = (RadComboBox)editedItem.FindControl("cboReporting");
        RadComboBox cboCompany = (RadComboBox)editedItem.FindControl("cboCompany");
       
        if (RdcmboBx.SelectedValue == "5")
        {
            cboBranch.Enabled = false;
            cboDeptId.Enabled = false;
            cboCompany.Enabled = false;
            cboReporting.Enabled = false;
            cboBranch.SelectedValue ="0";
            cboDeptId.SelectedValue = "0";
            cboCompany.SelectedValue = "0";
            cboReporting.SelectedValue = "0";
        }
        else
        {
            cboBranch.Enabled = true;
            cboDeptId.Enabled = true;
            cboCompany.Enabled = true;
            cboReporting.Enabled = true;
        }

        ///lblStatus.Text = "You selected " + e.Text + " item"; // === Test
    }

    //----------------- RadGrid1 -------------------------------------------------------------------//

    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridEditFormItem && e.Item.IsInEditMode)//if the item is in edit mode
        {
            GridEditFormItem editItem = (GridEditFormItem)e.Item;
            RadComboBox combo = (RadComboBox)editItem.FindControl("cboEMPtype");
            combo.AutoPostBack = true;
            combo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(cboEMPtype_SelectedIndexChanged);
        }
        if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)//if the item is in insert mode
        {
            GridEditFormInsertItem insertItem = (GridEditFormInsertItem)e.Item;
            RadComboBox combo = (RadComboBox)insertItem.FindControl("cboEMPtype");
            combo.AutoPostBack = true;
            combo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(cboEMPtype_SelectedIndexChanged);
        }
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {

            if (e.Item is GridDataItem)
            {

                //GridEditFormItem editedItem = e.Item as GridEditFormItem;
                Label lblStaffOfficial_ID = (Label)e.Item.FindControl("lblStaffOfficial_ID");

                Label lblmedi = (Label)e.Item.FindControl("lblmedi");
                Label lblh2s = (Label)e.Item.FindControl("lblh2s");
                Label lblCOMPENTANCY = (Label)e.Item.FindControl("lblCOMPENTANCY");
                Label lblcid = (Label)e.Item.FindControl("lblcid");
                Label lblCspace = (Label)e.Item.FindControl("lblCspace");
                Label lblpermit = (Label)e.Item.FindControl("lblpermit");
                Label lblBording = (Label)e.Item.FindControl("lblBording");
                Label lblPT = (Label)e.Item.FindControl("lblPT");

                string sql1 = "select * FROM Vw_Staff_Official WHERE Deleted = 0 and StaffOfficial_ID = '" + lblStaffOfficial_ID.Text.ToString() + "'";
                SqlCommand command1 = new SqlCommand(sql1, conn);
                SqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.Read())
                {
                    colortxt(reader1["MEDICALVALIDITY"].ToString(), lblmedi);
                    colortxt(reader1["H2SCERTVALIDITY"].ToString(), lblh2s);
                    colortxt(reader1["COMPENTANCYCARDVALIDITY"].ToString(), lblCOMPENTANCY);
                    colortxt(reader1["CIDB"].ToString(), lblcid);
                    colortxt(reader1["ConfineSpace"].ToString(), lblCspace);
                    colortxt(reader1["WorkPermit"].ToString(), lblpermit);
                    colortxt(reader1["BordingPass"].ToString(), lblBording);
                    colortxt(reader1["PTW"].ToString(), lblPT);                   
                }

                BusinessTier.DisposeReader(reader1);

            }


            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                Label lbl = (Label)editedItem.FindControl("lblStaffID");
                RadComboBox cboStaffame = (RadComboBox)editedItem.FindControl("cboStaffame");
                RadComboBox cboDeptId = (RadComboBox)editedItem.FindControl("cboDeptId");
                RadComboBox cboBranch = (RadComboBox)editedItem.FindControl("cboBranch");
                RadComboBox cboCompany = (RadComboBox)editedItem.FindControl("cboCompany");
                RadComboBox cboReporting = (RadComboBox)editedItem.FindControl("cboReporting");
                RadDatePicker DtOGSP = (RadDatePicker)editedItem.FindControl("DtOGSP");
                RadDatePicker dtMedical = (RadDatePicker)editedItem.FindControl("dtMedical");
                RadDatePicker dtH2S = (RadDatePicker)editedItem.FindControl("dtH2S");
                RadDatePicker dtBOSIET = (RadDatePicker)editedItem.FindControl("dtBOSIET");
                RadDatePicker dtCompentancy = (RadDatePicker)editedItem.FindControl("dtCompentancy");
                RadDatePicker dtCIDB = (RadDatePicker)editedItem.FindControl("dtCIDB");
                RadDatePicker dtConfineSpace = (RadDatePicker)editedItem.FindControl("dtConfineSpace");
                RadDatePicker dtWorkPermit = (RadDatePicker)editedItem.FindControl("dtWorkPermit");
                RadDatePicker dtBordingPass = (RadDatePicker)editedItem.FindControl("dtBordingPass");
                RadDatePicker dtPTW = (RadDatePicker)editedItem.FindControl("dtPTW");

                if (!(string.IsNullOrEmpty(lbl.Text.ToString())))
                {
                    string sql = "select * FROM Vw_Staff_Official WHERE Deleted = 0 and StaffOfficial_ID = '" + lbl.Text.ToString() + "'";
                    SqlCommand command = new SqlCommand(sql, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        cboStaffame.SelectedValue = reader["STAFF_ID"].ToString();
                       

                        if (reader["EMPType"].ToString() == "FreeLancer")
                        {
                            cboBranch.Enabled = false;
                            cboDeptId.Enabled = false;
                            cboCompany.Enabled = false;
                            cboReporting.Enabled = false;
                            cboBranch.SelectedValue = "0";
                            cboDeptId.SelectedValue = "0";
                            cboCompany.SelectedValue = "0";
                            cboReporting.SelectedValue = "0";
                        }
                        else
                        {
                            cboBranch.Enabled = true;
                            cboDeptId.Enabled = true;
                            cboCompany.Enabled = true;
                            cboReporting.Enabled = true;
                            cboDeptId.SelectedValue = reader["Dept_ID"].ToString();
                            cboBranch.SelectedValue = reader["COUNTRY_ID"].ToString();
                            cboCompany.SelectedValue = reader["Company_ID"].ToString();
                            cboReporting.SelectedValue = reader["ReportingTo"].ToString();

                            cboDeptId.Text = reader["Dept_Name"].ToString();
                            cboBranch.Text = reader["Location"].ToString();
                            cboCompany.Text = reader["Company_Name"].ToString();
                            cboReporting.Text = reader["ReportingName"].ToString();
                        }
                    }
                    BusinessTier.DisposeReader(reader);

                    DtOGSP.SelectedDate = null; dtMedical.SelectedDate = null; dtH2S.SelectedDate = null; dtBOSIET.SelectedDate = null; dtCompentancy.SelectedDate = null;

                    if (!(DtOGSP.ToolTip == "1/1/1900 12:00:00 AM"))
                    {
                        DtOGSP.SelectedDate = Convert.ToDateTime(DtOGSP.ToolTip.ToString());
                    }
                    if (!(dtMedical.ToolTip == "1/1/1900 12:00:00 AM"))
                    {
                        dtMedical.SelectedDate = Convert.ToDateTime(dtMedical.ToolTip.ToString());
                    }
                    if (!(dtH2S.ToolTip == "1/1/1900 12:00:00 AM"))
                    {
                        dtH2S.SelectedDate = Convert.ToDateTime(dtH2S.ToolTip.ToString());
                    }
                    if (!(dtBOSIET.ToolTip == "1/1/1900 12:00:00 AM"))
                    {
                        dtBOSIET.SelectedDate = Convert.ToDateTime(dtBOSIET.ToolTip.ToString());
                    }
                    if (!(dtCompentancy.ToolTip == "1/1/1900 12:00:00 AM"))
                    {
                        dtCompentancy.SelectedDate = Convert.ToDateTime(dtCompentancy.ToolTip.ToString());
                    }
                    dtCIDB.SelectedDate = null; dtConfineSpace.SelectedDate = null; dtWorkPermit.SelectedDate = null; dtBordingPass.SelectedDate = null; dtPTW.SelectedDate = null;

                    if (!(dtCIDB.ToolTip == "1/1/1900 12:00:00 AM"))
                    {
                        dtCIDB.SelectedDate = Convert.ToDateTime(dtCIDB.ToolTip.ToString());
                    }
                    if (!(dtConfineSpace.ToolTip == ""))
                    {
                        dtConfineSpace.SelectedDate = Convert.ToDateTime(dtConfineSpace.ToolTip.ToString());
                    }
                    if (!(dtWorkPermit.ToolTip == ""))
                    {
                        dtWorkPermit.SelectedDate = Convert.ToDateTime(dtWorkPermit.ToolTip.ToString());
                    }
                    if (!(dtBordingPass.ToolTip == ""))
                    {
                        dtBordingPass.SelectedDate = Convert.ToDateTime(dtBordingPass.ToolTip.ToString());
                    }
                    if (!(dtPTW.ToolTip == ""))
                    {
                        dtPTW.SelectedDate = Convert.ToDateTime(dtPTW.ToolTip.ToString());
                    }
                }
            }
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff_Official", "RadGrid1_ItemDataBound", ex.ToString(), "Audit");
            BusinessTier.DisposeConnection(conn);
        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
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
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff_Official", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "select * FROM Vw_Staff_Official where Deleted = 0";
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
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["StaffOfficial_ID"].ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            int flg = BusinessTier.SaveStaffMasterOffical(conn, Convert.ToInt32(ID.ToString()), 1, "", "", "", "", 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", "", "", "", Session["sesUserID"].ToString(), "D");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(41);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff_Official", "Delete", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(7);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff_Official", "Delete", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            conn.Open();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            RadComboBox cboStaffame = (RadComboBox)editedItem.FindControl("cboStaffame");
            RadDatePicker DtDOJ = (RadDatePicker)editedItem.FindControl("DtDOJ");
            RadComboBox cboEMPtype = (RadComboBox)editedItem.FindControl("cboEMPtype");
            TextBox txtDesignation = (TextBox)editedItem.FindControl("txtDesignation");
            RadComboBox cboBranch = (RadComboBox)editedItem.FindControl("cboBranch");
            RadComboBox cboDeptId = (RadComboBox)editedItem.FindControl("cboDeptId");
            RadComboBox cboCompany = (RadComboBox)editedItem.FindControl("cboCompany");
            RadComboBox cboReporting = (RadComboBox)editedItem.FindControl("cboReporting");

            TextBox txtOGSP = (TextBox)editedItem.FindControl("txtOGSP");
            RadDatePicker DtOGSP = (RadDatePicker)editedItem.FindControl("DtOGSP");
            RadDatePicker dtMedical = (RadDatePicker)editedItem.FindControl("dtMedical");
            RadDatePicker dtH2S = (RadDatePicker)editedItem.FindControl("dtH2S");
            RadDatePicker dtBOSIET = (RadDatePicker)editedItem.FindControl("dtBOSIET");
            RadDatePicker dtCompentancy = (RadDatePicker)editedItem.FindControl("dtCompentancy");

            RadDatePicker dtCIDB = (RadDatePicker)editedItem.FindControl("dtCIDB");
            RadDatePicker dtConfineSpace = (RadDatePicker)editedItem.FindControl("dtConfineSpace");
            RadDatePicker dtWorkPermit = (RadDatePicker)editedItem.FindControl("dtWorkPermit");
            RadDatePicker dtBordingPass = (RadDatePicker)editedItem.FindControl("dtBordingPass");
            RadDatePicker dtPTW = (RadDatePicker)editedItem.FindControl("dtPTW");

            TextBox txtSkill = (TextBox)editedItem.FindControl("txtSkill");
            TextBox txtSHELLPassport = (TextBox)editedItem.FindControl("txtSHELLPassport");

            if (string.IsNullOrEmpty(cboBranch.SelectedValue))
            {
                cboBranch.SelectedValue = "0";
            }
            if (string.IsNullOrEmpty(cboDeptId.SelectedValue))
            {
                cboDeptId.SelectedValue = "0";
            }
            if (string.IsNullOrEmpty(cboCompany.SelectedValue))
            {
                cboCompany.SelectedValue = "0";
            }
            if (string.IsNullOrEmpty(cboReporting.SelectedValue))
            {
                cboReporting.SelectedValue = "0";
            }

            string strCheckflag = "0";
            SqlDataReader reader = BusinessTier.IDCheck(conn, cboStaffame.SelectedValue.ToString().Trim(), "SO");
            if (reader.Read())
            {
                strCheckflag = reader["IDCheck"].ToString();

            }
            BusinessTier.DisposeReader(reader);

            if (strCheckflag.ToString() == "1")
            {
                //ShowMessage(39);
                lblStatus.Text = "Selected Staff Data is already Exist";
                return;
            }

            String DOJ = string.Empty, OGSP = string.Empty, Medical = string.Empty, H2S = string.Empty, BOSIET = string.Empty, Compentancy = string.Empty, SHELL = string.Empty, CIDB = string.Empty, ConfineSpace = string.Empty, WorkPermit = string.Empty, BordingPass = string.Empty, PTW = string.Empty;
            DOJ = BusinessTier.chkdate(Convert.ToString(DtDOJ.SelectedDate));
            OGSP = BusinessTier.chkdate(Convert.ToString(DtOGSP.SelectedDate));
            Medical = BusinessTier.chkdate(Convert.ToString(dtMedical.SelectedDate));
            H2S = BusinessTier.chkdate(Convert.ToString(dtH2S.SelectedDate));
            BOSIET = BusinessTier.chkdate(Convert.ToString(dtBOSIET.SelectedDate));
            Compentancy = BusinessTier.chkdate(Convert.ToString(dtCompentancy.SelectedDate));

            CIDB = BusinessTier.chkdate(Convert.ToString(dtCIDB.SelectedDate));
            ConfineSpace = BusinessTier.chkdate(Convert.ToString(dtConfineSpace.SelectedDate));
            WorkPermit = BusinessTier.chkdate(Convert.ToString(dtWorkPermit.SelectedDate));
            BordingPass = BusinessTier.chkdate(Convert.ToString(dtBordingPass.SelectedDate));
            PTW = BusinessTier.chkdate(Convert.ToString(dtPTW.SelectedDate));

            int flg = BusinessTier.SaveStaffMasterOffical(conn, 1, Convert.ToInt32(cboStaffame.SelectedValue.ToString().Trim()), DOJ.ToString().Trim(), cboEMPtype.Text.ToString().Trim(), txtDesignation.Text.ToString().Trim().ToUpper(), "", Convert.ToInt32(cboBranch.SelectedValue.ToString()), Convert.ToInt32(cboDeptId.SelectedValue.ToString()), Convert.ToInt32(cboCompany.SelectedValue.ToString()), Convert.ToInt32(cboReporting.SelectedValue.ToString()), txtOGSP.Text.ToString(), OGSP.ToString(), Medical.ToString(), H2S.ToString(), BOSIET.ToString(), Compentancy.ToString(), txtSHELLPassport.Text.ToString(), txtSkill.Text.ToString().ToUpper(), CIDB.ToString(), ConfineSpace.ToString(), WorkPermit.ToString(), BordingPass.ToString(), PTW.ToString(), Session["sesUserID"].ToString(), "N");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(40);
            }
            // InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff_Official", "Insert", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            // e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff_Official", "Insert", ex.ToString(), "Audit");
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
            RadComboBox cboStaffame = (RadComboBox)editedItem.FindControl("cboStaffame");
            RadDatePicker DtDOJ = (RadDatePicker)editedItem.FindControl("DtDOJ");
            RadComboBox cboEMPtype = (RadComboBox)editedItem.FindControl("cboEMPtype");
            TextBox txtDesignation = (TextBox)editedItem.FindControl("txtDesignation");
            RadComboBox cboBranch = (RadComboBox)editedItem.FindControl("cboBranch");
            RadComboBox cboDeptId = (RadComboBox)editedItem.FindControl("cboDeptId");
            RadComboBox cboCompany = (RadComboBox)editedItem.FindControl("cboCompany");
            RadComboBox cboReporting = (RadComboBox)editedItem.FindControl("cboReporting");
            TextBox txtOGSP = (TextBox)editedItem.FindControl("txtOGSP");
            RadDatePicker DtOGSP = (RadDatePicker)editedItem.FindControl("DtOGSP");
            RadDatePicker dtMedical = (RadDatePicker)editedItem.FindControl("dtMedical");
            RadDatePicker dtH2S = (RadDatePicker)editedItem.FindControl("dtH2S");
            RadDatePicker dtBOSIET = (RadDatePicker)editedItem.FindControl("dtBOSIET");
            RadDatePicker dtCompentancy = (RadDatePicker)editedItem.FindControl("dtCompentancy");
            RadDatePicker dtCIDB = (RadDatePicker)editedItem.FindControl("dtCIDB");
            RadDatePicker dtConfineSpace = (RadDatePicker)editedItem.FindControl("dtConfineSpace");
            RadDatePicker dtWorkPermit = (RadDatePicker)editedItem.FindControl("dtWorkPermit");
            RadDatePicker dtBordingPass = (RadDatePicker)editedItem.FindControl("dtBordingPass");
            RadDatePicker dtPTW = (RadDatePicker)editedItem.FindControl("dtPTW");
            TextBox txtSkill = (TextBox)editedItem.FindControl("txtSkill");
            TextBox txtSHELLPassport = (TextBox)editedItem.FindControl("txtSHELLPassport");

            if (string.IsNullOrEmpty(cboBranch.SelectedValue))
            {
                cboBranch.SelectedValue = "0";
            }
            if (string.IsNullOrEmpty(cboDeptId.SelectedValue))
            {
                cboDeptId.SelectedValue = "0";
            }
            if (string.IsNullOrEmpty(cboCompany.SelectedValue))
            {
                cboCompany.SelectedValue = "0";
            }
            if (string.IsNullOrEmpty(cboReporting.SelectedValue))
            {
                cboReporting.SelectedValue = "0";
            }
            String DOJ = string.Empty, OGSP = string.Empty, Medical = string.Empty, H2S = string.Empty, BOSIET = string.Empty, Compentancy = string.Empty, SHELL = string.Empty, CIDB = string.Empty, ConfineSpace = string.Empty, WorkPermit = string.Empty, BordingPass = string.Empty, PTW = string.Empty;
            DOJ = BusinessTier.chkdate(Convert.ToString(DtDOJ.SelectedDate));
            OGSP = BusinessTier.chkdate(Convert.ToString(DtOGSP.SelectedDate));
            Medical = BusinessTier.chkdate(Convert.ToString(dtMedical.SelectedDate));
            H2S = BusinessTier.chkdate(Convert.ToString(dtH2S.SelectedDate));
            BOSIET = BusinessTier.chkdate(Convert.ToString(dtBOSIET.SelectedDate));
            Compentancy = BusinessTier.chkdate(Convert.ToString(dtCompentancy.SelectedDate));

            CIDB = BusinessTier.chkdate(Convert.ToString(dtCIDB.SelectedDate));
            ConfineSpace = BusinessTier.chkdate(Convert.ToString(dtConfineSpace.SelectedDate));
            WorkPermit = BusinessTier.chkdate(Convert.ToString(dtWorkPermit.SelectedDate));
            BordingPass = BusinessTier.chkdate(Convert.ToString(dtBordingPass.SelectedDate));
            PTW = BusinessTier.chkdate(Convert.ToString(dtPTW.SelectedDate));

            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["StaffOfficial_ID"].ToString();
            //int flg = BusinessTier.SaveStaffMasterOffical(conn, Convert.ToInt32(ID.ToString()), Convert.ToInt32(cboStaffame.SelectedValue.ToString().Trim()), DOJ.ToString().Trim(), cboEMPtype.Text.ToString().Trim(), txtDesignation.Text.ToString().Trim(), "", Convert.ToInt32(cboBranch.SelectedValue.ToString()), Convert.ToInt32(cboDeptId.SelectedValue.ToString()), Convert.ToInt32(cboCompany.SelectedValue.ToString()), Convert.ToInt32(cboReporting.SelectedValue.ToString()), txtOGSP.Text.ToString(), OGSP.ToString(), Medical.ToString(), H2S.ToString(), BOSIET.ToString(), Compentancy.ToString(), SHELL.ToString(), txtSkill.Text.ToString(), Session["sesUserID"].ToString(), "U");
            int flg = BusinessTier.SaveStaffMasterOffical(conn, Convert.ToInt32(ID.ToString()), Convert.ToInt32(cboStaffame.SelectedValue.ToString().Trim()), DOJ.ToString().Trim(), cboEMPtype.Text.ToString().Trim(), txtDesignation.Text.ToString().Trim().ToUpper(), "", Convert.ToInt32(cboBranch.SelectedValue.ToString()), Convert.ToInt32(cboDeptId.SelectedValue.ToString()), Convert.ToInt32(cboCompany.SelectedValue.ToString()), Convert.ToInt32(cboReporting.SelectedValue.ToString()), txtOGSP.Text.ToString(), OGSP.ToString(), Medical.ToString(), H2S.ToString(), BOSIET.ToString(), Compentancy.ToString(), txtSHELLPassport.Text.ToString(), txtSkill.Text.ToString().ToUpper(), CIDB.ToString(), ConfineSpace.ToString(), WorkPermit.ToString(), BordingPass.ToString(), PTW.ToString(), Session["sesUserID"].ToString(), "U");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                ShowMessage(42);
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff_Official", "Update", "Success", "Log");
        }
        catch (Exception ex)
        {
            ShowMessage(5);
            e.Canceled = true;
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Staff_Official", "Update", ex.ToString(), "Audit");
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

    protected void Onclick_btnExport(object sender, EventArgs e)
    {
        //RadGrid1.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
        //RadGrid1.ExportSettings.IgnorePaging = CheckBox1.Checked;
        //RadGrid1.ExportSettings.ExportOnlyData = true;
        //RadGrid1.ExportSettings.OpenInNewWindow = true;
        //RadGrid1.MasterTableView.ExportToExcel();

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

    public void colortxt(string thethi, Label txt)
    {
        DateTime dt ;
        if (!(string.IsNullOrEmpty(thethi.ToString())))
        {
            dt = Convert.ToDateTime(thethi.ToString());
            if (dt <= DateTime.Now)
            {
                if (dt.ToString() == "1/1/1900 12:00:00 AM")
                {
                    txt.Text = string.Empty;
                }
                else
                {
                    txt.Text = String.Format("{0:dd/MM/yyyy}", dt);
                    txt.ForeColor = Color.Red;
                }
            }
            else
            {
                txt.Text = String.Format("{0:dd/MM/yyyy}", dt);
            }
        }
    }
}