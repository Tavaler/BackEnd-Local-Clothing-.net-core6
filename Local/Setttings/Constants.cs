
namespace Local.Settings
{
    public class Constants
    {
        static public int FileSizeLimit = 10485760;
        static public string[] TypeImageForUploads = { ".jpg", ".png", ".jpeg", ".JPEG", ".jfif" };
        //static public string PathServer = "https://localhost:7123/images/";
        //static public string PathServer = "http://tee.kru.ac.th/cs63/s15/BackendLocal/images/";
        static public string PathServer = "http://10.103.0.15/cs63/s15/Local/Backend/images/";



        static public string NewIdImage() => uuid12() + DateTime.Now.ToString("yyyyMMdd HH:mm:ss.FFF");

        static public string DateId16() => DateTime.Now.Ticks.ToString().Substring(0, 8) + uuid8();
        static public string DateId24() => DateTime.Now.Ticks.ToString().Substring(0, 12) + uuid12();

        static public string uuid6() => Guid.NewGuid().ToString("N").Substring(0, 6);
        static public string uuid8() => Guid.NewGuid().ToString("N").Substring(0, 8);
        static public string uuid12() => Guid.NewGuid().ToString("N").Substring(0, 12);
        static public string uuid18() => Guid.NewGuid().ToString("N").Substring(0, 18);
        static public string uuid24() => Guid.NewGuid().ToString("N").Substring(0, 24);
        static public string uuid32() => Guid.NewGuid().ToString("N").Substring(0, 32);

        static public object Return200(string message) => new { statusCode = 200, message };
        static public object Return400(string message) => new { statusCode = 400, message };
        static public object Return200Data(string message, object data) => new { statusCode = 200, message, data };
        static public object Return400Data(string message, object data) => new { statusCode = 400, message, data };
        static public object Return200TotalData(string message,int total , object data) => new { statusCode = 200, message, total, data };
        static public object Return200PaginData(string message, object pagination, object data) => new { statusCode = 200, message, pagination, data  };
        static public object Return400PaginData(string message, object pagination, object data) => new { statusCode = 400, message, pagination, data  };
        static public object Return200Token(string message, string token) => new { statusCode = 200, message, token };
        static public object Return400Token(string message, string token) => new { statusCode = 400, message, token };
    }
}
