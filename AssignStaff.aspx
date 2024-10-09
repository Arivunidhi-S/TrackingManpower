<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignStaff.aspx.cs" Inherits="AssignStaff" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <title>Assign Staff</title>
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
        .search
        {
            background: url(images/Reject.png) no-repeat;
            background-position: center;
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
        
        .fancy-green .ajax__tab_header
        {
            background: url(images/green_bg_Tab.gif) repeat-x;
            cursor: pointer;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer
        {
            background: url(images/green_left_Tab.gif) no-repeat left top;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner
        {
            background: url(images/green_right_Tab.gif) no-repeat right top;
        }
        .fancy .ajax__tab_header
        {
            font-size: 13px;
            font-weight: bold;
            color: #000;
            font-family: sans-serif;
        }
        .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer
        {
            height: 46px;
        }
        .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner
        {
            height: 46px;
            margin-left: 16px; /* offset the width of the left image */
        }
        .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab
        {
            margin: 16px 16px 0px 0px;
        }
        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab
        {
            color: #fff;
        }
        .fancy .ajax__tab_body
        {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
            <td align="left" colspan="2" style="width: 100%;">
                <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                </telerik:RadCodeBlock>
                <telerik:RadScriptManager ID="ScriptManager1" runat="server" />
                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                    <AjaxSettings>
                        <%--<telerik:AjaxSetting AjaxControlID="RadGridAssignStaff">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="RadGridAssignStaff" />
                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                <telerik:AjaxUpdatedControl ControlID="cboStaff" />
                                <telerik:AjaxUpdatedControl ControlID="DtStart" />
                                <telerik:AjaxUpdatedControl ControlID="DtEnd" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>--%>
                        <telerik:AjaxSetting AjaxControlID="cboProject">
                            <UpdatedControls>
                              <telerik:AjaxUpdatedControl ControlID="RadGridFreelancer" />
                                <telerik:AjaxUpdatedControl ControlID="RadGridAssignStaff" />
                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />                             
                                <telerik:AjaxUpdatedControl ControlID="cboStaff" />
                                <telerik:AjaxUpdatedControl ControlID="cboFreeLanceStaffId" />
                                <telerik:AjaxUpdatedControl ControlID="DtStart" />
                                <telerik:AjaxUpdatedControl ControlID="DtEnd" />
                                <telerik:AjaxUpdatedControl ControlID="txtProjectInfo" />
                                <telerik:AjaxUpdatedControl ControlID="cboCompany" />
                                <telerik:AjaxUpdatedControl ControlID="cboDesignation" />                               
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <%--<telerik:AjaxSetting AjaxControlID="RadGridFreelancer">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="RadGridFreelancer" />
                                <telerik:AjaxUpdatedControl ControlID="txtNameFreelancer" />                               
                                <telerik:AjaxUpdatedControl ControlID="txtICNo" />
                                <telerik:AjaxUpdatedControl ControlID="txtSalary" />
                                <telerik:AjaxUpdatedControl ControlID="cboSalaryBy" />
                                <telerik:AjaxUpdatedControl ControlID="DtStart" />
                                <telerik:AjaxUpdatedControl ControlID="DtEnd" />
                                 <telerik:AjaxUpdatedControl ControlID="btnclikrjt" />
                                 <telerik:AjaxUpdatedControl ControlID="lnkDownload" />
                                  <telerik:AjaxUpdatedControl ControlID="upProjectFile" />
                                   <telerik:AjaxUpdatedControl ControlID="btnSave" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>--%>                       
                    
                        <telerik:AjaxSetting AjaxControlID="cboStaff">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="cboCompany" />
                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                <telerik:AjaxUpdatedControl ControlID="txtStaffHistory" />
                                <telerik:AjaxUpdatedControl ControlID="imgGreen" />
                                <telerik:AjaxUpdatedControl ControlID="imgRed" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                             <telerik:AjaxSetting AjaxControlID="btnSave">                          
                                 <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                 <telerik:AjaxUpdatedControl ControlID="RadGridFreelancer" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadInputManager ID="RadInputManager1" runat="server">
                    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                        Validation-IsRequired="true">
                    </telerik:TextBoxSetting>
                </telerik:RadInputManager>
                <%--<div id="DivHeader" runat="server" class="otto">
                                ASSIGN STAFF
                            </div>--%>
                <div id="DivHeader" runat="server" class="otto">
                    ASSIGN STAFF TASK - DETAILS
                </div>
                <div style="height: 20px; text-align: center">
                    <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                </div>
                <div id="Div1" style="background: -webkit-linear-gradient(#FFFFFF,#a3ce65);
                    border: 4px inset #000000;">
                    <table width="100%" border="1px" cellpadding="5" cellspacing="2">
                        <tr>
                            <td style="width: 5%; text-align: right">
                                <asp:Label ID="lblProject" Text="Contract/ Project:" Font-Names="Verdana" Font-Size="Small"
                                    runat="server" Style="color: Black" />
                            </td>
                            <td style="width: 20%; text-align: left">
                                <telerik:RadComboBox ID="cboProject" runat="server" Height="300px" Width="150px"
                                    Skin="Hay" OnSelectedIndexChanged="cboProject_SelectedIndexChanged" DropDownWidth="610px"
                                    EmptyMessage="Select Project Name" EnableLoadOnDemand="true" AutoPostBack="true"
                                    AppendDataBoundItems="True" Visible="true" OnItemsRequested="cboProject_OnItemsRequested">
                                    <HeaderTemplate>
                                        <table style="width: 600px" cellspacing="0" cellpadding="0">
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
                                        <table style="width: 600px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 250px; border: 1px solid #000000; padding: 2px 2px">
                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                </td>
                                                <td style="width: 150px; border-bottom: 1px solid #000000; border-top: 1px solid #000000;">
                                                    <%# DataBinder.Eval(Container, "Attributes['Project_No']")%>
                                                </td>
                                                <td style="width: 200px; border: 1px solid #000000; padding: 2px 2px">
                                                    <%# DataBinder.Eval(Container, "Attributes['STAFF_NAME']")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td style="width: 5%; text-align: right">
                                            <asp:Label ID="Label7" Text="Assign Designation:" runat="server" Font-Names="Verdana" Font-Size="Small"
                                                Style="color: Black" />
                                        </td>
                                        <td style="width: 10%; text-align: left;">
                                            <telerik:RadComboBox ID="cboDesignation" runat="server" Height="200px" Width="150px"
                                                DropDownWidth="310px" DataValueField="Dept_ID" OnItemsRequested="cboDesignation_OnItemsRequested"
                                                EnableLoadOnDemand="true" AppendDataBoundItems="True" EmptyMessage="Select Designation"
                                                Skin="Hay">
                                                <HeaderTemplate>
                                                    <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 200px;">
                                                                Designation
                                                            </td>
                                                            <td style="width: 100px;">
                                                                Manpower Qty
                                                            </td>
                                                            <%-- <td style="width: 100px;">
                                                               Skill
                                                            </td>--%>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 200px;" align="left">
                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                            </td>
                                                            <td style="width: 100px;" align="left">
                                                                <%# DataBinder.Eval(Container, "Attributes['Manpower']")%>
                                                            </td>
                                                            <%--<td style="width: 100px;" align="left">
                                                                <%# DataBinder.Eval(Container, "Attributes['Skill']")%>
                                                            </td>--%>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                            <td style="width: 15%; text-align: right">
                                <asp:Label ID="Label2" Text="Staff Start Date:" runat="server" Font-Names="Verdana" Font-Size="Small"
                                    Style="color: Black" />
                            </td>
                            <td style="width: 10%;">
                                <telerik:RadDatePicker ID="DtStart" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                    DbSelectedDate='<%# Bind("StartDate") %>' DateInput-DateFormat="dd/MMM/yyyy"
                                    Culture="en-US" MinDate="01/01/1900" Skin="Hay">
                                    <Calendar ID="Calendar1" runat="server">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                            </td>
                            <td style="width: 15%; text-align: right">
                                <asp:Label ID="Label1" Text="Staff End Date:" runat="server" Font-Names="Verdana" Font-Size="Small"
                                    Style="color: Black" />
                            </td>
                            <td style="width: 10%; text-align: left">
                                <telerik:RadDatePicker ID="DtEnd" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                    Skin="Hay" DbSelectedDate='<%# Bind("EndDate") %>' DateInput-DateFormat="dd/MMM/yyyy"
                                    Culture="en-US" MinDate="01/01/1900">
                                    <Calendar ID="Calendar2" runat="server">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                            </td>
                            <td style="width: 20%; text-align: left">
                                <telerik:RadTextBox Text="" ID="txtProjectInfo" Height="65px" Width="280px" TextMode="MultiLine"
                                    BackColor="White" Font-Size="12px" CssClass="textboxstyle_new" runat="server"
                                    Visible="true" Enabled="false" Font-Bold="false" />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" />
                <ajaxToolkit:TabContainer runat="server" ID="TabContainer1" ActiveTabIndex="0" CssClass="fancy fancy-green"
                    BackColor="#C0D7E8">
                    <ajaxToolkit:TabPanel BackColor="Transparent" runat="server" ID="TabPanel1" HeaderText="Step 1: Add From Staff Master">
                        <ContentTemplate>
                       <%-- <table>
                        <tr><td>Add to Assign Staff</td> <td>Add to FreeLancer</td></tr>
                        <tr><td>--%>
                            <div style="height: auto; background: -webkit-linear-gradient(#a3ce65,#f2e9b0); border: 4px solid Black;">
                                <table cellspacing="1" cellpadding="1" width="100%" border="0">
                                    <tr>
                                        <td style="width: 10%; text-align: right">
                                            <asp:Label ID="Label3" Text="Branch:" runat="server" Font-Names="Verdana" Font-Size="Small"
                                                Style="color: Black" />
                                        </td>
                                        <td style="width: 25%; text-align: left;">
                                            <telerik:RadComboBox ID="cboCompany" runat="server" Height="200px" Width="200px"
                                                AutoPostBack="true" DropDownWidth="360px" DataValueField="Dept_ID" OnItemsRequested="cboCompany_OnItemsRequested"
                                                EnableLoadOnDemand="true" AppendDataBoundItems="True" Text='<%# Eval("Company_Name") %>'
                                                EmptyMessage="Select Branch" Skin="Hay">
                                                <HeaderTemplate>
                                                    <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                Branch Name
                                                            </td>
                                                            <td style="width: 100px;">
                                                                Branch Code
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 250px;" align="left">
                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                            </td>
                                                            <td style="width: 100px;" align="left">
                                                                <%# DataBinder.Eval(Container, "Attributes['Country_Code']")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="width: 10%; text-align: right">
                                            <asp:Label ID="lblStaff" Text="Staff:" runat="server" Font-Names="Verdana" Font-Size="Small"
                                                Style="color: Black" />
                                        </td>
                                        <td style="width: 10%; text-align: left;">
                                            <telerik:RadComboBox ID="cboStaff" runat="server" Height="200px" Width="200px" DropDownWidth="610px"
                                                AutoPostBack="true" DataValueField="Dept_ID" OnItemsRequested="cboStaffId_OnItemsRequested"
                                                OnSelectedIndexChanged="cboStaff_SelectedIndexChanged" EnableLoadOnDemand="true"
                                                AppendDataBoundItems="True" EmptyMessage="Select Staff" Skin="Hay">
                                                <HeaderTemplate>
                                                    <table style="width: 600px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                Staff Name
                                                            </td>
                                                            <td style="width: 100px;">
                                                                Staff No
                                                            </td>
                                                             <td style="width: 250px;">
                                                               Position
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 600px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 250px; border: thin solid #000000;padding :2px 2px" align="left">
                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                            </td>
                                                            <td style="width: 100px; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding :2px 2px " align="left">
                                                                <%# DataBinder.Eval(Container, "Attributes['Staff_No']")%>
                                                            </td>
                                                            <td style="width: 250px;border: thin solid #000000;padding :2px 2px" align="left">
                                                                <%# DataBinder.Eval(Container, "Attributes['Designation']")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        
                                        <td style="width: 10%; text-align: left;">
                                            <asp:Button ID="btnAssignStaff" runat="server" Text=" Assign " Font-Names="Verdana"
                                                Font-Size="Small" CssClass="button" OnClick="btnAssignStaff_OnClick" />
                                        </td>
                                        <td style="width: 10%; text-align: left;">
                                            <asp:Image ImageUrl="~/Images/greenLed.jpg" BorderStyle="None" Height="30px" Width="30px"
                                                runat="server" ID="imgGreen" Visible="false" />
                                            <asp:Image ImageUrl="~/Images/redled.jpg" BorderStyle="None" Height="30px" Width="30px"
                                                runat="server" ID="imgRed" Visible="false" />
                                        </td>
                                        <td style="width: 25%; text-align: left;">
                                            <telerik:RadTextBox Text="" ID="txtStaffHistory" Height="60px" Width="250px" TextMode="MultiLine"
                                                BackColor="White" Font-Size="12px" CssClass="textboxstyle_new" runat="server"
                                                Font-Bold="false" Visible="true" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 100%;" colspan="9">
                                            <div id="Div2" runat="server" style="width: 100%; height: 390px; overflow: auto;">
                                                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                                                <telerik:RadGrid ID="RadGridAssignStaff" runat="server" AllowMultiRowEdit="false" AllowAutomaticDeletes="true"
                                                    OnDeleteCommand="RadGridAssignStaff_DeleteCommand" OnNeedDataSource="RadGridAssignStaff_NeedDataSource"
                                                    GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                                                    Skin="Hay" PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowSorting="true" AllowFilteringByColumn="true"> 
                                                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                                                        <Selecting AllowRowSelect="false" />
                                                    </ClientSettings>
                                                    <GroupingSettings CaseSensitive="false" />
                                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="Assign_ID" CommandItemDisplay="None"
                                                        CommandItemSettings-AddNewRecordText="Add New Contact Details">
                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                        <Columns>
                                                            <%--<telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                                        <HeaderStyle Width="5%" />
                                                                    </telerik:GridEditCommandColumn>--%>
                                                            <telerik:GridBoundColumn DataField="Assign_ID" DataType="System.Int64" HeaderText="ID"
                                                                ReadOnly="True" SortExpression="Assign_ID" UniqueName="Assign_ID" AllowFiltering="false"
                                                                AllowSorting="false" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                     
                                                           
                                                            <telerik:GridBoundColumn DataField="STAFF_NAME" DataType="System.String" HeaderText="Staff Name"
                                                                SortExpression="STAFF_NAME" UniqueName="STAFF_NAME" FilterControlWidth="200px"
                                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" ShowFilterIcon="false"
                                                                FilterDelay="2000">
                                                                <HeaderStyle Width="15%" />
                                                            </telerik:GridBoundColumn>
                                                                   <telerik:GridBoundColumn DataField="STAFF_NO" DataType="System.String" HeaderText="Staff No"
                                                                SortExpression="STAFF_NO" UniqueName="STAFF_NO" FilterControlWidth="200px"
                                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" ShowFilterIcon="false"
                                                                FilterDelay="2000">
                                                                <HeaderStyle Width="10%" />
                                                            </telerik:GridBoundColumn>
                                                             <telerik:GridBoundColumn DataField="AssignDesignation" DataType="System.String" HeaderText="Assign Designation"
                                                                SortExpression="AssignDesignation" UniqueName="AssignDesignation" FilterControlWidth="200px"
                                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" ShowFilterIcon="false"
                                                                FilterDelay="2000">
                                                                <HeaderStyle Width="15%" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="AssignStartDt" DataType="System.DateTime" HeaderText="Start Date"
                                                                AllowFiltering="false" SortExpression="AssignStartDt" UniqueName="AssignStartDt"
                                                                Visible="true" DataFormatString="{0:dd/MMM/yyyy}">
                                                                <HeaderStyle Width="10%" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="AssignEndtDt" DataType="System.DateTime" HeaderText="End Date"
                                                                SortExpression="AssignEndtDt" UniqueName="AssignEndtDt" DataFormatString="{0:dd/MMM/yyyy}"
                                                                AllowFiltering="false">
                                                                <HeaderStyle Width="10%" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                                ConfirmText="Are you sure want to delete?">
                                                                <HeaderStyle Width="5%" />
                                                            </telerik:GridButtonColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                                </telerik:RadGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div> 
                            <%--</td><td>--%>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel BackColor="Transparent" runat="server" ID="TabPanel2" HeaderText="Step 2: Add From Freelancer">
                        <ContentTemplate>
                            <div style="height: auto; background: -webkit-linear-gradient(#a3ce65,#f2e9b0); border: 4px solid Black;">
                                <table cellspacing="1" cellpadding="1" width="100%" border="0">
                                    <tr>
                                        <td style="width: 10%; text-align: right">
                                            <asp:Label ID="Label4" Text="Freelancer:" runat="server" Font-Names="Verdana" Font-Size="Small"
                                                Style="color: Black" />
                                        </td>
                                        <td style="width: 25%; text-align: left;">
                                            <telerik:RadTextBox Text="" ID="txtNameFreelancer" Height="20px" Width="150px" BackColor="White"
                                                Font-Size="12px" CssClass="textboxstyle_new" runat="server" Visible="false" Enabled="true"
                                                Font-Bold="false" EmptyMessage="Name"  />

                                                  <telerik:RadComboBox ID="cboFreeLanceStaffId" runat="server" Height="200px" Width="200px" DropDownWidth="610px"
                                                AutoPostBack="false" DataValueField="Dept_ID" OnItemsRequested="cboFreeLanceStaffId_OnItemsRequested"
                                                 EnableLoadOnDemand="true"
                                                AppendDataBoundItems="True" EmptyMessage="Select Freelancer" Skin="Hay">
                                                <HeaderTemplate>
                                                    <table style="width: 600px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                Staff Name
                                                            </td>
                                                            <td style="width: 100px;">
                                                                Staff No
                                                            </td>
                                                             <td style="width: 250px;">
                                                               Position
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 600px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 250px; border: thin solid #000000;padding :2px 2px" align="left">
                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                            </td>
                                                            <td style="width: 100px; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding :2px 2px " align="left">
                                                                <%# DataBinder.Eval(Container, "Attributes['Staff_No']")%>
                                                            </td>
                                                            <td style="width: 250px;border: thin solid #000000;padding :2px 2px" align="left">
                                                                <%# DataBinder.Eval(Container, "Attributes['Designation']")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        
                                        <td style="width: 10%; text-align: right;">
                                            <asp:Label ID="Label6" Text="Salary:" runat="server" Font-Names="Verdana" Font-Size="Small"
                                                Style="color: Black" />
                                        </td>
                                        <td style="width: 10%; text-align: left;">
                                            <%--<telerik:RadTextBox Text="" ID="txtSalary" Height="20px" Width="150px" BackColor="White"
                                                            Font-Size="12px" CssClass="textboxstyle_new" runat="server" Visible="true" Enabled="true"
                                                            Font-Bold="false" />--%>
                                            <telerik:RadNumericTextBox MinValue="0" MaxValue="9999999999999999999999999" MaxLength="10"
                                                ToolTip="Maximum Length: 20" BackColor="#f4f793" ID="txtSalary" Height="20px"
                                                Width="150px" runat="server" EmptyMessage="Salary">
                                                <NumberFormat GroupSeparator="" DecimalDigits="2" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%; text-align: left;">
                                            <telerik:RadComboBox ID="cboSalaryBy" runat="server" Height="80px" Width="80px" DataTextField="Calib_type"
                                                DataValueField="Calib_type" AppendDataBoundItems="True" AutoPostBack="false">
                                                <%-- Text='<%# Bind("Enquiry_type") %>'--%>
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="/Hour" Value="0" />
                                                    <telerik:RadComboBoxItem Text="/Day" Value="1" />
                                                    <telerik:RadComboBoxItem Text="/Month" Value="2" />
                                                    <telerik:RadComboBoxItem Text="/Lumpsum" Value="3" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                          <td>
                                            <asp:Button ID="btnSave" runat="server" Text=" Save " Font-Names="Verdana" Font-Size="Small"
                                                CssClass="button" OnClick="btnSave_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                    <td style="width: 10%; text-align: right">
                                           <%-- <asp:Label ID="Label5" Text="IC Number:" runat="server" Font-Names="Verdana" Font-Size="Small"
                                                Style="color: Black" />--%>
                                        </td>
                                        <td style="width: 10%; text-align: left;">
                                            <telerik:RadTextBox Text="" ID="txtICNo" Height="20px" Width="150px" BackColor="White"
                                                Font-Size="12px" CssClass="textboxstyle_new" runat="server" Visible="false" Enabled="true"
                                                Font-Bold="false" EmptyMessage="ICNo" />
                                        </td>
                                       <%-- <td style="width: 10%; text-align: right">
                                            File:
                                        </td>--%>
                                        <td style="width: 10%; text-align: center;" colspan="2">
                                            <telerik:RadAsyncUpload runat="server" ID="upProjectFile" Enabled="true" AllowedFileExtensions="pdf,doc,docx"
                                                ToolTip="Max : 2MB Size/Pic" MaxFileInputsCount="1" MaxFileSize="2097152" AutoAddFileInputs="true"
                                                ViewStateMode="Enabled" Visible="false">
                                                <%--  <Localization Select='<%# DataBinder.Eval(Container, "FileName")%>' /> --%>
                                            </telerik:RadAsyncUpload>
                                            <asp:LinkButton ID="lnkDownload" Font-Size="13px" runat="server" Visible="true" ForeColor="Red"
                                                OnClick="lnkDownload_OnClick" CommandName="Download" />
                                            <asp:Button runat="server" ID="btnclikrjt" Font-Size="Smaller" ForeColor="Red" Text=" "
                                                BorderStyle="None" CssClass="search" AutoPostBack="true" Width="25px" Height="20px"
                                                OnClick="btnclikrjt_Onclick" />
                                        </td>
                                      
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 100%;" colspan="7">
                                            <div id="Div4" runat="server" style="width: 100%; height: 390px; overflow: auto;">
                                                <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel3" runat="server" />--%>
                                                <telerik:RadGrid ID="RadGridFreelancer" runat="server" AllowMultiRowEdit="false" OnDeleteCommand="RadGridFreelancer_DeleteCommand"
                                                    OnItemCommand="RadGridFreelancer_ItemCommand" OnNeedDataSource="RadGridFreelancer_NeedDataSource"
                                                    GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                                                    Skin="Hay" PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowSorting="true" AllowFilteringByColumn="true">
                                                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                                        <Selecting AllowRowSelect="true" />
                                                    </ClientSettings>
                                                    <GroupingSettings CaseSensitive="false" />
                                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="FreeLancer_ID" CommandItemDisplay="None"
                                                        CommandItemSettings-AddNewRecordText="Add New Contact Details">
                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                        <Columns>
                                                            <%--<telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                                        <HeaderStyle Width="5%" />
                                                                    </telerik:GridEditCommandColumn>--%>
                                                            <telerik:GridBoundColumn DataField="FreeLancer_ID" DataType="System.Int64" HeaderText="ID"
                                                                ReadOnly="True" SortExpression="FreeLancer_ID" UniqueName="FreeLancer_ID" AllowFiltering="false"
                                                                AllowSorting="false" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                           <%-- <telerik:GridBoundColumn DataField="Project_Name" DataType="System.String" HeaderText="Project Name"
                                                                SortExpression="Project_Name" UniqueName="Project_Name" FilterControlWidth="100px"
                                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" ShowFilterIcon="false"
                                                                FilterDelay="2000">
                                                                <HeaderStyle Width="15%" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Project_Manager" DataType="System.String" HeaderText="Project Manager"
                                                                SortExpression="Project_Manager" UniqueName="Project_Manager" FilterControlWidth="100px"
                                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" ShowFilterIcon="false"
                                                                FilterDelay="2000">
                                                                <HeaderStyle Width="15%" />
                                                            </telerik:GridBoundColumn>--%>
                                                            <telerik:GridBoundColumn DataField="STAFF_NAME" DataType="System.String" HeaderText="Staff Name"
                                                                SortExpression="STAFF_NAME" UniqueName="STAFF_NAME" FilterControlWidth="100px"
                                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" ShowFilterIcon="false"
                                                                FilterDelay="2000">
                                                                <HeaderStyle Width="15%" />
                                                            </telerik:GridBoundColumn>
                                                              
                                                                   <telerik:GridBoundColumn DataField="STAFF_NO" DataType="System.String" HeaderText="Staff No"
                                                                SortExpression="STAFF_NO" UniqueName="STAFF_NO" FilterControlWidth="200px"
                                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" ShowFilterIcon="false"
                                                                FilterDelay="2000">
                                                                <HeaderStyle Width="10%" />
                                                            </telerik:GridBoundColumn>
                                                             <telerik:GridBoundColumn DataField="AssignDesignation" DataType="System.String" HeaderText="Assign Designation"
                                                                SortExpression="AssignDesignation" UniqueName="AssignDesignation" FilterControlWidth="200px"
                                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" ShowFilterIcon="false"
                                                                FilterDelay="2000">
                                                                <HeaderStyle Width="15%" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Salary" DataType="System.String" HeaderText="Salary"
                                                                SortExpression="Salary" UniqueName="Salary" AllowFiltering="false">
                                                                <HeaderStyle Width="10%" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="BasisSalary" DataType="System.String" HeaderText="BasisSalary"
                                                                SortExpression="BasisSalary" UniqueName="BasisSalary" AllowFiltering="false">
                                                                <HeaderStyle Width="5%" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="AssignStartDt" DataType="System.DateTime" HeaderText="Start Date"
                                                                AllowFiltering="false" SortExpression="AssignStartDt" UniqueName="AssignStartDt"
                                                                Visible="true" DataFormatString="{0:dd/MMM/yyyy}">
                                                                <HeaderStyle Width="10%" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="AssignEndtDt" DataType="System.DateTime" HeaderText="End Date"
                                                                SortExpression="AssignEndtDt" UniqueName="AssignEndtDt" DataFormatString="{0:dd/MMM/yyyy}"
                                                                AllowFiltering="false">
                                                                <HeaderStyle Width="10%" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                                ConfirmText="Are you sure want to delete?">
                                                                <HeaderStyle Width="5%" />
                                                            </telerik:GridButtonColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                                </telerik:RadGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                           <%-- </td></tr></table>--%>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 100%;" align="right">
                <div id="Div3" runat="server" style="width: 100%; background-color: White; background-image: url(Images/Untitled.jpg);
                    overflow: auto;">
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
