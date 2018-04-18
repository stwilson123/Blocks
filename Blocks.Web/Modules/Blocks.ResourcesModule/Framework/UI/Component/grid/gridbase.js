'use strict';
;define(['jquery', 'blocks_utility', 'jqGrid', '../dialog', './config/config','../../../../lib/jqGrid/script/i18n/grid.locale-cn'],function ($, utility, jqGrid, dialog,gridConfig) {


    var validate = utility.validate;
    var grid = function (option) {
        validate.mustUseNew(grid);
        this._eventsStore = $.extend(true, {},this.config.eventsStore);
        this._options = $.extend(true, this.config.default, option);
        //initEvent.call(this);
    };
    function initEvent() 
    {
        var gridOptions = this._options;
        var eventsStore = this._eventsStore;

        function eventFire(eventname) {

            gridOptions[eventname] = function () {
                var eventArray = eventsStore[eventname];
                for (var i = 0; i < eventArray.length; i++) {
                    eventArray[i].apply(this, arguments);
                }
            };
        }

        for(var eventname in this._eventsStore)
        {
            var sourceEvent = gridOptions[eventname];
            eventFire(eventname);
            if (sourceEvent)
                this.on(eventname, sourceEvent);
        }
    }
    
    grid.prototype.on = function (eventName, loadEventFun) {
        if ($.type(loadEventFun) !== 'function')
            throw new Error('loadEventFun must be function');
        var event = this._eventsStore[eventName];
        if (!event)
            throw new Error("eventName " + eventName + " can't implement");
 
        event.push(loadEventFun);
        
    };

    grid.prototype.config = gridConfig;

    grid.prototype.init = function () {
    }

    grid.prototype.afterInit = function () {
    }
    return grid;
     
});