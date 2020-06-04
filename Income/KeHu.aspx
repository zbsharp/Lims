<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KeHu.aspx.cs" Inherits="Income_KeHu" %>

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
     <div class="Body_Title">
        财务管理 》》倍测检测导入客户</div>

        <table class="text" cellspacing="1" cellpadding="0" width="100%" bgcolor="#1d82d0"
            border="0">
            <tr bgcolor="#ffffff">
                <td valign="top">
                    <table class="text" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                           
                            <td valign="top" width="100%">
                                <table class="text" cellspacing="1" cellpadding="0" width="100%" border="0">
                                    <tr height="30">
                                       
                                        <td  align="left" >
                                            <input id="FileExcel" style="width: 101px" type="file" size="42" name="filephoto"
                                                runat="server"><font color="red"></font>
                                        </td>
                                        <td class="hint">
                                            <font face="宋体">
                                                <asp:Button ID="BtnImport" Text="导 入" CssClass="button" runat="server" 
                                                OnClick="BtnImport_Click1" Width="60px">
                                                </asp:Button>
                                                <asp:Button ID="Button1" runat="server" Text="取消导入"  OnClick="Button1_Click" 
                                                Width="94px" />批此号：<asp:TextBox
                                                    ID="TextBox1" runat="server" Width="91px"></asp:TextBox></font>
                                            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Visible ="false"  Text="确认" />
                                          
                                            客户：<asp:TextBox ID="TextBox2" runat="server" Width="99px"></asp:TextBox>
                                            且<asp:DropDownList ID="DropDownList1" Visible ="false"  runat="server">
                                                <asp:ListItem>否</asp:ListItem>
                                                <asp:ListItem>是</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="查询" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                        </td>
                                    </tr>
                                </table>
                             
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LblMessage" runat="server" Font-Bold="True" ForeColor="Red" Width="224px"></asp:Label>
                    <br /> 
                  
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound"
                        OnRowDeleting="GridView1_RowDeleting">
                        <Columns>
                           
                           
                           
                            <asp:BoundField DataField="bianhao" HeaderText="编号" />
                            <asp:BoundField DataField="kehuname" HeaderText="购买企业名称" />
                        
                                 <asp:BoundField DataField="name" HeaderText="导入人" />

                                   <asp:BoundField DataField="filltime" HeaderText="导入日期"  DataFormatString ="{0:d}" />
                                  

                          
                          <asp:BoundField DataField="pici" HeaderText="批次号" />

                          


                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                        CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                        InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " PrevPageText="【前页】 "
                        ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                        Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="15" OnPageChanged="AspNetPager2_PageChanged">
                    </webdiyer:AspNetPager>






                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
