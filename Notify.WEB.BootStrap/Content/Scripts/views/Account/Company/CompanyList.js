// 打开窗口
function openAddCompany() {
    cleraAddUser();
    window.layer.open({
        type: 1,
        shade: null,//[0.1, "#000"],
        area: ["750", "auto"],
        title: ["添加公司", ""],
        border: [1],
        closeBtn: 1,
        content: $("#addCompany")
    });
    return false;
}


// 关闭
function closeEdit() {
    window.layer.closeAll();
}

// 清除编辑信息
function cleraAddUser() {
    $("#txtCompanyAccountNo").val("");
    $("#txtCompanyName").val("");
}

// 验证
function validateRole(user) {
    if (user.CompanyAccountNo === "") {
        $.layerAlert("请填写帐号", { icon: 2 });
        return false;
    }
    if (user.CompanyName === "") {
        $.layerAlert("请填写用户名", { icon: 2 });
        return false;
    }
    return true;
}

//保存
function saveAddCompany() {
    var parameter = new Object();
    parameter.CompanyAccountNo = $("#txtCompanyAccountNo").val();
    parameter.CompanyName = $("#txtCompanyName").val();
    if (validateRole(parameter)) {
        $.ajaxExtend({
            data: JSON.stringify(parameter),
            url: "/Company/AddCompany",
            success: function (d) {
                if (d.IsSucceed) {
                    $("#btnSubmit").click();
                    $.layerAlert(d.Message, { icon: 2 });
                } else {
                    $.layerAlert(d.Message, { icon: 2 });
                }
            }
        });
    }
}