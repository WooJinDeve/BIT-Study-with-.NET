using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_과제.이벤트처리
{
    /// <summary>
    /// 계좌생성시 사용되는 대리자
    /// </summary>
    internal delegate void AddAccountEvent(object obj,
        AddAccountEventArgs e);

    internal delegate void AddIOEvent(object obj,
        AddIOEventArgs e);

}
