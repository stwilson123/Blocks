define(['../gridbase', 'blocks_utility','../extensions/checkboxPlugin'], function (grid, utility,checkboxHanlder) {
    var validate = utility.validate;
    var gridBody = function (gridObj) {
        this.lastsel;
        gridObj._options = $.extend(true, {}, this.config, gridObj._options);
        initColumn.call(this,gridObj);
        initSelectRow.call(this,gridObj);
        initCheckBox.call(this, gridObj);
        this.bodyVisile = true;

    };
    grid.prototype.getSelectRowIds = function () {
        var gridObj = this._options.gridObj;
        var result = [];
        if (!this._options.multiselect)
        {
            var selone = gridObj.jqGrid('getGridParam','selrow');
            if (selone)
                result.push(selone);
        }
        else 
        {
            result = gridObj.jqGrid('getGridParam','selarrrow').concat();
        }
        return result
    };
    
    function initColumn(gridObj) {
        var options= gridObj._options;
        if (validate.isNullOrEmpty(options.colModel) || validate.isNullOrEmpty(options.colNames))
            throw("Grid对象必须含有colModel和colNames属性");

        if (options.colModel.length !== options.colNames.length) {
            throw "colModel与colNames长度不一致";
        }

        for (var i = 0; i < options.colModel.length; i++) {
            if (validate.isNullOrEmpty(options.colModel[i].name)) {
                throw "colModel必须含有name属性";
            }

            if (validate.isNullOrEmpty(options.colModel[i].index)) options.colModel[i].index = options.colModel[i].name;
            options.colModel[i] = $.extend(true, {}, gridObj.config.body.default.colModel, options.colModel[i]);
        }
    }

    function initSelectRow(gridObj) {
        var gridOptions = gridObj._options;
        var gridBodyThis = this;
        var editSelectRow = function (rowid, status) {
            var gridObj = gridOptions.gridObj;
            if (!validate.isNullOrEmpty(gridOptions.onSelectRow && gridOptions.multiselectEdit === false)) {
                var lastsel = gridBodyThis.lastsel;
                if (rowid && rowid !== lastsel) {
                    if (lastsel)
                        gridObj.jqGrid("saveRow", lastsel, null, 'clientArray');
                    if (!validate.isNullOrEmpty(gridOptions.editParams))
                        gridObj.jqGrid('editRow', rowid, true, gridOptions.editParams.oneditfunc, null, 'clientArray', null, gridOptions.editParams.aftersavefunc);
                    else
                        gridObj.jqGrid('editRow', rowid, true);

                    gridBodyThis.lastsel = rowid;

                }
            }
        };
        gridObj.on('onSelectRow', editSelectRow);
    }
    
    function initCheckBox(gridObj) {
        if (gridObj._options.multiselect) {
            gridObj.on('loadComplete', function () {

                gridObj.getTopObj().find("input:checkbox").each(function (i) {
                    checkboxHanlder.call(this,i);
                   
                })

            });
        }
    }
    return gridBody;
});