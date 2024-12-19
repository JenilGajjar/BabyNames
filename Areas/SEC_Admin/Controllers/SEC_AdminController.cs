using BabyNames.Areas.BabyName.Models;
using BabyNames.Areas.SEC_User.Models;
using BabyNames.BAL;
using BabyNames.DAL.Baby_DAL;
using BabyNames.DAL.SEC_AdminDAL;
using BabyNames.DAL.SEC_UserDAL;
using BabyNames.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BabyNames.Areas.SEC_Admin.Controllers
{
    [CheckAccess]
    [Area("SEC_Admin")]
    [Route("SEC_Admin/[controller]/[action]")]
    public class SEC_AdminController : Controller
    {
        SEC_AdminDALBase sEC_AdminDALBase = new SEC_AdminDALBase();

        private readonly IWebHostEnvironment _webHostEnvironment;

        public SEC_AdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            DataTable dt = sEC_AdminDALBase.PR_COUNT_RECORDS();

            return View(dt);
        }

        public IActionResult UserList()
        {
            SEC_UserDALBase sEC_UserDALBase = new SEC_UserDALBase(_webHostEnvironment);

            return View(sEC_UserDALBase.PR_SEC_User_SelectALL());
        }
        public IActionResult BabyList(int pg = 1)
        {
            Baby_DALBase baby_DALBase = new Baby_DALBase();

            try
            {


                List<BabyModel> babyList = baby_DALBase.PR_SELECT_ALL_BabyNames();

                const int pageSize = 10;
                if (pg < 1) pg = 1;

                int resCount = (babyList == null) ? 0 : babyList.Count;

                var pager = new Pager(resCount, pg, pageSize);

                int recSkip = (pg - 1) * pageSize;

                var data = babyList.Skip(recSkip).Take(pager.PageSize).ToList();


                ViewBag.Pager = pager;

                return View(data);
            }
            catch
            {
                return View();
            }
        }


        #region Delete

        public IActionResult BabyDelete(int NameID)
        {
            bool IsSuccess = sEC_AdminDALBase.PR_Baby_Name_Delete(NameID);
            if (IsSuccess)
            {
                TempData["BabyNameDeleteMessage"] = "Recored Deleted SuccessFully";
                return RedirectToAction("BabyList");
            }

            return RedirectToAction("BabyList");
        }

        #endregion

        #region Save

        public IActionResult BabySave(BabyModel babyModel)
        {

            Console.WriteLine("NameID " + babyModel.NameID);
            Console.WriteLine("Name " + babyModel.Name);
            Console.WriteLine("Meaning " + babyModel.Meaning);
            Console.WriteLine("Numerology " + babyModel.Numerology);
            Console.WriteLine("Gender " + babyModel.Gender);
            Console.WriteLine("NakshatraID " + babyModel.NakshatraID);
            Console.WriteLine("ZodiacID " + babyModel.ZodiacID);
            Console.WriteLine("ReligionID " + babyModel.ReligionID);
            Console.WriteLine("Syllables " + babyModel.Syllables);
            Console.WriteLine("CategoryID " + babyModel.CategoryID);
            Console.WriteLine(ModelState.ErrorCount);


            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var e in errors)
            {
                Console.WriteLine(e.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                if (sEC_AdminDALBase.PR_Baby_Name_Save(babyModel))
                {
                    TempData["BabyNameSaveMessage"] = "Name Saved SuccessFully";
                }
                return RedirectToAction("BabyList");

            }
            Baby_DALBase baby_DALBase = new Baby_DALBase();
                
            ViewBag.ZodiacFilterList = baby_DALBase.ZodiacFilter();
            ViewBag.ReligionFilterList = baby_DALBase.ReligionFilter();
            ViewBag.NakshatraFilterList = baby_DALBase.NakshatraFilter();

            return View("BabyNameAddEdit");
        }

        #endregion


        #region Add

        public IActionResult BabyAdd(int NameID = 0)
        {
            Baby_DALBase baby_DALBase = new Baby_DALBase();

            ViewBag.ZodiacFilterList = baby_DALBase.ZodiacFilter();
            ViewBag.ReligionFilterList = baby_DALBase.ReligionFilter();
            ViewBag.NakshatraFilterList = baby_DALBase.NakshatraFilter();


            BabyModel babyModel = new BabyModel();
            if (NameID != 0)
            {
                ViewData["Title"] = "Edit";
                babyModel = sEC_AdminDALBase.PR_Baby_Name_SelectByPK(NameID);
                return View("BabyNameAddEdit", babyModel);
            }
            else
            {
                ViewData["Title"] = "Add";
                return View("BabyNameAddEdit", babyModel);
            }

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
            return RedirectToAction("Index");   
        }
        #endregion 


        #region UserProfile
        public IActionResult UserProfile()
        {
            SEC_UserDALBase userDAL = new SEC_UserDALBase(_webHostEnvironment);

            int UserID = (int)CV.UserID();

            SEC_UserModel modelSEC_User = userDAL.PR_SEC_UserSelectByUserName(UserID);
            Console.WriteLine(CV.PhotoPath());

            return View(modelSEC_User);
        }
        #endregion

        #region User Update
        public IActionResult UserUpdate(SEC_UserModel sEC_UserModel)
        {


            sEC_UserModel.UserID = (int)CV.UserID();
            SEC_UserDALBase userDAL = new SEC_UserDALBase(_webHostEnvironment);
            sEC_UserModel.Password = sEC_UserModel.Password ?? CV.Password();

            userDAL.PR_SEC_User_UPDATEByPK(sEC_UserModel);


            return RedirectToAction("Index");

        }
        #endregion
    }
}
