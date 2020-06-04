<%@ Page Language="C#" AutoEventWireup="true" CodeFile="daoexcel.aspx.cs" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" Inherits="ZiYuan_daoexcel" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            font-size: 12px;
            cursor: default;
            font-family: 宋体;
        }
        .button
        {}
    </style>
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
                                &nbsp;
                            </td>
                            <td valign="top" width="100%">
                                <table class="text" cellspacing="1" cellpadding="0" width="100%" border="0">
                                    <tr height="30">
                                        <td style="width: 151px">
                                            <font face="宋体">请选择要导入的文件(11列)</font>
                                        </td>
                                        <td style="width: 350px" align="left" width="350">
                                            <input id="FileExcel" style="width: 300px" type="file" size="42" name="filephoto"
                                                runat="server"><font color="red"></font>
                                        </td>
                                        <td class="hint">
                                            <font face="宋体">
                                                <asp:Button ID="BtnImport" Text="导 入" CssClass="button" runat="server" 
                                                OnClick="BtnImport_Click1" Width="60px">
                                                </asp:Button>
                                                <asp:Button ID="Button1" runat="server" Text="取消导入" Visible ="false"  OnClick="Button1_Click" 
                                                Width="78px" /><asp:TextBox
                                                    ID="TextBox1" runat="server" Visible ="false"  Width="74px"></asp:TextBox></font>
                                            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Visible ="false"  Text="确认" />
                                        </td>
                                    </tr>
                                </table>
                             
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LblMessage" runat="server" Font-Bold="True" ForeColor="Red" Width="224px"></asp:Label>
                    <br />
                     <asp:GridView ID="GridView1" runat="server" Width="100%" 
                               AutoGenerateColumns="False" DataKeyNames="id"  CssClass="Admin_Table" 
                               style="font-size: 9pt"   >
                                                       
                                                        <HeaderStyle CssClass="Admin_Table_Title " />
                                                       
                                                        <Columns>
                                                          
                                                          <asp:TemplateField HeaderText="序号">
                                                                <ItemTemplate  >
                                                                     <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>' CommandArgument='<%# Eval("id") %>' CommandName="chakan"  ForeColor="Green"></asp:LinkButton>
                                                                    
                                                                </ItemTemplate>
                                                                 <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="bianhao" HeaderText="编号" />
                                                            <asp:BoundField DataField="jq_name" HeaderText="名称" />
                                                            <asp:BoundField DataField="jq_id" HeaderText="型号" />
                                                            <asp:BoundField DataField="sqbumen" HeaderText="申请部门" />
                                                            <asp:BoundField DataField="sqby" HeaderText="申请人" />
                                                            <asp:BoundField DataField="daohuoflag" HeaderText="是否到货" />
                                                            <asp:BoundField DataField="state1" HeaderText="状态" />
                                                            <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="填写日期" />
                                                            
                                                            
                                                              <asp:TemplateField HeaderText="查看">
                                                                <ItemTemplate  >
                                      <asp:LinkButton ID="LinkButton8" runat="server" Text="明细" ForeColor="blue" CommandArgument='<%# Eval("bianhao") %>' CommandName="chakan"  ></asp:LinkButton>
                                                                </ItemTemplate>
                                                                 <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>

                                                          
                                                        </Columns>
                               <EmptyDataTemplate>
                                   <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                               </EmptyDataTemplate>
                                                          
                                                    </asp:GridView>

        <webdiyer:aspnetpager id="AspNetPager2" runat="server" custominfohtml="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第%StartRecordIndex%-%EndRecordIndex%"
                                                        custominfotextalign="Center" firstpagetext="【首页】" height="25px" horizontalalign="Center"
                                                        inputboxstyle="width:19px" lastpagetext="【尾页】" nextpagetext="【下页】 " onpagechanged="AspNetPager2_PageChanged"
                                                        prevpagetext="【前页】 " showcustominfosection="Left" showinputbox="Never" shownavigationtooltip="True"
                                                        width="682px" style="font-size: 9pt" UrlPaging="True" PageSize="15">
              </webdiyer:aspnetpager>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

