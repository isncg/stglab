# 事件系统
事件系统是发布订阅模式的实现。包括事件注册、反注册、派发三个主要操作。

事件实例负责特定事件的发布订阅处理，事件实例通过事件类型和参数类型来获得

Event<事件类型 [,参数类型]>