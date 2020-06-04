<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cashin2.aspx.cs" Inherits="Income_Cashin2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" 
                       >
                        <Columns>
                           

                            <asp:BoundField DataField="taskid" HeaderText="任务编号" />
                            
                            <asp:BoundField DataField="fukuanriqi" DataFormatString="{0:d}" HeaderText="付款日期" />
                            <asp:BoundField DataField="fukuanren" HeaderText="付款方" />
                           
                            <asp:BoundField DataField="xiaojine" HeaderText="付款金额" />

                          

                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>
    </div>
    </form>
</body>
</html>
