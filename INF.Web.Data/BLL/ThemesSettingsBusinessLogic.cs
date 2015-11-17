using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using INF.Web.Data.DAL.SqlClient;

namespace INF.Web.Data.BLL
{
    public class ThemesSettingsBusinessLogic : BaseBusinessLogic
    {
        public bool SaveLogoAndSlogan(LogoAndSloganSettings logoAndSlogan)
        {
            FlatFieldsProvider.Instance.SaveFlatField("slogan", logoAndSlogan.Slogan);
            FlatFieldsProvider.Instance.SaveFlatField("logo_width", logoAndSlogan.LogoWidth);
            FlatFieldsProvider.Instance.SaveFlatField("logo_height", logoAndSlogan.LogoHeight);
            FlatFieldsProvider.Instance.SaveFlatField("logo", logoAndSlogan.LogoUrl);
            return true;
        }

        public LogoAndSloganSettings LoadLogoAndSloganSettings()
        {
            var logoAndSloganSettings = new LogoAndSloganSettings();
            logoAndSloganSettings.Slogan = FlatFieldsProvider.Instance.GetFlatFieldValue("slogan");
            logoAndSloganSettings.LogoUrl = FlatFieldsProvider.Instance.GetFlatFieldValue("logo");
            logoAndSloganSettings.LogoWidth = FlatFieldsProvider.Instance.GetFlatFieldValue("logo_width");
            logoAndSloganSettings.LogoHeight = FlatFieldsProvider.Instance.GetFlatFieldValue("logo_height");
            return logoAndSloganSettings;
        }

        public NavigationImagesSettings LoadNavigationImagesSettings()
        {
            var logoAndSloganSettings = new NavigationImagesSettings();
            logoAndSloganSettings.ImageUrl = FlatFieldsProvider.Instance.GetFlatFieldValue("nav_image");
            logoAndSloganSettings.ImageWidth = FlatFieldsProvider.Instance.GetFlatFieldValue("nav_image_width");
            logoAndSloganSettings.ImageHeight = FlatFieldsProvider.Instance.GetFlatFieldValue("nav_image_height");

            logoAndSloganSettings.ImageHoverUrl = FlatFieldsProvider.Instance.GetFlatFieldValue("nav_image_hover");
            logoAndSloganSettings.ImageHoverWidth = FlatFieldsProvider.Instance.GetFlatFieldValue("nav_image_hover_width");
            logoAndSloganSettings.ImageHoverHeight = FlatFieldsProvider.Instance.GetFlatFieldValue("nav_image_hover_height");
            return logoAndSloganSettings;
        }

        public bool SaveNavigationImagesSettings(NavigationImagesSettings navigationImagesSettings)
        {
            FlatFieldsProvider.Instance.SaveFlatField("nav_image", navigationImagesSettings.ImageUrl);
            FlatFieldsProvider.Instance.SaveFlatField("nav_image_width", navigationImagesSettings.ImageWidth);
            FlatFieldsProvider.Instance.SaveFlatField("nav_image_height", navigationImagesSettings.ImageHeight);

            FlatFieldsProvider.Instance.SaveFlatField("nav_image_hover", navigationImagesSettings.ImageHoverUrl);
            FlatFieldsProvider.Instance.SaveFlatField("nav_image_hover_width", navigationImagesSettings.ImageHoverWidth);
            FlatFieldsProvider.Instance.SaveFlatField("nav_image_hover_height", navigationImagesSettings.ImageHoverHeight);

            return true;
        }

        public HeaderSettings LoadHeaderSettings()
        {
            var headerSettings = new HeaderSettings();
            headerSettings.ImageUrl = FlatFieldsProvider.Instance.GetFlatFieldValue("header_background_image");
            headerSettings.ImageWidth = FlatFieldsProvider.Instance.GetFlatFieldValue("header_background_image_width");
            headerSettings.ImageHeight = FlatFieldsProvider.Instance.GetFlatFieldValue("header_background_image_height");

            return headerSettings;
        }

        public bool SaveHeaderSettings(HeaderSettings headerSettings)
        {

            FlatFieldsProvider.Instance.SaveFlatField("header_background_image", headerSettings.ImageUrl);
            FlatFieldsProvider.Instance.SaveFlatField("header_background_image_width", headerSettings.ImageWidth);
            FlatFieldsProvider.Instance.SaveFlatField("header_background_image_height", headerSettings.ImageHeight);

            return true;

        }

        public FooterSettings LoadFooterSettings()
        {
            var footerSettings = new FooterSettings();
            footerSettings.ImageUrl = FlatFieldsProvider.Instance.GetFlatFieldValue("footer_background_image");
            footerSettings.ImageWidth = FlatFieldsProvider.Instance.GetFlatFieldValue("footer_background_image_width");
            footerSettings.ImageHeight = FlatFieldsProvider.Instance.GetFlatFieldValue("footer_background_image_height");

            return footerSettings;
        }

        public bool SaveFooterSettings(FooterSettings footerSettings)
        {

            FlatFieldsProvider.Instance.SaveFlatField("footer_background_image", footerSettings.ImageUrl);
            FlatFieldsProvider.Instance.SaveFlatField("footer_background_image_width", footerSettings.ImageWidth);
            FlatFieldsProvider.Instance.SaveFlatField("footer_background_image_height", footerSettings.ImageHeight);

            return true;

        }

        public HomePageSettings LoadHomePageSettings()
        {

            var hpSettings = new HomePageSettings();
            hpSettings.ImageUrl = FlatFieldsProvider.Instance.GetFlatFieldValue("homepage_background_image");
            hpSettings.ImageWidth = FlatFieldsProvider.Instance.GetFlatFieldValue("homepage_background_image_width");
            hpSettings.ImageHeight = FlatFieldsProvider.Instance.GetFlatFieldValue("homepage_background_image_height");

            return hpSettings;

        }

        public bool SaveHomePageSettings(HomePageSettings hpSettings)
        {

            FlatFieldsProvider.Instance.SaveFlatField("homepage_background_image", hpSettings.ImageUrl);
            FlatFieldsProvider.Instance.SaveFlatField("homepage_background_image_width", hpSettings.ImageWidth);
            FlatFieldsProvider.Instance.SaveFlatField("homepage_background_image_height", hpSettings.ImageHeight);

            return true;

        }

        public WebsiteInformation LoadWebsiteInformation()
        {

            var websiteInfo = new WebsiteInformation();
            websiteInfo.Name = FlatFieldsProvider.Instance.GetFlatFieldValue("website_name");
            websiteInfo.Meta = FlatFieldsProvider.Instance.GetFlatFieldValue("website_meta");
            return websiteInfo;

        }

        public bool SaveWebsiteInformation(WebsiteInformation websiteInfo)
        {

            FlatFieldsProvider.Instance.SaveFlatField("website_name", websiteInfo.Name);
            FlatFieldsProvider.Instance.SaveFlatField("website_meta", websiteInfo.Meta);
            return true;

        }

        public MenuCategorySettings LoadMenuCategorySettings()
        {
            var menuCatSettings = new MenuCategorySettings();
            menuCatSettings.Width = FlatFieldsProvider.Instance.GetFlatFieldValue("menu_category_width");
            menuCatSettings.Height = FlatFieldsProvider.Instance.GetFlatFieldValue("menu_category_height");
            return menuCatSettings;
        }

        public bool SaveMenuCategorySettings(MenuCategorySettings menuCategorySettings)
        {
            FlatFieldsProvider.Instance.SaveFlatField("menu_category_width", menuCategorySettings.Width);
            FlatFieldsProvider.Instance.SaveFlatField("menu_category_height", menuCategorySettings.Height);
            return true;
        }

        public ColorSettings LoadColorSettings()
        {
            var colors = new ColorSettings();
            colors.BaseColor = FlatFieldsProvider.Instance.GetFlatFieldValue("base_color");
            colors.BackColor = FlatFieldsProvider.Instance.GetFlatFieldValue("back_color");
            colors.EditOrderImageUrl = FlatFieldsProvider.Instance.GetFlatFieldValue("edit_order_image_url");
            colors.ConfirmOrderImageUrl = FlatFieldsProvider.Instance.GetFlatFieldValue("confirm_order_image_url");
            colors.CheckOutImageUrl = FlatFieldsProvider.Instance.GetFlatFieldValue("check_out_image_url");
            colors.AddToCartImageUrl = FlatFieldsProvider.Instance.GetFlatFieldValue("add_to_cart_image_url");
            return colors;
        }

        public bool SaveColorSettings(ColorSettings colors)
        {
            FlatFieldsProvider.Instance.SaveFlatField("base_color", colors.BaseColor);
            FlatFieldsProvider.Instance.SaveFlatField("back_color", colors.BackColor);
            FlatFieldsProvider.Instance.SaveFlatField("edit_order_image_url", colors.EditOrderImageUrl);
            FlatFieldsProvider.Instance.SaveFlatField("confirm_order_image_url", colors.ConfirmOrderImageUrl);
            FlatFieldsProvider.Instance.SaveFlatField("check_out_image_url", colors.CheckOutImageUrl);
            FlatFieldsProvider.Instance.SaveFlatField("add_to_cart_image_url", colors.AddToCartImageUrl);
            return true;
        }
    }

    public class ColorSettings
    {
        public string BaseColor { get; set; }
        public string BackColor { get; set; }
        public string EditOrderImageUrl { get; set; }
        public string ConfirmOrderImageUrl { get; set; }
        public string CheckOutImageUrl { get; set; }
        public string AddToCartImageUrl { get; set; }
    }

    public class WebsiteInformation
    {
        public string Name { get; set; }
        public string Meta { get; set; }
    }

    public class HomePageSettings
    {
        public string ImageUrl { get; set; }
        public string ImageWidth { get; set; }
        public string ImageHeight { get; set; }
    }

    public class HeaderSettings
    {
        public string ImageUrl { get; set; }
        public string ImageWidth { get; set; }
        public string ImageHeight { get; set; }
    }

    public class FooterSettings
    {
        public string ImageUrl { get; set; }
        public string ImageWidth { get; set; }
        public string ImageHeight { get; set; }
    }

    public class NavigationImagesSettings
    {
        public string ImageWidth { get; set; }
        public string ImageHeight { get; set; }
        public string ImageUrl { get; set; }

        public string ImageHoverUrl { get; set; }
        public string ImageHoverWidth { get; set; }
        public string ImageHoverHeight { get; set; }
    }

    public class LogoAndSloganSettings
    {
        public string Slogan { get; set; }
        public string LogoUrl { get; set; }
        public string LogoWidth { get; set; }
        public string LogoHeight { get; set; }
    }

    public class MenuCategorySettings
    {
        public string Width { get; set; }
        public string Height { get; set; }
    }
}
