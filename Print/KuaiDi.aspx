<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KuaiDi.aspx.cs" Inherits="Print_KuaiDi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!--
LQ-630�Ĵ�ӡ����Ϊ 180����/Ӣ�磬��70.866����/���ס�
˳���ݵ��ٵݵ���СΪ��21.6*13.9���ף��ۺ� 650��354����
-->
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />

  <!--media=print   ������Կ����ڴ�ӡʱ��Ч-->   
  <style   media=print>   
  .Noprint{display:none; font-size:14px;font-family: "����"; }  
  </style>
  <!--�ڴ˶��屨���е�����-->   
  <style>
  .STYLE1 {	font-size: 16px; font-family: "����";}
  .STYLE2 {
	font-size: 18px;
	font-family: "����_GB2312";
	font-weight: bold;
}
  </style>   
  
<title></title>
</head>
<body>
  <center   class="Noprint"   >   
  <br />   
      <OBJECT id=WebBrowser classid=CLSID:8856F961-340A-11D0-A96B-00C04FD705A2 height=0 width=0>   
      </OBJECT>   
		<input type=button value="Ԥ ��" onclick=document.all.WebBrowser.ExecWB(7,1)>  
		<input type=button value="�� ӡ" onclick=document.all.WebBrowser.ExecWB(6,1)> 
		<input type=button value="�� ��" onclick=document.all.WebBrowser.ExecWB(8,1)>   
		<input type=button value="�� ��" onClick="javascript:window.close();">   
  <br /><p>(������˳���ݵ��ݴ�ӡ��ֽ�Ŵ�С21.6*13.9cm�����Ұ�װ������ҳ�߾���3mm��5mm�����ҳ��ҳ��)
  <hr   align="center"   width="90%"   size="1"   noshade>   
  </center>
  


<table width="250" border="0" align="left" cellpadding="1" cellspacing="0" bordercolor="#000000">
<tr height="130">
	<td width="30">&nbsp;</td>
	<td width="100">&nbsp;</td>
	<td  width="60">&nbsp;</td>
</tr>
<tr height="40">
	<td colspan="2" class="STYLE1">�м켯���Ϸ����Ӳ�Ʒ����(����)���޹�˾</td>
	<td class="STYLE2"><%=jijianren%></td>
</tr>
<tr height="45">
	<td colspan="3" class="STYLE1">�㶫ʡ��������ɽ������ɳ��·���Ӽ�����</td>
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
