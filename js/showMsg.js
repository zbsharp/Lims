var xmlreq;
var value = "";
var resultValue = "";
var interval = 20000;  //10????
var timer = null;
var isIE = navigator.appName.indexOf("Internet Explore") != -1;

function _scrolling()
{
    var scrollTop = (document.body.scrollTop)?document.body.scrollTop:document.documentElement.scrollTop;
    if(scrollTop == null) return;
    var t = document.getElementById("_div_message").getAttribute("_top");

    document.getElementById("_div_message").style.top = (parseInt(t) + parseInt(scrollTop)) + "px";
}

function _resize()
{
    var w = document.getElementById("_div_message").offsetWidth;
    var h = document.getElementById("_div_message").offsetHeight;
    var sHeight     = document.documentElement.clientHeight;
    var docWidth    = document.documentElement.clientWidth;
    var docHeight   = sHeight - h;    

    var scrollTop = (document.body.scrollTop)?document.body.scrollTop:document.documentElement.scrollTop;
    var topPadding = (navigator.userAgent.indexOf("Safari") != -1)?85:92;
    document.getElementById("_div_message").setAttribute("_top", docHeight - h * 0.5 + topPadding);
    topPadding = topPadding + scrollTop;

    document.getElementById("_div_message").style.left = (docWidth-w-5) + "px";
    document.getElementById("_div_message").style.top = (docHeight - h * 0.5  + topPadding) + "px";
}

if(!isIE)
{
    document.write("<div id=_div_message style='position:absolute; z-index:9999;display: none;overflow:hidden'>");
    document.write("<iframe name=frmMessage id=frmMessage scrolling=no frameborder=0 src=message.htm></iframe></div>");
    if(document.attachEvent)
    {
        document.attachEvent("onscroll", _scrolling);
        window.attachEvent("onresize", _resize);
    }
    else if(document.addEventListener)
    {
        document.addEventListener("scroll", _scrolling, false);
        window.addEventListener("resize", _resize, false);
    }
}

var e = document.createElement("script");
e.type = "text/javascript";
e.src = "js/CLASS_MSN_MESSAGE.js";
document.getElementsByTagName("head")[0].appendChild(e);

function consistMessge()
{	
    if(frames["onlineUsers"].showPop()) return;
    if(MSG1 == null) return;
    if(__xroot != null && __xroot.length > 0 && __index<__xroot.length)
    {
        showPop();
    }
    else
    {
        if(timer ==  null) timer = window.setTimeout("showMsg()", interval);
    }
}

function showMsg()
{
    timer = null;
    if(window.XMLHttpRequest)
    {
        xmlreq = new XMLHttpRequest();
    }
    else if(window.ActiveXObject)
    {
        try 
        {
            xmlreq = new ActiveXObject("Msxml2.XMLHTTP");
        } 
        catch (e) 
        {
            try{
                xmlreq = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (e) {}
        }
    }
    var url = '../innet/msginfo.jsp';
    if(xmlreq)
    {
        xmlreq.open("GET", url, true);
        xmlreq.onreadystatechange = callback;
        xmlreq.send(null);
    }
}

function doKnowMsg(url)
{
    var _xmlreq = null;
    if(window.XMLHttpRequest)
    {
        _xmlreq = new XMLHttpRequest();
    }
    else if(window.ActiveXObject)
      {
        try 
        {
            _xmlreq = new ActiveXObject("Msxml2.XMLHTTP");
        } 
        catch (e) 
        {
            try {
                _xmlreq = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (e) {}
        }
    }
    if(_xmlreq)
    {
        _xmlreq.open("GET", url, true);
        _xmlreq.send(null);
    }
}

function callback(){
    if(xmlreq.readyState == 4){
        if(xmlreq.status == 200){
            parseMessage(); 
        }
    }
}

var __xroot = null;
var __index = 0;
var MSG1 = null;

function parseMessage()
{
	  //alert("calling......");
	  var xmlDoc = xmlreq.responseXML.documentElement;
	  if(xmlDoc != null)
    {
		    __xroot = xmlDoc.getElementsByTagName("value");

        if(__xroot != null && __xroot.length > 0)
        {
            __index = 0;
            showPop();
        }
        else
        {
          //alert(0)
          if(timer == null) timer = window.setTimeout("showMsg()", interval);
        }
	  }
}

function showPop()
{
    var xtype = __xroot[__index].childNodes[0].firstChild.nodeValue;
    var xtitle = __xroot[__index].childNodes[1].firstChild.nodeValue;
    var xurl = __xroot[__index].childNodes[2].firstChild.nodeValue;
    var xediturl = __xroot[__index].childNodes[3].firstChild.nodeValue;
    var time = __xroot[__index].childNodes[4].firstChild.nodeValue;
    var target = (__xroot[__index].childNodes.length >5)?__xroot[__index].childNodes[5].firstChild.nodeValue:null;

    getWake(xtitle, "知道了", xurl, xediturl, xtype, time, target); 
}

function doNext()
{
    if(frames["onlineUsers"].hasNext()) frames["onlineUsers"].next();		
    if(frames["onlineUsers"].showPop()) return;
    //其他的提醒
    __index++;
    if(__xroot != null && __xroot.length > __index) 
    {
        showPop();
    }
    else
    {
        if(timer ==  null) timer = window.setTimeout("showMsg()", interval);
    }
}

function getWake(content, button, url, editUrl, title, time, target)
{
    MSG1 = new CLASS_MSN_MESSAGE("aa", 270, 190, title, content, button, target);
    if(time != null && time != "") MSG1.time = time;
    MSG1.oncommand = function(){	
    try
    {
        //frames("exc").location = editUrl;
        doKnowMsg(editUrl);
    }catch(e){}
    MSG1.hide();			
    }; 		
    MSG1.oncommand2 = function(){
      if(MSG1.target == null || MSG1.target == "")
      {
          parent.frames["submain"].focus();
          parent.frames["submain"].location.href=url;
      }
      else
      {
          window.open(url, MSG1.target);
      }
      MSG1.hide();
    };
    MSG1.rect(null,null,null,screen.height-50);
    MSG1.speed = 10;
    MSG1.step = 3;
    MSG1.show(); 
}

function attachClickEvent(name)
{
	  frames[name].document.attachEvent("onclick", consistMessge);
}


if(document.attachEvent)
{
    document.attachEvent("onclick", consistMessge);
}
else if(document.addEventListener)
{
    document.addEventListener("click", consistMessge, false);
}

window.setTimeout("showMsg()", 2000);