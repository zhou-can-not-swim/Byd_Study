using BYDS7_2;
using BYDS7_2.plc_mst;
using BYDS7_2.utils;
using Microsoft.Extensions.Options;
using Microsoft.FSharp.Core;
using StdUnit.Sharp7.Options;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class PlcCtrl : S7PlcCtrl
{
    public PlcCtrl(string plcName, IOptionsMonitor<S7PlcOptItem> optionsMonitor) : base(plcName, optionsMonitor)
    {
    }


    /// <summary>
    /// 读取DEV
    /// </summary>
    /// <returns></returns>
    public async Task<FSharpResult<PlcMsg, string>> ReadPlcMsgAsync()
    {
        var bytes = await ReadDBAsync(PlcMsg.DB_INDEX, PlcMsg.DB_OFFSET, Marshal.SizeOf<PlcMsg>());
        if (bytes.IsError)
        {
            return FSharpResult<PlcMsg, string>.NewError(bytes.ErrorValue.ToString());
        }
        var bytesn = bytes.ResultValue;
        var plcmsg = MarshalHelper.BytesToStruct<PlcMsg>(bytesn);//test有值了
        return FSharpResult<PlcMsg, string>.NewOk(plcmsg);
    }


    /// <summary>
    /// 发送控制指令
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public async Task<FSharpResult<bool, string>> SendCmdAsync(MstMsg cmd)
    {
        var bytes = MarshalHelper.StructToBytes(cmd);
        FSharpResult<bool, string> res = await this.WriteDBAsync(MstMsg.DB_INDEX, MstMsg.DB_OFFSET, bytes);
        return res;
    }


}

