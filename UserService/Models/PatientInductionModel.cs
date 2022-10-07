using System.Text.RegularExpressions;

namespace UserService.Models
{
    public class PatientInductionModel
    {
         public string patientName  { get; set; }
         public string add1  { get; set; }
         public string add2  { get; set; }
         public string add3  { get; set; }
         public string district  { get; set; }
         public string state  { get; set; }
         public string country  { get; set; }
         public string DOB  { get; set; }
         public string emailID  { get; set; }
         public string phoneNo  { get; set; }
         public string drugID  { get; set; }
         public string drugName  { get; set; }


        public bool CheckValidations( out string errorMsg)
        {
            string errorMessage = "";

            if (string.IsNullOrEmpty(patientName))
                errorMessage += "Patient name is required ,";
            if (string.IsNullOrEmpty(add1))
                errorMessage += "Add1 is required ,";
            if (string.IsNullOrEmpty(district))
                errorMessage += "District is required ,";
            if (string.IsNullOrEmpty(state))
                errorMessage += "State is required ,";
            if (string.IsNullOrEmpty(country))
                errorMessage += "Country is required ,";
            if (string.IsNullOrEmpty(DOB))
                errorMessage += "DOB is required ,";
            if (string.IsNullOrEmpty(emailID))
                errorMessage += "EmailID is required ,";
            if (string.IsNullOrEmpty(phoneNo))
                errorMessage += "PhoneNo is required ,";
            if (string.IsNullOrEmpty(drugID))
                errorMessage += "DrugID is required ,";
            if (string.IsNullOrEmpty(drugName))
                errorMessage += "DrugName is required ,";


            if(errorMessage == "")
            {
                string pattern = @"^[a-zA-Z ]{5,30}$";
                var match = Regex.Match(patientName, pattern, RegexOptions.IgnoreCase);

                if (!match.Success)
                {
                    // does not match
                    errorMessage += "Patient name accepts only aplabates and white spaces and range between 5 to 30 ,";
                }

                string phoneNumberPattern = @"^[0-9]{10}$";
                var matchPhoneNumber = Regex.Match(phoneNo, phoneNumberPattern, RegexOptions.IgnoreCase);

                if (!matchPhoneNumber.Success)
                {
                    // does not match
                    errorMessage += "Phone number should contain only numbers and length should be 10 digits ,";
                }

                string emailIdPattern = @"^[A-Za-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
                var matchEmailID = Regex.Match(emailID, emailIdPattern, RegexOptions.IgnoreCase);

                if (!matchEmailID.Success)
                {
                    // does not match
                    errorMessage += "Invalid email id ,";
                } 
                
                string drugIdPattern = @"^\(?([0-9]{5})\)?[-]([0-9]{4})[-]([0-9]{2})$";
                var matchDrugID = Regex.Match(drugID, drugIdPattern, RegexOptions.IgnoreCase);

                if (!matchDrugID.Success)
                {
                    // does not match
                    errorMessage += "Invalid drug id ,";
                }

                try
                {
                    string date = Convert.ToDateTime(DOB).ToString("MM/dd/yyyy").Replace("-","/");
                    string dobMMddyyyy = @"^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$";
                    var matchDOB = Regex.Match(date, dobMMddyyyy, RegexOptions.IgnoreCase);

                    if (!matchDOB.Success)
                    {
                        // does not match
                        errorMessage += "Please enter DOB in MM/dd/yyyy format ,";
                    }
                }
                catch (Exception ex)
                {
                    errorMessage += "Incorrect date ,";
                }
            }

            errorMsg = errorMessage;

            return errorMessage == "" ? true : false;

        }
    }
}
