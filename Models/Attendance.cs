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

    public class Attendance : SerializableModel<Attendance> {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<Member> Attendees { get; set; }

    }
}
