<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustMangeEmail.aspx.cs" EnableViewState ="true"  Inherits="Customer_CustMangeEmail" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title></title>
    <style type="text/css"> 
   /*选项卡1*/
#Tab1{
width:100%;
background-color:White;
margin:0px;
padding:0px;
margin:0 auto;}
/*菜单class*/
.Menubox {
width:100%;
height:28px;
line-height:28px;
}
.Menubox ul{
margin:0px;
padding:0px;
}
.Menubox li{
float:left; 
cursor:pointer;
width:115px;
text-align:center;
border-top:1px solid #97C8FB;
border-right:1px solid #97C8FB;
border-left:1px solid #97C8FB;
             margin-left: 0px;
             height:10px;
         }
.Menubox li.hover{
padding:0px;
background:#fff;
width:116px;
border-top:1px solid #97C8FB;
font-weight:bold;
background-color:#E1ECF9;

}
.Contentbox{
clear:both;
margin-top:0px;

border-top:none;
height:100%;
text-align:center;
width:100%;background-color:#E1ECF9;
}
</style>

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

    <meta http-equiv="Content-Language" content="zh-cn">
    <link href="css.css" rel="stylesheet" type="text/css" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../css.css" rel="stylesheet" type="text/css" />
    <link href="../css/main.css" type="text/css" rel="stylesheet">
    <style type="text/css">
		    body{font-size: 12px;cursor: default;font-family: 宋体;}
		</style>

 

    <script language="javascript">
        function Show(qusetion, answer, email) {

            x = event.clientX + document.body.scrollLeft;
            y = event.clientY + document.body.scrollTop + 30;
            div1.style.display = "block";
            div1.style.left = x;
            div1.style.top = y;

            document.getElementById('div1').innerHTML = qusetion;
        }
        function Hide() {
            div1.style.display = "none";
        } 
    </script>

    <script language="javascript">
        function Show1(qusetion, answer, email) {

            x = event.clientX + document.body.scrollLeft;
            y = event.clientY + document.body.scrollTop + 30;
            div11.style.display = "block";
            div11.style.left = x;
            div11.style.top = y;

            document.getElementById('div11').innerHTML = qusetion;
        }
        function Hide1() {
            div11.style.display = "none";
        } 
    </script>

    <style type="text/css">
		    body{font-size: 12px;cursor: default;font-family: 宋体;}
		</style>
    <style type="text/css">
    /* 强制弹出的可拖曳处 */
    .cssDragHandler
    {
        background-color: #e2ded6;
        color: #00a;
        text-align: center;
        vertical-align: middle;
        border: 5px double #fff;
        cursor: move;
        font-weight: bolder;
        font-size: 15pt;
        height: 40px;
    }
    
    /* 在显示‘强制弹出’时，背景项目所用的样式 */
    .modalBackground 
    {
        background-color: #777;
        filter: alpha(opacity=75);
        opacity: 0.75;
    }
    
    /* 强制弹出对话框所用的样式 */
    .cssModalPopup
    {
        background-color: #ffffdd;
        border-width: 3px;
        border-style: solid;
        border-color: #284775;
        width: 400px;
    }
    
     /* ‘关闭’按钮 */
    .close
    {
        top: 7px;
        right: 7px;
        background: url(Images/Close01.gif) no-repeat;
        width: 34px;
        height: 34px;
        cursor: hand;
        position: absolute;
    }
    </style>
    <style type="text/css">
    /* 弹出控件扩展器样式 */
    .cssPopupControl
    {
        background-color: White;
        position: absolute;
        visibility: hidden;
        border: 1px outset white;
    }

    /* 标题（Caption）样式 */
    .cssCaption
    {
        background-image: url(Images/BG_03.gif);
        font: bold 150% 标楷体, 楷体;
    }

    /* 停留菜单 */
    .cssHoverMenu
    {
        position: absolute;
        display: none;
    }
    
    /* 提示信息 */
    .cssHint
    {
        background-color: #996699;
        color: #ffffff;
        text-align: center;
        border: 2pt double;
    }
    .cssHintBDay
    {
        background-color: #996699;
        color: #ffffff;
        text-align: center;
        border: 2pt double;
        width: 200px;
    }
    </style>

    <script type="text/javascript">
<!--
        /*第一种形式 第二种形式 更换显示样式*/
        function setTab(name, cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i);
                var con = document.getElementById("con_" + name + "_" + i);
                menu.className = i == cursel ? "hover" : "";
                con.style.display = i == cursel ? "block" : "none";
            }
        }


//-->
    </script>

 
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
  
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>


</head>
<body>
    <form id="Form1" method="post" runat="server">
       
         <div id="div1" style="display: none; position: absolute; border-collapse: collapse;
            background-color: #ffffdd; border-width: 1px; border-style: solid; border-color: red;">
        </div>
        <div id="div11" style="display: none; position: absolute; border-collapse: collapse;
            background-color: #ffffdd; border-width: 1px; border-style: solid; border-color: green;">
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></div>
       
     
                   
                            <asp:DropDownList ID="SerchCondition" runat="server" Width="95px" CssClass="DDLStyle">
                                
                               
                                <asp:ListItem value="0">联系人</asp:ListItem>
                                <asp:ListItem value="1">客户名称</asp:ListItem>
                               
                               
                            </asp:DropDownList>
                            <asp:TextBox ID="SerchText" runat="server" CssClass="TextBoxCss" Width="91px"></asp:TextBox>
                            <asp:Button ID="Button2" runat="server" CssClass="BnCss" 
             Text="查询" Width="76px" onclick="Button2_Click" />&nbsp;
                            <asp:Button ID="Button4" runat="server" Text="发送邮件" CssClass="BnCss" Width="76px"
                                OnClick="Button4_Click" /><asp:Button ID="Button3" 
             runat="server" 
                                    CssClass="BnCss" Text="导出到Excel" 
             onclick="Button3_Click"  />
                            <asp:Label ID="My" runat="server" Text=""></asp:Label>
                       
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="kehuid" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound">
                                  
                                    <Columns>
                                        <asp:TemplateField HeaderText="序 号" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("0000") %>'
                                                    CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle ForeColor="Green" />
                                        </asp:TemplateField>
                                     

                                        <asp:BoundField DataField="kehuid"  HeaderText="编号" />
                                        <asp:BoundField DataField="customname" SortExpression="customname" HeaderText="名称" />
                                        <asp:TemplateField HeaderText="联系人">
                                            <ItemTemplate>
                                                <asp:Panel ID="panName" runat="server">
                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal"
                                                        RepeatLayout="Flow">
                                                    </asp:CheckBoxList></asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="b" HeaderText="是否分派" Visible="false" />
                                        <asp:BoundField DataField="fillname" HeaderText="填写人" SortExpression="fillname" />
                                        <asp:TemplateField HeaderText="业务员">
                                            <ItemTemplate>
                                                <asp:Panel ID="panName1" runat="server">
                                                    <asp:CheckBoxList ID="CheckBoxList11" runat="server" RepeatDirection="Horizontal"
                                                        RepeatLayout="Flow">
                                                    </asp:CheckBoxList></asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="xingyongdengji" Visible="false" HeaderText="信用" />
                                        <asp:BoundField DataField="filltime" HeaderText="时间" DataFormatString="{0:d}"  />
                                        <asp:BoundField DataField="sid" HeaderText="sid" Visible="False" />
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton10" runat="server">查看/修改</asp:LinkButton>
                                                <asp:Label ID="Label10" Text='<%#DataBinder.Eval(Container.DataItem, "kehuid")%>'
                                                    runat="server" Visible="False" Width="0px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton20" runat="server">修改</asp:LinkButton>
                                                <asp:Label ID="Label20" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kehuid")%>'
                                                    Visible="False" Width="0px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                        <asp:HyperLinkField DataNavigateUrlFields="kehuid" Visible="false"  DataNavigateUrlFormatString="~/UserWork/genzongjilu.aspx?sid={0}"
                                            HeaderText="" Text="跟踪" />
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton30" runat="server">联系</asp:LinkButton>
                                                <asp:Label ID="Label30" Text='<%#DataBinder.Eval(Container.DataItem, "kehuid")%>'
                                                    runat="server" Visible="False" Width="0px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                        <asp:HyperLinkField DataNavigateUrlFields="kehuid" Visible="false" DataNavigateUrlFormatString="~/baojia/baojialuru.aspx?kehuid={0}"
                                            HeaderText="" Text="报价" />
                                        <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Customer/CustomerSee.aspx?kehuid={0}"
                                            HeaderText="" Text="查看" />
                                        <asp:TemplateField HeaderText="联系" Visible="false">
                                            <ItemTemplate>
                                                <a href="#" class="BnCss" onclick="window.open('addlianxiren.aspx?kehuid=<%#Eval("kehuid")%>','','width=350,height=200,top=220,left=300')">
                                                    <span style="">增加</span></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                    </Columns>
                                    <HeaderStyle CssClass="Admin_Table_Title " />
                                </asp:GridView>
                                  <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                                    CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                                    InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
                                    PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                                    Style="font-size: 9pt" UrlPaging="True" PageSize="15">
                                </webdiyer:AspNetPager> 

             
    </form>
</body>
</html>
