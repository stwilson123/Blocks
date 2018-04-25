define(function (grid) {
    var gridHead = function (gridObj) {
        gridObj._eventsStore['onheadcheck'] = [];
        inithead.call(this, gridObj);
        initheadCheck.call(this,gridObj);
        fixCheckAll.call(this,gridObj);

    };

    function inithead(gridObj) {
        var $gridObj = gridObj._options.gridObj;
        //显示列序号 
        if (gridObj._options.rownumbers) {
            gridObj.on('loadComplete', function () {
               // $gridObj.jqGrid('setLabel', 0, '序号', 'labelstyle',{"style":"width:44px"});
            });
        }
    };

    function initheadCheck(gridObj) {
        var gridOptions = gridObj._options;
        var gridID = gridObj._options.gridObj.attr('id');
        if (gridObj._eventsStore['onheadcheck'].length > 0) {
            gridObj.on('loadComplete', function () {
                gridOptions.gridObj.find('#cb_' + gridID).unbind('change', changeEventFun);
                gridOptions.gridObj.find('#cb_' + gridID).on('change', changeEventFun);
            });
        }

        function changeEventFun(e) {
            for (eventFun in gridObj._eventsStore['onheadcheck']) {
                gridObj._eventsStore['onheadcheck'][eventFun](gridID, false, e);
            }
        };
    };

    function fixCheckAll(gridObj) {
        var checkedAll = function (rowid, status) {
            var jqGridObj = $(this);
            var rowData = jqGridObj.jqGrid('getGridParam', 'selarrrow');
            if (rowData.length === jqGridObj[0].p.reccount) {
                jqGridObj.find('#cb_' + jqGridObj.attr('id')).prop("checked", 'true');

            }
        };
        gridObj.on('onSelectRow', checkedAll);
    };
    return gridHead;
});