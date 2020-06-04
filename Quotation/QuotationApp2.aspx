<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuotationApp2.aspx.cs" Inherits="Quotation_QuotationApp2" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="Body_Title">
        <asp:Label ID="Label1" runat="server"></asp:Label>
        》》<asp:Label ID="Label2" runat="server"></asp:Label>》》<asp:Label ID="Label3" runat="server"></asp:Label></div>
   
   <fieldset>
            <legend style="color: Red">审批信息</legend>

        <table class="Admin_Table">
            <tr>
                
                <td colspan="4">
                    <asp:TextBox ID="TextBox2" runat="server"  TextMode ="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">结论： <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>通过</asp:ListItem>
                        <asp:ListItem>不通过</asp:ListItem>
                        <asp:ListItem>待定</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="Button3" runat="server" Text="确定保存" OnClick="Button3_Click" /><asp:Label
                        ID="Label4" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:GridView ID="GridView3" runat="server" Width="100%" AutoGenerateColumns="false"
                        DataKeyNames="id" CssClass="Admin_Table">
                        <Columns>
                            <asp:BoundField DataField="result" HeaderText="审批结果" />
                            <asp:BoundField DataField="yijian" HeaderText="审批意见" />
                            <asp:BoundField DataField="fillname" HeaderText="审批人" />
                            <asp:BoundField DataField="filltime" HeaderText="审批时间" />
                            <asp:BoundField DataField="leibie" HeaderText="审批级别" />

                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </fieldset>
   
   
   
    <fieldset>
        <legend style="color: Red">产品信息</legend>
        <table class="Admin_Table">
            <tr>
                <td colspan="4">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                        DataKeyNames="id" CssClass="Admin_Table">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                        CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="name" HeaderText="产品名称" />
                            <asp:BoundField DataField="type" HeaderText="产品型号" />
                            <asp:BoundField DataField="beizhu" HeaderText="备注" />
                            <asp:CommandField HeaderText="取消产品" ShowDeleteButton="false" ShowEditButton="false" />
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend style="color: Red">项目信息</legend>
        <table class="Admin_Table">
            <tr>
                <td align="center" colspan="4">
                    <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="false"
                        DataKeyNames="id" CssClass="Admin_Table" OnRowDataBound="GridView2_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                        CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ceshiname" HeaderText="测试项目" />
                            <asp:BoundField DataField="biaozhun" HeaderText="标准" />
                            <asp:BoundField DataField="neirong" HeaderText="内容" />
                            <asp:BoundField DataField="yp" HeaderText="样品" />
                            <asp:BoundField DataField="feiyong" HeaderText="实际报价" />
                             <asp:BoundField DataField="yuanshi" ReadOnly ="true"  HeaderText="标准单价" />

                            <asp:BoundField DataField="" HeaderText="折扣"  />
                            <asp:BoundField DataField="shuliang" HeaderText="数量" />
                            <asp:BoundField DataField="beizhu" HeaderText="备注" />
                            <asp:CommandField HeaderText="取消产品" ShowDeleteButton="false" ShowEditButton="false" />

                            

                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend style="color: Red">报价信息</legend>
        <table class="Admin_Table">
            <tr style ="display :none ;">
                <td>
                    标准报价：
                </td>
                <td>
                    <asp:TextBox ID="baojiazong_txt"  Enabled ="false"   runat="server"></asp:TextBox>
                </td>
                <td>
                    </td>
                <td>
                    <asp:TextBox ID="baojiazhekou_txt" Enabled ="false" Visible="False" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>
                    <asp:TextBox ID="realzhekou_txt" Enabled ="false"  runat="server" 
                        ReadOnly="true" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="weituo" Enabled ="false"  runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    实际报价：
                </td>
                <td>
                    <asp:TextBox ID="baojiazhehou_txt" Enabled ="false"  ForeColor ="Red"  runat="server" ></asp:TextBox>
                </td>
                <td>
                    报价备注：
                </td>
                <td>
                    <asp:TextBox ID="baojiabeizhu_txt" Enabled ="false"  runat="server"></asp:TextBox>
                </td>
            </tr>
           
        </table></fieldset> 
        <fieldset>
            <legend style="color: Red">条款信息</legend>
            <table class="Admin_Table">
                <tr>
                    <td align="left" colspan="4">
                        <asp:CheckBoxList ID="CheckBoxList9" Enabled ="false"  runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                   
                    <td align="left" colspan="4">
                        <asp:TextBox ID="TextBox1" Enabled ="false"  runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan ="4">账户版本:<asp:DropDownList ID="DropDownList3" runat="server">

                    <asp:ListItem>中检中文</asp:ListItem>
                                            <asp:ListItem>中检英文</asp:ListItem>
                                            <asp:ListItem>中认中文</asp:ListItem>
                                            <asp:ListItem>中认英文</asp:ListItem>

                    </asp:DropDownList></td></tr>
            </table>
        </fieldset>


         <fieldset>
        <legend style="color: Red">联系人
                 <asp:DropDownList ID="DropDownList2"
                                            runat="server" Enabled ="false">
                                          
                                        </asp:DropDownList></legend>




        <table class="Admin_Table">
            <tr>
                <td colspan="7" align="center">
                    <asp:GridView ID="GridView5" runat="server" Width="100%" AutoGenerateColumns="false"
                        DataKeyNames="id"
                        CssClass="Admin_Table" >
                        <Columns>
                            <asp:BoundField DataField="name" HeaderText="姓名" />
                                <asp:BoundField DataField="department" HeaderText="部门" />
                                <asp:BoundField DataField="rode" HeaderText="角色" />
                                <asp:BoundField DataField="telephone" HeaderText="电话" />
                                <asp:BoundField DataField="mobile" HeaderText="手机" />
                                <asp:BoundField DataField="email" HeaderText="邮箱" />
                                <asp:BoundField DataField="fax" HeaderText="传真" />
                                <asp:BoundField DataField="beizhu" HeaderText="备注" />
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                </td>
            </tr>
        </table></fieldset> 



         
    <fieldset>
        <legend style="color: Red">协议信息</legend>
        <asp:GridView ID="GridView4" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="false">
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
    </fieldset>
    </form>
</body>
</html>

