<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Master_Project_Contact.aspx.cs"
    Inherits="Master_Project_Contact" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Project Contact Master</title>
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
    <form id="form2" runat="server">
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                border-bottom: blue thin solid; border-width: 0px" align="center">
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
                                    CONTACT MASTER
                                </div>
                                <div style="height: 20px; text-align: center">
                                    <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                                </div>
                                <div id="Div2" runat="server" style="font-size: large; color: Blue; font-weight: bold;
                                    width: 1300px; height: 300px; overflow: auto; background-image: url(Images/main.jpg);
                                    background-repeat: repeat; margin: auto; text-align: center">
                                    <table width="1300px" border="1" cellpadding="3" cellspacing="3" style="border-bottom-color: transparent;
                                        border-left-color: transparent; border-right-color: transparent; border-right-color: transparent">
                                        <table border="1" cellspacing="2" cellpadding="2" style="width: 1070px;">
                                            <tr>
                                                <td align="left" style="width: 10%">
                                                    <asp:Label ID="Label3" runat="server" CssClass="labelstyle_1" Text="Customer : " />
                                                </td>
                                                <td align="left" style="width: 90%">
                                                    <asp:TextBox runat="server" Width="280px" CssClass="labelstyle_1" BackColor="#fceed1"
                                                        Enabled="false" ID="lblCust" BorderStyle="None" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%; text-align: right">
                                                    <asp:Label ID="Label4" Text="Contact Person Name 1:" Font-Size="Small" runat="server"
                                                        Style="color: Black" />
                                                </td>
                                                <td style="width: 90%; text-align: left">
                                                    <telerik:RadComboBox ID="CboCustomerContact1" runat="server" Height="300px" Width="250px"
                                                        DropDownWidth="440px" EnableLoadOnDemand="true" AutoPostBack="true" AppendDataBoundItems="True"
                                                        Visible="true" OnItemsRequested="cboCustomerContact1_OnItemsRequested">
                                                        <HeaderTemplate>
                                                            <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 300px;">
                                                                        Customer Name
                                                                    </td>
                                                                    <td style="width: 100px;">
                                                                        Contact No.
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 300px;" align="left">
                                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                                    </td>
                                                                    <td style="width: 100px;" align="left">
                                                                        <%# DataBinder.Eval(Container, "Attributes['CONTACT_NO']")%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                    <%--<asp:Button runat="server" ID="btncliksrch" Text="search" Font-Size="9px" ForeColor="Red"
                                                    AutoPostBack="true" Width="40px" Height="22px" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%; text-align: right">
                                                    <asp:Label ID="Label1" Text="Contact Person Name 2:" Font-Size="Small" runat="server"
                                                        Style="color: Black" />
                                                </td>
                                                <td style="width: 90%; text-align: left">
                                                    <telerik:RadComboBox ID="CboCustomerContact2" runat="server" Height="300px" Width="250px"
                                                        DropDownWidth="440px" EnableLoadOnDemand="true" AutoPostBack="true" AppendDataBoundItems="True"
                                                        Visible="true" OnItemsRequested="cboCustomerContact2_OnItemsRequested">
                                                        <HeaderTemplate>
                                                            <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 300px;">
                                                                        Customer Name
                                                                    </td>
                                                                    <td style="width: 100px;">
                                                                        Contact No.
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 300px;" align="left">
                                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                                    </td>
                                                                    <td style="width: 100px;" align="left">
                                                                        <%# DataBinder.Eval(Container, "Attributes['CONTACT_NO']")%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%; text-align: right">
                                                    <asp:Label ID="Label2" Text="Contact Person Name 3:" Font-Size="Small" runat="server"
                                                        Style="color: Black" />
                                                </td>
                                                <td style="width: 90%; text-align: left">
                                                    <telerik:RadComboBox ID="CboCustomerContact3" runat="server" Height="300px" Width="250px"
                                                        DropDownWidth="440px" EnableLoadOnDemand="true" AutoPostBack="true" AppendDataBoundItems="True"
                                                        Visible="true" OnItemsRequested="cboCustomerContact3_OnItemsRequested">
                                                        <HeaderTemplate>
                                                            <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 300px;">
                                                                        Customer Name
                                                                    </td>
                                                                    <td style="width: 100px;">
                                                                        Contact No.
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table style="width: 400px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 300px;" align="left">
                                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                                    </td>
                                                                    <td style="width: 100px;" align="left">
                                                                        <%# DataBinder.Eval(Container, "Attributes['CONTACT_NO']")%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
