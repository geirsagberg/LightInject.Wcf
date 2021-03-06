namespace LightInject.Wcf.Tests
{
    using System;
    using LightInject.Interception;    
    using LightInject.Wcf.SampleLibrary;

    using Xunit;

    [Collection("WcfTests")]
    public class ServiceHostTests : TestBase
    {
        [Fact]
        public void CreateServiceHost_ServiceTypeIsInterfaceWithServiceContractAttribute_ReturnsServieHostWithProxyAsServiceType()
        {
            using (var serviceHost = StartService<IService>())
            {
                Assert.True(typeof(IProxy).IsAssignableFrom(serviceHost.Description.ServiceType));
            }                                     
        }

        [Fact]
        public void CreateServiceHost_ServiceTypeIsConcrete_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => StartService<ServiceWithoutInterface>());            
        }

        [Fact]
        public void CreateServiceHost_ServiceTypeIsInterfaceWithoutServiceContractAttribute_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => StartService<IServiceWithoutServiceContractAttribute>());            
        }

        [Fact]
        public void CreateServiceHost_ConfiguredService_AppliesConfiguration()
        {
            using (var serviceHost = StartService<IConfiguredService>())
            {
                Assert.Equal(2, serviceHost.Description.Endpoints.Count);
            }                        
        }
    }       
}
