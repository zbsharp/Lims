$(function () {
    var myDate = new Date();
    var time = myDate.toLocaleDateString().split('/').join('-');
    $('#dd').datebox('setValue', time);
    $('#btnselect').click(function () {
        LoadDataGrid();
    });

    $('#btnselectFY').click(function () {
        var time = $('#dd').datebox('getValue');
        //window.location.href = '\spreadFY.html?' + time + ''; 
        window.open('datatableFY.html?time=' + time + '&area=FY');
    });
    $('#btnselectLH').click(function () {
        var time = $('#dd').datebox('getValue');
        // window.location.href = '\spreadFY.html?' + time + '';
        window.open('datatableLH.html?time=' + time + '&area=LH');
    });
});

function LoadDataGrid() {
    var time = $('#dd').datebox('getValue');
    var region = $('#cc').combobox('getText');
    if (region == '龙华') {
        region = 'LH';
    }
    else {
        region = 'FY'
    }
    $('#dg').datagrid({
        url: 'select.ashx?parameter=Select&time=' + time + '&region=' + region + '',
        fitColumns: 'true',//宽度自适应
        striped: 'true',//斑马线
        rownumbers: 'true',//显示行号
        singleSelect: 'true',//只允许选择一行
        //pagination: 'true',
        //pagePosition: 'bottom',//分页栏的位置
        //pageList: [10, 20],
        //pageSize: 10,//初始化页面大小
        columns: [[
            { field: 'id', title: 'id', hidden: 'true' },
            { field: 'Testitem', title: '测试场地', width: 350 },
            { field: 'Starttime', title: '预约开始时间', width: 200, styler: function (value, row, index) { return 'background-color:#FFFFCC;color:black'; } },
            { field: 'Stoptime', title: '预约结束时间', width: 200, styler: function (value, row, index) { return 'background-color:#FFFF66;color:black'; } },
            { field: 'Responser', title: '预约人', width: 100 },
            { field: 'Fillname', title: '创建人', width: 100 },
            { field: 'Customername', title: '客户名称', width: 100 },
            { field: 'Remark', title: '备注', width: 100 },
            { field: 'EMCid', title: '预约编号', width: 140 }
        ]]
    });
}
