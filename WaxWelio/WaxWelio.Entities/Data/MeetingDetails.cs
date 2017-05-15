using System;

namespace WaxWelio.Entities.Data
{
    public class MeetingDetails
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the on line meeting identifier.
        /// </summary>
        /// <value>
        /// The on line meeting identifier.
        /// </value>
        public string OnLineMeetingId { get; set; }

        /// <summary>
        /// Gets or sets the online meeting URI.
        /// </summary>
        /// <value>
        /// The online meeting URI.
        /// </value>
        public string OnlineMeetingUri { get; set; }

        /// <summary>
        /// Gets or sets the online meeting URL.
        /// </summary>
        /// <value>
        /// The online meeting URL.
        /// </value>
        public string OnlineMeetingUrl { get; set; }

        /// <summary>
        /// Gets or sets the organizer.
        /// </summary>
        /// <value>
        /// The organizer.
        /// </value>
        public string Organizer { get; set; }

        /// <summary>
        /// Gets or sets the attendees.
        /// </summary>
        /// <value>
        /// The attendees.
        /// </value>
        public string Attendees { get; set; }

        /// <summary>
        /// Gets or sets the join URL.
        /// </summary>
        /// <value>
        /// The join URL.
        /// </value>
        public string JoinUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the patient.
        /// </summary>
        /// <value>
        /// The name of the patient.
        /// </value>
        public string PatientName { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the appointment date.
        /// </summary>
        /// <value>
        /// The appointment date.
        /// </value>
        public string AppointmentDate { get; set; }

        /// <summary>
        /// Gets or sets the questionnaire required.
        /// </summary>
        /// <value>
        /// The questionnaire required.
        /// </value>
        public string QuestionnaireRequired { get; set; }

        /// <summary>
        /// Gets or sets the question category.
        /// </summary>
        /// <value>
        /// The question category.
        /// </value>
        public string QuestionCategory { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the name of the doctors.
        /// </summary>
        /// <value>
        /// The name of the doctors.
        /// </value>
        public string DoctorsName { get; set; }
    }
}
