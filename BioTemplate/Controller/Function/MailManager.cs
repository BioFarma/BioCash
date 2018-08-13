using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Xml.Linq;
using BioTemplate.Controller.Database;
using BioTemplate.Model.Function;
using BioTemplate.Model.Object;

namespace BioTemplate.Controller.Function
{
    public class MailManager
    {
        public static void SendMailNotification()
        {
            // Get list receiver mail
            // foreach list receiver mail
                // Get content placeholder for each list mail
                // send mail

            DataTable listQueueMail = MailCatalog.GetQueueMailToBeSend();

            if (listQueueMail != null)
            {
                var listHeader = MailConstructor.GetListHeaderMail(listQueueMail);
                foreach (var queue in listHeader)
                {
                    var listContent = MailConstructor.GetListContentMail(listQueueMail, queue.MailId);
                    string bodyMail = MailConstructor.ConstructBodyMail((string) queue.MailTemplate, (List<ContentMail>) listContent);
                    SendingMail(MailConstructor.ConstructorMail((string) queue.AddressReceiver, (string) queue.MailSubject, bodyMail));                               
                }
                MailHelper.DequeueMailList(listHeader); // Call method to update flag queue mail to be already send so it won't be send twice
            }
        }

        public static void SendingMail(MailMessage mailMessage)
        {
            var mySmtpClient = new SmtpClient(ConfigurationManager.AppSettings["BiofarmaMailServerSMTP"], Convert.ToInt16(ConfigurationManager.AppSettings["BiofarmaMailServerPort"]))
            {
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                EnableSsl = false,
                Credentials =
                    new NetworkCredential(ConfigurationManager.AppSettings["MailModeratorUsername"],
                        ConfigurationManager.AppSettings["MailModeratorPassword"])
            };


            try  {
                mySmtpClient.Send(mailMessage);
            } finally {
                mySmtpClient.Dispose();
            }
        }
    }

    public class MailConstructor
    {
        public static MailMessage ConstructorMail(string receiverMail, string subjectMail, string bodyMail)
        {
            var mailMsg = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress( ConfigurationManager.AppSettings["MailModerator"],
                    ConfigurationManager.AppSettings["MailModeratorName"]),
                Subject = subjectMail,
                Body =  bodyMail
            };

            mailMsg.To.Add(receiverMail);
            mailMsg.IsBodyHtml = true;

            return mailMsg;
        }

        public static MailMessage ConstructorMail(List<string> receiverMail, string subjectMail, string bodyMail)
        {
            var mailMsg = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(ConfigurationManager.AppSettings["MailModerator"],
                    ConfigurationManager.AppSettings["MailModeratorName"]),
                Subject = subjectMail,
                Body = bodyMail
            };

            mailMsg.IsBodyHtml = true;

            foreach (var receiver in receiverMail) {
                mailMsg.To.Add(receiver);                
            }

            return mailMsg;
        }

        public static List<HeaderMail> GetListHeaderMail(DataTable queueMail)
        {
            var queueList = from r in queueMail.AsEnumerable()
                group r by new { A = r.Field<int>("IDMEL"), B = r.Field<int>("TMPID"), ADR = r.Field<string>("ADDRS"), SDR = r.Field<string>("SENDR"), S = r.Field<string>("SUBJT"), BDY = r.Field<string>("TMPTH") }
                into g
                let l = g.ToList()
                select new HeaderMail()
                {
                    MailId = g.Key.A,
                    TemplateMailId = g.Key.B,
                    AddressReceiver = g.Key.ADR,
                    AddressSender = g.Key.SDR,
                    MailSubject = g.Key.S,
                    MailTemplate = g.Key.BDY
                };

            var headerList = queueList.ToList();

            return headerList;
        }

        public static List<ContentMail> GetListContentMail(DataTable queueMail, int idMail)
        {
            var queueList = from r in queueMail.AsEnumerable()
                group r by new { A = r.Field<int>("IDMEL"), B = r.Field<int>("TMPID"), C = r.Field<int>("CNTID"), KEY = r.Field<string>("CTKEY"), VAL = r.Field<string>("CTVAL") }
                into g
                where g.Key.A == idMail
                let l = g.ToList()
                select new ContentMail()
                {
                    MailId = g.Key.A,
                    TemplateMailId = g.Key.B,
                    ContentMailId = g.Key.C,
                    ContentKey = g.Key.KEY,
                    ContentValue = g.Key.VAL
                };

            var contentList = queueList.ToList();

            return contentList;
        }

        public static DataTable GetDataTableContentMail(DataTable queueMail, int idMail)
        {
            var queueList = from r in queueMail.AsEnumerable()
                group r by new { A = r.Field<Int16>("IDMEL"), B = r.Field<Int16>("TMPID"), C = r.Field<Int16>("CNTID"), KEY = r.Field<string>("CTKEY"), VAL = r.Field<string>("CTVAL") }
                into g
                where g.Key.A == idMail
                let l = g.ToList()
                select new ContentMail()
                {
                    MailId = g.Key.A,
                    TemplateMailId = g.Key.B,
                    ContentMailId = g.Key.C,
                    ContentKey = g.Key.KEY,
                    ContentValue = g.Key.VAL
                }; ;

            var dtContentMail = new DataTable();
            dtContentMail.Columns.Add(new DataColumn("IDMEL", typeof(int)));
            dtContentMail.Columns.Add(new DataColumn("TMPID", typeof(int)));
            dtContentMail.Columns.Add(new DataColumn("CNTID", typeof(int)));
            dtContentMail.Columns.Add(new DataColumn("CTKEY", typeof(string)));
            dtContentMail.Columns.Add(new DataColumn("CTVAL", typeof(string)));

            foreach (var array in queueList)
            {
                dtContentMail.Rows.Add(array.MailId, array.TemplateMailId, array.ContentMailId, array.ContentKey, array.ContentValue);
            }

            return dtContentMail;
        }

        public static string ConstructBodyMail(string fileTemplate, DataTable keyValueTemplate)
        {
            string mailBody = fileTemplate;

            if (keyValueTemplate.Rows.Count > 0)
            {
                mailBody = keyValueTemplate.Rows.Cast<DataRow>().Aggregate(mailBody, (current, row) => current.Replace(row["CTKEY"].ToString(), row["CTVAL"].ToString()));
            }

            return mailBody;
        }

        public static string ConstructBodyMail(string fileTemplate, List<ContentMail> keyValueTemplate)
        {
            return keyValueTemplate.Aggregate(fileTemplate, (current, mail) => current.Replace(mail.ContentKey.ToString(), mail.ContentValue.ToString()));
        }
    }

    public class MailHelper
    {
        public static void DequeueMailList(List<HeaderMail> queueMail)
        {
            XDocument detailsXmlQueueMail = ApplicationXmlGenerator.GenerateHeaderQueueMailToXml(queueMail);
            MailCatalog.DequequeMailList(detailsXmlQueueMail);
        }
    }
}