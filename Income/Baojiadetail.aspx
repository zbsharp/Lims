<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Baojiadetail.aspx.cs" Inherits="Income_Baojiadetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报价详细信息</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <legend style="color: red;">报价信息</legend>
                <asp:GridView ID="GV_baojia" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="customer" HeaderText="客户名称" />
                        <asp:BoundField DataField="filltime" HeaderText="报价时间" />
                        <asp:BoundField DataField="huiqiantime" HeaderText="回签时间" />
                        <asp:BoundField DataField="Discount" HeaderText="折扣" />
                        <asp:BoundField DataField="zhehoujia" HeaderText="实际价格" />
                        <asp:BoundField DataField="coupon" HeaderText="优惠后金额" />
                        <asp:BoundField DataField="epiboly_Price" HeaderText="外包金额" />
                        <asp:BoundField DataField="paymentmethod" HeaderText="付款方式" />
                        <asp:BoundField DataField="currency" HeaderText="币种" />
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title" />
                </asp:GridView>
            </fieldset>
            <br />
            <fieldset>
                <legend style="color: red;">项目信息</legend>
                <asp:GridView ID="GV_xm" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="xiaoid" HeaderText="项目编号" />
                        <asp:BoundField DataField="cpname" HeaderText="产品名称" />
                        <asp:BoundField DataField="ceshiname" HeaderText="测试项目" />
                        <asp:BoundField DataField="biaozhun" HeaderText="测试标准" />
                        <asp:BoundField DataField="yp" HeaderText="样品" />
                        <asp:BoundField DataField="feiyong" HeaderText="费用" />
                        <asp:BoundField DataField="shuliang" HeaderText="数量" />
                        <asp:BoundField DataField="total" HeaderText="合计" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="epiboly" HeaderText="是否外包" />
                        <asp:BoundField DataField="epiboly_Price" HeaderText="外包金额" />
                        <asp:BoundField DataField="bumen" HeaderText="部门" />
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title" />
                </asp:GridView>
            </fieldset>
            <br />
            <fieldset>
                <legend style="color:red">
                    核算完成信息
                </legend>
                <asp:GridView ID="GV_hs" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="Admin_Table">
                    <Columns>
                        <asp:BoundField DataField="number" HeaderText="项目编号" />
                        <asp:BoundField DataField="name" HeaderText="测试项目" />
                        <asp:BoundField DataField="biaozhun" HeaderText="测试标准" />
                        <asp:BoundField DataField="money" HeaderText="核算费用" />
                        <asp:BoundField DataField="dempert" HeaderText="部门" />
                        <asp:BoundField DataField="fillname" HeaderText="核算认" />
                        <asp:BoundField DataField="filltime" HeaderText="核算时间" />
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title"/>
                </asp:GridView>
            </fieldset>
            <br />
            <fieldset>
                <legend style="color: red">实验室基准项目信息</legend>
                <asp:GridView ID="GV_jz" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="项目编号" />
                        <asp:BoundField DataField="leibiename" HeaderText="实验室" />
                        <asp:BoundField DataField="chanpinname" HeaderText="产品名称" />
                        <asp:BoundField DataField="neirong" HeaderText="项目名称" />
                        <asp:BoundField DataField="biaozhun" HeaderText="标准" />
                        <asp:BoundField DataField="shoufei" HeaderText="标准价格" />
                        <asp:BoundField DataField="yp" HeaderText="样品" />
                        <asp:BoundField DataField="beizhu" HeaderText="备注" />
                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title" />
                </asp:GridView>
            </fieldset>
        </div>
    </form>
</body>
</html>
