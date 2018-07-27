;define(function () {
    var _callbacks = {};

    var on = function (eventName, callback) {
        var events = eventName.split(" ");
        events.forEach(function (event) {
            if (!_callbacks[event]) {
                _callbacks[event] = [];
            }

            _callbacks[event].push(callback);
        })
         
    };

    var off = function (eventName, callback) {
        var callbacks = _callbacks[eventName];
        if (!callbacks) {
            return;
        }

        var index = -1;
        for (var i = 0; i < callbacks.length; i++) {
            if (callbacks[i] === callback) {
                index = i;
                break;
            }
        }

        if (index < 0) {
            return;
        }

        _callbacks[eventName].splice(index, 1);
    };

    var trigger = function (eventName) {
        var callbacks = _callbacks[eventName];
        if (!callbacks || !callbacks.length) {
            return;
        }

        var args = Array.prototype.slice.call(arguments, 1);
        for (var i = 0; i < callbacks.length; i++) {
            callbacks[i].apply(this, args);
        }
    };

    // Public interface ///////////////////////////////////////////////////

    return {
        on: on,
        off: off,
        trigger: trigger
    };
});