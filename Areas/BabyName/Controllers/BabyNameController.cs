using BabyNames.Areas.BabyName.Models;
using BabyNames.BAL;
using BabyNames.DAL.Baby_DAL;
using BabyNames.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabyNames.Areas.BabyName.Controllers
{
    [CheckAccess]
    [Area("BabyName")]
    [Route("BabyName/[controller]/[action]")]
    public class BabyNameController : Controller
    {
        Baby_DALBase baby_DALBase = new Baby_DALBase();


        public IActionResult BabyFilter(BabyFilterModel babyfiltermodel)
        {

            Console.WriteLine(babyfiltermodel.Gender);

            ViewBag.ZodiacFilterList = baby_DALBase.ZodiacFilter();
            ViewBag.ReligionFilterList = baby_DALBase.ReligionFilter();
            ViewBag.NakshatraFilterList = baby_DALBase.NakshatraFilter();


            List<BabyModel>  list = baby_DALBase.PR_BabyNames_FILTER(babyfiltermodel);


            ModelState.Clear();

            return View("Index", list);

        }


        #region BabyList
        public IActionResult Index()
        {


            try
            {

            ViewBag.ZodiacFilterList = baby_DALBase.ZodiacFilter();
            ViewBag.ReligionFilterList = baby_DALBase.ReligionFilter();
            ViewBag.NakshatraFilterList = baby_DALBase.NakshatraFilter();


            List<BabyModel> babyList = baby_DALBase.PR_SELECT_ALL_BabyNames();

           

            return View(babyList);

            }catch 
            {
                return View();
            }
        }

        #endregion
    }
}
