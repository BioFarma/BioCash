using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BioTemplate.Model.Object
{
    public class Workflow
    {
        public int SupplierId { set; get; }
        public int DocumentId {set;get;}
        public string DocumentCode { set; get; }
        public int SequenceId { set; get; }
        public int GroupId { set; get; }
        public int  PositionId { set; get; }
        public string NIK { set; get; }
        public int Status { set; get; }
        public int IsCyto { set;get; }
    }
}