using System;
using System.Collections.Generic;
using System.Text;

namespace TheCoreBanking.Authenticate.Data.Model
{
   public class EndofDayModel
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public DateTime ydate { get; set; }
        public DateTime newdate { get; set; }
        public string deals { get; set; }
        public decimal product { get; set; }
        public decimal sumproduct { get; set; }
        public int year { get; set; }
        public DateTime CheckDate { get; set; }
        public decimal iprincipal { get; set; }
        public decimal interestRate { get; set; }
        public DateTime effectiveDate { get; set; }
        public int countTemp { get; set; }
        public int kounter { get; set; }
        public string DealID { get; set; }
        public DateTime LastEOD { get; set; }
        public int Tenor { get; set; }
        public decimal iaccruedTodate { get; set; }
        public DateTime startingDate { get; set; }
        public int countDown { get; set; }
        public bool BackDate { get; set; }
        public DateTime NextWorkingDate { get; set; }
        public int DayDiff { get; set; }
        public DateTime today { get; set; }
        public string CoyCode { get; set; }
        public string Time { get; set; }
        public string Ref { get; set; }
        public string ErrorMsg { get; set; }
        public string ErrorSp_Name { get; set; }
        public int ErrorLine { get; set; }
        public int ErrorNumber { get; set; }
        public int ErrorState { get; set; }
    }
}
