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
                errorMessage += "patient name is required ,";
            if (string.IsNullOrEmpty(add1))
                errorMessage += "add1 is required ,";
            if (string.IsNullOrEmpty(district))
                errorMessage += "district is required ,";
            if (string.IsNullOrEmpty(state))
                errorMessage += "state is required ,";
            if (string.IsNullOrEmpty(country))
                errorMessage += "country is required ,";
            if (string.IsNullOrEmpty(DOB))
                errorMessage += "DOB is required ,";
            if (string.IsNullOrEmpty(emailID))
                errorMessage += "emailID is required ,";
            if (string.IsNullOrEmpty(phoneNo))
                errorMessage += "phoneNo is required ,";
            if (string.IsNullOrEmpty(drugID))
                errorMessage += "drugID is required ,";
            if (string.IsNullOrEmpty(drugName))
                errorMessage += "drugName is required ,";


            if(errorMessage == "")
            {
                string pattern = @"^[a-zA-Z ]{5,30}$";
                var match = Regex.Match(patientName, pattern, RegexOptions.IgnoreCase);

                if (!match.Success)
                {
                    // does not match
                    errorMessage += "patient name accepts only aplabates and white spaces and range between 5 to 30 ,";
                }

                string phoneNumberPattern = @"^[0-9]{10}$";
                var matchPhoneNumber = Regex.Match(phoneNo, phoneNumberPattern, RegexOptions.IgnoreCase);

                if (!matchPhoneNumber.Success)
                {
                    // does not match
                    errorMessage += "phone number should contain only numbers and length should be 10 digits ,";
                }

                string emailIdPattern = @"^[A-Za-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
                var matchEmailID = Regex.Match(emailID, emailIdPattern, RegexOptions.IgnoreCase);

                if (!matchEmailID.Success)
                {
                    // does not match
                    errorMessage += "invalid email id ,";
                }

                try
                {
                    string date = Convert.ToDateTime(DOB).ToString("MM/dd/yyyy").Replace("-","/");
                    string dobMMddyyyy = @"^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$";
                    var matchDOB = Regex.Match(date, dobMMddyyyy, RegexOptions.IgnoreCase);

                    if (!matchDOB.Success)
                    {
                        // does not match
                        errorMessage += "please enter DOB in MM/dd/yyyy format ,";
                    }
                }
                catch (Exception ex)
                {
                    errorMessage += "incorrect date ,";
                }
            }

            errorMsg = errorMessage;

            return errorMessage == "" ? true : false;

        }
    }
}
