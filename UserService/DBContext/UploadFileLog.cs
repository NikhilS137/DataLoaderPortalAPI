using System;
using System.Collections.Generic;

namespace UserService.DBContext
{
    public partial class UploadFileLog
    {
        public int FileUploadId { get; set; }
        public string FileName { get; set; } = null!;
        public string? ServerFileName { get; set; }
        public string? Status { get; set; }
        public int? SavedRecordsCount { get; set; }
        public int? ValidationFailedRecordsCount { get; set; }
        public int? TotalRecordsCount { get; set; }
        public string? FileLocation { get; set; }
        public string? ValidationFailedFileName { get; set; }
        public string? ValidationFailedFileLocation { get; set; }
        public int? CreatedBy { get; set; }
        public int? CreatedDate { get; set; }
    }
}
