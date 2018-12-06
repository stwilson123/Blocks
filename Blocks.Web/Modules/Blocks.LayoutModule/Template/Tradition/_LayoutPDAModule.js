; define(/*['RequireConfig',"jquery", "blocks", "Blocks.LayoutModule/js/admin","Blocks.LayoutModule/js/main"],*/ function (req) {
    req(['RequireConfig'],function () {
        req(['jquery', 'blocks', 'Blocks.LayoutModule/js/admin', 'Blocks.LayoutModule/js/main'], function ($,blockFramework) {
            //Skin changer
            function skinChanger() {
                $('.right-sidebar .demo-choose-skin li').on('click', function() {
                    var currentTheme = $('.right-sidebar .demo-choose-skin li.active').data('theme');
                    $('.right-sidebar .demo-choose-skin li').removeClass('active');

                    var $selected = $(this);
                    $selected.addClass('active');
                    var selectedTheme = $selected.data('theme');

                    $('body')
                        .removeClass('theme-' + currentTheme)
                        .addClass('theme-' + selectedTheme);

                    //Change theme settings on the server
                    abp.services.app.configuration.changeUiTheme({
                        theme: selectedTheme
                    });
                });
            }

            //Skin tab content set height and show scroll
            function setSkinListHeightAndScroll() {
                var height = $(window).height() - ($('.navbar').innerHeight() + $('.right-sidebar .nav-tabs').outerHeight());
                var $el = $('.demo-choose-skin');

                $el.slimScroll({ destroy: true }).height('auto');
                $el.parent().find('.slimScrollBar, .slimScrollRail').remove();

                $el.slimscroll({
                    height: height + 'px',
                    color: 'rgba(0,0,0,0.5)',
                    size: '4px',
                    alwaysVisible: false,
                    borderRadius: '0',
                    railBorderRadius: '0'
                });
            }

            //Setting tab content set height and show scroll
            function setSettingListHeightAndScroll() {
                var height = $(window).height() - ($('.navbar').innerHeight() + $('.right-sidebar .nav-tabs').outerHeight());
                var $el = $('.right-sidebar .demo-settings');

                $el.slimScroll({ destroy: true }).height('auto');
                $el.parent().find('.slimScrollBar, .slimScrollRail').remove();

                $el.slimscroll({
                    height: height + 'px',
                    color: 'rgba(0,0,0,0.5)',
                    size: '4px',
                    alwaysVisible: false,
                    borderRadius: '0',
                    railBorderRadius: '0'
                });
            }

            //Activate notification and task dropdown on top right menu
            function activateNotificationAndTasksScroll() {
                $('.navbar-right .dropdown-menu .body .menu').slimscroll({
                    height: '254px',
                    color: 'rgba(0,0,0,0.5)',
                    size: '4px',
                    alwaysVisible: false,
                    borderRadius: '0',
                    railBorderRadius: '0'
                });
            }

            (function ($) {

                //Initialize BSB admin features
                $(function () {
                    skinChanger();
                    activateNotificationAndTasksScroll();

                    setSkinListHeightAndScroll();
                    setSettingListHeightAndScroll();
                    $(window).resize(function () {
                        setSkinListHeightAndScroll();
                        setSettingListHeightAndScroll();
                    });
                    
                    $('body').addClass('ls-closed');
                });

            })(jQuery);

         
            if (blocks.pageContext.subPageJsVirtualPath)
            {
             
                require([blockFramework.utility.url.pathToRelative(blocks.pageContext.subPageJsVirtualPath, blocks.pageContext.modulePrefix, '.js'), 'blocks'], function (containerModules, blocksJS) {
                    var localization = new blocksJS.localization();
                    localization.dictionary = blocks.localization;
                    containerModules.init({
                        view: $("#container"),
                        pageContext: $.extend(true, {}, blocks.pageContext),
                        localization: localization
                    });
                });

                //  require(['Blocks.BussnessWebModule/Views/MasterData/Index']);
            }
        });

    });
   
   
});
