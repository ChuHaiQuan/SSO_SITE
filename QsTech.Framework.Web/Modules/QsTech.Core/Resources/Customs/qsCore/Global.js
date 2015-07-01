/* ************************
* 事件对象，添加事件，触发事件
* 全局js  需要jquery  
* 示例：模态窗口确认事件（只需关注‘2.触发事件调用’;）
* 1.注册事件: modalConfirm.addListener(listener,scope); listener:执行动作；scope：作用域(需要时传入)
* 2.触发事件:modalConfirm.trigger(argument);argument:传入的参数(按需要传入)
* *************************** */
function qsEventObject(name) {
    this.name = name;
    this.eventstack = [];
}
qsEventObject.prototype = {
    trigger: function () {
        var len = this.eventstack.length;
        if (len != 0) {
            var eventData = this.eventstack[len - 1];
            eventData.listener.apply(eventData.scope, arguments);
            //this.eventstack = this.eventstack.slice(0, -1);
        }
    },
    addListener: function (listener, scope) {
        if ($.isFunction(listener)) {
            var eventData = {};
            eventData.listener = listener;
            eventData.scope = scope == undefined ? this : scope;
            this.eventstack.push(eventData);
        }
    },
    removeListener: function () { },
    outStack: function () {
        var len = this.eventstack.length;
        if (len != 0) {
            this.eventstack = this.eventstack.slice(0, -1);
        }
    }
};
var modalConfirm = new qsEventObject("modalConfirm");
var modalCancel = new qsEventObject("modalCancel");
