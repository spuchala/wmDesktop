using AlexaSkillsKit.Speechlet;
using System;
using System.Collections.Generic;
using System.Linq;
using AlexaSkillsKit.UI;
using AlexaSkillsKit.Slu;
using System.Xml;
using System.Text;
using WalMartAPI.Models;
using WalMartAPI.Helpers;

namespace WalMartAPI.Services
{
    public class AlexaService : Speechlet
    {
        #region members
        private WalMartHelper _walmartHelper;
        #endregion

        public AlexaService()
        {
            _walmartHelper = new WalMartHelper();
        }

        public override SpeechletResponse OnIntent(IntentRequest intentRequest, Session session)
        {
            // Get intent from the request object.
            Intent intent = intentRequest.Intent;
            string intentName = (intent != null) ? intent.Name : null;

            //check reviews intent
            if (Constants.RequestReviews.Equals(intentName))
            {
                return ProcessReviews(intent);
            }
            return BuildSpeechletResponse("Reviews", "Error", true);
        }

        public override SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session)
        {
            return null;
        }

        public override void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session)
        {

        }

        public override void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session)
        {

        }

        #region Helper Methods       

        private SpeechletResponse ProcessReviews(Intent intent)
        {
            // Get the slots from the intent.
            Dictionary<string, Slot> slots = intent.Slots;
            Slot productSlot = slots[Constants.ProductSlot];

            // Check for product and create output to user.
            if (productSlot != null && !string.IsNullOrEmpty(productSlot.Value))
            {
                string product = productSlot.Value;
                var output = string.Empty;
                var data = _walmartHelper.GetSearchData(product.Replace(" ", "+"));
                if (data != null && data.items.Any())
                {
                    var reviews = _walmartHelper.GetReviews(data.items[0].itemId);
                    //read first review and ask for more if needed
                    output = reviews[0].reviewText;
                }
                return BuildSpeechletResponse("Reviews", output, true);
            }
            return BuildSpeechletResponse("Reviews", Constants.Error, true);
        }

        private SpeechletResponse BuildSpeechletResponse(string title, string output, bool shouldEndSession)
        {
            // Create the Simple card content.
            SimpleCard card = new SimpleCard();
            card.Title = title;
            card.Content = output;

            // Create the plain text output.
            PlainTextOutputSpeech speech = new PlainTextOutputSpeech();
            speech.Text = output;

            // Create the speechlet response.
            SpeechletResponse response = new SpeechletResponse();
            response.ShouldEndSession = shouldEndSession;
            response.OutputSpeech = speech;
            response.Card = card;
            return response;
        }

        #endregion
    }
}
