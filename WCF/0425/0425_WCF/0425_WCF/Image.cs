using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace _0425_WCF
{
    public static class Image
    {
        private static string Path = @"c:\aaa\image\";

        public static byte[] GetPicture(string strFileName)
        {
            byte[] bytePic = { 0 }; // 바이트 배열을 하나 만든다.
            try
            {
                // 해당 이미지 파일을 스트림 형식으로 오픈한다.
                FileStream picFileStream = new FileStream(Path + strFileName, FileMode.Open, FileAccess.Read, FileShare.Read);

                // 이미지 파일 스트림을 읽을 객체를 하나 만든다.
                BinaryReader picReader = new BinaryReader(picFileStream);
                // 이미지 파일을 바이트 배열에 넣는다.
                //bytePic = picReader.ReadBytes(int.Parse(picFileStream.Length.ToString()));
                bytePic = picReader.ReadBytes(Convert.ToInt32(picFileStream.Length));
                // 파일스트림을 닫는다.
                picFileStream.Close();
                // 이미지 파일이 들어있는 바이트 배열을 리턴한다.
                return bytePic;
            }
            catch 
            {
                // 초기값을 그냥 리턴한다.
                return bytePic;
            }
        }

        public static string[] GetPictureList()
        {
            // 이미지 파일이 들어있는 디렉토리에서 파일 이름들을 문자열 배열에 넣는다.
            string[] strPicList = Directory.GetFiles(Path);

            // 파일 경로를 뺀 파일 이름만 다시 추출한다.
            for (int i = 0; i < strPicList.Length; i++)
            {
                FileInfo fi = new FileInfo(strPicList[i]);
                strPicList[i] = fi.Name;
            }
            // 이 이미지 파일 이름들을 리턴한다.
            return strPicList;
        }

        public static bool UploadPicture(string strFileName, byte[] bytePic)
        {
            try
            {
                // 주어진 이미지 파일의 이름으로 파일을 하나 만든다.
                FileStream writeFileStream = new FileStream(Path + strFileName, FileMode.Create, FileAccess.Write);

                // 이 파일에 바이너리를 넣기 위해 BinaryWriter 객체 생성
                BinaryWriter picWriter = new BinaryWriter(writeFileStream);
                // 바이트 배열로 받은 이미지를 파일에 쓴다.
                picWriter.Write(bytePic);
                // 파일스트림을 닫는다.
                writeFileStream.Close();
                // 업로드 성공
                return true;
            }
            catch
            {
                // 업로드 실패
                return false;
            }
        }
    }
}