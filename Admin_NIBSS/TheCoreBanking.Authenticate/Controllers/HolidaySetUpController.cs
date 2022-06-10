using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCoreBanking.Authenticate.Data;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.IO;
using System.Numerics;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Helpers;
using TheCoreBanking.Authenticate.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace TheCoreBanking.Authenticate.Controllers
{
    //[Authorize]helooo
    public class HolidaySetUpController : Controller
    {


        private IAuthenticateUnitOfWork AuthenticateUnitOfWork { get; }

        private readonly TheCoreBankingAuthenticateContext _context = new TheCoreBankingAuthenticateContext();
        public HolidaySetUpController(IAuthenticateUnitOfWork uow)
        {
            AuthenticateUnitOfWork = uow;

        }


        public IActionResult Index()
        {
            return View();
        }



        public JsonResult LoadHoliday()
        {
            var result = AuthenticateUnitOfWork.FinHols.GetAll().ToList();



            return Json(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddHoliday(Data.Model.TblFinTrakHolsDate holly)
        {
            //holly.Date = DateTime.Now;

            AuthenticateUnitOfWork.FinHols.Add(holly);
            AuthenticateUnitOfWork.Commit();

            return Json(holly);
        }




        [HttpGet]
        public JsonResult Edit(int id)
        {
            var result = AuthenticateUnitOfWork.FinHols.GetById(id);
            return Json(result);
        }



        // POST: CallMemoSetUp/Edit/5
        [HttpPost]
        public JsonResult EditHoliday(TblFinTrakHolsDate holiy)
        {

            AuthenticateUnitOfWork.FinHols.Update(holiy);
            AuthenticateUnitOfWork.Commit();
            return Json(holiy.Id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ReturnWeekends(declare _declare)
        {
            DateTime startDate = _declare.Date;
            DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);

            int days = endDate.Subtract(startDate).Days;

            for (int i = 1; i <= days - 1; i++)

            {

                System.TimeSpan duration = new System.TimeSpan(i, 0, 0, 0);
                System.DateTime answer = startDate.Add(duration);

                var day = answer.ToString("ddd");

                if (day == "Sat" || day == "Sun")
                {

                    TblFinTrakHolsDate tblFinTrakHolsDate = new TblFinTrakHolsDate();

                    tblFinTrakHolsDate.Date = answer;
                    if (day == "Sat")
                    {

                        tblFinTrakHolsDate.Description = "Saturday";

                    }
                    else
                    {

                        tblFinTrakHolsDate.Description = "Sunday";

                    }

                    tblFinTrakHolsDate.HolidayType = "Weekend";

                    var dbContextTransaction = _context.Database.BeginTransaction();
                    try
                    {
                        _context.TblFinTrakHolsDate.Add(tblFinTrakHolsDate);
                        _context.SaveChanges();
                        dbContextTransaction.Commit();

                    }
                    catch
                    {

                        dbContextTransaction.Rollback();

                    }
                }


            }


            return Json(true);
        }


        public JsonResult DeleteWeekends()
        {

            var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {
                var holidays = _context.TblFinTrakHolsDate.Where(s => s.HolidayType == "Weekend").ToList();
                if (holidays != null)
                {
                    _context.TblFinTrakHolsDate.RemoveRange(holidays);
                    _context.SaveChanges();
                }

                dbContextTransaction.Commit();

            }
            catch
            {

                dbContextTransaction.Rollback();

            }

            return Json(true);
        }



        public class SelectTwoContentM
        {
            public string id { get; set; }
            public string text { get; set; }
        }

    }

    public class declare
    {
        public DateTime Date { get; set; }
    }
}