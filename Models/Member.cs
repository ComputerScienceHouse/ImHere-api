namespace ImHereAPI.Models {
    public class Member {
        public int exp { get; set; }
        public int auth_time { get; set; }
        public string jti { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }
        public string sub { get; set; }
        public string typ { get; set; }
        public string azp { get; set; }
        public string nonce { get; set; }
        public string session_state { get; set; }
        public string acr { get; set; }
        //public string[] allowed_origins { get; set; }
        public RealmAccess realm_access { get; set; }
        public Dictionary<string, ResourceAccess> resource_access { get; set; }
        public string scope { get; set; }
        public string sid { get; set; }
        public bool email_verified { get; set; }
        public string name { get; set; }
        public string[] groups { get; set; }
        public string preferred_username { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string email { get; set; }
    }

    public class RealmAccess {
        public string[] roles { get; set; }
    }

    public class ResourceAccess {
        public string[] roles { get; set; }
    }

}