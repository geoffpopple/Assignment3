using System.Runtime.Serialization;

namespace BookStoreService
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public float Price { get; set; }
        [DataMember]
        public int Stock { get; set; }
    }
}