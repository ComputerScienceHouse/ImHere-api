using System.Text.Json;

namespace ImHere_api.Models {
    public abstract class SerializableModel<T> {
        public int Id { get; set; }
        public string Serialize() 
            => JsonSerializer.Serialize(this);

        public T Deserialize(string json) 
            => JsonSerializer.Deserialize<T>(json)!;
    }
}
