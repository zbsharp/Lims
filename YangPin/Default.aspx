<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="YangPin_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="EXCEL" onclick="Button1_Click" />
     <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
                            CssClass="Admin_Table" >
                            <Columns>
                               
                                <asp:BoundField DataField="sampleid" HeaderText="样品编号" />

                                <asp:BoundField DataField="anjianid" HeaderText="任务编号"  />
                              
                                 <asp:BoundField DataField="pub_field3" HeaderText="封存编号"  />
                                 <asp:BoundField DataField="sq" HeaderText="申请编号" />
                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                            <EmptyDataTemplate>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时没有数据"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
    </div>
    </form>
</body>
</html>
