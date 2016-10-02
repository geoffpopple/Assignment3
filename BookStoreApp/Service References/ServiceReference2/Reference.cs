﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStoreApp.ServiceReference2 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Book", Namespace="http://schemas.datacontract.org/2004/07/BookStoreService")]
    [System.SerializableAttribute()]
    public partial class Book : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AuthorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float PriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StockField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int YearField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Author {
            get {
                return this.AuthorField;
            }
            set {
                if ((object.ReferenceEquals(this.AuthorField, value) != true)) {
                    this.AuthorField = value;
                    this.RaisePropertyChanged("Author");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Stock {
            get {
                return this.StockField;
            }
            set {
                if ((this.StockField.Equals(value) != true)) {
                    this.StockField = value;
                    this.RaisePropertyChanged("Stock");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Year {
            get {
                return this.YearField;
            }
            set {
                if ((this.YearField.Equals(value) != true)) {
                    this.YearField = value;
                    this.RaisePropertyChanged("Year");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DeletableField", Namespace="http://schemas.datacontract.org/2004/07/BookStoreService")]
    public enum DeletableField : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        BookName = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Year = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Id = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SearchableField", Namespace="http://schemas.datacontract.org/2004/07/BookStoreService")]
    public enum SearchableField : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        BookName = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Year = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Id = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AuthorName = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference2.IBookStore")]
    public interface IBookStore {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookStore/GetAllBooks", ReplyAction="http://tempuri.org/IBookStore/GetAllBooksResponse")]
        BookStoreApp.ServiceReference2.Book[] GetAllBooks();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookStore/GetAllBooks", ReplyAction="http://tempuri.org/IBookStore/GetAllBooksResponse")]
        System.Threading.Tasks.Task<BookStoreApp.ServiceReference2.Book[]> GetAllBooksAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookStore/AddBook", ReplyAction="http://tempuri.org/IBookStore/AddBookResponse")]
        bool AddBook(string id, string bookName, string author, string year, string price, string stock);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookStore/AddBook", ReplyAction="http://tempuri.org/IBookStore/AddBookResponse")]
        System.Threading.Tasks.Task<bool> AddBookAsync(string id, string bookName, string author, string year, string price, string stock);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookStore/DeleteBook", ReplyAction="http://tempuri.org/IBookStore/DeleteBookResponse")]
        bool DeleteBook(BookStoreApp.ServiceReference2.DeletableField field, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookStore/DeleteBook", ReplyAction="http://tempuri.org/IBookStore/DeleteBookResponse")]
        System.Threading.Tasks.Task<bool> DeleteBookAsync(BookStoreApp.ServiceReference2.DeletableField field, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookStore/SearchBooks", ReplyAction="http://tempuri.org/IBookStore/SearchBooksResponse")]
        BookStoreApp.ServiceReference2.Book[] SearchBooks(BookStoreApp.ServiceReference2.SearchableField field, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookStore/SearchBooks", ReplyAction="http://tempuri.org/IBookStore/SearchBooksResponse")]
        System.Threading.Tasks.Task<BookStoreApp.ServiceReference2.Book[]> SearchBooksAsync(BookStoreApp.ServiceReference2.SearchableField field, string value);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBookStoreChannel : BookStoreApp.ServiceReference2.IBookStore, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BookStoreClient : System.ServiceModel.ClientBase<BookStoreApp.ServiceReference2.IBookStore>, BookStoreApp.ServiceReference2.IBookStore {
        
        public BookStoreClient() {
        }
        
        public BookStoreClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BookStoreClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BookStoreClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BookStoreClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public BookStoreApp.ServiceReference2.Book[] GetAllBooks() {
            return base.Channel.GetAllBooks();
        }
        
        public System.Threading.Tasks.Task<BookStoreApp.ServiceReference2.Book[]> GetAllBooksAsync() {
            return base.Channel.GetAllBooksAsync();
        }
        
        public bool AddBook(string id, string bookName, string author, string year, string price, string stock) {
            return base.Channel.AddBook(id, bookName, author, year, price, stock);
        }
        
        public System.Threading.Tasks.Task<bool> AddBookAsync(string id, string bookName, string author, string year, string price, string stock) {
            return base.Channel.AddBookAsync(id, bookName, author, year, price, stock);
        }
        
        public bool DeleteBook(BookStoreApp.ServiceReference2.DeletableField field, string value) {
            return base.Channel.DeleteBook(field, value);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteBookAsync(BookStoreApp.ServiceReference2.DeletableField field, string value) {
            return base.Channel.DeleteBookAsync(field, value);
        }
        
        public BookStoreApp.ServiceReference2.Book[] SearchBooks(BookStoreApp.ServiceReference2.SearchableField field, string value) {
            return base.Channel.SearchBooks(field, value);
        }
        
        public System.Threading.Tasks.Task<BookStoreApp.ServiceReference2.Book[]> SearchBooksAsync(BookStoreApp.ServiceReference2.SearchableField field, string value) {
            return base.Channel.SearchBooksAsync(field, value);
        }
    }
}
