using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using System.Data.OleDb;
using UserService.DBContext;

namespace UserService.Models
{
    public class FileUploadModel
    {
        public string FileName { get; set; }
        public string strFile { get; set; }

        public string SaveFileOnServer(out int id)
        {
            id = 0;
            string[] strs = strFile.Split(",");

            string base64code = strFile.Replace("data:application/vnd.ms-excel;base64,", "");
            if (strs.Count() > 1)
                base64code = strs[1];
            var filesPath = Directory.GetCurrentDirectory() + "\\Uploadfiles";
            if (!System.IO.Directory.Exists(filesPath))//create path 
            {
                Directory.CreateDirectory(filesPath);
            }
            string filename = "excel_"+ DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";

            var path = Path.Combine(filesPath, filename);//the path to upload

            byte[] fileinbytes = Convert.FromBase64String(base64code);
            File.WriteAllBytes(path, fileinbytes);

            SaveFileUploadLog(FileName, filename, path, out id);

            return path;
        }
        private void SaveFileUploadLog(string fileName, string serverFileName,
            string fileLocation, out int id)
        {
            // insert
            using (var db = new DBDataLoaderPortalContext())
            {
                var uploadFileLogs = db.Set<UploadFileLog>();

                var fileLog = new UploadFileLog()
                {
                    FileName = fileName,
                    ServerFileName = serverFileName,
                    Status = "Pending",
                    SavedRecordsCount = 0,
                    ValidationFailedRecordsCount = 0,
                    TotalRecordsCount = 0,
                    FileLocation = fileLocation,
                };

                uploadFileLogs.Add(fileLog);

                db.SaveChanges();

                id = fileLog.FileUploadId;

            }
        }
        public  DataTable ConvertExcelToDataTable(string FileName)
        {
            DataTable dtResult = null;
            int totalSheet = 0; //No of sheets on excel file  
            using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName
                + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];
                objConn.Close();
                return dtResult; //Returning Dattable  
            }
        }

        public bool SaveRecordsInDB(string FileName, int Fileid)
        {
            bool retVal = false;
            try
            {
                int totalCount = 0;
                int savedRecordsCount = 0;
                int validationFailedRecordsCount = 0;

                DataTable dt = new DataTable();
                //dt = ConvertExcelToDataTable(FileName);
                dt = ExcelToDT(FileName);

                PatientInductionModel patientInductionModel = new PatientInductionModel();
                string errorMsg = "";

                if (dt.Rows.Count > 0)
                {
                    totalCount = dt.Rows.Count; 
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        patientInductionModel.patientName = dt.Rows[i]["Patient_Name"].ToString();
                        patientInductionModel.add1 = dt.Rows[i]["Address1"].ToString();
                        patientInductionModel.add2 = dt.Rows[i]["Address2"].ToString();
                        patientInductionModel.add3 = dt.Rows[i]["Address3"].ToString();
                        patientInductionModel.district = dt.Rows[i]["District"].ToString();
                        patientInductionModel.state = dt.Rows[i]["State"].ToString();
                        patientInductionModel.country = dt.Rows[i]["Country"].ToString();
                        patientInductionModel.DOB = dt.Rows[i]["DOB"].ToString();
                        patientInductionModel.emailID = dt.Rows[i]["Email_Id"].ToString();
                        patientInductionModel.phoneNo = dt.Rows[i]["Phone_Number"].ToString();
                        patientInductionModel.drugID = dt.Rows[i]["Drug_ID"].ToString();
                        patientInductionModel.drugName = dt.Rows[i]["Drug_Name"].ToString();


                        if (patientInductionModel.CheckValidations(out errorMsg))
                        {
                            // insert
                            using (var db = new DBDataLoaderPortalContext())
                            {
                                var patientMasters = db.Set<PatientMaster>();
                                patientMasters.Add(new PatientMaster
                                {
                                    PatientName = patientInductionModel.patientName,
                                    Address1 = patientInductionModel.add1,
                                    Address2 = patientInductionModel.add2,
                                    Address3 = patientInductionModel.add3,
                                    District = patientInductionModel.district,
                                    State = patientInductionModel.state,
                                    Country = patientInductionModel.country,
                                    Dob = Convert.ToDateTime(patientInductionModel.DOB),
                                    EmailId = patientInductionModel.emailID,
                                    PhoneNumber = patientInductionModel.phoneNo,
                                    DrugId = patientInductionModel.drugID,
                                    DrugName = patientInductionModel.drugName,
                                    Status = "Pending",
                                    IsActive = true,
                                    CreatedDate = DateTime.Now,
                                    FileId = Fileid
                                });

                                db.SaveChanges();
                                savedRecordsCount += 1;
                            }

                        }
                        else
                        {
                            // Save Data in validation failed table
                            using (var db = new DBDataLoaderPortalContext())
                            {
                                var validationFailedMasters = db.Set<ValidationFailedMaster>();
                                validationFailedMasters.Add(new ValidationFailedMaster
                                {
                                    PatientName = patientInductionModel.patientName,
                                    Address1 = patientInductionModel.add1,
                                    Address2 = patientInductionModel.add2,
                                    Address3 = patientInductionModel.add3,
                                    District = patientInductionModel.district,
                                    State = patientInductionModel.state,
                                    Country = patientInductionModel.country,
                                    Dob = patientInductionModel.DOB,
                                    EmailId = patientInductionModel.emailID,
                                    PhoneNumber = patientInductionModel.phoneNo,
                                    DrugId = patientInductionModel.drugID,
                                    DrugName = patientInductionModel.drugName,
                                    ErrorMessage = errorMsg,
                                    Status = "Validation Failed",
                                    IsActive = true,
                                    CreatedDate = DateTime.Now,
                                    FileId = Fileid
                                });

                                db.SaveChanges();
                                validationFailedRecordsCount += 1;
                            }
                        }
                    }
                }
                retVal = true;
               bool val = updateFileLog(Fileid, "Completed", savedRecordsCount, validationFailedRecordsCount, totalCount);
            }
            catch (Exception ex)
            {
                //Something went wrong 
                bool val = updateFileLog(Fileid, "Failed", 0, 0, 0);
            }

            return retVal;
        }

        private bool updateFileLog(int FileId,string status,int savedRecordsCount,
            int validationFailedCount, int totalCount)
        {
            bool retVal = false;    
            var fileUploadRecord = new UploadFileLog()
            {
                FileUploadId = FileId,  
                Status = status,
                SavedRecordsCount = savedRecordsCount,
                ValidationFailedRecordsCount = validationFailedCount,
                TotalRecordsCount = totalCount
            };

            using (var db = new DBDataLoaderPortalContext())
            {
                db.UploadFileLogs.Attach(fileUploadRecord);
                db.Entry(fileUploadRecord).Property(x => x.Status).IsModified = true;
                db.Entry(fileUploadRecord).Property(x => x.SavedRecordsCount).IsModified = true;
                db.Entry(fileUploadRecord).Property(x => x.ValidationFailedRecordsCount).IsModified = true;
                db.Entry(fileUploadRecord).Property(x => x.TotalRecordsCount).IsModified = true;
                db.SaveChanges();
                retVal=true;
            }

            return retVal;


            //using (var db = new DBDataLoaderPortalContext())
            //{
            //    var uploadFileLogs = db.Set<UploadFileLog>();

            //    var record = uploadFileLogs.Where(x => x.FileUploadId == FileId).SingleOrDefault();

            //    db.UploadFileLogs.Attach(record);
            //    db.Entry(record).Property(x => x.Password).IsModified = true;
            //    db.SaveChanges();

            //    var fileLog = new UploadFileLog()
            //    {
            //        FileName = fileName,
            //        ServerFileName = serverFileName,
            //        Status = "Pending",
            //        SavedRecordsCount = 0,
            //        ValidationFailedRecordsCount = 0,
            //        TotalRecordsCount = 0,
            //        FileLocation = fileLocation,
            //    };

            //    uploadFileLogs.Add(fileLog);

            //    db.SaveChanges();

            //    id = fileLog.FileUploadId;

            //}
        }

        private DataTable ExcelToDT(string filepath)
        {
            //Create a new DataTable.
            DataTable dt = new DataTable();

            string filePath = filepath;

            //Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

               

                //Loop through the Worksheet rows.
                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }
            }

            dt = dt.Rows
                        .Cast<DataRow>()
                        .Where(row => !row.ItemArray.All(field => field is DBNull ||
                                                         string.IsNullOrWhiteSpace(field as string)))
                        .CopyToDataTable();

            return dt;
        }

    }
}
