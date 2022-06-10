using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;
using TheCoreBanking.Authenticate.Models;

namespace TheCoreBanking.Authenticate.Controllers
{
    public class SetupController : Controller
    {
        private IAuthenticateUnitOfWork CustomerUnitOfWork { get; set; }
        public SetupController(IAuthenticateUnitOfWork uow)
        {
            CustomerUnitOfWork = uow;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddIndustrySec([FromBody]TblIndustry industrySec)
        {
            
            CustomerUnitOfWork.Industries.Add(industrySec);
            CustomerUnitOfWork.Commit();
            return Json(industrySec.Industryid);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddSectorSetup([FromBody]TblSector Sectorsetup)
        {
            //IQueryable<TblSector> setupTracker = CustomerUnitOfWork.Sectors.GetSingleByNameOrCode(Sectorsetup);
            //var setupTrackerValue = setupTracker.FirstOrDefault();
            var setupTracker = CustomerUnitOfWork.Sectors.GetAll().Where(o => o.Name == Sectorsetup.Name || o.Code==Sectorsetup.Code);
            var setupTrackerValue = setupTracker.Count();
            if (setupTrackerValue >= 1)
            {
                return Json(false);
            }
            else
            {
                CustomerUnitOfWork.Sectors.Add(Sectorsetup);
                CustomerUnitOfWork.Commit();
                return Json(true);

            }
        }
        public JsonResult LoadSectors()
        {
            var list = new List<SelectTwoContent>();
            var result = CustomerUnitOfWork.Sectors.GetAll();
            foreach (var item in result)
            {
                list.Add(new SelectTwoContent
                {
                    Id = item.Sectorid.ToString(),
                    Text = item.Name
                });
            }
            return Json(list);
        }
        [HttpPost]
        public JsonResult DeleteSectorSetup([FromBody]TblSector sectorSetup)
        {
            var item = CustomerUnitOfWork.Sectors.GetById(sectorSetup.Sectorid);
            item.Isdeleted = true;

            CustomerUnitOfWork.Sectors.Update(item);
            CustomerUnitOfWork.Commit();
            return Json(sectorSetup.Sectorid);
        }

        [HttpPost]
        public JsonResult DeleteIndustrySetup([FromBody]TblIndustry industrysec)
        {
            var item = CustomerUnitOfWork.Industries.GetById(industrysec.Industryid);
            item.Isdeleted = true;

            CustomerUnitOfWork.Industries.Update(item);
            CustomerUnitOfWork.Commit();
            return Json(industrysec.Industryid);
        }

        [HttpPost]
        public JsonResult updateIndustrySec([FromBody]TblIndustry industrysec)
        {
            CustomerUnitOfWork.Industries.Update(industrysec);
            CustomerUnitOfWork.Commit();
            return Json(industrysec.Industryid);
        }

        [HttpPost]
        public JsonResult updateSectorSetup([FromBody]TblSector sectorsetup)
        {
           // IQueryable<TblSector> setupCodeTracker = CustomerUnitOfWork.Sectors.GetSingleByCode(sectorsetup);
            //IQueryable<TblSector> setupNameTracker = CustomerUnitOfWork.Sectors.GetSingleByName(sectorsetup);
            var setupCodeTracker = CustomerUnitOfWork.Sectors.GetAll().Where(o => o.Code == sectorsetup.Code);
            var setupNameTracker = CustomerUnitOfWork.Sectors.GetAll().Where(o => o.Name == sectorsetup.Name );
            // var setupTrackerValue = setupCodeTracker.Count();
            if (setupCodeTracker.Count() == 1 && setupNameTracker.Count() == 1)
            {
                //return Json(setupTrackerValue);
                return Json(false);
            }
            else if (setupCodeTracker.Count() == 0 && setupNameTracker.Count() == 1)
            {
                CustomerUnitOfWork.Sectors.Update(sectorsetup);
                CustomerUnitOfWork.Commit();
                //return Json(sectorsetup.Sectorid);
                return Json(true);
            }

            else if (setupCodeTracker.Count() == 1 && setupNameTracker.Count() == 0)
            {
                CustomerUnitOfWork.Sectors.Update(sectorsetup);
                CustomerUnitOfWork.Commit();
                //return Json(sectorsetup.Sectorid);
                return Json(true);
            }

            else if (setupCodeTracker.Count() == 0 && setupNameTracker.Count() == 0)
            {
                CustomerUnitOfWork.Sectors.Update(sectorsetup);
                CustomerUnitOfWork.Commit();
                //return Json(sectorsetup.Sectorid);
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        public JsonResult ListSectors()
        {
            var result = CustomerUnitOfWork.Sectors.GetAll().Where(o=>o.Isdeleted==false);
            return Json(result);
        }

        public JsonResult ListOfIndustry()
        {
            var result = CustomerUnitOfWork.Industries.GetActive();
            return Json(result);
        }
        public class SelectTwoContent
        {
            public string Id { get; set; }
            public string Text { get; set; }
        }
    }
}