using Newtonsoft.Json;
using System;

namespace WaxWelio.Entities.Data
{
    public class CalendarEvent
    {
        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        /// <value>
        /// The display name of the user.
        /// </value>
        public string UserDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>
        /// The event identifier.
        /// </value>
        [JsonProperty("id")]
        public string EventId { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the body preview.
        /// </summary>
        /// <value>
        /// The body preview.
        /// </value>
        [JsonProperty("bodyPreview")]
        public string BodyPreview { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        [JsonProperty("start")]
        public DateEvent StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        [JsonProperty("end")]
        public DateEvent EndDate { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the attendees.
        /// </summary>
        /// <value>
        /// The attendees.
        /// </value>
        [JsonProperty("attendees")]
        public string Attendees { get; set; }

        /// <summary>
        /// Gets or sets the organizer.
        /// </summary>
        /// <value>
        /// The organizer.
        /// </value>
        [JsonProperty("organizer")]
        public string Organizer { get; set; }

        /// <summary>
        /// Gets or sets the web link.
        /// </summary>
        /// <value>
        /// The web link.
        /// </value>
        [JsonProperty("webLink")]
        public string WebLink { get; set; }

        /// <summary>
        /// Gets or sets the name of the patient.
        /// </summary>
        /// <value>
        /// The name of the patient.
        /// </value>
        [JsonProperty("doctorName")]
        public string DoctorName { get; set; }

        /// <summary>
        /// Gets or sets the name of the patient.
        /// </summary>
        /// <value>
        /// The name of the patient.
        /// </value>
        [JsonProperty("patientName")]
        public string PatientName { get; set; }

        /// <summary>
        /// Gets or sets the meeting URL.
        /// </summary>
        /// <value>
        /// The meeting URL.
        /// </value>
        public string MeetingUrl { get; set; }

        /// <summary>
        /// Gets the encrypted doctor URI.
        /// </summary>
        /// <value>
        /// The encrypted doctor URI.
        /// </value>
        public string EncryptedDoctorUri
        {
            get
            {
                var parameters = MeetingUrl.Split('?')[1] + "&userType=Doctor&displayName=" + DoctorName ?? "Admin";
                return EncryptionHelper.Encrypt(parameters);
            }
        }


        /// <summary>
        /// Gets the encrypted patient URI.
        /// </summary>
        /// <value>
        /// The encrypted patient URI.
        /// </value>
        public string EncryptedPatientUri
        {
            get
            {
                var parameters = MeetingUrl.Split('?')[1] + "&userType=Patient&displayName=" + PatientName ?? "Cloud";
                return EncryptionHelper.Encrypt(parameters);
            }
        }

        /// <summary>
        /// Gets the encrypted meeting.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public string GetEncryptedMeeting(string parameters)
        {
            return EncryptionHelper.Encrypt(parameters);
        }
    }

    public class DateEvent
    {
        /// <summary>
        /// Gets or sets the date time value.
        /// </summary>
        /// <value>
        /// The date time value.
        /// </value>
        public DateTime DateTimeValue { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>
        /// The time zone.
        /// </value>
        public string TimeZone { get; set; }
    }
}
