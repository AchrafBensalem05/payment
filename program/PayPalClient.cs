
using System;
using PayoutsSdk.Core;
using PayPalHttp;

using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace Samples
{
    public class PayPalClient
    {
        /**
            Setting up PayPal environment with credentials with sandbox cerdentails. 
            For Live, this should be LiveEnvironment Instance. 
         */
        public static PayPalEnvironment environment()
        {


            var clientId = "AV6yctBGHFhutCxLixHSf-R6rPnm89kQrtfXtT_ExiX_RQ7jviwEt5CUtT7HxVrClfT9J0BpTo1ZNFm6";
            var secret = "EIgwq6UiNa_GNB27kzs7YRkAbltlQ10IqRjmsWHhDBEV-LgbREWsWfknPzjPHfHOvHH0D6Xip-TWnzTK";


            //var clientId = "";
            //var secret = "";


            return new SandboxEnvironment(clientId,secret);



            //new SandboxEnvironment(
            //     System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_ID") != null ?
            //     System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_ID"): "AV6yctBGHFhutCxLixHSf-R6rPnm89kQrtfXtT_ExiX_RQ7jviwEt5CUtT7HxVrClfT9J0BpTo1ZNFm6",
            //     System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_SECRET") != null ?
            //     System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_SECRET"): "EIgwq6UiNa_GNB27kzs7YRkAbltlQ10IqRjmsWHhDBEV-LgbREWsWfknPzjPHfHOvHH0D6Xip-TWnzTK");

        }

        /**
            Returns PayPalHttpClient instance which can be used to invoke PayPal API's.
         */
        public static PayPalHttp.HttpClient client()
        {
            return new PayPalHttpClient(environment());
        }

        public static PayPalHttp.HttpClient client(string refreshToken)
        {
            return new PayPalHttpClient(environment(), refreshToken);
        }

        /**
            This method can be used to Serialize Object to JSON string.
        */
        public static String ObjectToJSONString(Object serializableObject)
        {
            MemoryStream memoryStream = new MemoryStream();
            var writer = JsonReaderWriterFactory.CreateJsonWriter(
                        memoryStream, Encoding.UTF8, true, true, "  ");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(serializableObject.GetType(), new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
            ser.WriteObject(writer, serializableObject);
            memoryStream.Position = 0;
            StreamReader sr = new StreamReader(memoryStream);
            return sr.ReadToEnd();
        }
    }
}