namespace ImHereAPI.Models {
    public class Attendance {
        private static uint gid = 0;
        private static readonly object gidlock = new();

        public uint ID { get; init; }
        public string Name { get; init; }
        public DateTime Date { get; init; }
        public Dictionary<string, Member> Members { get; init; }
        public List<string> Blacklist { get; init; }
        public string HostUID { get; set; }

        public Attendance(string name)
        {
            lock (gidlock)
                ID = gid++;
            Members = new();
            Blacklist = new();
            Name = name;
            Date = DateTime.Now;
            HostUID = string.Empty;
        }
    }
}
