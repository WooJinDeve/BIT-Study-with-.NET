﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _0427_File_WPF_Client_.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StudentData", Namespace="http://schemas.datacontract.org/2004/07/_0427_File_Server_")]
    [System.SerializableAttribute()]
    public partial class StudentData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool FlagField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SeatNumField;
        
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
        public bool Flag {
            get {
                return this.FlagField;
            }
            set {
                if ((this.FlagField.Equals(value) != true)) {
                    this.FlagField = value;
                    this.RaisePropertyChanged("Flag");
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
        public int SeatNum {
            get {
                return this.SeatNumField;
            }
            set {
                if ((this.SeatNumField.Equals(value) != true)) {
                    this.SeatNumField = value;
                    this.RaisePropertyChanged("SeatNum");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IFile", CallbackContract=typeof(_0427_File_WPF_Client_.ServiceReference1.IFileCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IFile {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFile/UpLoadFile", ReplyAction="http://tempuri.org/IFile/UpLoadFileResponse")]
        bool UpLoadFile(string name, int idx, string filename, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IFile/UpLoadFile", ReplyAction="http://tempuri.org/IFile/UpLoadFileResponse")]
        System.IAsyncResult BeginUpLoadFile(string name, int idx, string filename, byte[] data, System.AsyncCallback callback, object asyncState);
        
        bool EndUpLoadFile(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFile/Join", ReplyAction="http://tempuri.org/IFile/JoinResponse")]
        _0427_File_WPF_Client_.ServiceReference1.StudentData[] Join(string name, int idx);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IFile/Join", ReplyAction="http://tempuri.org/IFile/JoinResponse")]
        System.IAsyncResult BeginJoin(string name, int idx, System.AsyncCallback callback, object asyncState);
        
        _0427_File_WPF_Client_.ServiceReference1.StudentData[] EndJoin(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsTerminating=true, IsInitiating=false, Action="http://tempuri.org/IFile/Leave")]
        void Leave(string name, int idx);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsTerminating=true, IsInitiating=false, AsyncPattern=true, Action="http://tempuri.org/IFile/Leave")]
        System.IAsyncResult BeginLeave(string name, int idx, System.AsyncCallback callback, object asyncState);
        
        void EndLeave(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFileCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFile/FileRecive")]
        void FileRecive(string name, int idx, string msg, byte[] filedata);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="http://tempuri.org/IFile/FileRecive")]
        System.IAsyncResult BeginFileRecive(string name, int idx, string msg, byte[] filedata, System.AsyncCallback callback, object asyncState);
        
        void EndFileRecive(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFile/UserEnter")]
        void UserEnter(string name, int idx);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="http://tempuri.org/IFile/UserEnter")]
        System.IAsyncResult BeginUserEnter(string name, int idx, System.AsyncCallback callback, object asyncState);
        
        void EndUserEnter(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFile/UserLeave")]
        void UserLeave(string name, int idx);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="http://tempuri.org/IFile/UserLeave")]
        System.IAsyncResult BeginUserLeave(string name, int idx, System.AsyncCallback callback, object asyncState);
        
        void EndUserLeave(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFileChannel : _0427_File_WPF_Client_.ServiceReference1.IFile, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UpLoadFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public UpLoadFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public bool Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class JoinCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public JoinCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public _0427_File_WPF_Client_.ServiceReference1.StudentData[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((_0427_File_WPF_Client_.ServiceReference1.StudentData[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FileClient : System.ServiceModel.DuplexClientBase<_0427_File_WPF_Client_.ServiceReference1.IFile>, _0427_File_WPF_Client_.ServiceReference1.IFile {
        
        private BeginOperationDelegate onBeginUpLoadFileDelegate;
        
        private EndOperationDelegate onEndUpLoadFileDelegate;
        
        private System.Threading.SendOrPostCallback onUpLoadFileCompletedDelegate;
        
        private BeginOperationDelegate onBeginJoinDelegate;
        
        private EndOperationDelegate onEndJoinDelegate;
        
        private System.Threading.SendOrPostCallback onJoinCompletedDelegate;
        
        private BeginOperationDelegate onBeginLeaveDelegate;
        
        private EndOperationDelegate onEndLeaveDelegate;
        
        private System.Threading.SendOrPostCallback onLeaveCompletedDelegate;
        
        public FileClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public FileClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public FileClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public FileClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public FileClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public event System.EventHandler<UpLoadFileCompletedEventArgs> UpLoadFileCompleted;
        
        public event System.EventHandler<JoinCompletedEventArgs> JoinCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> LeaveCompleted;
        
        public bool UpLoadFile(string name, int idx, string filename, byte[] data) {
            return base.Channel.UpLoadFile(name, idx, filename, data);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginUpLoadFile(string name, int idx, string filename, byte[] data, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginUpLoadFile(name, idx, filename, data, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public bool EndUpLoadFile(System.IAsyncResult result) {
            return base.Channel.EndUpLoadFile(result);
        }
        
        private System.IAsyncResult OnBeginUpLoadFile(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string name = ((string)(inValues[0]));
            int idx = ((int)(inValues[1]));
            string filename = ((string)(inValues[2]));
            byte[] data = ((byte[])(inValues[3]));
            return this.BeginUpLoadFile(name, idx, filename, data, callback, asyncState);
        }
        
        private object[] OnEndUpLoadFile(System.IAsyncResult result) {
            bool retVal = this.EndUpLoadFile(result);
            return new object[] {
                    retVal};
        }
        
        private void OnUpLoadFileCompleted(object state) {
            if ((this.UpLoadFileCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.UpLoadFileCompleted(this, new UpLoadFileCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void UpLoadFileAsync(string name, int idx, string filename, byte[] data) {
            this.UpLoadFileAsync(name, idx, filename, data, null);
        }
        
        public void UpLoadFileAsync(string name, int idx, string filename, byte[] data, object userState) {
            if ((this.onBeginUpLoadFileDelegate == null)) {
                this.onBeginUpLoadFileDelegate = new BeginOperationDelegate(this.OnBeginUpLoadFile);
            }
            if ((this.onEndUpLoadFileDelegate == null)) {
                this.onEndUpLoadFileDelegate = new EndOperationDelegate(this.OnEndUpLoadFile);
            }
            if ((this.onUpLoadFileCompletedDelegate == null)) {
                this.onUpLoadFileCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnUpLoadFileCompleted);
            }
            base.InvokeAsync(this.onBeginUpLoadFileDelegate, new object[] {
                        name,
                        idx,
                        filename,
                        data}, this.onEndUpLoadFileDelegate, this.onUpLoadFileCompletedDelegate, userState);
        }
        
        public _0427_File_WPF_Client_.ServiceReference1.StudentData[] Join(string name, int idx) {
            return base.Channel.Join(name, idx);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginJoin(string name, int idx, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginJoin(name, idx, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public _0427_File_WPF_Client_.ServiceReference1.StudentData[] EndJoin(System.IAsyncResult result) {
            return base.Channel.EndJoin(result);
        }
        
        private System.IAsyncResult OnBeginJoin(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string name = ((string)(inValues[0]));
            int idx = ((int)(inValues[1]));
            return this.BeginJoin(name, idx, callback, asyncState);
        }
        
        private object[] OnEndJoin(System.IAsyncResult result) {
            _0427_File_WPF_Client_.ServiceReference1.StudentData[] retVal = this.EndJoin(result);
            return new object[] {
                    retVal};
        }
        
        private void OnJoinCompleted(object state) {
            if ((this.JoinCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.JoinCompleted(this, new JoinCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void JoinAsync(string name, int idx) {
            this.JoinAsync(name, idx, null);
        }
        
        public void JoinAsync(string name, int idx, object userState) {
            if ((this.onBeginJoinDelegate == null)) {
                this.onBeginJoinDelegate = new BeginOperationDelegate(this.OnBeginJoin);
            }
            if ((this.onEndJoinDelegate == null)) {
                this.onEndJoinDelegate = new EndOperationDelegate(this.OnEndJoin);
            }
            if ((this.onJoinCompletedDelegate == null)) {
                this.onJoinCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnJoinCompleted);
            }
            base.InvokeAsync(this.onBeginJoinDelegate, new object[] {
                        name,
                        idx}, this.onEndJoinDelegate, this.onJoinCompletedDelegate, userState);
        }
        
        public void Leave(string name, int idx) {
            base.Channel.Leave(name, idx);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginLeave(string name, int idx, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginLeave(name, idx, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndLeave(System.IAsyncResult result) {
            base.Channel.EndLeave(result);
        }
        
        private System.IAsyncResult OnBeginLeave(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string name = ((string)(inValues[0]));
            int idx = ((int)(inValues[1]));
            return this.BeginLeave(name, idx, callback, asyncState);
        }
        
        private object[] OnEndLeave(System.IAsyncResult result) {
            this.EndLeave(result);
            return null;
        }
        
        private void OnLeaveCompleted(object state) {
            if ((this.LeaveCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.LeaveCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void LeaveAsync(string name, int idx) {
            this.LeaveAsync(name, idx, null);
        }
        
        public void LeaveAsync(string name, int idx, object userState) {
            if ((this.onBeginLeaveDelegate == null)) {
                this.onBeginLeaveDelegate = new BeginOperationDelegate(this.OnBeginLeave);
            }
            if ((this.onEndLeaveDelegate == null)) {
                this.onEndLeaveDelegate = new EndOperationDelegate(this.OnEndLeave);
            }
            if ((this.onLeaveCompletedDelegate == null)) {
                this.onLeaveCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnLeaveCompleted);
            }
            base.InvokeAsync(this.onBeginLeaveDelegate, new object[] {
                        name,
                        idx}, this.onEndLeaveDelegate, this.onLeaveCompletedDelegate, userState);
        }
    }
}