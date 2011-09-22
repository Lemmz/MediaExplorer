using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.IO;
using System.Xml;



namespace mediaRetriever
{
    //contract definition for add and subtract functions
    [ServiceContract()]
    public interface interfaceMediaService
    {
        [OperationContract]
        XmlDocument retrieveList();

        [OperationContract]
        void addWatchFolder(string path);
        
    };

    // class to be called from accessing web apps
    public class MediaService : interfaceMediaService
    {
        public string watchList = "folderWatchlist.xml";

        //Function Name: retrieveList
        //Purpose: Report the current list of media that can be viewed

        public XmlDocument retrieveList()
        {
            Console.WriteLine("Returning media list.");

            XmlDocument mediaList = new XmlDocument();
            mediaList.LoadXml(watchList);

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
        public string watchList = "folderWatchlist.xml";

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

        public void listInit()
        {
             
        }
    }
}
