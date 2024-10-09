<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Master_ProjectPo.aspx.cs"
    Inherits="Master_ProjectPo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Project Po</title>
    <link rel="shortcut icon" href="images/serbatitle.png" />
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="css/styles.css" />
    <%--   <script src="css/jquery-latest.min.js" type="text/javascript"></script>--%>
    <style type="text/css">
        .search
        {
            background: url(images/Reject.png) no-repeat;
            background-position: center;
        }
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
                </div>
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
                                <telerik:AjaxUpdatedControl ControlID="cboProjectManager" />
                                <telerik:AjaxUpdatedControl ControlID="cboBankCurrency" />
                                <telerik:AjaxUpdatedControl ControlID="cboInsuranceCurrency" />
                                <telerik:AjaxUpdatedControl ControlID="cboStaffame" />
                                <telerik:AjaxUpdatedControl ControlID="cboTenderCurrency" />
                                <telerik:AjaxUpdatedControl ControlID="upProjectFile" />
                                <telerik:AjaxUpdatedControl ControlID="lblFileName" />
                                <telerik:AjaxUpdatedControl ControlID="cboProject" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="btnclikrjt">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="btnclikrjt" />
                                <telerik:AjaxUpdatedControl ControlID="lnkDownload" />
                                <telerik:AjaxUpdatedControl ControlID="upProjectFile" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="RadGrid2">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="RadGrid2" />
                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="cboProject">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                <telerik:AjaxUpdatedControl ControlID="RadGrid2" />
                                <telerik:AjaxUpdatedControl ControlID="cboProject" />
                                <telerik:AjaxUpdatedControl ControlID="txtCustomerName" />
                                <telerik:AjaxUpdatedControl ControlID="cboContact1" />
                                <telerik:AjaxUpdatedControl ControlID="cboContact2" />
                                <telerik:AjaxUpdatedControl ControlID="cboContact3" />
                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                <telerik:AjaxUpdatedControl ControlID="upProjectFile" />
                                <telerik:AjaxUpdatedControl ControlID="lnkDownload" />
                                <telerik:AjaxUpdatedControl ControlID="btnclikrjt" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="cboContact1">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="upProjectFile">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="upProjectFile" />
                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                    <script type="text/javascript">
                        function ShowEditForm(id, CustName) {
                            window.radopen("Master_Project_Contact.aspx?Cust_ID=" + id + "&CustName=" + CustName, "UserListDialog");
                            return false;
                        }
                        function RowDblClick(sender, eventArgs) {
                            var _Status = eventArgs.get_item().get_cell("status").innerHTML;
                            window.radopen("Master_Project_Contact.aspx?ContratQuotationDetailID=" + eventArgs.getDataKeyValue("ContratQuotationDetailID") + "&Status=" + _Status, "UserListDialog");
                        }
                        function refreshGrid(arg) {
                            //                if (!arg) {
                            //                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
                            //                }
                            //                else {
                            //                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");
                            //                }
                            var masterTable = $find("<%=RadGrid1.ClientID%>").get_masterTableView();
                            masterTable.rebind();
                        }
                        function OnClientPageLoad(sender, eventArgs) {
                            setTimeout(function () { sender.set_status(""); }, 0);
                        }
                    </script>
                </telerik:RadCodeBlock>
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                <!-- content start -->
                <div id="Div1" runat="server">
                    <telerik:RadInputManager ID="RadInputManager1" runat="server">
                        <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                            Validation-IsRequired="true">
                        </telerik:TextBoxSetting>
                    </telerik:RadInputManager>
                    <div id="DivHeader" runat="server" class="otto">
                        PROJECT PO DETAILS
                    </div>
                    <div style="height: 20px; text-align: center">
                        <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                        <asp:Label class="labelstyle" ID="lblFileparameter" runat="server" ForeColor="Red"
                            Font-Bold="true" Visible="true" />
                    </div>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                        GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                        OnDeleteCommand="RadGrid1_DeleteCommand" AllowAutomaticUpdates="false" AllowAutomaticInserts="false"
                        PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="Hay" AllowAutomaticDeletes="false"
                        OnInsertCommand="RadGrid1_InsertCommand" AllowSorting="true" AllowFilteringByColumn="true"
                        OnUpdateCommand="RadGrid1_UpdateCommand" OnItemDataBound="RadGrid1_ItemDataBound">
                        <ClientSettings EnableRowHoverStyle="true">
                            <%-- <Selecting AllowRowSelect="false" />--%>
                        </ClientSettings>
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="ProjectPo_ID" CommandItemDisplay="Top"
                            CommandItemSettings-AddNewRecordText="Add New Project Po Details">
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="2%" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="ProjectPo_ID" DataType="System.Int64" HeaderText="ID"
                                    ReadOnly="True" SortExpression="ProjectPo_ID" UniqueName="ProjectPo_ID" AllowFiltering="false"
                                    AllowSorting="false" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Project_No" DataType="System.String" HeaderText="Project No"
                                    SortExpression="Project_No" UniqueName="Project_No" Visible="true">
                                    <HeaderStyle Width="7%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Project_Name" DataType="System.String" HeaderText="Project Name"
                                    SortExpression="Project_Name" UniqueName="Project_Name">
                                    <HeaderStyle Width="15%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SupplyScope" DataType="System.String" HeaderText="Supply Scope"
                                    AllowFiltering="false" SortExpression="SupplyScope" UniqueName="SupplyScope">
                                    <HeaderStyle Width="7%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="StartDate" DataType="System.DateTime" HeaderText="Start Date"
                                    AllowFiltering="false" SortExpression="StartDate" UniqueName="StartDate" DataFormatString="{0:dd/MMM/yyyy}">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EndDate" DataType="System.DateTime" HeaderText="End Date"
                                    AllowFiltering="false" SortExpression="EndDate" UniqueName="EndDate" DataFormatString="{0:dd/MMM/yyyy}">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TenderValue" DataType="System.String" HeaderText="TenderValue"
                                    AllowFiltering="false" SortExpression="TenderValue" UniqueName="TenderValue">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ProjectAwardDate" DataType="System.DateTime"
                                    HeaderText="ProjectAwardDate" AllowFiltering="false" SortExpression="ProjectAwardDate"
                                    UniqueName="ProjectAwardDate" DataFormatString="{0:dd/MMM/yyyy}">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="STAFF_NAME" DataType="System.String" HeaderText="Project Manager"
                                    AllowFiltering="false" SortExpression="STAFF_NAME" UniqueName="STAFF_NAME">
                                    <HeaderStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CUSTOMER_NAME" DataType="System.String" HeaderText="Customer Name"
                                    AllowFiltering="false" SortExpression="CUSTOMER_NAME" UniqueName="CUSTOMER_NAME">
                                    <HeaderStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="Location" DataType="System.String" HeaderText="Branch"
                                                SortExpression="Location" UniqueName="Location">
                                                <HeaderStyle Width="5%" />
                                            </telerik:GridBoundColumn>
                                <%-- <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" AllowFiltering="false">
                                                                <HeaderStyle Width="6%" />
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="EditLink" runat="server" Text="Add Contact"></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>--%>
                                <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                    ConfirmText="Are you sure want to delete?">
                                    <HeaderStyle Width="2%" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings EditFormType="Template">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                                <FormTemplate>
                                    <table cellspacing="2" cellpadding="1" width="100%" border="0">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblProjectID" Visible="false" runat="server" Width="20px" Text='<%# Eval("ProjectPo_ID")%>' />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%; text-align: left">
                                                         Project:
                                                        </td>
                                                        <td style="width: 25%; text-align: left;">
                                                           <telerik:RadComboBox ID="cboProject" runat="server" Height="300px" Width="200px"
                    DropDownWidth="600px" EmptyMessage="Select Project Name" EnableLoadOnDemand="true" Text='<%# Eval("Project_Name") %>'
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
                                <td style="width: 250px; border: thin solid #000000;">
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                                <td style="width: 150px; border: thin solid #000000;">
                                    <%# DataBinder.Eval(Container, "Attributes['Project_No']")%>
                                </td>
                                <td style="width: 200px; border: thin solid #000000;">
                                    <%# DataBinder.Eval(Container, "Attributes['STAFF_NAME']")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
                                                        </td>
                                            <td>
                                             <%--   Project No:--%>
                                            </td>
                                            <td>
                                              <%--  <asp:Label ID="lblExistingname" runat="server" Text='<%# Eval("Project_No") %>' Visible="false" />
                                                <asp:TextBox Width="200px" ID="txtProjectNo" MaxLength="150" ToolTip="Maximum Length: 50"
                                                    runat="server" Text='<%# Bind("Project_No") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtProjectNo"
                                                    ErrorMessage="Project No is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Start Date:
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="DtStart" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    DbSelectedDate='<%# Bind("StartDate") %>' DateInput-DateFormat="dd/MMM/yyyy"
                                                    Culture="en-US" Skin="Hay" MinDate="01/01/1900">
                                                    <Calendar ID="Calendar1" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                End Date:
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="DtEnd" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    Culture="en-US" Skin="Hay" DbSelectedDate='<%# Bind("EndDate") %>' DateInput-DateFormat="dd/MMM/yyyy"
                                                    MinDate="01/01/1900">
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
                                                Tender Value:
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadNumericTextBox Width="200px" MinValue="0" MaxValue="9999999999999999999999999"
                                                                DbValue='<%# Bind("TenderValue") %>' MaxLength="10" ToolTip="Maximum Length: 20"
                                                                BackColor="#f4f793" ID="txtTenderValue" runat="server">
                                                                <NumberFormat GroupSeparator="" DecimalDigits="2" />
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboTenderCurrency" runat="server" DropDownWidth="175px"
                                                                Skin="Hay" Text='<%# Bind("TenderCurrency") %>' Height="300px" Width="150px"
                                                                AppendDataBoundItems="true" EmptyMessage="Select Currency Type" EnableLoadOnDemand="true">
                                                                <Items>
                                                                    <%--  <telerik:RadComboBoxItem Text="Select Employee Type" Value="0" Font-Italic="true" />--%>
                                                                    <telerik:RadComboBoxItem Text="MYR-Malaysian Ringgit" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="CAD-Canadian Dollar" Value="2" />
                                                                    <telerik:RadComboBoxItem Text="CNY-Chinese Yuan" Value="3" />
                                                                    <telerik:RadComboBoxItem Text="EUR-Euro" Value="4" />
                                                                    <telerik:RadComboBoxItem Text="INR-Indian Rupee" Value="5" />
                                                                    <telerik:RadComboBoxItem Text="JPY-Japanese Yen" Value="6" />
                                                                    <telerik:RadComboBoxItem Text="AUD-Australian Dollar" Value="7" />
                                                                    <telerik:RadComboBoxItem Text="MXN-Mexican Peso" Value="8" />
                                                                    <telerik:RadComboBoxItem Text="NZD-New Zealand Dollar" Value="9" />
                                                                    <telerik:RadComboBoxItem Text="NOK-Norwegian Krone" Value="10" />
                                                                    <telerik:RadComboBoxItem Text="PHP-Philippine Peso" Value="11" />
                                                                    <telerik:RadComboBoxItem Text="PLN-Polish Zloty" Value="12" />
                                                                    <telerik:RadComboBoxItem Text="GBP-Pound Sterling" Value="13" />
                                                                    <telerik:RadComboBoxItem Text="SGD-Singapore Dollar" Value="14" />
                                                                    <telerik:RadComboBoxItem Text="ZAR-South African Rand" Value="15" />
                                                                    <telerik:RadComboBoxItem Text="SEK-Swedish Krona" Value="16" />
                                                                    <telerik:RadComboBoxItem Text="CHF-Swish Franc" Value="17" />
                                                                    <telerik:RadComboBoxItem Text="THB-Thai Bhat" Value="18" />
                                                                    <telerik:RadComboBoxItem Text="AED-United Arab Emirates Dirham" Value="19" />
                                                                    <telerik:RadComboBoxItem Text="USD-United States Dollar" Value="20" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                Supply Scope:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cboSupplyScope" runat="server" DropDownWidth="200px" Text='<%# Bind("SupplyScope") %>'
                                                    Height="40px" Width="200px" AppendDataBoundItems="true" EmptyMessage="Select Supply Scope"
                                                    Skin="Hay" EnableLoadOnDemand="true">
                                                    <Items>
                                                        <%--  <telerik:RadComboBoxItem Text="Select Employee Type" Value="0" Font-Italic="true" />--%>
                                                        <telerik:RadComboBoxItem Text="LumpSum" Value="1" />
                                                        <telerik:RadComboBoxItem Text="Other" Value="2" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <asp:TextBox Width="200px" ID="txtSupplyScope" MaxLength="100" ToolTip="Maximum Length: 100"
                                                    runat="server" Text='<%# Bind("SupplyScope_Others") %>'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Project Award Date:
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="DtProjectAwardDate" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    Culture="en-US" DbSelectedDate='<%# Bind("ProjectAwardDate") %>' DateInput-DateFormat="dd/MMM/yyyy"
                                                    MinDate="01/01/1900" Skin="Hay">
                                                    <Calendar ID="Calendar3" runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                Project Manager :
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cboProjectManager" runat="server" Height="200px" Width="200px"
                                                    Skin="Hay" DropDownWidth="360px" DataValueField="Dept_ID" OnItemsRequested="cboStaffId_OnItemsRequested"
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Customer:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cboCustomer" runat="server" Height="200px" Width="200px"
                                                    Skin="Hay" DropDownWidth="310px" DataValueField="Dept_ID" OnItemsRequested="CboCustomer_OnItemsRequested"
                                                    EnableLoadOnDemand="true" AppendDataBoundItems="True" Text='<%# Eval("CUSTOMER_NAME") %>'
                                                    EmptyMessage="Select Customer">
                                                    <HeaderTemplate>
                                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 200px;">
                                                                    Customer Name
                                                                </td>
                                                                <td style="width: 100px;">
                                                                    Customer Code
                                                                </td>
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
                                                                    <%# DataBinder.Eval(Container, "Attributes['CUSTOMER_CODE']")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                Branch:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cboBranch" runat="server" Height="200px" Width="200px" DropDownWidth="380px"
                                                    DataValueField="Dept_ID" OnItemsRequested="cboBranchId_OnItemsRequested" EnableLoadOnDemand="true"
                                                    AppendDataBoundItems="True"  EmptyMessage="Select Branch" Text='<%# Eval("Location") %>'
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
                                        </tr>
                                        <tr>
                                            <td>
                                                Description:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtDescription" MaxLength="100" ToolTip="Maximum Length: 100"
                                                    TextMode="MultiLine" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
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
