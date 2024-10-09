<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Master_Staff_Official.aspx.cs"
    Inherits="Master_Staff_Official" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Staff Master</title>
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
            background-image: url(images/Bg1.jpg); /*You will specify your image path here.*/
            -moz-background-size: cover;
            -webkit-background-size: cover;
            background-size: cover;
            background-position: top center !important;
            background-repeat: no-repeat !important;
            background-attachment: fixed;
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
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 80%;">
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                    <Scripts>
                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
                    </Scripts>
                </telerik:RadScriptManager>
                <script type="text/javascript">
                </script>
                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="RadGrid1">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                <telerik:AjaxUpdatedControl ControlID="cboDeptId" />
                                <telerik:AjaxUpdatedControl ControlID="cboStaffame" />
                                <telerik:AjaxUpdatedControl ControlID="cboEMPtype" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>

                        <telerik:AjaxSetting AjaxControlID="cboEMPtype">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                <telerik:AjaxUpdatedControl ControlID="cboBranch" />
                                <telerik:AjaxUpdatedControl ControlID="cboCompany" />
                                <telerik:AjaxUpdatedControl ControlID="cboReporting" />                                
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                <!-- content start -->
                <div id="Div1" runat="server">
                    <telerik:RadInputManager ID="RadInputManager1" runat="server">
                        <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                            Validation-IsRequired="true">
                        </telerik:TextBoxSetting>
                    </telerik:RadInputManager>
                    <div id="DivHeader" runat="server" class="otto">
                        STAFF OFFFICIAL DETAILS
                    </div>
                    <div style="height: 20px; text-align: center">
                        <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                    </div>
                    <div style="height: 20px; text-align: right">
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Ignore paging (exports all pages)"
                            Visible="false"></asp:CheckBox>
                        <asp:Button ID="btnExport" runat="server" Text="  Export  " BackColor="Black" ForeColor="White"
                            OnClick="Onclick_btnExport" />
                    </div>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                        GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                        OnDeleteCommand="RadGrid1_DeleteCommand" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                        OnItemDataBound="RadGrid1_ItemDataBound" PagerStyle-Mode="NextPrevNumericAndAdvanced" OnItemCreated="RadGrid1_ItemCreated"
                        Skin="Hay" AllowAutomaticDeletes="true" OnInsertCommand="RadGrid1_InsertCommand"
                        PageSize="50" AllowSorting="true" AllowFilteringByColumn="true" OnUpdateCommand="RadGrid1_UpdateCommand">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="StaffOfficial_ID" CommandItemDisplay="Top"
                            CommandItemSettings-AddNewRecordText="Add New Staff Details">
                            <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="true"
                                ShowExportToCsvButton="false" ShowExportToPdfButton="false"></CommandItemSettings>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="2%" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="StaffOfficial_ID" DataType="System.Int64" HeaderText="ID"
                                    ReadOnly="True" SortExpression="StaffOfficial_ID" UniqueName="StaffOfficial_ID"
                                    AllowFiltering="false" AllowSorting="false" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Staff_Name" DataType="System.String" HeaderText="Staff Name"
                                    SortExpression="Staff_Name" UniqueName="Staff_Name" FilterControlWidth="200px">
                                    <HeaderStyle Width="15%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Designation" DataType="System.String" HeaderText="Designation"
                                    SortExpression="Designation" UniqueName="Designation">
                                    <HeaderStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Location" DataType="System.String" HeaderText="Branch"
                                    SortExpression="Location" UniqueName="Location" Visible="true">
                                    <HeaderStyle Width="8%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="OGSPNO" DataType="System.String" HeaderText="OGSP NO"
                                    AllowFiltering="false" SortExpression="OGSPNO" UniqueName="OGSPNO">
                                    <HeaderStyle Width="4%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SHELLPASSPORTVALIDITY" DataType="System.String"
                                    HeaderText="Shell Pass No" AllowFiltering="false" SortExpression="SHELLPASSPORTVALIDITY"
                                    UniqueName="SHELLPASSPORTVALIDITY">
                                    <HeaderStyle Width="4%" />
                                </telerik:GridBoundColumn>
                                <%--                                <telerik:GridBoundColumn DataField="medi" DataType="System.String" HeaderText="Medical"
                                    SortExpression="medi" UniqueName="medi" AllowFiltering="false">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" AllowFiltering="false"
                                    HeaderText="Medical">
                                    <HeaderStyle Width="6%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStaffOfficial_ID" runat="server" Text='<%# Eval("StaffOfficial_ID")%>' Visible="false"/>
                                        <asp:Label ID="lblmedi" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" AllowFiltering="false"
                                    HeaderText="H2S">
                                    <HeaderStyle Width="5%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblh2s" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                 <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" AllowFiltering="false"
                                    HeaderText="Compentancy">
                                    <HeaderStyle Width="5%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCOMPENTANCY" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>                            
                                  <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" AllowFiltering="false"
                                    HeaderText="CIDBCert">
                                    <HeaderStyle Width="5%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblcid" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>  
                                 <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" AllowFiltering="false"
                                    HeaderText="ConfineSpace">
                                    <HeaderStyle Width="5%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCspace" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>  
                                 <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" AllowFiltering="false"
                                    HeaderText="WorkPermit">
                                    <HeaderStyle Width="5%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblpermit" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>  
                                <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" AllowFiltering="false"
                                    HeaderText="BordingPass">
                                    <HeaderStyle Width="5%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBording" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>  
                                <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" AllowFiltering="false"
                                    HeaderText="PTW">
                                    <HeaderStyle Width="5%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPT" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>                                  
                                <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                    ConfirmText="Are you sure want to delete?">
                                    <HeaderStyle Width="3%" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings EditFormType="Template">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                                <FormTemplate>
                                    <table cellspacing="2" cellpadding="1" width="100%" border="0">
                                        <tr>
                                            <td colspan="2">
                                                <b>ID:
                                                    <%--  <%# Eval("Staff_Id")%>--%>
                                                    <asp:Label ID="lblStaffID" Visible="true" runat="server" Width="20px" Text='<%# Eval("StaffOfficial_ID")%>' />
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Staff Name:
                                                <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red" Visible="true" />
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cboStaffame" runat="server" Height="200px" Width="200px"
                                                    Skin="Hay" DropDownWidth="360px" DataValueField="Dept_ID" OnItemsRequested="cboSTAFF_OnItemsRequested"
                                                    EnableLoadOnDemand="true" AppendDataBoundItems="True" Text='<%# Eval("Staff_Name") %>'
                                                    EmptyMessage="Select Staff">
                                                    <HeaderTemplate>
                                                        <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 250px;">
                                                                    Staff Name
                                                                </td>
                                                                <td style="width: 100px;">
                                                                    Staff No
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
                                                                    <%# DataBinder.Eval(Container, "Attributes['Staff_No']")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator runat="server" ID="DeptNameValidator1" ControlToValidate="cboStaffame"
                                                    ErrorMessage="Staff Name is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                Date Of Join:
                                                <%--   <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red" Visible="true" />--%>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="DtDOJ" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    Skin="Hay" DbSelectedDate='<%# Bind("DOJ") %>' DateInput-DateFormat="dd/MMM/yyyy"
                                                    MinDate="01/01/1900">
                                                    <Calendar ID="Calendar1" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Employee Type:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cboEMPtype" runat="server" DropDownWidth="200px" Text='<%# Bind("EMPType") %>'
                                                    Skin="Hay" Height="100px" Width="200px" AppendDataBoundItems="true" EmptyMessage="Select Employee Type" 
                                                    EnableLoadOnDemand="true">
                                                    <Items>
                                                        <%--  <telerik:RadComboBoxItem Text="Select Employee Type" Value="0" Font-Italic="true" />--%>
                                                        <telerik:RadComboBoxItem Text="Contract" Value="1" />
                                                        <telerik:RadComboBoxItem Text="Permanent" Value="2" />
                                                        <telerik:RadComboBoxItem Text="Probation" Value="3" />
                                                        <telerik:RadComboBoxItem Text="FreeLancer" Value="5" />
                                                        <telerik:RadComboBoxItem Text="Resign" Value="4" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                Designation:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtDesignation" MaxLength="150" ToolTip="Maximum Length: 50"
                                                    runat="server" Text='<%# Bind("Designation") %>'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Branch:
                                            </td>
                                            <td> <%--Text='<%# Eval("Location") %>'--%>
                                                <telerik:RadComboBox ID="cboBranch" runat="server" Height="200px" Width="200px" DropDownWidth="380px"
                                                    DataValueField="Dept_ID" OnItemsRequested="cboBranchId_OnItemsRequested" EnableLoadOnDemand="true"
                                                    AppendDataBoundItems="True"  EmptyMessage="Select Branch"
                                                    Skin="Hay">
                                                    <HeaderTemplate>
                                                        <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 250px;">
                                                                    Branch
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
                                            <td>
                                                Department Name:
                                            </td>
                                            <td> <%--Text='<%# Eval("Dept_Name") %>'--%>
                                                <telerik:RadComboBox ID="cboDeptId" runat="server" Height="200px" Width="200px" DropDownWidth="380px"
                                                    DataValueField="Dept_ID" OnItemsRequested="cboDeptId_OnItemsRequested" EnableLoadOnDemand="true"
                                                    AppendDataBoundItems="True"  EmptyMessage="Select Department"
                                                    Skin="Hay">
                                                    <HeaderTemplate>
                                                        <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 250px;">
                                                                    Department Name
                                                                </td>
                                                                <td style="width: 100px;">
                                                                    Department Code
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
                                                                    <%# DataBinder.Eval(Container, "Attributes['Dept_Code']")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Company:
                                            </td>
                                            <td> <%--Text='<%# Eval("Company_Name") %>'--%>
                                                <telerik:RadComboBox ID="cboCompany" runat="server" Height="200px" Width="200px"
                                                    DropDownWidth="360px" DataValueField="Dept_ID" OnItemsRequested="cboCompany_OnItemsRequested"
                                                    EnableLoadOnDemand="true" AppendDataBoundItems="True" 
                                                    EmptyMessage="Select Company" Skin="Hay">
                                                    <HeaderTemplate>
                                                        <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 250px;">
                                                                    Company Name
                                                                </td>
                                                                <td style="width: 100px;">
                                                                    Company Code
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
                                                                    <%# DataBinder.Eval(Container, "Attributes['Company_CODE']")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                Reporting to:
                                            </td>
                                            <td> <%--Text='<%# Eval("ReportingName") %>'--%>
                                                <telerik:RadComboBox ID="cboReporting" runat="server" Height="200px" Width="200px"
                                                    Skin="Hay" DropDownWidth="360px" DataValueField="Dept_ID" OnItemsRequested="cboSTAFFReporting_OnItemsRequested"
                                                    EnableLoadOnDemand="true" AppendDataBoundItems="True" 
                                                    EmptyMessage="Select Staff">
                                                    <HeaderTemplate>
                                                        <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 250px;">
                                                                    Staff Name
                                                                </td>
                                                                <td style="width: 100px;">
                                                                    Staff No
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
                                                                    <%# DataBinder.Eval(Container, "Attributes['Staff_No']")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                OGSP NO :
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtOGSP" MaxLength="50" ToolTip="Maximum Length: 50"
                                                    runat="server" Text='<%# Eval("OGSPNO") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                OGSP Validity
                                            </td>
                                            <td>
                                                <%-- DbSelectedDate='<%# Bind("OGSPVALIDITY") %>'  --%>
                                                <telerik:RadDatePicker ID="DtOGSP" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    DateInput-DateFormat="dd/MMM/yyyy" ToolTip='<%# Bind("OGSPVALIDITY") %>' MinDate="01/01/1900"
                                                    Skin="Hay">
                                                    <Calendar ID="Calendar2" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Medical Validity
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dtMedical" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    ToolTip='<%# Bind("MEDICALVALIDITY") %>' DateInput-DateFormat="dd/MMM/yyyy" MinDate="01/01/1900"
                                                    Skin="Hay">
                                                    <Calendar ID="Calendar3" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                H2S Cert Validity
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dtH2S" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    ToolTip='<%# Bind("H2SCERTVALIDITY") %>' DateInput-DateFormat="dd/MMM/yyyy" MinDate="01/01/1900"
                                                    Skin="Hay">
                                                    <Calendar ID="Calendar4" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                BOSIET Validity
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dtBOSIET" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    ToolTip='<%# Bind("BOSIETVALIDITY") %>' DateInput-DateFormat="dd/MMM/yyyy" MinDate="01/01/1900"
                                                    Skin="Hay">
                                                    <Calendar ID="Calendar5" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                Compentancy Card Validity
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dtCompentancy" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    ToolTip='<%# Bind("COMPENTANCYCARDVALIDITY") %>' DateInput-DateFormat="dd/MMM/yyyy"
                                                    MinDate="01/01/1900" Skin="Hay">
                                                    <Calendar ID="Calendar6" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                CIDB
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dtCIDB" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    ToolTip='<%# Bind("CIDB") %>' DateInput-DateFormat="dd/MMM/yyyy" MinDate="01/01/1900"
                                                    Skin="Hay">
                                                    <Calendar ID="Calendar7" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                Confine Space
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dtConfineSpace" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    ToolTip='<%# Bind("ConfineSpace") %>' DateInput-DateFormat="dd/MMM/yyyy" MinDate="01/01/1900"
                                                    Skin="Hay">
                                                    <Calendar ID="Calendar8" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Working Permit
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dtWorkPermit" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    ToolTip='<%# Bind("WorkPermit") %>' DateInput-DateFormat="dd/MMM/yyyy" MinDate="01/01/1900"
                                                    Skin="Hay">
                                                    <Calendar ID="Calendar9" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                Boarding Pass
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dtBordingPass" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    ToolTip='<%# Bind("BordingPass") %>' DateInput-DateFormat="dd/MMM/yyyy" MinDate="01/01/1900"
                                                    Skin="Hay">
                                                    <Calendar ID="Calendar10" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                PTW
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="dtPTW" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    ToolTip='<%# Bind("PTW") %>' DateInput-DateFormat="dd/MMM/yyyy" MinDate="01/01/1900"
                                                    Skin="Hay">
                                                    <Calendar ID="Calendar11" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                SHELL Passport No
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtSHELLPassport" MaxLength="50" ToolTip="Maximum Length: 50"
                                                    runat="server" Text='<%# Eval("SHELLPASSPORTVALIDITY") %>'></asp:TextBox>
                                                <%-- <telerik:RadDatePicker ID="dtSHELL" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                                DbSelectedDate='<%# Bind("SHELLPASSPORTVALIDITY") %>' DateInput-DateFormat="dd/MMM/yyyy"
                                                                MinDate="01/01/1900" Skin="Hay">
                                                                <Calendar ID="Calendar7" runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                                    </SpecialDays>
                                                                </Calendar>
                                                            </telerik:RadDatePicker>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Skill:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="300px" ID="txtSkill" Height="50px" MaxLength="200" TextMode="MultiLine"
                                                    ToolTip="Maximum Length: 200" runat="server" Text='<%# Eval("Skill") %>'></asp:TextBox>
                                                <%--                                                            <telerik:RadComboBox ID="cboSkill" runat="server" DropDownWidth="200px" Text='<%# Bind("Skill") %>'
                                                                Skin="Hay" Height="40px" Width="200px" AppendDataBoundItems="true" EmptyMessage="Select Skill Type"
                                                                EnableLoadOnDemand="true">
                                                                <Items>
                                                                    <%--  <telerik:RadComboBoxItem Text="Select Employee Type" Value="0" Font-Italic="true" />
                                                                    <telerik:RadComboBoxItem Text="Skilled" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="Semi Skilled" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="Button1" runat="server" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                    CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                </asp:Button>
                                                <asp:Button ID="Button2" runat="server" Text="Cancel" CausesValidation="false" CommandName="Cancel">
                                                </asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </FormTemplate>
                            </EditFormSettings>
                        </MasterTableView>
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
