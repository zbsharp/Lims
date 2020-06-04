$(function () {
    var date = new Date();
    $('#timestart').datebox('setValue', date.toLocaleDateString().replace(/\//g, '-'));
    //toLocaleDateString().split('/').join('-');
    var time = date.setDate(date.getDate() + 1);
    var s = new Date(time);
    $('#timestop').datebox('setValue', s.toLocaleDateString().replace(/\//g, '-'));
    Loaddg();

    $('#btnselect').click(function () {
        var starttime = $('#timestart').datebox('getValue');
        var stoptime = $('#timestop').datebox('getValue');
        console.log(starttime);
        console.log(stoptime);

        $('#dg').datagrid('load', { btnselect: '是', start: starttime, stop: stoptime });
    });
});

function Loaddg() {
    var starttime = $('#timestart').datebox('getValue');
    var stoptime = $('#timestop').datebox('getValue');
    $('#dg').datagrid({
        url: 'exprot.ashx?parameter=Select&starttime=' + starttime + '&stoptime=' + stoptime + '',
        //fitColumns:'true',//宽度自适应
        striped: 'true',//斑马线
        rownumbers: 'true',//显示行号
        columns: [[
            { field: 'ID', title: 'id', hidden: 'true', sortable: true },
            { field: 'Bookingstatus', title: '状态', width: 70 },
            { field: 'Region', title: '地区', width: 40 },
            { field: 'Starttime', title: '预约开始时间', width: 135, sortable: true },
            { field: 'Stoptime', title: '预约结束时间', width: 135, sortable: true },
            { field: 'Testitem', title: '测试场地', width: 100 },
            { field: 'Testsite', title: '测试项目', width: 100 },
            { field: 'Teststandard', title: '测试标准', width: 100 },
            { field: 'model', title: '型号', width: 100 },
            { field: 'Customername', title: '客户名称', width: 100 },
            { field: 'Linkman', title: '客户联系人', width: 100 },
            { field: 'Linkmanphone', title: '联系电话', width: 100 },
            { field: 'Project', title: '产品', width: 100 },
            { field: 'Responser', title: '预约人', width: 100 },
            { field: 'Fillname', title: '创建人', width: 100 },
            { field: 'Filltime', title: '创建时间', width: 100 },
            { field: 'Isscene', title: '客户是否来现场', width: 100 },
            { field: 'Price', title: '标准价格', width: 100 },
            { field: 'Newprice', title: '实际价格', width: 100 },
            { field: 'hour', title: '预约时长', width: 100 },
            { field: 'Shijihour', title: '实际时长', width: 100 },
            { field: 'Sumprice', title: '预约总金额', width: 100 },
            { field: 'Shijisubprice', title: '实际总金额', width: 100 },
            { field: 'Isfree', title: '是否免费', width: 100 },
            { field: 'Isreceive', title: '是否到款', width: 100 },
            { field: 'Money', title: '到款金额', width: 100 },
            { field: 'EMCnumber', title: '测试单号', width: 100 },
            { field: 'Engineer', title: '测试人', width: 100 },
            { field: 'Baojiaid', title: '报价编号', width: 100 },
            { field: 'EMCid', title: '预约编号', width: 130 },
            { field: 'Remark', title: '备注', width: 100 }
        ]]
    });
}


function toExcel() {
    var body = $('#dg').datagrid('toArray');
    var docDefinition = {
        content: [{
            table: {
                headerRows: 4,
                widths: ['*', '*', '*', '*', 'auto', '*'],
                body: body
            }
        }]
    };
    pdfMake.createPdf(docDefinition).open();
}