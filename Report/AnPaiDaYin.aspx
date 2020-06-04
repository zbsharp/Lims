<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnPaiDaYin.aspx.cs" Inherits="Report_AnPaiDaYin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">


    <head id="Head1" runat="server">
    <title>安排打印</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>

</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="Body_Title">
       报告管理 》》安排打印</div>

        <div>
        
         <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="False"
                        DataKeyNames="id">
                        <Columns>


                              <asp:TemplateField  Visible ="false" >
                            <HeaderTemplate>
                                请选择
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                        CommandArgument='<%# Eval("id") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle ForeColor="Green" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="rwid" HeaderText="任务编号" />
                            <asp:BoundField DataField="bumen" HeaderText="部门" />
                         
                             <asp:BoundField DataField="jieshouname" HeaderText="接收人" />
                              <asp:BoundField DataField="jieshoutime" HeaderText="接收日期" />
                               <asp:BoundField DataField="beizhu" HeaderText="备注" />
                          
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
        
        </div>
        <div style ="text-align :center;">
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
        </div>
        <div style ="text-align :center;">
            <asp:Button ID="Button1" runat="server" Text="保存安排信息" onclick="Button1_Click" /></div>
            <asp:Literal ID="ld" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>

