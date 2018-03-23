define(['jquery', 'blocks_utility','vueJS','select2'], function ($, utility,vueJS,select2) {


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
                    vm.$watch(tag,function (newVal,oldVal) {
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
        
        var options = $.extend({},this.config.default,setting);
        validate.mustJQueryObj(options.viewObj,'options.viewObj');
        if (options.isRmote === false)
        {
            options =  $.extend({}, this.config.SelectLocal,options);
            if (options.data[0].id !== '')
                options.data.unshift(this.config.SelectLocal.data[0]);
        }
        else {
            options =  $.extend({}, this.config.SelectRemote,options);

        }
        select2Fun.call(this, options.viewObj,options);
    };
    utility.obj.inherit(select2Fun,select);
    select.prototype.config = {
        'default': {
            viewObj: undefined, placeholder: "请选择",
            multiple: false, allowClear: true, url: '', postData: undefined, isRmote: false
        },
        'SelectLocal': {
            data: [{id: '', text: ''}]
        },
        'SelectRemote': {
            ajax: {
                url: "",
                dataType: 'json',
                delay: 50,
                data: function (params) {
                    return {
                        q: params.term, // 查询参数  
                        rows: 10,//页面大小  
                        page: params.page//当前页  
                    };
                },
                minimumInputLength : 2,// 最少输入一个字符才开始检索
                processResults: function (data, params) {
                    params.page = params.page || 1;

                    return {
                        results: data.items,
                        pagination: {
                            more: (params.page * 10) < data.total_count
                        }
                    };
                },
                cache: true
            }
        },
         
    };
 
    return select;
});