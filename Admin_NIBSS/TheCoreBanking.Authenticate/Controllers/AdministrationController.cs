using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Services;
using TheCoreBanking.Authenticate.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using TheCoreBanking.Authenticate.Models;
using IdentityModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TheCoreBanking.Data.Contracts;
using TheCoreBanking.Data;
using Microsoft.IdentityModel.Protocols;
using System.IO;
using System.Web;
//using TheCoreBanking.Data.Models;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using static IdentityServer4.Models.IdentityResources;
using TheCoreBanking.Data.Models;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using NLog;
using Microsoft.Data.SqlClient;
using RestSharp;
using static TheCoreBanking.Authenticate.Models.APIMISVModel;
using Newtonsoft.Json;
using NWebsec.AspNetCore.Mvc;
using NWebsec.AspNetCore.Mvc.Csp;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheCoreBanking.Authenticate.Controllers
{
   [Authorize()]
    [Csp]
    [CspDefaultSrc(Self = true)]
    [NoCacheHttpHeaders]
    public class AdministrationController : Controller
    {
        //private TheCoreBanking.Data.Models.TheCoreBankingContext _Context;
        TheCoreBankingAuthenticateContext ValidateContext = new TheCoreBankingAuthenticateContext();
        TheCoreBankingAuthenticateContext db = new TheCoreBankingAuthenticateContext();
        private readonly ILogger<ApplicationUser> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        private readonly AccountService _account;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        
        string[] Gender= { "Male", "Female" };
           public AdministrationController(          
          IIdentityServerInteractionService interaction,
          IClientStore clientStore,
          IHttpContextAccessor httpContextAccessor,
          IAuthenticationSchemeProvider schemeProvider,
          IEventService events,
          ILogger<ApplicationUser> logger,
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          ISetupUnitOfWork uowSetup,
          IAuthenticateUnitOfWork uowAuthenticate,
          IEmailSender emailSender,
          RoleManager<IdentityRole> roleManager)        
        {
            // if the TestUserStore is not in DI, then we'll just use the global users collection
            _userManager = userManager;
            _interaction = interaction;
            _events = events;
            _logger = logger;
            _account = new AccountService(interaction, httpContextAccessor, schemeProvider, clientStore);
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            setupUnitOfWork = uowSetup;
            authenticateUnitOfWork = uowAuthenticate;
        }     

        public ISetupUnitOfWork setupUnitOfWork { get; set; }
        public IAuthenticateUnitOfWork authenticateUnitOfWork { get; set; }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.gender = Gender;
            return View();

        }
        public string GetLoggedUser()
        {
            var logUser = User.Identity.Name;
            if (logUser == null)
            {
                logUser = "tayo.olawumi";
            }
            else
            {
                logUser = User.Identity.Name;
            }
            return logUser;
        }
        public IActionResult ManageRole()
        {
            ViewBag.gender = Gender;
            return View();
        }
        public IActionResult UserManagement()
        {
            return View();
        }
        public IActionResult StartAndEndDay()
        {
            var CurrentDates = authenticateUnitOfWork.CurrentDate.GetAll();
            var CurrentDate= CurrentDates.FirstOrDefault().CurrentDate;
            ViewBag.CurrentDate = string.Format("{0:dddd MMM dd,yyyy}",CurrentDate);
            return View();
        }
        [Authorize(Roles = "Setup")]
        public IActionResult AutoLogOut()
        {

            return View();
        }
        public JsonResult listAutoLogOut()
        {
            var LogOut = ValidateContext.TblAutoLogOff.ToList();
           
            return Json(LogOut);
          
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RemoveDuration(TblAutoLogOff logOff)
        {
            ValidateContext.TblAutoLogOff.Remove(logOff);
            ValidateContext.SaveChanges();
            return Json("");
        }
        //public JsonResult DirectorBVN(string Bvn)
        //{
        //    var checkBVN = setupUnitOfWork.Director.GetAll().Where(o => o.Bvn == Bvn).Count();
        //    if (string.IsNullOrEmpty(Bvn))
        //        return Json("BVN cannot be null");
        //    if (Bvn.Length != 11)
        //        return Json("BVN number must be 11 characters");
        //    if (checkBVN > 0)

        //        return Json("BVN already exist");
        //    else
        //        return Json("");
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AutoLogOut(TblAutoLogOff autoLogOff)
        {
            autoLogOff.CreatedBy = User.Identity.Name;
            autoLogOff.DateCreated = DateTime.Now;
            ValidateContext.TblAutoLogOff.Add(autoLogOff);
            ValidateContext.SaveChanges();
            return Json("");
        }
        public JsonResult listUserManagement()
        {
            var staff = ValidateContext.vwUsermanagement.ToList();
            //var staff = ValidateContext.TblStaffInformation.ToList();
            return Json(staff);
           // return Json("");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult lockUser(int Id)
        {
            var userStaff = db.TblStaffInformation.Where(o => o.StaffName == User.Identity.Name && o.Approved == true && o.Locked == "false").FirstOrDefault();
            var staff = db.TblStaffInformation.Where(o => o.Id == Id).FirstOrDefault();
            authenticateUnitOfWork.FinanceTransaction.UpdateStaffInformation(Id);
           
            Data.Model.TblBankingAuditTrail Audit = new Data.Model.TblBankingAuditTrail();            
            Audit.BrCode = userStaff.BranchId;
            Audit.CmpName = userStaff.CoyName;
            Audit.CoyCode = userStaff.CompanyId;
            Audit.DeptCode = userStaff.DeptCode;
            Audit.Status = "Lock User";
            Audit.TransDate = DateTime.Now;
            Audit.TransTime = DateTime.Now.ToShortTimeString();
            Audit.TransType = "UserManagement";
            Audit.UserName = userStaff.StaffName;
            db.TblBankingAuditTrail.Add(Audit);
            db.SaveChanges();


            return Json(new  { message =" "});
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult UnlockUser(int Id)
        {
            var userStaff = db.TblStaffInformation.Where(o => o.Approved == true && o.Locked == "Locked" && o.Id == Id).FirstOrDefault();
            var staff = db.TblStaffInformation.Where(o => o.Id == Id).FirstOrDefault();
            authenticateUnitOfWork.FinanceTransaction.UpdateUnlockInformation(Id);

            Data.Model.TblBankingAuditTrail Audit = new Data.Model.TblBankingAuditTrail();
            Audit.BrCode = userStaff.BranchId;
            Audit.CmpName = userStaff.CoyName;
            Audit.CoyCode = userStaff.CompanyId;
            Audit.DeptCode = userStaff.DeptCode;
            Audit.Status = "Unlock User";
            Audit.TransDate = DateTime.Now;
            Audit.TransTime = DateTime.Now.ToShortTimeString();
            Audit.TransType = "UserManagement";
            Audit.UserName = userStaff.StaffName;
            db.TblBankingAuditTrail.Add(Audit);
            db.SaveChanges();


            return Json(new { message = " " });
        }
        [HttpGet]
        public IActionResult ManageSOD()
        {
            var CheckEndOfDay = "";
            try
            {
                var endDay = authenticateUnitOfWork.MutuallyExclusive.GetAll().Where(i => i.Endofday != null).FirstOrDefault().Endofday;
                if (endDay == null)
                {
                    CheckEndOfDay = "No data found";
                }
                if (endDay == true)
                {
                    TheCoreBankingAuthenticateContext db = new TheCoreBankingAuthenticateContext();
                    try
                    {
                        db.Database.ExecuteSqlCommand("[dbo].[spFintrakSOD]");
                        authenticateUnitOfWork.EODMSYProcess.ClearCheques();
                        CheckEndOfDay = "Successful";
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    CheckEndOfDay = "You cannot start the day because end of day has not been processed";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Json(CheckEndOfDay);
        }
        public JsonResult EODMSYProcessResult()
        {
            //DateTime EODdate;
            string checkResult = string.Empty;


            var AppDate = ValidateContext.TblFinanceCurrentDate.FirstOrDefault();


            int SavingsAndDebitInterest = authenticateUnitOfWork.EODMSYProcess.RunSavingsAndDebitInterest(AppDate.CurrentDate);
            int runCot = authenticateUnitOfWork.EODMSYProcess.RunCOT(AppDate.CurrentDate);
            int runDormantAcct = authenticateUnitOfWork.EODMSYProcess.RunDormantAccount(AppDate.CurrentDate);
            int runSmsCharges = authenticateUnitOfWork.EODMSYProcess.RunSMSCharges(AppDate.CurrentDate);
            int validateMeansOfID = authenticateUnitOfWork.EODMSYProcess.ValidateValidMeansOfId();

            if (SavingsAndDebitInterest != 0 && runCot != 0 && runDormantAcct != 0 && runSmsCharges != 0 && validateMeansOfID != 0)
            {
                checkResult = "Successful";
                return Json(checkResult);
            }
            else
            {
                checkResult = "Failure";
                return Json(checkResult);
            }
        }
        public int GetEOMProcess()
        {
            int day = 0;
            var year = DateTime.Now.Year;

            if (DateTime.IsLeapYear(year))
            {
                var getMonth = DateTime.Now.Month;

                switch (getMonth)
                {
                    case 1:
                        day = 31; break;
                    case 2:
                        day = 29; break;
                    case 3:
                        day = 31; break;
                    case 4:
                        day = 30; break;
                    case 5:
                        day = 31; break;
                    case 6:
                        day = 30; break;
                    case 7:
                        day = 31; break;
                    case 8:
                        day = 31; break;
                    case 9:
                        day = 30; break;
                    case 10:
                        day = 31; break;
                    case 11:
                        day = 30; break;
                    case 12:
                        day = 31; break;

                    default:
                        day = 0; break;

                }
            }
            else
            {
                var getMonth = DateTime.Now.Month;

                switch (getMonth)
                {
                    case 1:
                        day = 31; break;
                    case 2:
                        day = 28; break;
                    case 3:
                        day = 31; break;
                    case 4:
                        day = 30; break;
                    case 5:
                        day = 31; break;
                    case 6:
                        day = 30; break;
                    case 7:
                        day = 31; break;
                    case 8:
                        day = 31; break;
                    case 9:
                        day = 30; break;
                    case 10:
                        day = 31; break;
                    case 11:
                        day = 30; break;
                    case 12:
                        day = 31; break;

                    default:
                        day = 0; break;

                }
            }
            return day;
        }
        [HttpGet]
        public JsonResult ManageEOD()
        {
            var intDay = DateTime.Now.Day;
            int getDay = GetEOMProcess();
            string CheckResult = "";
            if (intDay == getDay)
            {
                //Perform End of Month process here
                if (ValidatingEodResult() == false)
                {
                    EODMSYProcessResult();
                    db.Database.ExecuteSqlCommand("[dbo].[spd_EndOfDay]");
                    CheckResult = "Successful";
                }
                else
                {
                    CheckResult = "Failure";
                }
            }
            else
            {
                if (ValidatingEodResult() == false)
                {
                    EODMSYProcessResult();
                    db.Database.ExecuteSqlCommand("[dbo].[spd_EndOfDay]");
                    CheckResult = "Successful";
                }
                else
                {
                    CheckResult = "Failure";
                }
            }
            return Json(CheckResult);
        }

        [HttpGet]
        public JsonResult ManageEOY(string YearDate)
        {
            string CheckResult = "";

            string sqlQuery;
            // Initialization.  
            List<EndofDayModel> lst = new List<EndofDayModel>();
            TheCoreBankingAuthenticateContext db = new TheCoreBankingAuthenticateContext();
            try
            {
                SqlParameter yearParam = new SqlParameter("@YearEndDate", YearDate);

                // Processing.  
                db.Database.ExecuteSqlCommand("[dbo].[sp_Posting] @YearEndDate", yearParam);

                CheckResult = "Successful";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(CheckResult);
        }
        //public IActionResult LogTrack()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult LogTrack(string user, DateTime dateFrom, DateTime dateTo)
        {
            user = "tayo.olawumi"; dateFrom =Convert.ToDateTime("2017-12-01"); dateTo = Convert.ToDateTime("2017-12-06");
            Models.TheCoreBankingContext context = new Models.TheCoreBankingContext();
          var result=  context.TblBankingAuditTrail.ToList().Where(o => o.UserName == user && o.TransDate >= dateFrom && o.TransDate <= dateTo);
            return Json(result.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRole(RoleViewModel model)
        {
            //int count = 0;
            int response = 0;
            bool isRoleExist = await _roleManager.RoleExistsAsync(model.RoleName);
            if (isRoleExist == true)
            {
                response = 2;
            }
            if (!isRoleExist)
            {
                var addModule = new TblAspNetModuleRoles
                {
                    ModuleName = model.RoleName
                };
                db.TblAspNetModuleRoles.Add(addModule);
                db.SaveChanges();
                var role = new IdentityRole(model.RoleName);
                role.Name = model.RoleName;
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    await _roleManager.AddClaimAsync(role,
                             new Claim("User", "Register")
                         );

                }
                return View();
                //response = 1;
            }
            //else
            //{
            //    count = 2;
            //}
            return Json(response);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddUserRole(UserRoleViewModel model)
        {
            bool checkIfMapped = false;
            //var loginUser = "tayo.olawumi";
            //var staff = "";
            var RoleId = "";
            var subRole = "";
            //var lstRole = "";
            var newRole=string.Empty;
              string[] result2 = model.RoleName.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] result1 = model.Username.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> lstRole = new List<string>() { result2.ToString() };
            foreach (string item1 in result2)
            {
                newRole = item1;
                RoleId = db.TblAspNetSubModuleRoles.Where(i => i.SubmoduleName == item1).FirstOrDefault().ModuleId;
                subRole = _roleManager.Roles.Where(i => i.Id == RoleId).FirstOrDefault().Name;
            }
            foreach (string item in result1)
                {
                   //var staff = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == item).FirstOrDefault();
                   //var staff = db.TblStaffInformation.ToList().Where(o => o.StaffName == item).FirstOrDefault();
                    var lstUser = new ListUserRoles()
                    {
                        LstUsers = item,
                        LstUserActivities = newRole,
                        LstModules = subRole,
                        //UserRoleId = RoleId,
                        DateCreated = DateTime.Now
                    };
                    db.ListUserRoles.Add(lstUser);
                    db.SaveChanges();
                }

            ViewBag.gender = Gender;
            var role = new IdentityRole(newRole);
            bool isRoleExist = await _roleManager.RoleExistsAsync(newRole);
            ApplicationUser isUser = await _userManager.FindByNameAsync(result1[0]);
            if (isRoleExist == false && isUser != null)
            {
                
                var result = await _userManager.AddToRoleAsync(isUser, subRole);
                // Remove existing claims
                IList<Claim> existingClaims = await _userManager.GetClaimsAsync(isUser);
                var removal = await _userManager.RemoveClaimsAsync(isUser, existingClaims);

                var reply = await _roleManager.AddClaimAsync(role,
                           new Claim(isUser.UserName, newRole)
                       );
                if (result.Succeeded)
                {
                  
                    checkIfMapped = true;

                }
            }
            else
            {
                checkIfMapped = false;
            }
            //await _userManager.AddToRoleAsync(isUser, model.RoleName)
            return Json(checkIfMapped);
        }

        [HttpGet]
        public JsonResult GetAllSubModuleByModuleID(string ModuleId)
        {
            var result_set = db.TblAspNetSubModuleRoles.Where(i => i.ModuleId == ModuleId).ToList();
            return Json(result_set);
        }

        //[HttpGet]
        //public JsonResult ListRole()
        //{
        //    var allRoles =  _roleManager.Roles;
        //    return Json(allRoles.Select(o => new { o.Id, o.Name}));
        //}

        //, ActionName("Delete")
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> RemoveRole(string ID)
        {
            var allRoles = _roleManager.Roles;
            var Del = allRoles.Single(i => i.Id == ID);
            
           var reply=await _roleManager.DeleteAsync(Del);
            
            return RedirectToAction("ManageRole");
        }
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSubRole(string ModuleId,string SubModuleName)
        {
            int result = 0;
            try
            {
                if (ModuleId != null && SubModuleName != null)
                {
                    var addSubModule = new TblAspNetSubModuleRoles
                    {
                        ModuleId = ModuleId,
                        SubmoduleName = SubModuleName
                    };
                    db.TblAspNetSubModuleRoles.Add(addSubModule);
                    db.SaveChanges();
                    //return RedirectToAction("ManageRole","Administration");
                   result = 1;
                    return Json(result);
                }
                else
                {
                    result = 1;
                    return Json(result);
                }
            }
            catch (Exception)
            {
                result = 0;
              
            }
            
            return Json(result);
        }

        [HttpGet]
        public JsonResult ListUser()
        {
            var allUsers = _userManager.Users;
            return Json(allUsers.Select(o => new { o.Id, o.UserName }));
        }
        [AllowAnonymous]
        public JsonResult ListUsers()
        {
            var allUsers = _userManager.Users;
            return Json(allUsers.Select(o => new { o.Id, o.UserName,o.tblStaffInformation.CompanyId,o.tblStaffInformation.BranchId,o.tblStaffInformation.DeptCode,
                o.Email,o.PhoneNumber,o.tblStaffInformation.StaffName,o.tblStaffInformation.Address,o.tblStaffInformation.Nationality,o.tblStaffInformation.State
            }));
        }

        [HttpGet]
        public JsonResult ListOfUserRoles()
        {
            var result = db.ListUserRoles.ToList();
            return Json(result);
        }
            public JsonResult ListUsersRoles()
        {
            List<UserRoles> userRole = new List<UserRoles>();
            var allUsers = _userManager.Users;
            foreach (var user in allUsers)
            {
                var userRoles = _userManager.GetRolesAsync(user).Result;
                foreach (var role in userRoles)
                {
                    userRole.Add(new UserRoles { role = role, Username = user.UserName });
                }
            }
            return Json(userRole);
        }
        public JsonResult ListUserRoles(string userName)
        {
            var findUser = _userManager.FindByNameAsync(userName).Result;
            var allUserRoles =  _userManager.GetRolesAsync(findUser).Result;

            return Json(allUserRoles);
        }

        public class UserRoles
        {
            public string Username { get; set; }
            public string role { get; set; }
        }

        
        [Authorize()]
        public  IActionResult Register()
        {
            var staffInfo = db.TblStaffInformation.Where(i => i.StaffName == GetLoggedUser()).FirstOrDefault();
            var branch = authenticateUnitOfWork.Branch.GetAll().Where(i => i.BrId == staffInfo.BranchId).FirstOrDefault().BrAddress;
            var currentDate = authenticateUnitOfWork.CurrentDate.GetAll().FirstOrDefault().CurrentDate;
            ViewData["StaffName"] = staffInfo.StaffName;
            ViewData["CompanyName"] = staffInfo.CoyName;
            ViewData["CurrentDate"] = string.Format("{0:dddd MMMM dd, yyyy}",currentDate);
            ViewData["Branch"] = branch;
            ViewBag.gender = Gender;           
            return View();
        }

        

        //}
        [AcceptVerbs("GET","POST")]
        public async Task<IActionResult> IsEmailInUse(string Username)
        {
            var user =await  _userManager.FindByNameAsync(Username);
            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"The username {Username} is already in use ");
            }
        }
        [Authorize()]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Register(RegisterViewModel model, string returnUrl = null)
        {


            returnUrl = returnUrl ?? Url.Action("Login", "Account");
            ViewBag.gender = Gender;
            var newUser = _userManager.FindByNameAsync(model.Username).Result;
            var newEmail = _userManager.FindByEmailAsync(model.staffInformation.Email).Result;
            if (model.Staffsignature != null)
            {
                try
                {
                    foreach (var item in model.Staffsignature)
                    {
                        if (item.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                item.CopyTo(stream);
                                model.staffInformation.Staffsignature = stream.ToArray();
                            }
                        }
                    }
                }
                //catch (Exception ex)
                catch
                {
                    // Logger logger = LogManager.GetLogger("databaseLogger");
                    //throw ex;
                }
            }
           

            if (newUser == null && newEmail == null)
            {
                //var userEmails = _userManager.FindByNameAsync(model.staffInformation.Email).Result;
                var misCount = AuthenticateMISCODE().Where(o => o.id == Convert.ToInt32(model.staffInformation.Miscode)).Count();
                var mis = "";
                if(misCount > 0)
                {
                     mis = AuthenticateMISCODE().Where(o => o.id == Convert.ToInt32(model.staffInformation.Miscode)).FirstOrDefault().customCode;
                }
                
                Models.TblStaffInformation tblStaff = new Models.TblStaffInformation
                {
                    StaffName = model.Username,
                    Address = model.staffInformation.Address,
                    Age = model.staffInformation.Age,
                    BranchId ="101",
                    Comment = model.staffInformation.Comment,
                    CompanyId = model.staffInformation.CompanyId,
                    Department = model.staffInformation.Department,
                    DeptCode = model.staffInformation.DeptCode,
                    DesignationCode = model.staffInformation.DesignationCode,
                    Email = model.staffInformation.Email,
                    Gender = model.staffInformation.Gender,
                    Id = model.staffInformation.Id,
                    ImageData = model.staffInformation.ImageData,
                    ImageTitle = model.staffInformation.ImageTitle,
                    JobTitle = model.staffInformation.JobTitle,
                    Miscode = mis,
                    Nationality = model.staffInformation.Nationality,
                    NextOfKinAddress = model.staffInformation.NextOfKinAddress,
                    NextOfKinEmail = model.staffInformation.NextOfKinEmail,
                    NextOfKinGender = model.staffInformation.NextOfKinGender,
                    NextOfKinName = model.staffInformation.NextOfKinName,
                    NextOfKinPhone = model.staffInformation.NextOfKinPhone,
                    PcCode = model.staffInformation.PcCode,
                    Phone = model.staffInformation.Phone,
                    Rank = model.staffInformation.Rank,
                    RelationShip = model.staffInformation.RelationShip,
                    StaffNo = model.staffInformation.StaffNo,
                    Staffsignature = model.staffInformation.Staffsignature,
                    State = model.staffInformation.State,
                    Unit = model.staffInformation.Unit,
                    UnitCode = model.staffInformation.UnitCode,
                    Updated = model.staffInformation.Updated,
                    Locked="false",
                    Status="Active"

                };

                newUser = new ApplicationUser
                {
                    UserName = model.Username,
                    tblStaffInformation = tblStaff,
                    Email = model.staffInformation.Email,
                    PhoneNumber = model.staffInformation.Phone,


                };

                var result = _userManager.CreateAsync(newUser, model.Password).Result;
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password");
                    var Token = _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = newUser.Id, code = Token }, protocol: Request.Scheme);

                    _emailSender.SendEmailAsync(model.staffInformation.Email, "Confirm your email",
                    "Confirm Email:<a href=\"" + callbackUrl + "\">Click here</a>"
                    );

                    _signInManager.SignInAsync(newUser, isPersistent: false);
                    // ViewBag.message = "You've successfully registered!";
                    return Redirect(returnUrl);



                }

                else
                {
                    throw new Exception(result.Errors.First().Description);

                }
            }


            return View();


        }
        [AllowAnonymous]
        // [HttpPost]
        public List<ItemMis> AuthenticateMISCODE()
        {

            var client = new RestClient("https://nibsserpapi.nibss-plc.com.ng/api/TokenAuth/Authenticate");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("abp.tenantId", "1");
            request.AddHeader("Content-Type", "application/json");
            var body = @"{
" + "\n" +
            @"   
" + "\n" +
            @"        ""userNameOrEmailAddress"": ""admin"",
" + "\n" +
            @"        ""password"": ""123qwe"",
" + "\n" +
            @"        ""twoFactorVerificationCode"": ""string"",
" + "\n" +
            @"        ""rememberClient"": true,
" + "\n" +
            @"        ""TenancyName"": ""string""
" + "\n" +
            @"   
" + "\n" +
            @"   
" + "\n" +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string reply = string.Empty;
            if (response.IsSuccessful)
            {
                string output = response.Content;
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(output);
                reply = myDeserializedClass.result.accessToken;

            }
           

            var clients = new RestClient($"https://nibsserpapi.nibss-plc.com.ng/api/services/app/CompanyStructure/getcompanystructure");
            clients.Timeout = -1;
            var requests = new RestRequest(Method.GET);
            requests.AddHeader("Authorization", $"Bearer {reply} ");
            requests.AddHeader("Content-Type", "application/json");
            List<ItemMis> item = new List<ItemMis>();

            IRestResponse responses = clients.Execute(requests);
            if (responses.IsSuccessful)
            {
                string outputs = responses.Content;
                RootMis myDeserializedClass = JsonConvert.DeserializeObject<RootMis>(outputs);
                item = myDeserializedClass.result.items;

            }
            return item;

        }

        [HttpGet]
        public JsonResult loadMisCode()
        {

            var reply = AuthenticateMISCODE().ToList();

            Select2Format loadFormat = new Select2Format();

            List<SelectContent> list = new List<SelectContent>();
            foreach (var item in reply)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.id.ToString(),
                    text = item.customCode + "-" + item.displayName


                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        #region Validate EOD

        public IActionResult ValidateEOD()
        {
            return View();
        }

        //public JsonResult listPendingTransactions()
        //{
        //    var result = authenticateUnitOfWork.validateEOD.GetAll();
        //    return Json(result);

        //}


        [HttpGet]
        public JsonResult listPendingTransactions()
        {
            TheCoreBankingAuthenticateContext ValidateContext = new TheCoreBankingAuthenticateContext();

            var result = from a in authenticateUnitOfWork.validateEOD.GetAll()
                         select new
                         {
                             dealId = a.DealId,
                             operation =a.Operation,
                             transDate = a.TransDate,
                             officer = a.Officer
                             //$"{ ValidateContext.TblCasa.Where(i => i.Accountnumber == a.DealId).FirstOrDefault().Accountname}"+  nknn
                             //customerName = ValidateContext.TblCasa.Where(i => i.Accountnumber == a.DealId).FirstOrDefault().Accountname ,
                             //loanName = ValidateContext.TblBankingLoanLease.Where(i => i.CurrentAcct == a.DealId || i.ProductAcctNo == a.DealId).FirstOrDefault().CustName,
                         };
            return Json(result);
        }


        public bool ValidatingEodResult()
        {
            DateTime EODdate;
            bool checkResult = false;

            TheCoreBankingAuthenticateContext ValidateContext = new TheCoreBankingAuthenticateContext();
            var AppDate = ValidateContext.TblFinanceCurrentDate.FirstOrDefault();
            EODdate = AppDate.CurrentDate;

            bool EODValue = authenticateUnitOfWork.validateEOD.ValidateEODTransactions(EODdate);

            if (EODValue != false)
            {
                authenticateUnitOfWork.validateEOD.insertPendingTransactions();
                checkResult = true;
                return checkResult;
            }
            else
            {
                checkResult = false;
                return checkResult;
            }
        }
        public JsonResult ValidateEodResult()
        {
            DateTime EODdate;
            string checkResult = "Failure";

            TheCoreBankingAuthenticateContext ValidateContext = new TheCoreBankingAuthenticateContext();
            var AppDate = ValidateContext.TblFinanceCurrentDate.FirstOrDefault();
            EODdate = AppDate.CurrentDate;

            bool EODValue = authenticateUnitOfWork.validateEOD.ValidateEODTransactions(EODdate);

            if (EODValue != false)
            {
                authenticateUnitOfWork.validateEOD.insertPendingTransactions();
                checkResult = "Pending Approval";
                return Json(checkResult);
            }
            else
            {
                checkResult = "Successful";
                return Json(checkResult);
            }
        }


        public JsonResult DeleteValidateEod(int id)
        {
            int result = 0;
            result = authenticateUnitOfWork.validateEOD.DeletePendingTransactions(id);

            return Json(result);

        }

        #endregion      

        #region UploadSignature
        public JsonResult AddSignature(UploadSignature upload, RegisterViewModel model)
        {
            using (var db = new TheCoreBanking.Data.Models.TheCoreBankingContext())
            {
                for (int signatureCounter = 0; signatureCounter < upload.Staffsignature.Count; signatureCounter++)
                {
                    TheCoreBanking.Data.Models.TblStaffInformation tblStaffInformation = new TheCoreBanking.Data.Models.TblStaffInformation
                    {
                        //Staffsignature = model.staffInformation.Staffsignature,
                        //ImageData = model.staffInformation.ImageData,
                        Address = model.staffInformation.Address,
                        Age = model.staffInformation.Age,
                        BranchId = model.staffInformation.BranchId,
                        CompanyId = model.staffInformation.CompanyId,
                        Department = model.staffInformation.Department,
                        DeptCode = model.staffInformation.DeptCode,
                        //DesignationCode =Convert.ToInt32(model.staffInformation.DesignationCode),
                        Email = model.staffInformation.Email,
                        Gender = model.staffInformation.Gender,
                        Id = model.staffInformation.Id,
                        JobTitle = model.staffInformation.JobTitle,
                        Miscode = model.staffInformation.Miscode,
                        Nationality = model.staffInformation.Nationality,
                        NextOfKinAddress = model.staffInformation.NextOfKinAddress,
                        NextOfKinEmail = model.staffInformation.NextOfKinEmail,
                        NextOfKinGender = model.staffInformation.NextOfKinGender,
                        NextOfKinName = model.staffInformation.NextOfKinName,
                        NextOfKinPhone = model.staffInformation.NextOfKinPhone,
                        PcCode = model.staffInformation.PcCode,
                        Phone = model.staffInformation.Phone,
                        Rank = model.staffInformation.Rank,
                        RelationShip = model.staffInformation.RelationShip,
                        StaffName = model.staffInformation.StaffName,
                        StaffNo = model.staffInformation.StaffNo,
                        State = model.staffInformation.State,
                        Unit = model.staffInformation.Unit,
                        Updated = model.staffInformation.Updated,
                        UnitCode = model.staffInformation.UnitCode,


                    };
                    using (var stream = new MemoryStream())
                    {
                        upload.Staffsignature[signatureCounter].CopyTo(stream);
                        tblStaffInformation.Staffsignature = stream.ToArray();
                    }
                    db.TblStaffInformation.Add(tblStaffInformation);
                }
                db.SaveChanges();
            }
            return Json(true);
        }
        #endregion
 
        #region Fetch Company Data
        //For Dropdown list for company() box Select2 starts here
        [HttpGet]
        public JsonResult loadCompany()
        {
            Select2Format loadFormat = new Select2Format();                
            var results = db.TblCompanyInformation.ToList();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in results)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.CoyName,                   
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);
        
        }

        [HttpGet]
        public JsonResult loadBranch(string CoyId)
        {
            Select2Format loadFormat = new Select2Format();
            var result = db.TblBranchInformation.ToList().Where(p=>p.CoyId==CoyId).ToList();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.BrName
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }

        [HttpGet]
        public JsonResult loadDept()
        {
            Select2Format loadFormat = new Select2Format();
            var result = db.TblDepartment.ToList();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.Department
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }

        [HttpGet]
        public JsonResult loadRank()
        {
            Select2Format loadFormat = new Select2Format();
            var result = db.TblRank.ToList();

            List<SelectContent> list = new List<SelectContent>();
            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.Rank
                    //brId = item.brId

                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        [HttpGet]
        public JsonResult loadUnit()
        {
            Select2Format loadFormat = new Select2Format();
            var result = db.TblUnit.ToList();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.UnitName
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        [HttpGet]
        public JsonResult loadMis()
        {
            Select2Format loadFormat = new Select2Format();
            var result = db.TblDesignation.ToList();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.Designation
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }

        [HttpGet]
        public JsonResult loadCountry()
        {
            Select2Format loadFormat = new Select2Format();
            var result = db.TblCountry.ToList();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Countryid.ToString(),
                    text = item.Name                                       
                     
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }

        [HttpGet]
        public JsonResult listCountry()
        {
            var result= db.TblCountry.ToList();
            return Json(result);
        }

      [HttpGet]
        public JsonResult LoadState()
        {
            Select2Format loadFormat = new Select2Format();
            var result = db.TblState.ToList();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Stateid.ToString(),
                    text = item.Statename,
                    country ="None"

                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }

        #region Select2 Helper

        public class Select2Format
        {
            public List<SelectContent> results { get; set; }
        }
        public class SelectContent
        {
            public string id { get; set; }
            public string text { get; set; }
            public string country { get; set; }
            public string brId { get; set; }
            public string brLocation { get; set; }
        }
        #endregion





        #endregion

        public class Result
        {
            public string accessToken { get; set; }
            public string encryptedAccessToken { get; set; }
            public int expireInSeconds { get; set; }
            public bool shouldResetPassword { get; set; }
            public object passwordResetCode { get; set; }
            public int userId { get; set; }
            public bool requiresTwoFactorVerification { get; set; }
            public object twoFactorAuthProviders { get; set; }
            public object twoFactorRememberClientToken { get; set; }
            public object returnUrl { get; set; }
            public string refreshToken { get; set; }
            public int refreshTokenExpireInSeconds { get; set; }
        }

        public class Root
        {
            public Result result { get; set; }
            public object targetUrl { get; set; }
            public bool success { get; set; }
            public object error { get; set; }
            public bool unAuthorizedRequest { get; set; }
            public bool __abp { get; set; }
        }
    }
}
