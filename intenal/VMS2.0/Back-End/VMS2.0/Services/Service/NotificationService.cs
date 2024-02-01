
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Net.Mail;
using System.Net;
using VMS2._0.Services.IService;
using VMS2._0.Models;
using VMS2._0.DTO;
using DinkToPdf;
using System.Text;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;

namespace VMS2._0.Services.Service
{
    public class NotificationService : INotificationService
    {

        private readonly SmtpConfig _smtpConfig;

        public NotificationService(IConfiguration configuration)
        {
            _smtpConfig = configuration.GetSection("SmtpConfig").Get<SmtpConfig>();
        }

        // Common method to send the mail - Don't change this, it can accept any type of object.
        public Task SendEmailAsync(MailMessage mailMessage)
        {
            var client = new SmtpClient(_smtpConfig.Server, _smtpConfig.Port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpConfig.Username, _smtpConfig.Password)
            };

            return client.SendMailAsync(mailMessage);
        }//- SendEmailAsync



        
        // Step 1 - 
        public async Task HostSendVisitNotificationToAdminForApprovalAsync<T>(string email, string subject, string message, T entity) where T : class
        {
            // Prepare the email content using the provided entity
            string templateName = typeof(T).Name; // Assuming the template name matches the DTO's name
            var entitiesDictionary = new Dictionary<string, object> { { templateName, entity } };
            string preparedMessage = await PrepareEmailContentAsync(templateName, entitiesDictionary, message);

            // Generate the PDF using the provided entity
            byte[] pdfBytes = GeneratePdf(entity);

            // Attach the generated PDF to the email
            var mailMessage = new MailMessage(_smtpConfig.Username, email, subject, preparedMessage)
            {
                IsBodyHtml = true
            };
            var attachment = new Attachment(new MemoryStream(pdfBytes), $"{templateName}Details.pdf", "application/pdf");
            mailMessage.Attachments.Add(attachment);

            // Send the email using the existing SendEmailAsync method
            await SendEmailAsync(mailMessage);
        }//- HostSendVisitNotificationToAdminForApprovalAsync


        // Step 2 - 
        public async Task SendApprovalOrRejectionMailToHostOrVisitor<T>(string email, string subject, string message, T entity) where T : class
        {
            // Prepare the email content using the provided entity
            string templateName = typeof(T).Name;
            var entitiesDictionary = new Dictionary<string, object> { { templateName, entity } };
            string preparedMessage = await PrepareEmailContentAsync(templateName, entitiesDictionary, message);

            // Generate the PDF using the provided entity
            byte[] pdfBytes = GeneratePdf(entity);

            // Attach the generated PDF to the email
            var mailMessage = new MailMessage(_smtpConfig.Username, email, subject, preparedMessage)
            {
                IsBodyHtml = true
            };
            var attachment = new Attachment(new MemoryStream(pdfBytes), $"{templateName}Details.pdf", "application/pdf");
            mailMessage.Attachments.Add(attachment);

            // Send the email using the existing SendEmailAsync method
            await SendEmailAsync(mailMessage);
        }//- SendApprovalOrRejectionMailToHostOrVisitor













        public async Task<string> PrepareEmailContentAsync(string templateName, Dictionary<string, object> entities, string additionalMessage = "")
        {
            // Read the HTML template
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var relativePath = $@"..\..\..\Data\EmailTemplates\DTObased.html";
            var fullPath = Path.Combine(baseDir, relativePath);
            var templateContent = File.ReadAllText(fullPath);

            StringBuilder contentBuilder = new StringBuilder();

            foreach (var entity in entities)
            {
                var properties = entity.Value.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(entity.Value);
                    var stringValue = value?.ToString() ?? "N/A";
                    contentBuilder.AppendLine($"<p><strong>{prop.Name}:</strong> {stringValue}</p>");
                }
            }

            templateContent = templateContent.Replace("{content}", contentBuilder.ToString());
            templateContent = templateContent.Replace("{additionalMessage}", additionalMessage);

            return templateContent;
        }//- PrepareEmailContentAsync










        // Helper Methods
        public byte[] GeneratePdf<T>(params T[] entities) where T : class
        {
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                foreach (var entity in entities)
                {
                    // Add title for each DTO
                    document.Add(new Paragraph($"Details for {typeof(T).Name}")
                        .SetFontColor(new iText.Kernel.Colors.DeviceRgb(0, 33, 88))
                        .SetFontSize(20));

                    var properties = typeof(T).GetProperties();
                    foreach (var prop in properties)
                    {
                        var value = prop.GetValue(entity);
                        if (value is byte[] && prop.Name.Contains("Image"))
                        {
                            try
                            {
                                ImageData imageData = ImageDataFactory.Create((byte[])value);
                                Image image = new Image(imageData).SetWidth(150).SetAutoScaleWidth(true);
                                document.Add(image);
                            }
                            catch (iText.IO.Exceptions.IOException)
                            {
                                // Handle the error (e.g., log it, skip the image, etc.)
                                document.Add(new Paragraph($"Error adding image for property {prop.Name}."));
                            }
                        }
                        else
                        {
                            var stringValue = value?.ToString() ?? "N/A";
                            document.Add(new Paragraph($"{prop.Name}: {stringValue}"));
                        }
                    }
                }

                // Add instructions
                document.Add(new Paragraph("Instructions:").SetBold());
                List list = new List()
                    .Add("All visitors must sign in and out upon entering and leaving NCS facilities.")
                    .Add("All visitors will be issued a dated visitor's pass, which should be returned to the issuing party when signing out.")
                    .Add("Any unauthorized visitor failing to secure a pass will be asked to leave the premises until a pass can be obtained.");
                document.Add(list);

                document.Close();

                // Merge the external PDF
                byte[] externalPdfBytes = GetExternalPdf("https://www.ncs.co/dam/jcr:137e9b67-350b-4ae9-8c58-23eeb068474f/Ext-NCSCoC-23-08-2023.pdf/");
                byte[] generatedPdfBytes = ms.ToArray();

                return MergePdfs(generatedPdfBytes, externalPdfBytes); // This method will need to be updated to use iText7 for merging
            }
        }//- GeneratePdf -> Dependency = GenerateHtmlContent


        private string GenerateHtmlContent<T>(params T[] entities) where T : class
        {
            StringBuilder htmlContent = new StringBuilder();

            foreach (var entity in entities)
            {
                htmlContent.AppendLine($"<h1 style='color:#002158;'>Details for {typeof(T).Name}</h1>");

                byte[] imageBytes = null;

                var properties = typeof(T).GetProperties();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(entity);

                    if (value is byte[] && prop.Name.Contains("Image"))
                    {
                        imageBytes = (byte[])value;
                        if (imageBytes.Length > 0)
                        {
                            try
                            {
                                htmlContent.AppendLine($"<img src='data:image/jpeg;base64,{Convert.ToBase64String(imageBytes)}' style='float:right; width:30%; border-radius:50%;' />");
                            }
                            catch
                            {
                                // If there's an issue with the image data, just continue without adding it to the document
                            }
                        }
                    }
                    else
                    {
                        var stringValue = value?.ToString() ?? "N/A";
                        htmlContent.AppendLine($"<p>{prop.Name}: {stringValue}</p>");
                    }
                }
            }

            // Add instructions
            htmlContent.AppendLine("<h2>Instructions:</h2>");
            htmlContent.AppendLine("<ol>");
            htmlContent.AppendLine("<li>All visitors must sign in and out upon entering and leaving NCS facilities.</li>");
            htmlContent.AppendLine("<li>All visitors will be issued a dated visitor's pass, which should be returned to the issuing party when signing out.</li>");
            htmlContent.AppendLine("<li>Any unauthorized visitor failing to secure a pass will be asked to leave the premises until a pass can be obtained.</li>");
            htmlContent.AppendLine("</ol>");

            return htmlContent.ToString();
        }
        //- GenerateHtmlContent

        private byte[] GetExternalPdf(string url)
        {
            using (var webClient = new WebClient())
            {
                return webClient.DownloadData(url);
            }
        }//- GetExternalPdf

        private byte[] MergePdfs(byte[] pdf1, byte[] pdf2)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                PdfDocument pdf1Doc = new PdfDocument(new PdfReader(new MemoryStream(pdf1)));
                PdfDocument pdf2Doc = new PdfDocument(new PdfReader(new MemoryStream(pdf2)));

                pdf1Doc.CopyPagesTo(1, pdf1Doc.GetNumberOfPages(), pdf);
                pdf2Doc.CopyPagesTo(1, pdf2Doc.GetNumberOfPages(), pdf);

                pdf1Doc.Close();
                pdf2Doc.Close();
                pdf.Close();

                return ms.ToArray();
            }
        }//- MergePdfs









        // Testing Methods.
        private VisitDTO GenerateRandomVisitDTO()
        {
            return new VisitDTO
            {
                VisitID = "SampleVisitID123",
                ParentVisitID = "SampleParentVisitID456",
                VisitorID = "SampleVisitorID789",
                HostName = "SampleHostName",
                HostEmail = "samplehost@email.com",
                Purpose = "SamplePurpose",
                ExpectedArrival = DateTime.Now.AddHours(1),
                ExpectedDepart = DateTime.Now.AddHours(2),
                CheckIn = DateTime.Now, 
                CheckOut = null,
                RequestStatus = "SampleRequestStatus",
                Feedback = "SampleFeedback",
                VisitStatus = "SampleVisitStatus"
            };
        }

    }
}



