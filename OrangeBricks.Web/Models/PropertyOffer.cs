using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Models
{
    public class PropertyOffer
    {
        public bool IsAccepted { get; set; }
        public DateTime? AcceptDate { get; internal set; }
    }
}