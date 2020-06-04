<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskBeiZhu.aspx.cs" Inherits="ShiXiao_TaskBeiZhu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>昨天和今天的备注信息 </title>
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
    <div>
     <div class="Body_Title">
            时效管理 》》昨天和今天的备注信息</div>


 <asp:GridView ID="GridView1" runat="server" Width="100%" OnRowDataBound="GridView1_RowDataBound"  AutoGenerateColumns="False"
                         CssClass="Admin_Table" >
                        <HeaderStyle CssClass="Admin_Table_Title " />
                        <Columns>

                        <asp:HyperLinkField HeaderText="任务号"  DataTextField ="xiangmuid" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=1"
                    DataNavigateUrlFields="bianhao" />

                           
                            <asp:BoundField DataField="weituodanwei" HeaderText="委托方" />
                            <asp:BoundField DataField="shenqingbianhao" HeaderText="申请编号" />
                              <asp:BoundField DataField="state" HeaderText="状态" />
                           <asp:BoundField DataField="neirong" HeaderText="备注内容" />
                             <asp:BoundField DataField="name" HeaderText="备注人" />
                              <asp:BoundField DataField="time" HeaderText="备注时间" />
                              

                      

            

                  
                    


                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>

    </div>
    </form>
</body>
</html>
