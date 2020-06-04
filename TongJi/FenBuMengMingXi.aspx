<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FenBuMengMingXi.aspx.cs" Inherits="TongJi_FenBuMengMingXi" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
      <script type="text/javascript" src="../js/calendar.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js">
    
    	
    
    
    
    </script>
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
    <form name="form1" runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
    <div>
        <div class="Body_Title">
            统计管理 》》按部门查询客户结算费用1</div>
       结算日期： <input id="txFDate" runat="server" class="TxCss" name="txFDate" onclick="new Calendar().show(this.form.txFDate);"
            style="width: 90px" type="text" visible="true" />
        到
        <input id="txTDate" runat="server" class="TxCss" name="txTDate" onclick="new Calendar().show(this.form.txTDate);"
            style="width: 90px" type="text" visible="true" />
        <asp:DropDownList ID="DropDownList1" AutoPostBack="true"  runat="server" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged" >
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList2"   runat="server">
        </asp:DropDownList>
        &nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="111px" Visible ="false" ></asp:TextBox>
        <asp:Button ID="Button2" CssClass="BnCss" runat="server" OnClick="Button2_Click"
            Text="查询" /><asp:Button ID="Button1"    runat="server" 
            onclick="Button1_Click" Text="导出excel" />
        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
             CssClass="Admin_Table"  ShowFooter ="true" 
            OnRowDataBound="GridView1_RowDataBound">
            <Columns>
          
                 

                 <asp:BoundField DataField="kehuid" HeaderText="客户编号" />
                    <asp:BoundField DataField="" HeaderText="客户名称" />
                        <asp:BoundField DataField="pinzheng" HeaderText="结算单号" />
                          <asp:BoundField DataField="beizhu3" HeaderText="结算部门" />
                            <asp:BoundField DataField="jine" HeaderText="部门金额" />

                             <asp:BoundField DataField="tichenriqi" DataFormatString ="{0:d}" HeaderText="结算日期" />
                        <asp:BoundField DataField="taskid" HeaderText="任务号" />
                      
                     <asp:HyperLinkField HeaderText="查看任务" Text="查看任务" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=1"
                    DataNavigateUrlFields="bianhao" />
                  
                   <asp:BoundField DataField="" HeaderText="工程师" />
               <asp:BoundField DataField="" HeaderText="流水号" />
               <asp:BoundField DataField="" HeaderText="备注" />
            </Columns>
            <HeaderStyle CssClass="Admin_Table_Title " />
            <EmptyDataTemplate>
                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
            CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
            InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
            PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
            Style="font-size: 9pt" UrlPaging="True" PageSize="15">
        </webdiyer:AspNetPager>
    </div>
    <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>
