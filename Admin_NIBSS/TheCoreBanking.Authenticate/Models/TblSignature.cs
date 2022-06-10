using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoreBanking.Authenticate.Models
{
    public class TblSignature
    {
       
            public int FileId { get; set; }
            public string ProductAcctNo { get; set; }
            public string Description { get; set; }
            public byte[] FileData { get; set; }
            public string Mime { get; set; }
            public bool? IsDeleted { get; set; }
     
    }
}
