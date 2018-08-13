using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BioTemplate.Model.Object
{
    public class Mail
    {
    }

    public class HeaderMail
    {
        //private int _idEmail;
        //private int _mailTemplateId;
        //private string _mailReceiver;
        //private string _mailSender;
        //private string _mailSubject;
        //private string _mailTemplate;

        public int MailId { get; set; }
        public int TemplateMailId { get; set; }
        public string AddressReceiver { get; set; }
        public string AddressSender { get; set; }
        public string MailSubject { get; set; }
        public string MailTemplate { get; set; }
    }

    public class ContentMail
    {
        //private int _idEmail;
        //private int _mailTemplateId;
        //private string _mailContentId;
        //private string _contentKey;
        //private string _contentValue;

        public int MailId { get; set; }
        public int TemplateMailId { get; set; }
        public int ContentMailId { get; set; }
        public string ContentKey { get; set; }
        public string ContentValue { get; set; }
    }
}