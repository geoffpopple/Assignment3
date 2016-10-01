using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BookStoreService
{
    [DataContract]
    //comment
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

    public enum DeletableField
    {
        [EnumMember]
        BookName,
        [EnumMember]
        Year,
        [EnumMember]
        Id
    }

    [DataContract]
    public class Book:IEqualityComparer<Book>
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

        public bool Equals(Book x, Book y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            if (object.ReferenceEquals(x, null) ||
                object.ReferenceEquals(y, null))
            {
                return false;
            }
            return x.Id == y.Id;
        }

        public int GetHashCode(Book obj)
        {
            return obj?.Id.GetHashCode() ?? 0;
        }
    }
}