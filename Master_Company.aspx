<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Master_Company.aspx.cs" Inherits="Master_Company" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Company Master</title>
    <link rel="shortcut icon" href="images/serbatitle.png" />
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="css/styles.css" />
    <script src="css/jquery-latest.min.js" type="text/javascript"></script>
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
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="txtCountry">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                <telerik:AjaxUpdatedControl ControlID="cboBranch" />
                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
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
                        COMPANY MASTER DETAILS
                    </div>
                    <div style="height: 20px; text-align: center">
                        <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                    </div>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                        GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                        OnDeleteCommand="RadGrid1_DeleteCommand" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                        OnItemDataBound="RadGrid1_ItemDataBound" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                        Skin="Hay" AllowAutomaticDeletes="true" OnInsertCommand="RadGrid1_InsertCommand"
                        OnItemCreated="RadGrid1_ItemCreated" AllowSorting="true" AllowFilteringByColumn="true"
                        OnUpdateCommand="RadGrid1_UpdateCommand">
                        <GroupingSettings CaseSensitive="false" />
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="Company_ID" CommandItemDisplay="Top"
                            CommandItemSettings-AddNewRecordText="Add New Company Details">
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="Company_ID" DataType="System.Int64" HeaderText="ID"
                                    ReadOnly="True" SortExpression="Company_ID" UniqueName="Company_ID" AllowFiltering="false"
                                    AllowSorting="false" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Company_CODE" DataType="System.String" HeaderText="Company Code"
                                    SortExpression="Company_CODE" UniqueName="Company_CODE" Visible="true" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="false" ShowFilterIcon="false" FilterDelay="2000">
                                    <HeaderStyle Width="7%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Company_Name" DataType="System.String" HeaderText="Company Name"
                                    SortExpression="Company_Name" UniqueName="Company_Name" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="false" ShowFilterIcon="false" FilterDelay="2000">
                                    <HeaderStyle Width="17%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Addr_Line1" DataType="System.String" HeaderText="Adds Line1"
                                    AllowFiltering="false" SortExpression="Addr_Line1" UniqueName="Addr_Line1">
                                    <HeaderStyle Width="15%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Addr_Line2" DataType="System.String" HeaderText="Adds Line2"
                                    AllowFiltering="false" SortExpression="Addr_Line2" UniqueName="Addr_Line2">
                                    <HeaderStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="State" DataType="System.String" HeaderText="State"
                                    AllowFiltering="false" SortExpression="State" UniqueName="State">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Country" DataType="System.String" HeaderText="Country"
                                    AllowFiltering="false" SortExpression="Country" UniqueName="Country">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Contact_No" DataType="System.String" HeaderText="Contact No"
                                    AllowFiltering="true" SortExpression="Contact_No" UniqueName="Contact_No" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="false" ShowFilterIcon="false" FilterDelay="2000">
                                    <HeaderStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fax_No" DataType="System.String" HeaderText="Fax No"
                                    AllowFiltering="false" SortExpression="Fax_No" UniqueName="Fax_No">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Email" DataType="System.String" HeaderText="Email"
                                    AllowFiltering="false" SortExpression="Email" UniqueName="Email">
                                    <HeaderStyle Width="12%" />
                                </telerik:GridBoundColumn>
                                <%-- <telerik:GridBoundColumn DataField="Contract_No" DataType="System.String" HeaderText="Contract No"
                                                SortExpression="Contract_No" UniqueName="Contract_No">
                                                <HeaderStyle Width="15%" />
                                            </telerik:GridBoundColumn>--%>
                                <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                    ConfirmText="Are you sure want to delete?">
                                    <HeaderStyle Width="5%" />
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
                                                <asp:Label ID="lblCustomerID" Visible="false" runat="server" Width="20px" Text='<%# Eval("Company_ID")%>' />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Company Name:
                                                <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red" Visible="true" />
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtCompanyName" MaxLength="100" ToolTip="Maximum Length: 100"
                                                    runat="server" Text='<%# Bind("Company_Name") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="BranchNameValidator1" ControlToValidate="txtCompanyName"
                                                    ErrorMessage="Company Name is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                Company Code:
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExistingname" runat="server" Text='<%# Eval("Company_CODE") %>'
                                                    Visible="false" />
                                                <asp:TextBox Width="200px" ID="txtCompanycode" MaxLength="150" ToolTip="Maximum Length: 50"
                                                    runat="server" Text='<%# Bind("Company_CODE") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtCompanycode"
                                                    ErrorMessage="Company Code is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Roc No:
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("REG_ROC_NO") %>' Visible="false" />
                                                <asp:TextBox Width="200px" ID="txtRocNo" MaxLength="150" ToolTip="Maximum Length: 50"
                                                    runat="server" Text='<%# Bind("REG_ROC_NO") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                Roc Reg Date :
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="DtRocregdate" runat="server" Width="150px" DateInput-EmptyMessage="Date"
                                                    Skin="Hay" DbSelectedDate='<%# Bind("REG_DATE") %>' DateInput-DateFormat="dd/MMM/yyyy"
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
                                                Address 1:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtAddress1" MaxLength="200" ToolTip="Maximum Length: 200"
                                                    runat="server" Text='<%# Bind("Addr_Line1") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                Address 2:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtAddress2" MaxLength="200" ToolTip="Maximum Length: 200"
                                                    runat="server" Text='<%# Bind("Addr_Line2") %>'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Address 3:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtAddress3" MaxLength="200" ToolTip="Maximum Length: 200"
                                                    runat="server" Text='<%# Bind("Addr_Line3") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                City:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtCity" MaxLength="200" ToolTip="Maximum Length: 200"
                                                    runat="server" Text='<%# Bind("city") %>'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                State:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtState" MaxLength="50" ToolTip="Maximum Length: 50"
                                                    runat="server" Text='<%# Bind("State") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                Country:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="txtCountry" runat="server" Height="200px" Width="200px"
                                                    Skin="Hay" AutoPostBack="true" DataValueField="Country_Code" OnItemsRequested="txtState_OnItemsRequested"
                                                    EnableLoadOnDemand="true" AppendDataBoundItems="True" EmptyMessage="Select Country">
                                                    <HeaderTemplate>
                                                        <table style="width: 200px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 160px;">
                                                                    Country Name
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 200px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 160px;" align="left">
                                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Postal Code :
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox Width="200px" MinValue="0" MaxValue="9999999999999999999999999"
                                                    DbValue='<%# Bind("Postal_Code") %>' MaxLength="10" ToolTip="Maximum Length: 20"
                                                    ID="txtPostalcode" runat="server">
                                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox Width="200px" MinValue="0" MaxValue="9999999999999999999999999"
                                                    DbValue='<%# Bind("Contact_no") %>' Visible="false" MaxLength="10" ToolTip="Maximum Length: 20"
                                                    ID="txtPhone" runat="server">
                                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Fax:
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox Width="200px" MinValue="0" MaxValue="9999999999999999999999999"
                                                    DbValue='<%# Bind("Fax_no") %>' MaxLength="10" ToolTip="Maximum Length: 20" ID="txtFax"
                                                    runat="server">
                                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                Website:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtWebsite" MaxLength="100" ToolTip="Maximum Length: 100"
                                                    runat="server" Text='<%# Bind("website") %>'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Email:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtEmail" MaxLength="50" ToolTip="Maximum Length: 50"
                                                    runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtEmail"
                                                    ErrorMessage="Email is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ForeColor="Red">
                                                </asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                <%--Branch:--%>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cboBranch" runat="server" Height="200px" Width="200px" AutoPostBack="true"
                                                    EmptyMessage="Select Branch" DataValueField="Country_Code" EnableLoadOnDemand="true"
                                                    AppendDataBoundItems="True" DropDownWidth="300px" Skin="Hay" Visible="false">
                                                    <HeaderTemplate>
                                                        <table cellspacing="0" cellpadding="0" style="width: 300px">
                                                            <tr>
                                                                <td style="width: 160px;">
                                                                    Country Name
                                                                </td>
                                                                <td style="width: 140px;">
                                                                    Location Code
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 160px;" align="left">
                                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                                </td>
                                                                <td style="width: 140px;" align="left">
                                                                    <%# DataBinder.Eval(Container, "Attributes['Country_Code']")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
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
