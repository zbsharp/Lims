<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="Chat_Chat" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
 <title>聊天室</title>
  <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
  <meta content="C#" name="CODE_LANGUAGE">
  <meta content="JavaScript" name="vs_defaultClientScript">
  <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
  <LINK href="global.css" type="text/css" rel="stylesheet">
  <script language="javascript">
      //发送消息
      function send() {
          var txtContent = document.all("content").value; //文本框输入内容
          if (txtContent == "") return;

          var user_to = document.all("userlist").value;  //聊天对象
          var textcolor = document.all("textcolor").value;  //颜色
          var expression = document.all("expression").value;  //表情
          var isPublic = !(document.all("isSecret").checked);  //是否密谈    

          //调用服务器端方法发送消息
          Chat_Chat.SendMsg(txtContent, user_to, textcolor, expression, isPublic);

          //更新聊天内容显示
          var div = document.all("chatcontent");
          div.innerHTML = Chat_Chat.GetNewMsgString().value + div.innerHTML;

          //清空输入框
          document.all("content").value = "";
      }

      //定时更新聊天内容
      function refresh_chatcontent() {
          //调用服务器方法获取最新消息的HTML字符串
          var div = document.all("chatcontent");
          var strNewMsg = Chat_Chat.GetNewMsgString().value;

          //判断是否为空，避免不必要的更新
          if (strNewMsg != "")
              div.innerHTML = strNewMsg + div.innerHTML;

          //定时更新
          window.setTimeout(refresh_chatcontent, 1000);
      }

      //更新用户列表（左侧和下拉列表）
      function refresh_onlineusers() {
          //发送对象列表
          var userlist = document.all("userlist");

          //调用服务器端方法获取用户列表字符串（用逗号分隔）
          var strUserlist = Chat_Chat.GetOnlineUserString().value;

          //获取客户端显示的用户列表字符串
          var strUserlistClient = "";
          for (var i = 1; i < userlist.options.length; i++) {
              if (i != userlist.options.length - 1) {
                  strUserlistClient += userlist.options[i].value + ",";
              }
              else {
                  strUserlistClient += userlist.options[i].value;
              }
          }

          if (strUserlistClient != strUserlist)  //在线用户列表发生变化
          {
              var userArr = strUserlist.split(',');

              //在线用户数
              var usercount = document.all("usercount");
              usercount.innerHTML = "在线名单：（" + userArr.length + "人）";

              //左边用户列表
              var tableHTML = "<table>";
              for (var i = 0; i < userArr.length; i++) {
                  tableHTML += "<tr><td><label onmouseover=\"this.style.cursor='hand'\" onmouseout=\"this.style.cursor='default'\" onclick=\"setObj('" + userArr[i] + "')\">" + userArr[i] + "</label></td></tr>";
              }
              tableHTML += "</table>";
              var div = document.all("onlineusers");
              div.innerHTML = tableHTML;


              //初始化
              while (userlist.options.length > 0) {
                  userlist.removeChild(userlist.options[0]);  //清空所有选项
              }

              //增加“所有的人”选项
              var oOption = document.createElement("OPTION");
              oOption.text = "所有的人";
              oOption.value = "大家";
              userlist.add(oOption);

              //下拉列表中增加在线用户的选项
              for (var i = 0; i < userArr.length; i++) {
                  var oOption = document.createElement("OPTION");
                  oOption.text = userArr[i];
                  oOption.value = userArr[i];
                  userlist.add(oOption);
              }
          }

          //每隔1秒更新
          window.setTimeout(refresh_onlineusers, 1000);
      }

      //退出聊天室
      function logout() {
          Chat_Chat.Logout();
      }

      //设置聊天对象
      function setObj(str) {
          var userlist = document.all("userlist");
          for (var i = 0; i < userlist.options.length; i++) {
              if (str == userlist.options[i].value) {
                  userlist.selectedIndex = i;
                  break;
              }
          }
      }

      //关闭浏览器窗口
      function Close() {
          var ua = navigator.userAgent;
          var ie = navigator.appName == "Microsoft Internet Explorer" ? true : false;
          if (ie) {
              var IEversion = parseFloat(ua.substring(ua.indexOf("MSIE ") + 5, ua.indexOf(";", ua.indexOf("MSIE "))));
              if (IEversion < 5.5) {
                  var str = '<object id=noTipClose classid="clsid:ADB880A6-D8FF-11CF-9377-00AA003B7A11">';
                  str += '<param name="Command" value="Close"></object>';
                  document.body.insertAdjacentHTML("beforeEnd", str);
                  document.all.noTipClose.Click();
              }
              else {
                  window.opener = null;
                  window.close();
              }
          }
          else {
              window.close();
          }
      }

  </script>
<style type="text/css">
<!--
@import url("css/home_ge.css");
@import url("css/home_ly.css");
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	background-image: url(images/bg.jpg);
}
a:link {
	text-decoration: none;
}
a:visited {
	text-decoration: none;
	color: #333333;
}
a:hover {
	text-decoration: underline;
	color: #FF0000;
}
a:active {
	text-decoration: none;
}
.STYLE8 {
	font-size: 12px;
	color: #333333;
}
.STYLE9 {font-size: 12px}
-->
</style>
</head>
<body bottomMargin="0" onbeforeunload="logout()" leftMargin="0" topMargin="0" rightMargin="0">
  <form id="Form1" method="post" runat="server">
<table width="772" height="572" border="0" align="center" cellspacing="0" bgcolor="#FFFFFF">
  <tr>
    <td height="19" valign="top"></td>
    <td width="606" rowspan="2" valign="top"><table height="490" border="0" cellspacing="0" class="f2">
      <tr>
        <td height="488"><table width="100%" border="0" cellpadding="0" cellspacing="0">
          <!--DWLayoutTable-->
          <tbody>
            <tr>
              <td height="65" colspan="3" valign="top"><img src="image/top.bmp" width="576" height="63" /></td>
            </tr>
            <tr>
              <td width="441" height="429" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" class="f2">
                <!--DWLayoutTable-->
                <tr>
                  <td width="49" height="46" align="center" valign="middle" bgcolor="#F5F5F5"><a href="Chuanshu.aspx"><img src="image/N_agency.gif" width="34" height="30" border="0" /></a></td>
                  <td width="45" align="left" valign="middle" bgcolor="#F5F5F5"><a href="Chuanshu.aspx">上传</a></td>
                  <td width="58" align="center" valign="middle" bgcolor="#F5F5F5"><a href="SaveFile.aspx"><img src="image/N_download.GIF" width="39" height="30" border="0" /></a></td>
                  <td width="43" align="left" valign="middle" bgcolor="#F5F5F5"><a href="SaveFile.aspx">下载</a></td>
                  <td width="56" align="center" valign="middle" bgcolor="#F5F5F5"><img src="image/feedback.GIF" width="30" height="22" /></td>
                  <td width="51" align="left" valign="middle" bgcolor="#F5F5F5"><label onMouseOver="this.style.cursor='hand'" onMouseOut="this.style.cursor='default'" onClick="document.all('chatcontent').innerHTML = ''" style="FONT-SIZE:14px; COLOR: #ffcc00; TEXT-ALIGN: center"><span class="STYLE8">清空屏幕</span></label> </td>
                  <td width="53" align="center" valign="middle" bgcolor="#F5F5F5"><img src="image/tui.gif" width="34" height="35" /></td>
                  <td width="60" align="left" valign="middle" bgcolor="#F5F5F5"><label onMouseOver="this.style.cursor='hand'" onMouseOut="this.style.cursor='default'" onClick="if (confirm('您确定要退出该聊天室吗？')) Close();" style="FONT-SIZE:14px; COLOR: #ffcc00; TEXT-ALIGN: center"><span class="STYLE8">退出聊天</span></label>&nbsp;                   </td>
                  <td width="24" align="left" valign="middle" bgcolor="#F5F5F5"></td>
                </tr>
                <tr>
                  <td height="177" colspan="9" valign="top"><div id="chatcontent" style="OVERFLOW-Y: scroll;WIDTH: 100%; POSITION: relative; HEIGHT: 100%"><span class="STYLE9"></span></div></td>
                </tr>
                <tr>
                  <td height="28" colspan="9" valign="top" bgcolor="#F5F5F5">颜色<span style="FONT-SIZE: 13px">
           <select style="FONT-SIZE: 12px" name="textcolor">
             <option style="COLOR: #000000" value="000000" selected> 绝对黑色
               <option style="COLOR: #000080" value="000080"> 忧郁的蓝
                <option style="COLOR: #0000ff" value="0000ff"> 碧空蓝天
                <option style="COLOR: #008080" value="008080"> 灰蓝种族
                <option style="COLOR: #0080ff" value="0080ff"> 蔚蓝海洋
                <option style="COLOR: #8080ff" value="8080ff"> 清清之蓝
                <option style="COLOR: #8000ff" value="8000ff"> 发亮蓝紫
                <option style="COLOR: #aa00cc" value="aa00cc"> 紫的拘谨
                <option style="COLOR: #808000" value="808000"> 卡其制服
                <option style="COLOR: #808080" value="808080"> 伦敦灰雾
                <option style="COLOR: #ccaa00" value="ccaa00"> 卡布其诺
                <option style="COLOR: #800000" value="800000"> 苦涩心红
                <option style="COLOR: #ff0000" value="ff0000"> 正宗喜红
                <option style="COLOR: #ff0080" value="ff0080"> 爱的暗示
                <option style="COLOR: #ff00ff" value="ff00ff"> 红的发紫
                <option style="COLOR: #ff8080" value="ff8080"> 红旗飘飘
                <option style="COLOR: #ff8000" value="ff8000"> 黄金岁月
                <option style="COLOR: #ff80ff" value="ff80ff"> 紫金绣贴
                <option style="COLOR: #008000" value="008000"> 橄榄树绿
                <option style="COLOR: #345678" value="345678">我不知道</option>
           </select>
         表情
         <select style="FONT-SIZE: 12px" name="expression">
           <option value="" selected> 请选择
             <option value="笑着"> 笑着
               <option value="高兴地"> 高兴地
                <option value="含情脉脉"> 含情脉脉
                <option value="微笑"> 微笑
                <option value="幸福"> 幸福
                <option value="有点脸红"> 有点脸红
                <option value="使劲安慰"> 使劲安慰
                <option value="自言自语"> 自言自语
                <option value="差点要哭"> 差点要哭
                <option value="嚎啕大哭"> 嚎啕大哭
                <option value="一把鼻涕"> 一把鼻涕
                <option value="很无辜"> 很无辜
                <option value="流口水"> 流口水
                <option value="神秘兮兮"> 神秘兮兮
                <option value="幸灾乐祸"> 幸灾乐祸
                <option value="很不服气"> 很不服气
                <option value="不怀好意"> 不怀好意
                <option value="拳打脚踢"> 拳打脚踢
                <option value="不知所措"> 不知所措
                <option value="翻箱倒柜"> 翻箱倒柜
                <option value="很遗憾"> 很遗憾
                <option value="很严肃"> 很严肃
                <option value="善意警告"> 善意警告
                <option value="正气凛然"> 正气凛然
                <option value="哈欠连天"> 哈欠连天
                <option value="小声讲"> 小声讲
                <option value="大声喊叫"> 大声喊叫
                <option value="尖叫一声"> 尖叫一声
                <option value="遗憾地说"> 遗憾地说
                <option value="无精打采"> 无精打采
                <option value="想吐"> 想吐
                <option value="真诚"> 真诚
                <option value="不好意思"> 不好意思
                <option value="高兴地唱"> 高兴地唱
                <option value="轻轻地唱"> 轻轻地唱
                <option value="很诧异"> 很诧异
                <option value="依依不舍">依依不舍</option>
         </select>
         聊天对象
         <SELECT style="FONT-SIZE: 12px" name="userlist">
           <OPTION value="大家" selected>所有的人</OPTION>
         </SELECT>
         <INPUT id="isSecret" type="checkbox" name="isSecret">
         密谈</span></td>
                </tr>
                <tr>
                  <td height="133" colspan="9"><span style="FONT-SIZE: 13px">
                    <textarea id="content" onKeyDown="if (event.keyCode == 13) {send();return false;}" style="width: 432px; height: 120px; right: 0px;" name="content"></textarea>
                  </span></td>
                  </tr>
                <tr>
                  <td height="34" colspan="9" align="center" valign="middle"><span style="FONT-SIZE: 13px; height: 39px;">
                    <input id="btnSend" onClick="send();return false;" type="button" value="发送" name="btnSend" />
                  </span>&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; <span style="FONT-SIZE: 13px; height: 39px;">
                  <input id="btnSend2" onClick="document.all('chatcontent').innerHTML = ''" type="button" value="重置" name="btnSend2" />
                  </span>&nbsp;</td>
                </tr>
                
                
              </table>              </td>
              <td width="13">&nbsp;</td>
              <td width="122" valign="top"><br>
      <br>
      <div id="usercount"></div>
      <div id="onlineusers"></div></td>
            </tr>
          </tbody>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td width="162" height="539" valign="top">&nbsp;</td>
  </tr>
</table>
 <div align="center">
     <script language="javascript">
         refresh_chatcontent();
         refresh_onlineusers();
   </script>
    </div>
  </form>
</body>
</html>

