
/*/////////////////////////////////
///    Geoff Popple S4208241    ///
///    INFS 3204:Assignmet 3    ///
/////////////////////////////////*/

//Book.cs

//Task 1 Required Class
using System.Runtime.Serialization;

namespace BookStoreService
{
    [DataContract]
    //used to restrict the fields that can be searched on
    public enum SearchableField
    {
        [EnumMember]
        BookName,
        [EnumMember]
        Year,
        [EnumMember]
        Id,
        [EnumMember]
        AuthorName
    }
    //used to restrict the fields that can be the source of a delete
    public enum DeletableField
    {
        [EnumMember]
        BookName,
        [EnumMember]
        Year,
        [EnumMember]
        Id,
        [EnumMember]
        Num
    }

    //Task 1.1.1
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