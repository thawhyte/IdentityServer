using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoreBanking.Authenticate.Models
{
    public class DemoResponse<T>
    {
        public string Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }

        public static DemoResponse<T> GetResult(string code, string msg)
        {
            return new DemoResponse<T>
            {
                Code = code,
                Msg = msg,
                //Data = data
            };
        }
    }
}