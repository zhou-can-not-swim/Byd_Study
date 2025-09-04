using BYDS7_2;
using Microsoft.Extensions.Options;
using Microsoft.FSharp.Core;
using Sharp7;


namespace StdUnit.Sharp7.Options;

public abstract class S7PlcCtrl : IDisposable
{
    protected S7Client Client { get; set; }
    protected readonly string _plcName;
    protected readonly IOptionsMonitor<S7PlcOptItem> _optionsMonitor;

    public virtual string Name => _plcName;

    public S7PlcCtrl(string plcName, IOptionsMonitor<S7PlcOptItem> optionsMonitor)
    {
        if (string.IsNullOrEmpty(plcName))
        {
            throw new ArgumentException("'plcName' cannot be null or empty", "plcName");
        }

        _plcName = plcName;
        _optionsMonitor = optionsMonitor;
        Client = new S7Client();
    }

    public virtual void Dispose()
    {
        if (Client != null && Client.Connected)
        {
            try
            {
                Client.Disconnect();   //断开连接
            }
            catch (Exception)
            {
                // 忽略断开连接时的异常
            }
        }
    }

    public virtual bool Connect()
    {
        try
        {
            var options = _optionsMonitor.Get(_plcName);
            int result = Client.ConnectTo(options.IpAddr, options.Rack, options.Slot);
            return result == 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public virtual void Disconnect()
    {
        if (Client != null && Client.Connected)
        {
            Client.Disconnect();
        }
    }

    public virtual void EnsureConnected(bool force = false)
    {
        if (force || Client == null || !Client.Connected)
        {
            if (Client == null)
            {
                Client = new S7Client();
            }

            var options = _optionsMonitor.Get(_plcName);
            int result = Client.ConnectTo(options.IpAddr, options.Rack, options.Slot);

            if (result != 0)
            {
                throw new Exception($"连接PLC失败，错误代码: {result}");
            }
        }
    }

    protected async Task<FSharpResult<byte[], string>> ReadDBAsync(int DB, int start, int size)
    {
        return await Task.Run(() =>
        {
            try
            {
                byte[] buffer = new byte[size];
                int result = Client.DBRead(DB, start, size, buffer);
                Console.WriteLine("得到的完整数据: " + BitConverter.ToString(buffer));//将字节数组转换成16进制字符串
                if (result == 0)
                {
                    return FSharpResult<byte[], string>.NewOk(buffer);
                }
                else
                {
                    return FSharpResult<byte[], string>.NewError($"读取DB失败，错误代码: {result}");
                }
            }
            catch (Exception ex)
            {
                return FSharpResult<byte[], string>.NewError($"读取异常: {ex.Message}");
            }
        });
    }

    protected async Task<FSharpResult<bool, string>> WriteDBAsync(int DB, int start, byte[] buffer)
    {
        return await Task.Run(() =>
        {
            try
            {
                int result = Client.DBWrite(DB, start, buffer.Length, buffer);
                
                if (result == 0)
                {
                    return FSharpResult<bool, string>.NewOk(true);
                }
                else
                {
                    return FSharpResult<bool, string>.NewError($"写入DB失败，错误代码: {result}");
                }
            }
            catch (Exception ex)
            {
                return FSharpResult<bool, string>.NewError($"写入异常: {ex.Message}");
            }
        });
    }
}