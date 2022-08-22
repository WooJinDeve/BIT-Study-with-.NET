using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0426_ImageWCFService
{
    //서비스객체
    class WbImage : IImage
    {
        public byte[] GetPicture(string strFileName)
        {
            return Image.GetPicture(strFileName);
        }

        public string[] GetPictureList()
        {
            return Image.GetPictureList();
        }

        public bool UploadPicture(string strFileName, byte[] bytePic)
        {
            return Image.UploadPicture(strFileName, bytePic);
        }
    }
}
