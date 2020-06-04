if (!GetMessageCount)
{
    var GetMessageCount = {};
}

$(document).ready(
    function(){
        GetMessageCount.FindMessage();
    }
);

GetMessageCount.FindMessage = function(){
        $.ajax({
           //处理ajax请求
           url:'FindNewMessage.ashx',
           // 当前用户的ID，这里图省事就省略了，直接写死为 1，
           //实际使用过程中可以从session中获取 。。。。
           data:{Uid:1},
           cache: false,
           //回调函数返回未读短信数目
           success: function(response)
           {
              $('#messageCount').val(response);
           },
           error:function(data)
           {
               alert("加载失败");
           }
       });
       //每隔5 秒递归调用一次，刷新未读短信数目
       window.setTimeout(GetMessageCount.FindMessage,5000*5);
}
