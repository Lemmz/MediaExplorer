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
        string[] retrieveList();

        [OperationContract]
        void addWatchFolder(string path);
        
    };

    // class to be called from accessing web apps
    public class MediaService : interfaceMediaService
    {
        //Function Name: retrieveList
        //Purpose: Report the current list of media that can be viewed

        public string[] retrieveList()
        {
            string[] mediaList =  new string[10];

            mediaList[0] = "working";


            Console.WriteLine("Returning media list.");
            return mediaList;
        }

        //Function Name: addWatchFolder
        //Purpose: Add a folder to be monitored by the server for media 

        public void addWatchFolder(string path)
        {
            //update watch list
        }

    }

    class Program
    {

        public string[] mediaList;

        static void Main(string[] args)
        {

            // setup listening address for WCF
            Uri baseAddress = new Uri("http://localhost:8000/mediaService");
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
            Console.WriteLine("Media service is ready.");
            Console.WriteLine("Press <ENTER> to terminate service operation.");
            Console.WriteLine();
            Console.ReadLine();

            // Close the ServiceHostBase to shutdown the service.
            adderHost.Close();

        }
    }
}
