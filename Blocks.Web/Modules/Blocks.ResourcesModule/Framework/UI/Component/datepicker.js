;define(['jquery','./viewStruct/viewEvent', 'blocks_utility','../../Event/event','my97DatePicker'],function ($,viewEvent,utility,event) {

    "use strict";
    var eventBus = event;
   /* 1、默认日期格式: yyyy-MM-dd 其他格式请指定 DateFmt；
2、单个日期控件: 必要参数 pickerID：控件id ;
    非必要参数 startDate： 默认起始日期
    minDate： 静态限制作用的最小日期
    maxDate： 静态限制作用的最大日期 ；

3、两个日期控件: 必要参数 start、 end;
    非必要参数 minDate：起始控件最小日期
    maxDate：结束控件最大日期

    */
    var Datepicker = function (settings) {
        utility.validate.mustJQueryObj(settings.viewObj,"settings.viewObj");
        var pickerSettings = $.extend(true, {}, this.config.default, settings);
        if (pickerSettings.afterDatepicker &&  !pickerSettings.afterDatepicker instanceof Datepicker)
            throw new Error("pickerSettings.afterDatepicker must be Datepicker type.");
        viewEvent.call(this, {
            eventsStore: $.extend(true, {}, this.config.eventsStore),
            options: pickerSettings
        });
      
     
       this.init(pickerSettings);
        // //单个日期控件
        // var isSingle = !ValidateHelper.isNullOrEmpty(settings.pickerID);
        // if (isSingle) {
        //
        //     var picker = $('#' + settings.pickerID);
        //
        //   
        //     if (picker.length === 0) {
        //         alert("请指定正确的控件ID [" + settings.pickerID + "] !");
        //         return;
        //     };
        //
        // } else {
        //
        //     //两个日期控件 动态限制
        //     if (!ValidateHelper.isNullOrEmpty(settings.start) && !ValidateHelper.isNullOrEmpty(settings.end)) {
        //
        //         var startPicker = $('#' + settings.start);
        //
        //         if (startPicker.length === 0) {
        //             alert("请指定正确的控件ID [" + settings.start + "] !");
        //
        //             return
        //         };
        //         var endPicker = $('#' + settings.end);
        //
        //         if (endPicker.length === 0) {
        //
        //             alert("请指定正确的控件ID [" + settings.end + "] !");
        //             return
        //         };
        //
        //         startPicker.focus(function () { });
        //         endPicker.focus(function () { });
        //
        //         startPicker.attr('tabindex', '-1');
        //         endPicker.attr('tabindex', '-1');
        //         if (!startPicker.hasClass('Wdate')) startPicker.addClass('Wdate');
        //         if (!endPicker.hasClass('Wdate')) endPicker.addClass('Wdate');
        //
        //         var startPickerMaxDate = '#F{$dp.$D(\'' + settings.end + '\')';
        //         if (!ValidateHelper.isNullOrEmpty(settings.maxDate)) {
        //             startPickerMaxDate += '||\'' + settings.maxDate + '\'';
        //         }
        //         startPickerMaxDate += "}";
        //
        //         var startPickerSettings = jQuery.extend({
        //             maxDate: startPickerMaxDate
        //         }, pickerSettings);
        //
        //         if (!ValidateHelper.isNullOrEmpty(settings.minDate)) {
        //             startPickerSettings.minDate = settings.minDate;
        //         }
        //
        //         var endPickerMinDate = '#F{$dp.$D(\'' + settings.start + '\')}';
        //
        //         var endPickerSettings = jQuery.extend({
        //             minDate: endPickerMinDate
        //         }, pickerSettings);
        //
        //         if (!ValidateHelper.isNullOrEmpty(settings.maxDate)) {
        //             endPickerSettings.maxDate = settings.maxDate;
        //         }
        //
        //         //修改单击文本框日期面板不弹出的问题
        //         endPicker.click(function () {
        //             WdatePicker(endPickerSettings);
        //         });
        //
        //         startPicker.click(function () {
        //             WdatePicker(startPickerSettings);
        //         });
        //
        //     }
        // }
 
    }
    utility.obj.inherit(viewEvent,Datepicker);
    Datepicker.prototype.config = {
      'default':  {
          skin: 'twoer',
          dateFmt: 'yyyy/MM/dd HH:mm:ss',
          readOnly:true,
          // startDate: undefined,
          // minDate: undefined,
          // maxDate: undefined,
          afterDatepicker:undefined,
          autoPickDate:true,
          onpicked:function () {
              //$($dp.el).trigger('input');
              utility.nativeJs.trigger($dp.el,'input');
              eventBus.trigger("datepickered", $($dp.el));
          },
          onhided:function () {
              eventBus.trigger("datepickerhided", $(this.el));
          }
      },
      'eventsStore':{'onpicked':[],'onpicking':[],'oncleared':[],'onclearing ':[],'onhided':[]}
    };

    Datepicker.prototype.init = function (option) {
        var datePickerSetting = option;
        var picker = option.viewObj;
        picker.attr('tabindex', '-1');

        if (!picker.hasClass('Wdate')) picker.addClass('Wdate date');

        //picker.click(function () {
        //    WdatePicker(pickerSettings);
        //});
        picker.on('click',function () {
            if (datePickerSetting.afterDatepicker)
                datePickerSetting.maxDate = datePickerSetting.afterDatepicker.val();
            datePickerSetting.el = this;
            WdatePicker(datePickerSetting);
            try {
                eventBus.trigger("datepicker", $(this));
            } finally {

            }
        });
    };
    Datepicker.prototype.val = function (value) {
        if (!value)
            this._options.viewObj.val();
    };
    
    return Datepicker;
});