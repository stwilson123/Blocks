define(['jquery', '../../Event/event'], function ($, eventBus) {

    eventBus.on("moduleInit", function (view) {
        view.find('.form-control').focus(function () {
            $(this).parent().addClass('focused');
        });

        view.find('.form-control').focusout(function () {
            var $this = $(this);
            if ($this.parents('.form-group').hasClass('form-float')) {
                if ($this.val() == '') {
                    $this.parents('.form-line').removeClass('focused');
                }
            }
            else {
                $this.parents('.form-line').removeClass('focused');
            }
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
    return {};
});