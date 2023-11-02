namespace ImHere_api.Models {
    public class Member {
        public int Exp { get; set; }
        public int AuthTime { get; set; }
        public string Jti { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }
        public string Sub { get; set; }
        public string Typ { get; set; }
        public string Azp { get; set; }
        public string Nonce { get; set; }
        public string SessionState { get; set; }
        public string Acr { get; set; }
        public List<string> AllowedOrigins { get; set; }
        public RealmAccess RealmAccess { get; set; }
        public Dictionary<string, ResourceAccess> ResourceAccess { get; set; }
        public string Scope { get; set; }
        public string Sid { get; set; }
        public bool EmailVerified { get; set; }
        public string Name { get; set; }
        public List<string> Groups { get; set; }
        public string PreferredUsername { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
    }

    public class RealmAccess {
        public List<string> Roles { get; set; }
    }

    public class ResourceAccess {
        public List<string> Roles { get; set; }
    }
}