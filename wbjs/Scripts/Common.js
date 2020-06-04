
//请求连接参数分割
$UrlParameter = function(_key) {
	//debugger
	var _url = window.location.search;
	//如果不等于空表示存在参数
	if (_url.length != 0) {
		//清除问号字符
		_params = _url.replace('?', "").split('&');
		for (var i = 0; _p = _params[i]; i++) {
			_params[_p.split('=')[0]] = _p.split('=')[1];
		}
		//是否返回固定参数值
		if (_key && _key.length != 0) {
			return _params[_key];
		}
		//否则返回对象集合
		return _params;
	}
}
//Ajax对象
/*参数
Method;//方法 Get | Post
Url;//请求连接
Param;//请求参数
Error();//错误处理
Success();//处理成功
Complete();//完成操作
*/
$Ajax = function (_sender) {
    var xmlhttp = undefined;
    //验证是否IE浏览器
    if (window.ActiveXObject) {
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    } //非IE浏览器
    else if (window.XMLHttpRequest) {
        xmlhttp = new XMLHttpRequest();
    }
    else {
        alert("Error:Your broswer not support XMLHttpRequest!");
        //throw new Error("Error:Your broswer not support XMLHttpRequest!");
        return;
    }
	if(xmlhttp.overrideMimeType && _sender.ResultType != "HTML"){
		xmlhttp.overrideMimeType("text/xml");  //此处应该将text/html修改成text/xml,否则会出现问题的
	}
    xmlhttp.open(_sender.Method, _sender.Url +"?"+ _sender.Param, true);
    xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xmlhttp.setRequestHeader("Content-Length", (_sender.Url.length) + _sender.Param.length);
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {//那么如何知道是否调用成功呢，状态为200说明调用成功，500则说明出错
            try {
                var result = undefined;
                switch (_sender.ResultType) {
                    case "Object":
                        result = xmlhttp;
                        break;
                    case "XML":
                        result = xmlhttp.responseXML;
                        break;
                    case "HTML":
                    default:
                        result = xmlhttp.responseText;
                        break;
                }
                //加载成功
                _sender.Success(result);
                //加载完成
                _sender.Complete();
            } catch (e) {
                //错误提示
                //_sender.Error(e);
            }
            //撤销对象
            xmlhttp.abort();
        }
    }
    xmlhttp.send(null);
}
//Xml对象
/*参数
Url;//请求连接
IsAsync;//是否异步请求
Error();//错误处理
Success();//处理成功
Complete();//完成操作
*/
$Xml=function(_sender){
	var _xml = undefined;
    //验证是否IE浏览器
    if(window.ActiveXObject){
        var xmlArray = ["MSXML2.DOMDocument.6.0","MSXML2.DOMDocument.5.0","MSXML2.DOMDocument.4.0","MSXML2.DOMDocument.3.0","MSXML2.DOMDocument","Microsoft.XMLHTTP"];
        try{
            for( var i=0; i < xmlArray.length ; i++ ){
                _xml = new ActiveXObject(xmlArray[i]);
                break;
            }
        }catch(e){}
    }//非IE浏览器
    else if(document.implementation && document.implementation.createDocument){
        _xml = document.implementation.createDocument("","",null);
    }
    else{
		alert("Create XML Document Object Mode Error! Plase Contact Us");
        throw new Error("Create XML Document Object Mode Error! Plase Contact Us");
		return;
    }
	//是否异步
    _xml.async = _sender.IsAsync?_sender.IsAsync:false;
	//加载文件
    _xml.load(_sender.Url);
    try{
		//firefox暂不支持错误提示
		if(_xml.parseError.line != 0){
			var outError = "错误信息:"+_xml.parseError.reason+"\r\n" //错误信息
			outError += "错误节点:"+_xml.parseError.srcText+"\r\n" //错误节点
			outError += "错误资源:"+_xml.parseError.url+"\r\n" //错误资源
			outError += "错误行数:"+_xml.parseError.line+"\r\n" //错误行数
			alert(outError);
        	throw new Error(outError);
		}
    }catch(e){}
	//返回对象
    return _xml;
}


//input
function inputFocus(obj,text){
	obj.focus(function(){
		if($(this).val()==text){
           $(this).val("");
        }
	}).blur(function(){
		if($(this).val()==""){
           $(this).val(text);
        }	
	})
}
// var input1 = new inputFocus($('#search'),"请输入品牌搜索");
//导航菜单
(function($){
//selectShow
$.fn.selectShow = function() {
        return this.each(function() {
            var $text = $(this).find("dt span");
            var $listBox = $(this).find("dd");
            var $list = $listBox.find("div");
            $(this).click(function() {
                $listBox.slideToggle("fast");
            });
            $list.click(function() {
                var text = $(this).text();
                $text.text(text);
            })
        })
    }
$(document).ready(function(){
    var windowHref = window.location.href.split("/");
        windowHref = "/" + windowHref[windowHref.length - 2] + "/" + windowHref[windowHref.length - 1];
        $("#chAll").click(function(event){
                event.stopPropagation(); 
            })
            $(".table_none").hover(function(){
                $(this).css("background-color","#f2f2f2");
            },function(){
               $(this).css("background-color","#ffffff"); 
            })
		$(".pages .pages:eq(0)").css("text-align","left");
		$(".pages .pages:eq(1)").css("text-align","right");
		    var _move=false;
		    $("#divtitle,#divtitle1,#divtitle2,.divtitle").mousedown(function(e){   
		            _move=true;        
			        _x=e.pageX-parseInt($(this).parent("div").css("left"));        
			        _y=e.pageY-parseInt($(this).parent("div").css("top"));      
			        //$(this).fadeTo(20, 0.25);//点击后开始拖动并透明显示
		    });
		    $(document).mousemove(function(e){        
			    if(_move){           
			      var x=e.pageX-_x;//移动时根据鼠标位置计算控件左上角的绝对位置      
			      var y=e.pageY-_y;  
			      if(y<0){
			        y=0;
			      }
			      $("#divtitle,#divtitle1,#divtitle2,.divtitle").each(function(){
			        if($(this).is(":hidden")){
    			        
			        }
			        else{
			            $(this).parent("div").css({"top":y,"left":x});//控件新位置
			        }
			      }) 
				     }   
			      }).mouseup(function(){    
			      _move=false;    
			      //$(this).fadeTo("fast", 1);//松开鼠标后停止移动并恢复成不透明
			      })    
})
})(jQuery)
//tab 切换
    var tab = {
        tabControler: function(btn, layer, className, index) {
            btn.eq(index).addClass(className).siblings().removeClass(className);
            layer.eq(index).show().siblings("div").hide();
            btn.each(function(i) {
                if (index == 0) {
                    $(this).mouseover(function() {
                        $(this).addClass(className).siblings().removeClass(className);
                        layer.eq(i).show().siblings("div").hide();
                    });
                }
                else {
                    $(this).mousedown(function() {
                        $(this).addClass(className).siblings().removeClass(className);
                        layer.eq(i).show().siblings("div").hide();
                    });
                }
            });
        }
    }
    //tab.tabControler($(".tab_nav >a"),$(".tab_box >div"),"current");


