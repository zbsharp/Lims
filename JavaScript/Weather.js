var  getImg=function(id){
    id=parseInt(id.slice(1).replace(/\.gif/,""),10);
    return 'http://tool.115.com/static/tools/weather/icos/i/'+id+'.png';
}

var getHtml=function(date,id){
    var len=date.icon.length,html;
    html='';
    if(!!window.ActiveXObject&&!window.XMLHttpRequest){
        html+='<span  class="index_ico" style="vertical-align:middle;width:24px;height:20px; FILTER: progid:DXImageTransform.Microsoft.AlphaImageLoader(src=\''+getImg(date.icon[0])+'\', sizingMethod=\'scale\');" ></span>';
        if(len>1&&date.icon[0].substring(1,2)!=date.icon[1].substring(1,2)){
            html+='<span  class="index_ico" style="vertical-align:middle;width:24px;height:20px;FILTER: progid:DXImageTransform.Microsoft.AlphaImageLoader(src=\''+getImg(date.icon[1])+'\', sizingMethod=\'scale\');" ></span>';
        }
    }else{
        html+='<img style="vertical-align:middle;border:none;" src="'+getImg(date.icon[0])+'" />';
        if(len>1&&date.icon[0].substring(1,2)!=date.icon[1].substring(1,2)){
            html+='<img style="vertical-align:middle;border:none;" align="middle" src="'+getImg(date.icon[1])+'" />';
        }
    }
    
    if(date.stat){
        html+=' '+date.stat;
    }else{
        html+=' '+date.desc[0];
    }
    html+=' '+date.temp;
    return html;
}
var ILData_callback=function(){
    var ID=ILData[4];
    var URL="http://tool.115.com/static/weather/"+ID+".txt";
    getTextWithScript(
        URL,
        function(){
            var html='<strong>'+weatherJSON.city[1]+'</strong> <span class="WEATHER-DAY">今天</span>'+getHtml(weatherJSON.weather.today,ID)+' <span class="WEATHER-DAY">明天</span>'+getHtml(weatherJSON.weather.tomorrow,ID);
            var JS=document.getElementById("TOOL_115_COM_JS");
            var OUTER=JS.parentNode;
            var div=document.createElement("span");
            OUTER.replaceChild(div,JS);
            div.id="TOOL_115_COM_JS";
            div.style.fontSize="12px";
            div.style.height="20px";
            div.style.lineHeight="20px";
            div.innerHTML=html;
        }
    );
}
var getTextWithScript=function(url,callback){
    var head = document.getElementsByTagName("head")[0];
    var script = document.createElement("script");
    script.src=url;
    script.onload=script.onreadystatechange=function(){
        if(!this.readyState||this.readyState == "loaded" || this.readyState == "complete"){
            callback();
            script.onload = script.onreadystatechange = null;
            head.removeChild(script);
        }
    }
    head.appendChild(script);
}
window.onload=function(){
    var tool_115_api=document.createElement("script");
    tool_115_api.src='http://tool.115.com/?ct=site&ac=ip_api';
    document.body.appendChild(tool_115_api);
}

