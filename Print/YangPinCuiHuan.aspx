<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YangPinCuiHuan.aspx.cs" Inherits="Print_YangPinCuiHuan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>样品催款通知书</title>
    <style type="text/css">
        .normal,.normal td,.normal th{border:dashed 1px gray;padding:3px 5px;height:30px;}
        .normal th{text-align:center;height:30px;}
        table, td,th{border-collapse: collapse;font-size:14px;}
    </style>
    <style media="print" type="text/css"> 
        .Noprint
        {
            display:none;
        } 
        .PageNext
        {
            page-break-after: always;
        } 
        
    </style>
    <style type="text/css">
       .tdp 
        { 
            border-color:#000000;
            border-bottom:1px dashed  #000000; 
            border-left:0 dashed  #000000; 
            border-right:1px dashed  #000000; 
            border-top:1px dashed  #000000;
            font-size: 14px; 
        } 
        .tabp 
        { 
            border-color: #000000; 
            border-style:1px dashed ; 
            border-top-width: 0px; 
            border-right-width: 0px; 
            border-bottom-width: 0px; 
            border-left-width: 1px; 
            
             
        }  
        .F_size12 {font-size:12px;}
        .F_size16 {font-size:21px;}
        .F_size17 {font-size:22px;}
        .F_size19 {font-size:16px; font-family :Times New Roman;}
        .F_size18 {font-size:medium;}
        .F_B{font-weight:bold;}
        .nobr{white-space:nowrap;}
    </style>

      <style>  
  .STYLE1 {font-size: 28px;	font-weight: bold;font-family: "华文中宋";letter-spacing:5px}
  .STYLE2 {font-size: 20px;	font-family: "宋体";}	  	  
  .STYLE3 {font-size: 20px;	font-family: "楷体";}	
  
  .STYLE4 {font-size: 20px; font-weight: bold; font-family: "黑体"; letter-spacing:4px }  
  .STYLE5 {font-size: 14px; font-family: "Times New Roman", Times, serif; }  
  
  </style>  

    <base target="_self" />
</head>
<body style="font:20px; font-family :Times New Roman;">
    <center class="Noprint">
        <br />
        <object id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" width="0">
        </object>
        <input type="button" value="打印" onclick="document.all.WebBrowser.ExecWB(6,1)" />
        <input type="button" value="直接打印" onclick="document.all.WebBrowser.ExecWB(6,6)" />
        <input type="button" value="页面设置" onclick="document.all.WebBrowser.ExecWB(8,1)" />
        <input type="button" value="打印预览" onclick="document.all.WebBrowser.ExecWB(7,1)" />
        <br />（打印设置要求：纸张大小A4，页边距上下10mm左右20mm，清空页眉页脚）
        <hr align="center" width="90%" size="1" />
    </center>
    <form id="form2" runat="server">
        <table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
	<tr height="35">
	  <td width="100" align="right" rowspan="2"></td>
	  <td align="center" class="STYLE4">中检集团南方电子产品测试(深圳)有限公司</td>
	</tr>
	<tr height="15">
	  <td align="center" class="STYLE5">CCIC Southern Electronic Product Testing (Shenzhen) Co., Ltd.</td>
	</tr>
</table>
<hr align="center" width="100%" size="1">
<br>






<!--内页开始-->
<table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
	<tr height="100">
	  <td align="center" class="STYLE1">样品催还通知单<br /><asp:Label runat="server" ID="lblTable" Visible ="false" ></asp:Label></td>
	</tr>
</table>
<br>



<table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
	<tr height="60">
	  <td align="left" class="STYLE2">尊敬的<font style="text-decoration:underline"><%=gongchengshi %></font>工程师:</td>
	</tr>
	<tr height="60">
	  <td align="left" class="STYLE2"><p style="line-height:200%">&nbsp;&nbsp;&nbsp;&nbsp;<%=rwbianhao %>号任务已于<font style="text-decoration:underline"><%=wancheng %></font><font style="text-decoration:underline"><%=state %></font>，但样品至今尚未退库。请收集齐全样品及相关配件，组装完整，退还到前台样品管理员处，以便我们及时清退给客户。</td>
	</tr>
</table>

<table width="500" border="0" align="center" cellpadding="0" cellspacing="0" style="border-collapse:collapse">
	<tr height="40">
		<td width="150" align="center" Class="STYLE2">样品编号</td>
		<td Class="STYLE3"><%=sampleid %></td>
	</tr>
	<tr height="40">
		<td align="center" Class="STYLE2">申请编号</td>		
		<td Class="STYLE3"><%=shenqingbianhao %></td>
	</tr>
	<tr height="40">
		<td align="center" Class="STYLE2">样品名称</td>
		<td Class="STYLE3"><%=name %></td>
	</tr>
	<tr height="40">
		<td align="center" Class="STYLE2">样品型号</td>
		<td Class="STYLE3"><%=xinghao %></td>
	</tr>
	<tr height="40">
		<td align="center" Class="STYLE2">制造厂家</td>
		<td Class="STYLE3"><%=zhizao %></td>
	</tr>	
</table>


<table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
	<tr height="60">
		<td align="left" class="STYLE2">&nbsp;&nbsp;&nbsp;&nbsp;特此通知，多谢合作！</td>
	</tr>
	<tr height="50">
		<td align="right" class="STYLE2"><br><br>客户服务部</td>
	</tr>
	<tr height="50">
		<td align="right" class="STYLE2"><%=DateTime.Now.ToShortDateString() %></td>
	</tr>
</table>

        
      
        
        
    </form>
</body>
</html>
