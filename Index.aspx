<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1"  runat="server">
    <title>倍测检测事业部</title>

 

        <META HTTP-EQUIV="Pragma" CONTENT="no-cache">
        <META HTTP-EQUIV="Cache-Control" CONTENT="no-cache">
        <META HTTP-EQUIV="Expires" CONTENT="0">
     


</head>






<noframes>
<body on>
  <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
  </body>
    </noframes>

    <frameset rows="25,*"  frameborder="no" frameborder="1" framespacing="0">
	<frame src="Top.aspx" noresize="noresize" frameborder="NO" name="topFrame" scrolling="no" marginwidth="0" marginheight="0" target="tops" />
  <frameset cols="175,*"   id="Leftframe">
	<frame src="Menu.aspx?Menuid2=2" name="leftFrame" noresize="noresize" marginwidth="0" marginheight="0" frameborder="0" scrolling="no" target="lefts" />
	
	<%--<frame src="Menu1.aspx" name="leftFrame" noresize="noresize" marginwidth="0" marginheight="0" frameborder="0" scrolling="no" target="lefts" />
  --%>  
    
    <frame src="Body.aspx" name="main" marginwidth="0" marginheight="0" frameborder="1" scrolling="auto" target="main" />
  </frameset>
  </frameset> 

</html>

 <script language="JavaScript" type="text/javascript">

     var DispClose = true;

     function CloseEvent() {
         if (DispClose) {
             return "是否离开当前页面?";
         }
     }
     window.onbeforeunload = CloseEvent;
        
 </script>

