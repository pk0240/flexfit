using System;

namespace DBConnection_Steppy_Giseppy.DBConn
{
    class AccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
        public string refresh_token { get; set; }
        public string user_id { get; set; }
    }
}