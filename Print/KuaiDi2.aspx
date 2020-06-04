<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KuaiDi2.aspx.cs" Inherits="Print_KuaiDi2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!--
LQ-630的打印质量为 180像素/英寸，即70.866像素/厘米。
顺风快递的速递单大小为：21.6*13.9厘米；折合 650×354像素
-->
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />

  <!--media=print   这个属性可以在打印时有效-->   
  <style   media=print>   
  .Noprint{display:none; font-size:14px;font-family: "宋体"; }  
  </style>
  <!--在此定义报表中的字体-->   
  <style>
  .STYLE1 {	font-size: 16px; font-family: "宋体";}
  .STYLE2 {
	font-size: 18px;
	font-family: "楷体_GB2312";
	font-weight: bold;
}
  </style>   
  
<title></title>
</head>
<body style ="margin-top :0px;" >
  <center   class="Noprint"   >   
  <br />   
      <OBJECT id=WebBrowser classid=CLSID:8856F961-340A-11D0-A96B-00C04FD705A2 height=0 width=0>   
      </OBJECT>   
		<input type=button value="预 览" onclick=document.all.WebBrowser.ExecWB(7,1)>  
		<input type=button value="打 印" onclick=document.all.WebBrowser.ExecWB(6,1)> 
		<input type=button value="设 置" onclick=document.all.WebBrowser.ExecWB(8,1)>   
		<input type=button value="关 闭" onClick="javascript:window.close();">   
  <br /><p>(适用于德邦快递单据打印，纸张大小21.6*13.9cm，靠右安装；纵向，页边距左3mm上5mm，清空页面页脚)
  <hr   align="center"   width="90%"   size="1"   noshade>   
  </center>
  


<table width="250" border="0" align="left"  style ="margin-top :10px;margin-left :30px;" cellpadding="1" cellspacing="0" bordercolor="#000000">
<tr height="60">
	<td width="30">&nbsp;</td>
	<td width="100">&nbsp;</td>
	<td  width="60">&nbsp;</td>
</tr>
<tr height="40">
	<td colspan="2" class="STYLE1">中检集团南方电子产品测试(深圳)有限公司</td>
	<td class="STYLE2"><%=jijianren%></td>
</tr>
<tr height="45">
	<td colspan="3" class="STYLE1">广东省深圳市南山区西丽沙河路电子检测大厦</td>
</tr>
<tr height="26">
	<td class="STYLE1"></td>
	<td colspan="2" class="STYLE1"><%=jidiandianhua%></td>
</tr>

<tr height="35">
	<td colspan="3">&nbsp;</td>
</tr>

<tr height="35">
	<td colspan="2" class="STYLE1"><%=shoujiandanwei%></td>
	<td class="STYLE2"><%=shoujianren%></td>
</tr>
<tr height="40">
	<td colspan="3" class="STYLE1" ><%=shoujiandizhi%></td>
</tr>
<tr height="20">
	<td>&nbsp;</td>
	<td colspan="2" class="STYLE1"><%=shoujiandianhua%></td>
</tr>
</table>

</body>
</html>

