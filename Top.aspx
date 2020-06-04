<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Top.aspx.cs" Inherits="Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="JavaScript/ManageWeb.js"></script>
    <script language="JavaScript" type="text/javascript">

        function ComfirmExit() {
            var myconfirm = confirm("确实要重新登录吗？");

        }
		
    </script>
    <script language="javascript" type="text/javascript">
        function Show(url, urls) {


            top.leftFrame.location = url;
            top.main.location = urls;

        }
		
    </script>

   


</head>
<body>
    <form id="form1" runat="server">
    <asp:TextBox ID="TextBox1" runat="server"  Visible ="false" ></asp:TextBox>

    <div class="TopBack">
        <div class="TopMes">
            祝你工作愉快：<font color="red"><asp:Label ID="Label1" runat="server" Text=""></asp:Label></font>&nbsp;
            <asp:LinkButton ID="LBQuit" runat="server" Font-Bold="True" ForeColor="White"
                target="_self" OnClientClick='ComfirmExit(2)' onclick="LBQuit_Click">[退出]</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp; <a href="#" onclick="Show('Menu.aspx?Menuid=2','WelCome.aspx?Menuid2=2')">
                <font style="font-weight: bolder; color: White">[客户管理]</font></a> <span style="color: Red;">
                    --<%--</span><span style="margin-right: 0px;"> <a href="#" onclick="Show('Menu.aspx?Menuid=6','WelCome.aspx?Menuid2=6')">
                        <font style="font-weight: bolder; color: White">[报价管理]</font></a> </span>--%>
           <%-- <span style="color: Red;">--</span> <a href="#" onclick="Show('Menu.aspx?Menuid=8','WelCome.aspx?Menuid2=8')">
                <font style="font-weight: bolder; color: White">[业务管理]</font></a> </span> <span style="color: Red;">
                    --</span> <a href="#" onclick="Show('Menu.aspx?Menuid=3','WelCome.aspx?Menuid2=3')">
                        <font style="font-weight: bolder; color: White">[工程管理]</font></a> <span style="color: Red;">
                            --</span> <a href="#" onclick="Show('Menu.aspx?Menuid=9','WelCome.aspx?Menuid2=9')">--%>
                                <font style="font-weight: bolder; color: White">[报告管理]</font></a>
            <span style="color: Red;">--</span> <a href="#" onclick="Show('Menu.aspx?Menuid=4','WelCome.aspx?Menuid2=4')">
                <font style="font-weight: bolder; color: White; margin-right: 0px;">[财务管理]</font></a>
          <%--  <span style="color: Red;">--</span> <a href="#" onclick="Show('Menu.aspx?Menuid=10','WelCome.aspx?Memuid2=10')">
                <font style="font-weight: bolder; color: White; margin-right: 0px;">[时效管理]</font></a>--%>
            <span style="color: Red;">--</span> <a href="#" onclick="Show('Menu.aspx?Menuid=7','WelCome.aspx?Memuid2=7')">
                <font style="font-weight: bolder; color: White; margin-right: 0px;">[统计管理]</font></a>


              <%--  <span style="color: Red;">--</span> <a href="#" onclick="Show('Menu.aspx?Menuid=12','WelCome.aspx?Memuid2=12')">
                <font style="font-weight: bolder; color: White; margin-right: 0px;">[质量管理]</font></a>--%>


            <span style="color: Red;">--</span> <a href="#"  onclick="Show('Menu.aspx?Menuid=1','WelCome.aspx?Memuid2=1')">
                <font style="font-weight: bolder; color: White; margin-right: 0px;">[系统管理]</font></a>

              <%-- <span style="color: Red;">--</span> <input type="text" runat ="server"    style ="color: red; width :80px;"  value="0" id="messageCount"/><a href ="../Case/CaseFrame.aspx" style ="color:White;" target ="main" >前往查看</a>--%>


        </div>
    </div>
    </form>
</body>
</html>
<script language="JavaScript" type="text/javascript">

    var DispClose = true;
    window.onbeforeunload = ajax;



    var xmlhttp;
    function ajax() {

        var id = "0";
        var dq = new Date();
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        xmlhttp.open("GET", "Handler.ashx?id=" + Math.random(), false);
        xmlhttp.onreadystatechange = process;
        xmlhttp.send();

    }
    function process() {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {

            var data = xmlhttp.responseText;

            // alert(data);
        }

    }

    
    
        
</script>
