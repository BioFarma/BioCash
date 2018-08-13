var supplierTestData = {
    supplierId: 1,
    categoryCode: "DN",
    supplierName: "K2BF",
    supplierStatus: "1",
    supplierActive: "1",
    agentFlag: "",
    agentType: "-",
    supplierAddress: {
        sequenceId: "1",
        branch: "non Branch",
        street: "Jl.Pasteur No.28",
        postCode: "40266",
        addressCity: "Bandung",
        addressRegion: "JB",
        addressCountry: "Indonesia",
        headQuarterFlag: "x"
    },
    supplierContacts: [
        {
            communicationType: "01",
            communicationSequence: "1",
            communicationNumber: "022-7308653"
        },
        {
            communicationType: "02",
            communicationSequence: "2",
            communicationNumber: "+6285659084847"
        }
    ],
    dataAdministrations: [
        {
            documentType: "1",
            documentName: "Akta",
            documentDate: "2016-02-01",
            documentNumber: "SN-02001-0993",
            qualification: "Approved",
            validationDate: "2019-09-02",
            notary: "SN-101"
        },
        {
            documentType: "2",
            documentName: "Strukture Organisasi",
            documentDate: "2016-02-01",
            documentNumber: "SN-02001-0993",
            qualification: "Approved",
            validationDate: "2019-09-02",
            notary: "SN-101"
        }
    ],
    bankAccounts: [
        {
            bankType: "002",
            payee: "Awwal Mulyana",
            bankCode: "MDR00124",
            bankAccount: "10202009930",
            payment: "Bio Farma",
            Nilai: "10203",
            attachmentId: "23",
            checkingAccount: "Exist"
        },
        {
            bankType: "002",
            payee: "Awwal Mulyana",
            bankCode: "MDR00124",
            bankAccount: "10202009930",
            payment: "Bio Farma",
            Nilai: "10203",
            attachmentId: "23",
            checkingAccount: "Exist"
        }
    ],
    taxAccounts: [
        {
            taxid: "2",
            taxDate: "2016-09-09"
        },
        {
            taxid: "4",
            taxDate: "2016-09-10"
        }
    ],
    jenisBarangJasa: [
        {
            groupMaterialId: "1",
            groupMaterialCode: "A.0101.1",
            erpCode: "K2bf-1",
            supplierMaterials: [
                {
                    groupMaterialId: "1",
                    productId: "2"
                },
                {
                    groupMaterialId: "1",
                    productId: "2"
                }
            ]
        }
    ]
};

function getCheckBoxData() {
    var checkboxes = $('input.i-checks');
    var checkArray = [];

    checkboxes.on('ifChecked ifUnchecked', function (event) {
        if (event.type == 'ifChecked') {
            checkArray.push($(this).val());
            console.log(checkArray);
        } else {
            var i = checkArray.indexOf($(this).val());
            if (i != -1) {
                checkArray.splice(i, 1);
            }
            console.log(checkArray);
        }
    });

    return checkArray;
}

function getFormData() {
    //console.log($("#tbSupplierName").val());
    var supplier = {
        supplierId: 1,
        categoryCode: $("#ddlJenisRekanan").val(),
        supplierName: $("#tbSupplierName").val(),
        supplierStatus: "1",
        supplierActive: "1",
        agentFlag: "",
        agentType: "-",
        fileIdLogo: "1",
        shortDescription: $('#tbShortDescription').val(),
        supplierContacts: [
            {
                communicationType: "01",
                communicationSequence: "1",
                communicationNumber: $("#tbSupplierPhone").val()
            },
            {
                communicationType: "02",
                communicationSequence: "1",
                communicationNumber: $("#tbSupplierFax").val()
            },
            {
                communicationType: "03",
                communicationSequence: "1",
                communicationNumber: $("#tbSupplierEmail").val()
            }
        ],
        supplierAddress: {
            sequenceId: "1",
            branch: "non Branch",
            street: $("#tbSupplierAddressStreet").val(),
            addressCity: $("#ddlJenisRekanan").val() == 'LN' ? $('#tbSupplierCity').val() : $("#ddlSupplierCity").val(),
            addressRegion: $("#ddlJenisRekanan").val() == 'LN' ? $('#tbSupplierProvince').val() : $("#ddlSupplierProvince").val(),
            addressCountry: $("#tbSupplierAddressCountry").val(),
            postCode: $("#tbSupplierPostalCode").val(),
            headQuarterFlag: "x"
        },
        jenisBarangJasa: [
            {
                groupMaterialId: "1",
                groupMaterialCode: "A.0101.1",
                erpCode: "K2bf-1",
                supplierMaterials: [
                    {
                        groupMaterialId: "1",
                        productId: "2"
                    },
                    {
                        groupMaterialId: "1",
                        productId: "2"
                    }
                ]
            }
        ],
        dataAdministrations: [
            {
                documentType: "1",
                documentName: "Akta",
                documentDate: "2016-02-01",
                documentNumber: "SN-02001-0993",
                qualification: "Approved",
                validationDate: "2019-09-02",
                notary: "SN-101"
            },
            {
                documentType: "2",
                documentName: "Strukture Organisasi",
                documentDate: "2016-02-01",
                documentNumber: "SN-02001-0993",
                qualification: "Approved",
                validationDate: "2019-09-02",
                notary: "SN-101"
            }
        ],
        bankAccounts: [
            {
                bankType: "002",
                payee: "Awwal Mulyana",
                bankCode: "MDR00124",
                bankAccount: "10202009930",
                payment: "Bio Farma",
                Nilai: "10203",
                attachmentId: "23",
                checkingAccount: "Exist"
            },
            {
                bankType: "002",
                payee: "Awwal Mulyana",
                bankCode: "MDR00124",
                bankAccount: "10202009930",
                payment: "Bio Farma",
                Nilai: "10203",
                attachmentId: "23",
                checkingAccount: "Exist"
            }
        ],
        taxAccounts: [
            {
                taxid: "2",
                taxDate: "2016-09-09"
            },
            {
                taxid: "4",
                taxDate: "2016-09-10"
            }
        ]
    };

    return supplier;
}

function testProsesInsert(link, supplier) {
    $.ajax({
        type: "POST",
        data: '{supplierInformation: ' + JSON.stringify(supplier) + '}',
        url: link,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert("Data has been added successfully.");
        },
        error: function (xhr) {
            alert(xhr); //error occurred
        }
    });
}

//function insertToBackEnd(object, dataset, link) {
//    var x = document.getElementById("root");
//    var lis = document.getElementById("root").getElementsByTagName("li");
//    var seq = 0;
//    var Controller = [];

//    var Product = document.getElementById("ddlProduk").value;
//    var Type = document.getElementById("SelectType").value;
//    var Title = document.getElementById("txtJudul").value;
//    var StartDate = document.getElementById("txtStartDate").value;

//    if (document.getElementById('cekNoLimit').checked) {
//        var EndDate = "31/12/9999";
//    } else {
//        var EndDate = document.getElementById("txtEndDate").value;
//    }
//    var form = {};

//    $(".dd-item").each(function () {
//        var control = {};
//        var CellArray = [];
//        Sequence = seq++;
//        ParentId = ""; NodeId = ""; Label = ""; Value = "";
//        ParentId = $(this).parent().parent().attr('name');
//        NodeId = $(this).attr('name');
//        Label = $(this).find("label").html();
//        Value = $(this).find("p").html();
//        type = $(this).attr('cont-type');
//        dtype = $(this).attr('data-type');
//        if (type == "title") {
//            type = "label";
//        }
//        if (Value == null) {
//            Value = "";
//        }
//        if (type == "Table") {
//            Label = ""; Value = "";
//            row = 1;
//            col = 1;
//            $(this).find("tr").each(function () {
//                $(this).find("td").each(function () {
//                    Cell = {};
//                    CellValue = $(this).find("span").html();
//                    if (CellValue == "" || CellValue == null) {
//                        Cell.CellValue = "";
//                        Cell.CellType = "TextBox";
//                    }
//                    else {
//                        Cell.CellValue = CellValue;
//                        Cell.CellType = "Label";
//                    }
//                    Cell.Row = row;
//                    Cell.Col = col;
//                    CellArray.push(Cell);
//                    col++;
//                });
//                row++;
//            });
//        }
//        control.Sequence = Sequence;
//        control.ParentId = ParentId;
//        control.NodeId = NodeId;
//        control.Label = Label;
//        control.Value = Value;
//        control.ControlType = type;
//        control.DataType = dtype;
//        control.CellArray = CellArray;

//        Controller.push(control);
//    });
//    form.Product = Product;
//    form.Type = Type;
//    form.Title = Title;
//    form.StartDate = StartDate;
//    form.EndDate = EndDate;
//    form.Controller = Controller;
//    console.log(form);

//    $.ajax({
//        type: "POST",
//        data: '{Supplier: ' + JSON.stringify(form) + '}',
//        url: link,
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            alert("Data has been added successfully.");
//        },
//        error: function (xhr) {
//            alert(xhr);  //error occurred
//        }
//    });
//}