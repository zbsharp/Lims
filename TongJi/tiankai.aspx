<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tiankai.aspx.cs" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" Inherits="shoufei_tiankai" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改到帐</title>
    <style type="text/css">
        /*选项卡1*/
        #Tab1
        {
            width: 100%;
            background-color: White;
            margin: 0px;
            padding: 0px;
            margin: 0 auto;
        }
        /*菜单class*/
        .Menubox
        {
            width: 100%;
            height: 28px;
            line-height: 28px;
        }
        .Menubox ul
        {
            margin: 0px;
            padding: 0px;
        }
        .Menubox li
        {
            float: left;
            cursor: pointer;
            width: 115px;
            text-align: center;
            border-top: 1px solid #97C8FB;
            border-right: 1px solid #97C8FB;
            border-left: 1px solid #97C8FB;
            margin-left: 0px;
            height: 28px;
        }
        .Menubox li.hover
        {
            padding: 0px;
            background: #fff;
            width: 116px;
            border-top: 1px solid #97C8FB;
            font-weight: bold;
            background-color: #E1ECF9;
        }
        .Contentbox
        {
            clear: both;
            margin-top: 0px;
            border-top: none;
            height: 100%;
            text-align: center;
            width: 100%;
            background-color: #E1ECF9;
        }
    </style>
    <script type="text/javascript">
<!--
        /*第一种形式 第二种形式 更换显示样式*/
        function setTab(name, cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i);
                var con = document.getElementById("con_" + name + "_" + i);
                menu.className = i == cursel ? "hover" : "";
                con.style.display = i == cursel ? "block" : "none";
            }
        }


//-->
    </script>
    <base target="_self" />
    <link href="../css/main.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function expandcollapse(obj, row) {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);

            if (div.style.display == "none") {
                div.style.display = "block";
                if (row == 'alt') {
                    img.src = "../Images/minus.gif";
                }
                else {
                    img.src = "../Images/minus.gif";
                }
                img.alt = "Close to view other Customers";
            }
            else {
                div.style.display = "block";
                if (row == 'alt') {
                    img.src = "../Images/plus.gif";
                }
                else {
                    img.src = "../Images/plus.gif";
                }
                img.alt = "Expand to show Orders";
            }
        } 
    </script>
    <script type="text/javascript">
        function add() {



            var d = (parseFloat(document.getElementById("TextBox10").value)) * parseFloat(document.getElementById("TextBox12").value);





            var a = d;

            if (a != null) {
                document.getElementById("TextBox7").value = a;
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
    <div style="margin-top: 10px;">
        <table class="usertableborder" cellspacing="1" cellpadding="3" width="100%" align="center"
            border="0">
            <tr>
                <td class="usertablerow2" align="right" style="height: 32px; width: 753px;" colspan="2">
                    <table align="center" bgcolor="#000000" border="0" cellpadding="2" cellspacing="1"
                        class="small" width="95%">
                        <tr>
                            <td class="usertablerow2" colspan="4" align="center" style="color: Red;">
                                此单由文员修改相应收费单的到帐状态
                            </td>
                        </tr>
                        <tr>
                            <td class="usertablerow2">
                                流水帐号：
                            </td>
                            <td class="usertablerow2">
                                <span style="font-size: 13pt; vertical-align: middle; color: red">
                                    <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True"></asp:TextBox></span>
                            </td>
                            <td class="usertablerow2">
                                原始金额：
                            </td>
                            <td class="usertablerow2">
                                <asp:TextBox ID="TextBox10" runat="server" Width="69px"></asp:TextBox><asp:TextBox
                                    ID="TextBox11" runat="server" Width="66px"></asp:TextBox>汇率<asp:TextBox ID="TextBox12"
                                        runat="server" Width="64px" Text="1.0"></asp:TextBox>
                                <asp:TextBox ID="TextBox2" Visible="false" runat="server" ReadOnly="True" Width="20px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="usertablerow2">
                                付款人：
                            </td>
                            <td class="usertablerow2">
                                <asp:TextBox ID="TextBox5" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td class="usertablerow2">
                                付款日期：
                            </td>
                            <td class="usertablerow2">
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="usertablerow2">
                                人民金额：
                            </td>
                            <td class="usertablerow2">
                                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                <asp:TextBox ID="TextBox4" runat="server" Width="51px" ReadOnly="true" Text="RMB"></asp:TextBox>
                            </td>
                            <td class="usertablerow2">
                                付款方式
                            </td>
                            <td class="usertablerow2">
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="134px">
                                    <asp:ListItem Selected="True">转帐</asp:ListItem>
                                    <asp:ListItem>现金</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td class="usertablerow2">
                                项目分款
                            </td>
                            <td class="usertablerow2">
                                <asp:DropDownList ID="DropDownList1" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="usertablerow2">
                                <asp:UpdatePanel ID="UpdatePanel30" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DropDownList2" runat="server">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td class="usertablerow2">
                                <div style="width: 200px;">
                                    <div style="float: left">
                                        金额:</div>
                                    <div style="float: left">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="TextBox9" runat="server" Width="53px"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div style="float: left">
                                        <asp:Button ID="Button2" runat="server" Text="确定" OnClick="Button2_Click1" /></div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="usertablerow2">
                                <asp:DropDownList ID="DropDownList4" runat="server">
                                    <asp:ListItem>报价单号</asp:ListItem>
                                    <asp:ListItem>测试收费单</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="usertablerow2">
                                <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                            </td>
                            <td class="usertablerow2">
                                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="查找" />
                            </td>
                            <td class="usertablerow2">
                            </td>
                        </tr>
                        <tr>
                            <td class="usertablerow2">
                                备注：
                            </td>
                            <td class="usertablerow2" colspan="3">
                                <asp:TextBox ID="TextBox8" runat="server" Width="613px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="usertablerow2">
                                发票抬头：
                            </td>
                            <td class="usertablerow2" colspan="3">
                                <asp:TextBox ID="TextBox3" runat="server" Width="613px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="usertablerow2">
                                发票内容：
                            </td>
                            <td class="usertablerow2" colspan="3">
                                <asp:DropDownList ID="DropDownList3" runat="server" Width="612px">
                                    <asp:ListItem>检测费</asp:ListItem>
                                    <asp:ListItem>测试费</asp:ListItem>
                                    <asp:ListItem>认证费</asp:ListItem>
                                    <asp:ListItem>EMC测试费</asp:ListItem>
                                    <asp:ListItem>按规测试费</asp:ListItem>
                                    <asp:ListItem>化学测试费</asp:ListItem>
                                    <asp:ListItem>ROHS测试费</asp:ListItem>
                                    <asp:ListItem>其它</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="usertablerow2" colspan="4" align="center">
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="确定" />(整笔款已全部分完后可点该确定)<asp:Button
                                    ID="Button4" runat="server" OnClick="Button4_Click" Text="测试到帐" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table class="usertableborder" cellspacing="1" cellpadding="3" width="100%" align="center"
            border="0">
            <tr>
                <td class="usertablerow2" align="right" style="height: 32px; width: 753px;" colspan="2">
                    <div id="Tab1">
                        <div class="Menubox">
                            <ul>
                                <li id="one1" onclick="setTab('one',1,2)" class="hover">报价到款</li><li id="one2" onclick="setTab('one',2,2)">
                                    测试到款</li></ul>
                        </div>
                        <div id="con_one_1">
                            <table align="center" bgcolor="#000000" border="0" cellpadding="2" cellspacing="1"
                                class="small" width="95%">
                                <tr>
                                    <td class="usertablerow2" colspan="4">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="GridView1" Width="100%" OnRowDataBound="GridView1_RowDataBound"
                                                    EnableSortingAndPagingCallbacks="false" AutoGenerateColumns="False" DataKeyNames="id"
                                                    ShowFooter="false" Font-Size="Small" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" runat="server" AllowSorting="True"
                                                    OnRowEditing="GridView1_RowEditing" AllowPaging="True" PageSize="4" OnPageIndexChanging="GridView1_PageIndexChanging">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <HeaderStyle BackColor="#669966" Font-Bold="True" ForeColor="White" />
                                                    <FooterStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <a href="javascript:expandcollapse('div<%# Eval("id") %>', 'one');">
                                                                    <img id="imgdiv<%# Eval("id") %>" alt="Click to show/hide Orders for Customer <%# Eval("id") %>"
                                                                        width="9px" border="0" src="../Images/plus.gif" />
                                                                </a>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="100%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="报价编号" SortExpression="shoufeiid">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerID" Text='<%# Eval("baojiaid") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblCustomerID" Text='<%# Eval("baojiaid") %>' runat="server"></asp:Label>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtCustomerID" Text='' runat="server"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ControlStyle Width="100%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="客户名称(点击)" SortExpression="kehuname">
                                                            <ItemTemplate>
                                                                <%# Eval("kehuname")%></ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCompanyName" Text='<%# Eval("kehuname") %>' runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtCompanyName" Text='' runat="server"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ControlStyle Width="100%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="报价编号" SortExpression="baojiaid" Visible="false">
                                                            <ItemTemplate>
                                                                <%# Eval("baojiaid")%></ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtContactName" Text='<%# Eval("baojiaid") %>' runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtContactName" Text='' runat="server"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ControlStyle Width="100%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="认证费" SortExpression="rquan">
                                                            <ItemTemplate>
                                                                <%# Eval("rquan")%></ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtContactTitle" Text='<%# Eval("rquan") %>' runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtContactTitle" Text='' runat="server"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ControlStyle Width="100%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="测试费" SortExpression="fquan">
                                                            <ItemTemplate>
                                                                <%# Eval("fquan")%></ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtAddress" Text='<%# Eval("fquan") %>' runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddress" Text='' runat="server"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ControlStyle Width="100%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="测试优惠" SortExpression="yzong">
                                                            <ItemTemplate>
                                                                <%# Eval("yzong")%></ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtAddress" Text='<%# Eval("yzong") %>' runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddress" Text='' runat="server"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ControlStyle Width="100%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="是否到帐" DataField="shifoudaozhang" />
                                                        <asp:BoundField HeaderText="已收金额" DataField="name6" />
                                                        <asp:CommandField ShowEditButton="True" EditText="全额到帐" CausesValidation="False">
                                                            <ControlStyle Font-Underline="True" ForeColor="Red" />
                                                        </asp:CommandField>
                                                        <asp:TemplateField HeaderText="Delete" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkDeleteCust" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="linkAddCust" CommandName="AddCustomer" runat="server">Add</asp:LinkButton>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td colspan="100%">
                                                                        <div id="div<%# Eval("id") %>" style="position: relative; left: 20px; overflow: auto;
                                                                            width: 96%">
                                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:GridView ID="GridView2" AllowSorting="true" BackColor="White" Width="100%" Font-Size="9"
                                                                                        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="id"
                                                                                        ShowFooter="false" OnPageIndexChanging="GridView2_PageIndexChanging" OnRowUpdating="GridView2_RowUpdating"
                                                                                        OnRowCommand="GridView2_RowCommand" OnRowEditing="GridView2_RowEditing" GridLines="None"
                                                                                        OnRowUpdated="GridView2_RowUpdated" OnRowCancelingEdit="GridView2_CancelingEdit"
                                                                                        OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting"
                                                                                        OnRowDeleted="GridView2_RowDeleted" BorderStyle="Double" BorderColor="#cc3300">
                                                                                        <RowStyle BackColor="Gainsboro" />
                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                        <HeaderStyle BackColor="#cc3300" ForeColor="White" />
                                                                                        <FooterStyle BackColor="White" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="项目编号" Visible="false" SortExpression="beizhu2" HeaderStyle-HorizontalAlign="Left"
                                                                                                ControlStyle-Width="100%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblOrderID" Text='<%# Eval("beizhu") %>' runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:Label ID="lblOrderID" Text='<%# Eval("beizhu") %>' runat="server"></asp:Label>
                                                                                                </EditItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="产品名称" SortExpression="name" HeaderStyle-HorizontalAlign="Left"
                                                                                                ControlStyle-Width="100%">
                                                                                                <ItemTemplate>
                                                                                                    <%# Eval("name")%></ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtFreight" Text='<%# Eval("name")%>' runat="server"></asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                                <FooterTemplate>
                                                                                                    <asp:TextBox ID="txtFreight" Text='' runat="server"></asp:TextBox>
                                                                                                </FooterTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="认证项目" SortExpression="xiaolei" HeaderStyle-HorizontalAlign="Left"
                                                                                                ControlStyle-Width="100%">
                                                                                                <ItemTemplate>
                                                                                                    <%# Eval("xiaolei")%></ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtShipperName" Text='<%# Eval("xiaolei")%>' runat="server"></asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                                <FooterTemplate>
                                                                                                    <asp:TextBox ID="txtShipperName" Text='' runat="server"></asp:TextBox>
                                                                                                </FooterTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="单位">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("rdanwei") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="5%" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="renzhengfei" HeaderText="认证费"></asp:BoundField>
                                                                                            <asp:TemplateField HeaderText="单位">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("fdanwei") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="5%" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="fuwufei" HeaderText="服务费"></asp:BoundField>
                                                                                            <asp:BoundField DataField="ren" HeaderText="优惠后服务费"></asp:BoundField>
                                                                                            <asp:BoundField DataField="qkfwqk" HeaderText="是否全额付款" Visible="false" />
                                                                                            <asp:BoundField DataField="shoukuan" HeaderText="到帐金额" />
                                                                                            <asp:BoundField DataField="qkrzqk" HeaderText="付款日期" />
                                                                                            <asp:CommandField ShowEditButton="false" EditText="到帐" ControlStyle-ForeColor="Red"
                                                                                                ControlStyle-Font-Underline="true" CausesValidation="False" />
                                                                                        </Columns>
                                                                                        <EmptyDataTemplate>
                                                                                            <div style="text-align: center; color: Red;">
                                                                                                没有录入项目信息</div>
                                                                                        </EmptyDataTemplate>
                                                                                    </asp:GridView>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings Visible="False" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HTWConnectionString1 %>"
                                            SelectCommand="SELECT * FROM [shoufeidan]"></asp:SqlDataSource>
                                        <asp:LinkButton ID="btnFirst" OnClick="PagerButtonClick" runat="server" Font-Size="8pt"
                                            ForeColor="navy" CommandArgument="0"></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="btnPrev" OnClick="PagerButtonClick" runat="server" Font-Size="8pt"
                                            ForeColor="navy" CommandArgument="prev"></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="btnNext" OnClick="PagerButtonClick" runat="server" Font-Size="8pt"
                                            ForeColor="navy" CommandArgument="next"></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="btnLast" OnClick="PagerButtonClick" runat="server" Font-Size="8pt"
                                            ForeColor="navy" CommandArgument="last"></asp:LinkButton>
                                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="con_one_2">
                            <asp:Button ID="Button10" runat="server" CssClass="BnCss" Text="确定到帐" OnClick="Button10_Click"
                                Visible="False" /><asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView6" runat="server" Width="100%" AutoGenerateColumns="False"
                                            DataKeyNames="jiludanhao" CellPadding="4" Style="font-size: 9pt" ForeColor="#333333"
                                            GridLines="None">
                                            <RowStyle CssClass="textcenter" BackColor="#EFF3FB" />
                                            <FooterStyle CssClass="headcenter" BackColor="#507CD1" ForeColor="White" Font-Bold="True">
                                            </FooterStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>'
                                                            CommandArgument='<%# Eval("jiludanhao") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        全选<asp:CheckBox ID="CheckBox2" runat="server" OnCheckedChanged="CheckBox2_CheckedChanged"
                                                            AutoPostBack="True" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ceshihao" HeaderText="EMC单号" />
                                                <asp:BoundField DataField="kehuname" HeaderText="客户" ReadOnly="True" />
                                                <asp:BoundField DataField="ceshiname" HeaderText="项目" />
                                                <asp:BoundField DataField="danjia" HeaderText="单价" />
                                                <asp:BoundField DataField="ceshitime" HeaderText="日期" DataFormatString="{0:d}" HtmlEncode="False" />
                                                <asp:BoundField DataField="time" HeaderText="时长" />
                                                <asp:BoundField DataField="songjian" HeaderText="送检" />
                                                <asp:BoundField DataField="ceshigongchengshi" HeaderText="工程师" />
                                                <asp:BoundField DataField="yfeiyong" HeaderText="原始价" />
                                                <asp:BoundField DataField="zhekou" HeaderText="折扣" />
                                                <asp:BoundField DataField="hfeiyong" HeaderText="折后价" />
                                                <asp:BoundField DataField="shoufei" HeaderText="收费单" />
                                                <asp:BoundField DataField="daozhang" HeaderText="到帐" />
                                                <asp:BoundField DataField="id" HeaderText="id" Visible="False" />
                                                <asp:CommandField HeaderText="删除" Visible="False" ShowDeleteButton="True" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div style="text-align: center; color: Red;">
                                                    无测试信息</div>
                                            </EmptyDataTemplate>
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Button10" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
