using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class FlatFieldsProvider : DataAccess
    {
        public readonly string[] DefaultFlatFields = new[]
                                                         {
                                                             "logo", "logo_width", "logo_height",
                                                             "slogan",
                                                             "nav_image", "nav_image_width", "nav_image_height",
                                                             "nav_image_hover", "nav_image_hover_width",
                                                             "nav_image_hover_height",
                                                             "header_background_image", "header_background_image_width",
                                                             "header_background_image_height",
                                                             "footer_background_image", "footer_background_image_width",
                                                             "footer_background_image_height",
                                                             "homepage_background_image",
                                                             "homepage_background_image_width",
                                                             "homepage_background_image_height",
                                                             "wesbite_name",
                                                             "website_meta",
                                                             "menu_category_width", "menu_category_height",
                                                             "base_color", "background_color",
                                                             "edit_order_image_url", "confirm_order_image_url",
                                                             "check_out_image_url", "add_to_cart_image_url"
                                                         };

        private static FlatFieldsProvider _instance;

        static FlatFieldsProvider()
        {
            _instance = new FlatFieldsProvider();
        }

        FlatFieldsProvider()
        {
            InitDefaultFlatFields();
        }

        public static FlatFieldsProvider Instance
        {
            get { return _instance ?? (_instance = new FlatFieldsProvider()); }
        }

        private void InitDefaultFlatFields()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();

                foreach (var defaultFlatField in DefaultFlatFields)
                {
                    var flatField = new CsFlatFieldsName { FieldName = defaultFlatField };
                    session.Insert(flatField);
                }

                tranx.Commit();
            }
        }

        public string GetFlatFieldValue(string fieldName)
        {
            string value = string.Empty;
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsFlatFieldsValue>(" WHERE FieldName = '" + fieldName + "'");
                var fieldsValue = query.GetSingleResult<CsFlatFieldsValue>();
                if (fieldsValue != null)
                    value = fieldsValue.FieldValue;
            }
            return value;
        }

        public string SaveFlatField(string name, string value)
        {
            string savedValue;
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsFlatFieldsName>(" WHERE FieldName = '" + name + "'");
                var fieldsName = query.GetSingleResult<CsFlatFieldsName>();


                query = session.CreateQuery<CsFlatFieldsValue>(" WHERE FieldName = '" + name + "'");
                var fieldsValue = query.GetSingleResult<CsFlatFieldsValue>();

                var tranx = session.GetTransaction();
                // Insert this new field
                if (fieldsName == null)
                {
                    fieldsName = new CsFlatFieldsName { FieldName = name };
                    session.Insert(fieldsName);
                }

                // Save its value to FlatFieldsValue table
                if (fieldsValue == null)
                    fieldsValue = new CsFlatFieldsValue { FieldName = name };

                fieldsValue.FieldValue = value;
                var savedObj = fieldsValue.ID > 0 ? session.Update(fieldsValue) : session.Insert(fieldsValue);
                savedValue = savedObj.FieldValue;
                tranx.Commit();
            }
            return savedValue;
        }
    }
}
