using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using INF.Web.Data.BLL;
using INF.Web.Data.Entities;
using INF.Web.UI.Logging.Log4Net;
using INF.Web.UI.Settings;
using Microsoft.VisualBasic;

namespace INF.Web.UI
{
    public class BasePage : Page
    {
        #region "Constants"

        protected const string ADMIN_ROLE = "SystemUsers";
        protected const string CONNECTION_STR_KEY = "PizzaWebConnectionString";

        protected const string MENU_CATEGORY_IMAGES_FOLDER = "~/upload/category/";

        #endregion

        #region "Vars"

        //protected readonly log4net.ILog Logger;
        private static readonly Log4NetLogger _log = new Log4NetLogger();

        #endregion

        public BasePage()
        {
        }

        #region "Properties"

        //protected string CurrentTheme
        //{
        //    get
        //    {
        //        if (Session[SSN_SELECTED_THEME] == null)
        //        {
        //            string savedThemeName = string.Empty;

        //            try
        //            {
        //                var resInfoBll = new RestaurantBusinessLogic();
        //                CsRestaurant res = resInfoBll.GetRestaurantInfo();
        //                if (res != null)
        //                {
        //                    savedThemeName = res.SelectedTheme;
        //                }
        //            }
        //            catch
        //            {
        //            }

        //            Session.Add(SSN_SELECTED_THEME, !string.IsNullOrEmpty(savedThemeName) ? savedThemeName : "default");
        //        }
        //        return (string)Session[SSN_SELECTED_THEME];
        //    }
        //}

        #endregion
        

        protected override void OnLoad(EventArgs e)
        {
            // add onfocus and onblur javascripts to all input controls on the forum,
            // so that the active control has a difference appearance
            //Helpers.SetInputControlsHighlight(this, "highlight", false);

            base.OnLoad(e);
        }

        public void BuildErrorMessagesControl(PlaceHolder placeHolder, List<string> messages)
        {
            if (messages != null && messages.Count > 0)
            {
                var ul = new HtmlGenericControl("ul");
                foreach (string message in messages)
                {
                    var li = new HtmlGenericControl("li");
                    li.InnerText = message;
                    li.Attributes.Add("class", "bad-msg");
                    ul.Controls.Add(li);
                }

                placeHolder.Controls.Add(ul);
            }
        }

        public void BuildMessagesControl(PlaceHolder placeHolder, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var ul = new HtmlGenericControl("ul");

                var li = new HtmlGenericControl("li") { InnerText = message };
                li.Attributes.Add("class", "good-msg");
                ul.Controls.Add(li);

                placeHolder.Controls.Add(ul);
            }
        }

        public string BaseUrl
        {
            get
            {
                string url = this.Request.ApplicationPath;
                if (url != null && url.EndsWith("/"))
                    return url;
                else
                    return url + "/";
            }
        }

        public string FullBaseUrl
        {
            get
            {
                return this.Request.Url.AbsoluteUri.Replace(
                    this.Request.Url.PathAndQuery, "") + this.BaseUrl;
            }
        }

        protected void RequestLogin()
        {
            //this.Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + this.Request.Url.PathAndQuery);
            this.Response.RedirectTo(FormsAuthentication.LoginUrl + "?ReturnUrl=" + this.Request.Url.PathAndQuery);
        }

        public string FormatPrice(object price)
        {
            //return Convert.ToDecimal(price).ToString("N2") + " " + Globals.Settings.Store.CurrencyCode;
            return Convert.ToDecimal(price).ToString("N2");
        }

        public Type GetCallerType(int vSkipFrames)
        {
            var trace = new StackTrace(vSkipFrames + 1, false);
            var count = trace.FrameCount;

            for (var index = 0; index <= count; index++)
            {
                var frame = trace.GetFrame(index);
                var type = frame.GetMethod().DeclaringType;
                if (type != typeof(BasePage))
                {
                    return type;
                }
            }
            return null;
        }

        protected bool IsDBNull(object value)
        {
            return DBNull.Value.Equals(value);
        }

        public bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            if (ext != null)
            {
                switch (ext.ToLower())
                {
                    case ".gif":
                        return true;
                    case ".png":
                        return true;
                    case ".jpg":
                        return true;
                    case ".jpeg":
                        return true;
                    case ".bmp":
                        return true;
                    default:
                        return false;
                }
            }
            return false;
        }

        public string UploadImageWithoutResize(FileUpload vFileUpload, string uploadFolder)
        {
            var url = string.Empty;
            if (CheckFileType(vFileUpload.FileName))
            {
                string folderToUpload = string.IsNullOrEmpty(uploadFolder)
                    ? "~/content/" + WebsiteConfig.Instance.INFTheme + "/" + vFileUpload.FileName
                    : uploadFolder + vFileUpload.FileName;
                string serverDir = HttpContext.Current.Server.MapPath(folderToUpload);
                if (!Directory.Exists(serverDir))
                {
                    try
                    {
                        Directory.CreateDirectory(serverDir);
                    }
                    catch
                    {
                    }
                }

                if (!folderToUpload.EndsWith("/"))
                {
                    folderToUpload = folderToUpload + "/";
                }

                url = folderToUpload + vFileUpload.FileName;
                vFileUpload.SaveAs(MapPath(url));
            }
            return url;
        }

        public string ResizeAndUploadImage(FileUpload vFileUpload, int vWidth, int vHeight)
        {
            string url = string.Empty;
            if (CheckFileType(vFileUpload.FileName))
            {
                int newWidth = vWidth;
                int newHeight = vHeight;

                string upName = Strings.Mid(vFileUpload.FileName, 1, (Strings.InStr(vFileUpload.FileName, ".") - 1));

                string folderToUpload = "~/upload/";
                string serverDir = HttpContext.Current.Server.MapPath(folderToUpload);
                if (!Directory.Exists(serverDir))
                {
                    try
                    {
                        Directory.CreateDirectory(serverDir);
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex);
                    }
                }
                string filePath = folderToUpload + upName + ".png";
                //string filePath = "~/content/" + CurrentTheme + "/" + upName + ".png";

                Bitmap upBmp = (Bitmap)Bitmap.FromStream(vFileUpload.PostedFile.InputStream);
                Bitmap newBmp = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                newBmp.SetResolution(72, 72);

                //Get the uploaded image width and height
                int upWidth = upBmp.Width;
                int upHeight = upBmp.Height;

                // Save the file without resize if the defined sizes are the same the original sizes
                if (upWidth == vWidth && upHeight == vHeight)
                {
                    vFileUpload.SaveAs(MapPath(filePath));
                    return filePath;
                }

                int newX = 0;
                int newY = 0;
                decimal reDuce = default(decimal);

                //Keep the aspect ratio of image the same if not 4:3 and work out the newX and newY positions
                //to ensure the image is always in the centre of the canvas vertically and horizontally
                //Landscape picture
                if (upWidth > upHeight)
                {
                    reDuce = (decimal)newWidth / upWidth;
                    newHeight = Convert.ToInt32(Conversion.Int(upHeight * reDuce));
                    newY = Convert.ToInt32(Conversion.Int((vHeight - newHeight) / 2));
                    newX = 0;

                    //Portrait picture
                }
                else if (upWidth < upHeight)
                {
                    reDuce = (decimal)newHeight / upHeight;
                    newWidth = Convert.ToInt32(Conversion.Int(upWidth * reDuce));
                    newX = Convert.ToInt32(Conversion.Int((vWidth - newWidth) / 2));
                    newY = 0;

                    //square picture
                }
                else if (upWidth == upHeight)
                {
                    reDuce = (decimal)newHeight / upHeight;
                    newWidth = Convert.ToInt32(Conversion.Int(upWidth * reDuce));
                    newX = Convert.ToInt32(Conversion.Int((vWidth - newWidth) / 2));
                    newY = Convert.ToInt32(Conversion.Int((vHeight - newHeight) / 2));

                }

                //Create a new image from the uploaded picture using the Graphics class
                //Clear the graphic and set the background colour to white
                //Use Antialias and High Quality Bicubic to maintain a good quality picture
                //Save the new bitmap image using 'Png' picture format and the calculated canvas positioning
                Graphics newGraphic = Graphics.FromImage(newBmp);

                try
                {
                    newGraphic.Clear(Color.White);
                    newGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    newGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    newGraphic.DrawImage(upBmp, newX, newY, newWidth, newHeight);
                    Color backColor = newBmp.GetPixel(1, 1);
                    newBmp.MakeTransparent(backColor);
                    newBmp.Save(MapPath(filePath), System.Drawing.Imaging.ImageFormat.Png);
                    url = filePath;
                }
                finally
                {
                    upBmp.Dispose();
                    newBmp.Dispose();
                    newGraphic.Dispose();
                }
            }

            return url;
        }

        protected void SendMail(MailMsg msg)
        {
            SendMail(msg, new Dictionary<string, string>() { { msg.ToAddress, msg.ToDisplayName } });
        }

        protected void SendMailOrderTemp(MailMsg msg, string mailFromAddr, string mailFromName, string mailFromSubj)
        {
            //SendMailOrder(msg, new Dictionary<string, string>() { { msg.ToAddress, msg.ToDisplayName } }, mailFromAddr, mailFromName, mailFromSubj);
        }

        public void SendMailOrder(MailMsg msg, string mailToAddr, string mailToName, string mailFromAddr, string mailFromName, string mailFromSubj)
        {
            //SendMail(msg, new Dictionary<string, string>() { { msg.ToAddress, msg.ToDisplayName } });
        
            
            try
            {
                var businessTemp = new EmailSettingBusinessLogic();
                var setting = businessTemp.GetFirstEmailSetting();
                if (setting != null)
                {
                    var message = new MailMessage();
                    {
                        message.From = new MailAddress(mailFromAddr, mailFromName);
                        message.To.Add(new MailAddress(mailToAddr, mailToName));
                        message.Subject = mailFromSubj;
                        message.Body = msg.Body;
                        message.IsBodyHtml = true;
                    }

                    var smtp = new System.Net.Mail.SmtpClient();
                    {
                        //smtp.Host = "smtp.gmail.com";
                        smtp.Host = setting.Host;
                        //smtp.Port = 587;
                        smtp.Port = setting.Port;
                        //smtp.EnableSsl = true;
                        smtp.EnableSsl = setting.EnableSsl;
                        //smtp.Timeout = 20000;
                        smtp.Timeout = setting.Timeout;

                        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(setting.AuthenticationUser, setting.AuthenticationPassword);
                    }
                    // Passing values to smtp object
                    //smtp.Send(message.FromAddress, message.ToAddress, message.Subject, message.Body);
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

        protected void SendMail(MailMsg msg, Dictionary<string, string> mailTo)
        {
            try
            {
                var business = new EmailSettingBusinessLogic();
                var setting = business.GetFirstEmailSetting();

                if (setting != null)
                {
                    var message = new MailMessage();
                    {
                        if (EPATheme.Current!=null && EPATheme.Current.Themes!= null && !string.IsNullOrEmpty(EPATheme.Current.Themes.WebsiteName))
                        {
                            message.From = new MailAddress(setting.Sender, EPATheme.Current.Themes.WebsiteName);    
                        }
                        else
                        {
                            message.From = new MailAddress(setting.Sender, "ePosAnytime");
                        }
                        
                        foreach (var pair in mailTo)
                        {
                            message.To.Add(new MailAddress(pair.Key, pair.Value));
                        }
                        message.Subject = msg.Subject;
                        message.Body = msg.Body;
                        message.IsBodyHtml = true;
                    }

                    // smtp settings
                    var smtp = new System.Net.Mail.SmtpClient();
                    {
                        //smtp.Host = "smtp.gmail.com";
                        smtp.Host = setting.Host;
                        //smtp.Port = 587;
                        smtp.Port = setting.Port;
                        //smtp.EnableSsl = true;
                        smtp.EnableSsl = setting.EnableSsl;
                        //smtp.Timeout = 20000;
                        smtp.Timeout = setting.Timeout;

                        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(setting.AuthenticationUser, setting.AuthenticationPassword);
                    }
                    // Passing values to smtp object
                    //smtp.Send(message.FromAddress, message.ToAddress, message.Subject, message.Body);
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
    }


    public class MailMsg
    {
        public string ToAddress { get; set; }
        public string ToDisplayName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}