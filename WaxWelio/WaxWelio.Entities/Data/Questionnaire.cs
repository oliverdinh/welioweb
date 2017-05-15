using System.Collections.Generic;

public class Questionnaire
{
    public int QuestionnaireId { get; set; }

    public string Question { get; set; }

    public IList<string> Options { get; set; }

    public bool AllowMultipleChoice { get; set; }

    public string SelectedOptions { get; set; }

    public string MeetingId { get; set; }
}