;define(['moment'],function (moment) {
    
    
    var dateConvert = {
      toUtcDate:function (date) {
          return moment.utc(date).toDate();;
      }  
    };
    
    return dateConvert;
});