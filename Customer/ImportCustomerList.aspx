<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportCustomerList.aspx.cs"
    EnableViewState="true" MaintainScrollPositionOnPostback="true" Inherits="CCSZJiaoZhun_Customer_ImportCustomerList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title></title>
     <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="text" cellspacing="1" cellpadding="0" width="100%" bgcolor="#1d82d0"
                border="0">
                <tr bgcolor="#ffffff">
                    <td valign="top">
                        
                        
                        <table class="text" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td width="15">
                                    &nbsp;</td>
                                <td valign="top" width="100%">
                                    <table class="text" cellspacing="1" cellpadding="0" width="100%" border="0">
                                        <tr height="30">
                                            <td style="width: 151px">
                                                <font face="宋体">请选择要导入的文件</font></td>
                                            <td style="width: 350px" align="left" width="350">
                                                <input id="FileExcel" style="width: 300px" type="file" size="42" name="filephoto"
                                                    runat="server" /><font color="red"></font></td>
                                            <td class="hint">
                                                <font face="宋体"><a href="../Customer/Customer.xls">请按照所提供表的格式要求并保证Excel为2003-97版本(点击下载)</a>
                                                    <asp:Button ID="BtnImport" Text="导 入" CssClass="button" runat="server" OnClick="BtnImport_Click">
                                                    </asp:Button>
                                                    <asp:Button ID="BtnCancelImport" runat="server" Text="取消导入" 
                                                    OnClick="BtnCancelImport_Click" Width="96px" />批次号：<asp:TextBox
                                                        ID="TxtPici" runat="server" Width="74px"></asp:TextBox></font></td>
                                        </tr>
                                    </table>
                                  </td>
                            </tr>
                        </table>
                        
                        <asp:Label ID="LblMessage" runat="server" Font-Bold="True" ForeColor="Red" Width="224px"></asp:Label>
                        <br />
                        
                        
                        <asp:GridView ID="GridView1" runat="server" Width="2000px" AutoGenerateColumns="False"
                            DataKeyNames="id" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound"
                            OnRowDeleting="GridView1_RowDeleting">
                          
                            <Columns>
                                <asp:TemplateField HeaderText="序 号" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>'
                                            CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="Kehuid" HeaderText="客户ID" />
                                <asp:BoundField DataField="CustomName" HeaderText="客户名称" ReadOnly="True" />
                                <asp:BoundField DataField="WeiTuoName" HeaderText="委托名称" />
                                <asp:BoundField DataField="Address" HeaderText="客户地址" />
                                <asp:BoundField DataField="url" HeaderText="客户公司网址" />
                                <asp:BoundField DataField="Responser" HeaderText="导入人" />
                                <asp:BoundField DataField="Fillname" HeaderText="导入人" />
                                <asp:BoundField DataField="Filltime" DataFormatString="{0:d}" HeaderText="导入时间" />
                                <asp:BoundField DataField="b" HeaderText="标志" />
                                <asp:BoundField DataField="pubtime1" HeaderText="时间1" />
                                <asp:BoundField DataField="pubtime2" HeaderText="时间2" />
                                <asp:BoundField DataField="pubtime3" HeaderText="时间3" />
                                <asp:BoundField DataField="xingyongdengji" HeaderText="信用等级" />
                                <asp:BoundField DataField="CustomSouce" HeaderText="客户来源" />
                                <asp:BoundField DataField="CustomType" HeaderText="客户类型" />
                                <asp:BoundField DataField="BianHao" HeaderText="批次号" />
                                <asp:BoundField DataField="Remark" HeaderText="备注" />
                                <asp:BoundField DataField="Request" HeaderText="是否请求" />
                                <asp:BoundField DataField="providername" HeaderText="提供者" />
                                <asp:BoundField DataField="Class" HeaderText="等级" />
                                <asp:BoundField DataField="customlevel" HeaderText="备注1" />
                                <asp:BoundField DataField="requestdate" HeaderText="备注2" />
                                
                                <asp:CommandField ShowDeleteButton="true" DeleteText="删除" />
                            </Columns>
                           <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>
                        
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
