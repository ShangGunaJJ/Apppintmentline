﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace Chloe.Admin.WebService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WebServiceSoap", Namespace="http://tempuri.org/")]
    public partial class WebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddServiceOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddAppOperationCompleted;
        
        private System.Threading.SendOrPostCallback WaitNumberOperationCompleted;
        
        private System.Threading.SendOrPostCallback SelectAppStateOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTicketCodeOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetListTicketCodeOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetSerOperationCompleted;
        
        private System.Threading.SendOrPostCallback IsValidOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebService() {
            this.Url = global::Chloe.Admin.Properties.Settings.Default.Chloe_Admin_WebService_WebService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event HelloWorldCompletedEventHandler HelloWorldCompleted;
        
        /// <remarks/>
        public event AddServiceCompletedEventHandler AddServiceCompleted;
        
        /// <remarks/>
        public event AddAppCompletedEventHandler AddAppCompleted;
        
        /// <remarks/>
        public event WaitNumberCompletedEventHandler WaitNumberCompleted;
        
        /// <remarks/>
        public event SelectAppStateCompletedEventHandler SelectAppStateCompleted;
        
        /// <remarks/>
        public event GetTicketCodeCompletedEventHandler GetTicketCodeCompleted;
        
        /// <remarks/>
        public event GetListTicketCodeCompletedEventHandler GetListTicketCodeCompleted;
        
        /// <remarks/>
        public event GetSerCompletedEventHandler GetSerCompleted;
        
        /// <remarks/>
        public event IsValidCompletedEventHandler IsValidCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void HelloWorldAsync() {
            this.HelloWorldAsync(null);
        }
        
        /// <remarks/>
        public void HelloWorldAsync(object userState) {
            if ((this.HelloWorldOperationCompleted == null)) {
                this.HelloWorldOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHelloWorldOperationCompleted);
            }
            this.InvokeAsync("HelloWorld", new object[0], this.HelloWorldOperationCompleted, userState);
        }
        
        private void OnHelloWorldOperationCompleted(object arg) {
            if ((this.HelloWorldCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AddService", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int AddService(string _ServiceType, string _ServiceName, string _WorkflowText, string _WorkflowCounter, System.DateTime _AMStartTime, System.DateTime _AMEndTime, int _AMTotal, System.DateTime _PMStartTime, System.DateTime _PMEndTime, int _PMTotal) {
            object[] results = this.Invoke("AddService", new object[] {
                        _ServiceType,
                        _ServiceName,
                        _WorkflowText,
                        _WorkflowCounter,
                        _AMStartTime,
                        _AMEndTime,
                        _AMTotal,
                        _PMStartTime,
                        _PMEndTime,
                        _PMTotal});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void AddServiceAsync(string _ServiceType, string _ServiceName, string _WorkflowText, string _WorkflowCounter, System.DateTime _AMStartTime, System.DateTime _AMEndTime, int _AMTotal, System.DateTime _PMStartTime, System.DateTime _PMEndTime, int _PMTotal) {
            this.AddServiceAsync(_ServiceType, _ServiceName, _WorkflowText, _WorkflowCounter, _AMStartTime, _AMEndTime, _AMTotal, _PMStartTime, _PMEndTime, _PMTotal, null);
        }
        
        /// <remarks/>
        public void AddServiceAsync(string _ServiceType, string _ServiceName, string _WorkflowText, string _WorkflowCounter, System.DateTime _AMStartTime, System.DateTime _AMEndTime, int _AMTotal, System.DateTime _PMStartTime, System.DateTime _PMEndTime, int _PMTotal, object userState) {
            if ((this.AddServiceOperationCompleted == null)) {
                this.AddServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddServiceOperationCompleted);
            }
            this.InvokeAsync("AddService", new object[] {
                        _ServiceType,
                        _ServiceName,
                        _WorkflowText,
                        _WorkflowCounter,
                        _AMStartTime,
                        _AMEndTime,
                        _AMTotal,
                        _PMStartTime,
                        _PMEndTime,
                        _PMTotal}, this.AddServiceOperationCompleted, userState);
        }
        
        private void OnAddServiceOperationCompleted(object arg) {
            if ((this.AddServiceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddServiceCompleted(this, new AddServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AddApp", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string AddApp(System.DateTime BookingDate, int ServiceNo, string IDCardNo, string Name, string MobilePhone, string StartTime, string EndTime) {
            object[] results = this.Invoke("AddApp", new object[] {
                        BookingDate,
                        ServiceNo,
                        IDCardNo,
                        Name,
                        MobilePhone,
                        StartTime,
                        EndTime});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void AddAppAsync(System.DateTime BookingDate, int ServiceNo, string IDCardNo, string Name, string MobilePhone, string StartTime, string EndTime) {
            this.AddAppAsync(BookingDate, ServiceNo, IDCardNo, Name, MobilePhone, StartTime, EndTime, null);
        }
        
        /// <remarks/>
        public void AddAppAsync(System.DateTime BookingDate, int ServiceNo, string IDCardNo, string Name, string MobilePhone, string StartTime, string EndTime, object userState) {
            if ((this.AddAppOperationCompleted == null)) {
                this.AddAppOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddAppOperationCompleted);
            }
            this.InvokeAsync("AddApp", new object[] {
                        BookingDate,
                        ServiceNo,
                        IDCardNo,
                        Name,
                        MobilePhone,
                        StartTime,
                        EndTime}, this.AddAppOperationCompleted, userState);
        }
        
        private void OnAddAppOperationCompleted(object arg) {
            if ((this.AddAppCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddAppCompleted(this, new AddAppCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/WaitNumber", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int WaitNumber(int SerNo) {
            object[] results = this.Invoke("WaitNumber", new object[] {
                        SerNo});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void WaitNumberAsync(int SerNo) {
            this.WaitNumberAsync(SerNo, null);
        }
        
        /// <remarks/>
        public void WaitNumberAsync(int SerNo, object userState) {
            if ((this.WaitNumberOperationCompleted == null)) {
                this.WaitNumberOperationCompleted = new System.Threading.SendOrPostCallback(this.OnWaitNumberOperationCompleted);
            }
            this.InvokeAsync("WaitNumber", new object[] {
                        SerNo}, this.WaitNumberOperationCompleted, userState);
        }
        
        private void OnWaitNumberOperationCompleted(object arg) {
            if ((this.WaitNumberCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.WaitNumberCompleted(this, new WaitNumberCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SelectAppState", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int SelectAppState(int ServiceNo, string ReservationNo) {
            object[] results = this.Invoke("SelectAppState", new object[] {
                        ServiceNo,
                        ReservationNo});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void SelectAppStateAsync(int ServiceNo, string ReservationNo) {
            this.SelectAppStateAsync(ServiceNo, ReservationNo, null);
        }
        
        /// <remarks/>
        public void SelectAppStateAsync(int ServiceNo, string ReservationNo, object userState) {
            if ((this.SelectAppStateOperationCompleted == null)) {
                this.SelectAppStateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelectAppStateOperationCompleted);
            }
            this.InvokeAsync("SelectAppState", new object[] {
                        ServiceNo,
                        ReservationNo}, this.SelectAppStateOperationCompleted, userState);
        }
        
        private void OnSelectAppStateOperationCompleted(object arg) {
            if ((this.SelectAppStateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SelectAppStateCompleted(this, new SelectAppStateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTicketCode", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetTicketCode(int ServiceNo, string ReservationNo) {
            object[] results = this.Invoke("GetTicketCode", new object[] {
                        ServiceNo,
                        ReservationNo});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetTicketCodeAsync(int ServiceNo, string ReservationNo) {
            this.GetTicketCodeAsync(ServiceNo, ReservationNo, null);
        }
        
        /// <remarks/>
        public void GetTicketCodeAsync(int ServiceNo, string ReservationNo, object userState) {
            if ((this.GetTicketCodeOperationCompleted == null)) {
                this.GetTicketCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTicketCodeOperationCompleted);
            }
            this.InvokeAsync("GetTicketCode", new object[] {
                        ServiceNo,
                        ReservationNo}, this.GetTicketCodeOperationCompleted, userState);
        }
        
        private void OnGetTicketCodeOperationCompleted(object arg) {
            if ((this.GetTicketCodeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTicketCodeCompleted(this, new GetTicketCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetListTicketCode", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetListTicketCode(int ServiceNo) {
            object[] results = this.Invoke("GetListTicketCode", new object[] {
                        ServiceNo});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetListTicketCodeAsync(int ServiceNo) {
            this.GetListTicketCodeAsync(ServiceNo, null);
        }
        
        /// <remarks/>
        public void GetListTicketCodeAsync(int ServiceNo, object userState) {
            if ((this.GetListTicketCodeOperationCompleted == null)) {
                this.GetListTicketCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListTicketCodeOperationCompleted);
            }
            this.InvokeAsync("GetListTicketCode", new object[] {
                        ServiceNo}, this.GetListTicketCodeOperationCompleted, userState);
        }
        
        private void OnGetListTicketCodeOperationCompleted(object arg) {
            if ((this.GetListTicketCodeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListTicketCodeCompleted(this, new GetListTicketCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetSer", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ServiceInput[] GetSer() {
            object[] results = this.Invoke("GetSer", new object[0]);
            return ((ServiceInput[])(results[0]));
        }
        
        /// <remarks/>
        public void GetSerAsync() {
            this.GetSerAsync(null);
        }
        
        /// <remarks/>
        public void GetSerAsync(object userState) {
            if ((this.GetSerOperationCompleted == null)) {
                this.GetSerOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetSerOperationCompleted);
            }
            this.InvokeAsync("GetSer", new object[0], this.GetSerOperationCompleted, userState);
        }
        
        private void OnGetSerOperationCompleted(object arg) {
            if ((this.GetSerCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetSerCompleted(this, new GetSerCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IsValid", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int IsValid(string id) {
            object[] results = this.Invoke("IsValid", new object[] {
                        id});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void IsValidAsync(string id) {
            this.IsValidAsync(id, null);
        }
        
        /// <remarks/>
        public void IsValidAsync(string id, object userState) {
            if ((this.IsValidOperationCompleted == null)) {
                this.IsValidOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIsValidOperationCompleted);
            }
            this.InvokeAsync("IsValid", new object[] {
                        id}, this.IsValidOperationCompleted, userState);
        }
        
        private void OnIsValidOperationCompleted(object arg) {
            if ((this.IsValidCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IsValidCompleted(this, new IsValidCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ServiceInput {
        
        private int serviceNoField;
        
        private string serviceNameField;
        
        private string serviceTypeField;
        
        /// <remarks/>
        public int ServiceNo {
            get {
                return this.serviceNoField;
            }
            set {
                this.serviceNoField = value;
            }
        }
        
        /// <remarks/>
        public string ServiceName {
            get {
                return this.serviceNameField;
            }
            set {
                this.serviceNameField = value;
            }
        }
        
        /// <remarks/>
        public string ServiceType {
            get {
                return this.serviceTypeField;
            }
            set {
                this.serviceTypeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void HelloWorldCompletedEventHandler(object sender, HelloWorldCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HelloWorldCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HelloWorldCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void AddServiceCompletedEventHandler(object sender, AddServiceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void AddAppCompletedEventHandler(object sender, AddAppCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddAppCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddAppCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void WaitNumberCompletedEventHandler(object sender, WaitNumberCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class WaitNumberCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal WaitNumberCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void SelectAppStateCompletedEventHandler(object sender, SelectAppStateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SelectAppStateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SelectAppStateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void GetTicketCodeCompletedEventHandler(object sender, GetTicketCodeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTicketCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTicketCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void GetListTicketCodeCompletedEventHandler(object sender, GetListTicketCodeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetListTicketCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetListTicketCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void GetSerCompletedEventHandler(object sender, GetSerCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetSerCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetSerCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ServiceInput[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ServiceInput[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void IsValidCompletedEventHandler(object sender, IsValidCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IsValidCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal IsValidCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591