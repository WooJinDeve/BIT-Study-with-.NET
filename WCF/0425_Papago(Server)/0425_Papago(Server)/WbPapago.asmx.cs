using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _0425_Papago_Server_
{
    /// <summary>
    /// WbPapago의 요약 설명입니다.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // ASP.NET AJAX를 사용하여 스크립트에서 이 웹 서비스를 호출하려면 다음 줄의 주석 처리를 제거합니다. 
    // [System.Web.Script.Services.ScriptService]
    public class WbPapago : System.Web.Services.WebService, IPapago
    {

        private Papago papago = new Papago();

        [WebMethod]
        public string OutputText(string in_lan, string out_lan, string intput_text)
        {
            return papago.OutputText(in_lan, out_lan, intput_text);
        }
    }
}
