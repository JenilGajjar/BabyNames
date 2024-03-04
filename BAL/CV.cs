namespace BabyNames.BAL
{
    public class CV
    {
        private static IHttpContextAccessor _HttpContextAccessor;

        static CV()
        {
            _HttpContextAccessor = new HttpContextAccessor();
        }


        public static int? UserID()
        {
            //Initialize the UserID with null
            int? UserID = null;
            //check if session contains specified key?
            //if it contains then return the value contained by the key.
            if (_HttpContextAccessor.HttpContext.Session.GetString("UserID") != null)
            {
                UserID =
               Convert.ToInt32(_HttpContextAccessor.HttpContext.Session.GetString("UserID").ToString());

            }
            Console.WriteLine(UserID);
            return UserID;
        }

        public static string? Password()
        {
            //Initialize the Password with null
            string? Password = null;
            //check if session contains specified key?
            //if it contains then return the value contained by the key.
            if (_HttpContextAccessor.HttpContext.Session.GetString("Password") != null)
            {
                Password = (_HttpContextAccessor.HttpContext.Session.GetString("Password").ToString());

            }
            Console.WriteLine(Password);
            return Password;
        }


        public static string? UserName()
        {
            string? UserName = null;
            if (_HttpContextAccessor.HttpContext.Session.GetString("UserName") != null)
            {
                UserName =
               _HttpContextAccessor.HttpContext.Session.GetString("UserName").ToString();
            }

            return UserName;
        }

        public static string? FirstName()
        {
            string? FirstName = null;
            if (_HttpContextAccessor.HttpContext.Session.GetString("FirstName") != null)
            {
                FirstName =
               _HttpContextAccessor.HttpContext.Session.GetString("FirstName").ToString();
            }

            return FirstName;
        }

        public static string? LastName()
        {
            string? LastName = null;
            if (_HttpContextAccessor.HttpContext.Session.GetString("LastName") != null)
            {
                LastName =
               _HttpContextAccessor.HttpContext.Session.GetString("LastName").ToString();
            }

            return LastName;
        }

        public static string? Email()
        {
            string? Email = null;
            if (_HttpContextAccessor.HttpContext.Session.GetString("Email") != null)
            {
                Email =
               _HttpContextAccessor.HttpContext.Session.GetString("Email").ToString();
            }

            return Email;
        }


        public static bool IsAdmin()
        {
            //Initialize the UserID with null
            bool IsAdmin = false;
            //check if session contains specified key?
            //if it contains then return the value contained by the key.
            if (_HttpContextAccessor.HttpContext.Session.GetString("IsAdmin") != null)
            {
                IsAdmin = Convert.ToBoolean(_HttpContextAccessor.HttpContext.Session.GetString("IsAdmin").ToString());

            }
            Console.WriteLine("Admin" + IsAdmin);
            return IsAdmin;
        }

        public static string? PhotoPath()
        {
            string? ProfilePicture = null;
            if (_HttpContextAccessor.HttpContext.Session.GetString("PhotoPath") != null)
            {
                ProfilePicture =
               _HttpContextAccessor.HttpContext.Session.GetString("PhotoPath").ToString();
            }
            return ProfilePicture;
        }


        public static DateTime? Created()
        {
            //Initialize the UserID with null
            DateTime? Created = null;
            //check if session contains specified key?
            //if it contains then return the value contained by the key.
            if (_HttpContextAccessor.HttpContext.Session.GetString("Created") != null)
            {
                Created =
               Convert.ToDateTime(_HttpContextAccessor.HttpContext.Session.GetString("Created").ToString());

            }
            Console.WriteLine(Created);
            return Created;
        }


    }
}
