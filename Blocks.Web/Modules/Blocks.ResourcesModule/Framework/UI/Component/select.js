define(['jquery', 'blocks_utility', 'vueJS', 'select2', './dialog'], function ($, utility, vueJS, select2, dialog) {


    vueJS.directive('select2', {
        inserted: function (el, binding, vnode) {
            var options = binding.value || {};

            var defaultOpt = {
                placeholder: "--请选择--",
                allowClear: true
            };
            options = Object.assign(defaultOpt, options);
            $(el).select2(options);


            // $(el).select2(options).on("select2:select", function (e) {
            //     // v-model looks for
            //     //  - an event named "change"
            //     //  - a value with property path "$event.target.value"
            //     el.dispatchEvent(new Event('change', {target: e.target})); //双向绑定不生效
            //     //绑定选中选项的事件
            //     options && options.onSelect && options.onSelect(e);
            // });
            // $(el).select2(options).on("select2:select", function (e) {
            //     utility.log.info(e);
            // });
            // //allowClear:清除选中
            // $(el).select2(options).on("select2:unselecting", function (e) {
            //     //清空这个值，这个值即vuejs model绑定的值
            //     e.target.value = "";
            //     el.dispatchEvent(new Event('change', {
            //         target: e.target
            //     })); //双向绑定不生效
            // });

            //绑定select2 dom渲染完毕时触发的事件
            options && options.onInit && options.onInit();
            for (var i = 0; i < vnode.data.directives.length; i++) {
                if (vnode.data.directives[i].name === "model") {
                    var vm = vnode.context;
                    var tag = vnode.data.directives[i].expression;
                    var currentEl = el;

                    $(currentEl).val(vm[tag]).trigger('change.select2');

                    $(currentEl).select2().on('change', function () {
                        vm[tag] = this.value;
                    });
                    vm.$watch(tag, function (newVal, oldVal) {
                        // utility.log.info(newVal + ' ' + oldVal);
                        // if (newVal != oldVal)
                        //     $(el).trigger('change');
                        $(currentEl).trigger('change.select2');
                    });
                }
            }

        },
        update: function (el, binding, vnode) {

            // $(el).trigger("change");
        },
        componentUpdated: function (el, binding, vnode) {
            // $(el).trigger("change");
        },
    });


    var select2Fun = select2;
    var validate = utility.validate;
    var select = function (setting) {
       
        var options = $.extend({}, this.config.default, setting);
        validate.mustJQueryObj(options.viewObj, 'options.viewObj');
        this._options = options;

        // var selectObj = new select2Fun(options.viewObj, options);
        // this._options = selectObj;
       // this.initEvent();
    }; 
    select.prototype.on = function (eventName,eventCallback) {
        if ($.type(eventCallback) !== 'function')
            throw new Error('eventCallback must be function');
        var event = this._options.eventsStore[eventName];
        if (!event)
            throw new Error("eventName " + eventName + " can't implement");
        event.push(eventCallback);
    };

    select.prototype.initEvent = function () {
        for (eventsTemp in this._options.eventsStore) {
            var currentEvents = this._options.eventsStore[eventsTemp];
            if (currentEvents.length > 0) {
                var callbackWrapper = function (event) {
                    for (currentEventIndex in currentEvents) {
                        currentEvents[currentEventIndex].apply(this, arguments)
                    }
                }
                this._options.viewObj.on(eventsTemp, callbackWrapper);
            }
        }


    };
    select.prototype.load = function () {
        
        this._options.viewObj.select2(this._options);
        this.initEvent();
    };
    select.prototype.config = {
        'default': {
            viewObj: undefined, placeholder: "请选择",
            multiple: false, allowClear: true, url: '', postData: undefined, isRmote: false,
            isCombobox: true,
            width: "100%",
            eventsStore :{'select2:opening select2:closing': []}
            
        },
        'SelectLocal': {
            data: [{id: '', text: ''}],
            page: {pageSize: -1, page: 1}
        },
        'SelectRemote': {
            page: {pageSize: 10, page: 1},
            ajax: {
                url: "",
                dataType: 'json',
                type: 'POST',
                delay: 50,
                contentType: 'application/json',
                data: function (params) {
                    var queryParams = {
                        page: {
                            page: params.page, pageSize: 10, filters: {
                                groupOp: "AND", rules: [{field: "Text", op: "cn", data: params.term}]
                            }
                        }//当前页  
                    };
                    return utility.Json.stringify(queryParams)
                },
                minimumInputLength: 2,// 最少输入一个字符才开始检索
                processResults: function (data, params) {
                    params.page = params.page || 1;
                    var content = data.content;
                    return {
                        results: data.content.rows,
                        pagination: {
                            more: (params.page * 10) < data.content.pagerInfo.records
                        }
                    };
                },
                cache: true
            }
        }

    };

    var localSelect = function (setting) {
        select.call(this, setting);
        this._options = $.extend({}, this.config.SelectLocal, this._options);
        this.loadData(this._options);
        this.buildSearchbox(this._options);
        this.load();
    };
    utility.obj.inherit(select, localSelect);
    localSelect.prototype.loadData = function (options) {

        var selectThis = this;
        if (options && options.url) {
            dialog.loading.open();
           
            var queryParams = $.extend({}, options.postData, {'page': options.page});
            utility.ajax.pubAjax({
                url: options.url,
                data: utility.Json.stringify(queryParams)
            }).done(function (data) {
                var option = $.extend({}, options, {data: data.content.rows});
                dataInsertPlaceholder.call(selectThis, option.data);
                options.viewObj.select2(option);
            }).always(function () {
                dialog.loading.close();
            });
        }
        else {
            dataInsertPlaceholder.call(selectThis, options.data);
        }
        function dataInsertPlaceholder(data) {
            utility.validate.mustArray(data, "options.data")
            if (data[0].id !== '')
                data.unshift(this.config.SelectLocal.data[0]);
        }
    }
    localSelect.prototype.buildSearchbox = function (options) {
        if (!options.isCombobox)
            options.minimumResultsForSearch = Infinity;
    }

    var remoteSelect = function (setting) {
        select.call(this, setting);
        this._options = $.extend({}, this.config.SelectRemote, this._options);
        this._options.ajax.url = this._options.url;
        this.buildSearchbox(this._options);
        this.load();
    };
    utility.obj.inherit(select, remoteSelect);
    remoteSelect.prototype.buildSearchbox = function (options) {
        if (!options.isCombobox) {
            options.minimumResultsForSearch = Infinity;
            // this.on('select2:opening select2:closing', function (event) {
            //     var $searchfield = $(this).parent().find('.select2-search--dropdown');
            //     $searchfield.css('display', 'none');
            // });
        }
    }
    
   var selectFactory =function create(setting) {
       if (setting && setting.isRemote === true)
           return new remoteSelect(setting);
       else
           return new localSelect(setting);
   }
    return selectFactory;
});