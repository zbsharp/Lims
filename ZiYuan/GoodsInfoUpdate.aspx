<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoodsInfoUpdate.aspx.cs" Inherits="ZiYuan_GoodsInfoUpdate" %>

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
  <script type="text/javascript" src="../js/calendar.js"></script>


</head>
<body>
    <form name="form1"  runat="server"  id="form1">
<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"   width="100%">
                <tr bgcolor="#f4faff">
                    <td>
                        <strong>资产修改<asp:TextBox ID="TextBox14" runat="server" 
                            onclick="new Calendar().show(this.form.TextBox14);" Visible="False"></asp:TextBox>
                        <asp:DropDownList ID="DropDownList4" runat="server" Width="154px" 
                            Visible="False">
                            <asp:ListItem>赠送</asp:ListItem>
                            <asp:ListItem>转移</asp:ListItem>
                            <asp:ListItem>购买</asp:ListItem>
                        </asp:DropDownList>
                        </strong></td>
                </tr>
            </table>

              <fieldset>
            <legend style="color: Red">采购信息</legend>


            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"   width="100%">
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        类别：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="154px" Enabled="False">
                             <asp:ListItem></asp:ListItem>
                            <asp:ListItem>中检</asp:ListItem>
                            <asp:ListItem>中认</asp:ListItem>
                            <asp:ListItem>佛山</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="width: 75px">
                        申请日期：</td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" onclick="new Calendar().show(this.form.TextBox1);"></asp:TextBox></td>
                    <td style="width: 75px">
                        申请部门：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="154px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        申请人：</td>
                    <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="DropDownList2" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                        <asp:DropDownList ID="DropDownList3" runat="server" Width="154px">
                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                    </td>
                    <td style="width: 75px">
                        申请数量：</td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server">0</asp:TextBox></td>
                    <td style="width: 75px">
                        预算编号：</td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        预算金额：</td>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server">0.00</asp:TextBox></td>
                    <td style="width: 75px">
                        采购人：</td>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                    <td style="width: 75px">
                        资产名称：</td>
                    <td>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td style="width: 75px">
                        单位：</td>
                    <td>
                        <asp:TextBox ID="TextBox10" runat="server">台</asp:TextBox></td>
                    <td style="width: 75px">
                        供应商：</td>
                    <td>
                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></td>
                </tr>
              
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        供应商地址</td>
                    <td>
                        <asp:TextBox ID="TextBox26" runat="server"></asp:TextBox></td>
                    <td style="width: 75px">
                        供应商联系人</td>
                    <td>
                        <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox></td>
                    <td style="width: 75px">
                        供应商电话</td>
                    <td>
                        <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        资质：</td>
                    <td>
                        <asp:TextBox ID="TextBox29" runat="server"></asp:TextBox></td>
                    <td style="width: 75px">
                        计划：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList10" runat="server" Width="154px">
                             <asp:ListItem></asp:ListItem>
                            <asp:ListItem>计划内</asp:ListItem>
                            <asp:ListItem>计划外</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="width: 75px">
                        优惠金额：</td>
                    <td>
                        <asp:TextBox ID="TextBox30" runat="server"></asp:TextBox></td>
                </tr>


                  <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        实际金额：</td>
                    <td>
                        <asp:TextBox ID="TextBox12" runat="server">0.00</asp:TextBox></td>
                    <td style="width: 75px">
                        是否优惠：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList5" runat="server" Width="154px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>是</asp:ListItem>
                            <asp:ListItem>否</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="width: 75px">
                        是否发票：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList6" runat="server" Width="154px">
                            <asp:ListItem></asp:ListItem>
                           
                        </asp:DropDownList></td>
                </tr>

                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        付款金额：</td>
                    <td>
                        <asp:TextBox ID="TextBox31" runat="server"></asp:TextBox></td>
                    <td style="width: 75px">
                        是否到货：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList11" runat="server" Width="154px" >
                       <asp:ListItem></asp:ListItem>
                       <asp:ListItem>否</asp:ListItem>
                       
                         <asp:ListItem>是</asp:ListItem>
                            
                        </asp:DropDownList>
                    </td>
                    <td style="width: 75px">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        备注：</td>
                    <td colspan="5">
                        <asp:TextBox ID="TextBox34" runat="server" Width="90%"></asp:TextBox></td>
                </tr>
                
                </table> 
                </fieldset> 

                  <fieldset>
            <legend style="color: Red">验收信息</legend>


                  <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"   width="100%">
         
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        验收日期：</td>
                    <td>
                        <asp:TextBox ID="TextBox13" runat="server" onclick="new Calendar().show(this.form.TextBox13);"></asp:TextBox></td>
                    <td style="width: 75px">
                        制造商：</td>
                    <td>
                        <asp:TextBox ID="TextBox33" runat="server"></asp:TextBox></td>
                    <td style="width: 75px">
                        使用日期：</td>
                    <td>
                        <asp:TextBox ID="TextBox15" runat="server" onclick="new Calendar().show(this.form.TextBox15);"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        验收人：</td>
                    <td>
                        <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox></td>
                    <td style="width: 75px">
                        是否校准：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList7" runat="server" Width="154px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>是</asp:ListItem>
                            <asp:ListItem>否</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="width: 75px">
                        机身编号：</td>
                    <td>
                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        签字验收人：</td>
                    <td>
                        <strong>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></strong></td>
                    <td style="width: 75px">
                        资产型号：</td>
                    <td>
                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
                    <td style="width: 75px">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        备注：</td>
                    <td colspan="5">
                        <asp:TextBox ID="TextBox35" runat="server" Width="90%"></asp:TextBox></td>
                </tr>
                </table> 
                </fieldset> 
                  <fieldset>
            <legend style="color: Red">校准信息</legend>
                  <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"   width="100%">
         
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        校准日期：</td>
                    <td>
                        <asp:TextBox ID="TextBox18" runat="server" onclick="new Calendar().show(this.form.TextBox18);"></asp:TextBox></td>
                    <td style="width: 75px">
                        校准周期：</td>
                    <td>
                        <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox></td>
                    <td style="width: 75px">
                        使用情况：</td>
                    <td>
                        <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        </td>
                    <td>
                        <asp:DropDownList ID="DropDownList8" Visible ="false"  runat="server" Width="154px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>使用</asp:ListItem>
                            <asp:ListItem>报废</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="width: 75px">
                        仪器状态：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList9" runat="server" Width="154px">
                             <asp:ListItem></asp:ListItem>
                            <asp:ListItem>合格</asp:ListItem>
                            <asp:ListItem>准用</asp:ListItem>
                              <asp:ListItem>停用</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="width: 75px">
                        异常情况：</td>
                    <td>
                        <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        档案号：</td>
                    <td>
                        <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox></td>
                    <td style="width: 75px">
                        有效日期：</td>
                    <td>
                        <asp:TextBox ID="TextBox16" runat="server" onclick="new Calendar().show(this.form.TextBox16);"></asp:TextBox></td>
                    <td style="width: 75px">
                        校准单位：</td>
                    <td>
                        <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        备注：</td>
                    <td colspan="5">
                        <asp:TextBox ID="TextBox24" runat="server" Width="90%"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td colspan="6" style="text-align: center">
                        <asp:Button ID="Button1" runat="server" CssClass="BnCss" OnClick="Button1_Click"
                                    Text="更 新" />
                        <asp:Button ID="Button2" runat="server" CssClass="BnCss" OnClick="Button2_Click"
                                    Text="删 除" /></td>
                </tr>
            </table></fieldset> 
            <hr />
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"   width="100%">
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        附件上传：</td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="70%" /></td>
                </tr>
                <tr bgcolor="#f4faff">
                    <td style="width: 75px">
                        操作：</td>
                    <td>
                        <asp:Button ID="Button3" runat="server" CssClass="BnCss"
                                    Text="上 传" OnClick="Button3_Click" />
                        <asp:Label ID="Label2" runat="server" BackColor="White" ForeColor="Red"></asp:Label>
                        <asp:TextBox ID="TextBox25" runat="server" BackColor="Transparent" BorderStyle="None"
                            Enabled="False" ForeColor="Red" ReadOnly="True"></asp:TextBox></td>
                </tr>
            </table>
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"   width="100%">
                <tr bgcolor="#f4faff">
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="3" DataKeyNames="id" OnRowCommand="GridView1_RowCommand"
                            OnRowDataBound="GridView1_RowDataBound" Style="font-size: 9pt" Width="98%">
                            <RowStyle CssClass="textcenter" ForeColor="#000066" />
                            <FooterStyle BackColor="White" CssClass="headcenter" ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#669966" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="url" DataTextField="filename" HeaderText="下载">
                                    <ItemStyle ForeColor="Green" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="fillname" HeaderText="上传人">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="上传时间">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="浏览" HeaderStyle-Width="60px"> 
                        <ItemTemplate>    
               <span  style ="cursor :hand;color :Blue ;" onclick ="window.open('GoodsInfoFilesSee.aspx?id=<%#Eval("id") %>')"><asp:Label id="seeLB" runat="server" Text="浏览"></asp:Label></span>                                 </ItemTemplate>
                  </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <span id="message" onclick="JavaScript:return confirm('确定要删除吗？')">
                                    <asp:LinkButton ID="LinkButtondelete" runat="server" CommandArgument='<%# Eval("id") %>'
                                        CommandName="shanchu" ForeColor="blue" Text="删除"></asp:LinkButton>
                                </span>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                        </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="暂无相关文档附件"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
                 
            
       </div> 

</form>
</body>
</html>