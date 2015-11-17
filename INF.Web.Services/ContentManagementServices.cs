using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using INF.Database;
using INF.Web.Data.BLL;
using INF.Web.Data.Entities;

namespace INF.Web.Services
{
    public class ContentManagementServices
    {
        private readonly ISessionFactory _sessionFactory;

        #region "Constants Definition"

        public const string DEFAULT_CONNECTION_STRING = "ConnectionStringDefault";

        #endregion

        public ContentManagementServices()
        {
            Assembly asm = Assembly.Load("INF.Web.Data");
            _sessionFactory = SessionFactory.Create(asm, GetDbConnectionString());
        }

        //public string DBServerName { get; set; }

        protected string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Gets connection string that is specified in the appp.config
        /// </summary>
        /// <returns>Return the connection string. If it's not specified in app.config, this will retun the detault connection string</returns>
        /// <remarks></remarks>
        private string GetDbConnectionString()
        {
            //if (string.IsNullOrEmpty(DBServerName))
            //{
            //    DBServerName = SystemInformation.ComputerName;
            //}

            string connectionStr = string.Empty;
            string connectionStringKey = string.Empty;
            if (ConfigurationManager.AppSettings[DEFAULT_CONNECTION_STRING] != null)
            {
                connectionStringKey = ConfigurationManager.AppSettings[DEFAULT_CONNECTION_STRING].Trim();
            }

            if (!string.IsNullOrEmpty(connectionStringKey))
            {
                connectionStr = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
            }

            return connectionStr;
        }

        #region "PostCodes Management"

        public List<CsPostCodePrice> GetAllPostCodeCharges()
        {
            using (ISession session = _sessionFactory.CreateSession())
            {
                return session.FindAll<CsPostCodePrice>().ToList();
            }
        }

        public CsPostCodePrice SavePostCodeCharge(CsPostCodePrice postCodesPrices)
        {
            using (ISession session = _sessionFactory.CreateSession())
            {
                try
                {
                    postCodesPrices = postCodesPrices.ID == 0
                                          ? session.Insert(postCodesPrices)
                                          : session.Update(postCodesPrices);
                    session.Commit();
                }
                catch
                {
                    session.Rollback();
                }
            }
            return postCodesPrices;
        }

        public void DeletePostCodeCharge(CsPostCodePrice postCodesPrices)
        {
            using (ISession session = _sessionFactory.CreateSession())
            {
                try
                {
                    session.Delete(postCodesPrices);
                    session.Commit();
                }
                catch
                {
                    session.Rollback();
                }
            }
        }

        public CsPostCodePrice GetPostCodePrice(string postCode)
        {
            using (ISession session = _sessionFactory.CreateSession())
            {
                try
                {
                    IQuery query = session.CreateQuery<CsPostCodePrice>("WHERE [POST_CODE] = '" + postCode + "'");
                    return query.GetSingleResult<CsPostCodePrice>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        #endregion

        #region "Restaurant Management"

        public CsRestaurant SaveRestaurantChanges(CsRestaurant restaurant)
        {
            using (ISession session = _sessionFactory.CreateSession())
            {
                try
                {
                    restaurant = restaurant.ID == 0 ? session.Insert(restaurant) : session.Update(restaurant);
                    session.Commit();
                }
                catch
                {
                    return null;
                }
            }
            return restaurant;
        }

        public CsRestaurant LoadRestaurantInfo()
        {
            using (ISession session = _sessionFactory.CreateSession())
            {
                try
                {
                    IQuery query = session.CreateQuery<CsRestaurant>("");
                    return query.GetSingleResult<CsRestaurant>();
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region "Menu-Options"

        public CsMenuOption SaveMenuOption(CsMenuOption menuOpt)
        {
            using (var session = _sessionFactory.CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    CsMenuOption savedMenuOpt = menuOpt.ID == 0 ? session.Insert(menuOpt) : session.Update(menuOpt);
                    tranx.Commit();
                    return savedMenuOpt;
                }
                catch (Exception)
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }

        public bool DeleteMenuOption(CsMenuOption menuOpt)
        {
            using (var session = _sessionFactory.CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    session.Delete(menuOpt);
                    tranx.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }

        public List<CsMenuOption> GetAllMenuOptions()
        {
            using (var session = _sessionFactory.CreateSession())
            {
                return session.FindAll<CsMenuOption>().ToList();
            }
        }

        public CsOptionDetail SaveOptionDetail(CsOptionDetail optDetail)
        {
            using (var session = _sessionFactory.CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    CsOptionDetail savedMenuOpt = optDetail.ID == 0 ? session.Insert(optDetail) : session.Update(optDetail);
                    tranx.Commit();
                    return savedMenuOpt;
                }
                catch (Exception)
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }

        public bool DeleteOptionDetail(CsOptionDetail optDetail)
        {
            using (var session = _sessionFactory.CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    session.Delete(optDetail);
                    tranx.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }

        public List<CsOptionDetail> GetAllOptionDetails(int optionID)
        {
            using (var session = _sessionFactory.CreateSession())
            {
                IQuery query = session.CreateQuery<CsOptionDetail>(" WHERE [OptionID] = " + optionID);
                return query.GetResults<CsOptionDetail>().ToList();
            }
        }

        #endregion

        #region "Flat Fields"

        public string GetFlatFieldValue(string fieldName)
        {
            string value = string.Empty;
            using (var session = _sessionFactory.CreateSession())
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
            using (var session = _sessionFactory.CreateSession())
            {
                var query = session.CreateQuery<CsFlatFieldsName>(" WHERE FieldName = '" + name + "'");
                var fieldsName = query.GetSingleResult<CsFlatFieldsName>();

                query = session.CreateQuery<CsFlatFieldsValue>(" WHERE FieldName = '" + name + "'");
                var fieldsValue = query.GetSingleResult<CsFlatFieldsValue>();

                var tranx = session.GetTransaction();
                try
                {
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
                catch (Exception)
                {
                    tranx.Rollback();
                    throw;
                }
            }
            return savedValue;
        }

        #endregion
    }
}