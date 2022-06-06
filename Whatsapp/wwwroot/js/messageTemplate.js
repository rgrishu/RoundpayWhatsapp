
// Header Section
var headerType = $("#headerType");
$('input[name=mediaType]').unbind().bind("click", function () {
    let __value = $(this).val();
    let __element = $("#HeaderFile");
    if (__value == '1') {
        __element.attr("accept", "video/mp4");
        __element.click();
        $("#docImage").addClass("d-none");
        VideoPreview(__element);
    }
    else if (__value == '2') {
        __element.attr("accept", "image/*").click().change(evt => {
            let [file] = HeaderFile.files
            if (file) {
                imgPreview.src = URL.createObjectURL(file);
            }
            $("#VideoDisplay").addClass("d-none");
            $("#imgPreview").removeClass("d-none");
            $("#docImage").addClass("d-none");
        });
    }
    else if (__value == '3') {
        if (__value == '3') {
            $('#HeaderFile').unbind();
        }
        __element.attr("accept", ".txt").click();
        $("#imgPreview").addClass("d-none");
        $("#VideoDisplay").addClass("d-none");
        $("#docImage").removeClass("d-none");
    }
});
$(document).ready(function () {
    CheckHeaderType();
});
headerType.change(function () {
    $("#imgPreview").addClass("d-none");
    $("#VideoDisplay").addClass("d-none");
    CheckHeaderType();
});
function CheckHeaderType() {
    if (headerType.val() == "2") {
        $("#mediaSection").removeClass("d-none");
        $("#sectionTextArea").addClass("d-none");
    }
    else {
        $("#sectionTextArea").removeClass("d-none");
        $("#mediaSection").addClass("d-none");
    }
}
function VideoPreview(inputObj) {
    var video = $("video");
    var input = inputObj;
    var duration = 0;
    var img = $("#imgPreview");
    input.on("change", function (e) {
        var file = e.target.files[0];
        if (["video/mp4"].indexOf(file.type) === -1) {
            return;
        }
        video.find("source").attr("src", URL.createObjectURL(file));
        video.get(0).load();
        video.on("loadedmetadata", function (e) {
            duration = video.get(0).duration;
            video[0].currentTime = Math.ceil(duration / 2);
            video.one("timeupdate", function () {
            });
        });
        $("#VideoDisplay").removeClass("d-none");
        $("#imgPreview").addClass("d-none");
    });

}
// Header Ends
//Body Section
var data = [{

}]
var excelData;
var __NumberType = $('input[name=NumberType]');
var __NumberBox = $('#numberBox');
var __NumberInput = $('#numberInput');
var __CSVHeaderBox = $('#CSVHeaderBox');
__NumberType.click(function () {
    let __csvNumber = $(this).val();
    console.log(__NumberBox.val());
    if (__csvNumber == 2) {
        $("#CSVNumbers").click();
        __NumberBox.addClass("d-none");
        __NumberInput.val("");
        __CSVHeaderBox.removeClass('d-none');
        $('.keys').empty();
    }
    else {
        __NumberBox.removeClass("d-none");
        __CSVHeaderBox.addClass('d-none');
    }
})

$(".keys").delegate(".columnsHeader", "click", function () {
    let _clmHead = $(this).text();
    let _msgBody = $("#msgBody");
    let newtxt = _msgBody.val() + '{' + _clmHead + '} ';
    _msgBody.val(newtxt);
});


$('#CSVNumbers').change(() => {
    selectedFile = event.currentTarget.files[0]
    XLSX.utils.json_to_sheet(data, 'out.xlsx');
    if (selectedFile) {
        let fileReader = new FileReader();
        fileReader.readAsBinaryString(selectedFile);
        fileReader.onload = (event) => {
            let data = event.target.result;
            let workbook = XLSX.read(data, { type: "binary" });
            workbook.SheetNames.forEach(sheet => {
                let rowObject = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheet]);
                excelData = rowObject;
                let keys = Object.keys(rowObject[0]);
                $('.keys').empty().append(keys.map((item) => `<span class='mr-2 badge badge-outline-info columnsHeader'>${item}</span>`));
            });
        }
    }
})
// Body Ends
// Action Buttons
var __callToActionBox = $("#callToActionBox");
var __qucikReply = $("#qucikReply");
var __AddButtonBox = $("#AddButtonBox");
var __ActionDDl = $("#ActionDDl");
var __addBtn = $("#addBtn");
var callToActionHtml = '<div class="card my-2 mb-2"><div class= "card-body row"><div class="col-3"><label>Type of Action</label><select class="form-control"><option value="1">Call phone number</option><option value="2">Visit website</option></select ></div><div class="col-3"><label>Button Action</label><input type="text" class="form-control" /></div><div class="col-2"><label>Country</label><select class="form-control"><option>Select</option></select></div><div class="col-3"><label>Phone  Number</label><input type="text" class="form-control" /></div><div class="col-1 align-self-center"><i class="fas fa-trash text-danger mt-2" style="cursor:pointer"></i></div></div></div>'
var quickReplyHtml = '<div class="col-3 mb-2"><label>Button Text</label><input class="form-control mr-2" maxlength="25" type="text" /></div ><div class="col-1 align-self-center"><i class="fas fa-trash text-danger f-left mt-4" style="cursor:pointer;"></i></div>';
var count = 0;
__ActionDDl.change(function () {
    let __thisVal = $(this).val();
    if (__thisVal == '1') {
        __AddButtonBox.removeClass('d-none');
        __callToActionBox.empty().append(callToActionHtml);
        __qucikReply.empty();
        count = 1;
    }
    else if (__thisVal == '2') {
        __AddButtonBox.removeClass('d-none');
        __callToActionBox.empty();
        __qucikReply.empty().append(quickReplyHtml);
        count = 1;
    }
    else {
        __AddButtonBox.addClass('d-none');
        __callToActionBox.empty();
        __qucikReply.empty();
        count = 0;
    }
})
__addBtn.click(function () {
    loadHtml(__ActionDDl.val());
    CheckMaxBtn();
})
function loadHtml(id) {
    if (id == '1') {
        count++;
        __callToActionBox.append(callToActionHtml);
    }
    else if (id == '2') {
        count++;
        __qucikReply.append(quickReplyHtml);
    }
}
function CheckMaxBtn() {
    if (__ActionDDl.val() == '1' && count == 2) {
        __AddButtonBox.addClass('d-none');
    }
    else if (__ActionDDl.val() == '2' && count == 3) {
        __AddButtonBox.addClass('d-none');
    }
}
// Action Button Ends