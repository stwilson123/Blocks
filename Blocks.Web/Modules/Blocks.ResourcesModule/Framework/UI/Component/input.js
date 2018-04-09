define(['jquery', '../../Event/event'], function ($, eventBus) {

    var inputAnimation = {
        'focus': function () {
            $(this).parent().addClass('focused');
        },
        'focusout': function () {
            var $this = $(this);
            if ($this.parents('.form-group').hasClass('form-float')) {
                if ($this.val() == '') {
                    $this.parents('.form-line').removeClass('focused');
                }
            }
            else {
                $this.parents('.form-line').removeClass('focused');
            }
        },
    };
    eventBus.on("moduleInit", function (view) {
        view.find('.form-control:not(.date)').focus(function () {
            inputAnimation.focus.apply(this,arguments);
        });

        view.find('.form-control:not(.date)').focusout(function () {
            inputAnimation.focusout.apply(this, arguments);
        });

        view.find('input.form-control,textarea.form-control').each(function(){
            var $this =  $(this);
            if ($this.parents('.form-group').hasClass('form-float')) {
                if ($this.val() != '') {
                    $this.parents('.form-line').addClass('focused');
                }
            }
        });
         
    });

    eventBus.on("datepicker", function (view) {
        inputAnimation.focus.apply(view,arguments);
    });
    eventBus.on("datepickerhided", function (view) {
        inputAnimation.focusout.apply(view, arguments);
    });
    return {};
});