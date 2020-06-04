<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuoLink.aspx.cs" Inherits="Quotation_QuoLink" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
</head>
<body>
       <form name="form1"  runat="server"  id="form1">
<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

    
       <table cellpadding="2" cellspacing="1" class="Admin_Table" style="width:99%; margin:5px auto;">
                        
                         <tr >
            <td style="width: 110px; text-align: left">
                类别：</td>
            <td style="text-align: left">
                <asp:DropDownList ID="DropDownList1" runat="server" Width="151px">
                    <asp:ListItem>委托方</asp:ListItem>
                    <asp:ListItem>代理方</asp:ListItem>
                    <asp:ListItem>制造方</asp:ListItem>
                    <asp:ListItem>付款方</asp:ListItem>
                    <asp:ListItem>生产方</asp:ListItem>
                </asp:DropDownList></td>
            <td style="width: 90px; text-align: left">
                标志：</td>
            <td style="text-align: left">
                <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox></td>
        </tr>
                        
                        <tr >
                            <td style="text-align: left; width: 110px;">
                                编号：</td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                        </tr>
        <tr >
            <td style="width: 110px; text-align: left">
                名称：</td>
            <td colspan="3" style="text-align: left">
                <asp:TextBox ID="TextBox2" runat="server" Width="90%"></asp:TextBox></td>
        </tr>
                        <tr >
                            <td style="text-align: left; width: 110px;">
                                地址：</td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="TextBox3" runat="server" Width="90%"></asp:TextBox></td>
                        </tr>
                        <tr >
                            <td style="text-align: left; width: 110px;">
                                联系人：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 90px;">
                                电话：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr >
                            <td style="text-align: left; width: 110px;">
                                手机：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 90px;">
                                邮箱：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
                        </tr>
        <tr >
            <td style="width: 110px; text-align: left">
                传真：</td>
            <td style="text-align: left">
                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
            <td style="width: 90px; text-align: left">
                                备注：</td>
            <td style="text-align: left">
                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
        </tr>
       
                        <tr >
                            <td colspan="4" style="text-align: center">
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                                    Text="保 存" OnClick="Button1_Click" />
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr >
                            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                            <RowStyle CssClass="textcenter" ForeColor="#000066" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                            CommandName="BussinessNeeds" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="bianhao" HeaderText="编号" />
                                <asp:BoundField DataField="name" HeaderText="客户名" />
                                <asp:BoundField DataField="address" HeaderText="地址" />
                                <asp:BoundField DataField="lianxiren" HeaderText="联系人" />
                                <asp:BoundField DataField="dianhua" HeaderText="电话" />
                                <asp:BoundField DataField="shouji" HeaderText="手机" />
                                <asp:BoundField DataField="youxiang" HeaderText="邮箱" />
                                <asp:BoundField DataField="chuanzhen" HeaderText="传真" />
                                <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                <asp:BoundField DataField="type" HeaderText="类别" />
                                <asp:BoundField DataField="beizhu1" HeaderText="标志" />
                                <asp:CommandField HeaderText="编辑" ShowDeleteButton="True" ShowEditButton="True" >
                                                    <ItemStyle ForeColor="Blue" />
                                                </asp:CommandField>
                            </Columns>
                           <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                            </td>
                        </tr>
    </table>


       </div> 

</form>
</body>
</html>
