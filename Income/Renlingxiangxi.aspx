<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Renlingxiangxi.aspx.cs" Inherits="Income_Renlingxiangxi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>已认未对</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.9.0.min.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="Body_Title">
            财务管理》》已认未对详细
        </div>
        
        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
            DataKeyNames="id" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="序 号" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("000") %>' ForeColor="Green"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                <asp:BoundField DataField="baojiaid" HeaderText="合同号"  />
                <asp:BoundField DataField="taskid" HeaderText="任务号" />
                <asp:BoundField DataField="type" HeaderText="结算项目" />
                <asp:BoundField DataField="project" HeaderText="结算类别" />
                <asp:BoundField DataField="feiyong" HeaderText="应收金额" />
                <asp:BoundField DataField="beizhu3" HeaderText="工程部门" />
                <asp:BoundField DataField="yiquerenjine" HeaderText="已经确认金额" />
                <asp:BoundField DataField="bencitijiao" HeaderText="本次提交金额" />
                <asp:BoundField DataField="tijiaoren" HeaderText="提交人" />
                <asp:BoundField DataField="tijiaoshijian" HeaderText="提交时间" />
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
        </asp:GridView>
    </form>
</body>
</html>
