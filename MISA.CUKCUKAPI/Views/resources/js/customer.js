$(document).ready(function () {
    // lấy dữ liệu
    CustomerJS.loadData();
    /* -------------------- TOOLBAR Ở TRÊN BẢNG DỮ LIỆU-------------------------- */
    $(".toolbar #add").click(CustomerJS.openAddCustomerDialog);     // Nút Thêm 
    $(".toolbar #copy").click(CustomerJS.openCopyCustomerDialog);   // Nút Nhân bản
    $(".toolbar #edit").click(CustomerJS.openEditCustomerDialog);   // Nút Sửa
    $(".toolbar #delete").click(CustomerJS.deleteManyCustomers);    // Nút Xóa
    $(".toolbar #refresh").click(CustomerJS.refreshTableData);      // Nút Nạp
});

// function tạo dialog
// Hàm tạo một dialog mới
function createNewDialog(dialogDisplayNow, widthDialog, heightDialog, isModal, isResize, isEscapeClose, isBeforeClose) {
    dialogDisplayNow.dialog({
        width: widthDialog,
        height: heightDialog,
        modal: isModal,
        resizable: isResize,
        closeOnEscape: isEscapeClose,
        classes: {
            "ui-dialog-buttonpane": "no-border-button"
        },
        //close: function () {
        //    CustomerJS.closeDialog(dialogDisplayNow);
        //},
    });
    dialogDisplayNow.dialog("open");
    $(window).resize(function () {
        if (dialogDisplayNow) {
            dialogDisplayNow.dialog("option", "position", { my: "center", at: "center", of: window });
        }
    });
}
// Hàm tạo một Request AJAX
function ajaxRequest(requestUrl, requestType, requestDataType, requestData, successFunction) {
    $.ajax({
        url: requestUrl,
        type: requestType,
        dataType: requestDataType,
        data: requestData,
        error: function () {
            console.log(requestUrl + " error " + requestType);
        },
        success: successFunction
    });
}
/**
 * Đối tượng JS lưu trữ toàn bộ các field, function cho trang Customer:
 */
var CustomerJS = Object.create({
    uri: "/api/customers",
    rowRecentClicked: "",   // Biến xác định dòng dữ liệu được chọn trong bảng dữ liệu
    loadData: function () {
        serverObject.getAllCustomer(CustomerJS.callback);
    },
    callback: function (data) {
        if (!data) {
            alert("Đã có lỗi xảy ra!");
            return;
        }
        $("#table-body-customer").html("");
        $.each(data,function(index,row){
            var rowHtml = 
                `<tr rowID="${row.CID}" rowIndex="${index}">
                        <td>${row.CCode}</td>
                        <td>${row.CName}</td>
                        <td>${row.CCompany}</td>
                        <td>${row.CTaxCode}</td>
                        <td>${row.CAddress}</td>
                        <td>${row.CPhone}</td>
                        <td>${row.CEmail}</td>
                        <td>${row.CMemberNum}</td>
                        <td>${row.CMemberType}</td>
                        <td>${row.CDebit}</td>
                        <td>${row.CDescription}</td>
                        <td><input type="checkbox" checked="${row.Is5Food}" name="thành viên 5food"></td>
                        <td><input type="checkbox" checked="${row.IsFollow}" name="Ngừng theo dõi"></td>
                    </tr>`;
            $("#table-body-customer").append(rowHtml);
        });
    },
    refreshTableData: function () {
        CustomerJS.loadData();
        CustomerJS.rowRecentClicked = $(".rowSelected");
    },
    openAddCustomerDialog: function () {
        var dialogDisplayNow = $("#add-customer-dialog");
        // Tạo Dialog
        createNewDialog(dialogDisplayNow, 682, 350, true, false, true, true);
        dialogDisplayNow.dialog({ close: cancelDialog });
        CustomerJS.setBeforeClose(dialogDisplayNow, CustomerJS.addNewCustomer, cancelDialog);
        // Các nút bấm
        dialogDisplayNow.on("click", "a[name=add]", CustomerJS.addNewCustomer);
        dialogDisplayNow.on("click", "a[name=cancel]", cancelDialog);

        function cancelDialog() {
            $(".table-tbody-wrapper tr").eq(0).remove();
            //commonJS.setFirstRowSelected($(".table-tbody-wrapper table"));
            $("#add-customer-dialog").unbind("click");
            CustomerJS.closeDialog(dialogDisplayNow);
        }
        return false;
    },
    addNewCustomer: function () {
        var dialogDisplayNow = $("#add-customer-dialog").is(':visible') ? $("#add-customer-dialog") : $("#copy-customer-dialog");

        // Kiểm tra xác thực dữ liệu
        dialogDisplayNow.find(".warning-feild").trigger('blur');
        if ($(".warning-icon").length !== 0)
            return;

        // Lấy dữ liệu
        var formData = {
            'CCode': dialogDisplayNow.find("input[name=CustomerCode]").val(),
            'CName': dialogDisplayNow.find("input[name=CustomerName]").val(),
            'CPhone': dialogDisplayNow.find("input[name=PhoneNumber]").val(),
            'CCompany': dialogDisplayNow.find("input[name=CompanyName]").val(),
            'CTaxCode': dialogDisplayNow.find("input[name=TaxCode]").val(),
            'CEmail': dialogDisplayNow.find("input[name=Email]").val(),
            'CAddress': dialogDisplayNow.find("input[name=Address]").val(),
            'CDescription': dialogDisplayNow.find("textarea[name=Note]").val(),
            'CMemberNum': "",
            'CMemberType': "Khách hàng cá nhân",
            'CDebit': 0,
            'Is5Food': false,
            'IsFollow': false,
            //'CCompany': dialogDisplayNow.find("input[name=CustomerGroup]").val(),
            //'DateOfBirth': dialogDisplayNow.find("input[name=Date]").val(),
        };
        debugger;
        // Truy vấn
        ajaxRequest(CustomerJS.uri, "post", "json", formData, function (data) {
            CustomerJS.refreshTableData();
            CustomerJS.closeDialog(dialogDisplayNow);
            dialogDisplayNow.unbind("click");
        });
    },
    setBeforeClose: function (dialogDisplayNow, yesFunction, noFunction) {
        $("input[type='text']").change(function () {
            var confirm = $("#over-cancel");
            dialogDisplayNow.on("dialogbeforeclose", function (event, ui) {
                createNewDialog(confirm, 400, "auto", true, false, false, false);
                confirm.dialog("option", "ui-dialog-titlebar-close", "hide");
                confirm.dialog("option", "ui-dialog-buttonpane", "no-border-button");
                confirm.dialog({
                    buttons: {
                        "Có": function () {
                            yesFunction();
                            confirm.dialog("close");
                        },
                        "Không": function () {
                            noFunction();
                            confirm.dialog("close");
                        },
                        "Hủy bỏ": function () {
                            confirm.dialog("close");
                        },
                    }
                });
                return false;
            });
        });
    },
    closeDialog: function (dialogDisplayNow) {
        CustomerJS.cleanDataDialog(dialogDisplayNow);
        dialogDisplayNow.dialog("destroy");
    },
    cleanDataDialog: function (dialogDisplayNow) {
        $(dialogDisplayNow).find('input').val(null);
        $(dialogDisplayNow).find('.warning-feild').removeClass('border-red')
        $(dialogDisplayNow).find('.warning-feild').next().remove();
    },
});
var serverObject = Object.create({
    uri: "/api/customers",
    getAllCustomer: function (callback) {
        $.ajax({
            url: serverObject.uri,
            type: "GET",
            error: function () { },
            success: function (data) {
                callback(data);
            }
        });
    },
});