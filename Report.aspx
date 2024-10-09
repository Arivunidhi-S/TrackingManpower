<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2011.2.1100.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a"
    Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report</title>
    <link rel="shortcut icon" href="images/serbatitle.png" />
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="css/styles.css" />
    <style type="text/css">
        .otto
        {
            text-align: center;
            font-size: x-large;
            font-family: Arial;
            font-weight: bold;
            color: white;
            height: 30px;
            text-shadow: 2px 1px 5px rgba(0, 0, 0, 1);
        }
        .box
        {
            box-shadow: 0.2px 0.2px 8px 0.2px;
        }
        body
        {
            margin: 0;
            background-image: url(images/bg1.jpg); /*You will specify your image path here.*/
            -moz-background-size: cover;
            -webkit-background-size: cover;
            background-size: cover;
            background-position: top center !important;
            background-repeat: no-repeat !important;
            background-attachment: fixed;
        }
    </style>
</head>
<script language="javascript" type="text/javascript">
    history.go(1); /* undo user navigation (ex: IE Back Button) */
</script>
<body >
    <form id="server" runat="server">
    <table border="0" width="100%">
        <tr>
            <td id="Td1" align="left" runat="server" colspan="2">
                <div id='cssmenu'>
                    <ul>
                        <% for (int a = 0; a < dtMenuItems.Rows.Count; a++)
                           { %>
                        <li class="has-sub"><a href="#"><span>
                            <%= dtMenuItems.Rows[a][0].ToString()  %></span></a>
                            <ul>
                                <% dtSubMenuItems = BusinessTier.getSubMenuItems(dtMenuItems.Rows[a][0].ToString(), Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                                   int aa;

                                   for (aa = 0; aa < dtSubMenuItems.Rows.Count; aa++)
                                   { %>
                                <li class="has-sub"><a id='<%= dtSubMenuItems.Rows[aa][0].ToString() %>' href='<%= dtSubMenuItems.Rows[aa][1].ToString() %>'>
                                    <span>
                                        <%= dtSubMenuItems.Rows[aa][2].ToString()%>
                                    </span></a></li>
                                <% } %>
                            </ul>
                        </li>
                        <% } %>
                        <li class="last"><a href="Login.aspx" style="border-right-width: 1px;">LOGOUT</a></li>
                        <div style="text-align: right; margin: 10px 10px 0px 0px; font-famliy: Arial; color: #e0e0e0;
                            text-shadow: 0 0 2px #49ff18;">
                            <asp:Label ID="lblname" runat="server" Font-Bold="true" /></div>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                    <Scripts>
                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
                    </Scripts>
                </telerik:RadScriptManager>
                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="RadGrid1">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
            </td>
        </tr>
        <tr style="background-color: transparent" valign="top">
            <td align="center">
                <div id="DivHeader" runat="server" class="otto">
                    Reports
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">
                <div>
                    <asp:Label ID="lblStatus" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Report :"></asp:Label>
                &nbsp; &nbsp;
                <telerik:RadComboBox ID="cboReports" runat="server" Height="40px" Width="150px" Visible="true"
                    AutoPostBack="true" DropDownWidth="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                    OnSelectedIndexChanged="cboReports_Select_Change">
                    <Items>
                        <telerik:RadComboBoxItem Text="Project List" Value="1" />
                        <telerik:RadComboBoxItem Text="Manpower List" Value="2" />
                       <%-- <telerik:RadComboBoxItem Text="Staff List" Value="3" />--%>
                        <%--
                                    <telerik:RadComboBoxItem Text="Material Details" Value="4" />
                                    <telerik:RadComboBoxItem Text="Incoming Material" Value="7" />
                                    <telerik:RadComboBoxItem Text="Staff Details" Value="5" />
                                    <telerik:RadComboBoxItem Text="Warranty Sticker" Value="6" />
                                    <telerik:RadComboBoxItem Text="Spring Details" Value="8" />
                                    <telerik:RadComboBoxItem Text="Sales Report" Value="9" />--%>
                    </Items>
                </telerik:RadComboBox>
                &nbsp; &nbsp;
                <asp:Label ID="lblProject" runat="server" Font-Bold="true" Text="Project :"></asp:Label>
                &nbsp; &nbsp;
                <telerik:RadComboBox ID="cboProject" runat="server" Height="300px" Width="200px"
                    DropDownWidth="600px" EmptyMessage="Select Project Name" EnableLoadOnDemand="true"
                    AutoPostBack="true" AppendDataBoundItems="True" Visible="true" OnItemsRequested="cboProject_OnItemsRequested">
                    <HeaderTemplate>
                        <table style="width: 590px" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 250px;">
                                    Project Name
                                </td>
                                <td style="width: 150px;">
                                    Project No
                                </td>
                                <td style="width: 200px;">
                                    Project Manager
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 590px" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 250px; border: thin solid #000000;padding :2px 2px">
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                              <td style="width: 150px;border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding :2px 2px ">
                                    <%# DataBinder.Eval(Container, "Attributes['Project_No']")%>
                                </td>
                                <td style="width: 200px; border: thin solid #000000;padding :2px 2px">
                                    <%# DataBinder.Eval(Container, "Attributes['STAFF_NAME']")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
                <asp:Label ID="lblOrderid" runat="server" Font-Bold="true" Text="OrderNo :"></asp:Label>
                &nbsp; &nbsp;
                <%--    <telerik:RadComboBox ID="cboOrderID" runat="server" Height="160px" Width="150px"
                                DropDownWidth="150px" OnSelectedIndexChanged="cboOrderID_Select_Change" AutoPostBack="true"
                                AppendDataBoundItems="True" EmptyMessage="Select">
                                <Items>
                                </Items>
                            </telerik:RadComboBox>--%>
                &nbsp; &nbsp;
                <asp:Label ID="lblBranch" runat="server" Font-Bold="true" Text="Branch :"></asp:Label>
                &nbsp; &nbsp;
                <telerik:RadComboBox ID="cboBrach" runat="server" Height="200px" Width="200px" DropDownWidth="250"
                    AutoPostBack="true" AppendDataBoundItems="True" EmptyMessage="Select Branch"
                    OnItemsRequested="cboBranch_OnItemsRequested" EnableLoadOnDemand="true">
                    <HeaderTemplate>
                        <table style="width: 240px; font-size: small" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 250px;">
                                    Branch
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 240px; font-size: small" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 250px;" align="left">
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
                &nbsp; &nbsp;
                <asp:Label ID="lblDateStart" runat="server" Font-Bold="true" Text="StartDate :"></asp:Label>
                &nbsp; &nbsp;
                <%--   <telerik:RadDatePicker ID="dtStartDate" runat="server" Width="150px" PopupDirection="BottomRight"
                                DateInput-EmptyMessage="Select  Date" OnSelectedDateChanged="dtStartDate_Select_Change"
                                AutoPostBack="true">
                                <Calendar ID="Calendar2" runat="server" ShowRowHeaders="true">
                                </Calendar>
                                <DateInput ID="DateInput2" runat="server" Enabled="true" DateFormat="dd/MMM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>--%>
                &nbsp; &nbsp;
                <asp:Label ID="lblDateEnd" runat="server" Font-Bold="true" Text="EndDate :"></asp:Label>
                &nbsp; &nbsp;
                <%--<telerik:RadDatePicker ID="dtEndDate" runat="server" Width="150px" PopupDirection="BottomRight"
                                DateInput-EmptyMessage="Select  Date">
                                <Calendar ID="Calendar1" runat="server" ShowRowHeaders="true">
                                </Calendar>
                                <DateInput ID="DateInput1" runat="server" Enabled="true" DateFormat="dd/MMM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>--%>
                &nbsp; &nbsp;
                <asp:Button ID="btn_Report_Submit" runat="server" Text="  Submit  " BackColor="Black"
                    ForeColor="White" OnClick="Onclick_btnSubmit" />
                &nbsp; &nbsp;
                <asp:Label ID="lblmail" runat="server" Font-Bold="true"></asp:Label>&nbsp;
                <telerik:RadComboBox ID="cboGroup" runat="server" Height="120px" Width="100px" Visible="true"
                    AppendDataBoundItems="True" OnSelectedIndexChanged="cboGroup_SelectedIndexChanged"
                    EmptyMessage="Select" AutoPostBack="true">
                    <Items>
                        <telerik:RadComboBoxItem Text="None" Value="0" />
                        <telerik:RadComboBoxItem Text="Group1" Value="1" />
                        <telerik:RadComboBoxItem Text="Group2" Value="2" />
                        <telerik:RadComboBoxItem Text="Group3" Value="3" />
                        <telerik:RadComboBoxItem Text="Group4" Value="4" />
                        <telerik:RadComboBoxItem Text="Group5" Value="5" />
                    </Items>
                </telerik:RadComboBox>
                <asp:Button ID="btnEmail" runat="server" Text="  Send Mail  " BackColor="Black" ForeColor="White"
                    OnClick="btnEmail_clik" Visible="true" />
                &nbsp; &nbsp;
                <%-- <asp:Button ID="btn_Report_Print" runat="server" Text="  Print  " BackColor="Blue" ForeColor="White" OnClick="btn_Report_Print_Click"/>--%>
                <asp:Button ID="btn_Clear" runat="server" Text="  Clear  " BackColor="Black"
                    ForeColor="White" OnClick="btnclear_Click" />
               <br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <br />
                <div id="divrepo" class="text" style="background: -webkit-linear-gradient(#FFFFFF,#a3ce65);
                    border: 4px solid Black; width: 1300px; overflow: auto; box-shadow: 3px 5px 6px rgba(0,0,0,0.5);">
                    <br />
                    <cc1:StiWebViewer ID="StiWebViewer1" runat="server" RenderMode="UseCache" ScrollBarsMode="true"
                        Width="1250px" Height="700px" />
                    <br />
                </div>
            </td>
        </tr>
        <tr>
            <td align="center" style="width: 100%;">
                <div>
                    <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                                    SelectCommand="select * from OrderForm where deleted=0 ORDER BY [OrderAutoID]">
                                </asp:SqlDataSource>--%>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
