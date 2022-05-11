﻿var Q;

paceOptions = {
    ajax: true, // disabled
    restartOnRequestAfter: false,
    document: false, // disabled
    eventLag: false, // disabled
    elements: {
        selectors: ['.my-page']
    }
};

var ajaxvalidationerror = xhr => {
    let validationerrors = xhr.responsejson;
    for (var i = 0; i < validationerrors.length; i++) {
        $('span[validation-error-for="' + validationerrors[i].key + '"]').text(validationerrors[i].errors[0]);
    }
}
/*var Q;*/
(Q => {
    function htmlEncoder(a) {
        switch (a) {
            case '&': return '&amp;';
            case '>': return '&gt;';
            case '<': return '&lt;';
        }
        return a;
    }
    function htmlEncode(s) {
        var text = (s === null ? '' : s.toString());
        if ((new RegExp('[><&]', 'g')).test(text)) {
            return text.replace(new RegExp('[><&]', 'g'), htmlEncoder);
        }
        return text;
    }
    Q.htmlEncode = htmlEncode;
    function alert(options) {
        var dialog;
        let maxWidth = $(window).width();
        if (options.hasOwnProperty('maxWidth')) {
            maxWidth = options.maxWidth != '' && options.maxWidth >= maxWidth ? maxWidth : options.maxWidth;
        }
        options = $.extend({
            htmlEncode: false,
            isOkButton: false,
            okButton: 'Ok',
            title: '',
            body: '',
            onClose: null,
            onOpen: null,
            autoOpen: false,
            dialogClass: 's-MessageDialog s-AlertDialog',
            modal: true,
            width: '540px',
            maxWidth: maxWidth,
            minWidth: '50%',
            fluid: true,
            responsive: true,
            top: '',
            resizable: true,
            open: function () {
                if (options.onOpen)
                    options.onOpen.call(this);
                if (options.top !== undefined && options.top !== '')
                    $('.ui-dialog').css({ 'top': options.top })
            },
            close: function () {
                dialog.dialog('destroy');
                if (options.onClose)
                    options.onClose();
            }
        }, options);
        if (options.htmlEncode)
            options.body = Q.htmlEncode(options.body);
        if (!options.buttons && options.isOkButton) {
            var buttons = [];
            buttons.push({
                text: options.okButton,
                click: function () {
                    dialog.dialog('close');
                }
            });
            options.buttons = buttons;
        }

        dialog = $('<div><div class="message"><\/div><\/div>')
            .dialog(options)
            .children('.message')
            .html(options.body)
            .parent()
            .dialog('open');
    };

    Q.alert = alert;
    Q.defaultNotifyOptions = {
        closeButton: true,
        timeOut: 3000,
        showDuration: 250,
        hideDuration: 500,
        extendedTimeOut: 500,
        positionClass: 'toast-top-right'
    };


    function getToastrOptions(options) {
        options = $.extend({}, Q.defaultNotifyOptions, options);
        positionToastContainer(false);
        return options;
    }
    function notifyWarning(message, title, options) {
        console.log(getToastrOptions(options));
        toastr.warning(message, title, getToastrOptions(options));
    }
    Q.notifyWarning = notifyWarning;
    function notifySuccess(message, title, options) {
        toastr.success(message, title, getToastrOptions(options));
    }
    Q.notifySuccess = notifySuccess;
    function notifyInfo(message, title, options) {
        toastr.info(message, title, getToastrOptions(options));
    }
    Q.notifyInfo = notifyInfo;
    function notifyError(message, title, options) {
        toastr.error(message, title, getToastrOptions(options));
    }
    Q.notifyError = notifyError;
    function positionToastContainer(create) {
        if (typeof toastr === 'undefined') {
            return;
        }
        var dialog = $(window.document.body).children('.ui-dialog:visible').last();
        var container = toastr.getContainer(null, create);
        if (container.length === 0) {
            return;
        }
        if (dialog.length > 0) {
            var position = dialog.position();
            container.addClass('positioned-toast toast-top-full-width');
            container.css({ position: 'absolute', top: position.top + 28 + 'px', left: position.left + 6 + 'px', width: dialog.width() - 12 + 'px' });
        }
        else {
            container.addClass('toast-top-full-width');
            if (container.hasClass('positioned-toast')) {
                container.removeClass('positioned-toast');
                container.css({ position: '', top: '', left: '', width: '' });
            }
        }
    }
    Q.positionToastContainer = positionToastContainer;
    Q.notify = (statusCode, message, title, options) => {
        switch (statusCode) {
            case -1: Q.notifyError(message, title, options);
                break;
            case 1: Q.notifySuccess(message, title, options);
                break;
            case 2: Q.notifyWarning(message, title, options);
                break;
        }
    };
    Q.confirm = (message, onYes, options) => {
        var dialog;
        options = $.extend({
            htmlEncode: true,
            yesButton: 'Yes',
            noButton: 'No',
            title: 'Confirmation',
            onNo: null,
            onCancel: null,
            onClose: null,
            autoOpen: false,
            modal: true,
            dialogClass: 's-MessageDialog s-ConfirmDialog',
            width: '40%',
            maxWidth: '450',
            minWidth: '180',
            resizable: false,
            open: function () {
                if (options.onOpen)
                    options.onOpen.call(this);
            },
            close: function () {
                dialog.dialog('destroy');
                if (!clicked && options.onCancel)
                    options.onCancel();
            },
            overlay: {
                opacity: 0.77,
                background: "black"
            }
        }, options);
        if (options.htmlEncode)
            message = Q.htmlEncode(message);
        var clicked = false;
        if (!options.buttons) {
            var buttons = [];
            buttons.push({
                text: options.yesButton,
                click: function () {
                    clicked = true;
                    dialog.dialog('close');
                    if (onYes)
                        onYes();
                },
                class: 'btn btn-outline-success'
            });
            if (options.noButton)
                buttons.push({
                    text: options.noButton,
                    click: function () {
                        clicked = true;
                        dialog.dialog('close');
                        if (options.onNo)
                            options.onNo();
                        else if (options.onCancel)
                            options.onCancel();
                    },
                    class: 'btn btn-outline-danger'
                });
            options.buttons = buttons;
        }
        dialog = $('<div><div class="message"><\/div><\/div>')
            .dialog(options)
            .children('.message')
            .html(message)
            .parent()
            .dialog('open');
    };
    Q.removeEditor = () => {
        alert(tinymce.editors.length)
        if (tinymce.editors.length > 0) {
            tinymce.remove('textarea');
            tinymce.execCommand('mceRemoveEditor', true, 'textarea');
        }
    };

    function initEditor(selector) {
        var initImageUpload = function (editor) {
            var inp = $('<input id="tinymce-uploader" type="file" name="pic" accept="image/*" style="display:none">');
            $(editor.getElement()).parent().append(inp);
            editor.addButton('imageupload', {
                text: '',
                icon: 'image',
                onclick: function (e) {
                    inp.trigger('click');
                }
            });
            inp.on("change", function (e) {
                uploadFile($(this), editor);
            });
        };
        var uploadFile = function (inp, editor) {
            if (inp.val() !== undefined && inp.val() !== '') {
                var input = inp.get(0);
                var data = new FormData();
                data.append('file', input.files[0]);
                $.ajax({
                    url: '/Admin/uploadTinyMCEImage',
                    type: 'POST',
                    data: data,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        console.log(data)
                        if (data.StatusCode === 1) {
                            editor.insertContent('<img class="content-img" src="' + data.ResponseText + '"/>');
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        if (jqXHR.responseText) {
                            errors = JSON.parse(jqXHR.responseText).errors
                            alert('Error uploading image: ' + errors.join(", ") + '. Make sure the file is an image and has extension jpg/jpeg/png.');
                        }
                    }
                });
            }
        };

        if (tinymce.editors.length > 0) {
            tinymce.remove('textarea');
        }
        tinymce.init({
            selector: selector !== '' ? selector : 'textarea',
            //height: 400,
            theme: 'modern',
            plugins: ['advlist autolink lists link image charmap print preview hr anchor pagebreak',
                'searchreplace wordcount visualblocks visualchars code fullscreen',
                'insertdatetime media nonbreaking save table contextmenu directionality',
                'emoticons template paste textcolor colorpicker textpattern imagetools'
            ],
            toolbar1: 'insertfile undo redo  |fontselect  fontsizeselect forecolor backcolor bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent table imageupload | preview code fullscreen print',
            setup: function (editor) {
                initImageUpload(editor);
            },
            image_advtab: true,
            templates: [
                { title: 'Test template 1', content: 'Test 1' },
                { title: 'Test template 2', content: 'Test 2' }
            ],
            content_css: ['//www.tinymce.com/css/codepen.min.css'
            ]
        });
    }

    Q.initEditor = (selector = '') => {
        import('/Content/lib/TinyMCE/tinymce.min.js')//http://cdn.tinymce.com/4/tinymce.min.js
            .then(obj => initEditor(selector))
            .catch(err => console.log('loading error, no such module exists'));
    };

    Q.getQueryString = () => {
        var queries = {};
        $.each(document.location.search.substr(1).split('&'), function (c, q) {
            var i = q.split('=');
            queries[i[0]] = i[1];
            queries[i[1]] = i[2];
        });
        return queries;
    };

    Q.getFormData = ($form) => {
        var unindexed_array = $form.serializeArray();
        var indexed_array = {};
        $.map(unindexed_array, function (n) {
            indexed_array[n['name']] = n['value'] === 'on' ? true : n['value'];
        });
        return indexed_array;
    };

    Q.geoLoaction = () => {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(geoSuccess);
        }
        function geoSuccess(position) {
            geoLoactionDetail.Latitude = position.coords.latitude;
            geoLoactionDetail.Longitute = position.coords.longitude;
        }
    };

    Q.insertAtCaret = (areaId, text) => {
        var txtarea = document.getElementById(areaId);
        if (!txtarea) {
            return;
        }

        var scrollPos = txtarea.scrollTop;
        var strPos = 0;
        var br = ((txtarea.selectionStart || txtarea.selectionStart == '0') ?
            "ff" : (document.selection ? "ie" : false));
        if (br == "ie") {
            txtarea.focus();
            var range = document.selection.createRange();
            range.moveStart('character', -txtarea.value.length);
            strPos = range.text.length;
        } else if (br == "ff") {
            strPos = txtarea.selectionStart;
        }

        var front = (txtarea.value).substring(0, strPos);
        var back = (txtarea.value).substring(strPos, txtarea.value.length);
        txtarea.value = front + text + back;
        strPos = strPos + text.length;
        if (br == "ie") {
            txtarea.focus();
            var ieRange = document.selection.createRange();
            ieRange.moveStart('character', -txtarea.value.length);
            ieRange.moveStart('character', strPos);
            ieRange.moveEnd('character', 0);
            ieRange.select();
        } else if (br == "ff") {
            txtarea.selectionStart = strPos;
            txtarea.selectionEnd = strPos;
            txtarea.focus();
        }

        txtarea.scrollTop = scrollPos;
    }

    Q.copyToClipboard = (str, areaId = '') => {
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(str).select();
        document.execCommand("copy");
        $temp.remove();
        Q.insertAtCaret(areaId, str);
        Q.notify(1, 'Text copied to clipboard')
    };

    Q.pasteAtControl = (ctrl, txtToAdd) => {
        var caretPos = ctrl[0].selectionStart;
        var textAreaTxt = ctrl.val();
        ctrl.val(textAreaTxt.substring(0, caretPos) + txtToAdd + textAreaTxt.substring(caretPos));
        ctrl.focus();
    };
    Q.reset = () => {
        $('input').val('');
        $('textarea').val('');
        $('select').val('0').trigger('change');
        $('select').each(function (i) {
            $(this).selectedIndex = -1;
        });
    };

    Q.readFile = (filePath, callback) => {
        var file;
        var xhr = new XMLHttpRequest();
        xhr.open('GET', filePath, true);
        xhr.onload = function (e) {
            if (this.status === 200) {
                file = new File([this.response], 'fileName.png');
                callback(file);
            }
        };
        xhr.send();
    };

    Q.preloader = {
        load: () => $("body").addClass('has-loading').append('<div class="loading">Loading&#8230;</div>'),
        remove: () => $(".loading").removeClass('has-loading').remove()
    };

    Q.print = (id, css = '') => {
        var n = document.getElementById("printDiv");
        newWin = window.open("", "_blank"),
            newWin.document.write(`<html>
                                     <head>
                                        <title>PrintReport</title>                                    
                                        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"crossorigin="anonymous"/>
                                        <style> .bg-gray{background:#dee2e6;}
                                                 @page{size:A4; margin:5mm 3mm 5mm 3mm;}
                                                 @media print{
                                                              footer {width:100%;position:fixed;bottom:0;}
                                                            }
                                                 .table td,.table th {border-top:none!important}
                                        </style>
                                        ${css}
                                     </head>
                                     <body>`),
            newWin.document.write(n.outerHTML),
            newWin.document.write(`<button id="btnPrint" onclick="printDiv('printDiv');">Print</button>`),
            newWin.document.write(`<script>
function printDiv(divName) {
        var printContents = document.getElementById(divName).innerHTML;
        var originalContents = document.body.innerHTML;
        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
    };
                                         //(()=>{
                                         //       document.getElementById("btnPrint").click();
                                         //       window.close()
                                         //      })();
                                   </script>`),
            newWin.document.write("</body></html>")
    };

    Q.printDiv = function (divName, f) {
        var printContents = document.getElementById(divName).innerHTML;
        var originalContents = document.body.innerHTML;
        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
        $('.btn-print').click(() => {
            Q.printDiv('printDiv', function () {
                let _html = `<div class="form-group">Is Certificate Printed Successfully ? </div>
                         <div class="form-group">
                               <button class="btn btn-outline-success" onclick="yes()">Yes</button>
                               <button class="btn btn-outline-danger" onclick="$('.ui-dialog,.ui-widget-overlay').remove()">No</button>`;
                Q.alert({
                    title: 'Confirmation',
                    body: _html,
                    width: '120px',
                    maxWidth: ''
                });
            });
        });
        $('#withoutBg').click(() => {
            if ($('#withoutBg').is(':checked')) {
                $('[name="BackGround"]').css({ 'display': 'none' })
            }
            else {
                $('[name="BackGround"]').css({ 'display': 'block' })
            }
        });
        $('button.ui-dialog-titlebar-close').unbind().click(() => f());
    };

    Q.IsFormValid = (option) => new Promise((resolve, reject) => {
        if (!option) {
            option = {
                Selector: '',
                callBack: function () {
                }
            }
        }
        let IsValid = true;
        let element = option.Selector === '' ? '[required="required"]' : option.Selector + ' [required="required"]';
        let totalRequiredTag = $(element).length;
        $('.validation-error').text('').removeClass('text-danger text-monospace error');
        $(element).removeClass('invalid');
        $(element).each(function (i) {
            let _tag = $(this).prop('tagName'), _ele = $(this);
            if (_ele.parent('div').html().indexOf('text-invalid') > -1) {
                _ele.parent('div').find('span.text-invalid').remove();
            }
            if (((_tag === 'SELECT' || _tag === 'INPUT' || _tag === 'TEXTAREA') && (_ele.val() === undefined || _ele.val() === '')) || (_tag === 'SELECT' && (_ele.val() === "0" || _ele.find('option:selected').attr('value') === undefined))) {
                let errorMsg = _ele.attr('data-error');
                let areaDescribe = _ele.attr('aria-describedby');
                if (errorMsg === undefined || errorMsg === '') {
                    errorMsg = 'This is mendetory field';
                }
                if (areaDescribe === undefined) {
                    _ele.addClass('invalid').after('<span class="p-absolute text-danger text-monospace text-invalid"><small>' + errorMsg + '</small></span>');
                } else {
                    _ele.addClass('invalid');
                    $('#' + areaDescribe).text(errorMsg).addClass('text-danger text-monospace validation-error');
                }
                _ele.focus();
                IsValid = false;
            }
            if ((i + 1) === totalRequiredTag) {
                if (IsValid) {
                    resolve(IsValid);
                }
                else {
                    reject('Form is not valid');
                }
            }
        });
    });

    Q.validationError = (r) => {
        if (r.hasOwnProperty('IsModalInvalid')) {
            if (r.IsModalInvalid && r.hasOwnProperty('ValidationError')) {
                for (let i = 0; i < r.ValidationError.length; i++) {
                    let element = $('[name="' + r.ValidationError[i].Key + '"]');
                    console.log(r.ValidationError[i].Key, element.parent('div').html());
                    if (element.parent('div').html().indexOf('text-invalid') > -1) {
                        element.parent('div').find('span.text-invalid').remove();
                    }
                    let errorMsg = r.ValidationError[i].Message;
                    let areaDescribe = element.attr('aria-describedby');
                    if (errorMsg === undefined || errorMsg === '') {
                        errorMsg = 'This is mendetory field';
                    }
                    if (areaDescribe === undefined) {
                        element.addClass('invalid').after('<span class="p-absolute text-danger text-monospace text-invalid"><small>' + errorMsg + '</small></span>');
                    } else {
                        element.addClass('invalid');
                        $('#' + areaDescribe).text(errorMsg).addClass('text-danger text-monospace validation-error');
                    }
                    element.focus();
                }
            }
        }
    };

    Q.htmlToword = (id) => {
        //-------------------=======================================
        let table = $('#' + id).find('table');
        table.find('tr').each(function () {
            $(this).css({ 'page-break-after': 'always' })
            $(this).find('th').each(function () {
                $(this).css({ 'vertical-align': 'top', 'line-height': '1.42857143', 'padding': '8px', 'border-bottom-width': '2px', 'border': '1px solid #ddd', 'border-spacing': '0', 'border-collapse': 'collapse' })
            })
            $(this).find('td').each(function () {
                $(this).css({ 'vertical-align': 'top', 'line-height': '1.42857143', 'padding': '8px', 'border-bottom-width': '2px', 'border': '1px solid #ddd', 'border-spacing': '0', 'border-collapse': 'collapse' })
            })
        });
        //---------------------=====================================
        var header = "<html xmlns:o='urn:schemas-microsoft-com:office:office' " +
            "xmlns:w='urn:schemas-microsoft-com:office:word' " +
            "xmlns='http://www.w3.org/TR/REC-html40'>" +
            "<head><meta charset='utf-8'><title>Export HTML to Word Document with JavaScript</title></head><body>";
        var footer = "</body></html>";
        var sourceHTML = header + document.getElementById(id).innerHTML + footer;

        var source = 'data:application/vnd.ms-word;charset=utf-8,' + encodeURIComponent(sourceHTML);
        var fileDownload = document.createElement("a");
        document.body.appendChild(fileDownload);
        fileDownload.href = source;
        fileDownload.download = 'document.doc';
        fileDownload.click();
        document.body.removeChild(fileDownload);
    }

    Q.exportToPdf = (option) => {
        option = $.extend({
            selector: '#printDiv',
            fileName: 'document.pdf'
        }, option);
        try {
            $('table:last').css('border', '1px solid #ddd').find('tr').each(function () {
                $(this).find('th').css({ 'border': '1px solid #ddd', 'padding': '8px', 'line-height': '1.42' });
                $(this).find('td').css({ 'border': '1px solid #ddd', 'padding': '8px', 'line-height': '1.42' });
                $(this).find('th').css({ 'border-top': '0' });
                $(this).find('td').css({ 'border-top': '0' });
            });
            let _html = $(option.selector).find('tbody').html();
            _html = _html.replace('https://staunchlyservices.com/Content/images/', '');
            _html = _html.replace('/Content/images/', '');
            let _header = $(option.selector).find('thead').html();
            _header = _header.replace('https://staunchlyservices.com/Content/images/', '');
            _header = _header.replace('/Content/images/', '');
            $.post('/Admin/DownloadHtmlAsPdf', { Content: _html, header: _header })
                .done(result => {
                    var sampleArr = Q.base64ToArrayBuffer(result.ResponseText);
                    var blob = new Blob([sampleArr], { type: "application/pdf" });
                    var link = document.createElement('a');
                    link.href = window.URL.createObjectURL(blob);
                    link.target = 'blank';
                    link.download = option.fileName;
                    link.click();
                });
        }
        catch (xhr) {
            console.log(xhr.responseText);
        }
    };

    Q.exportToWord = (option) => {
        Q.resolveStyle().then(function () {
            option = $.extend({
                selector: '#word-content',
                filename: 'document'
            }, option);
            if (!window.Blob) {
                alert('Your legacy browser does not support this action.');
                return;
            }
            var html, link, blob, url;
            // EU A4 use: size: 841.95pt 595.35pt;
            // US Letter use: size:11.0in 8.5in;
            css = (
                '<style>' +
                '@page WordSection{size: 595.35pt 841.95pt;mso-page-orientation:portrait;margin-top:5mm;mso-header:header;mso-footer:footer;' +
                'margin-bottom:5mm; margin-right:5mm; margin-left:5mm;}' +
                'div.WordSection {page: WordSection;}' +
                //'p.MsoFooter, li.MsoFooter, div.MsoFooter {mso-pagination:widow-orphan;tab-stops:center 3.0in right 6.0in;}'+
                //'p.MsoHeader, li.MsoHeader, div.MsoHeader {mso-pagination:widow-orphan;tab-stops:center 3.0in right 6.0in;}'+
                //'table#hrdftrtbl{margin:0in 0in 0in 9in;} ' +
                'table tbody{font-size:11pt}' +
                '</style>'
            );
            html = $(option.selector).html()
            blob = new Blob(['\ufeff', css + html], {
                type: 'application/msword'
            });
            url = URL.createObjectURL(blob);
            link = document.createElement('A');
            link.href = url;
            // Set default file name. 
            // Word will append file extension - do not add an extension here.
            link.download = option.filename;
            document.body.appendChild(link);
            if (navigator.msSaveOrOpenBlob) navigator.msSaveOrOpenBlob(blob, 'Document.doc'); // IE10-11
            else link.click();  // other browsers
            document.body.removeChild(link);
            $('label[name="checkbox"]').after('<input type="checkbox"/>').remove();
            $('table').find('td').removeAttr('style');
        });
    };

    Q.base64ToArrayBuffer = (base64) => {
        var binaryString = window.atob(base64);
        var binaryLen = binaryString.length;
        var bytes = new Uint8Array(binaryLen);
        for (var i = 0; i < binaryLen; i++) {
            var ascii = binaryString.charCodeAt(i);
            bytes[i] = ascii;
        }
        return bytes;
    }

    Q.exportToWord_old = (option) => {
        option = $.extend({
            filename: 'document.doc'
        }, option);
        Q.resolveStyle().then(function () {
            var o = {
                download: 0,
                filename: option.filename
            };
            $(document).googoose(o)
        })
    };

    Q.resolveStyle = () => new Promise((resolve, reject) => {
        $('input[type="checkbox"]').after(function () {
            let _html = '<label name="checkbox">☐</label>'
            let IsChecked = $(this).is(':checked');
            if (IsChecked) {
                _html = '<label name="checkbox">✅</label>';
            }
            return _html;
        }).remove();
        let element = $('.googoose-wrapper table');
        $('#footer').css({ 'mso-element': 'footer' });
        let totaltr = element.find('tr').length;
        element.find('tr').each(function (i) {
            $(this).find('td').css({ 'border': '2px solid #000', 'padding': '8px', 'line-height': '1.42', 'border-spacing': '0', 'border-collapse': 'collapse' });
            if ((i + 1) === totaltr) {
                resolve();
            }
        });
    });

    Q.alterStyle = (style) => {
        var keys = Object.keys(style);
        var sel = window.getSelection(); // Gets selection
        if (sel.rangeCount) {
            // Creates a new element, and insert the selected text with the chosen font inside
            var e = document.createElement('span');
            //e.style = keys[0] + ':' + style[keys] + ';';
            e.style = style.key + ':' + style.value + ';';
            e.innerHTML = sel.toString();
            // https://developer.mozilla.org/en-US/docs/Web/API/Selection/getRangeAt
            var range = sel.getRangeAt(0);
            range.deleteContents(); // Deletes selected text…
            range.insertNode(e); // … and inserts the new element at its place
        }
    };

    Q.dragElement = (elmnt) => {
        var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
        if (document.getElementById(elmnt.id + "header")) {
            // if present, the header is where you move the DIV from:
            document.getElementById(elmnt.id + "header").onmousedown = dragMouseDown;
        } else {
            // otherwise, move the DIV from anywhere inside the DIV:
            elmnt.onmousedown = dragMouseDown;
        }

        function dragMouseDown(e) {
            e = e || window.event;
            e.preventDefault();
            // get the mouse cursor position at startup:
            pos3 = e.clientX;
            pos4 = e.clientY;
            document.onmouseup = closeDragElement;
            // call a function whenever the cursor moves:
            document.onmousemove = elementDrag;
        }

        function elementDrag(e) {
            e = e || window.event;
            e.preventDefault();
            // calculate the new cursor position:
            pos1 = pos3 - e.clientX;
            pos2 = pos4 - e.clientY;
            pos3 = e.clientX;
            pos4 = e.clientY;
            // set the element's new position:
            elmnt.style.top = (elmnt.offsetTop - pos2) + "px";
            elmnt.style.left = (elmnt.offsetLeft - pos1) + "px";
        }

        function closeDragElement() {
            // stop moving when mouse button is released:
            document.onmouseup = null;
            document.onmousemove = null;
        }
    };

    Q.previewImage = (event, output = 'profile') => {
        output.src = URL.createObjectURL(event.target.files[0]);
        output.onload = function () {
            URL.revokeObjectURL(output.src) // free memory
        }
    };

    Q.renderError = xhr => {
        if (xhr.status === 400) {
            let validationErrors = xhr.responseJSON;
            for (var i = 0; i < validationErrors.length; i++) {
                let __span = $('span[data-valmsg-for="' + validationErrors[i].key + '"]');
                if (__span.index() == -1) {
                    console.log('[name="' + validationErrors[i].key + '"]');
                    $('[name="' + validationErrors[i].key + '"]').parent('div').append(`<span data-valmsg-for="${validationErrors[i].key}" class="error">${validationErrors[i].errors[0]}</span>`);
                }
                else {
                    __span.text(validationErrors[i].errors[0]);
                }

            }
        }
        else if (xhr.status === 401) {
            let currentUrl = window.location.href;
            let _url = new URL(currentUrl);
            window.location.href = "/Account/Login?ReturnUrl=" + _url.pathname;
        }
        else {
            console.log(xhr.responseText);
            Q.notify(-1, 'An error occurred.');
        }
    };
})(Q || (Q = {}));

(function ($) {
    $.fn.fixTableHeader = function () {
        let scrollTop = $(window).scrollTop(),
            elementOffset = $('table:last').offset().top,
            distance = (elementOffset - scrollTop),
            footer = $('footer').height();
        let _style = `<style>.calcHeight{height: calc(100vh - ${distance}px - 93px); }.fixedHeader th {background: #dcdbc1!important; position: sticky;top: -1px; z-index:9;padding:10px;}</style>`
        $('head').append(_style);
        $(this).addClass('fixedHeader');
        $(this).closest('div').addClass('calcHeight');
    };
}($));

(function (e) {
    e.fn.numeric = function (t) {
        let i = e.extend({ numericType: "number", maxLength: 0 }, t);
        e(this).keypress((t) => {
            if (0 !== i.maxLength && e(t.currentTarget).val().length > i.maxLength) return !1;
            let a = t.keyCode ? t.keyCode : t.which;
            return "number" === i.numericType ? a >= 48 && a <= 57 : "decimal" === i.numericType ? (a >= 48 && a <= 57) || (46 === a && -1 === e(t.currentTarget).val().indexOf(".")) : void 0;
        });
    };
})($);

function ajaxFormSubmit(form) {
    event.preventDefault();
    /*$.validator.unobtrusive.parse(form);*/
    var data, enctype = '';
    if ($(form).find('input[type="file"]').index() == -1) {
        data = $(form).serializeArray();
    }
    else {
        enctype = 'multipart/form-data';
        data = new FormData(form);
    }
    var ajaxConfig = {
        type: 'POST',
        url: form.action,
        data: data,
        success: function (response) {
            Q.notify(response.statusCode, response.responseText);
            if (response.statusCode == 1) {
                $('.error').text('');
                $(form).trigger("reset");
                Q.reset();
                if (typeof loadData !== 'undefined' && $.isFunction(loadData))
                    loadData();
            }
        },
        error: function (xhr) {
            Q.renderError(xhr);
        }
    }
    if (enctype == "multipart/form-data") {
        ajaxConfig["contentType"] = false;
        ajaxConfig["processData"] = false;
    }
    $.ajax(ajaxConfig);
}

(function ($) {
    $.renderDataTable = function (options) {
        options = $.extend({}, {
            columns: [],
            apiUrl: '/',
            selector: 'table',
            buttons: [
                'copyHtml5',
                'excelHtml5',
                'csvHtml5'
               // 'pdfHtml5'
            ],
            filters: {},
        }, options);
        // console.log(options.apiUrl);
        $(options.selector).dataTable({
            processing: true,
            serverSide: true,
            paging: true,
            destroy: true,
            responsive: true,
            dom: 'Bfrtip',
            buttons: options.buttons,
            ajax: {
                url: options.apiUrl,
                type: "POST",
                data: function (jsonAOData) {
                    //jsonAOData.param = JSON.stringify(options.filters);
                    //jsonAOData = $.extend(jsonAOData, options.filters);
                    //console.log(jsonAOData);
                    var filters = options.filters;
                    return { jsonAOData, filters }
                },
                //dataSrc: "data",
            },
            aoColumns: options.columns,
            //scrollY: $('[name="Applicationlist"]').offset().top + 118,
            scrollCollapse: true,
            // dom: 'R<"top"Bf>rt<"bottom"ilp><"clear">',
        });
    }
}($));

$.fn.dataTable.pipeline = function (opts) {
    // Configuration options
    var conf = $.extend({
        pages: 5,     // number of pages to cache
        url: '',      // script url
        data: null,   // function or object with parameters to send to the server
        // matching how `ajax.data` works in DataTables
        method: 'POST', // Ajax HTTP method
        customeEvent: false
    }, opts);

    // Private variables for storing the cache
    let cacheLower = -1;
    let cacheUpper = null;
    let cacheLastRequest = true;
    let cacheLastJson = null;

    return function (request, drawCallback, settings) {
        let ajax = false;
        let requestStart = request.start;
        let drawStart = request.start;
        let requestLength = request.length;
        let requestEnd = requestStart + requestLength;

        if (settings.clearCache) {
            // API requested that the cache be cleared
            ajax = true;
            settings.clearCache = false;
        }
        else if (cacheLower < 0 || requestStart < cacheLower || requestEnd > cacheUpper) {
            // outside cached data - need to make a request
            ajax = true;
        }
        else if (JSON.stringify(request.order) !== JSON.stringify(cacheLastRequest.order) ||
            JSON.stringify(request.columns) !== JSON.stringify(cacheLastRequest.columns) ||
            JSON.stringify(request.search) !== JSON.stringify(cacheLastRequest.search)
        ) {
            // properties changed (ordering, columns, searching)
            ajax = true;
        }
        if (conf.customeEvent == true) {
            ajax = true;
        }
        // Store the request for checking next time around
        cacheLastRequest = $.extend(true, {}, request);

        if (ajax) {
            // Need data from the server
            if (requestStart < cacheLower) {
                requestStart = requestStart - (requestLength * (conf.pages - 1));
                if (requestStart < 0) {
                    requestStart = 0;
                }
            }
            cacheLower = requestStart;
            cacheUpper = requestStart + (requestLength * conf.pages);
            request.start = requestStart;
            request.length = requestLength * conf.pages;

            // Provide the same `data` options as DataTables.
            if (typeof conf.data === 'function') {
                // As a function it is executed with the data object as an arg
                // for manipulation. If an object is returned, it is used as the
                // data object to submit
                var d = conf.data(request);
                if (d) {
                    $.extend(request, d);
                }
            }
            else if ($.isPlainObject(conf.data)) {
                // As an object, the data given extends the default
                $.extend(request, conf.data);
            }
            else if (opts.filters) {
                $.extend(request, opts.filters);
            }
            //var additionalFilters = opts.filters
            return $.ajax({
                "type": conf.method,
                "url": conf.url,
                "data": request,
                "dataType": "json",
                "cache": false,
                "success": function (json) {
                    cacheLastJson = $.extend(true, {}, json);

                    if (cacheLower != drawStart) {
                        json.data?.splice(0, drawStart - cacheLower);
                    }
                    if (requestLength >= -1) {
                        json.data?.splice(requestLength, json.data.length);
                    }
                    drawCallback(json);
                }
            });
        }
        else {
            json = $.extend(true, {}, cacheLastJson);
            json.draw = request.draw; // Update the echo for each response
            json.data?.splice(0, requestStart - cacheLower);
            json.data?.splice(requestLength, json.data.length);

            drawCallback(json);
        }
    }
};
// Register an API method that will empty the pipelined data, forcing an Ajax
// fetch on the next draw (i.e. `table.clearPipeline().draw()`)
$.fn.dataTable.Api.register('clearPipeline()', function () {
    return this.iterator('table', function (settings) {
        settings.clearCache = true;
    });
});
//
// DataTables initialisation
(function ($) {
    $.renderDataTable2 = function (options) {
        options = $.extend({}, {
            columns: [],
            apiUrl: '/',
            selector: 'table',
            customeEvent: false,
            buttons: [
                'copyHtml5',
                'excelHtml5',
                'csvHtml5'
               // 'pdfHtml5'
            ],
            filters: {},
        }, options);
        // console.log(options.apiUrl);
        var table = $(options.selector).DataTable({
            processing: true,
            serverSide: true,
            paging: true,
            customeEvent: false,
            destroy: true,
            //responsive: {
            //    details: {
            //        type: 'column',
            //        target: -1
            //    }
            //},
            columnDefs: [{
                className: 'dtr-control',
                orderable: false,
                targets: -1
            }],
            //dom: 'Bfrtip',
            dom: "<'row'<'col-sm-12'Bfrt>>" +
                "<'row'<'col-sm-4'l><'col-sm-8'p>>" +
                "<'row'<'col-sm-12'i>>",
            searching: true,
            buttons: options.buttons,
            stateSave: true,
            ajax: $.fn.dataTable.pipeline({
                url: options.apiUrl,
                pages: 5,// number of pages to cache,
                filters: options.filters,
                customeEvent: options.customeEvent
            }),
            aoColumns: options.columns,
            scrollY: "600px",

            scrollCollapse: true,
            initComplete: function () {
                delaySearch(this.api())
            },
            drawCallback: function (settings) {
                //this.api().fnAdjustColumnSizing();
            }
        });

        function delaySearch(api) {
            var timer = null;
            // Grab the datatables input box and alter how it is bound to events
            $(".dataTables_filter input")
                .unbind() // Unbind previous default bindings
                .bind("input", function (e) { // Bind our desired behavior
                    searchTerm = this.value;//item.val();
                    clearTimeout(timer);
                    timer = setTimeout(function () {
                        api.search(searchTerm).draw();
                    }, 600);
                    return;
                });
        }
    }
}($));