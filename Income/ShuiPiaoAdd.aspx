<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShuiPiaoAdd.aspx.cs" Inherits="Income_ShuiPiaoAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>现金录入</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>

        <script type="text/javascript" src="../js/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">

    <div class="Body_Title">
        财务管理 》》现金录入</div>


    <div>

     <table align="center" class="Admin_Table"
                        width="100%">
                        <tr style="display:none;">
                            <td style="text-align: left; width: 90px;">
                                是否需要确认：</td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="DropDownList3" runat="server">
                                    <asp:ListItem>是</asp:ListItem>
                                    <asp:ListItem>否</asp:ListItem>
                                </asp:DropDownList>
                                （非财务人员录入现金请选择是）</td>
                            <td style="text-align: left; width: 110px;">&nbsp;</td>
                            <td style="text-align: left">
                                &nbsp;</td>
                        </tr>
                        
                        <tr  >
                            <td style="text-align: left; width: 90px;">
                                付款人：</td>
                            <td style="text-align: left">
                            <asp:TextBox ID="cp" runat="server" ></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 110px;">付款日期：
                                </td>
                            <td style="text-align: left"> 
                               <asp:TextBox ID="guige" runat="server" autocomplete="off" onclick="new Calendar().show(this.form.guige);" ></asp:TextBox>
                            </td>
                        </tr>
                        
                          <tr  >
                            <td style="text-align: left; width: 90px;">
                                付款金额：</td>
                            <td style="text-align: left">
                            <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 110px;">单位：
                                </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="DropDownList2"  runat="server">
                                    <asp:ListItem>人民币</asp:ListItem>
                                    <asp:ListItem>美元</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        
                          <tr  >
                            <td style="text-align: left; width: 90px;">
                                付款方式： </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                   
                                     <asp:ListItem>转账</asp:ListItem>
                                    <asp:ListItem>现金</asp:ListItem>
                                    <asp:ListItem>支票</asp:ListItem>
                                    <asp:ListItem>Pos机</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 110px;">备注：</td>
                            <td style="text-align: left">
                               <asp:TextBox ID="TextBox4" runat="server"  ></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr><td colspan ="4" align ="center" ><asp:Button ID="Button1" runat="server" Text="保存" onclick="Button1_Click" /></td></tr>
                        
                        <tr><td colspan ="4" align ="center" >
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                CssClass="Admin_Table" DataKeyNames="id" 
                                 OnRowDeleting="GridView1_RowDeleting" 
                                Width="100%" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="序 号" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" 
                                                CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green" 
                                                Text='<%# (Container.DisplayIndex+1).ToString("000") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Green" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="liushuihao" HeaderText="流水号" ReadOnly="True" />
                                    <asp:BoundField DataField="fukuanren" HeaderText="付款人" />
                                    <asp:BoundField DataField="fukuanriqi" HeaderText="付款日期" />
                                    <asp:BoundField DataField="danwei" HeaderText="币种" />
                                    <asp:BoundField DataField="fukuanjine" HeaderText="付款金额" />
                                      <asp:BoundField DataField="fukuanfangshi" HeaderText="付款方式" />
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                    <asp:BoundField DataField="daoruren" HeaderText="导入人" />
                                    <asp:BoundField DataField="daorutime" DataFormatString="{0:d}" 
                                        HeaderText="导入时间" />
                                    <asp:BoundField DataField="fapiaoleibie" HeaderText="批次" />

                                     <asp:HyperLinkField DataNavigateUrlFields="liushuihao" HeaderText="开票" Target="_blank" DataNavigateUrlFormatString="FapiaoAdd.aspx?liushuihao={0}"
                                Text="开票" Visible="False" />
                                    <asp:CommandField DeleteText="删除" ShowDeleteButton="true" Visible="false"/>
                                </Columns>
                                <HeaderStyle CssClass="Admin_Table_Title " />
                            </asp:GridView>
                            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                            </td></tr>
                        </table> 


        
    </div>
    </form>
</body>
</html>
