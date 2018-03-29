; define(['jquery','json2'], function (jQuery,json2) {


    
    var validateDefine = function () {
        this.isNullOrEmpty = function isNullOrEmpty(v) {
            return v === '' || v == undefined || v == null || typeof (v) == "undefined";

        };
        this.isDecimal = function isDecimal(v) {
            return /^(-?\d+)(\.\d+)?$/.test(v);
        };
        this.isHtml = function isHtml(v) {
            return new RegExp('^<([^>\s]+)[^>]*>(.*?<\/\\1>)?$').test(v);
        };
        this.isInt = function isInt(v) {
            return /^[+]{0,1}(\d+)$/.test(v)
        };
        this.isErrorCode = function isErrorCode(code) {
            return (code != 100 && code < 3000) || code > 10000;
        };
        this.mustFunction = function mustFunction(object, paramName) {
            if (!this.isFunction(object))
                throw new Error(paramName + " must be function");
        };
        this.mustNotNullAndEmpty = function mustNotNullAndEmpty(object, paramName) {
            if (!this.isNullOrEmpty(object))
                throw new Error(paramName + " must be NotNull And NotEmpty");
        };

        this.mustArray = function mustArray(object, paramName) {
            if (!this.isArray(object))
                throw new Error(paramName + " must be Array");
        }
        this.mustJQueryObj = function mustJQueryObj(object, paramName) {
            if (this.isNullOrEmpty(object) || !object.attr || this.isNullOrEmpty(object.attr("id")))
                throw new Error(paramName + " must be JQueryObj");
        };
        this.isFunction = function isFunction(object) {
            return $.isFunction(object);
        };
        this.isArray = function isArray(object) {
            return $.isArray(object);
        }

    };
    var ValidateHelper = new validateDefine();

    var ajax = {};
    ajax.pubAjax = function (userOptions) {
        userOptions = userOptions || {};

        var options = $.extend({}, ajax.config.default, userOptions);
        options.url = options.url ? UrlHelper.GetRandURL(options.url) :options.url;

        return $.Deferred(function ($dfd) {
            $.ajax(options)
                .done(function (data, textStatus, jqXHR) {
                    ajax.handleResponse(data,options,$dfd, jqXHR);
                    $dfd.resolve(data);
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    userOptions.onFailCallBack && userOptions.onFailCallBack(jqXHR, textStatus, errorThrown);
                    $dfd.reject(jqXHR);
                })
                //data|jqXHR, textStatus, jqXHR|errorThrown
                .always(function (obj, textStatus, jqXHR) {
                    userOptions.onCompleteCallBack && userOptions.onCompleteCallBack(obj, textStatus, jqXHR);
                    //$dfd.resolve(jqXHR);
                });
        });
    };
    ajax.config = {
        'default': {
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json',
            async: true, //同步
            cache: false,
            beforeSend: function(){
                // Handle the beforeSend event
            }
        }

    };
    ajax.handleResponse = function (data, userOptions, $dfd, jqXHR) {
        userOptions && userOptions.onSuccessCallBack&& userOptions.onSuccessCallBack(data);
    };
    // var AjaxHelper = (function ($) {
    //     pubAjax = function (settings) {
    //         if (settings.url != "" && settings.url != null) {
    //             settings.url = UrlHelper.GetRandURL(settings.url);
    //             settings = jQuery.extend({
    //                 errorHandle: true, //是否处理服务器错误 error.call
    //                 onFailCallBack: function () { },
    //                 type: "post",
    //                 contentType: "application/x-www-form-urlencoded; charset=UTF-8",
    //                 data: settings.data,
    //                 datatype: ValidateHelper.isNullOrEmpty(settings.url) ? 'json' : settings.url,
    //                 async: false, //同步
    //                 cache: false,
    //                 statusCode: {
    //                     404: function () { alert('无效的URL'); }
    //                 },
    //                 beforeSend: function (XMLHttpRequest) {
    //                     this;
    //                 },
    //                 complete: function (XMLHttpRequest, Status) {
    //                     this;
    //                 },
    //                 success: function (data) {
    //                     this;
    //                 },
    //                 error: function (XMLHttpRequest, Status, errorThrown) {
    //                     this;
    //                 },
    //                 ajaxError: function () { }
    //             }, settings);
    //         } else {
    //             alert('请指定URL');
    //             return;
    //         }
    //
    //         var btnObj = null;
    //         var currentWindow = null;
    //
    //         if (!ValidateHelper.isNullOrEmpty(settings.buttonEvent)) {
    //             //ie and firefox
    //             if ($.browser.msie || $.browser.mozilla) btnObj = $(settings.buttonEvent.target);
    //             //chrome
    //             else btnObj = $(settings.buttonEvent.currentTarget);
    //         }
    //
    //         if (!ValidateHelper.isNullOrEmpty(settings.currentWindow)) {
    //             currentWindow = settings.currentWindow;
    //         }
    //
    //         jQuery.ajax({
    //             type: settings.type,
    //             url: settings.url,
    //             cache: settings.cache,
    //             sync: settings.async,
    //             contentType: settings.contentType,
    //             data: settings.data,
    //             datatype: settings.datatype,
    //             statusCode: settings.statusCode,
    //             success: function (data) {
    //                 if (settings.datatype === 'text/html') {
    //                     settings.onSuccessCallBack(data);
    //                     return;
    //                 }
    //                 if (!ValidateHelper.isNullOrEmpty(data)) {
    //                     var result = eval('(' + data + ')');
    //                     ajaxSuccess(result, settings.onSuccessCallBack, settings.onFailCallBack);
    //                     return;
    //                 }
    //                
    //               
    //                 ajaxSuccess({ msg: "Data from server is null.Please check response.", code: 901 }, settings.onSuccessCallBack, settings.onFailCallBack);
    //
    //             },
    //             error: function (XMLHttpRequest, Status, errorThrown) {
    //                 if (settings.errorHandle) {
    //                     settings.error(XMLHttpRequest, Status, errorThrown);
    //
    //
    //                 } else {
    //                     ;
    //                 }
    //                 if (null != btnObj) btnObj.button('enable'); //启用按钮 
    //                 if (null != currentWindow) {
    //                     UnLockWindow(currentWindow); //解锁窗口
    //                     HidePrompt(currentWindow);
    //                 }
    //             },
    //             beforeSend: function (XMLHttpRequest) {
    //                 settings.beforeSend(XMLHttpRequest);
    //
    //                 if (null != btnObj) btnObj.button('disable'); //禁用按钮 
    //                 if (null != currentWindow) {
    //                     LockWindow(currentWindow); //锁定窗口
    //                     ShowPrompt(currentWindow);
    //                 }
    //             },
    //             complete: function (XMLHttpRequest, Status) {
    //                 settings.complete(XMLHttpRequest, Status);
    //
    //                 if (null != btnObj) btnObj.button('enable'); //启用按钮 
    //                 if (null != currentWindow) {
    //                     UnLockWindow(currentWindow); //解锁窗口
    //                     HidePrompt(currentWindow);
    //                 }
    //             }
    //         });
    //     };
    //
    //
    //     
    //     //submitJSONForm: function submitJSONForm(setting) {
    //     //    var option = $.extend(setting, {});
    //
    //     //    //if (!EasyUiValidate(option.htmlFormId)) {
    //     //    //    return false;
    //     //    //}
    //
    //     //    var vPostData = FormHelper.getPostBodyToJson(option.htmlFormId);
    //     //    if ($.type(vPostData) == "string") {
    //     //        if (vPostData == "nodata")
    //     //            return;
    //     //    }
    //     //    var extendSetting = $.extend(true, {
    //     //        postJsonData: vPostData
    //     //    }, setting);
    //     //    pubAjax({
    //     //        url: setting.URL,
    //     //        data: extendSetting.postJsonData,
    //     //        type: "POST",
    //     //        currentWindow: option.currentWindow,
    //     //        buttonEvent: option.btnEvent,
    //     //        errorMessageAlertCallBack: option.onFailCallBack,
    //     //        onSuccessCallBack: function (msg, data, code) {
    //     //            extendSetting.onSuccessCallBack(msg, data, code);
    //     //        },
    //     //        error: function (XMLHttpRequest, Status, errorThrown) {
    //
    //     //            extendSetting.onFailCallBack(XMLHttpRequest, Status, errorThrown);
    //     //        },
    //     //        complete: function (XMLHttpRequest, Status) {
    //     //        }
    //     //    });
    //     //};
    //     //submitJSONFormWithLoadDialog: function submitJSONFormWithLoadDialog(setting) {
    //     //    // ShowLoading();//显示loading
    //     //    var onSuccessCallBackFun = setting.onSuccessCallBack;
    //     //    var onFailCallBackFun = setting.onFailCallBack;
    //
    //
    //     //    var option = $.extend(setting, {
    //     //        onSuccessCallBack: function (msg, content, code) {
    //
    //     //            if (ValidateHelper.isErrorCode(code)) {
    //     //                OpenTipWindowError(MessageHelper.getRemoveSpecialChar(msg));
    //     //                try {
    //     //                    if (DataType.isFunction(onFailCallBackFun))
    //     //                        onFailCallBackFun(msg, content, code);
    //     //                }
    //     //                catch (e) {
    //     //                    throw e;
    //     //                }
    //     //                finally {
    //     //                    // CloseLoading();
    //
    //     //                }
    //     //                return;
    //     //            }
    //     //            try {
    //     //                onSuccessCallBackFun(msg, content, code);
    //     //            }
    //     //            catch (e) {
    //     //                throw e;
    //     //            }
    //     //            finally {
    //     //                // CloseLoading();
    //
    //     //            }
    //     //        },
    //     //        onFailCallBack: function (XMLHttpRequest, Status, errorThrown) {
    //     //            OpenTipWindowError("意外出错，请刷新页面重试。");
    //
    //     //            try {
    //     //                if (DataType.isFunction(onFailCallBackFun))
    //     //                    onFailCallBackFun(Status, XMLHttpRequest, errorThrown);
    //     //            }
    //     //            catch (e) {
    //     //                throw e;
    //     //            }
    //     //            finally {
    //     //                //   CloseLoading();
    //
    //     //            }
    //     //        }
    //     //    });
    //
    //     //    submitJSONForm(option);
    //     //};
    //     LockWindow: function LockWindow(o) {
    //         if (isNullOrEmpty(o)) return;
    //         var wo = null;
    //
    //         if ($.type(o) == "string") {
    //             wo = $("#" + o);
    //         }
    //         if ($.type(o) == "object") {
    //             wo = o;
    //         }
    //         if (0 < wo.length) {
    //             wo.data('_Dialog_CanClose', false);
    //         }
    //     };
    //
    //     UnLockWindow: function UnLockWindow(o) {
    //         if (isNullOrEmpty(o)) return;
    //         var wo = null;
    //
    //         if ($.type(o) == "string") {
    //             wo = $("#" + o);
    //         }
    //         if ($.type(o) == "object") {
    //             wo = o;
    //         }
    //         if (0 < wo.length) {
    //             wo.data('_Dialog_CanClose', true);
    //         }
    //     };
    //
    //     ShowPrompt: function ShowPrompt(o) {
    //         if (isNullOrEmpty(o)) return;
    //         var wo = null;
    //
    //         if ($.type(o) == "string") {
    //             wo = $("#" + o);
    //         }
    //         if ($.type(o) == "object") {
    //             wo = o;
    //         }
    //         if (0 < wo.length) {
    //             var html = "<div class='post_tipp' style='width:86px;height:6px;float:left;margin:1.2em 0 0.5em 0.4em'><img src=\"/Images/ajaxtipimage.gif\" /></div>";
    //             $("div.ui-dialog[role='dialog'][aria-describedby='" + wo.attr('id') + "']").find('.ui-dialog-buttonpane').append(html);
    //         }
    //     };
    //
    //     HidePrompt: function HidePrompt(o) {
    //         if (isNullOrEmpty(o)) return;
    //         var wo = null;
    //
    //         if ($.type(o) == "string") {
    //             wo = $("#" + o);
    //         }
    //         if ($.type(o) == "object") {
    //             wo = o;
    //         }
    //         if (0 < wo.length) {
    //             $("div.ui-dialog[role='dialog'][aria-describedby='" + wo.attr('id') + "']").find('.post_tipp').remove();
    //         }
    //     };
    //
    //
    //     ajaxSuccess: function ajaxSuccess(result, callback, jqAlertCallBack) {
    //       
    //         if (!ValidateHelper.isNullOrEmpty(callback))
    //             callback(result.msg, result.content, result.code);
    //
    //     }
    //     // return { pubAjax: pubAjax, submitJSONForm: submitJSONForm, submitJSONFormWithLoadDialog: submitJSONFormWithLoadDialog };
    //     return { pubAjax: pubAjax,  };
    //
    // })(jQuery, ValidateHelper, UrlHelper, MessageHelper);


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

        pathToRelative: function pathToRelative(path,modulePrefix,fileExtensionName) {
            var moduleFrefix=modulePrefix;
            var startIndex = path.indexOf(moduleFrefix);
            var endIndex = path.lastIndexOf(fileExtensionName);

            return path.slice(startIndex > -1 ? startIndex + moduleFrefix.length + '\\'.length : 0,endIndex > -1 ? endIndex : undefined);
        };
        return {
            GetRandURL: GetRandURL,
            setUrlParam: setUrlParam,
            GetUrlParam: GetUrlParam,
            pathToRelative: pathToRelative
        };

    })(jQuery, MathHelper);
    var MathHelper = (function ($) {
        //获取随机数
        GetRandomNum: function GetRandomNum(Min, Max) {
            var Range = Max - Min;
            var Rand = Math.random();
            return (Min + Math.round(Rand * Range));
        };
        return { GetRandomNum: GetRandomNum };
    })(jQuery);
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

    var cookie = {};
    cookie.getCookieValue = function (key) {
        var equalities = document.cookie.split('; ');
        for (var i = 0; i < equalities.length; i++) {
            if (!equalities[i]) {
                continue;
            }

            var splitted = equalities[i].split('=');
            if (splitted.length != 2) {
                continue;
            }

            if (decodeURIComponent(splitted[0]) === key) {
                return decodeURIComponent(splitted[1] || '');
            }
        }

        return null;
    };




    /* LOGGING ***************************************************/
    //Implements Logging API that provides secure & controlled usage of console.log

    var log =  {};

    log.levels = {
        DEBUG: 1,
        INFO: 2,
        WARN: 3,
        ERROR: 4,
        FATAL: 5
    };

    log.level = log.levels.DEBUG;

    log.log = function (logObject, logLevel) {
        if (!window.console || !window.console.log) {
            return;
        }

        if (logLevel != undefined && logLevel < log.level) {
            return;
        }

        console.log(logObject);
    };

    log.debug = function (logObject) {
        log.log("DEBUG: ", log.levels.DEBUG);
        log.log(logObject, log.levels.DEBUG);
    };

    log.info = function (logObject) {
        log.log("INFO: ", log.levels.INFO);
        log.log(logObject, log.levels.INFO);
    };

    log.warn = function (logObject) {
        log.log("WARN: ", log.levels.WARN);
        log.log(logObject, log.levels.WARN);
    };

    log.error = function (logObject) {
        log.log("ERROR: ", log.levels.ERROR);
        log.log(logObject, log.levels.ERROR);
    };

    log.fatal = function (logObject) {
        log.log("FATAL: ", log.levels.FATAL);
        log.log(logObject, log.levels.FATAL);
    };
    
    var obj = {
        inherit:function (parent,child) {
            ValidateHelper.mustFunction(parent,'parent');
            ValidateHelper.mustFunction(child,'child');

            (function(){
                // 创建一个没有实例方法的类
                var Super = function(){};
                Super.prototype = parent.prototype;
                //将实例作为子类的原型
                child.prototype = new Super();
            })();
        }
        
    };
    //TODO JSON2 not work
    return { validate: ValidateHelper,ajax:ajax,url:UrlHelper,cookie:cookie,log:log,obj:obj,Json:JSON }
});