<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncomeCheck1.aspx.cs" Inherits="Income_IncomeCheck1" %>

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
                            
                            <td  width="100%">
                                付款方/金额/付款方式/流水号<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                   
                                   <asp:ListItem Value ="">未对</asp:ListItem>
                                    <asp:ListItem Value ="已确认">已对</asp:ListItem>
                                    
                                   
                                </asp:DropDownList>
                                <asp:Button ID="Button1"
                                    runat="server" Text="查询" onclick="Button1_Click1" />(在未确认之前可改删自己所录入的数据，否则不能删改)
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LblMessage" runat="server" Font-Bold="True" ForeColor="Red" Width="224px"></asp:Label>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound"
                        OnRowDeleting="GridView1_RowDeleting" 
                        onrowcancelingedit="GridView1_RowCancelingEdit" 
                        onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="序 号" Visible="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>'
                                        CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="liushuihao" HeaderText="流水号" ReadOnly="True" />
                            <asp:BoundField DataField="fukuanren" HeaderText="付款人" />
                            <asp:BoundField DataField="fukuanriqi" HeaderText="付款日期" DataFormatString="{0:d}"/>
                            <asp:BoundField DataField="danwei" HeaderText="币种" ReadOnly="True"/>
                            <asp:BoundField DataField="fukuanjine" HeaderText="付款金额" />
                            <asp:BoundField DataField="fukuanfangshi" HeaderText="付款方式" />
                            <asp:BoundField DataField="queren" HeaderText="确认" ReadOnly="True" Visible ="false" />

                            <asp:BoundField DataField="beizhu" HeaderText="备注" />
                            <asp:BoundField DataField="daoruren" HeaderText="导入人" ReadOnly="True"/>
                            <asp:BoundField DataField="daorutime" DataFormatString="{0:d}" HeaderText="导入时间" ReadOnly="True"/>
                            <asp:BoundField DataField="fapiaoleibie" HeaderText="批次" ReadOnly="True"  Visible ="false" />
                             <asp:BoundField DataField="shoufeiid" HeaderText="凭证号" ReadOnly="True"/>



                        

                                <asp:CommandField  ShowDeleteButton ="false"  ShowEditButton ="false" />

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


