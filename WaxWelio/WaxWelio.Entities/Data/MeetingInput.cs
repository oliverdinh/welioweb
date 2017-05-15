﻿
namespace WaxWelio.Entities.Data
{
    public class MeetingInput
    {
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the access level.
        /// </summary>
        /// <value>
        /// The access level.
        /// </value>
        public string AccessLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [entry exit announcement].
        /// </summary>
        /// <value>
        /// <c>true</c> if [entry exit announcement]; otherwise, <c>false</c>.
        /// </value>
        public bool EntryExitAnnouncement { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic leader assignment].
        /// </summary>
        /// <value>
        /// <c>true</c> if [automatic leader assignment]; otherwise, <c>false</c>.
        /// </value>
        public bool AutomaticLeaderAssignment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [phone user admission].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [phone user admission]; otherwise, <c>false</c>.
        /// </value>
        public bool PhoneUserAdmission { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [lobby bypass for phone users].
        /// </summary>
        /// <value>
        /// <c>true</c> if [lobby bypass for phone users]; otherwise, <c>false</c>.
        /// </value>
        public bool LobbyBypassForPhoneUsers { get; set; }

        /// <summary>
        /// Gets or sets the leaders.
        /// </summary>
        /// <value>
        /// The leaders.
        /// </value>
        public string[] Leaders { get; set; }
    }
}
