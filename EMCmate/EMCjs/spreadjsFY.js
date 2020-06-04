$(function () {
    //var url = location.search;//获取url中"?"符后的字串
    //if (url.indexOf('?') != -1) {//判断是否有参数
    //    var str = url.substring(1);//从第一个字符串开始获取、因为第0个是？号
    //    var strs = str.split('&');//将字符串分割为数组
    //    var brray = new Array();
    //    for (var i = 0; i < strs.length; i++) {
    //        brray[i] = strs[i].split('=');
    //    }
    //    time = brray[0][1];
    //    area = brray[1][1];
    //    if (area == 'FY') {
    alert('22')
    Spread();
    //}
    //else {
    //    SpreadLH();
    //}
    //}
})
var time = '2020-05-27', area = 'FY';

//福永
function Spread() {
    var spread = new GC.Spread.Sheets.Workbook(document.getElementById('ss'), { sheetCount: 1 });
    var sheet = spread.getSheet(0);
    //var person = { one: '场地1', two: '场地2', three: '场地3', four: '场地4' };
    //var source = new GC.Spread.Sheets.Bindings.CellBindingSource(person);
    //sheet.setBindingPath(0, 2, 'one');
    //sheet.setBindingPath(0, 3, 'two');
    //sheet.setBindingPath(0, 4, 'three');
    //sheet.setBindingPath(0, 5, 'four');
    //sheet.setDataSource(source);
    //sheet.getRange(1, 1, 1, 2).backColor("rgb(20, 140, 1218)")
    sheet.getCell(0, 2).text("FY-谐波电流和电压闪烁场地");
    sheet.getCell(0, 3).text("FY-传导，功率骚扰、灯具磁场辐射场地");
    sheet.getCell(0, 4).text("FY-3米暗室");
    sheet.getCell(0, 5).text("FY-雷击，快速脉冲群、电压跌落场地");
    sheet.getCell(0, 6).text("FY-传导抗扰度场地");
    sheet.getCell(0, 7).text("FY-静电场地");
    sheet.getCell(0, 8).text("FY-工频磁场场地");
    sheet.getCell(0, 9).text("FY-灯具EMF场地");
    sheet.getCell(0, 10).text("FY-RF实验室");
    sheet.getCell(0, 11).text("FY-家电EMF场地");
    //上午、下午、晚上跨行
    sheet.addSpan(1, 0, 7, 1);
    sheet.addSpan(8, 0, 9, 1);
    sheet.addSpan(17, 0, 11, 1);
    sheet.getCell(1, 0).text('上午');
    sheet.getCell(8, 0).text('下午');
    sheet.getCell(17, 0).text('晚上');
    //时间段
    sheet.getCell(1, 1).text('8:30-9:00');
    sheet.getCell(2, 1).text('9:00-9:30');
    sheet.getCell(3, 1).text('9:30-10:00');
    sheet.getCell(4, 1).text('10:00-10:30');
    sheet.getCell(5, 1).text('10:30-11:00');
    sheet.getCell(6, 1).text('11:00-11:30');
    sheet.getCell(7, 1).text('11:30-12:00');
    sheet.getCell(8, 1).text('13:30-14:00');
    sheet.getCell(9, 1).text('14:00-14:30');
    sheet.getCell(10, 1).text('14:30-15:00');
    sheet.getCell(11, 1).text('15:00-15:30');
    sheet.getCell(12, 1).text('15:30-16:00');
    sheet.getCell(13, 1).text('16:00-16:30');
    sheet.getCell(14, 1).text('16:30-17:00');
    sheet.getCell(15, 1).text('17:00-17:30');
    sheet.getCell(16, 1).text('17:30-18:00');
    sheet.getCell(17, 1).text('18:00-18:30');
    sheet.getCell(18, 1).text('18:30-19:00');
    sheet.getCell(19, 1).text('19:00-19:30');
    sheet.getCell(20, 1).text('19:30-20:00');
    sheet.getCell(21, 1).text('20:00-20:30');
    sheet.getCell(22, 1).text('20:30-21:00');
    sheet.getCell(23, 1).text('21:00-21:30');
    sheet.getCell(24, 1).text('21:30-22:00');
    sheet.getCell(25, 1).text('22:00-22:30');
    sheet.getCell(26, 1).text('22:30-23:00');
    sheet.getCell(27, 1).text('23:00-23:30');
    sheet.getCell(28, 1).text('23:30-23:59');
    //列宽
    sheet.setColumnWidth(0, 80);
    for (var i = 1; i <= 22; i++) {
        sheet.setColumnWidth(i, 120);
    }
    //行高
    sheet.setRowHeight(0, 40);
    for (var i = 1; i <= 28; i++) {
        sheet.setRowHeight(i, 30);
    }
    //居中
    sheet.getCell(1, 0).vAlign(GC.Spread.Sheets.VerticalAlign.center);
    sheet.getCell(8, 0).vAlign(GC.Spread.Sheets.VerticalAlign.center);
    sheet.getCell(17, 0).vAlign(GC.Spread.Sheets.VerticalAlign.center);
    //自动换行
    for (var i = 2; i < 12; i++) {
        sheet.getCell(0, i).wordWrap(true);
    }

    //动态插入json数据
    //BindData(sheet);
}
function BindData(sheet) {
    $.ajax({
        url: 'select.ashx?parameter=' + area + '&time=' + time + '',
        type: 'post',
        dataType: 'text',
        data: { time: time },
        success: function (data) {
            //表格填充数据
            //console.log(data);
            var jsonobj = eval(data);
            for (var p in jsonobj) {
                var site = jsonobj[p].Testitem;
                var cell;
                var cell1 = sheet.getCell(0, 2).text();
                var cell2 = sheet.getCell(0, 3).text();
                var cell3 = sheet.getCell(0, 4).text();
                var cell4 = sheet.getCell(0, 5).text();
                var cell5 = sheet.getCell(0, 6).text();
                var cell6 = sheet.getCell(0, 7).text();
                var cell7 = sheet.getCell(0, 8).text();
                var cell8 = sheet.getCell(0, 9).text();
                var cell9 = sheet.getCell(0, 10).text();
                var cell10 = sheet.getCell(0, 11).text();

                //取列
                if (site == cell1) {
                    cell = 2;
                }
                else if (site == cell2) {
                    cell = 3;
                }
                else if (site == cell3) {
                    cell = 4;
                }
                else if (site == cell4) {
                    cell = 5;
                }
                else if (site == cell5) {
                    cell = 6;
                }
                else if (site == cell6) {
                    cell = 7;
                }
                else if (site == cell7) {
                    cell = 8;
                }
                else if (site == cell8) {
                    cell = 9;
                }
                else if (site == cell9) {
                    cell = 10;
                }
                else {
                    cell = 11;
                }
                //取行
                var start = jsonobj[p].Starttime;
                var stop = jsonobj[p].Stoptime;
                var timestart = start.substring(10, 16);
                var timestop = stop.substring(10, 16);
                var rows = GetRows(timestart, timestop);
                for (var i = 0; i < rows.length; i++) {
                    var rowindex = rows[i];
                    sheet.getCell(rowindex, cell).text(jsonobj[p].Responser);
                }
            }
        }
    });
}

//龙华
function SpreadLH() {
    var spread = new GC.Spread.Sheets.Workbook(document.getElementById('ss'), { sheetCount: 1 });
    var sheet = spread.getSheet(0);
    sheet.getCell(0, 2).text("LH-谐波电流和电压闪烁场地");
    sheet.getCell(0, 3).text("LH-传导和功率骚扰场地");
    sheet.getCell(0, 4).text("LH-3米暗室");
    sheet.getCell(0, 5).text("LH-快速脉冲群场地");
    sheet.getCell(0, 6).text("LH-雷击（电源端）场地");
    sheet.getCell(0, 7).text("LH-雷击（通讯端）场地");
    sheet.getCell(0, 8).text("LH-电压跌落场地");
    sheet.getCell(0, 9).text("LH-3米暗室");
    sheet.getCell(0, 10).text("LH-汽车电子大电流注入场地");
    sheet.getCell(0, 11).text("LH-静电场地");
    sheet.getCell(0, 12).text("LH-汽车电子ISO7637场地");
    sheet.getCell(0, 13).text("LH-汽车电子ISO16750-2场地");
    //上午、下午、晚上跨行
    sheet.addSpan(1, 0, 7, 1);
    sheet.addSpan(8, 0, 9, 1);
    sheet.addSpan(17, 0, 11, 1);
    sheet.getCell(1, 0).text('上午');
    sheet.getCell(8, 0).text('下午');
    sheet.getCell(17, 0).text('晚上');
    //时间段
    sheet.getCell(1, 1).text('8:30-9:00');
    sheet.getCell(2, 1).text('9:00-9:30');
    sheet.getCell(3, 1).text('9:30-10:00');
    sheet.getCell(4, 1).text('10:00-10:30');
    sheet.getCell(5, 1).text('10:30-11:00');
    sheet.getCell(6, 1).text('11:00-11:30');
    sheet.getCell(7, 1).text('11:30-12:00');
    sheet.getCell(8, 1).text('13:30-14:00');
    sheet.getCell(9, 1).text('14:00-14:30');
    sheet.getCell(10, 1).text('14:30-15:00');
    sheet.getCell(11, 1).text('15:00-15:30');
    sheet.getCell(12, 1).text('15:30-16:00');
    sheet.getCell(13, 1).text('16:00-16:30');
    sheet.getCell(14, 1).text('16:30-17:00');
    sheet.getCell(15, 1).text('17:00-17:30');
    sheet.getCell(16, 1).text('17:30-18:00');
    sheet.getCell(17, 1).text('18:00-18:30');
    sheet.getCell(18, 1).text('18:30-19:00');
    sheet.getCell(19, 1).text('19:00-19:30');
    sheet.getCell(20, 1).text('19:30-20:00');
    sheet.getCell(21, 1).text('20:00-20:30');
    sheet.getCell(22, 1).text('20:30-21:00');
    sheet.getCell(23, 1).text('21:00-21:30');
    sheet.getCell(24, 1).text('21:30-22:00');
    sheet.getCell(25, 1).text('22:00-22:30');
    sheet.getCell(26, 1).text('22:30-23:00');
    sheet.getCell(27, 1).text('23:00-23:30');
    sheet.getCell(28, 1).text('23:30-23:59');
    //列宽
    sheet.setColumnWidth(0, 80);
    sheet.setColumnWidth(12, 190);
    sheet.setColumnWidth(13, 190);
    for (var i = 1; i <= 11; i++) {
        sheet.setColumnWidth(i, 120);
    }
    //行高
    sheet.setRowHeight(0, 40);
    for (var i = 1; i <= 28; i++) {
        sheet.setRowHeight(i, 30);
    }
    //居中
    sheet.getCell(1, 0).vAlign(GC.Spread.Sheets.VerticalAlign.center);
    sheet.getCell(8, 0).vAlign(GC.Spread.Sheets.VerticalAlign.center);
    sheet.getCell(17, 0).vAlign(GC.Spread.Sheets.VerticalAlign.center);
    //自动换行
    for (var i = 2; i < 12; i++) {
        sheet.getCell(0, i).wordWrap(true);
    }

    //动态插入json数据
    BindDataLH(sheet);
}
function BindDataLH(sheet) {
    $.ajax({
        url: 'select.ashx?parameter=' + area + '',
        type: 'post',
        dataType: 'text',
        data: { time: time },
        success: function (data) {
            //表格填充数据
            //console.log(data);
            var jsonobj = eval(data);
            for (var p in jsonobj) {
                var site = jsonobj[p].Testitem;
                var cell;
                var cell1 = sheet.getCell(0, 2).text();
                var cell2 = sheet.getCell(0, 3).text();
                var cell3 = sheet.getCell(0, 4).text();
                var cell4 = sheet.getCell(0, 5).text();
                var cell5 = sheet.getCell(0, 6).text();
                var cell6 = sheet.getCell(0, 7).text();
                var cell7 = sheet.getCell(0, 8).text();
                var cell8 = sheet.getCell(0, 9).text();
                var cell9 = sheet.getCell(0, 10).text();
                var cell10 = sheet.getCell(0, 11).text();
                var cell11 = sheet.getCell(0, 12).text();
                var cell12 = sheet.getCell(0, 13).text();

                //取列
                if (site == cell1) {
                    cell = 2;
                }
                else if (site == cell2) {
                    cell = 3;
                }
                else if (site == cell3) {
                    cell = 4;
                }
                else if (site == cell4) {
                    cell = 5;
                }
                else if (site == cell5) {
                    cell = 6;
                }
                else if (site == cell6) {
                    cell = 7;
                }
                else if (site == cell7) {
                    cell = 8;
                }
                else if (site == cell8) {
                    cell = 9;
                }
                else if (site == cell9) {
                    cell = 10;
                }
                else if (site == cell10) {
                    cell = 11;
                }
                else if (site == cell11) {
                    cell = 12;
                }
                else {
                    cell = 13;
                }
                //取行
                var start = jsonobj[p].Starttime;
                var stop = jsonobj[p].Stoptime;
                var timestart = start.substring(10, 16);
                var timestop = stop.substring(10, 16);
                var rows = GetRows(timestart, timestop);
                for (var i = 0; i < rows.length; i++) {
                    var rowindex = rows[i];
                    sheet.getCell(rowindex, cell).text(jsonobj[p].Responser);
                }
            }
        }
    });
}

//通过时间段获取行号
function GetRows(time0, time1) {
    var _rowstime = [{ rowid: 1, text: "@8:30-9:00" }
        , { rowid: 2, text: "@9:00-9:30" }
        , { rowid: 3, text: "@9:30-10:00" }
        , { rowid: 4, text: "@10:00-10:30" }
        , { rowid: 5, text: "@10:30-11:00" }
        , { rowid: 6, text: "@11:00-11:30" }
        , { rowid: 7, text: "@11:30-12:00" }
        , { rowid: 8, text: "@13:30-14:00" }
        , { rowid: 9, text: "@14:00-14:30" }
        , { rowid: 10, text: "@14:30-15:00" }
        , { rowid: 11, text: "@15:00-15:30" }
        , { rowid: 12, text: "@15:30-16:00" }
        , { rowid: 13, text: "@16:00-16:30" }
        , { rowid: 14, text: "@16:30-17:00" }
        , { rowid: 15, text: "@17:00-17:30" }
        , { rowid: 16, text: "@17:30-18:00" }
        , { rowid: 17, text: "@18:00-18:30" }
        , { rowid: 18, text: "@18:30-19:00" }
        , { rowid: 19, text: "@19:00-19:30" }
        , { rowid: 20, text: "@19:30-20:00" }
        , { rowid: 21, text: "@20:00-20:30" }
        , { rowid: 22, text: "@20:30-21:00" }
        , { rowid: 23, text: "@21:00-21:30" }
        , { rowid: 24, text: "@21:30-22:00" }
        , { rowid: 25, text: "@22:00-22:30" }
        , { rowid: 26, text: "@22:30-23:00" }
        , { rowid: 27, text: "@23:00-23:30" }
        , { rowid: 28, text: "@23:30-23:59" }];
    var rows = [];
    var times = time(time0, time1);
    var _bb = (parseInt(times[0].split(":")[0]) == 8 && parseInt(times[0].split(":")[1]) < 30 && parseInt(times[1].split(":")[0]) == 8 && parseInt(times[1].split(":")[1]) < 30) || (parseInt(times[0].split(":")[0]) < 8 && parseInt(times[1].split(":")[0]) < 8);
    if (_bb) {
        return [];
    } else if ((parseInt(times[0].split(":")[0]) == 8 && parseInt(times[0].split(":")[1]) < 30) || parseInt(times[0].split(":")[0]) < 8) {
        times[0] = "8:30";
    }
    var _b = false;
    $.each(_rowstime, function (index, item) {
        if ((item.text).indexOf("@" + times[0] + "-") != -1) {
            _b = true;
        }
        if (_b) {
            rows.push(item.rowid);
        }
        if ((item.text).indexOf("-" + times[1]) != -1) {
            _b = false;
            return false;
        }
    });
    return rows;

    function time(time, time1) {
        if (time1 != undefined) {
            var times = time.split(":");
            var time1s = time1.split(":");
            if (parseInt(times[0]) > parseInt(time1s[0])) {
                var _t = time;
                time = time1;
                time1 = _t;
            } else if (times[0] == time1s[0] && parseInt(times[1]) > parseInt(time1s[1])) {
                var _t = time;
                time = time1;
                time1 = _t;
            }
            return [_time(time, 1)[0], _time(time1, 2)[1]];
        }
        else {
            return _time(time, 1);
        }

        function _time(_time, index) {
            var times = _time.split(":");
            var hour = parseInt(times[0]);
            var minute = parseInt(times[1]);
            var begintime_hour = hour;
            var begintime_minute = minute;
            var endtime_hour = hour;
            var endtime_minute = minute;
            if (parseInt(minute) >= 30) {
                begintime_minute = 30;
                endtime_minute = endtime_hour == 23 ? "59" : "00";
                if (minute == 30 && index == 2) {
                    begintime_minute = "00";
                    endtime_minute = 30;
                } else {
                    endtime_hour++;
                }
            } else {
                begintime_minute = "00";
                endtime_minute = parseInt(minute) > 0 ? 30 : "00";
                if (minute == 0 && index == 2) {
                    begintime_hour--;
                    begintime_minute = 30;
                    endtime_minute = "00";
                }
            }
            return [begintime_hour + ":" + begintime_minute, endtime_hour + ":" + endtime_minute];
        }
    }
}