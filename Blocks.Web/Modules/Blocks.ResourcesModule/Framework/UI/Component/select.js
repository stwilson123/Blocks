define(['jquery', 'blocks_utility', 'vueJS', 'select2', './dialog','./viewStruct/viewEvent','../../Event/event'], function ($, utility, vueJS, select2, dialog,viewEvent,event) {


    vueJS.directive('select2', {
        inserted: function (el, binding, vnode) {
            var options = binding.value || {};

            var defaultOpt = {
                placeholder: "--请选择--",
                allowClear: true
            };
            options = Object.assign(defaultOpt, options);
            $(el).select2(options);

            //绑定select2 dom渲染完毕时触发的事件
            options && options.onInit && options.onInit();
            for (var i = 0; i < vnode.data.directives.length; i++) {
                if (vnode.data.directives[i].name === "model") {
                    var vm = vnode.context;
                    var tag = vnode.data.directives[i].expression;
                    var currentEl = el;
                   

                    $(currentEl).select2().on('change', function () {
                        vm[tag] = this.value;
                    });
                 
                    vm.$watch(tag, function (newVal, oldVal) {
                        var selectObj = $(currentEl).data('blocksSelect');
                        var selectOptions = selectObj._options;
                        // utility.log.info(newVal + ' ' + oldVal);
                        // if (newVal != oldVal)
                        //     $(el).trigger('change');
                        if (selectOptions.isRemote && !selectObj.isLoaded)
                        {
                          
                            viewModelInit();
                        }
                        $(currentEl).trigger('change.select2');
                    });
               
                    var viewModelInit = function () {
                        var currentVal = vm[tag];
                        if (!currentVal)
                            return;
                        var Id = currentVal;
                        var dataSource = $(currentEl).select2('data');
                        var selectObj = $(currentEl).data('blocksSelect');
                        var selectOptions = selectObj._options;
                        if (!dataSource || dataSource.length < 1 ||  (dataSource.length ===0 &&  dataSource[0].id === ''))
                        {
                          
                            if (selectObj && selectOptions.url && selectOptions.isRemote)
                            {
                                selectObj.isLoaded = true;
                                utility.ajax.pubAjax({
                                    url: selectOptions.url,
                                    data: selectOptions.ajax.data.apply( $(currentEl),[{ isRemote:true,Id:Id}])
                                }).done(function (data) {
                                    var datas = data.content.rows;
                                    for ( var dataIndex in datas)
                                    {
                                        var newOption = new Option(datas[dataIndex].text, datas[dataIndex].id, true, true);
                                        $(currentEl).append(newOption).trigger('change');
                                    }
                                    console.log(datas);
                                }).always(function () {
                                    dialog.loading.close();
                                });
                               
                             
                                return;
                            }
                            else
                            {
                                $(currentEl).val(currentVal).trigger('change.select2');
                            }
                        }

                       
                    };
                    event.on('moduleInit.AjaxFinish',viewModelInit,true);
                   
                    if (vm[tag])
                    {
                        $(currentEl).val(vm[tag]).trigger('change.select2');
                        $(currentEl).trigger('change.select2');
                    }
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

    event.on('moduleInit',function () {
        $.when.apply($,ajaxCache).always(function () {
            console.log('ajaxCacheAlways');
            event.trigger('moduleInit.AjaxFinish');
        });
        console.log('ajaxCacheWhen');
    });
    var ajaxCache = [];
    var select2Fun = select2;
    var validate = utility.validate;
    
    var select = function (setting) {
       
        var options = $.extend({}, this.config.default, setting);
        validate.mustJQueryObj(options.viewObj, 'options.viewObj');
        this._options = options;
        
        viewEvent.call(this, {
            eventsStore: $.extend(true, {}, this.config.eventsStore),
            options: $.extend(true, {}, this.config.default, options),
            bindingEvent:function (eventName,callback) {
                options.viewObj.on(eventName,callback);
            }
        });
        // var selectObj = new select2Fun(options.viewObj, options);
        // this._options = selectObj;
       // this.initEvent();
    };
    utility.obj.inherit(viewEvent,select);
    //
    // select.prototype.on = function (eventName,eventCallback) {
    //     if ($.type(eventCallback) !== 'function')
    //         throw new Error('eventCallback must be function');
    //     var event = this._options.eventsStore[eventName];
    //     if (!event)
    //         throw new Error("eventName " + eventName + " can't implement");
    //     event.push(eventCallback);
    // };

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
        
        var $select =this._options.viewObj.select2(this._options);
        $select.data('blocksSelect',this);
        
       // this.initEvent();
    };
    select.prototype.config = {
        'default': {
            viewObj: undefined, placeholder: "请选择",
            multiple: false, allowClear: true, url: '', postData: undefined, isRmote: false,
            isCombobox: true,
            width: "100%",
        },
        eventsStore :{'select2:opening select2:closing': [],'change':[]  },
        'SelectLocal': {
            data: [{id: '', text: '请选择'}],
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
                    if (params.isRemote && params.Id)
                    {
                        queryParams.page.filters.rules.push({field: "Id", op: "eq", data: params.Id})
                    }
                    var postData = $.extend({},this.data("blocksSelect")._options.postData,queryParams);
                    return utility.Json.stringify(postData)
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
    select.prototype.data = function () {
        return this._options.viewObj.select2("data");
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
          var ajaxDef =  utility.ajax.pubAjax({
                url: options.url,
                data: utility.Json.stringify(queryParams)
            }).done(function (data) {
                var option = $.extend({}, options, {data: data.content.rows});
                dataInsertPlaceholder.call(selectThis, option.data);
                 options.viewObj.select2(option);
               console.log('ajaxdone');
           }).always(function () {
                dialog.loading.close();
              ajaxCache.splice(ajaxCache.indexOf(ajaxDef));
               console.log('ajaxalways');
            });
            ajaxCache.push(ajaxDef);
        }
        else {
            dataInsertPlaceholder.call(selectThis, options.data);
        }
        
        function dataInsertPlaceholder(data) {
            utility.validate.mustArray(data, "options.data");
            if (data[0].id !== '')
               data.unshift(this.config.SelectLocal.data[0]);
            
        }
    };
    localSelect.prototype.buildSearchbox = function (options) {
        if (!options.isCombobox)
            options.minimumResultsForSearch = Infinity;
    };
    

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
    };
    
   var selectFactory =function create(setting) {
       if (setting && setting.isRemote === true)
           return new remoteSelect(setting);
       else
           return new localSelect(setting);
   };
    return selectFactory;
});