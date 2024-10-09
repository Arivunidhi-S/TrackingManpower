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

public partial class Report : System.Web.UI.Page
{

    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
            {
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
            lblProject.Visible = false;
            cboProject.Visible = false;
            lblBranch.Visible = false;
            cboBrach.Visible = false;
            //cboOrderID.Visible = false;
            lblOrderid.Visible = false;
            //dtStartDate.Visible = false;
            //dtEndDate.Visible = false;
            lblDateStart.Visible = false;
            lblDateEnd.Visible = false;
            StiWebViewer1.Visible = false;
            //cboMatreialNo.Visible = false;
            //ChkCC.Visible = false;
            //btnEmail.Visible = false;
            lblmail.Text = "To Mail : ";
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

    protected void cboProject_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            sql1 = " select Project_Name,STAFF_NAME,Project_ID,Project_No from Vw_Master_Project where DELETED=0 and [Project_Name] LIKE '%' + @text + '%'  order by Project_Name";
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
            //InsertLogAuditTrail(Session["sesUserID"].ToString(), "Invoice", "cboProject_ItemrequestedChanged", ex.ToString(), "Audit");
            //ShowMessage("Please Select the Installation Name", "Red");
        }

    }

    protected void cboBranch_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        try
        {

            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            string sql1 = "";
            sql1 = "select distinct(Location) as branch from Vw_AssignStaff where Deleted=0 and [Location] LIKE '%' + @text + '%'  order by location";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            adapter1.SelectCommand.Parameters.AddWithValue("@Text", e.Text);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            RadComboBox comboBox = (RadComboBox)sender;
            comboBox.Items.Clear();

            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["branch"].ToString().Trim();
                //item.Value = row["Project_ID"].ToString().Trim();
                //item.Attributes.Add("Project_Name", row["Project_Name"].ToString().Trim());
                //item.Attributes.Add("Project_No", row["Project_No"].ToString().Trim());
                //item.Attributes.Add("STAFF_NAME", row["STAFF_NAME"].ToString().Trim());

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

    //protected void cboOrderID_Select_Change(object sender, EventArgs e)
    //{
    //    StiWebViewer1.Visible = false;
    //    lblStatus.Text = "";
    //    if (cboReports.SelectedItem.Text == "OrderForm" || cboReports.SelectedItem.Text == "Final Inspection" || cboReports.SelectedItem.Text == "Warranty Sticker" || cboReports.SelectedItem.Text == "Spring Details" || cboReports.SelectedItem.Text == "Sales Report")
    //    {
    //        dtStartDate.Clear();
    //        dtEndDate.Clear();
    //    }
    //}

    //protected void dtStartDate_Select_Change(object sender, EventArgs e)
    //{
    //    StiWebViewer1.Visible = false;
    //    lblStatus.Text = "";
    //    if (cboReports.SelectedItem.Text == "OrderForm" || cboReports.SelectedItem.Text == "Final Inspection" || cboReports.SelectedItem.Text == "Warranty Sticker" || cboReports.SelectedItem.Text == "Spring Details" || cboReports.SelectedItem.Text == "Sales Report")
    //    {
    //        cboOrderID.ClearSelection();
    //        cboOrderID.Text = string.Empty;
    //    }
    //}

    protected void cboReports_Select_Change(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        //cboOrderID.ClearSelection();
        //cboOrderID.Text = string.Empty;
        //cboMatreialNo.ClearSelection();
        //cboMatreialNo.Text = string.Empty;
        //dtStartDate.Clear();
        //dtEndDate.Clear();
        //SqlConnection conn = BusinessTier.getConnection();
        //conn.Open();
        StiWebViewer1.Visible = false;
        if (cboReports.SelectedItem.Text == "Manpower List")
        {
            cboProject.Visible = true;
            lblProject.Visible = true;
            lblmail.Text = "CC Mail : ";
            lblBranch.Visible = false;
            cboBrach.Visible = false;
            //ChkCC.Visible = true;
            //btnEmail.Visible = true;

        }
        else if (cboReports.SelectedItem.Text == "Project List")
        {
            cboProject.Visible = false;
            lblProject.Visible = false;
            lblmail.Text = "To Mail : ";
            lblBranch.Visible = false;
            cboBrach.Visible = false;
            //ChkCC.Visible = false;
            //btnEmail.Visible = false;
        }
        else if (cboReports.SelectedItem.Text == "Staff List")
        {
            cboProject.Visible = false;
            lblProject.Visible = false;
            lblmail.Text = "To Mail : ";
            lblBranch.Visible = true;
            cboBrach.Visible = true;
            //ChkCC.Visible = false;
            //btnEmail.Visible = false;
        }

        //if (cboReports.SelectedItem.Text == "Customer Details")
        //{
        //    cboOrderID.Visible = false;
        //    lblOrderid.Visible = false;
        //    dtStartDate.Visible = false;
        //    dtEndDate.Visible = false;
        //    lblDateStart.Visible = false;
        //    lblDateEnd.Visible = false;
        //    cboMatreialNo.Visible = false;
        //    lblMaterialNo.Visible = false;
        //}
        //if (cboReports.SelectedItem.Text == "Material Details")
        //{
        //    cboOrderID.Visible = false;
        //    lblOrderid.Visible = false;
        //    dtStartDate.Visible = false;
        //    dtEndDate.Visible = false;
        //    lblDateStart.Visible = false;
        //    lblDateEnd.Visible = false;
        //    cboMatreialNo.Visible = false;
        //    lblMaterialNo.Visible = false;

        //}

        //if (cboReports.SelectedItem.Text == "Staff Details")
        //{
        //    cboOrderID.Visible = false;
        //    lblOrderid.Visible = false;
        //    dtStartDate.Visible = false;
        //    dtEndDate.Visible = false;
        //    lblDateStart.Visible = false;
        //    lblDateEnd.Visible = false;
        //    cboMatreialNo.Visible = false;
        //    lblMaterialNo.Visible = false;
        //}

        //if (cboReports.SelectedItem.Text == "Incoming Material")
        //{
        //    lblOrderid.Visible = false;
        //    cboOrderID.Visible = false;
        //    lblMaterialNo.Visible = true;
        //    cboMatreialNo.Visible = true;
        //    dtStartDate.Visible = true;
        //    dtEndDate.Visible = true;
        //    lblDateStart.Visible = true;
        //    lblDateEnd.Visible = true;

        //    string sql1 = "select distinct(Materialcode),MaterialName FROM IncomingMaterial WHERE Deleted=0";
        //    SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
        //    DataTable dataTable1 = new DataTable();
        //    adapter1.Fill(dataTable1);
        //    cboMatreialNo.Items.Clear();
        //    foreach (DataRow row in dataTable1.Rows)
        //    {
        //        RadComboBoxItem item = new RadComboBoxItem();
        //        item.Text = row["Materialcode"].ToString();
        //        item.Attributes.Add("Materialcode", row["Materialcode"].ToString());
        //        item.Attributes.Add("MaterialName", row["MaterialName"].ToString());

        //        cboMatreialNo.Items.Add(item);
        //        item.DataBind();
        //    }

        //}
        //if (cboReports.SelectedItem.Text == "Warranty Sticker" || cboReports.SelectedItem.Text == "Spring Details")
        //{
        //    lblMaterialNo.Visible = false;
        //    cboMatreialNo.Visible = false;
        //    cboOrderID.Visible = true;
        //    lblOrderid.Visible = true;
        //    dtStartDate.Visible = true;
        //    dtEndDate.Visible = true;
        //    lblDateStart.Visible = true;
        //    lblDateEnd.Visible = true;
        //    cboOrderID.Text = string.Empty;
        //    cboOrderID.Items.Clear();
        //    string sql2 = "select * from OrderForm where Deleted=0 ORDER BY [OrderAutoID] desc";
        //    SqlCommand cmd2 = new SqlCommand(sql2, conn);
        //    SqlDataReader rd2 = cmd2.ExecuteReader();
        //    while (rd2.Read())
        //    {
        //        RadComboBoxItem item1 = new RadComboBoxItem();
        //        item1.Text = rd2["orderno"].ToString();
        //        cboOrderID.Items.Add(item1);
        //    }
        //    BusinessTier.DisposeReader(rd2);
        //}
        //BusinessTier.DisposeConnection(conn);
    }

    //protected void btn_Report_Submit_Click(object sender, EventArgs e)
    //{

    //    Stimulsoft.Report.StiReport stiReport1;
    //    string path = string.Empty;
    //    lblStatus.Text = string.Empty;
    //    if ((string.IsNullOrEmpty(cboReports.Text.ToString())))
    //    {
    //        lblStatus.Text = "Select Reports";
    //        return;
    //    }

    //    string con = BusinessTier.getConnection1();
    //    SqlConnection conn = BusinessTier.getConnection();
    //    conn.Open();

    //    try
    //    {

    //        string sqldatasource4 = string.Empty;
    //        DataSet ds4 = new DataSet();
    //        string sqldatasource5 = string.Empty;
    //        DataSet ds5 = new DataSet();

    //        if (cboReports.SelectedItem.Text == "OrderForm")
    //        {

    //            sqldatasource4 = "OrderForm";
    //            string sql4 = string.Empty;
    //            if (!(string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (!(string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))))
    //            {
    //                if (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString()))
    //                {
    //                    lblStatus.Text = "Select Start Date";
    //                    return;
    //                }
    //                if (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))
    //                {
    //                    lblStatus.Text = "Select End Date";
    //                    return;
    //                }

    //                String stdt = dtStartDate.SelectedDate.ToString();
    //                DateTime pdt = DateTime.Parse(stdt);
    //                stdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

    //                String enddt = dtEndDate.SelectedDate.ToString();
    //                DateTime idt = DateTime.Parse(enddt);
    //                enddt = idt.Month + "/" + idt.Day + "/" + idt.Year;

    //                sql4 = "select *,CONVERT(VARCHAR(10), Date, 103) AS [Todate],CONVERT(VARCHAR(10), ShippingDate, 103) AS [InstallDate] from OrderForm where Deleted=0 and Date between '" + stdt.ToString() + "' and '" + enddt.ToString() + "'";
    //            }

    //            else if (!(string.IsNullOrEmpty(cboOrderID.Text.ToString())) || (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString())))
    //            {
    //                sql4 = "select *,CONVERT(VARCHAR(10), Date, 103) AS [Todate],CONVERT(VARCHAR(10), ShippingDate, 103) AS [InstallDate] from OrderForm where deleted=0 and orderno='" + cboOrderID.SelectedItem.Text.ToString().Trim() + "' and  Deleted=0";
    //            }
    //            else
    //            {
    //                sql4 = "select *,CONVERT(VARCHAR(10), Date, 103) AS [Todate],CONVERT(VARCHAR(10), ShippingDate, 103) AS [InstallDate] from OrderForm where deleted=0";
    //            }


    //            SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

    //            ds4.DataSetName = "DynamicDataSource1";
    //            ds4.Tables.Add(sqldatasource4);
    //            ad4.Fill(ds4, sqldatasource4);
    //            path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\OrderForm.mrt";

    //        }

    //        if (cboReports.SelectedItem.Text == "Final Inspection")
    //        {

    //            sqldatasource4 = "VW_FinalInspection";
    //            string sql4 = string.Empty;
    //            if (!(string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (!(string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))))
    //            {
    //                if (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString()))
    //                {
    //                    lblStatus.Text = "Select Start Date";
    //                    return;
    //                }
    //                if (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))
    //                {
    //                    lblStatus.Text = "Select End Date";
    //                    return;
    //                }

    //                String stdt = dtStartDate.SelectedDate.ToString();
    //                DateTime pdt = DateTime.Parse(stdt);
    //                stdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

    //                String enddt = dtEndDate.SelectedDate.ToString();
    //                DateTime idt = DateTime.Parse(enddt);
    //                enddt = idt.Month + "/" + idt.Day + "/" + idt.Year;

    //                sql4 = "select *,CONVERT(VARCHAR(10), ShippingDate, 103) as InstallDate from VW_FinalInspection where Deleted=0 and Date between '" + stdt.ToString() + "' and '" + enddt.ToString() + "'";
    //            }

    //            else if (!(string.IsNullOrEmpty(cboOrderID.Text.ToString())) || (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString())))
    //            {
    //                sql4 = "select *,CONVERT(VARCHAR(10), ShippingDate, 103) as InstallDate from VW_FinalInspection where deleted=0 and orderno='" + cboOrderID.SelectedItem.Text.ToString().Trim() + "' and  Deleted=0";
    //            }
    //            else
    //            {
    //                sql4 = "select *,CONVERT(VARCHAR(10), ShippingDate, 103) as InstallDate from VW_FinalInspection where deleted=0";
    //            }

    //            SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

    //            ds4.DataSetName = "DynamicDataSource1";
    //            ds4.Tables.Add(sqldatasource4);
    //            ad4.Fill(ds4, sqldatasource4);
    //            path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Final Inspection.mrt";

    //        }
    //        if (cboReports.SelectedItem.Text == "Customer Details")
    //        {

    //            cboOrderID.Visible = false;
    //            lblOrderid.Visible = false;
    //            sqldatasource4 = "Customer";
    //            string sql4 = "select * from Customer where Deleted=0";
    //            SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

    //            ds4.DataSetName = "DynamicDataSource1";
    //            ds4.Tables.Add(sqldatasource4);
    //            ad4.Fill(ds4, sqldatasource4);
    //            path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Customer.mrt";
    //        }

    //        if (cboReports.SelectedItem.Text == "Material Details")
    //        {

    //            sqldatasource4 = "MaterialMaster";
    //            string sql4 = "select *,CONVERT(VARCHAR(10), MnfDate, 103) as ShippingDate from MaterialMaster where Deleted=0";
    //            SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

    //            ds4.DataSetName = "DynamicDataSource1";
    //            ds4.Tables.Add(sqldatasource4);
    //            ad4.Fill(ds4, sqldatasource4);
    //            path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Material.mrt";
    //        }

    //        if (cboReports.SelectedItem.Text == "Incoming Material")
    //        {

    //            sqldatasource4 = "IncomingMaterial";
    //            string sql4 = string.Empty;
    //            if (string.IsNullOrEmpty(cboMatreialNo.Text.ToString()))
    //            {
    //                sql4 = "select *,CONVERT(VARCHAR(10), StockDate, 103) as ShippingDate from IncomingMaterial where Deleted=0";
    //            }
    //            else if (!(string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (!(string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))))
    //            {

    //                String stdt = dtStartDate.SelectedDate.ToString();
    //                DateTime pdt = DateTime.Parse(stdt);
    //                stdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

    //                String enddt = dtEndDate.SelectedDate.ToString();
    //                DateTime idt = DateTime.Parse(enddt);
    //                enddt = idt.Month + "/" + idt.Day + "/" + idt.Year;

    //                sql4 = "select *,CONVERT(VARCHAR(10), StockDate, 103) as ShippingDate from IncomingMaterial where Deleted=0 and Materialcode='" + cboMatreialNo.Text.ToString() + "' and StockDate between '" + stdt.ToString() + "' and '" + enddt.ToString() + "'";
    //            }

    //            else if (!(string.IsNullOrEmpty(cboMatreialNo.Text.ToString())) || (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString())))
    //            {
    //                sql4 = "select *,CONVERT(VARCHAR(10), StockDate, 103) as ShippingDate from IncomingMaterial where Deleted=0 and Materialcode='" + cboMatreialNo.Text.ToString() + "'";
    //            }

    //            SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

    //            ds4.DataSetName = "DynamicDataSource1";
    //            ds4.Tables.Add(sqldatasource4);
    //            ad4.Fill(ds4, sqldatasource4);
    //            path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Incoming Material.mrt";
    //        }

    //        if (cboReports.SelectedItem.Text == "Staff Details")
    //        {

    //            sqldatasource4 = "MasterStaff";
    //            string sql4 = "select * from MasterStaff where Deleted=0";
    //            SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

    //            ds4.DataSetName = "DynamicDataSource1";
    //            ds4.Tables.Add(sqldatasource4);
    //            ad4.Fill(ds4, sqldatasource4);
    //            path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Staff.mrt";
    //        }

    //        if (cboReports.SelectedItem.Text == "Warranty Sticker" || cboReports.SelectedItem.Text == "Spring Details")
    //        {

    //            string sql4 = string.Empty;
    //            sqldatasource4 = "OrderForm";
    //            if (!(string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (!(string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))))
    //            {
    //                if (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString()))
    //                {
    //                    lblStatus.Text = "Select Start Date";
    //                    return;
    //                }
    //                if (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))
    //                {
    //                    lblStatus.Text = "Select End Date";
    //                    return;
    //                }

    //                String stdt = dtStartDate.SelectedDate.ToString();
    //                DateTime pdt = DateTime.Parse(stdt);
    //                stdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

    //                String enddt = dtEndDate.SelectedDate.ToString();
    //                DateTime idt = DateTime.Parse(enddt);
    //                enddt = idt.Month + "/" + idt.Day + "/" + idt.Year;

    //                sql4 = "select *,CONVERT(VARCHAR(10), ShippingDate, 103) as InstallDate from OrderForm where Deleted=0 and Date between '" + stdt.ToString() + "' and '" + enddt.ToString() + "'";
    //            }

    //            else if (!(string.IsNullOrEmpty(cboOrderID.Text.ToString())) || (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString())))
    //            {
    //                sql4 = "select *,CONVERT(VARCHAR(10), ShippingDate, 103) as InstallDate from OrderForm where deleted=0 and orderno='" + cboOrderID.SelectedItem.Text.ToString().Trim() + "' and  Deleted=0";
    //            }
    //            else
    //            {
    //                sql4 = "select *,CONVERT(VARCHAR(10), ShippingDate, 103) as InstallDate from OrderForm where deleted=0";
    //            }



    //            SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

    //            ds4.DataSetName = "DynamicDataSource1";
    //            ds4.Tables.Add(sqldatasource4);
    //            ad4.Fill(ds4, sqldatasource4);
    //            if (cboReports.SelectedItem.Text == "Warranty Sticker")
    //            {
    //                path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Warranty.mrt";
    //            }
    //            else
    //            {
    //                path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\SpringDetails.mrt";
    //            }
    //        }

    //        if (cboReports.SelectedItem.Text == "Sales Report")
    //        {

    //            string sql4 = string.Empty, sql5 = string.Empty;
    //            sqldatasource4 = "OrderForm";
    //            sqldatasource5 = "DataSource1";
    //            if (!(string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (!(string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))))
    //            {
    //                if (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString()))
    //                {
    //                    lblStatus.Text = "Select Start Date";
    //                    return;
    //                }
    //                if (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))
    //                {
    //                    lblStatus.Text = "Select End Date";
    //                    return;
    //                }

    //                String stdt = dtStartDate.SelectedDate.ToString();
    //                DateTime pdt = DateTime.Parse(stdt);
    //                stdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

    //                String enddt = dtEndDate.SelectedDate.ToString();
    //                DateTime idt = DateTime.Parse(enddt);
    //                enddt = idt.Month + "/" + idt.Day + "/" + idt.Year;

    //                sql4 = "select * from OrderForm where Deleted=0 and Date between '" + stdt.ToString() + "' and '" + enddt.ToString() + "'";
    //                sql5 = "SELECT SUM(CAST(unitqty as int)) AS Qty,SUM(CAST([TotalAmt] as int)) AS TotalAmount FROM [SJClassic].[dbo].[OrderForm] where deleted=0 and Date between '" + stdt.ToString() + "' and '" + enddt.ToString() + "'";
    //            }

    //            else if (!(string.IsNullOrEmpty(cboOrderID.Text.ToString())) || (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString())))
    //            {
    //                sql4 = "select * from OrderForm where deleted=0 and orderno='" + cboOrderID.SelectedItem.Text.ToString().Trim() + "' and  Deleted=0";
    //                sql5 = "SELECT SUM(CAST(unitqty as int)) AS Qty,SUM(CAST([TotalAmt] as int)) AS TotalAmount FROM [SJClassic].[dbo].[OrderForm] where deleted=0 and orderno='" + cboOrderID.SelectedItem.Text.ToString().Trim() + "' and  Deleted=0";
    //            }
    //            else
    //            {
    //                sql4 = "select * from OrderForm where deleted=0";
    //                sql5 = "SELECT SUM(CAST(unitqty as int)) AS Qty,SUM(CAST([TotalAmt] as int)) AS TotalAmount FROM [SJClassic].[dbo].[OrderForm] where deleted=0";
    //            }


    //            SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);
    //            SqlDataAdapter ad5 = new SqlDataAdapter(sql5, con);

    //            ds4.DataSetName = "DynamicDataSource1";
    //            ds5.DataSetName = "DynamicDataSource1";

    //            ds4.Tables.Add(sqldatasource4);
    //            ds5.Tables.Add(sqldatasource5);

    //            ad4.Fill(ds4, sqldatasource4);
    //            ad5.Fill(ds5, sqldatasource5);

    //            path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Sales Report.mrt";

    //        }
    //        stiReport1 = new StiReport();
    //        stiReport1.Dictionary.DataStore.Clear();
    //        stiReport1.ClearAllStates();
    //        stiReport1.Load(path);
    //        stiReport1.Dictionary.Databases.Clear();
    //        stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
    //        stiReport1.Dictionary.DataSources.Clear();

    //        stiReport1.RegData(sqldatasource4, ds4);
    //        if (cboReports.SelectedItem.Text == "Sales Report")
    //        {
    //            stiReport1.RegData(sqldatasource5, ds5);
    //        }
    //        stiReport1.Dictionary.Synchronize();
    //        stiReport1.Compile();
    //        stiReport1.Render();
    //        StiWebViewer1.Report = stiReport1;
    //        StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
    //        //stiReport1.Print();
    //        stiReport1.Dispose();
    //        StiWebViewer1.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        lblStatus.Text = ex.Message;
    //        StiWebViewer1.Visible = false;
    //    }
    //}

    protected void Onclick_btnSubmit(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        try
        {
            if (cboReports.SelectedItem.Text == "Manpower List")
            {
                string con = BusinessTier.getConnection1();
                StiWebViewer1.Visible = true;
                string sqldatasource1 = string.Empty, sql1 = string.Empty, path = string.Empty, sql2 = string.Empty, sql3 = string.Empty, sql4 = string.Empty, sql5 = string.Empty, sqldatasource2 = string.Empty, sqldatasource3 = string.Empty, sqldatasource4 = string.Empty, sqldatasource5 = string.Empty;
                DataSet ds1 = new DataSet();
                sqldatasource1 = "Vw_AssignStaff";
                sql1 = "select * from Vw_AssignStaff where Project_ID='" + cboProject.SelectedValue.ToString().Trim() + "' and Deleted=0";
                SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);
                ds1.DataSetName = "DynamicDataSource1";
                ds1.Tables.Add(sqldatasource1);
                ad1.Fill(ds1, sqldatasource1);

                DataSet ds2 = new DataSet();
                sqldatasource2 = "Vw_FreeLancer";
                sql2 = "select * from Vw_FreeLancer where Project_ID='" + cboProject.SelectedValue.ToString().Trim() + "' and Deleted=0";
                SqlDataAdapter ad2 = new SqlDataAdapter(sql2, con);
                ds2.DataSetName = "DynamicDataSource1";
                ds2.Tables.Add(sqldatasource2);
                ad2.Fill(ds2, sqldatasource2);

                DataSet ds3 = new DataSet();
                sqldatasource3 = "Master_Project_Contact";
                sql3 = "select sum(manpower)as RequestManpower FROM Master_Project_Contact where Project_ID='" + cboProject.SelectedValue.ToString().Trim() + "' and Deleted=0";
                SqlDataAdapter ad3 = new SqlDataAdapter(sql3, con);
                ds3.DataSetName = "DynamicDataSource1";
                ds3.Tables.Add(sqldatasource3);
                ad3.Fill(ds3, sqldatasource3);

                DataSet ds4 = new DataSet();
                sqldatasource4 = "AssignStaff";
                sql4 = "select count(*) as Manpower1  from AssignStaff where Project_ID='" + cboProject.SelectedValue.ToString().Trim() + "' and Deleted=0";
                SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);
                ds4.DataSetName = "DynamicDataSource1";
                ds4.Tables.Add(sqldatasource4);
                ad4.Fill(ds4, sqldatasource4);

                DataSet ds5 = new DataSet();
                sqldatasource5 = "FreeLancer";
                sql5 = "select count(*) as Manpower2 from FreeLancer where Project_ID='" + cboProject.SelectedValue.ToString().Trim() + "' and Deleted=0";
                SqlDataAdapter ad5 = new SqlDataAdapter(sql5, con);
                ds5.DataSetName = "DynamicDataSource1";
                ds5.Tables.Add(sqldatasource5);
                ad5.Fill(ds5, sqldatasource5);

                path = "C:\\inetpub\\wwwroot\\TrackingManpower\\Reports\\LIST OF MANPOWER - Copy.mrt";
                Stimulsoft.Report.StiReport stiReport1;
                stiReport1 = new StiReport();
                stiReport1.Dictionary.DataStore.Clear();
                stiReport1.ClearAllStates();
                stiReport1.Load(path);
                stiReport1.Dictionary.Databases.Clear();
                stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
                stiReport1.Dictionary.DataSources.Clear();

                stiReport1.RegData(sqldatasource1, ds1);
                stiReport1.RegData(sqldatasource2, ds2);
                stiReport1.RegData(sqldatasource3, ds3);
                stiReport1.RegData(sqldatasource4, ds4);
                stiReport1.RegData(sqldatasource5, ds5);

                stiReport1.Dictionary.Synchronize();
                stiReport1.Compile();
                // stiReport1.Render();
                StiWebViewer1.Report = stiReport1;
                StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
                stiReport1.Dispose();
            }
            else if (cboReports.SelectedItem.Text == "Project List")
            {
                string con = BusinessTier.getConnection1();
                StiWebViewer1.Visible = true;
                string sqldatasource1 = string.Empty, sql1 = string.Empty, path = string.Empty, sql2 = string.Empty, sqldatasource2 = string.Empty;
                DataSet ds1 = new DataSet();
                sqldatasource1 = "Vw_Master_Project";
                sql1 = "select *,upper(Project_Name) as p_name,upper(CUSTOMER_NAME) as c_name from Vw_Master_Project where Deleted=0";
                SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);
                ds1.DataSetName = "DynamicDataSource1";
                ds1.Tables.Add(sqldatasource1);
                ad1.Fill(ds1, sqldatasource1);


                path = "C:\\inetpub\\wwwroot\\TrackingManpower\\Reports\\Project List.mrt";
                Stimulsoft.Report.StiReport stiReport1;
                stiReport1 = new StiReport();
                stiReport1.Dictionary.DataStore.Clear();
                stiReport1.ClearAllStates();
                stiReport1.Load(path);
                stiReport1.Dictionary.Databases.Clear();
                stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
                stiReport1.Dictionary.DataSources.Clear();

                stiReport1.RegData(sqldatasource1, ds1);


                stiReport1.Dictionary.Synchronize();
                stiReport1.Compile();
                // stiReport1.Render();
                StiWebViewer1.Report = stiReport1;
                StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
                stiReport1.Dispose();
            }
            else if (cboReports.SelectedItem.Text == "Staff List")
            {
                string con = BusinessTier.getConnection1();
                StiWebViewer1.Visible = true;
                string sqldatasource1 = string.Empty, sql1 = string.Empty, path = string.Empty, sql2 = string.Empty, sqldatasource2 = string.Empty;
                DataSet ds1 = new DataSet();
                sqldatasource1 = "Vw_AssignStaff";
                if (cboBrach.Text == "")
                {
                    sql1 = "select * from Vw_AssignStaff where Deleted=0 order by location";
                }
                else
                {
                    sql1 = "select * from Vw_AssignStaff where Deleted=0 and location='" + cboBrach.Text.ToString().Trim() + "' order by location";
                }
                SqlDataAdapter ad1 = new SqlDataAdapter(sql1, con);
                ds1.DataSetName = "DynamicDataSource1";
                ds1.Tables.Add(sqldatasource1);
                ad1.Fill(ds1, sqldatasource1);


                path = "C:\\inetpub\\wwwroot\\TrackingManpower\\Reports\\Staff.mrt";
                Stimulsoft.Report.StiReport stiReport1;
                stiReport1 = new StiReport();
                stiReport1.Dictionary.DataStore.Clear();
                stiReport1.ClearAllStates();
                stiReport1.Load(path);
                stiReport1.Dictionary.Databases.Clear();
                stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
                stiReport1.Dictionary.DataSources.Clear();

                stiReport1.RegData(sqldatasource1, ds1);


                stiReport1.Dictionary.Synchronize();
                stiReport1.Compile();
                // stiReport1.Render();
                StiWebViewer1.Report = stiReport1;
                StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
                stiReport1.Dispose();
            }
        }
        catch (Exception ex)
        {

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Report", "Onclick_btnSubmit", ex.ToString(), "Audit");
            return;
        }
    }

    //protected void btnEmail_Click(object sender, EventArgs e)
    //{
    //    System.IO.MemoryStream stream = null;
    //    SmtpClient smtp = new SmtpClient();
    //    MailMessage item = new MailMessage();

    //    Stimulsoft.Report.StiReport report = default(Stimulsoft.Report.StiReport);
    //    try
    //    {
    //        //Get Report
    //        report = StiWebViewer1.Report;
    //        //Create Mail Message

    //        MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["FromAddress"].ToString(), "PMT");
    //        item.From = fromAddress;


    //        item.Subject = "Serba's Tracking Manpower - Staff Listing";

    //        item.Body = "Please Find herewith Enclosed the Project Staff List. " + "\r\n" + "\r\n" + "  " + "\r\n" + "  " + "\r\n" + "\r\n" + "\r\n" + "Note: " + "\r\n" + "*. Do not reply to this mail" + "\r\n" + "*. This is system generated mail " + "\r\n";

    //        string strCCMail = "";
    //        SqlConnection conn = BusinessTier.getConnection();
    //        conn.Open();
    //        string sql2 = "select EmailList FROM PMT_EmailList WHERE AutoId=1";
    //        SqlCommand command2 = new SqlCommand(sql2, conn);
    //        SqlDataReader reader2 = command2.ExecuteReader();
    //        if (reader2.Read())
    //        {
    //            strCCMail = reader2["EmailList"].ToString();

    //        }

    //        BusinessTier.DisposeReader(reader2);
    //        BusinessTier.DisposeConnection(conn);
    //        //  string strCCMail = BusinessTier.getCCMailID("SIRIM");
    //        if (string.IsNullOrEmpty(strCCMail))
    //        {
    //            lblStatus.Text = "This PMT team list don't have Email Id.Please update the Email id.";
    //            return;
    //        }
    //        else
    //        {
    //            item.To.Add(strCCMail.ToString());

    //            stream = new System.IO.MemoryStream();
    //            report.ExportDocument(Stimulsoft.Report.StiExportFormat.Pdf, stream);
    //            stream.Seek(0, SeekOrigin.Begin);
    //            Attachment attachment = new Attachment(stream, "MyReport.pdf", "application/pdf");
    //            item.Attachments.Add(attachment);


    //            //Create SMTP Client
    //            smtp.Host = ConfigurationManager.AppSettings["ExchangeServer"].ToString();
    //            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString());

    //            //smtp.EnableSsl = true;
    //            smtp.Send(item);
    //            item.Dispose();
    //            smtp.Dispose();

    //            lblStatus.Text = "Email Send Successfully";
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblStatus.Text = "An error occured while trying to generate report, Please try again/Contact your administrator";
    //        // lblStatus.Text = "An error occured while trying to generate report.";
    //    }


    //}

    protected void btnEmail_clik(object sender, EventArgs e)
    {
        lblStatus.Text = "";

        //System.IO.MemoryStream stream = null;
        //Stimulsoft.Report.StiReport report = default(Stimulsoft.Report.StiReport);       

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
             //SmtpClient smtpClient = new SmtpClient();
        //MailMessage message = new MailMessage();
        try
        {
            //report = StiWebViewer1.Report;
            //stream = new System.IO.MemoryStream();
            //report.ExportDocument(Stimulsoft.Report.StiExportFormat.Pdf, stream);
            //stream.Seek(0, SeekOrigin.Begin);



            //MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["MailAddress"].ToString(), "PMT");
            //smtpClient.Host = ConfigurationManager.AppSettings["Webserver"].ToString();
            //smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString());


            //Attachment attachment = new Attachment(stream, "MyReport.pdf", "application/pdf");

            //message.Attachments.Add(attachment);


            //message.Priority = MailPriority.High;
            //message.From = fromAddress;

            string msg = "Dear All,\n\n" + "Please Find here with Enclosed the " + cboReports.Text.ToString() + ". " + "\r\n" + "\r\n" + "  " + "\r\n" + "  " + "\r\n" + "\r\n" + "\r\n" + "Note: " + "\r\n" + "*. Do not reply to this mail" + "\r\n" + "*. This is system generated mail " + "\r\n";

            string ToMail = "", Staff = "";

            string sql1 = "select distinct(Project_Manager) as Manager,EMAIL FROM Vw_AssignStaff WHERE Project_ID='" + cboProject.SelectedValue.ToString().Trim() + "'";
            SqlCommand command1 = new SqlCommand(sql1, conn);
            SqlDataReader reader1 = command1.ExecuteReader();
            if (reader1.Read())
            {
                Staff = reader1["Manager"].ToString();
                ToMail = reader1["EMAIL"].ToString();
                if (cboReports.SelectedItem.Text == "Manpower List")
                {

                    BusinessTier.SendMail(ToMail, "Project Staff List", msg, StiWebViewer1.Report);
                }
            }

            BusinessTier.DisposeReader(reader1);

            string strCCMail = "";

            string sql2 = "select EmailList FROM PMT_EmailList WHERE emailgroup ='" + cboGroup.Text.ToString().Trim() + "'";
            SqlCommand command2 = new SqlCommand(sql2, conn);
            SqlDataReader reader2 = command2.ExecuteReader();
            if (reader2.Read())
            {
                strCCMail = reader2["EmailList"].ToString();

            }
            BusinessTier.DisposeReader(reader2);





            //if (cboReports.SelectedItem.Text == "Manpower List")
            //{
            //message.To.Add(ToMail.ToString());
            if (cboGroup.Text != "None")
            {
                //message.CC.Add(strCCMail.ToString());

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

            //}
            //else if (cboReports.SelectedItem.Text == "Project List" || cboReports.SelectedItem.Text == "Staff List")

            //{
            //    if (cboGroup.Text=="None")
            //    {
            //        lblStatus.Text = "Please Select 'To Mail'";
            //        lblStatus.ForeColor = Color.Red;
            //        return;
            //    }
            //    message.To.Add(strCCMail.ToString());
            //}



            //smtpClient.EnableSsl = true;
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.UseDefaultCredentials = false;
            //smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailAddress"].ToString(), ConfigurationManager.AppSettings["Password"].ToString().Trim());
            //smtpClient.Send(message);
            //message.Dispose();
            //smtpClient.Dispose();

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

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }

}