namespace BYDS7_2.utils
{
    public struct ApiErrorCode
    {
        public short OsSocketError;

        public IsoTcpErrors IsoTcpError;

        public short S7Error;

        public override string ToString()
        {
            return $"OsSockerError={OsSocketError};IsoTcpError={IsoTcpError};S7Error={S7Error}";
        }
    }
    public enum IsoTcpErrors : byte
    {
        None,
        ConnectionError,
        DisconnectionError,
        MalformattedPDUSuppled,
        BadDatasizePassedToSendRecv,
        NullPointerSupplied,
        ShortPacketReceived,
        TooManyFragments,
        PduOverflow,
        SendPacketError,
        RecvPacketError,
        InvalidParams,
        Resvd1,
        Resvd2,
        Resvd3,
        Resvd4
    }
}
