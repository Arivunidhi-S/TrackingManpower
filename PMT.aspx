<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMT.aspx.cs" Inherits="PMT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PMT Master</title>
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
                        PMT MASTER
                    </div>
                    <div style="height: 20px; text-align: center">
                        <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                    </div>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                        GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                        OnDeleteCommand="RadGrid1_DeleteCommand" OnItemCreated="RadGrid1_ItemCreated"
                        Skin="Hay" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                        AllowAutomaticDeletes="true" OnInsertCommand="RadGrid1_InsertCommand" AllowSorting="true"
                        AllowFilteringByColumn="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="PMT_ID" CommandItemDisplay="Top"
                            CommandItemSettings-AddNewRecordText="Add New PMT Details">
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="PMT_ID" DataType="System.Int64" HeaderText="ID"
                                    ReadOnly="True" SortExpression="PMT_ID" UniqueName="PMT_ID" AllowFiltering="false"
                                    AllowSorting="false" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Company_Name" DataType="System.String" HeaderText="COMPANY NAME"
                                    SortExpression="Company_Name" UniqueName="Company_Name" FilterControlWidth="200px"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" ShowFilterIcon="false"
                                    FilterDelay="2000">
                                    <HeaderStyle Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="STAFF_NAME" DataType="System.String" HeaderText="STAFF NAME "
                                    SortExpression="STAFF_NAME" UniqueName="STAFF_NAME" FilterControlWidth="200px"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" ShowFilterIcon="false"
                                    FilterDelay="2000">
                                    <HeaderStyle Width="15%" />
                                </telerik:GridBoundColumn>
                                <%-- <telerik:GridBoundColumn DataField="CONTACT_NO" DataType="System.Int32" HeaderText="CONTACT NO"
                                                SortExpression="CONTACT_NO" UniqueName="CONTACT_NO" Visible="true">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EMAIL_ID" DataType="System.String" HeaderText="EMAIL"
                                                SortExpression="EMAIL_ID" UniqueName="EMAIL_ID">
                                                <HeaderStyle Width="10%" />
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
                                            <td colspan="2">
                                                <b>ID:
                                                    <%--  <%# Eval("Staff_Id")%>--%>
                                                    <asp:Label ID="lblcontID" Visible="true" runat="server" Width="20px" Text='<%# Eval("PMT_ID")%>' />
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Company Name:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cboCompany" runat="server" Height="300px" Width="360px"
                                                    Skin="Hay" AutoPostBack="true" DataValueField="Category_ID" OnItemsRequested="cboCompany_OnItemsRequested"
                                                    EnableLoadOnDemand="true" AppendDataBoundItems="True" EmptyMessage="Select Company">
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
                                                                <td style="width: 100px;" align="center">
                                                                    <%# DataBinder.Eval(Container, "Attributes['Company_CODE']")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                Project Manager:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cboProjectManager" runat="server" Height="200px" Width="200px"
                                                    DropDownWidth="360px" DataValueField="Dept_ID" Skin="Hay" EnableLoadOnDemand="true"
                                                    AppendDataBoundItems="True" EmptyMessage="Select Project Manager" OnItemsRequested="cboProjectManager_OnItemsRequested">
                                                    <HeaderTemplate>
                                                        <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 250px;">
                                                                    Staff Name
                                                                </td>
                                                                <td style="width: 100px;" align="left">
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
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
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
