; define(['moment'], function (moment) {


    var dateConvert = {
        toUtcDate: function (date) {
            return moment.utc(date).toDate();
        },
        format: function (date, formatStr) {
            return moment(date).format(formatStr.replace(new RegExp("y", "gm"), 'Y'));
        }

    };
   
    return dateConvert;
});