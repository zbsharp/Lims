<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaoGaoShenPiBuMen2.aspx.cs" Inherits="Report_BaoGaoShenPiBuMen2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>工程报告审核</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
  <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body style="text-align: center;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
     <div class="Body_Title">
            报告管理 》》工程报告审核</div>


                <table class="Admin_Table"
                     width="100%">
                  
                     <tr>
                        <td  align ="left" >
                            编号：
                        </td>
                        <td align ="left" >
                            <span style="font-size: 13pt; vertical-align: middle; color: red">
                                <asp:TextBox ID="TextBox1" runat="server"  Enabled ="false"  Width="140px" ReadOnly="True"></asp:TextBox></span>
                        </td>
                    </tr>
                    <tr>
                        <td align ="left" >
                            报告编号：
                        </td>
                        <td align ="left" >
                            <span style="font-size: 13pt; vertical-align: middle; color: red">
                                <asp:TextBox ID="TextBox3" runat="server"  Enabled ="false" Width="140px" ReadOnly="True"></asp:TextBox></span>
                        </td>
                    </tr>

                 
                  <tr  style ="display :none;">
                        <td  align ="left" >
                            客户名称：
                        </td>
                        <td align ="left" >
                            <asp:TextBox ID="TextBox2" runat="server" Width="514px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                   <tr  style ="display :none;">
                        <td  align ="left" >
                            错误类型：
                        </td>
                        <td align ="left" >
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>技术</asp:ListItem>
                                <asp:ListItem>粗心</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                 <tr  style ="display :none;">
                        <td align ="left" >
                            问题描述：
                        </td>
                        <td align ="left" >
                            <asp:TextBox ID="TextBox5" runat="server" Width="514px"></asp:TextBox>
                        </td>
                    </tr>
                  <tr  style ="display :none;">
                        <td align ="left" >
                            解决方案：
                        </td>
                        <td align ="left" >
                            <asp:TextBox ID="TextBox6" runat="server" Width="514px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  align ="left" >
                            审批意见：
                        </td>
                        <td align ="left" >
                            <asp:TextBox ID="TextBox8" runat="server" Width="515px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="2" align="center">
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="合格" />
                            <asp:Button ID="Button2" runat="server" Text="不合格" OnClick="Button2_Click" />
                        </td>
                    </tr>
                  
                </table>
       
    <br />
   
                <table class="Admin_Table"
                    class="small" width="100%">
                    <tr>
                        <td  colspan="4">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                        Font-Size="Small" OnRowDataBound="GridView1_RowDataBound" Width="100%" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                        OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" CssClass="Admin_Table" OnRowUpdating="GridView1_RowUpdating">
                                        <Columns>
                                           <asp:BoundField DataField="shenhebianhao" HeaderText="报告编号" ReadOnly="true" />
                                            <asp:BoundField DataField="shenhecontent" HeaderText="审核意见"></asp:BoundField>
                                           
                                           
                                            <asp:BoundField DataField="shenhejieguo" HeaderText="审核结果"></asp:BoundField>
                                            <asp:BoundField DataField="shenhetime1" HeaderText="审核时间"></asp:BoundField>
                                            <asp:CommandField ShowDeleteButton="True" />
                                        </Columns>
                                          <HeaderStyle CssClass="Admin_Table_Title " />
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                </Triggers>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
          
    </form>
</body>
</html>
