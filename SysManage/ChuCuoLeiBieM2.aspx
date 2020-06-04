<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChuCuoLeiBieM2.aspx.cs" Inherits="SysManage_ChuCuoLeiBieM2" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> </title> 
	
<link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>   

</head>
<body>
    <form name="form1"  runat="server"  id="form1">
<div>
  <div class="Body_Title">
           仪校管理 》》证书查询</div>	
<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

     
    
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
            <td style="text-align: center">
                                <asp:DropDownList ID="DropDownList1" Visible ="false"  runat="server">
                                </asp:DropDownList>类别或名称
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                                    Text="查询" OnClick="Button1_Click" /></td>
        </tr>
        <tr bgcolor="#f4faff">
            <td style="text-align: left">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table"
                            Width="100%" DataKeyNames="departmentid" OnRowDeleting="GridView1_RowDeleting" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                            <RowStyle CssClass="textcenter" ForeColor="#000066" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("departmentid") %>'
                                            CommandName="BussinessNeeds" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="name" HeaderText="文件类别" />
                                <asp:BoundField DataField="wenyuan" HeaderText="文件名称"  />
                                <asp:BoundField DataField="phone" HeaderText="电话"  Visible ="false" />
                                <asp:BoundField DataField="fax" HeaderText="传真" Visible ="false"/>
                                <asp:BoundField DataField="beizhu" HeaderText="备注" ReadOnly="True" />
                                <asp:CommandField HeaderText="编辑" ShowDeleteButton="false" ShowEditButton="false"  Visible ="false"  >
                                                    <ItemStyle ForeColor="Blue" />
                                                </asp:CommandField>

  <asp:HyperLinkField DataNavigateUrlFields="departmentid" DataNavigateUrlFormatString="~/SysManage/ChuCuoLeiBie1aspx1.aspx?id={0}"
                                HeaderText="" Text="修改" Target="_blank" />


                            </Columns>
                           <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>


                  <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第%StartRecordIndex%-%EndRecordIndex%"
                                            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                                            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager2_PageChanged"
                                            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                                            Style="font-size: 9pt"  PageSize="15"  UrlPaging="True" >
                                        </webdiyer:AspNetPager>

            </td>
        </tr>
                    </table>
    


       </div> 

</form>
</body>
</html>

