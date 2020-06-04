<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductPrice.aspx.cs" Inherits="SysManage_ProductPrice" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
     <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
   
     <div>
    <div class="Body_Title">
        系统管理 》》导入产品价格信息</div>
    
    
   
    
            <table class="Admin_Table">
              <tr height="30">
                <td style="width: 151px"><font face="宋体">请选择要导入的文件</font></td>
                <td style="width: 350px" align="left" width="350"><input id="FileExcel" style="width: 300px" type="file" size="42" name="filephoto" runat="server"><font color="red"></font></td>
                <td class="hint"><font face="宋体"><asp:button id="BtnImport" text="导 入"  CssClass ="BnCss" runat="server" OnClick="BtnImport_Click1"></asp:button>
                    <asp:Button ID="Button1" runat="server" visible="false"   CssClass ="BnCss"  OnClick="Button1_Click1" Text="取消" />
                    <asp:Button ID="Button3" runat="server" Text="加入单个设备"   CssClass ="BnCss" OnClick="Button3_Click" /><span  style ="color:Red;"></span></font></td>
              </tr>
            </table>
           
        查询<asp:DropDownList ID="DropDownList1" runat="server" Width="105px">
            
             <asp:ListItem>部门</asp:ListItem>
            <asp:ListItem>大类</asp:ListItem>
            <asp:ListItem>中类</asp:ListItem>
            <asp:ListItem>有折扣</asp:ListItem>
            <asp:ListItem>无折扣</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox2"    runat="server" Width="120px"></asp:TextBox>
              <asp:Button ID="Button2"  CssClass ="BnCss"  runat="server" Text="查询" OnClick="Button2_Click" />
 <asp:GridView ID="GridView1" runat="server" 
        AllowSorting="True" AutoGenerateColumns="False" CssClass="Admin_Table" 
        DataKeyNames="id" 
        PageSize="15" style="font-size: 9pt" Width="100%" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging1" OnRowDeleting="GridView1_RowDeleting1" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand">
        
        <Columns>
            <asp:TemplateField HeaderText="序 号">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" ForeColor="Green" 
                        Text='<%# (Container.DisplayIndex+1).ToString("000") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle ForeColor="Green" />
            </asp:TemplateField>
            
            <asp:BoundField DataField="BIGTYPE" HeaderText="大类" ReadOnly ="True"/>
            <asp:BoundField DataField="MIDTYPE" HeaderText="中类"  ReadOnly ="True"/>
           
            <asp:BoundField DataField="COST" HeaderText="平衡点单价"  />
            <asp:BoundField DataField="PRICE" HeaderText="建议单价"  />
            <asp:BoundField DataField="DISCOUNT" HeaderText="最底折扣" />
          
            <asp:BoundField DataField="department" HeaderText="科室" />
           <asp:BoundField DataField="checkmodel" HeaderText="校验方式" Visible ="false"  />
            <asp:BoundField DataField="cnas" HeaderText="CNAS标志" Visible ="false" />
            <asp:BoundField DataField="cardperiod" HeaderText="出证周期" Visible ="false" />
             <asp:TemplateField HeaderText="明细" >
                                                                <ItemTemplate  >
                                                                     <asp:LinkButton ID="LinkButton8" runat="server" Text="查看" ForeColor="blue"  CommandArgument='<%# Eval("id") %>' CommandName="detail"  ></asp:LinkButton>       
                                                                </ItemTemplate>
                                                                 <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True"   />
        </Columns>
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    </asp:GridView>

      <asp:label id="LblMessage" runat="server" font-bold="True" forecolor="Red" Width="224px"></asp:label>&nbsp;<br />
                                                    &nbsp;<webdiyer:aspnetpager id="AspNetPager2" runat="server" custominfohtml="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                                                        custominfotextalign="Center" firstpagetext="【首页】" height="25px" horizontalalign="Center"
                                                        inputboxstyle="width:19px" lastpagetext="【尾页】" nextpagetext="【下页】 " 
                                                        prevpagetext="【前页】 " showcustominfosection="Left" showinputbox="Never" shownavigationtooltip="True"
                                                        width="682px" style="font-size: 9pt" UrlPaging="True" PageSize="15" OnPageChanged="AspNetPager2_PageChanged"> </webdiyer:aspnetpager>
   

    </div>
   
    </form>
</body>
</html>

