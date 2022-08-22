using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0425_WCF
{
    interface IImage
    {
        byte[] GetPicture(string strFileName);
        string[] GetPictureList();
        bool UploadPicture(string strFileName, byte[] bytePic);

    }
}
