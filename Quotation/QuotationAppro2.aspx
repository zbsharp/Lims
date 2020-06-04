﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuotationAppro2.aspx.cs" Inherits="Quotation_QuotationAppro2" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
   
     <script type ="text/javascript" >


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
</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
        销售管理 》》待审批报价(如遇到故障报价单可点退回后又业务员处理或取消)</div>
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Value ="kehuname">客户名称</asp:ListItem>
        <asp:ListItem Value ="responser">业务人员</asp:ListItem>
        <asp:ListItem Value ="baojiaid">报价编号</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1"
        runat="server" Text="查询" onclick="Button1_Click" />
   
    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" 
        AutoGenerateColumns="false" onrowcommand="GridView1_RowCommand"  onrowdatabound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="序号">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                        CommandArgument='<%# Eval("baojiaid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle ForeColor="Green" />
            </asp:TemplateField>
          

                 <asp:BoundField DataField="baojiaid" HeaderText="报价编号"/>

               <asp:BoundField DataField="kehuname" HeaderText="客户"/>
            <asp:BoundField DataField="realdiscount" HeaderText="折扣"  Visible ="false" />
            <asp:BoundField DataField="zhehoujia" HeaderText="折后价格" />
            <asp:BoundField HeaderText="报价人" DataField="fillname"  />
            <asp:BoundField HeaderText="报价日期" DataField="filltime"  DataFormatString ="{0:d}" />
              <asp:BoundField HeaderText="是否需要领导审批" DataField="weituo" ReadOnly="True" />

              <asp:BoundField HeaderText="部门经理审批情况" DataField="fname1" ReadOnly="True" />

               <asp:BoundField HeaderText="提交日期" DataField="tijiaotime"  DataFormatString ="{0:d}" />
             <asp:BoundField HeaderText="审批状态" DataField="shenpibiaozhi" Visible ="false"  ReadOnly="True" />
            <asp:HyperLinkField HeaderText="打印"  Text ="打印" DataNavigateUrlFormatString="~/Print/QuoPrint.aspx?baojiaid={0}&&customerid={1}&&pi=1"
                DataNavigateUrlFields="baojiaid,kehuid" Target="_blank" />
          <asp:HyperLinkField  HeaderText="查看/审批"  Text ="查看/审批" Target="button" DataNavigateUrlFormatString="~/Quotation/QuotationApp2.aspx?baojiaid={0}&amp;&amp;kehuid={1}"
                DataNavigateUrlFields="baojiaid,kehuid" />


                     <asp:TemplateField HeaderText="直接退回">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton5" runat="server" Text="退回" CommandArgument='<%# Eval("baojiaid") %>'
                        CommandName="cancel1"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle ForeColor="Green" />
            </asp:TemplateField>


           
        </Columns>
        <HeaderStyle CssClass="Admin_Table_Title " />
    </asp:GridView>

     <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                                    CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                                    InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
                                    PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                                    Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="12">
                                </webdiyer:AspNetPager>
    </div>

     <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>

