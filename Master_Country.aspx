<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Master_Country.aspx.cs" Inherits="Master_Country" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customer Master</title>
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
                        LOCATION MASTER
                    </div>
                    <div style="height: 20px; text-align: center">
                        <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                    </div>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                        GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                        OnDeleteCommand="RadGrid1_DeleteCommand" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                        Skin="Hay" PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowAutomaticDeletes="true"
                        OnInsertCommand="RadGrid1_InsertCommand" AllowSorting="true" AllowFilteringByColumn="true"
                        OnUpdateCommand="RadGrid1_UpdateCommand">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                         <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="COUNTRY_ID" CommandItemDisplay="Top"
                            CommandItemSettings-AddNewRecordText="Add New Location Details">
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="COUNTRY_ID" DataType="System.Int64" HeaderText="ID"
                                    ReadOnly="True" SortExpression="COUNTRY_ID" UniqueName="COUNTRY_ID" AllowFiltering="false"
                                    AllowSorting="false" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COUNTRY_Name" DataType="System.String" HeaderText="Country Name"
                                    SortExpression="COUNTRY_Name" UniqueName="COUNTRY_Name" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="false" ShowFilterIcon="false" FilterDelay="2000" FilterControlWidth="200px">
                                    <HeaderStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Country_Code" DataType="System.String" HeaderText="Location Code"
                                    SortExpression="Country_Code" UniqueName="Country_Code" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="false" ShowFilterIcon="false" FilterDelay="2000" FilterControlWidth="200px">
                                    <HeaderStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Location" DataType="System.String" HeaderText="Location"
                                    SortExpression="Location" UniqueName="Location" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="false" ShowFilterIcon="false" FilterDelay="2000" FilterControlWidth="200px">
                                    <HeaderStyle Width="10%" />
                                </telerik:GridBoundColumn>
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
                                            <td colspan="2">
                                                <b>ID:
                                                    <%--  <%# Eval("Staff_Id")%>--%>
                                                    <asp:Label ID="lblCatgryID" Visible="true" runat="server" Width="20px" Text='<%# Eval("Country_ID")%>' />
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Location
                                                <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red" Visible="true" />
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtlocation" MaxLength="50" ToolTip="Maximum Length: 50"
                                                    runat="server" Text='<%# Bind("Location") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtlocation"
                                                    ErrorMessage="Location is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                Location Code:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200px" ID="txtRemarks" MaxLength="500" ToolTip="Maximum Length: 500"
                                                    TextMode="SingleLine" runat="server" Text='<%# Bind("Country_Code") %>'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Country Name:
                                                <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red" Visible="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExistingname" runat="server" Text='<%# Eval("COUNTRY_Name") %>'
                                                    Visible="false" />
                                                <telerik:RadComboBox ID="txtcountryname" runat="server" DropDownWidth="200px" Text='<%# Bind("COUNTRY_Name") %>'
                                                    Skin="Hay" Height="160px" Width="200px" AppendDataBoundItems="true" EmptyMessage="Select Country"
                                                    EnableLoadOnDemand="true">
                                                    <Items>
                                                        <%--  <telerik:RadComboBoxItem Text="Select Employee Type" Value="0" Font-Italic="true" />--%>
                                                        <telerik:RadComboBoxItem Text="Malaysia" Value="1" />
                                                        <telerik:RadComboBoxItem Text="Indonesia" Value="2" />
                                                        <telerik:RadComboBoxItem Text="United Kingdom" Value="3" />
                                                        <telerik:RadComboBoxItem Text="Bahrain" Value="4" />
                                                        <telerik:RadComboBoxItem Text="Oman" Value="5" />
                                                        <telerik:RadComboBoxItem Text="UAE" Value="6" />
                                                        <telerik:RadComboBoxItem Text="Quatar" Value="7" />
                                                        <telerik:RadComboBoxItem Text="Saudi Arabia" Value="8" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <%-- <asp:TextBox Width="200px" ID="txtcountryname" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("COUNTRY_Name") %>'></asp:TextBox>--%>
                                                <asp:RequiredFieldValidator runat="server" ID="DeptcodeValidator" ControlToValidate="txtcountryname"
                                                    ErrorMessage="Country Name is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
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
