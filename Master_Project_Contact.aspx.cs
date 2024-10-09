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

public partial class Master_Project_Contact : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

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

                lblCust.ToolTip = Request.QueryString.Get("CustID").ToString().Trim();
                lblCust.Text = Request.QueryString.Get("CustName").ToString().Trim();

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



    protected void cboCustomerContact1_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        try
        {
            string sql1 = "";

            sql1 = " select * from Master_CustomerContact where DELETED=0  and CUSTOMER_ID= " + Convert.ToInt32(lblCust.ToolTip.ToString().Trim()) + " + '%' and [CONTACT_PERSON] LIKE '%' + @text + '%'  order by CONTACT_PERSON";

            conn.Open();
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
                item.Attributes.Add("CONTACT_NO", row["CONTACT_NO"].ToString());
                comboBox.Items.Add(item);
                item.DataBind();
            }
            adapter1.Dispose();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {

            //ShowMessage("Please Select the Installation Name", "Red");
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Master_Project_Contact", "cboCustomerContact1_OnItemsRequested", ex.ToString(), "Audit");

        }
        finally
        {
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void cboCustomerContact2_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
    }

    protected void cboCustomerContact3_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }
}