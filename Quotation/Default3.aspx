<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <%-- 模态框样式 --%>
    <link href="../Web_CSS/QuotationAdd1.css" rel="stylesheet" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript">
        function RtValue(rtstr) {
            window.returnValue = rtstr;
            window.close();
        }
        //修改外包项目
        function Model_Update() {
            var gridview = document.getElementById("GridView1");
            var len = $("input:checkbox:checked").length;//查看用户选择了几个复选框
            if (len > 1) {
                alert('一次只能修改单个测试项目！');
                return;
            };
            if (len == 0) {
                alert('请选择一个外包项目！');
                return;
            };
            for (var i = 1; i < gridview.rows.length; i++) {
                var cb = gridview.rows(i).cells(0).children(0);
                if (cb.checked) {
                    var epiboly = gridview.rows(i).cells(9).innerText;
                    if (epiboly == '外包') {
                        var epiboly_neirongid = gridview.rows(i).cells(1).innerText;
                        var epiboly_neirong = gridview.rows(i).cells(2).innerText;
                        var epiboly_yp = gridview.rows(i).cells(5).innerText;
                        var epiboly_biaozhun = gridview.rows(i).cells(3).innerText;
                        var epiboly_shoufei = gridview.rows(i).cells(6).innerText;
                        var epiboly_danwei = gridview.rows(i).cells(7).innerText;
                        var epiboly_zhouqi = gridview.rows(i).cells(4).innerText;
                        var epiboly_beizhu = gridview.rows(i).cells(8).innerText;
                        var id = gridview.rows(i).cells(10).innerText;
                        $("#epiboly_neirongid").val('' + epiboly_neirongid + '');
                        $("#epiboly_neirong").val('' + epiboly_neirong + '');
                        $("#epiboly_yp").val('' + epiboly_yp + '');
                        $("#epiboly_biaozhun").val('' + epiboly_biaozhun + '');
                        $("#epiboly_shoufei").val('' + epiboly_shoufei + '');
                        $("#epiboly_danwei").val('' + epiboly_danwei + '');
                        $("#epiboly_zhouqi").val('' + epiboly_zhouqi + '');
                        $("#epiboly_beizhu").val('' + epiboly_beizhu + '');
                        $("#epiboly_id").val('' + id + '');
                        //显示模态框
                        $('.epiboly_model_1').css({ 'display': 'block' });
                        //隐藏网页的滚动条
                        $("body").css({ "overflow": "hidden" });

                    }
                    else {
                        alert('此操作只允许外包项目');
                        return;
                    }
                }
            }
        }

        $("#Button4").click(function () {
            $(".epiboly_model_1").hide();
        });
    </script>
    <script language="javascript" type="text/javascript">
        function OnTreeNodeChecked() {
            var Obj = event.srcElement;
            if (Obj.tagName == "A" || Obj.tagName == "a") {
                alert(Obj.id);//访问被点击结点的值，同样可以通过Obj.innerText设置它的值   
                window.returnValue = Obj.id;
                window.close()
                return;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lbname" runat="server" Text="产品名称：" Visible="false"></asp:Label><asp:Literal ID="lie_name" runat="server"></asp:Literal>
            &nbsp;
            <asp:Label ID="lbtype" runat="server" Text="型号：" Visible="false"></asp:Label>
            <asp:Literal ID="lie_type" runat="server"></asp:Literal>

            <%--            <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="144px">
            </asp:DropDownList>--%>

            <div style="text-align: center">

                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="保存项目" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button_close" runat="server" Text="关闭"
                    OnClick="Button_close_Click" />
                <asp:Label ID="Label_notice" runat="server" Text=""></asp:Label>
                <%--<input type="button" onclick="Model_Update()" value="编辑外包项目" />--%>
            </div>
            <asp:GridView ID="GridView1" runat="server" DataKeyNames="id" AutoGenerateColumns="False" CssClass="Admin_Table" Width="100%" Height="123px" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            请选择
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="leibiename" HeaderText="部门" />
                    <asp:BoundField DataField="chanpinname" HeaderText="二级分类" />
                    <asp:BoundField DataField="neirongid" HeaderText="项目编号" />
                    <asp:BoundField DataField="neirong" HeaderText="测试项目" HtmlEncode="False">
                        <HeaderStyle Width="200px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="biaozhun" HeaderText="依据标准" />
                    <asp:BoundField DataField="zhouqi" HeaderText="周期" />
                    <asp:BoundField DataField="yp" HeaderText="样品数量" />
                    <asp:BoundField DataField="shoufei" HeaderText="费用" />
                    <asp:BoundField DataField="danwei" HeaderText="单位" />
                    <asp:BoundField DataField="beizhu" HeaderText="说明" />
                    <asp:BoundField DataField="epiboly" HeaderText="项目类型" />
                    <asp:BoundField DataField="id" HeaderText="id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                </Columns>
                <HeaderStyle CssClass="Admin_Table_Title " />
            </asp:GridView>
        </div>
        <%-- 模态框  修改外包项目信息 --%>
        <div class="epiboly_model_1">
            <div class='epiboly_content'>
                <div class="Body_Title">
                    报价管理 》》修改外包测试项目
                </div>
                <table align="center" class="Admin_Table">
                    <tr>
                        <td>项目编号：</td>
                        <td>
                            <%-- 用于保存id，不显示 --%>
                            <asp:TextBox ID="epiboly_id" runat="server" CssClass="hidden"></asp:TextBox>
                            <asp:TextBox ID="epiboly_neirongid" runat="server" onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td>&nbsp;项目名称：</td>
                        <td>
                            <asp:TextBox ID="epiboly_neirong" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>样品：  </td>
                        <td>
                            <asp:TextBox ID="epiboly_yp" runat="server"></asp:TextBox>
                        </td>
                        <td>标准： </td>
                        <td>
                            <asp:TextBox ID="epiboly_biaozhun" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>费用：</td>
                        <td>
                            <asp:TextBox ID="epiboly_shoufei" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>(数字类型)
                        </td>
                        <td>单位：</td>
                        <td>
                            <asp:TextBox ID="epiboly_danwei" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>周期：</td>
                        <td>
                            <asp:TextBox ID="epiboly_zhouqi" runat="server"></asp:TextBox>
                        </td>
                        <td>备注： </td>
                        <td>
                            <asp:TextBox ID="epiboly_beizhu" runat="server" Width="500"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="middle" height="60px">
                        <td align="center" colspan="4">&nbsp;<asp:Button ID="Button3" CssClass="BnCss" runat="server" Text="修改" Width="53px" OnClick="Button3_Click" />
                            &nbsp;<asp:Button ID="Button4" CssClass="BnCss" runat="server" Text="取消" Width="53px" /></td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <fieldset>
            <legend style="color: red;">已添加的项目</legend>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" Width="100%">
                <Columns>
                    <asp:BoundField DataField="bumen" HeaderText="部门" />
                    <asp:BoundField DataField="ceshiname" HeaderText="测试项目" />
                    <asp:BoundField DataField="biaozhun" HeaderText="标准" />
                    <asp:BoundField DataField="feiyong" HeaderText="费用" />
                </Columns>
                <HeaderStyle CssClass="Admin_Table_Title" />
            </asp:GridView>

        </fieldset>
    </form>
</body>
</html>
