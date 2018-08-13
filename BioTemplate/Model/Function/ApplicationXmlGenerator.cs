using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using BioTemplate.Model.Object;


namespace BioTemplate.Model.Function
{
    public class ApplicationXmlGenerator
    {
        public static XDocument GenerateHeaderQueueMailToXml(List<HeaderMail> detailsDocument)
        {
            XDocument xmlDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("DetailsDocument", from dt in detailsDocument
                                                select new XElement("DetDocument",
                                                    new XElement("IDMEL", Convert.ToString(dt.MailId)))
                    ));
            return xmlDocument;
        }
        
        
        
        
        public static XDocument GenerateUploadedFileListToXml(List<Files> detailsDocument)
        {
            XDocument xmlDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("DetailsDocument", from dt in detailsDocument
                                                select new XElement("Attachment",
                                                    new XElement("FILNM", Convert.ToString(dt.FileOriginal)),
                                                    new XElement("FILOR", Convert.ToString(dt.FileName)),
                                                    new XElement("FLPTH", Convert.ToString(dt.FilePath)),
                                                    new XElement("FILSZ", Convert.ToString(dt.FileSize)),
                                                    new XElement("FLTYP", Convert.ToString(dt.FileType)),
                                                    new XElement("REFID", Convert.ToInt32(dt.ReferenceId)),
                                                    new XElement("REFCD", Convert.ToString(dt.ReferenceCode))
                                                    )
                    ));
            return xmlDocument;
        }

        
        public static XDocument GenerateWorkflowToXML(List<Workflow> workflow)
        {
            XDocument xmlDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
              new XElement("WorkflowList", from dt in workflow
                                          select new XElement("Workflow",
                                              new XElement("SUPID", dt.SupplierId),
                                              new XElement("DOCID", dt.DocumentId),
                                              new XElement("DOCCD", dt.DocumentCode),
                                              new XElement("SEQID", dt.SequenceId),
                                              new XElement("GRPID", dt.GroupId),
                                              new XElement("POSID", dt.PositionId),
                                              new XElement("PERNR", dt.NIK),
                                              new XElement("STAPP", dt.Status),
                                              new XElement("ICYTO", dt.IsCyto))
                  ));
            return xmlDocument;
        }
       

        

       

      
    }
}