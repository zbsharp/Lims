<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashinManage.aspx.cs" Inherits="Income_CashinManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />

     <script type ="text/javascript">


         var currentRowId = 0;
         function SelectRow() {
             if (event.keyCode == 40)
                 MarkRow(currentRowId + 1);
             else if (event.keyCode == 38)
                 MarkRow(currentRowId - 1);
         }

         function MarkRow(rowId) {
             if (document.getElementById(rowId) == null)
                 return;

             if (document.getElementById(currentRowId) != null)
                 document.getElementById(currentRowId).style.backgroundColor = '#ffffff';

             currentRowId = rowId;
             document.getElementById(rowId).style.backgroundColor = '#FFE0C0';
         }
         function text() {
             document.getElementById("bnClick").click();
         }
   
    
  
    
    </script>


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

      <div class="Body_Title">
        业务管理 》》凭证列表</div>

    <div>

    <asp:DropDownList
            ID="DropDownList1" runat="server" Width="77px" >
           
           <asp:ListItem Value ="kehuname">客户名称</asp:ListItem>
        <asp:ListItem Value ="shenqinghao">申请编号</asp:ListItem>
        <asp:ListItem Value ="renwuhao">任务编号</asp:ListItem>
            <asp:ListItem Value ="pinzheng">凭证编号</asp:ListItem>
            
              
        </asp:DropDownList>&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="111px"></asp:TextBox>
        <asp:Button ID="Button2"  CssClass ="BnCss"  runat="server" 
             Text="查询" onclick="Button2_Click" />


      


        <table class="text" cellspacing="1" cellpadding="0" width="100%" bgcolor="#1d82d0"
            border="0">
            <tr bgcolor="#ffffff">
                <td valign="top">
                     <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged"
                Text="全选" />
                    <asp:Label ID="LblMessage" runat="server" Font-Bold="True" ForeColor="Red" Width="224px"></asp:Label>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound"
                       >
                        <Columns>
                              <asp:TemplateField HeaderText="序 号" Visible="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("0000") %>'
                                        CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="shoufeiid" HeaderText="凭证号" />
                            
                            <asp:BoundField DataField="fukuanriqi" DataFormatString="{0:d}" HeaderText="付款日期" />
                            <asp:BoundField DataField="fukuanren" HeaderText="付款方" />
                           
                            <asp:BoundField DataField="fukuanjine" HeaderText="付款金额" />

                           <asp:BoundField DataField="yidui" HeaderText="已对账金额" />
                          
                           <asp:BoundField DataField="querenren" HeaderText="确认人" />
                           <asp:BoundField DataField="querenriqi" HeaderText="确认日期" />

                                 <asp:BoundField DataField="fapiaohao" HeaderText="是否做账" />
                                <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>


                             <asp:HyperLinkField HeaderText="打印"  Text ="打印" DataNavigateUrlFormatString="~/Print/CashinPrint.aspx?shoufeiid={0}"
                DataNavigateUrlFields="shoufeiid" Target="_blank" />

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

