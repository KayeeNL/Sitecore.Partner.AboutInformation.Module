// /*===========================================================
//    Copyright 2013 Robbert Hock
//  
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//  
// ============================================================*/

using System.Text;
using Sitecore.Data.Fields;
using Sitecore.Partner.AboutInformation.Module.FixedPaths.System.Modules.SitecorePartnerAboutInformationModule;
using Sitecore.Partner.AboutInformation.Module.Models;
using Sitecore.Pipelines.GetAboutInformation;
using Sitecore.Resources.Media;

namespace Sitecore.Partner.AboutInformation.Module
{
    public class GetAboutInformationProcessor
    {
        /// <summary>
        /// Processes the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        public void Process([NotNull] GetAboutInformationArgs args)
        {
            // 1st: The about information for the about text
            string aboutInformation = SettingsFixed.SettingsFromMaster.AboutInformation;
            if (!string.IsNullOrWhiteSpace(aboutInformation))
            {
                args.AboutText = aboutInformation;
            }


            // 2nd: The partner information for the Sitecore login page
            var loginPageStringBuilder = new StringBuilder();
            string header = SettingsFixed.SettingsFromMaster.Header;
            string slogan = SettingsFixed.SettingsFromMaster.Slogan;
            string partnerLogo = SettingsFixed.SettingsFromMaster.PartnerLogo;
            string website = SettingsFixed.SettingsFromMaster.PartnerWebsiteUrl;

            if (!string.IsNullOrWhiteSpace(header))
            {
                loginPageStringBuilder.AppendFormat("<h3>{0}</h3>", header);
            }

            if (!string.IsNullOrWhiteSpace(partnerLogo))
            {
                var imageField = (ImageField) ((SettingsFixed.SettingsFromMaster)).Item.Fields[Settings.FIELD_PARTNER_LOGO];
                if (imageField != null && imageField.MediaItem != null)
                {
                    if (!string.IsNullOrWhiteSpace(website))
                    {
                        loginPageStringBuilder.AppendFormat("<a href='{0}' target='_blank'>", website);
                    }

                    loginPageStringBuilder.AppendFormat("<img src='{0}?mw=210' />", MediaManager.GetMediaUrl(imageField.MediaItem).Replace("/sitecore/login", string.Empty));
                    
                    if (!string.IsNullOrWhiteSpace(website))
                    {
                        loginPageStringBuilder.Append("</a>");
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(slogan))
            {
                loginPageStringBuilder.AppendFormat("<h3>{0}</h3>", slogan);
            }

            args.LoginPageText = loginPageStringBuilder.ToString();
        }
    }
}