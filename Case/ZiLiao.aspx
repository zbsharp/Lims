<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZiLiao.aspx.cs" Inherits="Case_ZiLiao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>附件信息</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
  
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
</head>
<body>
    <form id="form1" runat="server">

    <div class="Body_Title">
        案件管理 》》上传资料附件</div>
    <div>
         <table class="Admin_Table">
                                
                                  <tr>
                                    <td  colspan="4" >
                                        <asp:DropDownList ID="DropDownList1" runat="server">

                                         <asp:ListItem>申请书</asp:ListItem>
                                            <asp:ListItem>关键件</asp:ListItem>
                                            <asp:ListItem>说明资料</asp:ListItem>
                                           <asp:ListItem>电子上报</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:FileUpload ID="FileUpload1"  runat="server"  />
                                        <asp:Button ID="Button5" runat="server" CausesValidation="false" OnClick="Button5_Click"
                                            Text="上传"  />
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td  >当前任务当类的资料：
                                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" CaptionAlign="Left"
                                            DataKeyNames="id" OnRowDeleting="GridView5_RowDeleting"  >
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="urltext" HeaderText="文件名"  DataTextField="filename">
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:HyperLinkField>
                                                <asp:BoundField DataField="leibie" HeaderText="附件类型" Visible ="false"/>
                                                <asp:BoundField DataField="typ" HeaderText="文件类型"   />
                                                <asp:BoundField DataField="fillname" HeaderText="上传人"   />
                                                <asp:BoundField DataField="filltime" HeaderText="上传日期" DataFormatString ="{0:d}"   />

                                                <asp:CommandField DeleteText="删除附件" HeaderText="删除" ShowDeleteButton="True" />
                                                <asp:BoundField DataField="leibie" HeaderText="类型"  />
                                            </Columns>
                                            <HeaderStyle CssClass="Admin_Table_Title " />   

                                        </asp:GridView>
                                    </td>
                                </tr>
                                

                                <tr>
                                    <td  >当前任务其他的资料：<asp:Button ID="Button6" runat="server" Text="批量下载" 
                                            onclick="Button6_Click" />(请直接用IE浏览器下载，不要使用迅雷等工具,否则下载后乱码)
&nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" CaptionAlign="Left"
                                            DataKeyNames="id"   >
                                            <Columns>

                                            <asp:TemplateField>
                          
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:BoundField DataField="filename" HeaderText="文件类型" />


                                                <asp:HyperLinkField DataNavigateUrlFields="urltext" HeaderText="文件名"  DataTextField="filename">
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:HyperLinkField>
                                                <asp:BoundField DataField="leibie" HeaderText="附件类型" Visible ="false"/>
                                                <asp:BoundField DataField="typ" HeaderText="文件类型" />
                                                <asp:BoundField DataField="fillname" HeaderText="上传人"   />
                                                <asp:BoundField DataField="filltime" HeaderText="上传日期" DataFormatString ="{0:d}"   />

                                                <asp:CommandField DeleteText="删除附件" HeaderText="删除" ShowDeleteButton="True"  Visible ="false" />
                                                 <asp:BoundField DataField="leibie" HeaderText="类型"  />
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
