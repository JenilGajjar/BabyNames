using BabyNames.Areas.SEC_User.Models;
using BabyNames.BAL;
using BabyNames.DAL.SEC_UserDAL;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;

namespace BabyNames.Areas.SEC_User.Controllers
{
    [Area("SEC_User")]
    [Route("SEC_User/[controller]/[action]")]
    public class SEC_UserController : Controller
    {
        #region IWebHostEnvironment

        private readonly IWebHostEnvironment _webHostEnvironment;

        public SEC_UserController( IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        #endregion


        #region SEC_UserSignIn
        public IActionResult SEC_UserSignIn()
        {
            return View("Login");
        }
        #endregion

        #region SEC_UserSignUp
        public IActionResult SEC_UserSignUp()
        {
            return View("Register");
        }
        #endregion


        [HttpPost]
        public IActionResult Login(SEC_UserModel modelSEC_User)
        {
            if (modelSEC_User.UserName == null)
            {
                TempData["UserNameError"] = "User Name is Required!";
            }

            if (modelSEC_User.Password == null)
            {
                TempData["PasswordError"] = "Password is Required!";
            }


            if (modelSEC_User.UserName == null || modelSEC_User.Password == null)
            {
                return RedirectToAction("SEC_UserSignIn", "SEC_User");
            }
            else
            {
                SEC_UserDALBase sEC_UserDALBase = new SEC_UserDALBase(_webHostEnvironment);


                DataTable dt = sEC_UserDALBase.PR_SEC_UserSelectByUserNamePassword(modelSEC_User.UserName, modelSEC_User.Password);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        HttpContext.Session.SetString("FirstName", dr["FirstName"].ToString());
                        HttpContext.Session.SetString("LastName", dr["LastName"].ToString());
                        HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        HttpContext.Session.SetString("PhotoPath", dr["PhotoPath"].ToString());
                        HttpContext.Session.SetString("IsAdmin", dr["IsAdmin"].ToString());
                        break;
                    }
                }

                else
                {
                    TempData["Error"] = "UserName or Password is invalid!";
                    return RedirectToAction("SEC_UserSignIn");
                }

                if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
                {
                    if (HttpContext.Session.GetString("IsAdmin") == "True")
                    {
                        return RedirectToAction("Index", "SEC_Admin", new { area = "SEC_Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "BabyName", new { area = "BabyName" });
                    }
                }

            }
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return View("Login");
        }
       
        public IActionResult Register(SEC_UserModel modelSEC_User)
        {

            if (modelSEC_User.UserName == null)
            {
                TempData["UserNameError"] = "User Name is Required!";
            }
            if (modelSEC_User.Password == null)
            {
                TempData["PasswordError"] = "Password is Required!";
            }
            if (modelSEC_User.FirstName == null)
            {
                TempData["FirstNameError"] = "First  Name is Required!";
            }
            if (modelSEC_User.LastName == null)
            {
                TempData["LastNameError"] = "Last Name is Required!";
            }
            if (modelSEC_User.Email == null)
            {
                TempData["EmailAddressError"] = "Email Address is Required!";
            }

            if (TempData["UserNameError"] != null || TempData["PasswordError"] != null || TempData["FirstNameError"] != null || TempData["LastNameError"] != null || TempData["EmailAddressError"] != null)
            {
                return RedirectToAction("SEC_UserSignUp", "SEC_User");
            }
            else
            {
                SEC_UserDALBase sEC_UserDALBase = new SEC_UserDALBase(_webHostEnvironment);
                bool IsSuccess = sEC_UserDALBase.SEC_User_Register(modelSEC_User);
                if (IsSuccess)
                {
                    return RedirectToAction("SEC_UserSignIn");
                }
                else
                {
                    TempData["InvalidUserNameError"] = "User Name is Already Taken";
                    return RedirectToAction("SEC_UserSignUp");
                }
            }
        }


    }
}
