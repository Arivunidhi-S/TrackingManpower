﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StaffHistoryReport.aspx.cs"
    Inherits="StaffHistoryReport" %>

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
            margin:0;
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
                               Staff History Report
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
                            <asp:Label ID="lblProject" runat="server" Font-Bold="true" Text="Staff Name :"></asp:Label>
                            <telerik:RadComboBox ID="cboStaffName" runat="server" Height="300px" Width="200px"
                                DropDownWidth="300px" EmptyMessage="Select Staff Name" EnableLoadOnDemand="true"
                                AutoPostBack="true" AppendDataBoundItems="True" Visible="true" OnItemsRequested="cboStaffName_OnItemsRequested">
                                <HeaderTemplate>
                                    <table style="width: 250px" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 250px;">
                                                Staff Name
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="width: 250px" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 250px; border: thin solid #000000;">
                                                <%# DataBinder.Eval(Container, "Text")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                         &nbsp; &nbsp;
                            <asp:Label class="labelstyle" ID="Label3" runat="server" Font-Bold="true" Text="From Date :" ForeColor="Black"
                                Font-Size="Small" Visible="true" />
                            <telerik:RadDatePicker ID="txtFromDate" runat="server" AutoPostBack="true" DateInput-EmptyMessage="MinDate"
                                Width="120px" MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy"
                                Visible="true">
                                <Calendar ID="Calendar2" runat="server">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                         &nbsp; &nbsp;
                            <asp:Label class="labelstyle" ID="Label4" runat="server" Font-Bold="true" Text=" To Date :" ForeColor="Black"
                                Font-Size="Small" Visible="true" />
                            <telerik:RadDatePicker ID="txtToDate" runat="server" AutoPostBack="true" Width="120px"
                                MinDate="01/01/1000" MaxDate="01/01/3000" DateInput-DateFormat="dd/MMM/yyyy"
                                Visible="true">
                                <Calendar ID="Calendar1" runat="server">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                        &nbsp; &nbsp;
                            <asp:Button ID="btn_Report_Submit" runat="server" Text="  Submit  " BackColor="Black"
                                ForeColor="White" OnClick="Onclick_btnSubmit" />
                                 &nbsp; <asp:Button ID="btn_Clear" runat="server" Text="  Clear  " BackColor="Black"
                    ForeColor="White" OnClick="btnclear_Click" />
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
