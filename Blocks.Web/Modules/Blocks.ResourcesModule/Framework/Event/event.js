;define(function () {
    var _callbacks = {};

    var on = function (eventName, callback,isOnce) {
        var events = eventName.split(" ");
        events.forEach(function (event) {
            if (!_callbacks[event]) {
                _callbacks[event] = [];
            }

            _callbacks[event].push({ callback:callback,isOnce:isOnce});
        })
         
    };

    var off = function (eventName, callback) {
        var callbacks = _callbacks[eventName];
        if (!callbacks) {
            return;
        }

        var index = -1;
        for (var i = 0; i < callbacks.length; i++) {
            if (callbacks[i].callback === callback) {
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
        var OnceArray = [];
        for (var i = 0; i < callbacks.length; i++) {
            var eventCallback = callbacks[i];
            if (eventCallback.isOnce)
                OnceArray.push(i);
            callbacks[i].callback.apply(this, args);
      }
        for (var OnceArrayIndex in OnceArray) {
            callbacks.splice(OnceArray[OnceArrayIndex], 1);
        }
    };

    // Public interface ///////////////////////////////////////////////////

    return {
        on: on,
        off: off,
        trigger: trigger
    };
});