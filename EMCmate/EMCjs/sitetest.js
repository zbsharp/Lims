$(function () {
    //打开龙华预约模态框
    $('#btnLH').click(function () {
        $('#win').window('open');
    });
    //关闭龙华预约模态框
    $('#btnclose').click(function () {
        $('#win').window('close');
    });
    //关闭龙华编辑模态框
    $('#btnupdateclose').click(function () {
        $('#winupdate').window('close');
    });
    //关闭前台编辑模态框
    $('#btnreceptionistClose').click(function () {
        $('#win_receptionist').window('close');
    });
    //关闭福永编辑模态框
    $('#btnupdatecloseFY').click(function () {
        $('#winupdateFY').window('close');
    });
    //添加龙华预约
    $('#btninsert').click(function () {
        LHSubmitForm();
    });
    //添加福永预约
    $('#btninsertFY').click(function () {
        FYSubmitForm();
    });
    //打开福永预约模态框
    $('#btnFY').click(function () {
        $('#winFY').window('open');
    });
    //关闭福永预约模态框
    $('#btncloseFY').click(function () {
        $('#winFY').window('close');
    });
    //删除预约
    $('#btndelete').click(function () {
        var rows = $('#dg').datagrid("getSelected");
        if (rows != null) {
            if (rows.Bookingstatus == '测试完成' || rows.Bookingstatus == '取消') {
                alert('已测试完成或被取消的预约项不能更改或删除');
            }
            else {
                var id = rows.ID;
                Delete(id);
            }
        }
        else {
            alert('请选择所要取消的行');
        }
    });
    //修改龙华预约信息
    $('#btnupdate').click(function () {
        Update();
    });
    //修改福永预约信息
    $('#btnupdateFY').click(function () {
        UpdateFY();
    });
    //前台修改事件
    $('#btnreceptionistYES').click(function () {
        Updatereceptionist();
    });
    //模糊查询
    $('#btnselect').click(function () {
        var condition = $('#txt').textbox('getValue');
        var area = $('#cmbarea').combobox('getText');
        var date = $('#dd').datebox('getValue');
        if (condition == '' && area == '' && date == '') {
            alert('请输入查询信息');
        }
        else {
            Selectcondition(condition, area, date);
        }
    });

    GetSession();
    Comload();
    ComloadFY();
    Windowload();
    WindowloadFY();
    Winupdate();
    WinupdateFY();
    Winreceptionist();
    LayDate();
    GetCustomer();
    GetCustomerFY();
    LoadDataGrid();
    //IsCustomer();
    SelectUserinfo();
    LoadArea();
    Bookingstatus();
    //DateboxLoad();

    //调用datagrid的扩展方法
    ExtendMehod();
    $('#dg').datagrid('columnMoving');

});

//删除
function Delete(id) {
    $.ajax({
        url: 'sitetest.ashx?parameter=delete&id=' + id + '',
        type: 'post',
        dataType: 'text',
        success: function (data) {
            if (data == 'ok') {
                $('#dg').datagrid('reload');// 重新载入当前页面数据  
            }
            else {
                alert('服务器异常、请通知管理员');
            }
        }
    });
}

//判断是否查询到了客户信息
function IsCustomer() {
    $('#cmbcustomer').combobox('textbox').bind('blur', function () {
        //  获得输入值  
        var datas = $('#cmbcustomer').combobox('getData');
        var size = datas.length;
        if (size == 0) {
            $('#cmbcustomer').combobox('clear');
        }
    });
}

//下拉框（龙华测试场地）
function Comload() {
    $('#cbtestitem').combobox({
        url: 'sitetest.ashx?parameter=region',
        valueField: 'Id',
        textField: 'Testsite',
        panelHeight: 'auto',
        onHidePanel: function () {
            var id = $('#cbtestitem').combobox('getValue');
            if (id != '') {
                $.ajax({
                    url: 'sitetest.ashx?parameter=regiontwo',
                    type: 'post',
                    data: { id: id },
                    dataType: 'json',
                    cache: 'false',
                    success: function (data) {
                        $('#cbxm').combobox({
                            data: data,
                            valueField: "Id",
                            textField: "Item",
                            panelHeight: 'auto',
                            onLoadSuccess: function () {
                                var cbxmdata = $('#cbxm').combobox('getData');//获取所有下拉框数据
                                if (cbxmdata.length > 0) {
                                    $("#cbxm").combobox("select", cbxmdata[0].Id);//设置下拉框默认选中项
                                }
                                var chkcbid = $('#cbxm').combobox('getValue');//获取选中的实际值
                                $.ajax({
                                    url: 'sitetest.ashx?parameter=regionprice',
                                    dataType: 'text',
                                    data: { id: chkcbid },
                                    cache: 'false',
                                    success: function (data) {
                                        $('#txprice').textbox('setValue', data);
                                        $('#txprice').textbox('readonly', true);
                                    }
                                })
                            }
                        });
                    }
                });
            }
        }
    });
}

//下拉框（福永测试场地）
function ComloadFY() {
    $('#cbtestitemFY').combobox({
        url: 'sitetest.ashx?parameter=regionFY',
        valueField: 'Id',
        textField: 'Testsite',
        panelHeight: 'auto',
        onHidePanel: function () {
            var id = $('#cbtestitemFY').combobox('getValue');
            if (id != '') {
                $.ajax({
                    url: 'sitetest.ashx?parameter=regiontwoFY',
                    type: 'post',
                    data: { id: id },
                    dataType: 'json',
                    cache: 'false',
                    success: function (data) {
                        $('#cbxmFY').combobox({
                            data: data,
                            valueField: "Id",
                            textField: "Item",
                            panelHeight: 'auto',
                            onLoadSuccess: function () {
                                var cbxmdata = $('#cbxmFY').combobox('getData');//获取所有下拉框数据
                                if (cbxmdata.length > 0) {
                                    $("#cbxmFY").combobox("select", cbxmdata[0].Id);//设置下拉框默认选中项
                                }
                                var chkcbid = $('#cbxmFY').combobox('getValue');//获取选中的实际值
                                $.ajax({
                                    url: 'sitetest.ashx?parameter=regionprice',
                                    dataType: 'text',
                                    data: { id: chkcbid },
                                    cache: 'false',
                                    success: function (data) {
                                        $('#txpriceFY').textbox('setValue', data);
                                        $('#txpriceFY').textbox('readonly', true);
                                    }
                                })
                            }
                        });
                    }
                });
            }
        }
    });
}

//模态框 龙华预约
function Windowload() {
    $('#win').window({
        width: 480,
        height: 440,
        modal: true,
        closed: true,
        title: '龙华场地'
    });
}

//模态框 福永预约
function WindowloadFY() {
    $('#winFY').window({
        width: 480,
        height: 440,
        modal: true,
        closed: true,
        title: '福永场地'
    });
}

//获取session值
var dutyname = '';
function GetSession() {
    //IE浏览器有缓存机制会对请求的url进行判断，发现短时间内请求url相同，则使用缓存的数据而不是重新去服务器获取一次数据、所以加个随机数作为参数
    $.ajax({
        url: 'sitetest.ashx?parameter=load&' + Math.random() + '',
        dataType: 'Json',
        //cache: 'false',
        success: function (data) {
            var obj = eval("(" + data + ")");//转换后的JSON对象
            $("#cmbResponser").combobox('setValue', obj.responser);
            $("#cmbResponserFY").combobox('setValue', obj.responser);
            dutyname = obj.dutyname;
        }
    });
}

//layDate日期控件
function LayDate() {
    laydate.render({
        elem: '#datestart', //指定元素
        type: 'datetime',
        trigger: 'click'//采用click弹出
    });
    laydate.render({
        elem: '#datestop',
        type: 'datetime',
        trigger: 'click'
    });
    laydate.render({
        elem: '#txupstartdate',
        type: 'datetime',
        trigger: 'click'
    });
    laydate.render({
        elem: '#txupstopdate',
        type: 'datetime',
        trigger: 'click'
    });
    laydate.render({
        elem: '#txupstartdateFY',
        type: 'datetime',
        trigger: 'click'
    });
    laydate.render({
        elem: '#txupstopdateFY',
        type: 'datetime',
        trigger: 'click'
    });
    laydate.render({
        elem: '#datestartFY',
        type: 'datetime',
        trigger: 'click'
    });
    laydate.render({
        elem: '#datestopFY',
        type: 'datetime',
        trigger: 'click'
    });

    laydate.render({
        elem: '#txstartdate_receptionist',
        type: 'datetime',
        trigger: 'click'
    });
    laydate.render({
        elem: '#txstopdate_receptionist',
        type: 'datetime',
        trigger: 'click'
    });



}

//客户名称   绑定龙华模态框
function GetCustomer() {
    $('#cmbcustomer').combobox({
        valueField: 'Kehuid',
        textField: 'CustomerName',
        panelHeight: 'auto',
        onChange: function (newValue) {
            //newValue下拉框的显示值
            var Responser = $('#cmbResponser').combobox('getText');
            var index = $.inArray(newValue, arraystr);
            if (newValue != null && newValue != '' && newValue.length > 1 && index == -1) {
                var customername = $('#cmbcustomer').combobox('getText');
                var urlStr = "sitetest.ashx?parameter=customer&customnername=" + customername + "&Responser=" + Responser + "";
                $('#cmbcustomer').combobox("reload", urlStr);
            }
        },
        onSelect: function () {
            //联系人
            var kehuid = $('#cmbcustomer').combobox('getValue');
            $.ajax({
                url: 'sitetest.ashx?parameter=linkman&kehuid=' + kehuid + '',
                dataType: 'json',
                type: 'post',
                success: function (data) {
                    $('#cmblinkman').combobox({   //绑定联系人的名称
                        data: data,
                        valueField: "Id",
                        textField: "Name",
                        panelHeight: 'auto',
                        onHidePanel: function () {   //绑定联系人的电话
                            var id = $('#cmblinkman').combobox('getValue');
                            if (regnumber.test(id) == false) {
                            }
                            else {
                                $.ajax({
                                    url: 'sitetest.ashx?parameter=phone&id=' + id + '',
                                    dataType: 'text',
                                    cache: 'false',
                                    success: function (data) {
                                        $("#txphone").textbox('setValue', data);
                                        //$('#txphone').textbox('readonly', true);
                                    }
                                });
                            }
                        }
                    });
                }
            });
        }
    });
}

//客户名称   绑定福永模态框
function GetCustomerFY() {
    $('#cmbcustomerFY').combobox({
        valueField: 'Kehuid',
        textField: 'CustomerName',
        panelHeight: 'auto',
        onChange: function (newValue) {
            //newValue下拉框的显示值
            var Responser = $('#cmbResponserFY').combobox('getText');
            var index = $.inArray(newValue, arraystr);
            if (newValue != null && newValue != '' && newValue.length > 1 && index == -1) {
                var customername = $('#cmbcustomerFY').combobox('getText');
                var urlStr = "sitetest.ashx?parameter=customer&customnername=" + customername + "&Responser=" + Responser + "";
                $('#cmbcustomerFY').combobox("reload", urlStr);
            }
        },
        onSelect: function () {
            //联系人
            var kehuid = $('#cmbcustomerFY').combobox('getValue');
            $.ajax({
                url: 'sitetest.ashx?parameter=linkman&kehuid=' + kehuid + '',
                dataType: 'json',
                type: 'post',
                success: function (data) {
                    $('#cmblinkmanFY').combobox({   //绑定联系人的名称
                        data: data,
                        valueField: "Id",
                        textField: "Name",
                        panelHeight: 'auto',
                        onHidePanel: function () {   //绑定联系人的电话
                            var id = $('#cmblinkmanFY').combobox('getValue');
                            if (regnumber.test(id) == false) {
                            }
                            else {
                                $.ajax({
                                    url: 'sitetest.ashx?parameter=phone&id=' + id + '',
                                    dataType: 'text',
                                    cache: 'false',
                                    success: function (data) {
                                        $("#txphoneFY").textbox('setValue', data);
                                        //$('#txphoneFY').textbox('readonly', true);
                                    }
                                });
                            }
                        }
                    });
                }
            });
        }
    });
}

//龙华场地提交表单
function LHSubmitForm() {
    var region = '龙华';
    var cbtestitem = $('#cbtestitem').combobox('getText');//测试场地
    var datestart = $('#datestart').val();//预约开始时间
    var datestop = $('#datestop').val();//预约结束时间
    var cbxm = $('#cbxm').combobox('getText');//测试项目
    var customername = $('#cmbcustomer').combobox('getText');//客户
    var cmblinkman = $('#cmblinkman').combobox('getText');//客户联系人
    var linkmanphone = $('#txphone').textbox('getValue');//客户联系电话
    var project = $('#project').textbox('getValue');//产品名称
    var price = $('#txprice').textbox('getValue');//原始单价
    var baojiaid = $('#txbaojiaid').textbox('getValue');//报价编号
    var chk = '是';//客户是否来现场
    var isfree = '是';//是否免费
    var model = $('#txmodel').textbox('getValue');//测试型号
    var responser = $('#cmbResponser').combobox('getText');

    var timestate = TimeSpan(datestart, datestop);
    if (timestate == false) {
        return;
    }

    if (document.getElementById('chkfree').checked) {
    }
    else {
        isfree = '否';
    }
    if (document.getElementById('chkcustomer').checked) { }
    else {
        chk = '否';
    }
    if (isfree == '是') {
        if (baojiaid == null || baojiaid == '') {
            alert('申请免费测试时、需填写报价编号');
            return;
        }
    }

    var teststandard = $('#teststandard').textbox('getValue');//测试标准
    var money = $('#txmoney').textbox('getValue');//测试费用
    var remork = $('#txremork').textbox('getValue');//备注

    if (reg.test(money) == false) {
        alert('金额输入不合法');
        $('#txmoney').textbox('setValue', '');
        return;
    }

    var empty = Validation(cbtestitem, customername, cmblinkman, datestart, datestop, money);
    if (empty == 'on') {
    }
    else {
        $.ajax({
            url: 'sitetest.ashx?parameter=LHadd',
            type: 'post',
            dataType: 'text',
            data: { region: region, testcd: cbtestitem, datestart: datestart, datestop: datestop, testxm: cbxm, customername: customername, linkman: cmblinkman, linkmanphone: linkmanphone, project: project, chk: chk, teststandard: teststandard, money: money, price: price, isfree: isfree, remork: remork, baojiaid: baojiaid, responser: responser, model: model },
            cache: 'false',
            success: function (data) {
                if (data == "OK") {
                    $('#win').window('close');
                    $('#dg').datagrid('load');// 重新载入当前页面数据  
                }
                else if (data == 'ON') {
                    alert('预约失败,服务器异常');
                }
                else if (data == 'date') {
                    alert('星期日请找实验室或前台预约');
                }
                else {
                    alert(datestart + '到' + datestop + '时间段\r\n与' + data + '有冲突！不能重复预约');
                }
            }
        });
    }
}

//福永场地提交表单
function FYSubmitForm() {
    var region = '福永';
    var respoonser = $('#cmbResponserFY').combobox('getText');//预约人
    var cbtestitem = $('#cbtestitemFY').combobox('getText');//测试场地
    var datestart = $('#datestartFY').val();//预约开始时间
    var datestop = $('#datestopFY').val();//预约结束时间
    var cbxm = $('#cbxmFY').combobox('getText');//测试项目
    var customername = $('#cmbcustomerFY').combobox('getText');//客户
    var cmblinkman = $('#cmblinkmanFY').combobox('getText');//客户联系人
    var linkmanphone = $('#txphoneFY').textbox('getValue');//客户联系电话
    var project = $('#projectFY').textbox('getValue');//产品名称
    var price = $('#txpriceFY').textbox('getValue');//原始单价
    var baojiaid = $('#txbaojiaidFY').textbox('getValue');//报价编号
    var model = $('#txmodelFY').textbox('getValue');//测试型号
    var chk = '是';//客户是否来现场
    var isfree = '是';//是否免费

    var timestate = TimeSpan(datestart, datestop);
    if (timestate == false) {
        return;
    }

    if (document.getElementById('chkfreeFY').checked) { }
    else {
        isfree = '否';
    }
    if (document.getElementById('chkcustomerFY').checked) { }
    else {
        chk = '否';
    }

    if (isfree == '是') {
        if (baojiaid == '' || baojiaid == null) {
            alert('申请免费测试时、需填写报价编号');
            return;
        }
    }

    var teststandard = $('#teststandardFY').textbox('getValue');//测试标准
    var money = $('#txmoneyFY').textbox('getValue');//测试费用
    var remork = $('#txremorkFY').textbox('getValue');//备注
    if (reg.test(money) == false) {
        alert('金额输入不合法');
        $('#txmoneyFY').textbox('setValue', '');
        return;
    }

    var empty = Validation(cbtestitem, customername, cmblinkman, datestart, datestop, money);
    if (empty == 'on') {
    }
    else {
        $.ajax({
            url: 'sitetest.ashx?parameter=FYadd',
            type: 'post',
            dataType: 'text',
            data: { region: region, testcd: cbtestitem, datestart: datestart, datestop: datestop, testxm: cbxm, customername: customername, linkman: cmblinkman, linkmanphone: linkmanphone, project: project, chk: chk, teststandard: teststandard, money: money, price: price, isfree: isfree, remork: remork, baojiaid: baojiaid, respoonser: respoonser, model: model },
            cache: 'false',
            success: function (data) {
                if (data == "OK") {
                    $('#winFY').window('close');
                    $('#dg').datagrid('reload');// 重新载入当前页面数据  
                }
                else if (data == "ON") {
                    alert('预约失败,服务器异常');
                }
                else if (data == 'date') {
                    alert('星期日请找实验室或前台预约');
                }
                else {
                    alert(datestart + '到' + datestop + '时间段\r\n与' + data + '有冲突！不能重复预约');
                }
            }
        });
    }
}

//查询用户
function SelectUserinfo() {
    $('#cmbupResponser').combobox({
        url: 'sitetest.ashx?parameter=Userinfo',
        textField: 'Responser',
        valueField: 'Responser',
        panelHeight: 'auto'
    });

    $('#cmbupResponserFY').combobox({
        url: 'sitetest.ashx?parameter=Userinfo',
        textField: 'Responser',
        valueField: 'Responser',
        panelHeight: 'auto'
    });

    $('#cmbResponser').combobox({
        url: 'sitetest.ashx?parameter=Userinfo',
        textField: 'Responser',
        valueField: 'Responser',
        panelHeight: 'auto'
    });

    $('#cmbResponserFY').combobox({
        url: 'sitetest.ashx?parameter=Userinfo',
        textField: 'Responser',
        valueField: 'Responser',
        panelHeight: 'auto'
    });

    $('#cmbResponser_receptionist').combobox({
        url: 'sitetest.ashx?parameter=Userinfo',
        textField: 'Responser',
        valueField: 'Responser',
        panelHeight: 'auto'
    });
}

//datagrid
function LoadDataGrid() {
    $('#dg').datagrid({
        url: 'sitetest.ashx?parameter=Select',
        //fitColumns:'true',//宽度自适应
        striped: 'true',//斑马线
        rownumbers: 'true',//显示行号
        singleSelect: 'true',//只允许选择一行
        pagination: 'true',
        pagePosition: 'bottom',//分页栏的位置
        pageList: [10, 20],
        pageSize: 10,//初始化页面大小
        remoteSort: false,//排序不刷新服务器
        sortName: '',
        sortOrder: 'asc',
        columns: [[
            { field: 'ID', title: 'id', hidden: 'true', sortable: true },
            { field: 'Bookingstatus', title: '状态', width: 70 },
            { field: 'Region', title: '地区', width: 40 },
            {
                field: 'Starttime', title: '预约开始时间', width: 135, sortable: true,
                sorter: function (a, b) {
                    a = a.split('/');
                    b = b.split('/');
                    if (a[0] == b[0]) {
                        if (a[1] == b[1]) {
                            return (a[2] > b[2] ? 1 : -1);
                        } else {
                            return (a[1] > b[1] ? 1 : -1);
                        }
                    } else {
                        return (a[0] > b[0] ? 1 : -1);
                    }
                }
            },
            {
                field: 'Stoptime', title: '预约结束时间', width: 135, sortable: true,
                sorter: function (a, b) {
                    a = a.split('/');
                    b = b.split('/');
                    if (a[0] == b[0]) {
                        if (a[1] == b[1]) {
                            return (a[2] > b[2] ? 1 : -1);
                        } else {
                            return (a[1] > b[1] ? 1 : -1);
                        }
                    } else {
                        return (a[0] > b[0] ? 1 : -1);
                    }
                }
            },
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
        ]],
        onDblClickRow: function (index, Data) {//双击单元格
            var region = Data.Region;
            if (dutyname == '业务员' || dutyname == '客户经理' || dutyname == '销售助理') {
                if (Data.Bookingstatus == '测试完成' || Data.Bookingstatus == '取消') {
                    alert('已测试完成或被取消的预约项不能更改或删除');
                }
                else {
                    if (region == '龙华') {
                        UpdateLoad(Data);
                        $('#winupdate').window('open');
                    }
                    else {
                        UpdateLoadFY(Data);
                        $('#winupdateFY').window('open');
                    }
                }
            }
            else {
                UpdateLoadreceptionist(Data);
                $('#win_receptionist').window('open');
            }
        }
    });
}

//模态框  实验室编辑
function Winreceptionist() {
    $('#win_receptionist').window({
        width: 420,
        height: 400,
        modal: true,
        closed: true,
        title: '编辑'
    });
}

//模态框  编辑龙华
function Winupdate() {
    $('#winupdate').window({
        width: 420,
        height: 340,
        modal: true,
        closed: true,
        title: '编辑龙华预约'
    });
}

//模态框  编辑福永
function WinupdateFY() {
    $('#winupdateFY').window({
        width: 420,
        height: 340,
        modal: true,
        closed: true,
        title: '编辑福永预约'
    });
}

//编辑信息赋值 龙华
function UpdateLoad(data) {
    $('#emcid').text(data.EMCid);
    $('#txupstartdate').val(data.Starttime);
    $('#txupstopdate').val(data.Stoptime);
    $('#txupprice').textbox('setValue', data.Newprice);
    $('#cmbupResponser').combobox('setValue', data.Responser);
    $('#txupremork').textbox('setValue', data.Remark);
    $('#cd').text(data.Testitem);
}

//编辑信息赋值 福永
function UpdateLoadFY(data) {
    $('#emcidFY').text(data.EMCid);
    $('#txupstartdateFY').val(data.Starttime);
    $('#txupstopdateFY').val(data.Stoptime);
    $('#txuppriceFY').textbox('setValue', data.Newprice);
    $('#cmbupResponserFY').combobox('setValue', data.Responser);
    $('#txupremorkFY').textbox('setValue', data.Remark);
    $('#cdFY').text(data.Testitem);
}

//编辑信息赋值 前台
function UpdateLoadreceptionist(data) {
    $('#emcid_receptionist').text(data.EMCid);
    $('#txstartdate_receptionist').val(data.Starttime);
    $('#txstopdate_receptionist').val(data.Stoptime);
    $('#txprice_receptionist').textbox('setValue', data.Newprice);
    $('#cmbResponser_receptionist').combobox('setValue', data.Responser);
    $('#txupremork_receptionist').textbox('setValue', data.Remark);
    $('#cmbResponser_bookingstatus').combobox('setValue', data.Bookingstatus);
    $('#txEMCnumber_receptionist').textbox('setValue', data.EMCnumber);
    $('#txengineer_receptionist').textbox('setValue', data.Engineer);
    $('#txshijisumprice_receptionist').textbox('setValue', data.Shijisubprice);
    $('#txshijihour_receptionist').textbox('setValue', data.Shijihour);
    $('#lbarea').text(data.Testitem);
}

//编辑事件  龙华
function Update() {
    var emcid = $('#emcid').text();
    var startdate = $('#txupstartdate').val();
    var stopdate = $('#txupstopdate').val();
    var price = $('#txupprice').textbox('getValue');
    var responser = $('#cmbupResponser').combobox('getText');
    var remork = $('#txupremork').textbox('getText');
    var cd = $('#cd').text();

    var timestate = TimeSpan(startdate, stopdate);
    if (timestate == false) {
        return;
    }

    if (reg.test(price) == false) {
        alert('金额输入不合法');
        $('#txupprice').textbox('setValue', '');
        return;
    }

    var list = { EMCid: emcid, Starttime: startdate, Stoptime: stopdate, Newprice: price, Responser: responser, Remark: remork, Testitem: cd };
    var jsonlist = JSON.stringify(list);
    $.ajax({
        url: 'sitetest.ashx?parameter=Update',
        type: 'post',
        dataType: 'json',
        data: { data: jsonlist },
        success: function (state) {
            console.log(state);
            if (state == 'ok') {
                $('#winupdate').window('close');
                $('#dg').datagrid('reload');// 重新载入当前页面数据  
            }
            else if (state == 'on') {
                alert('服务器异常、请联系管理员');
            }
            else if (state == 'date') {
                alert('星期日请找实验室或前台预约');
            }
            else {
                alert(startdate + '到' + stopdate + '时间段\r\n与' + state + '有冲突！不能重复预约');
            }
        }
    });
}

//编辑事件  福永
function UpdateFY() {
    var emcid = $('#emcidFY').text();
    var startdate = $('#txupstartdateFY').val();
    var stopdate = $('#txupstopdateFY').val();
    var price = $('#txuppriceFY').textbox('getValue');
    var responser = $('#cmbupResponserFY').combobox('getText');
    var remork = $('#txupremorkFY').textbox('getText');
    var cdFY = $('#cdFY').text();

    var timestate = TimeSpan(startdate, stopdate);
    if (timestate == false) {
        return;
    }

    if (reg.test(price) == false) {
        alert('金额输入不合法');
        $('#txuppriceFY').textbox('setValue', '');
        return;
    }

    var list = { EMCid: emcid, Starttime: startdate, Stoptime: stopdate, Newprice: price, Responser: responser, Remark: remork, Testitem: cdFY };
    var jsonlist = JSON.stringify(list);
    $.ajax({
        url: 'sitetest.ashx?parameter=UpdateFY',
        type: 'post',
        dataType: 'json',
        data: { data: jsonlist },
        success: function (state) {
            if (state == 'ok') {
                $('#winupdateFY').window('close');
                $('#dg').datagrid('reload');// 重新载入当前页面数据  
            }
            else if (state == 'on') {
                alert('服务器异常、请联系管理员');
            }
            else if (state == 'date') {
                alert('星期日请找实验室或前台预约');
            }
            else {
                alert(startdate + '到' + stopdate + '时间段\r\n与' + state + '有冲突！不能重复预约');
            }
        }
    });
}

//编辑事件  前台
function Updatereceptionist() {
    var area = $('#lbarea').text();
    var emcid = $('#emcid_receptionist').text();
    var startdate = $('#txstartdate_receptionist').val();
    var stopdate = $('#txstopdate_receptionist').val();
    var price = $('#txprice_receptionist').textbox('getValue');
    var responser = $('#cmbResponser_receptionist').combobox('getText');
    var remork = $('#txupremork_receptionist').textbox('getText');
    var bookingstatus = $('#cmbResponser_bookingstatus').combobox('getText');//状态
    var emcnumber = $('#txEMCnumber_receptionist').textbox('getValue');//测试单号
    var engineer = $('#txengineer_receptionist').textbox('getValue');//测试工程师
    var shijisumprice = $('#txshijisumprice_receptionist').textbox('getValue');//实际总费用
    var shijihour = $('#txshijihour_receptionist').textbox('getValue');//实际测试时常
    var timestate = TimeSpan(startdate, stopdate);
    if (timestate == false) {
        return;
    }

    if (reg.test(price) == false || reg.test(shijisumprice) == false) {
        alert('金额输入不合法');
        $('#txuppriceFY').textbox('setValue', '');
        return;
    }
    var list = { EMCid: emcid, Starttime: startdate, Stoptime: stopdate, Newprice: price, Responser: responser, Remark: remork, Bookingstatus: bookingstatus, Emcnumber: emcnumber, Engineer: engineer, Shijisubprice: shijisumprice, Shijihour: shijihour, Testitem: area };
    var jsonlist = JSON.stringify(list);
    $.ajax({
        url: 'sitetest.ashx?parameter=Updatereceptionist',
        type: 'post',
        dataType: 'json',
        data: { data: jsonlist },
        success: function (state) {
            if (state == 'ok') {
                $('#win_receptionist').window('close');
                $('#dg').datagrid('reload');// 重新载入当前页面数据  
            }
            else if (state == 'on') {
                alert('服务器异常、请联系管理员');
            }
            else {
                alert(startdate + '到' + stopdate + '时间段\r\n与' + state + '有冲突！不能重复预约');
            }
        }
    });
}

//模糊查询
function Selectcondition(txtshwere, area, date) {
    $('#dg').datagrid('load', { btnselect: '是', txtshwere: txtshwere, area: area, date: date });
}

//非空验证
function Validation(site, customer, linkman, datestart, datestop, price) {
    if (site == '') {
        alert('测试场地不能为空');
        return 'on';
    }
    else if (customer = '') {
        alert('客户名称不能为空');
        return 'on';
    }
    else if (linkman == '') {
        alert('联系人不能为空');
        return 'on';
    }
    else if (datestart == '' || datestop == '') {
        alert('日期不能为空');
        return 'on';
    }
    else if (price == '') {
        alert('价格不能为空');
        return 'on';
    }
    else {
        return 'ok';
    }
}

//日期判断
function TimeSpan(start, stop) {
    if (start >= stop) {
        alert("预约开始时间需小于结束时间");
        return false;
    }
    else {
        return true;
    }
}

//地区下拉框
function LoadArea() {
    $('#cmbarea').combobox({
        data: [{
            "id": "龙华",
            "text": "龙华"
        }, {
            "id": "福永",
            "text": "福永"
        }],
        valueField: 'id',
        textField: 'text',
        panelHeight: 'auto'
    });
}

//预约状态下拉框
function Bookingstatus() {
    $('#cmbResponser_bookingstatus').combobox({
        data: [{ id: '测试完成', text: '测试完成' }, { id: '取消', text: '取消' }, { id: '预约成功', text: '预约成功' }],
        valueField: 'id',
        textField: 'text',
        panelHeight: 'auto'
    });
}

//日期控件、默认为当前日期
function DateboxLoad() {
    var myDate = new Date();
    var time = myDate.toLocaleDateString().split('/').join('-');
    $('#dd').datebox('setValue', time);
}


//datagrid扩展方法
function ExtendMehod() {
    $.extend($.fn.datagrid.methods, {
        columnMoving: function (jq) {
            return jq.each(function () {
                var target = this;
                var cells = $(this).datagrid('getPanel').find('div.datagrid-header td[field]');
                cells.draggable({
                    revert: true,
                    cursor: 'pointer',
                    edge: 5,
                    proxy: function (source) {
                        var p = $('<div class="tree-node-proxy tree-dnd-no" style="position:absolute;border:1px solid #ff0000"/>').appendTo('body');
                        p.html($(source).text());
                        p.hide();
                        return p;
                    },
                    onBeforeDrag: function (e) {
                        e.data.startLeft = $(this).offset().left;
                        e.data.startTop = $(this).offset().top;
                    },
                    onStartDrag: function () {
                        $(this).draggable('proxy').css({
                            left: -10000,
                            top: -10000
                        });
                    },
                    onDrag: function (e) {
                        $(this).draggable('proxy').show().css({
                            left: e.pageX + 15,
                            top: e.pageY + 15
                        });
                        return false;
                    }
                }).droppable({
                    accept: 'td[field]',
                    onDragOver: function (e, source) {
                        $(source).draggable('proxy').removeClass('tree-dnd-no').addClass('tree-dnd-yes');
                        $(this).css('border-left', '1px solid #ff0000');
                    },
                    onDragLeave: function (e, source) {
                        $(source).draggable('proxy').removeClass('tree-dnd-yes').addClass('tree-dnd-no');
                        $(this).css('border-left', 0);
                    },
                    onDrop: function (e, source) {
                        $(this).css('border-left', 0);
                        var fromField = $(source).attr('field');
                        var toField = $(this).attr('field');
                        setTimeout(function () {
                            moveField(fromField, toField);
                            $(target).datagrid();
                            $(target).datagrid('columnMoving');
                        }, 0);
                    }
                });
                // move field to another location
                function moveField(from, to) {
                    var columns = $(target).datagrid('options').columns;
                    var cc = columns[0];
                    var c = _remove(from);
                    if (c) {
                        _insert(to, c);
                    }
                    function _remove(field) {
                        for (var i = 0; i < cc.length; i++) {
                            if (cc[i].field == field) {
                                var c = cc[i];
                                cc.splice(i, 1);
                                return c;
                            }
                        }
                        return null;
                    }
                    function _insert(field, c) {
                        var newcc = [];
                        for (var i = 0; i < cc.length; i++) {
                            if (cc[i].field == field) {
                                newcc.push(c);
                            }
                            newcc.push(cc[i]);
                        }
                        columns[0] = newcc;
                    }
                }
            });
        }
    });
}

//金额正则表达式
var reg = /(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)/;
//数字正则表达式
var regnumber = /^[0-9]*$/;


var arraystr =
    [
        "北京市", "公司", "有限公司", "有限", "有限公",
        "深圳",
        "深圳市",
        "上海",
        "广东",
        "天津市",
        "石家庄市",
        "唐山市",
        "秦皇岛市",
        "邯郸市",
        "邢台市",
        "保定市",
        "张家口市",
        "承德市",
        "沧州市",
        "廊坊市",
        "衡水市",
        "省直辖县",
        "太原市",
        "大同市",
        "阳泉市",
        "长治市",
        "晋城市",
        "朔州市",
        "晋中市",
        "运城市",
        "忻州市",
        "临汾市",
        "吕梁市",
        "呼和浩特市",
        "包头市",
        "乌海市",
        "赤峰市",
        "通辽市",
        "鄂尔多斯市",
        "呼伦贝尔市",
        "巴彦淖尔市",
        "乌兰察布市",
        "兴安盟",
        "锡林郭勒盟",
        "阿拉善盟",
        "沈阳市",
        "大连市",
        "鞍山市",
        "抚顺市",
        "本溪市",
        "丹东市",
        "锦州市",
        "营口市",
        "阜新市",
        "辽阳市",
        "盘锦市",
        "铁岭市",
        "朝阳市",
        "葫芦岛市",
        "长春市",
        "吉林市",
        "四平市",
        "辽源市",
        "通化市",
        "白山市",
        "松原市",
        "白城市",
        "延边朝鲜族自治州",
        "哈尔滨市",
        "齐齐哈尔市",
        "鸡西市",
        "鹤岗市",
        "双鸭山市",
        "大庆市",
        "伊春市",
        "佳木斯市",
        "七台河市",
        "牡丹江市",
        "黑河市",
        "绥化市",
        "大兴安岭地区",
        "上海市",
        "南京市",
        "无锡市",
        "徐州市",
        "常州市",
        "苏州市",
        "南通市",
        "连云港市",
        "淮安市",
        "盐城市",
        "扬州市",
        "镇江市",
        "泰州市",
        "宿迁市",
        "杭州市",
        "宁波市",
        "温州市",
        "嘉兴市",
        "湖州市",
        "绍兴市",
        "金华市",
        "衢州市",
        "舟山市",
        "台州市",
        "丽水市",
        "合肥市",
        "芜湖市",
        "蚌埠市",
        "淮南市",
        "马鞍山市",
        "淮北市",
        "铜陵市",
        "安庆市",
        "黄山市",
        "滁州市",
        "阜阳市",
        "宿州市",
        "六安市",
        "亳州市",
        "池州市",
        "宣城市",
        "福州市",
        "厦门市",
        "莆田市",
        "三明市",
        "泉州市",
        "漳州市",
        "南平市",
        "龙岩市",
        "宁德市",
        "南昌市",
        "景德镇市",
        "萍乡市",
        "九江市",
        "新余市",
        "鹰潭市",
        "赣州市",
        "吉安市",
        "宜春市",
        "抚州市",
        "上饶市",
        "济南市",
        "青岛市",
        "淄博市",
        "枣庄市",
        "东营市",
        "烟台市",
        "潍坊市",
        "济宁市",
        "泰安市",
        "威海市",
        "日照市",
        "莱芜市",
        "临沂市",
        "德州市",
        "聊城市",
        "滨州市",
        "菏泽市",
        "郑州市",
        "开封市",
        "洛阳市",
        "平顶山市",
        "安阳市",
        "鹤壁市",
        "新乡市",
        "焦作市",
        "濮阳市",
        "许昌市",
        "漯河市",
        "三门峡市",
        "南阳市",
        "商丘市",
        "信阳市",
        "周口市",
        "驻马店市",
        "省直辖县",
        "武汉市",
        "黄石市",
        "十堰市",
        "宜昌市",
        "襄阳市",
        "鄂州市",
        "荆门市",
        "孝感市",
        "荆州市",
        "黄冈市",
        "咸宁市",
        "随州市",
        "恩施土家族苗族自治州",
        "省直辖县",
        "长沙市",
        "株洲市",
        "湘潭市",
        "衡阳市",
        "邵阳市",
        "岳阳市",
        "常德市",
        "张家界市",
        "益阳市",
        "郴州市",
        "永州市",
        "怀化市",
        "娄底市",
        "湘西土家族苗族自治州",
        "广州市",
        "韶关市",
        "深圳市",
        "珠海市",
        "汕头市",
        "佛山市",
        "江门市",
        "湛江市",
        "茂名市",
        "肇庆市",
        "惠州市",
        "梅州市",
        "汕尾市",
        "河源市",
        "阳江市",
        "清远市",
        "东莞市",
        "中山市",
        "潮州市",
        "揭阳市",
        "云浮市",
        "南宁市",
        "柳州市",
        "桂林市",
        "梧州市",
        "北海市",
        "防城港市",
        "钦州市",
        "贵港市",
        "玉林市",
        "百色市",
        "贺州市",
        "河池市",
        "来宾市",
        "崇左市",
        "海口市",
        "三亚市",
        "三沙市",
        "儋州市",
        "省直辖县",
        "重庆市",
        "成都市",
        "自贡市",
        "攀枝花市",
        "泸州市",
        "德阳市",
        "绵阳市",
        "广元市",
        "遂宁市",
        "内江市",
        "乐山市",
        "南充市",
        "眉山市",
        "宜宾市",
        "广安市",
        "达州市",
        "雅安市",
        "巴中市",
        "资阳市",
        "阿坝藏族羌族自治州",
        "甘孜藏族自治州",
        "凉山彝族自治州",
        "贵阳市",
        "六盘水市",
        "遵义市",
        "安顺市",
        "毕节市",
        "铜仁市",
        "黔西南布依族苗族自治州",
        "黔东南苗族侗族自治州",
        "黔南布依族苗族自治州",
        "昆明市",
        "曲靖市",
        "玉溪市",
        "保山市",
        "昭通市",
        "丽江市",
        "普洱市",
        "临沧市",
        "楚雄彝族自治州",
        "红河哈尼族彝族自治州",
        "文山壮族苗族自治州",
        "西双版纳傣族自治州",
        "大理白族自治州",
        "德宏傣族景颇族自治州",
        "怒江傈僳族自治州",
        "迪庆藏族自治州",
        "拉萨市",
        "日喀则市",
        "昌都市",
        "林芝市",
        "山南市",
        "那曲市",
        "阿里地区",
        "西安市",
        "铜川市",
        "宝鸡市",
        "咸阳市",
        "渭南市",
        "延安市",
        "汉中市",
        "榆林市",
        "安康市",
        "商洛市",
        "兰州市",
        "嘉峪关市",
        "金昌市",
        "白银市",
        "天水市",
        "武威市",
        "张掖市",
        "平凉市",
        "酒泉市",
        "庆阳市",
        "定西市",
        "陇南市",
        "临夏回族自治州",
        "甘南藏族自治州",
        "西宁市",
        "海东市",
        "海北藏族自治州",
        "黄南藏族自治州",
        "海南藏族自治州",
        "果洛藏族自治州",
        "玉树藏族自治州",
        "海西蒙古族藏族自治州",
        "银川市",
        "石嘴山市",
        "吴忠市",
        "固原市",
        "中卫市",
        "乌鲁木齐市",
        "克拉玛依市",
        "吐鲁番市",
        "哈密市",
        "昌吉回族自治州",
        "博尔塔拉蒙古自治州",
        "巴音郭楞蒙古自治州",
        "阿克苏地区",
        "克孜勒苏柯尔克孜自治州",
        "喀什地区",
        "和田地区",
        "伊犁哈萨克自治州",
        "塔城地区",
        "阿勒泰地区",
        "自治区直辖县级行政区划",
        "台北市",
        "高雄市",
        "台南市",
        "台中市",
        "金门县",
        "南投县",
        "基隆市",
        "新竹市",
        "嘉义市",
        "新北市",
        "宜兰县",
        "新竹县",
        "桃园县",
        "苗栗县",
        "彰化县",
        "嘉义县",
        "云林县",
        "屏东县",
        "台东县",
        "花莲县",
        "澎湖县",
        "连江县",
        "香港岛",
        "九龙",
        "新界",
        "澳门半岛",
        "离岛"];