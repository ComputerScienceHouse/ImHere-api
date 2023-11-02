namespace ImHere_api.Models {
    public enum MeetingType {
        Chairman,
        Ad_Hoc,
        Evaluations,
        Financial,
        ResearchAndDevelopment,
        HouseImprovements,
        OpComm,
        History,
        Social,
        PublicRelations,
        TechnicalSeminar
    }

    public class Attendance {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Member[] Attendees { get; set; }

    }
}
