using BabyNames.Areas.BabyName.Models;
using BabyNames.Areas.SEC_User.Models;
using BabyNames.BAL;
using BabyNames.DAL.Baby_DAL;
using BabyNames.DAL.SEC_UserDAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
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

        Baby_DALBase baby_DALBase = new Baby_DALBase();

        #region Index
        public IActionResult Index()
        {
            return View();
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

        #region Login

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
                        Console.WriteLine(dr["PhotoPath"]);
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
                        return RedirectToAction("Index", "SEC_User", new { area = "SEC_User" });
                    }
                }

            }
            return View();
        }

        #endregion

        #region Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return View("Index");
        }
        #endregion

        #region Register
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
        #endregion

        #region BabyList
        public IActionResult BabyList()
        {
            try
            {

                ViewBag.ZodiacFilterList = baby_DALBase.ZodiacFilter();
                ViewBag.ReligionFilterList = baby_DALBase.ReligionFilter();
                ViewBag.NakshatraFilterList = baby_DALBase.NakshatraFilter();




                ViewBag.FavoriteStatus = baby_DALBase.PR_GetFavoriteStatus_For_SEC_User();

                List<BabyModel> babyList = baby_DALBase.PR_SELECT_ALL_BabyNames();



                return View(babyList);

            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Filter

        public IActionResult BabyFilter(BabyFilterModel babyfiltermodel)
        {

            Console.WriteLine(babyfiltermodel.Gender);

            ViewBag.ZodiacFilterList = baby_DALBase.ZodiacFilter();
            ViewBag.ReligionFilterList = baby_DALBase.ReligionFilter();
            ViewBag.NakshatraFilterList = baby_DALBase.NakshatraFilter();

            ViewBag.FavoriteStatus = baby_DALBase.PR_GetFavoriteStatus_For_SEC_User();

            List<BabyModel> list = baby_DALBase.PR_BabyNames_FILTER(babyfiltermodel);


            ModelState.Clear();

            return View("BabyList", list);

        }
        #endregion

        #region UserProfile
        public IActionResult UserProfile()
        {
            SEC_UserDALBase userDAL = new SEC_UserDALBase(_webHostEnvironment);

            int UserID = (int)CV.UserID();
            SEC_UserModel modelSEC_User = userDAL.PR_SEC_UserSelectByUserName(UserID);

            return View(modelSEC_User);
        }

        #endregion

        #region User Update
        public IActionResult UserUpdate(SEC_UserModel sEC_UserModel)
        {


            sEC_UserModel.UserID = (int) CV.UserID();
            SEC_UserDALBase userDAL = new SEC_UserDALBase(_webHostEnvironment);
            sEC_UserModel.Password = sEC_UserModel.Password ?? CV.Password();
            userDAL.PR_SEC_User_UPDATEByPK(sEC_UserModel);


            return View("Index");

        }
        #endregion

        #region Change Password
        [HttpPost]
        public IActionResult ChangePassword(string NewPassword)
        {

            // For example:
            SEC_UserDALBase sEC_UserDALBase = new SEC_UserDALBase(_webHostEnvironment);

            int UserID = (int)CV.UserID()!;
            

            sEC_UserDALBase.PR_SEC_User_CHANGE_PASSWORD(UserID, NewPassword);
            return View("Index");
        }
        #endregion

        #region  UpdateFavoriteStatus

        [HttpPost]
        public IActionResult UpdateFavoriteStatus(int NameId, bool IsFavorite)
        {
            // Get the current user ID
            int UserId = (int) CV.UserID();

            // Call the appropriate stored procedure based on isFavorite parameter
            if (IsFavorite)
            {
                // If isFavorite is true, call the procedure to select the favorite name
                baby_DALBase.PR_SelectFavoriteName(UserId, NameId);
            }
            else
            {
                // If isFavorite is false, call the procedure to unselect the favorite name
                baby_DALBase.PR_UnselectFavoriteName(UserId, NameId);
            }

            // Return success response
            return View("BabyList");
        }


        #endregion

        #region FavoriteNamesPage
        public IActionResult FavoriteNamesPage()
        {
            List<BabyModel> list = baby_DALBase.PR_GetUserFavoriteNames();
            return View(list);
        }

        #endregion

    }
}
