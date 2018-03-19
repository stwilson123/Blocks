;define(['jquery','blocks_utility','magicsuggest'],function ($,utility) {
    var validate = utility.validate;
    var combobox = function (setting) {
        if (! this instanceof combobox)
            return null;
        beforeInit.call(this,setting );
        var option = $.extend(this.config.default,setting);
        option.dataUrlParams = $.extend({ comboxTopNum: option.maxSuggestions }, option.postData );
        option.data = setting.url ? setting.url : option.data;
        this._options = option;
        this._ms = option.viewObj.magicSuggest(option);

        afterInit.call(this,option);
        

    };
    
    combobox.prototype.config = {
        'default':{
            viewObj:undefined,
            resultsField: 'content',
            placeholder: '请选择',
            // dataUrlParams: setting.postData,
            // data: setting.url,
            queryParam: 'comboxQ',
            valueField: 'ID',
            maxSuggestions: 10,
            displayField: 'Text',
            //expandOnFocus: true,
            invalidCls: 'ms-inv',
            minChars: 3,
            noSuggestionText: '没有找到 {{queryParam}}',
            minCharsRenderer: function (v) {
                return '请输入超过' + 3 + '位字符';
            }
        }
        
    };
    
    var beforeInit = function (setting) {
        if (!setting || !setting.viewObj || validate.isNullOrEmpty(setting.viewObj.attr('id')))
            throw new Error("未指定的viewObj");
    };
    var afterInit = function (option) {
        var htmlId = this._options.viewObj.attr("id");
        var ms = this._ms;
        var viewObj = this._options.viewObj;
        $(ms).on('selectionchange', function (e, m) {

            var sels = this.getSelection();
            if (sels.length > 0)
            {
                var currentSel = sels[sels.length - 1];
                var isExist = false;
                $.each(this.getData(), function (index, value) {
                    if (value.ID === currentSel.ID)
                    {
                        isExist = true;
                        return false;
                    }
                });
                if (!isExist)
                    this.removeFromSelection([currentSel], true);
            }
            viewObj.find("#" + htmlId +" .ms-sel-ctn input:eq(0)").focus()
        });
        var sourceValid = ms.isValid;
        ms.isValid = function()
        {
            var result = sourceValid();
            if (option.required && !result)
                ms.container.addClass(option.invalidCls);
            return result;
        }

    };
    return combobox;
});