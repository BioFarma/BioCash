namespace BioTemplate.Model.Object
{
    public class Files
    {
        private int _fileId;
        private string _fileName;
        private string _fileOriginal; 
        private string _filePath;
        private int _fileSize;
        private string _fileType;
        private int _referenceId;
        private string _referenceCode;

        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FileOriginal { get; set; }
        public string FilePath { get; set; }
        public int FileSize { get; set; }
        public string FileType { get; set; }
        public int ReferenceId { get; set; }
        public string ReferenceCode { get; set; }
        
    }

    public class AttachmenteProc
    {
        public int PackageId { get; set; }
        public string PackageCode { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public int FileId { get; set; }
    }
}