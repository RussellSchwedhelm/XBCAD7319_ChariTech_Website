using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class Exhortation
    {
        public int ExhortationID { get; set; }
        public int UploadedUserID { get; set; }
        public int ChurchID { get; set; }
        public int? AITranscriptionID { get; set; }  // Nullable int
        public int? AISummaryID { get; set; }        // Nullable int
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Speaker { get; set; }
        public string TranscriptionPrompt { get; set; }  // Nullable string
        public string SummaryPrompt { get; set; }        // Nullable string
        public byte[] AudioFile { get; set; }            // Nullable varbinary (byte array)


        public Exhortation()
        {
            
        }
    }

}