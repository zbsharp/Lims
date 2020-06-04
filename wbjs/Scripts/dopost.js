var posting = false;
function dopost()
{
	//提交表表单，先判断验证再判断重复提交
//	if (Page_ValidationActive && !Page_IsValid){
//		return;
//	}

	posting=true;
	dispProcessing();
	document.body.style.cursor='wait';
}

function dispProcessing()
{
	var div = document.createElement("div");
	div.innerHTML = "<br/><br/><div align='center' style='color:#777;font-weight:bold;'>正在提交数据，请等待！</div>";
	div.style.background="#eed url(../pic/processing.gif) no-repeat center 40%";
//	div.style.filter = "alpha(Opacity=100)";
//	div.style.opacity="0.8";
	div.style.width="300px";
	div.style.height="80px";
	div.style.lineHeight="25px";
	div.style.border="1px solid #ccc";
	div.style.position="absolute";
	div.style.top=document.body.clientHeight/2-150+"px";
	div.style.left=document.body.clientWidth/2-150+"px";
	div.style.zIndex=101;
	document.body.appendChild(div);
	
	var div1 = document.createElement("div");
	var w, h;
	with(document.body){
		if (scrollWidth<clientWidth){
			w = clientWidth;
		}else{
			w = scrollWidth;
		}
		if (scrollHeight<clientHeight){
			h = clientHeight;
		}else{
			h = scrollHeight;
		}
	}
	div1.style.background = "#ddd";
	div1.style.filter = "alpha(Opacity=40)";
	div1.style.opacity="0.3";
	div1.style.position = "absolute";
	div1.style.top = "0";
	div1.style.left = "0";
	div1.style.width = w+"px";
	div1.style.height = h+"px";
	div1.style.zIndex = 100;
	document.body.appendChild(div1);
	
	div1.focus();
	
	var sels = document.getElementsByTagName("SELECT");
	for(var i=0; i<sels.length; i++){
		sels[i].style.visibility = "hidden";
	}
}

