﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YangPinLiuZhuan.aspx.cs" Inherits="Print_YangPinLiuZhuan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>打印样品流转记录</title>
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
    <base target="_self" />
</head>
<body style="font:20px; font-family :Times New Roman;">
 
    <form id="form2" runat="server">
           <center class="Noprint">
        <br />
        将本样品转借给: <asp:DropDownList 
                   ID="DropDownList1" AutoPostBack ="true"  runat="server" 
                   onselectedindexchanged="DropDownList1_SelectedIndexChanged">
               </asp:DropDownList>
               <asp:DropDownList ID="DropDownList2" runat="server">
               </asp:DropDownList>
               <asp:Button ID="Button1" 
                   runat="server" Text="确定" onclick="Button1_Click" />(请如实登记，必要时保存台账备查)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
        <input type="button" value="打印" onclick="document.all.WebBrowser.ExecWB(6,1)" />
        <input type="button" value="直接打印" onclick="document.all.WebBrowser.ExecWB(6,6)" />
        <input type="button" value="页面设置" onclick="document.all.WebBrowser.ExecWB(8,1)" />
        <input type="button" value="打印预览" onclick="document.all.WebBrowser.ExecWB(7,1)" />
        
        </br>
                   <object id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" width="0">
        </object>

        <hr align="center" width="90%" size="1" />
    </center>
        
        
        <asp:Label runat="server" ID="lblTable"></asp:Label>
        
      
        
        
    </form>
</body>
</html>


