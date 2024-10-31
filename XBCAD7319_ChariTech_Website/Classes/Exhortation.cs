using System;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class Exhortation
    {
        public int ExhortationID { get; set; }
        public int UploadedUserID { get; set; }
        public int ChurchID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Speaker { get; set; }
        public string TranscriptionPrompt { get; set; }  // Nullable string for any transcription instructions or prompts
        public string SummaryPrompt { get; set; }        // Nullable string for any summary instructions or prompts
        public byte[] AudioFile { get; set; }            // Nullable byte array for audio data
        public string AITranscriptionText { get; set; }  // Nullable string for transcription text
        public string AISummaryText { get; set; }        // Nullable string for summary text
        //---------------------------------------------------------------------------------------------------------------------//
        public Exhortation()
        {
            // Constructor logic if any additional setup is required
        }
        //---------------------------------------------------------------------------------------------------------------------//
    }
}
//END OF PAGE---------------------------------------------------------------------------------------------------------------------//