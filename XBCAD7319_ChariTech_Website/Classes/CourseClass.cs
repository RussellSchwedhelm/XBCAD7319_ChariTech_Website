namespace XBCAD7319_ChariTech_Website.Classes
{
    public class CourseClass
    {
        public string CourseTitle { get; set; }
        public string Theme { get; set; }
        public string Duration { get; set; }
        public string DateUploaded { get; set; }
        public string Description { get; set; }

        public byte[] PdfFileContent { get; set; }

        public string PdfFileUrl { get; set; }

        //For tracking which church it belongs to
        public int ChurchID { get; set; }



    }
}