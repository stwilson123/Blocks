define(['jquery'],function ($) {
    return function (i) {
        var curObj = $(this);
        if(!curObj.next().is('label'))
        {
            if (i === 0)
            {
                $("<label for='"+curObj.attr("id")+"'></label>").insertAfter(curObj)
            }
            else
                $("<label ></label>").insertAfter(curObj)
        }
    };
});