using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace _0426_WCF
{
    class Program
    {
        static void Main(string[] args)
        {

            Service2();
        }

        //1. EndPoint구성(ServiceHost 객체 생성)
        //   ServiceHost : 어떤서비스객체를 어떻게 endpoint 구성
        //2. WSDL 문서 공개 : 주소가 필요하다.(ServiceHost 객체 생성시 주소 사용)
        //3. exe Hosting
        static void Service1()
        {
            //1. EndPoint구성
            ServiceHost host = new ServiceHost(typeof(HelloWorldWCFService),
                new Uri("http://localhost/wcf/example/helloworldservice"));

            host.AddServiceEndpoint(
                typeof(IHelloWorld),     // Contract
                new BasicHttpBinding(),  // Binding(XML WebService 100% 동일)
                "");                     // Address(비워두면 ServiceHost 객체 생성시 주소 사용)


            //2. WSDL 문서 공개(ServiceMetadataBehavior 설정)
            // http://localhost/wcf/example/helloworldservice?wsdl  WSDL문서 획득
            ServiceMetadataBehavior behavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (behavior == null)
            {
                behavior = new ServiceMetadataBehavior();
                host.Description.Behaviors.Add(behavior);
            }
            behavior.HttpGetEnabled = true;

            //3. exe Hosting
            host.Open();
            Console.WriteLine("Press Any key to stop the service");
            Console.ReadKey(true);
            host.Close();


        }

        //Service1과 동일(EndPoint 2개 구성 방법)
        static void Service1_1()
        {
            ServiceHost host = new ServiceHost(typeof(HelloWorldWCFService),
                new Uri("http://localhost/wcf/example/helloworldservice"),
                new Uri("net.tcp://localhost:7000/wcf/example/hellowordservice"));

			host.AddServiceEndpoint(
                typeof(IHelloWorld),        // contract
                new BasicHttpBinding(),     // binding
                "");                        // address

            host.AddServiceEndpoint(
                typeof(IHelloWorld),        // contract
                new NetTcpBinding(),        // binding
                "");                        // address

            host.Open();
            Console.WriteLine("Press Any key to stop the service");
            Console.ReadKey(true);
            host.Close();

        }

        static void Service2()
        {
            ServiceHost host = new ServiceHost(typeof(HelloWorldWCFService));


            host.Open();
            Console.WriteLine("Press Any key to stop the service");
            Console.ReadKey();
            host.Close();

        }

    }
}
