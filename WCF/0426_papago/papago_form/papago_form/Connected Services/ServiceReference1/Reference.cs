﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace papago_form.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IPapago")]
    public interface IPapago {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPapago/papago", ReplyAction="http://tempuri.org/IPapago/papagoResponse")]
        string papago(string source, string target, string text);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPapago/papago", ReplyAction="http://tempuri.org/IPapago/papagoResponse")]
        System.Threading.Tasks.Task<string> papagoAsync(string source, string target, string text);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPapago/RetMssage", ReplyAction="http://tempuri.org/IPapago/RetMssageResponse")]
        string RetMssage(string msg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPapago/RetMssage", ReplyAction="http://tempuri.org/IPapago/RetMssageResponse")]
        System.Threading.Tasks.Task<string> RetMssageAsync(string msg);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPapagoChannel : papago_form.ServiceReference1.IPapago, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PapagoClient : System.ServiceModel.ClientBase<papago_form.ServiceReference1.IPapago>, papago_form.ServiceReference1.IPapago {
        
        public PapagoClient() {
        }
        
        public PapagoClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PapagoClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PapagoClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PapagoClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string papago(string source, string target, string text) {
            return base.Channel.papago(source, target, text);
        }
        
        public System.Threading.Tasks.Task<string> papagoAsync(string source, string target, string text) {
            return base.Channel.papagoAsync(source, target, text);
        }
        
        public string RetMssage(string msg) {
            return base.Channel.RetMssage(msg);
        }
        
        public System.Threading.Tasks.Task<string> RetMssageAsync(string msg) {
            return base.Channel.RetMssageAsync(msg);
        }
    }
}