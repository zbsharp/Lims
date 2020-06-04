$(function () {
    $("#btn_close").click(function () {
        $(".model_1").hide();
        //清空文本框中的数据
        $("#txt_project").val('');
        $("#txt_productid").val('');
        $("#txt_standard").val('');
        $("#txt_specimen").val('');
        $("#txt_nums").val('');
        $("#txt_price").val('');
        $("#txt_technology").val('');
        $("#txt_remark").val('');
        $("#txt_period").val('');
        $("#txt_txt_idupdate").val('');
        //显示网页的滚动条
        $("body").css({ "overflow": "visible" });
    });
});
function Update() {
    //获取选中行的索引
    var e = event.srcElement;
    var rowIndex = e.parentNode.parentNode.rowIndex;
    var gridview = document.getElementById('GridView2');
    var id = gridview.rows(rowIndex).cells(14).innerText;//获取项目ID
    var project = gridview.rows(rowIndex).cells(1).innerText;//测试项目
    var productid = gridview.rows(rowIndex).cells(11).innerText;//产品编号
    var standard = gridview.rows(rowIndex).cells(2).innerText;//标准
    var specimen = gridview.rows(rowIndex).cells(3).innerText;//样品
    var nums = gridview.rows(rowIndex).cells(7).innerText;//数量
    var price = gridview.rows(rowIndex).cells(5).innerText;//费用
    //var technology = gridview.rows(rowIndex).cells(9).innerText;//技术要求
    var remark = gridview.rows(rowIndex).cells(8).innerText;//备注
    var period = gridview.rows(rowIndex).cells(4).innerText;//周期
    var waibao = gridview.rows(rowIndex).cells(15).innerText; //项目类型

    var epiloy_price = gridview.rows(rowIndex).cells(16).innerText;

    //显示模态框
    $(".model_1").css({ "display": "block" });
    //隐藏网页的滚动条
    $("body").css({ "overflow": "hidden" });

    //给文本框赋值
    $("#txt_idupdate").val('' + id + '');
    $("#txt_project").val('' + project + '');
    $("#txt_standard").val('' + standard + '');
    $("#txt_specimen").val('' + specimen + '');
    $("#txt_nums").val('' + nums + '');
    $("#txt_price").val('' + price + '');
    //$("#txt_technology").val('' + technology + '');
    $("#txt_remark").val('' + remark + '');
    $("#txt_period").val('' + period + '');
    $("#TextBox_epiboly_price").val('' + epiloy_price + '');
    $("#DropDownList_epiboly").val('' + waibao + '');
    //$('#dropUpdatecp').val('' + productid + '');

    console.log(productid);
    //设置产品下拉框的默认选中值
    var objList = document.getElementById("dropUpdatecp");
    for (var i = 0; i < objList.options.length; i++) {
        if (objList.options[i].value == productid) {
            objList.options[i].selected = true;
        }
    }
}
