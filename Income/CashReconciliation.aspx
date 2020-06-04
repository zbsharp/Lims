<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashReconciliation.aspx.cs" Inherits="Income_CashReconciliation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>财务对账</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>

    <script type="text/javascript">


        var currentRowId = 0;
        function SelectRow() {
            if (event.keyCode == 40)
                MarkRow(currentRowId + 1);
            else if (event.keyCode == 38)
                MarkRow(currentRowId - 1);
        }

        function MarkRow(rowId) {
            if (document.getElementById(rowId) == null)
                return;

            if (document.getElementById(currentRowId) != null)
                document.getElementById(currentRowId).style.backgroundColor = '#ffffff';

            currentRowId = rowId;
            document.getElementById(rowId).style.backgroundColor = '#FFE0C0';
        }
        function text() {
            document.getElementById("bnClick").click();
        }




    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            财务管理 》》对账
        </div>
        <fieldset>
            <legend style="color: red;">业务认领信息111</legend>
            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="Admin_Table">
                <Columns>
                    <asp:BoundField DataField="liushuihao" HeaderText="流水号" />
                    <asp:BoundField DataField="remark" HeaderText="认领信息" />
                    <asp:BoundField DataField="fillname" HeaderText="认领认" />
                    <asp:BoundField DataField="filltime" HeaderText="认领时间" />
                </Columns>
                <HeaderStyle CssClass="Admin_Table_Title" />
            </asp:GridView>
        </fieldset>
        <div>
            <table class="Admin_Table" width="100%">
                <tr>
                    <td align="left" colspan="4">付款客户：<asp:Label ID="Label1" runat="server" Text=""></asp:Label>--付款金额：<asp:Label ID="Label2"
                        runat="server" Text=""></asp:Label>--已分金额：<asp:Label ID="Label3" runat="server" Text=""></asp:Label>--付款日期：<asp:Label
                            ID="Label4" runat="server" Text=""></asp:Label>-
                        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td align="left" colspan="4">客户名称或任务号或联系人或付款单位或委托单位
                    <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True"></asp:TextBox>
                        <asp:TextBox ID="TextBox3" runat="server" Enabled="false" Visible="false"></asp:TextBox>

                        <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />
                    </td>
                </tr>

                <tr>
                    <td align="left" colspan="4" style="text-align: center">
                        <asp:GridView ID="GridView1" ShowFooter="True" runat="server"
                            class="Admin_Table" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound"
                            OnRowCommand="GridView1_RowCommand">
                            <Columns>


                                <asp:BoundField DataField="inid" HeaderText="收费编号" />
                                <asp:BoundField DataField="taskno" HeaderText="任务编号" />
                                <asp:BoundField DataField="kehuname" HeaderText="客户" />


                                <asp:BoundField DataField="feiyong" HeaderText="收费单金额" />
                                <asp:BoundField DataField="name" HeaderText="客户联系人" />
                                <asp:BoundField DataField="fillname" HeaderText="开单人" />


                                <asp:BoundField DataField="hesuanbiaozhi" HeaderText="对账记录" />
                                <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="开单日期" />




                                <asp:TemplateField HeaderText="收费项">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="明细" CommandArgument='<%# Eval("inid") %>'
                                            CommandName="showDetail"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>


                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center">收费单明细:
                    <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table">

                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                        CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="id" HeaderText="编号" />

                            <asp:BoundField DataField="taskid" HeaderText="任务号" />
                            <asp:BoundField DataField="type" HeaderText="项目" />
                            <asp:BoundField DataField="baojia" HeaderText="报价" />
                            <asp:BoundField DataField="zhekou" HeaderText="折扣" />
                            <asp:BoundField DataField="feiyong" HeaderText="应收" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="shishou" HeaderText="已对金额" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="shoufeibianhao" DataFormatString="{0:d}" HeaderText="收费编号" ReadOnly="true" />
                            <asp:BoundField DataField="beizhu2" HeaderText="备注" />
                            <asp:BoundField DataField="beizhu3" HeaderText="类别" />
                            <asp:BoundField DataField="fillname" HeaderText="录入人" ReadOnly="true" />
                            <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="时间" ReadOnly="true" />

                            <asp:BoundField DataField="heduibiaozhi" HeaderText="对账记录" />




                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
                        如有代付，请先为代付分款再分款其他项目,分款顺序按照序号对勾选的项目顺延分款，直到剩余款项用完，停止继续分款<br />
                        <asp:Button ID="Button_addcash" runat="server" Text="为勾选项目分款"
                            OnClick="Button_addcash_Click" />
                    </td>
                </tr>
                <tr>
                    <td>已分款金额：
                    <asp:GridView ID="GridView3" runat="server" CssClass="Admin_Table"
                        AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="id" OnRowDataBound="GridView3_RowDataBound"
                        OnRowDeleting="GridView3_RowDeleting">

                        <Columns>
                            <asp:TemplateField HeaderText="序 号" Visible="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("0000") %>'
                                        CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="taskid" HeaderText="任务编号" />
                            <asp:BoundField DataField="dengji2" HeaderText="收费单号" />



                            <asp:BoundField DataField="name" HeaderText="分款人" />

                            <asp:BoundField DataField="riqi" DataFormatString="{0:d}" HeaderText="分款日期" />
                            <asp:BoundField DataField="tichenbiaozhi" HeaderText="已确认" />
                            <asp:BoundField DataField="tichenriqi" HeaderText="确认日期" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="xiaojine" HeaderText="分款金额" />

                            <asp:BoundField DataField="beizhu3" HeaderText="类别" />
                            <asp:CommandField ShowDeleteButton="true" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />

                        <EmptyDataTemplate>
                            <div style="color: Red;">
                                无到账信息
                            </div>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>

                    </td>

                </tr>

                <tr>
                    <td>
                        <asp:Button ID="Button3" runat="server" Text="确认完成分款" OnClick="Button3_Click" OnClientClick="return confirm('确认要完成吗？完成后不能删除已确认分款金额');" />
                    </td>
                </tr>

            </table>
        </div>
        <asp:Literal ID="ld" runat="server"></asp:Literal>

    </form>
</body>
</html>
