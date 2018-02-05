using System;

namespace Comidat.Data.Model
{
    public class TBLTag
    {
       public long Id { get; set; }

       public string TagFirstName { get; set; }

       public string TagLastName { get; set; }

       public string TagFullName { get; set; }

       public string TagMacAddress { get; set; }

       public int TagNumber { get; set; }

       public bool TagActive { get; set; }

       public string TagStaticTelephone { get; set; }

       public string TagMobilTelephone { get; set; }

       public string TagDescription { get; set; }

       public string TagTCNo { get; set; }

       public int? RecordUserId { get; set; }

       public DateTime RecordDateTime { get; set; }

       public int? UpdateUserId { get; set; }

       public DateTime? UpdateDateTime { get; set; }

       public bool Deleted { get; set; }
    }
}