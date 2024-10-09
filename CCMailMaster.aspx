<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CCMailMaster.aspx.cs" Inherits="CCMailMaster" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2011.2.1100.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a"
    Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CC Mail Master</title>
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
<body>
    <%--onunload="HandleClose()"  style="background-color: #eeeeee; width: 1024px; margin: 10px 0px 0px 0px;"--%>
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
                    CC Mail Master
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
                <table>
                    <tr>
                        <td align="center">
                            <telerik:RadComboBox ID="cboGroup" runat="server" Height="120px" Width="400px" Visible="true"
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
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br />
                            <telerik:RadTextBox Text="" ID="txtCCMailList" Height="300px" Width="400px" TextMode="MultiLine"
                                BackColor="White" Font-Size="12px" CssClass="textboxstyle_new" runat="server"
                                Font-Bold="false" Visible="true" Enabled="true" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td valign="bottom" align="center">
                            <br />
                            <asp:Button ID="btnSave" runat="server" Text=" Update " Font-Names="Verdana" Font-Size="Small"
                                BackColor="Black" ForeColor="White" OnClick="btnSave_click" Width="400px" Height="30px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
