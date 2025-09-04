using BYDS7_2;
using BYDS7_2.plc_mst;
using BYDS7_2.test;
using Microsoft.FSharp.Core;

var options = new S7PlcOptItem();
var simpleMonitor = new SimpleOptionsMonitor<S7PlcOptItem>(options);
var plc = new PlcCtrl("西门子", simpleMonitor);


plc.Connect();



#region 读取PLC消息
// 读取PLC消息
//FSharpResult<PlcMsg, string> result =await plc.ReadPlcMsgAsync();
#endregion

#region 发送指令
// 创建 MstMsg 实例
var msg = new MstMsg();

// 只发送心跳请求
msg.GeneralCmdWord = MstMsg.MstFlags_GeneralCmdWord.心跳请求;

// 设置预留1（假设这是一个字节类型的预留字段）
msg.预留1 = 0x01; // 你可以设置任何你需要的值

await plc.SendCmdAsync(new MstMsg(
    msg
));

FSharpResult<PlcMsg, string> result =await plc.ReadPlcMsgAsync();
#endregion

