<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadFile.aspx.cs" Inherits="Case_UploadFile" %>

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
        案件管理 》》上传附件</div>
    <div>
         <table class="Admin_Table">
                                
                                  <tr>
                                    <td  colspan="4" >
                                       附件说明：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        <asp:FileUpload ID="FileUpload1"   runat="server" style="margin-left: 4px" 
                                             />
                                        <asp:Button ID="Button5" runat="server" CausesValidation="false" OnClick="Button5_Click"
                                            Text="上传"  />
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td  >
                                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" CaptionAlign="Left"
                                            DataKeyNames="id" OnRowDeleting="GridView5_RowDeleting" ShowHeader="true" >
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="urltext" DataTextField="filename">
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:HyperLinkField>
                                                <asp:BoundField DataField="leibie" HeaderText="附件说明" />
                                                <asp:BoundField DataField="typ" HeaderText="文件类型" />
                                                <asp:BoundField DataField="fillname" HeaderText="上传人"  Visible ="false" />
                                                <asp:CommandField DeleteText="删除附件" HeaderText="删除" ShowDeleteButton="True" />
                                                <asp:BoundField DataField="caseid" HeaderText="附件编号" Visible="False" />
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
