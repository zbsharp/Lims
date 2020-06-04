<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaogaoAdd.aspx.cs" Inherits="CCSZJiaoZhun_htw_BaogaoAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	获取报告
</title>

 <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
  
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>

</head>
<body>
    <form name="form1"  runat="server"  id="form1">

     <div class="Body_Title">
        案件管理 》》获取报告号</div>

<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

       
    
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                任务编号：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True"></asp:TextBox></td>
                            <td style="text-align: left; width: 90px;">
                                报告名称：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                产品：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 90px;">
                                型号：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                        </tr>
        <tr bgcolor="#f4faff">
            <td style="width: 90px; text-align: left">
                样品：</td>
            <td style="text-align: left">
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
            <td style="width: 90px; text-align: left">
            </td>
            <td style="text-align: left">
            </td>
        </tr>
                        <tr bgcolor="#f4faff">
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
                            </td>
                        </tr>
                    </table>
     <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
                            <td >
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" DataKeyNames="id" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                            OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Style="font-size: 9pt"
                                            Width="98%">
                                           
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                                            CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="baogaobianhao" HeaderText="报告编号" ReadOnly="True" />
                                                <asp:BoundField DataField="baogaoname" HeaderText="报告名称" />
                                                <asp:BoundField DataField="cp" HeaderText="产品" />
                                                <asp:BoundField DataField="xm" HeaderText="型号" />
                                                <asp:BoundField DataField="yp" HeaderText="样品" />
                                                <asp:BoundField DataField="beizhu1" HeaderText="备注1" />
                                                <asp:BoundField DataField="beizhu2" HeaderText="备注2" />
                                                <asp:BoundField DataField="beizhu3" HeaderText="备注3" />
                                                <asp:CommandField HeaderText="编辑" ShowDeleteButton="True" ShowEditButton="True">
                                                    <ItemStyle ForeColor="Blue" />
                                                </asp:CommandField>

                                                    <asp:HyperLinkField HeaderText="上传" Text="上传" Target="_blank" DataNavigateUrlFormatString="~/Report/BaoGaoFirstUpLoad.aspx?baogaoid={0}"
                                DataNavigateUrlFields="baogaobianhao" />

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
