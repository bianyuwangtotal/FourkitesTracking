using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using TrackingAssistance.App_Start;

namespace TrackingAssistance.Models
{
    public class FourkiteClient
    {

        private string auth;
        public FourkiteClient()
        {
            auth = StringConfig.auth;
        }
        private int RestRequestFourKite(string loadJson)
        {
            string  APIType = StringConfig.assignmentLocation;

            RestClient restClient = new RestClient(APIType);
            RestRequest restRequest = new RestRequest(Method.POST);

            restRequest.AddHeader("Authorization", "Basic " + auth);

            restRequest.AddParameter("application/json", loadJson, ParameterType.RequestBody);

            IRestResponse restResponse = restClient.Execute(restRequest);

            HttpStatusCode statusCode = restResponse.StatusCode;
            // if json sent correctly, save proleg to FourkitesTrackingSentLegs
            if ((int)statusCode != 200)
            { }
             //   SendToAdmin.SendMsgToAdmin(loadJson, restResponse.ErrorMessage, 1);

            return (int)statusCode;
        }

        public int AssignLocation(string bol, FourKites_GPS_Sequencer ping)
        {
            Location location = new Location();
            location.billOfLading = bol;
         //   location.shipper = sentLeg.customer_id.ToString();
            location.getshipper();
          
            location.latitude = double.Parse(ping.Lat);
            location.longitude = double.Parse(ping.@Long);

            string loadJson = JsonConvert.SerializeObject(location);
            int result = RestRequestFourKite(loadJson);

            return result;



        }

    }
}