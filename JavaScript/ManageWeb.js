/// <reference path="Jquery.doc.js"/>
window.onerror = function() { return true; }
(function() {
var x = 10;
var y = 10;
    // 左侧菜单效果
    $('.Menu').click(function() {
        var $NowMenu = $(this).next('.MenuNote');
        $('.MenuNote').hide(400, function() { });
        if ($NowMenu.is(":visible")) {
            $NowMenu.hide(300, function() { });
        } else {
            $NowMenu.show(500, function() { });
        }
    });

    $("tbody>tr:even").css("background", "#F0F0E8");
    $("tbody>tr").mouseover(function() {
        $(this).css("background", "#DAE9FB");
    });
    $("tbody>tr").mouseout(function() {
        $("tbody>tr:even").css("background", "#F0F0E8");
        $("tbody>tr:odd").css("background", "");
    });

    $("#BtnAllSelect").click(function() {
        if ($("#BtnAllSelect").val() == "全选") {
            $("#BtnAllSelect").val("反选");
            $("[name=CheckMes]:checkbox").attr("checked", true);
        }
        else {
            $("#BtnAllSelect").val("全选");
            $("[name=CheckMes]:checkbox").each(function() {
                this.checked = !this.checked;
            })
        }
    });
    $("#BtnAllDel").click(function() {
        if (confirm("你确定删除所有选中的信息？")) {
            var checkValue = "";
            $("[name=CheckMes]:checkbox:checked").each(function() {
                if ($.trim($(this).val()).length > 0) {
                    checkValue += "," + $.trim($(this).val());
                }
            })
            if (checkValue.length == 0) {
                alert("你没有选择任何信息，请先选中要删除的信息！");
                return false;
            }
            else {
                $("#HSelectID").val(checkValue.substr(1));
            }
            return true;
        }
        else {
            return false;
        }
    });

    $("#BtnSearch").click(function() {
        $("#Province").val($("#DDLProvince").find("option:selected").text());
        $("#City").val($("#DDLCity").find("option:selected").text());
    })

    $("#BtnSaveUserInfo").click(function() {
        if ($.trim($("#txtPerson").val()).length == 0) {
            alert("(┬＿┬) 说过了，联系人名称不能为空！");
            return false;
        }
        else if ($.trim($("#txtCompanyName").val()).length == 0) {
            alert("(┬＿┬) 说过了，公司名称不能为空！");
            $("#txtCompanyName").focus();
            return false;
        }

        else if ($.trim($("#txtTel").val()).length == 0) {
            alert("(┬＿┬) 说过了，联系电话不能为空！");
            $("#txtTel").focus();
            return false;
        }
        else {
            $("#Province").val($("#DDLProvince").find("option:selected").text());
            $("#City").val($("#DDLCity").find("option:selected").text());
        }
        return true;
    })
    $("#BtnUpdate").click(function() {
        if ($("#DDlService").val() == "-1") {
            alert("没有选定任何更新的数据列！");
            $("#DDlState").focus();
            return false;
        }
        else {
            $("#Province").val($("#DDLProvince").find("option:selected").text());
            $("#City").val($("#DDLCity").find("option:selected").text());
            var Warning = "您将要对选中的客户进行以下操作：\n\n";
            //            if ($("#DDlState").val() != "-1") {
            //                Warning += "❤ 更新用户状态为：【" + $("#DDlState").find("option:selected").text() + "】\n\n";
            //            }
            //            if ($("#DDlClass").val() != "-1") {
            //                Warning += "❤ 更新用户类别为：【" + $("#DDlClass").find("option:selected").text() + "】\n\n";
            //            }
            //            if ($("#DDLLeave").val() != "-1") {
            //                Warning += "❤ 更新用户等级为：【" + $("#DDLLeave").find("option:selected").text() + "】\n\n";
            //            }
            if ($("#DDlService").val() != "-1") {
                Warning += "❤ 更新用户归属给：【" + $("#DDlService").find("option:selected").text() + "】\n\n";
            }
            if (confirm(Warning)) {
                var checkValue = "";
                $("[name=CheckMes]:checkbox:checked").each(function() {
                    if ($.trim($(this).val()).length > 0) {
                        checkValue += "," + $.trim($(this).val());
                    }
                })
                if (checkValue.length == 0) {
                    alert("你没有选择任何信息，请先选中要删除的信息！");
                    return false;
                }
                else {
                    $("#HSelectID").val(checkValue.substr(1));
                }
                return true;
            }
            else {
                return false;
            }
        }
    })
    $(".GetMes").click(function() {
        if ($.trim($(this).attr("id")).length > 0) {
            ymPrompt.win({ message: "GetMoreMes.aspx?UID=" + $(this).attr("id"), width: 710, height: 560, title: '客户详细信息', handler: null, maxBtn: false, minBtn: false, iframe: true });
        }
    })

    $("#RpPowerUser_ctl01_IBDel").click(function() {
        if (confirm("删除后该角色下的所有用户将无法使用！你确定要删除该角色信息吗？！")) {
            return true;
        }
        return false;
    })

    $("#BtnSaveUserMes").click(function() {
        if ($.trim($("#txtPerson").val()).length == 0) {
            alert("(┬＿┬) 说过了，用户名称不能为空！");
            return false;
        }
        else if ($.trim($("#txtPassWord").val()).length == 0) {
            alert("(┬＿┬) 说过了，用户密码不能为空！");
            $("#txtPassWord").focus();
            return false;
        }
        else if ($.trim($("#txtCheckPass").val()).length == 0) {
            alert("(┬＿┬) 说过了，验证密码不能为空！");
            $("#txtCheckPass").focus();
            return false;
        }
        else if ($("#txtPassWord").val() != $("#txtCheckPass").val()) {
            alert("(┬＿┬) 验证密码和原密码不符！");
            $("#txtCheckPass").focus();
            return false;
        }
        else if ($.trim($("#txtMoblie").val()).length == 0) {
            alert("(┬＿┬) 联系电话不能为空，什么电话都行！");
            $("#txtMoblie").focus();
            return false;
        }
        else {
            return true;
        }
    })

    $("#RpServiceUser_ctl01_IBDelService").click(function() {
        if (confirm("删除该人员，该人员相关的所有记录信息都将被删除！确定吗？")) {
            return true;
        }
        else {
            return false;
        }
    })

    $("#txtPerson").blur(function() {
        if ($.trim($("#txtPerson").val()).length == 0) {
            alert("(┬＿┬) 说过了，用户名称不能为空！");
        }
        else {
            $.get("../AJAX.ASPX", { AjaxClass: "CheckName", Name: $("#txtPerson").val() }, ReturnValue);
        }
    })

    $("#UserWord>input").click(function() {
        if ($(this).attr("checked")) {
            $("#txtNowWork").val($("#txtNowWork").val() + $(this).val() + ";\n");
        }
        else {
            $("#txtNowWork").val($("#txtNowWork").val().replace($(this).val() + ";\n", ""));
        }
    })
    $("#BtnSaveWorkInfo").click(function() {
        if ($.trim($("#txtNowWork").val()).length == 0) {
            alert("请认真书写工作记录，经理要检查的o(︶︿︶)o 唉...");
            $("#txtNowWork").focus();
            return false;
        }
        else if (confirm("你确定保存当前工作记录信息吗？")) {
            return true;
        }
        else {
            return false;
        }
    })

    $("#BtnUpdateUserPass").click(function() {
        if ($.trim($("#txtOldPass").val()).length == 0) {
            alert("原始密码不能为空...");
            $("#txtOldPass").focus();
            return false;
        }
        else if ($.trim($("#txtPassWord").val()).length == 0) {
            alert("新密码不能为空...");
            $("#txtPassWord").focus();
            return false;
        }
        else if ($.trim($("#txtCheckPass").val()).length == 0) {
            alert("确认密码不能为空...");
            $("#txtCheckPass").focus();
            return false;
        }
        else if ($.trim($("#txtCheckPass").val()) != $.trim($("#txtPassWord").val())) {
            alert("确认密码和新密码不一致...");
            $("#txtCheckPass").focus();
            return false;
        }
    })

    $("#CloseMenu").click(function() {
        if (window.parent.document.getElementById("Leftframe").cols == "184,*") {
            window.parent.document.getElementById("Leftframe").cols = "0,*";
        }
    })
    $("#OpenMenu").click(function() {
        if (window.parent.document.getElementById("Leftframe").cols == "0,*") {
            window.parent.document.getElementById("Leftframe").cols = "184,*";
        }
    })
    $(".BakDATA").click(function() {
        document.getElementById("overlay").style.display = "block";
        window.parent.document.getElementById("Leftframe").cols = "0,*";
    })
    
    $("tr.TIP").mouseover(function(e) {
        this.myTitle = this.title;
        this.title = "";
        var toolTip = "<div id='tooltip'>" + this.myTitle + "</div>";
        $("body").append(toolTip);
        $("#tooltip").css({
            "position": "absolute",
            "padding": "5px",
            "background": "#F0F0E8",
            "border": "1px gray solid",
            "top": (e.pageY + y) + "px",
            "left": (e.pageX + x) + "px"
        }).show(200);
    }).mouseout(function() {
        this.title = this.myTitle;
        $("#tooltip").remove();
    }).mousemove(function(e) {
        $("#tooltip").css({
            "background": "#F0F0E8",
            "padding": "5px",
            "border": "1px gray solid",
            "top": (e.pageY + y) + "px",
            "left": (e.pageX + x) + "px"
        })
    })
})
function CloaseOver() {
    document.getElementById("overlay").style.display = "none";
    window.parent.document.getElementById("Leftframe").cols = "184,*";  
}
function ReturnValue(data) {
    if (data == "1") {
        alert("该登录名称已经存在了,青重新输入其它登录名...");
        $("#txtPerson").focus();
        $("#txtPerson").select();
    }
}
function GetNowDate() {
    var nowDate = new Date();
    var ReDate = nowDate.getFullYear() + "-" + (parseInt(nowDate.getMonth()) + 1) + "-" + nowDate.getDate() + " " + nowDate.getHours() + ":" + nowDate.getMinutes() + ":" + nowDate.getSeconds();
    return ReDate;
}

