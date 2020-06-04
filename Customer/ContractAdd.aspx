<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractAdd.aspx.cs" Inherits="Customer_ContractAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
   
    <div class="Body_Title">
        增加协议 》》 <asp:Label ID="Label1" runat="server" Text="<%#name %>"></asp:Label><input name="txTitle" type="text" id="txTitle"  visible ="false"  value="<%#username%>" class="TxCss"
                                        style="width: 147px;" runat="server" /></div>
         <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="false">
                           
                            <Columns>
                                <asp:BoundField DataField="contract" HeaderText="内容">
                                    <ItemStyle Width="30%" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="effectdate" HeaderText="有效期">
                                    <ItemStyle Width="15%" />
                                </asp:BoundField>
                               
                                <asp:BoundField DataField="filltime" HeaderText="填写时间" DataFormatString="{0:D}">
                                    <ItemStyle Width="15%" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="fillname" HeaderText="填写人">
                                    <ItemStyle Width="15%" />
                                </asp:BoundField>
                               
                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>


                         <table class="Admin_Table">
                                
                                  <tr>
                                    <td  colspan="4" >
                                        <asp:DropDownList ID="DropDownList1" runat="server">

                                         <asp:ListItem>协议信息</asp:ListItem>
                                           
                                        </asp:DropDownList>
                                        <asp:FileUpload ID="FileUpload1"  runat="server"  />
                                        <asp:Button ID="Button5" runat="server" CausesValidation="false" OnClick="Button5_Click"
                                            Text="上传"  />
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td  >
                                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" CaptionAlign="Left"
                                            DataKeyNames="id" OnRowDeleting="GridView5_RowDeleting" ShowHeader="False" >
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="urltext" DataTextField="filename">
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:HyperLinkField>
                                                <asp:BoundField DataField="leibie" HeaderText="附件类型" />
                                                <asp:BoundField DataField="typ" HeaderText="文件类型" />
                                                <asp:BoundField DataField="fillname" HeaderText="上传人"  Visible ="false" />
                                                <asp:CommandField DeleteText="删除附件" HeaderText="删除" ShowDeleteButton="false" />
                                                <asp:BoundField DataField="caseid" HeaderText="附件编号" Visible="False" />
                                            </Columns>
                                            <HeaderStyle CssClass="Admin_Table_Title " />   

                                        </asp:GridView>
                                    </td>
                                </tr>
                                
                                
                                </table> 
  
     
 
                        <table class="Admin_Table">
                          
                           
                            <tr>
                                <td>
                                    有效日期：
                                </td>
                                <td>
                                    <input id="Text2" name="txFDate" class="TxCss" type="text" visible ="true"  value="" onclick="popUpCalendar(this,document.forms[0].Text2,'yyyy-mm-dd')"   runat="server" style="width: 122px" />
                                    &nbsp; &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    内容：
                                </td>
                                <td>
                                    <textarea name="txContent" cols="20" id="txContent" class="TxCss" style="height: 138px;
                                        width: 510px;" runat="server"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="Button1" runat="server" Text="保  存" class="BnCss" OnClick="Button1_Click" />
                                    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                    <input id="Reset1" type="reset" value="重 置" class="BnCss" runat="server" />
                                </td>
                            </tr>
                        </table>
                    
                 
                 
  
    </form>
</body>
</html>
