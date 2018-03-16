({
    baseUrl: './Framework/',
    mainConfigFile: './config.js',
    dir: './build_home/',
    // removeCombined: true,
    modules: [
        {
            name: './blocks',//吧以下内容压缩到vendor.js下面，原文件进行压缩，源文件位置不变并不删除
            include: [],
            exclude: [
                'jquery',
                // 'abp_wrapper',
                // 'blocks_UI',
                // 'blocks_security',
                // 'blocks_utility',
                'bootstrap',
                'bootstrap_select',
                'jqGrid',
                'jquery',
                'jquery_blockUI',
                'jquery_spin',
                'jquery_validate',
                'json2',
                'layer',
                'moment',
                'push',
                'slimscroll',
                'spin',
                'sweetalert',
                'toastr',
                'vueJS',
                'waves',
                // 'Blocks.ResourcesModule/lib/abp-web-resources/Abp/Framework/scripts/abp',
                // 'Blocks.ResourcesModule/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.toastr',
                // 'Blocks.ResourcesModule/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.blockUI',
                // 'Blocks.ResourcesModule/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.spin',
                // 'Blocks.ResourcesModule/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.sweet-alert'
            ]
        }]
})

