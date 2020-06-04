$(function () {
    var url = location.search;//获取url中"?"符后的字串
    if (url.indexOf('?') != -1) {//判断是否有参数
        var str = url.substring(1);//从第一个字符串开始获取、因为第0个是？号
        var strs = str.split('&');//将字符串分割为数组
        var brray = new Array();
        for (var i = 0; i < strs.length; i++) {
            brray[i] = strs[i].split('=');
        }
        time = brray[0][1];
        area = brray[1][1];
        if (area == 'FY') {
            $('#titlesite').text(time + '福永场地');
            BindDataFY();
        }
        else {
            $('#titlesite').text(time + '龙华场地');
            BindDataLH();
        }
    }
});
var time, area;

//福永
function BindDataFY() {
    $.ajax({
        url: 'select.ashx?parameter=' + area + '&time=' + time + '',
        type: 'post',
        dataType: 'text',
        success: function (data) {
            var jsonobj = eval(data);//解析json数据
            var col;
            for (var p in jsonobj) {
                //获取列
                var site = jsonobj[p].Testitem;
                if (site == 'FY-谐波电流和电压闪烁场地') {
                    col = 2;
                }
                else if (site == 'FY-传导，功率骚扰，灯具磁场辐射场地') {
                    col = 3;
                }
                else if (site == 'FY-3米暗室') {
                    col = 4;
                }
                else if (site == 'FY-雷击，快速脉冲群，电压跌落场地') {
                    col = 5;
                }
                else if (site == 'FY-传导抗扰度场地') {
                    col = 6;
                }
                else if (site == 'FY-静电场地') {
                    col = 7;
                }
                else if (site == 'FY-工频磁场场地') {
                    col = 8;
                }
                else if (site == 'FY-灯具EMF场地') {
                    col = 9;
                }
                else if (site == 'FY-RF实验室') {
                    col = 10;
                }
                else if (site == 'FY-家电EMF场地') {
                    col = 11;
                }

                //取行
                var start = jsonobj[p].Starttime;
                var stop = jsonobj[p].Stoptime;
                var timestart = start.substring(10, 16);
                var timestop = stop.substring(10, 16);
                var rows = GetRows(timestart, timestop);
                if (rows.length == 0) {
                    continue;
                }
                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    var cell = 'cell' + row + '-' + col;
                    $('#' + cell + '').text(jsonobj[p].Responser);
                }
            }
        }
    });
}

//龙华
function BindDataLH() {
    $.ajax({
        url: 'select.ashx?parameter=' + area + '&time=' + time + '',
        type: 'post',
        dataType: 'text',
        success: function (data) {
            var jsonobj = eval(data);//解析json数据
            var col;
            for (var p in jsonobj) {
                //获取列
                var site = jsonobj[p].Testitem;
                console.log(site);
                if (site == 'LH-谐波电流和电压闪烁场地') {
                    col = 2;
                }
                else if (site == 'LH-传导和功率骚扰场地') {
                    col = 3;
                }
                else if (site == 'LH-3米暗室') {
                    col = 4;
                }
                else if (site == 'LH-快速脉冲群场地') {
                    col = 5;
                }
                else if (site == 'LH-雷击（电源端）场地') {
                    col = 6;
                }
                else if (site == 'LH-雷击（通讯端）场地') {
                    col = 7;
                }
                else if (site == 'LH-电压跌落场地') {
                    col = 8;
                }
                else if (site == 'LH-汽车电子大电流注入场地') {
                    col = 9;
                }
                else if (site == 'LH-静电场地') {
                    col = 10;
                }
                else if (site == 'LH-汽车电子ISO7637场地') {
                    col = 11;
                }
                else if (site == 'LH-汽车电子ISO16750-2场地') {
                    col = 12;
                }

                //取行
                var start = jsonobj[p].Starttime;
                var stop = jsonobj[p].Stoptime;
                var timestart = start.substring(10, 16);
                var timestop = stop.substring(10, 16);
                var rows = GetRows(timestart, timestop);
                if (rows.length == 0) {
                    continue;
                }
                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    var cell = 'LHcell' + row + '-' + col;
                    console.log(cell);
                    $('#' + cell + '').text(jsonobj[p].Responser);
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
    var times = time(time0, time1);//time();用于生成两个时间所在的区间
    //过滤一些时间段以外的时间
    var _bb = (parseInt(times[0].split(":")[0]) == 8 && parseInt(times[0].split(":")[1]) < 30 && parseInt(times[1].split(":")[0]) == 8 && parseInt(times[1].split(":")[1]) < 30) || (parseInt(times[0].split(":")[0]) < 8 && parseInt(times[1].split(":")[0]) < 8);
    if (_bb || (parseInt(times[0].split(":")[0]) < 8 && parseInt(times[1].split(":")[0]) < 8) || (parseInt(times[0].split(":")[0]) == 8 && parseInt(times[1].split(":")[0]) == 8 && parseInt(times[0].split(":")[1]) < 30 && parseInt(times[1].split(":")[1]) < 30) || (parseInt(times[0].split(":")[0]) < 8 && parseInt(times[1].split(":")[0]) == 8 && parseInt(times[1].split(":")[1]) <= 30)) {
        return [];
    } else if ((parseInt(times[0].split(":")[0]) == 8 && parseInt(times[0].split(":")[1]) < 30) || parseInt(times[0].split(":")[0]) < 8) {
        times[0] = "8:30";
    }
    var _b = false;//定义变量，用于存储是否是属于自定义时间段内的时间，_b为ture时，说明已经属于自定义时间段内的时间，_b为false则不属于
    //从自定义的时间段中获取行号。
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
        //判断是传进来两个参数还是一个参数
        if (time1 != undefined) {
            //两个参数情况下
            var times = time.split(":");//分割字符串，获取到开始时间的小时、分钟的值，times[0]为小时，times[1]为分钟
            var time1s = time1.split(":");//分割字符串，获取到结束时间的小时、分钟的值，time1s[0]为小时，time1s[1]为分钟
            //判断传入两个时间的大小，将times赋值为小的时间，time1s赋值为大的时间。
            if (parseInt(times[0]) > parseInt(time1s[0])) {//判断小时位，如果times的小时位大于time1s的小时位，则更换他们的值
                var _t = time;
                time = time1;
                time1 = _t;
            } else if (times[0] == time1s[0] && parseInt(times[1]) > parseInt(time1s[1])) {//当小时位相等的时候，times的分钟位大于time1s的分钟位，则更换他们的值
                var _t = time;
                time = time1;
                time1 = _t;
            }
            //_time();用生成单个时间存在的时间段，如：9:20==>["9:00","9:30"]
            return [_time(time, 1)[0], _time(time1, 2)[1]];//返回一个数组，数组第一位是小时间存在时间段的开始时间，数组第二位是大时间存在时间段的结束时间
        }
        else {
            //一个参数情况下，直接返回这个时间存在的时间段
            return _time(time, 1);
        }
        //_time();用生成单个时间存在的时间段，如：9:20==>["9:00","9:30"]，参数说明：第一个参数，传入时间，第二个参数用于判断是传入时间段的开始时间（值：0），还是结束时间（值：1）
        function _time(_time, index) {
            var times = _time.split(":");//获取到传入时间的小时、分钟数组
            var hour = parseInt(times[0]);//获取到时间的小时位
            var minute = parseInt(times[1]);//获取到时间的分钟位
            var begintime_hour = hour;//用于存储传入时间的所属时间段开始时间小时位，默认为传入时间的小时位
            var begintime_minute = minute;//用于存储传入时间的所属时间段开始时间分钟位，默认为传入时间的分钟位
            var endtime_hour = hour; //用于存储传入时间的所属时间段结束时间小时位，默认为传入时间的小时位
            var endtime_minute = minute;//用于存储传入时间的所属时间段结束时间分钟位，默认为传入时间的分钟位
            if (parseInt(minute) >= 30) {
                //分钟位大于30，如：8:40
                begintime_minute = 30;//赋值所属时间段开始时间的分钟位
                endtime_minute = endtime_hour == 23 ? "59" : "00";//赋值所属时间段结束时间的分钟位，如果小时位为23时，分钟位默认赋值59
                if (minute == 30 && index == 2) {
                    //当分钟位等于30时，并且是传入时间段的结束时间（index=2），所属时间段开始时间的分钟位为0，结束时间的分钟位为30，例：9:00-10:30中，结束时间为10:30，那么对于结束时间生成的区间数据为["10:00","10:30"]
                    begintime_minute = "00";
                    endtime_minute = 30;
                } else {
                    //当分钟位大于30，并时间传入时间的开始时间时，将结束时间小时位+1
                    endtime_hour++;
                }
            } else {
                //分钟位小于30时，如9:20
                begintime_minute = "00";//开始时间分钟位赋值为0，如：9:00
                endtime_minute = parseInt(minute) > 0 ? 30 : "00";//当分钟位大于0小于30的时候，结束时间分钟位为30；分钟位等于0的时候，结束时间先赋值0，后面还有判断。
                if (minute == 0 && index == 2) {
                    //分钟位等于0，并且处于传入时间段的结束时间时
                    begintime_hour--;//开始时间-1，后面数组中的9:30中的9，例：10:00==>["9:30","10:00"]
                    begintime_minute = 30;//开始时间分钟位，设置成30，上一行中9:30中的30
                    endtime_minute = "00";//结束时间分钟位，设置成0，上面10:00中的0
                }
            }
            return [begintime_hour + ":" + begintime_minute, endtime_hour + ":" + endtime_minute];//组装开始时间与结束时间的数组并返回。
        }
    }
}