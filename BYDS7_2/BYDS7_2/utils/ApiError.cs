using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYDS7_2.utils
{
    public struct ApiError
    {
        public string DevName { get; set; }

        public ApiErrorCode Error { get; set; }

        public string Text { get; set; }

        public override string ToString()
        {
            return $"{DevName}: {Text}：{Error}";
        }
    }
}
