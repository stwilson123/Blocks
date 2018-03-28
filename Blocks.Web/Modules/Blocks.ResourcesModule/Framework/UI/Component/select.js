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
        options=  this.initOptions(options);
        select2Fun.call(this, options.viewObj, options);
    };
    utility.obj.inherit(select2Fun, select);
    select.prototype.config = {
        'default': {
            viewObj: undefined, placeholder: "请选择",
            multiple: false, allowClear: true, url: '', postData: undefined, isRmote: false,

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
        },

    };
    select.prototype.initOptions = function (options) {
        function localDataInit(selectOptions) {
            var options = selectOptions;
            options = $.extend({}, this.config.SelectLocal, options);
            var selectThis = this;
            if (options && options.url) {
                dialog.loading.open();

                var queryParams = $.extend({}, options.postData, {'page': options.page});
                utility.ajax.pubAjax({
                    url: options.url,
                    data: utility.Json.stringify(queryParams)
                }).done(function (data) {
                    var option = $.extend({}, options, {data: data.content.rows});
                    dataInsertPlaceholder.call(selectThis,option );
                    options.viewObj.select2(option);
                }).always(function () {
                    dialog.loading.close();
                });

                return;
            }

            function dataInsertPlaceholder(options) {

                if (options.data[0].id !== '')
                    options.data.unshift(this.config.SelectLocal.data[0]);
            }

            dataInsertPlaceholder.call(this, options);
            return options;
        }

        if (options.isRmote === false) {
            return localDataInit.call(this, options);
        }
        else {
            var option = $.extend({}, this.config.SelectRemote, options);
            option.ajax.url = options.url;
            return option;
        }
    }
    return select;
});