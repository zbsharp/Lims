 //idt it is a Integer Number.
function  isEmpty(value,msg) {
			if(Trim(value)=='') {
				alert("请输入"+msg);
				return true;
			}			
			return false;
}

function  isEmptys(value,msg,inmsg) {
			if(Trim(value)=='') {
				alert("请"+inmsg+msg);
				return true;
			}			
			return false;
}

function isValidDecimal(theField)
{
	var inStr=theField;
	var inLen=inStr.length;

	if (inLen == 0)
		return true;
	if (inLen == 1 && inStr==" ")
		return false;
	for(var i=0; i< inLen; i++)
	{
		var ch = inStr.substring(i,i+1)
        if ((ch<"0" || ch>"9") && ch!="." && ch!="-")
		{
			if (ch != "")
				return false;
		}
    }
	return true;
}

function isPageNumber(msg,value) {	
if (value =="") {
	 alert(msg);
	 return false;	
	}	
if (!parseInt(value))
  {
    alert(msg);
	return false;     
  }
  totalPage = LblTotalPage.innerText;
  if (parseInt(totalPage) <  parseInt(value)) {
	alert('跳转页数不能大于总页数'+totalPage)
	theForm.TxtPage.value = "";
	return false;
  }
   return true;	
}
function OpenWindow(strFile,strTitle,theWidth,theHeight){//开窗
 window.open(strFile,strTitle,"scrollbars=yes,toolbar=no,menubar=no,location=no,left=20,top=0,height="+theHeight+",width="+theWidth+",resizable=no");
}


///打开一个窗口,sValue指定是否刷新这个源窗口
function showModalself(sFile,refreshed,theWidth,theHeight){
var sReturn=window.showModalDialog(sFile,"popUp","dialogHeight:"+theWidth+"px;dialogWidth:"+theHeight+"px;status:no;help:no;edge: Raised;");
//如果sReturn返回'Refresh'和refreshed为直,刚Refreshpage
var undefined;
if(sReturn != undefined)
{
if((refreshed)&&(sReturn=='Refresh')) {
  window.location.href = unescape(window.location.href);
}
if((refreshed)&(sReturn.indexOf('aspx') > -1))
{
    parent.location.href=sReturn;
}
}
}
///打开一个窗口,sValue指定是否刷新这个源窗口
function showModalTopSelf(sFile,refreshed,theWidth,theHeight){
var sReturn=window.showModalDialog(sFile,"popUp","dialogHeight:"+theWidth+"px;dialogWidth:"+theHeight+"px;status:no;help:no;edge: Raised;");
//如果sReturn返回'Refresh'和refreshed为直,刚Refreshpage
var undefined;
if(sReturn != undefined)
{
if((refreshed)&&(sReturn=='Refresh')) {
  window.location.href = unescape(window.location.href);
}
if((refreshed)&(sReturn.indexOf('aspx') > -1))
{
    parent.location.href=sReturn;
}
}
}


 function isValidInt(theField)
{
	var inStr=theField;
	var inLen=inStr.length;

	if (inLen == 0)
		return true;
	if (inLen == 1 && inStr==" ")
		return false;
	for(var i=0; i< inLen; i++)
	{
		var ch = inStr.substring(i,i+1)
        if (ch < "0" || ch > "9")
		{
			if (ch != "")
				return false;
		}
    }
	return true;
}

//检验是否是正确的日期格式:2008-01-22

 function isValidDate(theField)
{
	var s1, s2, sDate, sMonth, sYear;
	var inDate = theField;	
	
	if (inDate.substring(4,5)=="-"||inDate.substring(4,5)=="/")
	{
	  s1=4;
	}
	else
	{
	  return false;
	}
	
   
	
	if (inDate.substring(6,7)=="-"||inDate.substring(6,7)=="/")
	{
	 s2=6;
	 }
	else if (inDate.substring(7,8)=="-" || inDate.substring(7,8)=="/")
	{
	s2=7;
	}
	else
	{
	   return false;

	}

	
	if (isValidInt(inDate.substring(0,s1)))
	{
       sYear= parseInt(inDate.substring(0,s1));
       
	}
	else
	{
		return false;	
	}
	if(sYear<1753)
    {
        return false;
    }
 	if (isValidInt(inDate.substring(s1+1,s2)))
	{
      sMonth= inDate.substring(s1+1,s2);
	}
	else
	{
		return false;
	}
	

	if (isValidInt(inDate.substring(s2+1,inDate.length)))
	{
       sDate= inDate.substring(s2+1,inDate.length);
	}
	else
	{
		return false;
	}

	if (sMonth==1 || sMonth==3 || sMonth==5 || sMonth==7 || sMonth==8 || sMonth==10 || sMonth==12)
	{
		if (sDate>=1 && sDate<=31) { return true; }
		else {return false;}

	}
	if (sMonth==4 || sMonth==6 || sMonth==9 || sMonth==11)
	{
		if (sDate>=1 && sDate<=30) { return true; }
		else{ return false; }

	}
	if (sMonth==2)
	{
		if (sYear%4 == 0)
		{ if (sDate>=1 && sDate <= 29) { return true; }
		  else {return false;}
	        }
		else
		{ if (sDate>=1 && sDate <= 28) { return true; }
		   else{return false;}
		 }
	}
	   return false;

}





function AddWDRecord(values,Type)
{
    var sReturn=window.showModalDialog("../Inc/AddRecord.aspx?dbid="+values+"&Type="+Type,window,'dialogHeight:250px;dialogWidth:570px;status:no;help:no;scrollbars=no;');
    if(sReturn=="ok")
    {
        alert('跟进成功');
    }
}
function SetPopedom(values)
{
  var sReturn=window.showModalDialog("EditPopedom.aspx?type="+values,window,'dialogHeight:460px;dialogWidth:650px;status:no;help:no;scrollbars=no;');

if(sReturn=="ok"){
return true
    }
    return false;
}
function deferRequest(oid,values1,values2)
{
  var sReturn=window.showModalDialog("../WorkFlow/FDT/DeferRequest.aspx?OID="+oid+"&DBID="+values1+"&deferType="+values2 + "&viewType=NEW",window,'dialogHeight:600px;dialogWidth:800px;status:no;help:no;scrollbars=no;');
    if(sReturn=="ok")
    {
        alert('申请延期成功');
    }
}



function checkInputDouble(Value,theValue)
{
    if (theValue < 46 || theValue >57 || theValue == 47)
    {
    event.returnValue = false;
    }
    if(Value.split('.').length-1 >0 && theValue == 46)
    {
    event.returnValue = false;
    }
    if(Value == '' && theValue == 46)
    {
    event.returnValue = false;
    }
}

function checkInputInt(theValue)
{
    if (theValue < 48 || theValue >57)
    {
    event.returnValue = false;
    }
}

function checkLowercase(theValue)
{
    if (theValue < 97 || theValue >122)
    {
    event.returnValue = false;
    }
}

function isChinese(theValue){
     var re = new RegExp("[\\u4e00-\\u9fa5]", "");
     var yesorno = re.test(theValue);
     if(yesorno)
     {
         return true;
     }
     else
     {
         return false;
     }
}













function checkIdcard(idcard){
var Errors=new Array(
"验证通过!",
"身份证号码位数不对!",
"身份证号码出生日期超出范围或含有非法字符!",
"身份证号码校验错误!",
"身份证地区非法!"
);
var area={11:"北京",12:"天津",13:"河北",14:"山西",15:"内蒙古",21:"辽宁",22:"吉林",23:"黑龙江",31:"上海",32:"江苏",33:"浙江",34:"安徽",35:"福建",36:"江西",37:"山东",41:"河南",42:"湖北",43:"湖南",44:"广东",45:"广西",46:"海南",50:"重庆",51:"四川",52:"贵州",53:"云南",54:"西藏",61:"陕西",62:"甘肃",63:"青海",64:"宁夏",65:"新疆",71:"台湾",81:"香港",82:"澳门",91:"国外"}
var retflag=false;
var idcard,Y,JYM;
var S,M;
var idcard_array = new Array();
idcard_array = idcard.split("");
//地区检验
if(area[parseInt(idcard.substr(0,2))]==null) return Errors[4];
//身份号码位数及格式检验
switch(idcard.length){
case 15:
if ( (parseInt(idcard.substr(6,2))+1900) % 4 == 0 || ((parseInt(idcard.substr(6,2))+1900) % 100 == 0 && (parseInt(idcard.substr(6,2))+1900) % 4 == 0 )){
ereg=/^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}$/;//测试出生日期的合法性
} else {
ereg=/^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}$/;//测试出生日期的合法性
}
if(ereg.test(idcard)) 
return Errors[0];
else 
{
 return Errors[2];
}
break;
case 18:
//18位身份号码检测
//出生日期的合法性检查 
//闰年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))
//平年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))
if ( parseInt(idcard.substr(6,4)) % 4 == 0 || (parseInt(idcard.substr(6,4)) % 100 == 0 && 
parseInt(idcard.substr(6,4))%4 == 0 )){
ereg=/^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9Xx]$/;//闰年出生日期的合法性正则表达式
} else {
ereg=/^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9Xx]$/;//平年出生日期的合法性正则表达式
}
if(ereg.test(idcard)){//测试出生日期的合法性
//计算校验位
S = (parseInt(idcard_array[0]) + parseInt(idcard_array[10])) * 7
+ (parseInt(idcard_array[1]) + parseInt(idcard_array[11])) * 9
+ (parseInt(idcard_array[2]) + parseInt(idcard_array[12])) * 10
+ (parseInt(idcard_array[3]) + parseInt(idcard_array[13])) * 5
+ (parseInt(idcard_array[4]) + parseInt(idcard_array[14])) * 8
+ (parseInt(idcard_array[5]) + parseInt(idcard_array[15])) * 4
+ (parseInt(idcard_array[6]) + parseInt(idcard_array[16])) * 2
+ parseInt(idcard_array[7]) * 1 
+ parseInt(idcard_array[8]) * 6
+ parseInt(idcard_array[9]) * 3 ;
Y = S % 11;
M = "F";
JYM = "10X98765432";
M = JYM.substr(Y,1);//判断校验位
if(M == idcard_array[17]) return Errors[0]; //检测ID的校验位
else return Errors[3];
}
else return Errors[2];
break;
default:
return Errors[1];
break;
}
}


function  DateDiff(beginDate,  endDate){    //beginDate和endDate都是2007-8-10格式
       var  arrbeginDate,  Date1,  Date2, arrendDate,  iDays  
       arrbeginDate=  beginDate.split("-")  
       Date1=  new  Date(arrbeginDate[1]  +  '-'  +  arrbeginDate[2]  +  '-'  +  arrbeginDate[0])    //转换为2007-8-10格式
      arrendDate=  endDate.split("-")  
       Date2=  new  Date(arrendDate[1]  +  '-'  +  arrendDate[2]  +  '-'  +  arrendDate[0])  
       iDays  =  parseInt(Math.abs(Date1-  Date2)  /  1000  /  60  /  60  /24)    //转换为天数 
       return  iDays  
   }


function checkUserName(theValue)
{
//    return /^[\u4E00-\u9FFF]+\[\d+\,[\u4E00-\u9FFF]+\]$/.test(theValue);  
    return /[^\[]+\[\d+,[^\[]+\]/.test(theValue);    
}
function checkDeptName(theValue)
{
//    return /^[\u4e00-\u9fa5]+\[\d+\]$/.test(theValue);    
    return /[^\[]+\[\d+\]/.test(theValue);
}

function getDecode(str) 
{
    return unescape(str);
}

//数字转换成大写金额汉字（圆为基本单位）
function NumberToChinese(num) {
    if (num == "") {
        return "";
    }

    if (!/^\d*(\.\d*)?$/.test(num)) return "0";

    var AA = new Array("零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖");

    var BB = new Array("", "拾", "佰", "仟", "萬", "億", "圆", "");

    var CC = new Array("角", "分", "厘");

    var a = ("" + num).replace(/(^0*)/g, "").split("."), k = 0, re = "";

    for (var i = a[0].length - 1; i >= 0; i--) {

        switch (k) {

            case 0: re = BB[7] + re; break;

            case 4: if (!new RegExp("0{4}\\d{" + (a[0].length - i - 1) + "}$").test(a[0]))

                    re = BB[4] + re; break;

            case 8: re = BB[5] + re; BB[7] = BB[5]; k = 0; break;

        }

        if (k % 4 == 2 && a[0].charAt(i) == "0" && a[0].charAt(i + 2) != "0") re = AA[0] + re;

        if (a[0].charAt(i) != 0) re = AA[a[0].charAt(i)] + BB[k % 4] + re; k++;

    }

    if (a.length > 1) //加上小数部分(如果有小数部分)   
    {

        re += BB[6];

        for (var i = 0; i < a[1].length; i++) {

            re += AA[a[1].charAt(i)] + CC[i];

            if (i == 2) break;

        }

        if (a[1].charAt(0) == "0" && a[1].charAt(1) == "0") {

            re += "圆整";

        }

    }

    else {

        re += "圆整";

    }

    return re;

}

//限制文本框textbox的最大字符串长度为len
function LimitLength(textbox, len) {
    var str;
    try {
        str = textbox.value;
    } catch (ex) {
        str = textbox.innerText;
    }
    if (str.length > len) {
        //alert("对不起，最多只允许填写" + len + "个字符");
        try {
            textbox.value = str.substr(0, len);
        } catch (ex) {
            textbox.innerText = str.substr(0, len);
        }  
    }
    
    /*
    //验证正数和0
    var re = /^\d+(?=\.{0,1}\d+$|$)/
    //验证浮点数
    var re2=/^(-?\d+)(\.\d+)?$/
    var b=true;
    if(!re.test(str) && !re2.test(str))
    {
       b=false;
    }
    
    if(!re2.test(str) && !re.test(str))
    {
       b=false;
    }
    
    var Value=str;
    if(Value=="")
        return ;
        
    if(b==true)
    {
         if (Value.substr(0, 1)<=0)
         { 
            for(var i=0;i<Value.length;i++)
            {
                var strs=Value.substr(0, 1);
                if(strs==0)
                    Value=Value.substr(1, str.length-1);
            }
            
            try
            {
                textbox.value =Value;
            }
            catch(ex) 
            {
                textbox.innerText =Value;
            } 
        }
    }*/
}

function isMoney(s) {
    var re = /^-?\d+\.{0,}\d{0,}$/
    if (!re.test(s.value)) {
        s.value = '0';
    }

    if (s.value.split('.').length > 2) {
        s.value = s.value.substring(0, s.value.length - 1);
    }


}

function isIdCardNo(num) {
    var obj = num;
    num = num.value.toUpperCase();
    if (!(/(^\d{15}$)|(^\d{17}([0-9]|X)$)/.test(num))){
        obj.style["backgroundColor"] = "yellow";
        return false;
    }
    else
    {
        return true;
    }
}


function LTrim(str){ //去掉字符串 的头空格

var i;

for(i=0;i<str.length;i++){
  if(str.charAt(i)!=" "&&str.charAt(i)!=" ") {
	   break;
   } 
}

str = str.substring(i,str.length);

return str;

}

function RTrim(str){

var i;

for(i=str.length-1;i>=0;i--){

if(str.charAt(i)!=" "&&str.charAt(i)!=" ") break;

}

str = str.substring(0,i+1);

return str;

}

//保留N位小数
function changeToDecimal(x,i) {
    var f_x = parseFloat(x);
    if (isNaN(f_x)) {
        alert('function:changeTwoDecimal->parameter error');
        return false;
    }
    f_x = Math.round(f_x * 100) / 100;
    var s_x = f_x.toString();
    var pos_decimal = s_x.indexOf('.');
    if (pos_decimal < 0) {
        pos_decimal = s_x.length;
        s_x += '.';
    }
    while (s_x.length <= pos_decimal + i) {
        s_x += '0';
    }
    return s_x;
}
