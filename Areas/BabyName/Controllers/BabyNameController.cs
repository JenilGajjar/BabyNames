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


        //public IActionResult BabyFilter(BabyFilterModel babyfiltermodel)
        //{

        //    Console.WriteLine(babyfiltermodel.Gender);

        //    ViewBag.ZodiacFilterList = baby_BALBase.ZodiacFilter();
        //    ViewBag.ReligionFilterList = baby_BALBase.ReligionFilter();
        //    ViewBag.NakshatraFilterList = baby_BALBase.NakshatraFilter();

        //    list = baby_BALBase.PR_BabyNames_FILTER(babyfiltermodel);

        //    this.flag = false;
        //    Console.WriteLine("Value of flag : in filter " + flag);

        //    int pg = 1;

        //    const int pageSize = 20;
        //    if (pg < 1) pg = 1;

        //    int resCount = list.Count;

        //    var pager = new Pager(resCount, pg, pageSize);

        //    int recSkip = (pg - 1) * pageSize;

        //    var data = list.Skip(recSkip).Take(pager.PageSize).ToList();

        //    ViewBag.Pager = pager;

        //    ModelState.Clear();

        //    return View("Index", data);

        //}


        #region BabyList
        public IActionResult Index(int pg = 1)
        {


            try
            {

            //ViewBag.ZodiacFilterList = baby_BALBase.ZodiacFilter();
            //ViewBag.ReligionFilterList = baby_BALBase.ReligionFilter();
            //ViewBag.NakshatraFilterList = baby_BALBase.NakshatraFilter();


            List<BabyModel> babyList = baby_DALBase.PR_SELECT_ALL_BabyNames();

            const int pageSize = 20;
            if (pg < 1) pg = 1;

            int resCount = (babyList == null) ? 0 : babyList.Count ;

            var pager = new Pager(resCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = babyList.Skip(recSkip).Take(pager.PageSize).ToList();


            ViewBag.Pager = pager;


            return View(data);
            }catch 
            {
                return View();
            }
        }

        #endregion
    }
}
