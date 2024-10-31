using System;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class AITranscription
    {
        public int AITranscriptionID { get; set; }
        public int ExhortationID { get; set; }
        public string TranscriptionText { get; set; }
        public string AIProcessingStatus { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }  // Nullable
        public int? TranscriptionProcessingTime { get; set; }  // Nullable
        public bool IsTranscriptionEdited { get; set; }

        public AITranscription()
        {

        }
    }
}
//END OF PAGE---------------------------------------------------------------------------------------------------------------------//