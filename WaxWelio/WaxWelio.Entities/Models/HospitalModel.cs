using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WaxWelio.Entities.Models
{
    public class HospitalModel : BaseModel
    {
        [JsonProperty(PropertyName = "name")]
        [Required(ErrorMessage = "Clinic name is required.")]
        [Display(Name = "Clinic name")]
        public string ClinicName { get; set; }

        [Required(ErrorMessage = "Address line 1 is required.")]
        [Display(Name = "Address line 1")]
        [JsonProperty(PropertyName = "address")]
        public string AddressLine1 { get; set; }

        [JsonProperty(PropertyName = "address2")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Post code is required.")]
        [Display(Name = "Post Code")]
        [JsonProperty("postCode")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [Display(Name = "State")]
        [JsonProperty("suburb")]
        public string Suburb { get; set; }


        [Display(Name = "Phone")]
        [JsonProperty("phones")]
        public List<string> PhoneNumber { get; set; }

        [Display(Name = "Phone number")]
        [RegularExpression(@"^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$", ErrorMessage = "Phone number is invalid.")]
        [Required(ErrorMessage = "Phone number is required.")]
        public string Phone { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("signUpStatus")]
        public int SignUpStatus { get; set; }

        public string StrPhone => PhoneNumber.Count > 0 ? PhoneNumber[0] : "";

        public string StrSignUpStatus { get; set; }

        [JsonProperty("whenCreated")]
        public long CreatedDate { get; set; }

        public DateTime DCreateDate => CreatedDate.FromTimeStamp();
        public string SCreatedDate { get; set; }

        [JsonProperty("whenUpdated")]
        public long UpdatedDate { get; set; }
    }
}