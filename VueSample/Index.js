//import Vue from 'vue'
//import App1 from 'App2'

Vue.config.productionTip = false
var App1 = Vue.component('async-example', function (resolve, reject) {
    setTimeout(function () {
        // 将组件定义传入 resolve 回调函数
        resolve({
            template: '<div>I am async!<script src="https://code.jquery.com/jquery-1.11.3.js"></script></div>'
        })
    }, 1000)
});
/* eslint-disable no-new */
new Vue({
    el: '#app',
    //template: '<div/>',
    //components: { App1 }
})
//Vue.component('my-component', {
//    template: '<div>A custom component!</div>'
//})

//// 创建根实例
//new Vue({
//    el: '#app'
//})