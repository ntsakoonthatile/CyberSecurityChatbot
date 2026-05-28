using System;
using System.Collections.Generic;

namespace CyberSecurityChatbot
{
    class Chatbot
    {
        Dictionary<string, List<string>> responses =
            new Dictionary<string, List<string>>();

        Random random = new Random();

        string lastTopic = "";

        string userName = "";

        string favouriteTopic = "";

        public delegate string ResponseHandler(string input);

        public Chatbot()
        {
            responses.Add("password",
                new List<string>()
                {
                    "Use strong passwords with symbols and numbers.",
                    "Avoid using personal information in passwords.",
                    "Use different passwords for every account."
                });

            responses.Add("privacy",
                new List<string>()
                {
                    "Review your privacy settings regularly.",
                    "Enable two-factor authentication.",
                    "Avoid sharing personal information online."
                });

            responses.Add("scam",
                new List<string>()
                {
                    "Scammers often pretend to be trusted companies.",
                    "Avoid clicking suspicious links.",
                    "Never share banking details with strangers."
                });

            responses.Add("phishing",
                new List<string>()
                {
                    "Phishing emails often create urgency.",
                    "Check email addresses carefully.",
                    "Avoid opening suspicious attachments."
                });
        }

        public string GetResponse(string input)
        {
            input = input.ToLower();

            ResponseHandler handler = ProcessMessage;

            return handler(input);
        }

        private string ProcessMessage(string input)
        {
            if (input.Contains("my name is"))
            {
                userName =
                    input.Replace("my name is", "").Trim();

                return "Nice to meet you " +
                       userName + ".";
            }

            if (input.Contains("i like privacy") ||
                input.Contains("interested in privacy"))
            {
                favouriteTopic = "privacy";

                return "Great! I will remember that you are interested in privacy.";
            }

            if (input.Contains("worried"))
            {
                return "It is understandable to feel worried about scams. Never click suspicious links and always verify websites.";
            }

            if (input.Contains("frustrated"))
            {
                return "Cybersecurity can feel overwhelming sometimes, but learning small safety habits helps a lot.";
            }

            if (input.Contains("curious"))
            {
                return "Curiosity is important because learning helps keep you safe online.";
            }

            if (input.Contains("tell me more") ||
                input.Contains("another tip") ||
                input.Contains("explain more"))
            {
                if (responses.ContainsKey(lastTopic))
                {
                    return GetRandomResponse(lastTopic);
                }

                return "Please ask about passwords, scams, privacy or phishing.";
            }

            foreach (var keyword in responses.Keys)
            {
                if (input.Contains(keyword))
                {
                    lastTopic = keyword;

                    return GetRandomResponse(keyword);
                }
            }

            if (input.Contains("advice"))
            {
                if (favouriteTopic != "")
                {
                    return "Since you are interested in " +
                           favouriteTopic +
                           ", remember to review your account settings regularly.";
                }
            }

            if (input.Contains("hello") ||
                input.Contains("hi"))
            {
                return "Hello! How can I help you today?";
            }

            if (input.Contains("bye"))
            {
                return "Goodbye! Stay safe online.";
            }

            return "I am not sure I understand. Can you try rephrasing?";
        }

        private string GetRandomResponse(string keyword)
        {
            List<string> possibleResponses =
                responses[keyword];

            int index =
                random.Next(possibleResponses.Count);

            return possibleResponses[index];
        }
    }
}