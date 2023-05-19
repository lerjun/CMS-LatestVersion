namespace AuthSystem.Manager
{
    public class DBConn
    {
        private static string GetConnectionString()
        {
            // Return My.Settings.ConnString.ToString
            //  return "Data Source=192.168.0.84,36832;Initial Catalog=EQMS;User ID=randy;Password=otik"; //test
            //return "Data Source=192.168.0.222,36832;Initial Catalog=EQMS;User ID=randy;Password=otik"; //live
            //return "Data Source=LERJUN-PC;Initial Catalog=AOPCDB;User ID=test;Password=1234"; //live
            return "Data Source=EC2AMAZ-AN808JE\\MSSQLSERVER01;Initial Catalog=AOPCDB;User ID=test;Password=1234"; //server
        }
        private static string GetHttpString()
        {
            // return "ec2-35-79-24-148.ap-northeast-1.compute.amazonaws.com"; // live AWS
            // return "http://172.31.17.67"; // live
           //return "http://ec2-43-204-237-103.ap-south-1.compute.amazonaws.com:8082"; // live AOPC
       return "https://api.alfardanoysterprivilegeclub.com"; // live AOPC
            // return "https://43.204.237.103"; // live ALFARDAN 
            //  return "http://192.168.100.90"; // local 
           
        // return "https://localhost:7110"; // local live
        }
         
        private static string GetPath()
        {
           //return    "C:\\Files\\";
          return "C:\\inetpub\\AOPCAPP\\public\\assets\\img\\";
        }

        public static string ConnectionString
        {
            get
            {
                return GetConnectionString();
            }
        }
        public static string Path
        {
            get
            {
                return GetPath();
            }
        }
        public static string HttpString
        {
            get
            {
                return GetHttpString();
            }
        }
    }
}
