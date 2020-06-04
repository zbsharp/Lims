<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaoGaoFirstUpLoadShenHe2.aspx.cs" Inherits="Report_BaoGaoFirstUpLoadShenHe2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>报告</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
        报告管理 》》报告审核</div>
    <div>
        <table class="Admin_Table">
          <tr style ="display :none ;">
                <td colspan="4">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button ID="Button5" runat="server" CausesValidation="false" OnClick="Button5_Click"
                        Text="上传" />
                    <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                   
&nbsp;
                </td>
            </tr>
            <tr>
                <asp:CheckBox ID="CheckBox2" runat="server" Text="全选" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged" />
                <td> <asp:Button ID="Button6" runat="server" Text="批量下载" 
                                            onclick="Button6_Click" />(请直接用IE浏览器下载，不要使用迅雷等工具,否则下载后乱码)
                   
                   
                    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table"
                        CaptionAlign="Left" DataKeyNames="id" OnRowDeleting="GridView5_RowDeleting">
                        <Columns>

                               <asp:TemplateField>
                          
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="filename" HeaderText="文件类型" />

                          <asp:HyperLinkField DataNavigateUrlFields="urltext" HeaderText="报告名称" DataTextField="pub_field3">
                                <ItemStyle ForeColor="Green" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="leibie" HeaderText="部门" />
                            <asp:BoundField DataField="typ" HeaderText="文件类型" />
                            <asp:BoundField DataField="fillname" HeaderText="上传人" Visible="false" />
                            <asp:BoundField DataField="caseid" HeaderText="附件编号" Visible="False" />
                            <asp:BoundField DataField="state1" HeaderText="签字状态" />
                            <asp:BoundField DataField="state2" HeaderText="审核状态" />
                          <asp:HyperLinkField DataNavigateUrlFields="urltext" HeaderText="附件名称" DataTextField="filename">
                                <ItemStyle ForeColor="Green" />
                            </asp:HyperLinkField>
                            <asp:TemplateField HeaderText="工程审核"  Visible ="false" >
                                <ItemTemplate>
                                    <span style="cursor: hand; color: Blue;" onclick="window.open('BaoGaoShenPiBuMen2.aspx?id=<%#Eval("id") %>','test')">
                                        <asp:Label ID="seeLB" runat="server" Text="审核"></asp:Label></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                         
                          <asp:BoundField DataField="baojiaid" HeaderText="文件大小(单位：M,0表示不足1M)" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                </td>
            </tr>
        </table>

           签字记录
         <table class="Admin_Table"
                    width="100%">
                    <tr>
                        <td  colspan="4">
                          
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                       Width="100%" CssClass="Admin_Table">
                                        <Columns>
                                          <asp:BoundField DataField="shenhebianhao" HeaderText="报告编号" ReadOnly="true" />

                                                <asp:BoundField DataField="filename" HeaderText="当前报告名称" ReadOnly="true" />
                                                <asp:BoundField DataField="shenheren" HeaderText="签字人"></asp:BoundField>
                                            <asp:BoundField DataField="shenhecontent" HeaderText="签字意见"></asp:BoundField>
                                            
                                           
                                            <asp:BoundField DataField="shenhejieguo" HeaderText="签字结果"></asp:BoundField>
                                            <asp:BoundField DataField="shenhetime1" HeaderText="签字时间"></asp:BoundField>
                                            <asp:BoundField DataField="beizhu4" HeaderText="历史报告名称"></asp:BoundField>
                                            <asp:CommandField ShowDeleteButton="false" />
                                        </Columns>
                                          <HeaderStyle CssClass="Admin_Table_Title " />
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                            
                        </td>
                    </tr>
                </table>
                审核记录
                <table class="Admin_Table"
                    width="100%">
                    <tr>
                        <td  colspan="4">
                           
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                        Width="100%"  CssClass="Admin_Table" >
                                        <Columns>
                                            <asp:BoundField DataField="shenhebianhao" HeaderText="报告编号" ReadOnly="true" />

                                            <asp:BoundField DataField="filename" HeaderText="当前报告名称" ReadOnly="true" />

                                             <asp:BoundField DataField="shenheren" HeaderText="审核人"></asp:BoundField>
                                            <asp:BoundField DataField="shenhecontent" HeaderText="审核意见"></asp:BoundField>
                                         
                                           
                                            <asp:BoundField DataField="shenhejieguo" HeaderText="审核结果"></asp:BoundField>
                                            <asp:BoundField DataField="shenhetime1" HeaderText="审核时间"></asp:BoundField>
                                            <asp:BoundField DataField="beizhu4" HeaderText="历史报告名称"></asp:BoundField>
                                            <asp:CommandField ShowDeleteButton="false" />
                                        </Columns>
                                          <HeaderStyle CssClass="Admin_Table_Title " />
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                              
                        </td>
                    </tr>
                </table>


    </div>
    </form>
</body>
</html>
