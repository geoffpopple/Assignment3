﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStoreApp.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IBookPurchasesvc")]
    public interface IBookPurchasesvc {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookPurchasesvc/PurchaseBooks", ReplyAction="http://tempuri.org/IBookPurchasesvc/PurchaseBooksResponse")]
        BookStoreApp.ServiceReference1.BookPurchaseResponse PurchaseBooks(BookStoreApp.ServiceReference1.BookPurchaseInfo request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookPurchasesvc/PurchaseBooks", ReplyAction="http://tempuri.org/IBookPurchasesvc/PurchaseBooksResponse")]
        System.Threading.Tasks.Task<BookStoreApp.ServiceReference1.BookPurchaseResponse> PurchaseBooksAsync(BookStoreApp.ServiceReference1.BookPurchaseInfo request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BookPurchaseInfo", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class BookPurchaseInfo {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public float budget;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public System.Collections.Generic.Dictionary<int, int> items;
        
        public BookPurchaseInfo() {
        }
        
        public BookPurchaseInfo(float budget, System.Collections.Generic.Dictionary<int, int> items) {
            this.budget = budget;
            this.items = items;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BookPurchaseResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class BookPurchaseResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string response;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public bool result;
        
        public BookPurchaseResponse() {
        }
        
        public BookPurchaseResponse(string response, bool result) {
            this.response = response;
            this.result = result;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBookPurchasesvcChannel : BookStoreApp.ServiceReference1.IBookPurchasesvc, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BookPurchasesvcClient : System.ServiceModel.ClientBase<BookStoreApp.ServiceReference1.IBookPurchasesvc>, BookStoreApp.ServiceReference1.IBookPurchasesvc {
        
        public BookPurchasesvcClient() {
        }
        
        public BookPurchasesvcClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BookPurchasesvcClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BookPurchasesvcClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BookPurchasesvcClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public BookStoreApp.ServiceReference1.BookPurchaseResponse PurchaseBooks(BookStoreApp.ServiceReference1.BookPurchaseInfo request) {
            return base.Channel.PurchaseBooks(request);
        }
        
        public System.Threading.Tasks.Task<BookStoreApp.ServiceReference1.BookPurchaseResponse> PurchaseBooksAsync(BookStoreApp.ServiceReference1.BookPurchaseInfo request) {
            return base.Channel.PurchaseBooksAsync(request);
        }
    }
}