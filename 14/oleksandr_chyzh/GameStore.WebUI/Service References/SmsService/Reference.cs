﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GameStore.WebUI.SmsService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SmsService.ISmsService")]
    public interface ISmsService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISmsService/Send", ReplyAction="http://tempuri.org/ISmsService/SendResponse")]
        void Send(string phoneNumber, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISmsService/Send", ReplyAction="http://tempuri.org/ISmsService/SendResponse")]
        System.Threading.Tasks.Task SendAsync(string phoneNumber, string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISmsServiceChannel : GameStore.WebUI.SmsService.ISmsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SmsServiceClient : System.ServiceModel.ClientBase<GameStore.WebUI.SmsService.ISmsService>, GameStore.WebUI.SmsService.ISmsService {
        
        public SmsServiceClient() {
        }
        
        public SmsServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SmsServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SmsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SmsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Send(string phoneNumber, string message) {
            base.Channel.Send(phoneNumber, message);
        }
        
        public System.Threading.Tasks.Task SendAsync(string phoneNumber, string message) {
            return base.Channel.SendAsync(phoneNumber, message);
        }
    }
}
