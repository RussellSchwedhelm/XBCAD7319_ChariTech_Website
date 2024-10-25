using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class AISummary
    {
        public int AISummaryID { get; set; }
        public int ExhortationID { get; set; }
        public string SummaryText { get; set; }
        public string AIProcessingStatus { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }  // Nullable
        public int? SummaryProcessingTime { get; set; }  // Nullable
        public bool IsSummaryEdited { get; set; }

        public AISummary()
        {
            
        }
    }

}