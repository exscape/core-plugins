using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using System.IO;

namespace Twitterizer.Framework
{
    internal class TwitterRequest
    {
        public TwitterRequestData PerformWebRequest(TwitterRequestData Data)
        {
            PerformWebRequest(Data, "POST");
            
            return (Data);
        }

        public TwitterRequestData PerformWebRequest(TwitterRequestData Data, string HTTPMethod)
        {
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(Data.ActionUri);


            Request.Method = HTTPMethod;

            Stream receiveStream;
            StreamReader readStream;

            // Some limitations
            Request.MaximumAutomaticRedirections = 4;
            Request.MaximumResponseHeadersLength = 4;

            // Set our credentials
            Request.Credentials = new NetworkCredential(Data.UserName, Data.Password);

            HttpWebResponse Response = null;

            // Get the respon
            try
            {
                Response = (HttpWebResponse)Request.GetResponse();

                // Get the stream associated with the response.
                receiveStream = Response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                readStream = new StreamReader(receiveStream, Encoding.UTF8);

                Data.Response = readStream.ReadToEnd();
                Data = ParseResponseData(Data);

                Response.Close();
                readStream.Close();
            }
            catch (Exception ex)
            {
                throw new TwitterizerException(ex.Message, Data, ex);
            }

            return Data;
        }

        private TwitterRequestData ParseResponseData(TwitterRequestData Data)
        {
            if (Data == null || Data.Response == string.Empty)
                return null;

            try
            {
                XmlDocument ResultXmlDocument = new XmlDocument();
                ResultXmlDocument.LoadXml(Data.Response);
				
                switch (ResultXmlDocument.DocumentElement.Name.ToLower())
                {
                    case "status":
                        Data.Statuses = new TwitterStatusCollection();
                        Data.Statuses.Add(ParseStatusNode(ResultXmlDocument.DocumentElement));
                        break;
                    case "statuses":
                        Data.Statuses = ParseStatuses(ResultXmlDocument.DocumentElement);
                        break;
                    case "users":
                        Data.Users = ParseUsers(ResultXmlDocument.DocumentElement);
                        break;
                    case "user":
                        Data.Users = new TwitterUserCollection();
                        Data.Users.Add(ParseUserNode(ResultXmlDocument.DocumentElement));
                        break;
                    case "direct-messages":
                        Data.Statuses = new TwitterStatusCollection();
                        Data.Statuses = ParseDirectMessages(ResultXmlDocument.DocumentElement);
                        break;

                    case "nil-classes":
                        // do nothing, this seems to be a null response i.e. no messages since
                        break;

                    case "error":
                        throw new Exception("Error response from Twitter: " + ResultXmlDocument.DocumentElement.InnerText);
                    default:
                        
                        throw new Exception("Invalid response from Twitter");
                }

            }
            catch (Exception ex)
            {
                throw new TwitterizerException("Error Parsing Twitter Response.", Data, ex);
            }

            return Data;
        }

        #region Parse Statuses
        private TwitterStatusCollection ParseStatuses(XmlElement Element)
        {
            TwitterStatusCollection Collection = new TwitterStatusCollection();
            foreach (XmlElement Child in Element.GetElementsByTagName("status"))
            {
                Collection.Add(ParseStatusNode(Child));
            }

            return Collection;
        }

        private TwitterStatus ParseStatusNode(XmlElement Element)
        {
            TwitterStatus Status = new TwitterStatus();
            //Mon May 12 15:56:07 +0000 2008
            Status.ID = int.Parse(Element["id"].InnerText);
            //Console.Error.WriteLine ("ID {0}: ", Element["id"].InnerText);
            Status.Created = ParseDateString(Element["created_at"].InnerText);
            //Console.Error.WriteLine ("Created At {0}: ", Element["created_at"].InnerText);
            Status.Text = Element["text"].InnerText;
            //Console.Error.WriteLine ("Text {0}: ", Element["text"].InnerText);
            Status.Source = Element["source"].InnerText;
            //Console.Error.WriteLine ("Source {0}: ", Element["source"].InnerText);
            Status.IsTruncated = bool.Parse(Element["truncated"].InnerText);
            //Console.Error.WriteLine ("Truncated {0}: ", bool.Parse(Element["truncated"].InnerText));
            if (Element["in_reply_to_status_id"].InnerText != string.Empty) {
                Status.InReplyToStatusID = int.Parse(Element["in_reply_to_status_id"].InnerText);
                //Console.Error.WriteLine ("In reply to: {0}", int.Parse(Element["in_reply_to_status_id"].InnerText));
            }
            if (Element["in_reply_to_user_id"].InnerText != string.Empty) {
                Status.InReplyToUserID = int.Parse(Element["in_reply_to_user_id"].InnerText);
                //Console.Error.WriteLine ("In reply to: {0}", int.Parse(Element["in_reply_to_user_id"].InnerText));
            }
            try {
            	//Console.Error.WriteLine ("Favourited {0}: ", Element["favorited"].InnerText);
            	Status.IsFavorited = bool.Parse(Element["favorited"].InnerText);
            } catch { }
            Status.TwitterUser = ParseUserNode(Element["user"]);
 
            return Status;
        }
        #endregion

        #region Parse DirectMessages
        private TwitterStatusCollection ParseDirectMessages(XmlElement Element)
        {
            TwitterStatusCollection Collection = new TwitterStatusCollection();
            foreach (XmlElement Child in Element.GetElementsByTagName("direct_message"))
            {
                Collection.Add(ParseDirectMessageNode(Child));
            }

            return Collection;
        }

        private TwitterStatus ParseDirectMessageNode(XmlElement Element)
        {
            TwitterStatus Status = new TwitterStatus();

            //Mon May 12 15:56:07 +0000 2008
            Status.ID = int.Parse(Element["id"].InnerText);
            Status.Created = ParseDateString(Element["created_at"].InnerText);
            Status.Text = Element["text"].InnerText;
            
            //if (Element["in_reply_to_status_id"].InnerText != string.Empty)
            //    Status.InReplyToStatusID = int.Parse(Element["in_reply_to_status_id"].InnerText);
            //if (Element["in_reply_to_user_id"].InnerText != string.Empty)
            //    Status.InReplyToUserID = int.Parse(Element["in_reply_to_user_id"].InnerText);
           
           if (Element["favorited"] != null && (Element["in_reply_to_status_id"].InnerText != string.Empty))
                Status.IsFavorited = bool.Parse(Element["favorited"].InnerText);

            Status.TwitterUser = new TwitterUser();
           // Status.TwitterUser = ParseUserNode(Element["user"]);
            Status.TwitterUser.ScreenName = Element["sender_screen_name"].InnerText;
            Status.TwitterUser.ID = int.Parse(Element["sender_id"].InnerText);
            Status.RecipientID = int.Parse(Element["recipient_id"].InnerText);
          
           


            return Status;
        }
        #endregion

        #region Parse Users
        private TwitterUserCollection ParseUsers(XmlElement Element)
        {
            TwitterUserCollection Collection = new TwitterUserCollection();
            foreach (XmlElement Child in Element.GetElementsByTagName("user"))
            {
                Collection.Add(ParseUserNode(Child));
            }

            return Collection;
        }

        private TwitterUser ParseUserNode(XmlElement Element)
        {
            if (Element == null)
                return null;

            TwitterUser User = new TwitterUser();
            User.ID = int.Parse(Element["id"].InnerText);
            User.UserName = Element["name"].InnerText;
            User.ScreenName = Element["screen_name"].InnerText;
            User.Location = Element["location"].InnerText;
            User.Description = Element["description"].InnerText;
            if (Element["profile_image_url"].InnerText != string.Empty)
                User.ProfileImageUri = new Uri(Element["profile_image_url"].InnerText);
            if (Element["url"].InnerText != string.Empty)
                User.ProfileUri = new Uri(Element["url"].InnerText);
            User.IsProtected = bool.Parse(Element["protected"].InnerText);
            User.NumberOfFollowers = int.Parse(Element["followers_count"].InnerText);

            if (Element["friends_count"] != null)
                User.Friends_count = int.Parse(Element["friends_count"].InnerText);
            else
                User.Friends_count = -1;        // flag that we don't know, which is different than having zero friends

            if (Element["status"] != null)
                User.Status = ParseStatusNode(Element["status"]);
            return User;
        }
        #endregion

        private DateTime ParseDateString(string DateString)
        {
            DateTime parsedDate = DateTime.Now;

            Regex re = new Regex(@"(?<DayName>[^ ]+) (?<MonthName>[^ ]+) (?<Day>[^ ]{1,2}) (?<Hour>[0-9]{1,2}):(?<Minute>[0-9]{1,2}):(?<Second>[0-9]{1,2}) (?<TimeZone>[+-][0-9]{4}) (?<Year>[0-9]{4})");
            Match CreatedAt = re.Match(DateString);
            parsedDate = DateTime.Parse(
                string.Format(
                    "{0} {1} {2} {3}:{4}:{5}",
                    CreatedAt.Groups["MonthName"].Value,
                    CreatedAt.Groups["Day"].Value,
                    CreatedAt.Groups["Year"].Value,
                    CreatedAt.Groups["Hour"].Value,
                    CreatedAt.Groups["Minute"].Value,
                    CreatedAt.Groups["Second"].Value));

            return parsedDate;
        }
    }
}