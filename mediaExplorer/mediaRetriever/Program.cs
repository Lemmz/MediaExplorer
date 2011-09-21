using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;



namespace mediaRetriever
{
    //contract definition for add and subtract functions
    [ServiceContract()]
    public interface interfaceMediaService
    {
        [OperationContract]
        int add(int num1, int num2);

    };

    // class to be called from accessing web apps
    public class MediaService : interfaceMediaService
    {
        //Function Name: add
        //Purpose: Return integer value of 2 input integers that have been added

        public int add(int num1, int num2)
        {
            int result = num1 + num2;
            Console.WriteLine("Received Add request: " + num1 + ", " + num2);
            Console.WriteLine("Returning: " + result);
            return result;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            // setup listening address for WCF
            Uri baseAddress = new Uri("http://localhost:8000/PSBadder");
            ServiceHost adderHost = new ServiceHost(typeof(MediaService), baseAddress);

            try
            {
                //catch exception that allocating a address space may cause
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                adderHost.Abort();
            }

            // create a name 
            adderHost.AddServiceEndpoint(
                typeof(interfaceMediaService),
                new WSHttpBinding(),
                "MediaService");

            // enable metadata exchange for host
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            adderHost.Description.Behaviors.Add(smb);

            // open host and wait for connections
            adderHost.Open();
            Console.WriteLine("Add Subtract service is ready.");
            Console.WriteLine("Press <ENTER> to terminate service operation.");
            Console.WriteLine();
            Console.ReadLine();

            // Close the ServiceHostBase to shutdown the service.
            adderHost.Close();

        }
    }
}
