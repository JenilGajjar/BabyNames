using BabyNames.Areas.BabyName.Models;
using BabyNames.BAL;
using BabyNames.DAL.Baby_DAL;
using BabyNames.DAL.SEC_AdminDAL;
using BabyNames.DAL.SEC_UserDAL;
using BabyNames.Models;
using Microsoft.AspNetCore.Mvc;

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
            return View();
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

            Console.WriteLine(babyModel.NameID);

            Console.WriteLine(babyModel.Name);
            Console.WriteLine(babyModel.Meaning);
            Console.WriteLine(babyModel.Numerology);
            Console.WriteLine(babyModel.Gender);
            Console.WriteLine(babyModel.NakshatraID);
            Console.WriteLine(babyModel.ZodiacID);
            Console.WriteLine(babyModel.ReligionID);
            Console.WriteLine(babyModel.Syllables);
            Console.WriteLine(babyModel.CategoryID);
            Console.WriteLine(ModelState.ErrorCount);
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach(var e in errors)
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
            return RedirectToAction("BabyNameAddEdit");
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

        public IActionResult UserProfile()
        {
            return View();
        }

    }
}
