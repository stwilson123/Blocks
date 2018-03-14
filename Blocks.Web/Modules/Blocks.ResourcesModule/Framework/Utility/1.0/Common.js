

var ValidateHelper = (function ($) {
    isNullOrEmpty: function isNullOrEmpty(v) {
        return v === '' || v == undefined || v == null || typeof (v) == "undefined";

    };
    isDecimal: function isDecimal(v) {
        return /^(-?\d+)(\.\d+)?$/.test(v);
    };
    isHtml: function isHtml(v) {
        return new RegExp('^<([^>\s]+)[^>]*>(.*?<\/\\1>)?$').test(v);
    };
    isInt: function isInt(v) {
        return /^[+]{0,1}(\d+)$/.test(v)
    };
    isErrorCode: function isErrorCode(code)
    {
        return (code != 100 && code < 3000) || code > 10000;
    };
    return { isNullOrEmpty: isNullOrEmpty, isDecimal: isDecimal, isHtml: isHtml, isInt: isInt, isErrorCode: isErrorCode };
})(jQuery);
 
//Url
var UrlHelper = (function ($) {
    //URL随机化，防止缓存
    GetRandURL: function GetRandURL(url) {
        url = setUrlParam(url, "rannum", MathHelper.GetRandomNum(0, 99999999));
        return url;
    };
    //URL 参数赋值
    setUrlParam: function setUrlParam(oldurl, paramname, pvalue) {
        var reg = new RegExp("(\\?|&)" + paramname + "=([^&]*)(&|$)", "gi");
        var pst = oldurl.match(reg);
        if ((pst == undefined) || (pst == null)) {
            return oldurl + ((oldurl.indexOf("?") == -1) ? "?" : "&") + paramname + "=" + pvalue;
        }

        var t = pst[0];
        var retxt = t.substring(0, t.indexOf("=") + 1) + pvalue;
        if (t.charAt(t.length - 1) == '&') retxt += "&";

        return oldurl.replace(reg, retxt);
    };
    GetUrlParam: function GetUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(decodeURI(r[2])); return null;
    };
    return {
        GetRandURL: GetRandURL,
        setUrlParam: setUrlParam,
        GetUrlParam: GetUrlParam
    };

})(jQuery, MathHelper);

var FormHelper = (function ($) {
    //获取POST页面参数组合成Json数据
    getPostBodyToJson: function getPostBodyToJson(FormID) {
        var vForm = document.forms[0];
        if (typeof (FormID) == "undefined") {
            return;
        }
        vForm = document.getElementById(FormID);
        var vPostData = getRequestToJson(vForm);
        return vPostData;
    };

    //通过Form name组合Json数据
    getRequestToJson: function getRequestToJson(oform) {
        var aParams = new Object();
        var message = "";
        if (ValidateHelper.isNullOrEmpty(oform))
            return aParams;
        for (var i = 0; i < oform.elements.length; i++) {

            if (oform.elements[i].name != '__viewstate' && oform.elements[i].name != '__eventvalidation') {
                if (oform.elements[i].id != "") //没有id的不生成串
                {
                    var currentElement = $(oform.elements[i]);
                    if (currentElement.attr("type") == "text" || $(oform.elements[i]).is("select") || $(oform.elements[i]).is("textArea")) {
                        aParams[currentElement.attr("id")] = currentElement.val();
                    }
                    else if (currentElement.hasClass("____HIDGRIDDATA")) {
                        var gridid = currentElement.attr("gridid");
                        if ("" != gridid) {
                            var data = $("#" + gridid).getRowData();
                            var cols = currentElement.attr('rdmodel');
                            var colsarr = cols.split(',');

                            var tempArray = [];
                            for (var k = 0; k < data.length; k++) {
                                var jsonObj = {};
                                for (var j = 0; j < colsarr.length; j++) {
                                    var v = data[k][colsarr[j]];
                                    if (ValidateHelper.isNullOrEmpty(v)) {
                                        v = "";
                                    }
                                    jsonObj[colsarr[j]] = v;
                                }
                                tempArray.push(jsonObj);
                            }
                            if (tempArray.length > 0) {
                                aParams[gridid] = tempArray.Serialize();
                            }
                        }
                    }
                    
                }
            }
            //    if ($(oform.elements[i]).hasclass("easyui-combobox")) {
            //        var selectvalue = "";
            //        var data = $('#' + oform.elements[i].id).combobox('getdata');

            //        selectvalue = $('#' + oform.elements[i].id).combobox('getvalue').trim();
            //        var find = false;
            //        for (var j = 0; j < data.length; j++) {

            //            if (data[j].id != undefined) {
            //                if (selectvalue.tostring() == data[j].id.trim()) {
            //                    find = true;
            //                    break;
            //                }
            //            }
            //            if (data[j].text != undefined) {
            //                if (selectvalue.tostring() == data[j].text.trim()) {
            //                    find = true;
            //                    break;
            //                }
            //            }
            //            if (data[j].value != undefined) {
            //                if (selectvalue.tostring() == data[j].value.trim()) {
            //                    find = true;
            //                    break;
            //                }
            //            }
            //            if (data[j].text != undefined) {
            //                if (selectvalue.tostring() == data[j].text.trim()) {
            //                    find = true;
            //                    break;
            //                }
            //            }
            //        }
            //        if (data.length == 0) {
            //            if (selectvalue == "") {
            //                find = true;
            //            }
            //        }
            //        if (!find) {
            //            message += selectvalue + ",";
            //        }
            //        aparams[oform.elements[i].id] = selectvalue;
            //    }
            //    else if ($(oform.elements[i]).hasclass("____hidgriddata")) {
            //        var gridid = $(oform.elements[i]).attr("gridid");
            //        if ("" != gridid) {
            //            var data = $("#" + gridid).jqgrid('getrowdata');
            //            var cols = $(oform.elements[i]).attr('rdmodel');
            //            var colsarr = cols.split(',');

            //            var temp = [];
            //            for (var k = 0; k < data.length; k++) {
            //                var json = {};
            //                for (var j = 0; j < colsarr.length; j++) {
            //                    var v = data[k][colsarr[j]];
            //                    if (isnullorempty(v)) {
            //                        v = "";
            //                    }
            //                    json[colsarr[j]] = v;
            //                }
            //                temp.push(json);
            //            }
            //            if (temp.length > 0) {
            //                aparams[oform.elements[i].id] = temp.serialize();
            //            }
            //        }
            //    }
            //    else if ($(oform.elements[i]).attr('type') == "checkbox") {
            //        aparams[oform.elements[i].id] = $(oform.elements[i]).attr('checked') == "checked" ? 1 : 0;
            //    }
            //    else {
            //        aparams[oform.elements[i].id] = encodeuricomponent(oform.elements[i].value.trim());
            //    }
            //}
            //if (message.length != 0) {
            //    message = message.substring(0, message.length - 1);
            //    jqAlert("下拉框中不存在以下数据：<br />" + message + "<br />请修改！");
            //    return "nodata";
            //    }

        }
        return aParams;
    }

    getJsonToPostBody: function getJsonToPostBody(ResponseData) {
        for (dataProperty in ResponseData) {
            var element = $("#" + dataProperty);
            if (element.attr("type") == "text" || element.is("select"))
                element.val(ResponseData[dataProperty]);
        }
    };
    return {
        getPostBodyToJson: getPostBodyToJson,
        getRequestToJson: getRequestToJson,
        getJsonToPostBody: getJsonToPostBody
    };
})(jQuery);

var MathHelper = (function ($) {
    //获取随机数
    GetRandomNum: function GetRandomNum(Min, Max) {
        var Range = Max - Min;
        var Rand = Math.random();
        return (Min + Math.round(Rand * Range));
    };
    return { GetRandomNum: GetRandomNum };
})(jQuery);

var AjaxHelper = (function ($) {
    pubAjax = function (settings) {
        if (settings.url != "" && settings.url != null) {
            settings.url = UrlHelper.GetRandURL(settings.url);
            settings = jQuery.extend({
                errorHandle: true, //是否处理服务器错误 error.call
                onFailCallBack: function () { },
                type: "post",
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                data: settings.data,
                datatype: ValidateHelper.isNullOrEmpty(settings.url) ? 'json' : settings.url,
                async: false, //同步
                cache: false,
                statusCode: {
                    404: function () { alert('无效的URL'); }
                },
                beforeSend: function (XMLHttpRequest) {
                    this;
                },
                complete: function (XMLHttpRequest, Status) {
                    this;
                },
                success: function (data) {
                    this;
                },
                error: function (XMLHttpRequest, Status, errorThrown) {
                    this;
                },
                ajaxError: function () { }
            }, settings);
        } else {
            alert('请指定URL');
            return;
        }

        var btnObj = null;
        var currentWindow = null;

        if (!ValidateHelper.isNullOrEmpty(settings.buttonEvent)) {
            //ie and firefox
            if ($.browser.msie || $.browser.mozilla) btnObj = $(settings.buttonEvent.target);
                //chrome
            else btnObj = $(settings.buttonEvent.currentTarget);
        }

        if (!ValidateHelper.isNullOrEmpty(settings.currentWindow)) {
            currentWindow = settings.currentWindow;
        }

        jQuery.ajax({
            type: settings.type,
            url: settings.url,
            cache: settings.cache,
            sync: settings.async,
            contentType: settings.contentType,
            data: settings.data,
            datatype: settings.datatype,
            statusCode: settings.statusCode,
            success: function (data) {
                if (!ValidateHelper.isNullOrEmpty(data)) {
                    var result = eval('(' + data + ')');
                    ajaxSuccess(result, settings.onSuccessCallBack, settings.onFailCallBack);
                    return;
                }
                ajaxSuccess({ msg: "Data from server is null.Please check response.", code: 901 }, settings.onSuccessCallBack, settings.onFailCallBack);

            },
            error: function (XMLHttpRequest, Status, errorThrown) {
                if (settings.errorHandle) {
                    settings.error(XMLHttpRequest, Status, errorThrown);


                } else {
                    ;
                }
                if (null != btnObj) btnObj.button('enable'); //启用按钮 
                if (null != currentWindow) {
                    UnLockWindow(currentWindow); //解锁窗口
                    HidePrompt(currentWindow);
                }
            },
            beforeSend: function (XMLHttpRequest) {
                settings.beforeSend(XMLHttpRequest);

                if (null != btnObj) btnObj.button('disable'); //禁用按钮 
                if (null != currentWindow) {
                    LockWindow(currentWindow); //锁定窗口
                    ShowPrompt(currentWindow);
                }
            },
            complete: function (XMLHttpRequest, Status) {
                settings.complete(XMLHttpRequest, Status);

                if (null != btnObj) btnObj.button('enable'); //启用按钮 
                if (null != currentWindow) {
                    UnLockWindow(currentWindow); //解锁窗口
                    HidePrompt(currentWindow);
                }
            }
        });
    };


    //提交表单，执行easyui验证
    //URL, htmlFormId, onSuccessCallBack, btnEvent, currentWindow, onFailCallBack
    submitJSONForm: function submitJSONForm(setting) {
        var option = $.extend(setting, {});

        //if (!EasyUiValidate(option.htmlFormId)) {
        //    return false;
        //}

        var vPostData = FormHelper.getPostBodyToJson(option.htmlFormId);
        if ($.type(vPostData) == "string") {
            if (vPostData == "nodata")
                return;
        }
        var extendSetting = $.extend(true,{
            postJsonData: vPostData
        }, setting);
        pubAjax({
            url: setting.URL,
            data: extendSetting.postJsonData,
            type: "POST",
            currentWindow: option.currentWindow,
            buttonEvent: option.btnEvent,
            errorMessageAlertCallBack: option.onFailCallBack,
            onSuccessCallBack: function (msg, data, code) {
                extendSetting.onSuccessCallBack(msg, data, code);
            },
            error: function (XMLHttpRequest, Status, errorThrown) {

                extendSetting.onFailCallBack(XMLHttpRequest, Status, errorThrown);
            },
            complete: function (XMLHttpRequest, Status) {
            }
        });
    };
    submitJSONFormWithLoadDialog: function submitJSONFormWithLoadDialog(setting) {
       // ShowLoading();//显示loading
        var onSuccessCallBackFun = setting.onSuccessCallBack;
        var onFailCallBackFun = setting.onFailCallBack;


        var option = $.extend(setting, {
            onSuccessCallBack: function (msg, content, code) {

                if (ValidateHelper.isErrorCode(code)) {
                    OpenTipWindowError(MessageHelper.getRemoveSpecialChar(msg));
                    try{
                        if (DataType.isFunction(onFailCallBackFun))
                            onFailCallBackFun(msg, content, code);
                    }
                    catch (e) {
                        throw e;
                    }
                    finally {
                       // CloseLoading();

                    }
                    return;
                }
                try
                {
                    onSuccessCallBackFun(msg, content, code);
                }
                catch(e)
                {
                    throw e;
                }
                finally
                {
                   // CloseLoading();

                }
            },
            onFailCallBack: function (XMLHttpRequest, Status, errorThrown) {
                OpenTipWindowError("意外出错，请刷新页面重试。");
               
                try {
                    if (DataType.isFunction(onFailCallBackFun))
                        onFailCallBackFun(Status, XMLHttpRequest, errorThrown);
                }
                 catch (e) {
                        throw e;
                    }
                    finally {
                     //   CloseLoading();

                }
            }
        });

        submitJSONForm(option);
    };
    LockWindow: function LockWindow(o) {
        if (isNullOrEmpty(o)) return;
        var wo = null;

        if ($.type(o) == "string") {
            wo = $("#" + o);
        }
        if ($.type(o) == "object") {
            wo = o;
        }
        if (0 < wo.length) {
            wo.data('_Dialog_CanClose', false);
        }
    };

    UnLockWindow: function UnLockWindow(o) {
        if (isNullOrEmpty(o)) return;
        var wo = null;

        if ($.type(o) == "string") {
            wo = $("#" + o);
        }
        if ($.type(o) == "object") {
            wo = o;
        }
        if (0 < wo.length) {
            wo.data('_Dialog_CanClose', true);
        }
    };

    ShowPrompt: function ShowPrompt(o) {
        if (isNullOrEmpty(o)) return;
        var wo = null;

        if ($.type(o) == "string") {
            wo = $("#" + o);
        }
        if ($.type(o) == "object") {
            wo = o;
        }
        if (0 < wo.length) {
            var html = "<div class='post_tipp' style='width:86px;height:6px;float:left;margin:1.2em 0 0.5em 0.4em'><img src=\"/Images/ajaxtipimage.gif\" /></div>";
            $("div.ui-dialog[role='dialog'][aria-describedby='" + wo.attr('id') + "']").find('.ui-dialog-buttonpane').append(html);
        }
    };

    HidePrompt: function HidePrompt(o) {
        if (isNullOrEmpty(o)) return;
        var wo = null;

        if ($.type(o) == "string") {
            wo = $("#" + o);
        }
        if ($.type(o) == "object") {
            wo = o;
        }
        if (0 < wo.length) {
            $("div.ui-dialog[role='dialog'][aria-describedby='" + wo.attr('id') + "']").find('.post_tipp').remove();
        }
    };


    ajaxSuccess: function ajaxSuccess(result, callback, jqAlertCallBack) {
        ////登陆超时
        //if (result.Status == 100) {
        //    jqAlert(BuildExceptionMessage(result), function () { location.href = "/login.aspx"; });
        //    return;
        //}
        if (!ValidateHelper.isNullOrEmpty(callback))
            callback(result.msg, result.content, result.code);
        //if (result.code == 100) {

        //}
        //else if (result.code != 100) {
        //    if (!ValidateHelper.isNullOrEmpty(jqAlertCallBack))
        //        jqAlertCallBack(result.msg, result.content, result.code);
        //}

        //if (result.Status == 100) {
        //    //成功
        //    if (!ValidateHelper.isNullOrEmpty(callback)) {
        //        if (result === null) {
        //            callback(null);
        //            return;
        //        }
        //        var contentType = $.type(result.Content);
        //        if (contentType === "undefined" || contentType === "null") {
        //            callback(result);
        //        }
        //        else {
        //            callback(result.Content);
        //        }
        //    }
        //}
        //else if (result.Status == 300) {
        //    //失败
        //    var msg = result.StateDescription;
        //    if (jqAlertCallBack) {
        //        jqAlert(msg, jqAlertCallBack);
        //    }
        //    else {
        //        jqAlert(msg);
        //    }
        //}
        //else if (result.Status == 500) {
        //    //代码异常
        //    var title = "服务器错误：<br />日志ID：" + result.LogID + "<br />";
        //    var message = title + result.StateDescription;

        //    if (jqAlertCallBack) {
        //        jqAlert(message, jqAlertCallBack);
        //    }
        //    else {
        //        jqAlert(message);
        //    }
        //}
        //else if (result.Status == 600) {
        //    //平台异常
        //    var msg = BuildExceptionMessage(result);

        //    if (jqAlertCallBack) {
        //        jqAlert(msg, jqAlertCallBack);
        //    }
        //    else {
        //        jqAlert(msg);
        //    }
        //}


    }
    return { pubAjax: pubAjax, submitJSONForm: submitJSONForm, submitJSONFormWithLoadDialog: submitJSONFormWithLoadDialog };
})(jQuery, ValidateHelper, FormHelper, UrlHelper, MessageHelper);


var MessageHelper = (function ($) {

    getRemoveSpecialChar: function getRemoveSpecialChar(str) {
        if (str === undefined)
            return str;
        return str.replace(/\[|\]|\{|\}/g, '');
    };
    return {
        getRemoveSpecialChar: getRemoveSpecialChar
    };
})(jQuery);

//var PrintHelper = (function ($) {

//    printTemplate: function printTemplate(str) {
//        if (str === undefined)
//            return str;
//        return str.replace(/\[|\]|\{|\}/g, '');
//    };
//    return {
//        printTemplate: printTemplate
//    };
//})(jQuery);
////扩展验证
//$.extend($.fn.validatebox.defaults.rules, {
//    integer: {
//        validator: function (value) {
//            return /^-?\d+$/.test(value);
//        },
//        message: '该项应为整数'
//    },
//    NegativeABC:
//    {
//        validator: function (value) {
//            return /^-?\d+$/.test(value);
//        },
//        message: '该项应为整数'
//    },
//    positiveInteger: {
//        validator: function (value) {
//            return /^[0-9]*[1-9][0-9]*$/.test(value);
//        },
//        message: '该项应为大于0的整数'
//    },
//    Negative: {
//        validator: function (value) {
//            return /^[0-9]*[0-9][0-9]*$/.test(value);
//        },
//        message: '该项应为非负整数'
//    },
//    decimal: {
//        validator: function (value) {
//            return /^(-?\d+)(\.\d+)?$/.test(value);
//        },
//        message: '该项应为数字'
//    },
//    ZIPCode: {
//        validator: function (value) {
//            return /^[1-9]\d{5}$/.test(value);
//        },
//        message: '邮政编码不存在'
//    },
//    AreaNum: {
//        validator: function (value) {
//            return /^\d{3,4}$/.test(value);
//        },
//        message: '区号不正确'
//    },
//    Tell: {
//        validator: function (value) {
//            return /^(\d{3,4}-)?\d{6,8}$/.test(value);
//        },
//        message: '电话号码格式不正确'
//    },
//    PHONENUMBER: {
//        validator: function (value) {
//            return /^[1]+[3,5,8]+\d{9}/.test(value);
//        },
//        message: '手机号码格式不正确'
//    },
//    extensionNumber: {
//        validator: function (value) {
//            return /^[0-9]*[1-9][0-9]*$/.test(value);
//        },
//        message: '分机号码格式不正确'
//    },
//    Fax: {
//        validator: function (value) {
//            return /^(\d{3,4}-)?\d{7,8}$/.test(value);
//        },
//        message: '传真号码格式不正确'
//    },
//    FaxextensionNumber: {
//        validator: function (value) {
//            return /^[0-9]*[1-9][0-9]*$/.test(value);
//        },
//        message: '传真分机号码格式不正确'
//    },

//    IncludeChinese: {
//        validator: function (value) {
//            return !/.*[\u4e00-\u9fa5]+.*$/.test(value);
//        },
//        message: '字符中不能包含中文'
//    },

//    MaxLength: {
//        validator: function (value, parm) {
//            return value.length <= parm[0];
//        },
//        message: '字符长度不能超过{0}位'
//    },

//    MinLength: {
//        validator: function (value, parm) {
//            return value.length >= parm[0];
//        },
//        message: '字符长度必须大于等于{0}位'
//    },
//    //combobox 必填
//    ComboRequired: {
//        validator: function (value, parm) {
//            if (parm == undefined) {
//                jqAlert('请指定 [ComboRequired] 验证参数。'); return;
//            }
//            if ($("#" + parm[0]).length == 0) {
//                jqAlert('错误的 [ComboRequired] 验证参数。'); return;
//            }
//            var value = $("#" + parm[0]).combobox('getValue');
//            if ("" == value)
//                return false;
//            var data = $("#" + parm[0]).combobox('getData');
//            var find = false;
//            for (var j = 0; j < data.length; j++) {
//                if (value == data[j].ID || value == data[j].Text) {
//                    find = true;
//                    break;
//                }
//            }
//            return find;
//        },
//        message: '必选项'
//    },
//    ComboTreeRequired: {
//        validator: function (value, parm) {
//            if (parm == undefined) {
//                jqAlert('请指定 [ComboTreeRequired] 验证参数。'); return;
//            }
//            if ($("#" + parm[0]).length == 0) {
//                jqAlert('错误的 [ComboTreeRequired] 验证参数。'); return;
//            }
//            if ($("#" + parm[0]).combotree('getValues') == '') {
//                return false;
//            }
//            return true;
//        },
//        message: '必选项'
//    },
//    DecimelThree: {
//        validator: function (value) {
//            return /(^-?(?:(?:\d{0,3}(?:,\d{3})*)|\d*))(\.(\d{1,3}))?$/.test(value);
//        },
//        message: '大于0且小数不能超过3位'
//    },
//    DecimelTwo: {
//        validator: function (value) {
//            return /^[0-9]+(.[0-9]{2})?$/.test(value);
//        },
//        message: '非负数，如果是有小数部分，必须是2位小数'
//    },
//    TwoCharacter: {
//        validator: function (value) {
//            return /^[A-Za-z0-9]{2}/.test(value);
//        },
//        message: '必须为2位数字或者字符组合'
//    },
//    TellAndPhone: {
//        validator: function (value) {
//            return /(^(\d{3,4}-)?\d{7,8}$)|(^1[2|3|4|5|6|7|8|9][0-9]\d{4,8}$)/.test(value);
//        },
//        message: '电话或者手机格式不正确'//既可以是电话号码也可以是手机号码，7到11位
//    },
//    Email: {
//        validator: function (value) {
//            return /^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$/.test(value);
//        },
//        message: '邮箱格式不正确'
//    },
//    SepcIntegerTheLength: {
//        validator: function (value, parm) {
//            if (!(/^-?\d+$/.test(value))) {
//                return false;
//            }

//            var v = parseInt(value);
//            if (v < 0) {
//                v = v * -1;
//            }
//            if (v.toString().length > parm) {
//                return false;
//            }

//            return true;
//        },
//        message: '输入的数值必须是长度不超过{0}位的整数'
//    }
//});


Array.prototype.isArray = function(v){
     
    return toString.apply(v) === '[object Array]';
     
}
var DataType = (function ($) {
    return {
        isString: function (v) {
            return this.toString.apply(v) === '[object String]';
        },
        isFunction: function (v) {
            return jQuery.isFunction(v);
        }
    };

})(jQuery);

 
//序列化
Array.prototype.Serialize = function () {
    var str = "";
    str += "[";
    for (var i = 0; i < this.length; i++) {
        str += "{";
        for (var o in this[i]) {
            var v = this[i][o];
            v = (v == '' || v == undefined || v == null || typeof (v) == "undefined") ? '\"\"' : v;
            str += "\"" + o + "\":\"" + this[i][o] + "\",";
        }
        if (str.substr(str.length - 1) == ",")
            str = str.substr(0, str.length - 1);
        str += "},";
    }
    if (str.substr(str.length - 1) == ",")
        str = str.substr(0, str.length - 1);
    str += "]";
    return str;
}


Array.prototype.toSpliceJsonStr = function () {
    var str = "[";
    for (var i = 0; i < this.length; i++) {
        str += "'" + this[i] + "',";
    }
    if (str.substr(str.length - 1) == ",")
        str = str.substr(0, str.length - 1);
    str += "]";
    return str;
}

Array.prototype.where = function (s) { return eval("(jQuery.grep(this, function (o, i){return " + s + ";}))"); }

//临时的分离新增和修改方法
var ViewMethodHelper = (function ($) {

    viewEventRegister: function viewEventRegister(setting) {
        if (ValidateHelper.isNullOrEmpty(setting))
            alert("请输入合适的参数");
        if (UrlHelper.GetUrlParam("ViewType") == viewType.AddView && !ValidateHelper.isNullOrEmpty(setting.AddViewEvent)) {
            setting.AddViewEvent();
        }
        else if (UrlHelper.GetUrlParam("ViewType") == viewType.EditView && !ValidateHelper.isNullOrEmpty(setting.EditViewEvent)) {
            setting.EditViewEvent();
        }
    };
    viewEventRegisterHandler: function viewEventRegisterHandler(setting) {
        if (ValidateHelper.isNullOrEmpty(setting) && ValidateHelper.isNullOrEmpty(setting.viewContainer) && setting.viewContainer.length > 0)
            alert("请输入合适的参数");
        for (var i = 0; i < setting.viewContainer.length; i++) {
            if (UrlHelper.GetUrlParam("ViewType") == setting.viewContainer[i].ViewType && !ValidateHelper.isNullOrEmpty(setting.viewContainer[i].ViewEvent)) {
                try{
                    setting.viewContainer[i].ViewEvent();
                }
                catch(err)
                {
                    alert(err);
                }
            }
        }
       
    };

    var viewType = { EditView: 'EditView', AddView: 'AddView' };
    return {
        viewType: viewType,
        viewEventRegister: viewEventRegister,
        viewEventRegisterHandler: viewEventRegisterHandler

    };
})(jQuery, ValidateHelper, UrlHelper);
 
var DataHelper = (function ($) {

    RemoveDuplicate: function RemoveDuplicate(inputData, validateData) {
        if (ValidateHelper.isNullOrEmpty(inputData) || ValidateHelper.isNullOrEmpty(inputData) || ValidateHelper.isNullOrEmpty(validateData)
            || ValidateHelper.isNullOrEmpty(inputData.validateKey) || ValidateHelper.isNullOrEmpty(validateData.validateKey)) {
            alert("请输入合适的参数");
            return;
        }
        if (inputData.validateKey.length != validateData.validateKey.length) {
            alert("inputData和validate的validateKey参数数量不一致");
            return;
        }
        var resultData = [];
        for (var intpuDataIndex = 0; intpuDataIndex < inputData.length; intpuDataIndex++) {
            var isValidateDuplicated = true;
            for (var validateDataIndex = 0; validateDataIndex < validateData.length; validateDataIndex++) {
                var isDuplicated = true;
                for (var validateKeyIndex = 0; validateKeyIndex < inputData.validateKey.length; validateKeyIndex++) {
                    if (!(inputData[intpuDataIndex][inputData.validateKey[validateKeyIndex]] === validateData[validateDataIndex][inputData.validateKey[validateKeyIndex]])) {
                        isDuplicated = false;
                        break;
                    }
                }
                isValidateDuplicated = isDuplicated;
            }
            if (!isDuplicated)
                resultData.push(inputData[intpuDataIndex]);
        }

    };

})(jQuery, ValidateHelper);





/*************duanming 2017-4-16*************/

//上移
function moveUpData(gridId) {

    /*****判断只能选中一行****/
    var rowsId = $("#" + gridId).jqGrid("getGridParam", "selarrrow"); //获取选中行
    if (rowsId.length == 0) {
        OpenTipWindowError("请选择需要上移的数据");
        return;
    }
    if (rowsId.length > 1) {
        OpenTipWindowError("只能选择一条上移的数据");
        return;
    }
    /************************/

    var rowId = $("#" + gridId).jqGrid('getGridParam', 'selrow');// 选中行

    var rowData = $("#" + gridId).jqGrid('getRowData', rowId); //获取选中行的值

    var lastId = $("#" + gridId + " #" + rowId).prev()[0].id;//获得选中行的上一行
    if (lastId == "") {//选中的是第一行无法上移
        OpenTipWindowError("第一行数据不允许上移");
        return;
    }

    $("#" + gridId).delRowData(rowsId);//删除选中的行

    $("#" + gridId).jqGrid("addRowData", rowId, rowData, "before", lastId);//插入数据到指定的位置

    $("#" + gridId).jqGrid("setSelection", rowId);//自动选中移动的行
}

//下移
function moveDownData(gridId) {

    /*****判断只能选中一行****/
    var rowsId = $("#" + gridId).jqGrid("getGridParam", "selarrrow"); //获取选中行
    if (rowsId.length == 0) {
        OpenTipWindowError("请选择需要下移的数据");
        return;
    }
    if (rowsId.length > 1) {
        OpenTipWindowError("只能选择一条上移的数据");
        return;
    }
    /************************/

    var rowId = $("#" + gridId).jqGrid('getGridParam', 'selrow');// 选中行

    var rowData = $("#" + gridId).jqGrid('getRowData', rowId); //获取选中行的值

    if ($("#" + gridId + " #" + rowId).next()[0] == undefined) {//选中的是最后一行无法上移
        OpenTipWindowError("最后一行数据不允许下移");
        return;
    }
    var nextId = $("#" + gridId + " #" + rowId).next()[0].id;//获得选中行的上一行


    $("#" + gridId).delRowData(rowsId);//删除选中的行

    $("#" + gridId).jqGrid("addRowData", rowId, rowData, "after", nextId);//插入数据到指定的位置

    $("#" + gridId).jqGrid("setSelection", rowId);//自动选中移动的行

}

//去掉重复数据
//array grid中选中的数据的数组
//col1 array中需要对比值的列key
//compareGridId 需要对比的grid的id
//col2 需要对比的grid列key
//返回值 没有重复值的数组
function DeleteDuplicateData(array, col1, compareGridId, col2) {

    var data = [];//去重复的数据

    var rows = $("#" + compareGridId).jqGrid("getRowData");//获得右边grid所有的行

    if (rows.length == 0) {//右边列表没有数据，直接返回数据集

        return array;

    }

    var isHasDuplicateData = false;//是否有重复的数据
    for (var i = 0; i < array.length; i++) {

        for (var j = 0; j < rows.length; j++) {
            if (array[i][col1] == rows[j][col2]) {
                isHasDuplicateData = true;
                break;
            }
        }
        if (!isHasDuplicateData) {//如果数据不在右边的列表中就加入到数组中
            data.push(array[i]);
        }
        isHasDuplicateData = false;
    }

    return data;

}


/***********************************************/