define(function () {
    
    var viewEventRegister = function (setting) {
        this._eventsStore = setting.eventsStore;
        this._options = setting.options;
        initEvent.call(this);
    };
    function initEvent()
    {
        var gridOptions = this._options;
        var eventsStore = this._eventsStore;
        for(var eventname in this._eventsStore)
        {
            var sourceEvent = gridOptions[eventname];

            (function eventFire(eventname) {

                gridOptions[eventname] = function () {
                    var eventArray = eventsStore[eventname];
                    for (var i = 0; i < eventArray.length; i++) {
                        eventArray[i].apply(this, arguments);
                    }
                };
            })(eventname);

            if (sourceEvent)
                this.on(eventname, sourceEvent);
        }
    }

    viewEventRegister.prototype.on = function (eventName, loadEventFun) {
        if ($.type(loadEventFun) !== 'function')
            throw new Error('loadEventFun must be function');
        if (!eventName )
            throw new Error('eventName must be string');
        var eventsStore = this._eventsStore;
        $.each(eventName.split(' '),function (i,v) {
            var event = eventsStore[v];
            if (!event)
                throw new Error("eventName " + v + " can't implement");
            event.push(loadEventFun);
        });
       
      
    };
    return viewEventRegister;
});