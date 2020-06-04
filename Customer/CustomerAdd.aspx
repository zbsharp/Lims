<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerAdd.aspx.cs" EnableViewState="true" Inherits="Customer_CustomerAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>客户信息录入</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <%--  <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />--%>
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>

    <script type="text/javascript">
        $(function () {
            $('#kehuname').keyup(function () {
                setTimeout(function () {
                    var name = $('#kehuname').val();
                    if (name == "") {
                        $('.likehu').remove();
                    }
                    if (name.length >= 2) {
                        $.ajax({
                            url: "SelectCustomer.ashx",
                            type: 'get',
                            dataType: 'json',
                            data: { name: name },
                            cache: false,
                            success: function (sb) {
                                if (sb == 'NO') {
                                    $('.likehu').remove();
                                }
                                else {
                                    var list = eval(sb);
                                    $('.likehu').remove();
                                    var str = '';
                                    for (var i = 0; i < list.length; i++) {
                                        str += '<p class=\'likehu\'>客户名称:' + list[i].Customname + ',归属人:' + list[i].Fillname + '</p>';
                                    }
                                    $('.kehu').append(str);
                                }
                            }
                        });
                    }
                    else {
                        $('.likehu').remove();
                    }

                }, 1000);
            });
         });
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 28px;
        }

        /*.autocomplete_completionListElement {
            visibility: hidden;
            margin: 0px !important;
            background-color: inherit;
            color: windowtext;
            border: buttonshadow;
            border-width: 1px;
            border-style: solid;
            cursor: pointer;
            overflow: auto;
            text-align: left;
            list-style-type: none;
            font-family: Verdana;
            font-size: 13px;
            padding: 0;
        }

        .autocomplete_listItem {
            background-color: white;
            padding: 1px;
        }

        .autocomplete_highlightedListItem {
            background-color: #e9f5f7;
            padding: 1px;
        }*/

        .kehu {
            width: 380px;
            background-color: #e9f5f7;
            position: fixed;
            top: 8.5%;
            left: 14%;
        }

        .likehu:hover {
            background-color: white;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="div_All">
            <div class="Body_Title">
                销售管理 》》客户录入
            </div>
            <table class="Admin_Table">
                <tr>
                    <td>客户名称：
                    </td>
                    <td>
                        <asp:TextBox ID="kehuname" runat="server" CssClass="txtHInput" Height="16px" Width="300px" OnTextChanged="kehuname_TextChanged" AutoPostBack="True" autocomplete="off"></asp:TextBox><span style="font-size: 13pt; vertical-align: middle; color: red">*</span>
                        <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="kehuname" CompletionInterval="500" MinimumPrefixLength="2"
                            ServicePath="WebService.asmx" ServiceMethod="GetTextString" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                        </cc1:AutoCompleteExtender>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="kehuname"
                            Display="Dynamic" ErrorMessage="请输入客户名称"></asp:RequiredFieldValidator>
                        <%--<input id="Button5" type="button" onclick="xianshi()" value="申请分派" />--%><span
                            id="Span1" style="display: none; color: red"></span>
                        <div id="divv" style="display: none">
                            <asp:TextBox ID="TextBox1" ToolTip="在上面文本框中输入客户名称后在此处请写明申请理由" runat="server" CssClass="txtHInput"></asp:TextBox><asp:Button ID="Button2"
                                runat="server" Text="提交申请" OnClick="Button5_Click1" CausesValidation="false" />
                        </div>
                    </td>

                </tr>

                <tr>
                    <td>中文地址：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="txtHInput" Width="260px"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>英文地址：
                    </td>
                    <td>
                        <asp:TextBox ID="txt_address_en" runat="server" CssClass="txtHInput" Width="260px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>网址：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="txtHInput" Width="260px"></asp:TextBox>
                    </td>

                </tr>

                <tr>
                    <td>产品类别：
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">

                            <asp:ListItem>IT</asp:ListItem>
                            <asp:ListItem>AV</asp:ListItem>
                            <asp:ListItem>灯具</asp:ListItem>
                            <asp:ListItem>家电</asp:ListItem>


                            <asp:ListItem>电动工具</asp:ListItem>
                            <asp:ListItem>汽车电子</asp:ListItem>
                            <asp:ListItem>电池</asp:ListItem>


                            <asp:ListItem>玩具</asp:ListItem>
                            <asp:ListItem>其他</asp:ListItem>


                        </asp:CheckBoxList>



                    </td>

                </tr>
                <tr>
                    <td>客户类型：
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList5" runat="server" Width="80">
                            <asp:ListItem>制造企业</asp:ListItem>
                            <asp:ListItem>政府机构</asp:ListItem>
                            <asp:ListItem>贸易商</asp:ListItem>
                            <asp:ListItem>代理公司</asp:ListItem>

                        </asp:DropDownList>
                    </td>

                </tr>
                <%-- <tr>
                    <td>客户行业：
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="80" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                            <asp:ListItem Value="ODM">ODM</asp:ListItem>
                            <asp:ListItem Value="OEM">OEM</asp:ListItem>
                            <asp:ListItem Value="贸易商">贸易商</asp:ListItem>
                            <asp:ListItem Value="认证检测">认证检测</asp:ListItem>
                            <asp:ListItem Value="其它">其它</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                </tr>--%>
                <tr>
                    <td nowrap="nowrap">来源途径：
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList3" runat="server" Width="80">

                            <asp:ListItem Value="公司内部">公司内部</asp:ListItem>
                            <asp:ListItem Value="自行开发">自行开发</asp:ListItem>
                            <asp:ListItem Value="客户推荐">客户推荐</asp:ListItem>
                            <asp:ListItem Value="展会">展会</asp:ListItem>
                            <asp:ListItem Value="培训研讨会">培训研讨会</asp:ListItem>
                            <asp:ListItem Value="网络推广">网络推广</asp:ListItem>
                            <asp:ListItem Value="平面媒体">平面媒体</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td>信用情况：
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="80">
                            <asp:ListItem Value="正常">正常</asp:ListItem>
                            <asp:ListItem Value="月结">月结</asp:ListItem>
                            <asp:ListItem Value="欠款">欠款</asp:ListItem>
                            <asp:ListItem Value="黑名单">黑名单</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td>客户等级：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList6" runat="server" Enabled="false" Width="120px" Height="21px">
                            <asp:ListItem Value="普通客户">普通客户</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>备注：
                    </td>
                    <td>
                        <asp:TextBox ID="Intro" runat="server" ToolTip="备注" CssClass="txtHInput" Width="420px"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Button ID="Button4" runat="server" CssClass="BnCss" Text="保存" OnClick="Button4_Click" Visible="false" />
                        &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" CssClass="BnCss" Visible="false" Text="添加联系人" Enabled="False"
                        OnClick="Button1_Click" />
                        <asp:Button ID="Button6" runat="server" CssClass="BnCss" Visible="false" Enabled="False" Text="特殊要求"
                            OnClick="Button6_Click1" /><asp:Button ID="Button3" runat="server"
                                Text="保存,继续增加联系人" OnClick="Button3_Click" />
                    </td>
                </tr>
            </table>
            <div class="kehu"></div>
        </div>
        <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>
