<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="QuotationAdd1.aspx.cs" Inherits="Quotation_QuotationAdd1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户报价</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/QuotationAdd1.css" rel="stylesheet" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/QuotationAdd1.js"></script>
    <script type="text/javascript">
        //function changeTwoDecimal(x) {
        //    var f_x = parseFloat(x);
        //    if (isNaN(f_x)) {
        //        alert('function:changeTwoDecimal->parameter error');
        //        return false;
        //    }
        //    f_x = Math.round(f_x * 100) / 100;

        //    return f_x;
        //}

        //function add() {
        //    var a = parseFloat(document.getElementById("baojiazong_txt").value) * parseFloat(document.getElementById("baojiazhekou_txt").value);

        //    if (a != null) {
        //        document.getElementById("baojiazhehou_txt").value = a;
        //    }
        //}

        //function add2() {
        //    var a = parseFloat(document.getElementById("baojiazhehou_txt").value) / parseFloat(document.getElementById("baojiazong_txt").value);

        //    if (a != null) {

        //        var ab = changeTwoDecimal(a);
        //        document.getElementById("baojiazhekou_txt").value = ab;
        //    }
        //}
    </script>
    <script type="text/javascript">
        var xmlhttp;
        function bianhao() {
            //alert("nihao");

            //var id2=document.getElementById("Text51").value;

            //xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            //xmlhttp.open("GET", "Handler6.ashx?id1=" + Math.round(Math.random() * 100));
            //xmlhttp.onreadystatechange = processs;
            //xmlhttp.send();

            $.ajax({
                //处理ajax请求
                //url:'FindNewMessage.ashx',
                url: 'Handler6.ashx?id1=' + Math.round(Math.random() * 100),
                // 当前用户的ID，这里图省事就省略了，直接写死为 1，
                //实际使用过程中可以从session中获取 。。。。
                //data:{Uid:1},
                cache: false,
                //回调函数返回未读短信数目
                success: function (response) {
                    //$('#messageCount').val(response);
                    //var data1 = xmlhttp.responseText;
                    document.getElementById("Text50").value = response;// data1;
                    document.getElementById("Button11").disabled = true;
                },
                error: function (data) {
                    alert("加载失败");
                }
            });

        }
        function processs() {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {

                var data1 = xmlhttp.responseText;


                document.getElementById("Text50").value = data1;

                document.getElementById("Button11").disabled = true;
            }

        }
    </script>


    <style type="text/css">
        .auto-style1 {
            height: 28px;
        }

        .shiji {
            display: none;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="Body_Title" id="dd">
            <asp:Label ID="Label1" runat="server"></asp:Label>
            》》<asp:Label ID="Label2" runat="server"></asp:Label>
        </div>
        <div style="text-align: center; color: Red; font-size: large;">提示：对报价单做的任何修改，在离开该页面时请点击"保存报价"按钮方能保存修改.</div>
        <fieldset>
            <legend style="color: Red">请选择联系人
                 <asp:DropDownList ID="DropDownList1"
                     runat="server">
                 </asp:DropDownList>

                <input id="Button11" runat="server" type="button" class="BnCss" value="开始报价" style="text-align: left" onclick="bianhao()" /><span style="font-size: medium; vertical-align: middle; color: red">*</span>

                <input id="Text50" type="text" backcolor="#E0E0E0" bordercolor="#E0E0E0" runat="server" readonly="readonly" class="myline" style="width: 145px" />


                <input id="Text51" visible="false" value="<%#name%>" type="text" readonly="readonly" runat="server" class="myline" style="width: 150px" />


            </legend>




            <table class="Admin_Table">
                <tr>
                    <td colspan="7" align="center">
                        <asp:GridView ID="GridView5" runat="server" Width="100%" AutoGenerateColumns="false"
                            DataKeyNames="id"
                            CssClass="Admin_Table">
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
            </table>
        </fieldset>



        <fieldset>
            <legend style="color: Red">产品信息（必填）</legend>
            <table class="Admin_Table">
                <tr>
                    <td align="center">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowDeleting="GridView1_RowDeleting" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" DataKeyNames="id"
                            CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound1" OnRowCreated="GridView1_RowCreated">
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                            CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="id" HeaderText="产品编号" ReadOnly="true" />
                                <asp:BoundField DataField="name" HeaderText="产品名称" />
                                <asp:BoundField DataField="type" HeaderText="产品型号" />
                                <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                <asp:CommandField HeaderText="编辑产品" ShowDeleteButton="true" ShowEditButton="True" />

                                <asp:TemplateField HeaderText="项目"></asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server">
                <table class="Admin_Table">
                    <tr>
                        <td>产品名称：
                        </td>
                        <td>
                            <asp:TextBox ID="cpname_txt" runat="server"></asp:TextBox>
                        </td>
                        <td>产品型号：
                        </td>
                        <td>
                            <asp:TextBox ID="cpxinghao_txt" runat="server"></asp:TextBox>
                        </td>
                        <td>备注：
                        </td>
                        <td>
                            <asp:TextBox ID="cpbeizhu_txt" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="保存产品" OnClick="Button2_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>

        <fieldset>
            <legend style="color: Red">项目信息（必选）</legend>
            <table class="Admin_Table">
                <tr style="display: none">
                    <td>测试说明：
                    </td>
                    <td>
                        <asp:TextBox ID="csnr_txt" runat="server"></asp:TextBox>
                    </td>
                    <td>样品需求;
                    </td>
                    <td>
                        <asp:TextBox ID="csyp_txt" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>周期：
                    </td>
                    <td>
                        <asp:TextBox ID="cszq_txt" runat="server"></asp:TextBox>
                    </td>
                    <td>技术要求：
                    </td>
                    <td>
                        <asp:TextBox ID="csjs_txt" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>标准费用：
                    </td>
                    <td>
                        <asp:TextBox ID="csfy_txt" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'>

                        </asp:TextBox>
                    </td>
                    <td>单件折扣：
                    </td>
                    <td>
                        <asp:TextBox ID="cszk_txt" runat="server" Text="1" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>数量/时间：
                    </td>
                    <td>
                        <asp:TextBox ID="cssl_txt" Text="1" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>
                    </td>
                    <td>备注：
                    </td>
                    <td>
                        <asp:TextBox ID="csbeizhu_txt" runat="server" CssClass="txtHInput"></asp:TextBox>
                    </td>
                </tr>

                <tr style="display: none">
                    <td align="center" colspan="4">

                        <asp:Button ID="Button1" runat="server" Text="增加测试项目" OnClick="Button1_Click" Style="height: 21px" Visible="False" />

                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowDeleting="GridView2_RowDeleting" OnRowCancelingEdit="GridView2_RowCancelingEdit"
                            OnRowEditing="GridView2_RowEditing" OnRowUpdating="GridView2_RowUpdating" DataKeyNames="id"
                            CssClass="Admin_Table" OnRowDataBound="GridView2_RowDataBound1" OnRowCommand="GridView2_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                            CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ceshiname" HeaderText="测试项目" />
                                <asp:TemplateField HeaderText="标准"
                                    SortExpression="biaozhun">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Height="147px"
                                            Text='<%# Bind("biaozhun") %>' Width="100px" Wrap="true" TextMode="MultiLine"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("biaozhun") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                          <%--      <asp:BoundField DataField="neirong" HeaderText="内容" Visible="false" />--%>
                                <asp:BoundField DataField="yp" HeaderText="样品" />
                                <asp:BoundField DataField="zhouqi" HeaderText="周期" />
                                <asp:BoundField DataField="feiyong" HeaderText="实际费用" />
                                <asp:BoundField DataField="zhekou" HeaderText="折扣" ReadOnly="true" />
                                <asp:BoundField DataField="shuliang" HeaderText="数量" />
                                <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                <asp:BoundField DataField="total" HeaderText="小计" ReadOnly="true" DataFormatString="{0:f2}" />
                                <asp:BoundField DataField="yuanshi" ReadOnly="true" HeaderText="标准单价" />
                                <asp:BoundField DataField="cpid" HeaderText="产品编号"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField HeaderText="产品名称" />
                                <asp:BoundField HeaderText="产品型号" />
                                <asp:BoundField DataField="id" HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                                    <ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="epiboly" HeaderText="外包" />
                                <asp:BoundField DataField="epiboly_price" HeaderText="外包金额" />
                                <asp:BoundField DataField="bumen" HeaderText="对应部门" />
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <input type="button" id="BJCP_update" onclick="Update()" value="修改" />
                                        <asp:Button ID="btn_Delete" runat="server" Text="删除" CommandName="btn_Delete" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('你确定要删除吗？');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btncopy" runat="server" Text="复制" CommandName="copy" CommandArgument='<%#Eval("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:TextBox ID="cs_txt" runat="server" Visible="false"></asp:TextBox>
                        <asp:DropDownList Visible="false" ID="DropDownList3" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="Button10" runat="server" Text="增加测试项目" OnClick="Button1_Click" Style="height: 21px" Visible="false" />
                        <%-- <input type="Button" id="ButtonDepAdd" style="display: none" onclick="GetMyValue('cs_txt',window.open('Default4.aspx?kehuid=<%=d%>    >','window','dialogWidth=900px;DialogHeight=600px;status:no;help:no;resizable:yes; dialogTop:100px;edge:raised;'))"
                            value="添加" />--%>
                        <asp:Button ID="Button6" runat="server" Visible="false" Text="OK" OnClick="Button6_Click" />
                        <asp:TextBox ID="csbz_txt" Visible="false" runat="server"></asp:TextBox><span style="color: Red;"></span>
                    </td>
                </tr>
            </table>
        </fieldset>

        <fieldset style="display: none;">
            <%-- 添加外包项目 --%>
            <legend style="color: Red;">外包项目</legend>
            <table class="Admin_Table">
                <tr>
                    <td>选择产品：
                        <asp:DropDownList ID="drop_CP" runat="server" Width="240px"></asp:DropDownList></td>
                    <td>技术要求：
                        <asp:TextBox ID="txt_jishu" runat="server" Width="240px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>测试项目：
                        <asp:TextBox ID="txt_csxiangmu" runat="server" Width="240px"></asp:TextBox></td>
                    <td>周&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 期：
                        <asp:TextBox ID="txt_zhouqi" runat="server" Width="240px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>单价费用：
                        <asp:TextBox ID="txt_csprice" runat="server" Width="240px" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox></td>
                    <td>数&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 量：
                        <asp:TextBox ID="txt_num" runat="server" Width="240px" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>对应部门：
                        
                        <asp:DropDownList ID="DropDownList_xiangmubumen" Width="240px" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 注：
                        <asp:TextBox ID="txt_beizhu" runat="server" Width="240px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>测试标准：
                        <asp:TextBox ID="txt_biaozhun" runat="server" Width="240px"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="drop_Waibu" runat="server" Width="240px" Visible="false">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btn_action" runat="server" Text="保存" OnClick="btn_action_Click" Width="131px" Style="height: 27px" />

                    </td>

                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend style="color: Red">报价信息</legend>
            <table class="Admin_Table">
                <tr>
                    <td>不含外包项目实际报价：
                    <asp:TextBox ID="weituo" runat="server" Visible="false" Width="35px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="baojiazhehou_txt" runat="server" ReadOnly="true" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox>
                    </td>
                    <td>报价备注：
                    </td>
                    <td>
                        <asp:TextBox ID="baojiazhekou_txt" Visible="false" runat="server" Text="1"></asp:TextBox>
                        <asp:TextBox ID="baojiabeizhu_txt" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style1">标准价格总和：</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="baojiazong_txt" runat="server" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox>
                    </td>
                    <td class="auto-style1">外包总金额：</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="TextBox_epibolypricetotal" runat="server" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>含外包项目报价：</td>
                    <td>
                        <asp:TextBox ID="epiboly_price" runat="server" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox></td>
                    <td>整单折扣：</td>
                    <td>
                        <asp:TextBox ID="realzhekou_txt" runat="server" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>优惠后金额：</td>
                    <td>
                        <asp:TextBox ID="TextBox_finalprice" runat="server" OnTextChanged="TextBox_finalprice_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <asp:Label runat="server" ID="lb_txt" Text="输入优惠金额后鼠标点击该文本框外面将会自动计算折扣" Style="color: Red"></asp:Label>
                    </td>
                    <td>报价总金额：</td>
                    <td>
                        <asp:TextBox ID="totalmoney_txt" runat="server" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4">是否含税：
                        <asp:DropDownList ID="drop_vat" runat="server">
                            <asp:ListItem>是</asp:ListItem>
                            <asp:ListItem>否</asp:ListItem>
                            <asp:ListItem>增值税</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                        币种： 
                        <asp:DropDownList ID="drop_currency" runat="server" OnSelectedIndexChanged="drop_currency_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Selected="True">人民币</asp:ListItem>
                            <asp:ListItem>美元</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                        扩展费：
                        <asp:TextBox ID="txt_kuozhanfei" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <b>报价说明</b>
                        <br />
                        报价总金额=不含外包项目实际报价+含外包项目报价，外包总金额是指所有项目中外包的总金额
                                <br />
                        当报价不含优惠金额时：折扣=报价总金额 / 标准总价。  外包比列=外包总价 / 报价总金额<br />
                        当报价含有优惠金额时：折扣=优惠后金额）/ 标准总价。  外包比列=外包总价 / 优惠后金额<br />
                        不审批： 折扣大于或等于8折并且外包比列小于或等于80%   
                        <br />
                        经理审批：折扣在5.5到8折之间（包含5.5折不包含8折）或者外包比列在80%到90%之间（包含90%）
                        <br />
                        总经理审批：折扣小于 5.5折或外包比列大于90%
                        <br />
                        含扩展费的合同需部门经理审核、扩展费超过500元人民币则需要总经理审批
                    </td>
                </tr>
            </table>

        </fieldset>

        <fieldset>
            <legend style="color: Red">条款信息</legend>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="drop_language" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drop_language_SelectedIndexChanged">
                <asp:ListItem Selected="True">中文</asp:ListItem>
                <asp:ListItem>英文</asp:ListItem>
            </asp:DropDownList>&nbsp;<asp:Label ID="Label3" runat="server" Text="如果做英文报价请选择英文条款" Style="color: Red"></asp:Label>
            <table class="Admin_Table">
                <tr>
                    <td>选择条款：
                    </td>
                    <td align="left">
                        <asp:CheckBoxList ID="CheckBoxList9" runat="server" OnDataBound="CheckBoxList9_DataBound">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>条款补充：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">付款方式：<asp:DropDownList ID="drop_way" runat="server">
                        <asp:ListItem>全款</asp:ListItem>
                        <asp:ListItem>首款</asp:ListItem>
                    </asp:DropDownList>
                        首款金额：<asp:TextBox ID="txt_drow" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">银行账户:<asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="True">
                    </asp:DropDownList>
                        <asp:Button ID="Button3" runat="server" Text="保存报价" OnClick="Button3_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <%-- 报价审批信息 --%>
        <fieldset>
            <legend style="color: Red">审批信息</legend>
            <table class="Admin_Table">
                <tr>
                    <td>
                        <tr>
                            <td align="center">
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
                    </td>
                </tr>
            </table>
        </fieldset>
        <%-- 客户协议信息 --%>
        <fieldset>
            <legend style="color: Red">协议信息：</legend>
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
        <asp:Literal ID="ld" runat="server"></asp:Literal>
        <%-- 修改测试项目产品信息模态框 --%>
        <div class="model_1">
            <div class='content'>
                <table class="Admin_Table" align="center">
                    <tr style="display: none;">
                        <td>
                            <asp:TextBox ID="txt_idupdate" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr class="t">
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;测试项目：</td>
                        <td>
                            <asp:TextBox ID="txt_project" runat="server" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox></td>

                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            标准：</td>
                        <td>
                            <asp:TextBox ID="txt_standard" runat="server" Enabled="false" BackColor="#E0E0E0" BorderColor="#E0E0E0"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            样品：</td>
                        <td>
                            <asp:TextBox ID="txt_specimen" runat="server"></asp:TextBox></td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            数量：</td>
                        <td>
                            <asp:TextBox ID="txt_nums" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            周期：</td>
                        <td>
                            <asp:TextBox ID="txt_period" runat="server"></asp:TextBox></td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            费用：</td>
                        <td>
                            <asp:TextBox ID="txt_price" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;技术要求：</td>
                        <td>
                            <asp:TextBox ID="txt_technology" runat="server"></asp:TextBox></td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            产品名称：</td>
                        <td>
                            <asp:DropDownList ID="dropUpdatecp" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;是否外包：</td>
                        <td>
                            <asp:DropDownList ID="DropDownList_epiboly" runat="server">
                                <asp:ListItem Value="非外包">非外包</asp:ListItem>
                                <asp:ListItem Value="外包">外包</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            外包金额：</td>
                        <td>
                            <asp:TextBox ID="TextBox_epiboly_price" runat="server" onkeyup='this.value=this.value.replace(/[^0-9.]/gi,"")'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;备注： </td>
                        <td colspan="3">
                            <asp:TextBox ID="txt_remark" runat="server" Width="450"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td colspan="4" align="center">
                            <asp:Button ID="btn_Update" runat="server" Text="确认修改" OnClick="btn_Update_Click" />
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            <input type="button" id="btn_close" value="取消" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
