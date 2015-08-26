using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.IO;

namespace WRADA_AUTH_MVC
{
    public class SessionModel
    {
        public enum WRADA_Roles { Guest, Provider, User, Administrator }

        public string WSession { get; set; }
        public string ClientIP { get; set; }
        public WRADA_Roles Role { get; set; }
        public string ID { get; set; }
        public string UserName { get; set; }
        public DateTime Validity { get; set; }
        public Dictionary<string, object> Data { get; set; }

        public object GetValue(string key)
        {
            return Data.ContainsKey(key) ? Data[key] : null;
        }

        public void SetValue(string key, object value)
        {
            if (Data.ContainsKey(key))
                Data[key] = value;
            else
                Data.Add(key, value);
        }

        public object this[string key]
        {
            get
            {
                return GetValue(key);
            }
            set
            {
                SetValue(key, value);
            }
        }

    }

   
    public class WRADA_AUTH_MVC
    {
        public const int ValidityHours = 6;
        private string wrada_cookie = "";
        public SessionModel CurrentCookie = null;
        string path_to_dir = HttpContext.Current.Server.MapPath("/WSessions/");

        public WRADA_AUTH_MVC(){


            if (System.IO.Directory.Exists(path_to_dir) == false)
            {
                System.IO.Directory.CreateDirectory(path_to_dir);
            }
            
            if (HttpContext.Current.Request.Cookies["WSession"] != null)
            {
                wrada_cookie = HttpContext.Current.Request.Cookies["WSession"].Value;
            }
            else
            {
                wrada_cookie = Guid.NewGuid().ToString();
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("WSession", wrada_cookie));
            }

            if (File.Exists(path_to_dir + wrada_cookie + ".wjs") == false)
            {
                CurrentCookie = new SessionModel()
                {
                    ClientIP = HttpContext.Current.Request.UserHostAddress,                    
                    WSession = wrada_cookie,
                    Role = SessionModel.WRADA_Roles.Guest,
                    ID = "",
                    UserName = "",
                    Validity = DateTime.Now.AddHours(ValidityHours), Data = new Dictionary<string,object>()
                };
                
                string string_CurrentCookie = Newtonsoft.Json.JsonConvert.SerializeObject(CurrentCookie);

                File.WriteAllText(path_to_dir + wrada_cookie + ".wjs", string_CurrentCookie, System.Text.Encoding.Default);

            }
            else
            {

                string string_CurrentCookie = File.ReadAllText(path_to_dir + wrada_cookie + ".wjs", System.Text.Encoding.Default);

                CurrentCookie = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionModel>(string_CurrentCookie);

                if (CurrentCookie.Validity < DateTime.Now)
                {
                    CurrentCookie = new SessionModel()
                    {
                        ClientIP = HttpContext.Current.Request.UserHostAddress,
                        WSession = wrada_cookie,
                        Role = SessionModel.WRADA_Roles.Guest,
                        ID = "",
                        UserName = "",
                        Validity = DateTime.Now.AddHours(ValidityHours),
                        Data = new Dictionary<string, object>()
                    };

                    string string_CurrentCookie2 = Newtonsoft.Json.JsonConvert.SerializeObject(CurrentCookie);

                    File.WriteAllText(path_to_dir + wrada_cookie + ".wjs", string_CurrentCookie2, System.Text.Encoding.Default);

                }

                if (CurrentCookie.ClientIP != HttpContext.Current.Request.UserHostAddress)
                {
                    CurrentCookie = new SessionModel()
                    {
                        ClientIP = HttpContext.Current.Request.UserHostAddress,
                        WSession = wrada_cookie,
                        Role = SessionModel.WRADA_Roles.Guest,
                        ID = "",
                        UserName = "",
                        Validity = DateTime.Now.AddHours(ValidityHours),
                        Data = new Dictionary<string, object>()
                    };

                    string string_CurrentCookie2 = Newtonsoft.Json.JsonConvert.SerializeObject(CurrentCookie);

                    File.WriteAllText(path_to_dir + wrada_cookie + ".wjs", string_CurrentCookie2, System.Text.Encoding.Default);

                }

                CurrentCookie.Validity = DateTime.Now.AddHours (ValidityHours);

                this.Save();

            }
            
        }

        public void Save()
        {

            string string_CurrentCookie = Newtonsoft.Json.JsonConvert.SerializeObject(CurrentCookie);

            File.WriteAllText(path_to_dir + wrada_cookie + ".wjs", string_CurrentCookie, System.Text.Encoding.Default);
            
        }

        public void Destroy()
        {
            if (File.Exists(path_to_dir + wrada_cookie + ".wjs"))
                File.Delete(path_to_dir + wrada_cookie + ".wjs");
            CurrentCookie = new SessionModel()
                {
                    ClientIP = HttpContext.Current.Request.UserHostAddress,                    
                    WSession = wrada_cookie,
                    Role =SessionModel.WRADA_Roles.Guest,
                    ID = "",
                    UserName = "",
                    Validity = DateTime.Now.AddHours(ValidityHours), Data = new Dictionary<string,object>()
                };

            string string_CurrentCookie = Newtonsoft.Json.JsonConvert.SerializeObject(CurrentCookie);

            File.WriteAllText(path_to_dir + wrada_cookie + ".wjs", string_CurrentCookie, System.Text.Encoding.Default);

        }

     

    }


    public class WRADA_Controller : System.Web.Mvc.Controller
    {
        public static WRADA_AUTH_MVC WAuth = new WRADA_AUTH_MVC();
        public SessionModel WUser = WAuth.CurrentCookie;
        protected override void Dispose(bool disposing)
        {
            WUser = null;
            GC.Collect();
            base.Dispose(disposing);
        }

      
       
    }

  
   
}
