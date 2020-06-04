$(function () {
    GetSession();
    Windowload();
    GetCustomer();
    GetBankaccount();
    Datagridload();
    Chargeload();
    Currency();
    $('#btnAdd').click(function () {
        $('#win').window('open');
    });
    $('#bttyes').click(function () {
        Generate();
    });
    $('#btndelete').click(function () {
        var rows = $('#tb').datagrid("getSelected");
        if (rows != null) {
            var id = rows.ID;
            var chargeid = rows.Chargeid;
            Delete(id, chargeid);
        }
        else {
            alert('请选择所要取消的行');
        }
    });
});

//模态框
function Windowload() {
    $('#win').window({
        width: 780,
        height: 440,
        modal: true,
        closed: true,
        title: '生成收费单'
    });
}

//获取session值
var responser;
function GetSession() {
    $.ajax({
        url: 'charge.ashx?parameter=load',
        dataType: 'text',
        cache: 'false',
        success: function (data) {
            //$('#lbsession').text(data);
            responser = data;
        }
    });
}

//客户名称   绑定龙华模态框
function GetCustomer() {
    $('#cmbcustomer').combobox({
        url: 'charge.ashx?parameter=customer&responser=' + responser + '',
        valueField: 'Kehuid',
        textField: 'CustomerName',
        panelHeight: 'auto',
        onHidePanel: function () {
            //联系人
            var kehuid = $('#cmbcustomer').combobox('getValue');
            $.ajax({
                url: 'charge.ashx?parameter=linkman&kehuid=' + kehuid + '',
                dataType: 'json',
                type: 'post',
                success: function (data) {
                    $('#cmblinkman').combobox({   //绑定联系人的名称
                        data: data,
                        valueField: "ID",
                        textField: "Name",
                        panelHeight: 'auto',
                        onHidePanel: function () {   //绑定联系人的电话
                            var id = $('#cmblinkman').combobox('getValue');
                            $.ajax({
                                url: 'charge.ashx?parameter=phone&id=' + id + '',
                                dataType: 'text',
                                cache: 'false',
                                success: function (data) {
                                    $("#txphone").textbox('setValue', data);
                                    $('#txphone').textbox('readonly', true);
                                }
                            });
                        }
                    });
                }
            });
        }
    });
}

//收款银行
function GetBankaccount() {
    $('#cmbBankaccount').combobox({
        url: 'charge.ashx?parameter=Bankaccount',
        valueField: 'ID',
        textField: 'Name',
        panelHeight: 'auto'
    });
}

//datagrid
function Datagridload() {
    $('#dg').datagrid({
        url: 'charge.ashx?parameter=Select',
        striped: 'true',//斑马线
        rownumbers: 'true',//显示行号
        singleSelect: false,//允许多行选中
        fitColumns: false,
        autoRowHeight: false,
        scrollbarSize: 50,
        emptyMsg: '<span>无记录</span>',
        columns: [[
            { field: 'ID', title: 'id', hidden: 'true' },
            { title: '', field: 'ck', checkbox: true, sortable: false },  //添加checkbox  
            { field: 'Region', title: '地区', width: 40 },
            { field: 'Starttime', title: '预约开始时间', width: 135, },
            { field: 'Stoptime', title: '预约结束时间', width: 135 },
            { field: 'Filltime', title: '创建时间', width: 135 },
            { field: 'Site', title: '测试场地', width: 100 },
            { field: 'Hour', title: '测试时长', width: 100 },
            { field: 'Money', title: '合计金额', width: 100 },
            { field: 'Test', title: '测试项目', width: 100 },
            { field: 'Teststandard', title: '测试标准', width: 100 },
            { field: 'Customername', title: '客户名称', width: 100 },
            { field: 'Project', title: '产品', width: 100 },
            { field: 'Responser', title: '预约人', width: 100 },
            { field: 'Remork', title: '备注', width: 100 }
        ]]
    });
}

//生成
function Generate() {
    var customner = $('#cmbcustomer').combobox('getValue');
    var linkman = $('#cmblinkman').combobox('getValue');
    var Bankaccount = $('#cmbBankaccount').combobox('getValue');
    var checkedItems = $('#dg').datagrid('getChecked');
    var currency = $('#cmbcurrency').combobox('getValue');
    var remark = $('#txremork').textbox('getValue');
    var emcids = [];
    $.each(checkedItems, function (index, item) {
        emcids.push(item.ID);
    });

    if (emcids.length === 0) {
        alert('请选择预约项目');
        return;
    }

    console.log(emcids);

    var obj = { customner: customner, linkman: linkman, Bankaccount: Bankaccount, emcid: emcids.join(','), currency: currency, remark: remark };
    console.log(obj);

    $.ajax({
        url: 'charge.ashx?parameter=Add',
        type: 'post',
        dataType: 'json',
        data: { jsonobj: JSON.stringify(obj) },
        success: function (data) {
            if (data == "ok") {
                $('#win').window('close');
                $('#tb').datagrid('reload');// 重新载入当前页面数据  
            } else if (data == "on") {
                alert('生成失败');
            }
            else {
                alert("服务器异常、请联系管理员");
            }
        }
        //});
    });
}

//币种下拉框
function Currency() {
    $('#cmbcurrency').combobox({
        data: [{
            'id': '人民币',
            'text': '人民币'
        }, {
            'id': '美元',
            'text': '美元'
        }],
        valueField: 'id',
        textField: 'text',
        panelHeight: 'auto',
        onLoadSuccess: function () {
            $('#cmbcurrency').combobox('setValue', '人民币');
        }
    });
}

//加载datagrid
function Chargeload() {
    $('#tb').datagrid({
        url: 'charge.ashx?parameter=SelectCharge',
        fitColumns: 'true',//宽度自适应
        striped: 'true',//斑马线
        rownumbers: 'true',//显示行号
        singleSelect: 'true',//只允许选择一行
        pagination: 'true',
        pagePosition: 'bottom',//分页栏的位置
        pageList: [10, 20],
        pageSize: 10,//初始化页面大小
        emptyMsg: '<span>无记录</span>',
        columns: [[
            { field: 'ID', title: 'id', hidden: 'true' },
            { field: 'Chargeid', title: '收费单号', width: 100 },
            { field: 'Customer', title: '客户名称', width: 135, },
            { field: 'linkman', title: '客户联系人', width: 70 },
            { field: 'Openaccot', title: '收款银行', width: 100 },
            { field: 'Money', title: '总金额', width: 50 },
            { field: 'Currency', title: '币种', width: 50 },
            { field: 'Filltime', title: '生成时间', width: 100 },
            { field: 'Fillname', title: '生成人', width: 100 },
            { field: 'Remark', title: '备注', width: 100 },
            {
                field: 'a', title: 'PDF', width: 100, formatter: function (value, row, index) {
                    return "<a href='javascript:void(0)' onclick=\"PDF('" + row.Chargeid + "')\">PDF</a>";
                }
            }
        ]]
    });
}

function Delete(id, chargeid) {
    $.ajax({
        url: 'charge.ashx?parameter=Delete',
        type: 'post',
        dataType: 'text',
        data: { id: id, chargeid: chargeid },
        success: function (data) {
            if (data == 'ok') {
                $('#tb').datagrid('reload');// 重新载入当前页面数据  
            }
            else if (data == 'on') {
                alert('删除失败');
            }
            else {
                alert('服务器异常、请联系管理员');
            }
        }
    });
}


function PDF(chargeid) {
    window.open("../EMCmate/PDF.aspx?chargeid=" + chargeid + "");
}
