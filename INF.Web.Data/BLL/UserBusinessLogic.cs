using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.DAL.SqlClient;
using INF.Web.Data.Entities;
using Microsoft.SqlServer.Server;

namespace INF.Web.Data.BLL
{
    public class UserBusinessLogic : BaseBusinessLogic
    {
        public IEnumerable<CsUser> GetAllUsers(bool exclInAtivedOnes)
        {
            return UserProvider.Instance.GetAllUsers(exclInAtivedOnes);
        }

        public CsUser GetUser(int id)
        {
            return UserProvider.Instance.GetUser(id);
        }

        public CsUser GetUser(string username)
        {
            if (username == "fake")
            {
                var fakeUser = new CsUser()
                {
                    ID = 9999,
                    Email = "fake@eposanytime.co.uk",
                    RoleID = 1,
                    UserName = "system"
                };
                return fakeUser;
            }
            return UserProvider.Instance.GetUser(username);
        }

        public CsUser GetUserByEmail(string username)
        {
            return UserProvider.Instance.GetUserByEmail(username);
        }

        public CsUser SaveUser(CsUser user, string updateBy)
        {
            if (user == null)
                return null;

            user.LastLoggedIn = DateTime.MinValue.AddYears(2000);
            user.UpdatedBy = updateBy;
            user.LastUpdated = DateTime.Now;
            return UserProvider.Instance.SaveUser(user);
        }

        public bool BlockUser(int userId, string blockBy)
        {
            var user = UserProvider.Instance.GetUser(userId);
            if (user == null) return false;

            user.IsActived = false;
            user.UpdatedBy = blockBy;
            user.LastUpdated = DateTime.Now;
            return UserProvider.Instance.SaveUser(user) != null;
        }

        public bool BlockUser(string username, string blockBy)
        {
            var user = UserProvider.Instance.GetUser(username);
            if (user == null) return false;

            user.IsActived = false;
            user.UpdatedBy = blockBy;
            user.LastUpdated = DateTime.Now;
            return UserProvider.Instance.SaveUser(user) != null;
        }

        public bool UnBlockUser(int userId, string blockBy)
        {
            var user = UserProvider.Instance.GetUser(userId);
            if (user == null) return false;

            user.IsActived = true;
            user.UpdatedBy = blockBy;
            user.LastUpdated = DateTime.Now;
            return UserProvider.Instance.SaveUser(user) != null;
        }

        public bool UnBlockUser(string username, string blockBy)
        {
            var user = UserProvider.Instance.GetUser(username);
            if (user == null) return false;

            user.IsActived = true;
            user.UpdatedBy = blockBy;
            user.LastUpdated = DateTime.Now;
            return UserProvider.Instance.SaveUser(user) != null;
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword, string updatedBy)
        {
            var user = UserProvider.Instance.GetUser(username);
            if (user == null) return false;

            var encryptedOldPassword = CryptoUtility.EncryptText(oldPassword);
            if (user.UserName.ToLower() == username.ToLower() && user.Password == encryptedOldPassword)
            {
                var encryptedNewPassword = CryptoUtility.EncryptText(newPassword);
                user.Password = encryptedNewPassword;

                user.UpdatedBy = updatedBy;
                user.LastUpdated = DateTime.Now;
                return UserProvider.Instance.SaveUser(user) != null;
            }

            return false;
        }

        public bool SetNewPassword(string username, string newPassword)
        {
            var user = UserProvider.Instance.GetUser(username);
            if (user == null) return false;
            
            if (user.UserName.ToLower() == username.ToLower())
            {
                var encryptedNewPassword = CryptoUtility.EncryptText(newPassword);
                user.Password = encryptedNewPassword;

                user.UpdatedBy = "system";
                user.LastUpdated = DateTime.Now;
                return UserProvider.Instance.SaveUser(user) != null;
            }

            return false;
        }

        public bool ValidateUser(string username, string password)
        {
            if (IsFakeUser(username, password))
                return true;

            var user = UserProvider.Instance.GetUser(username);
            if (user == null) return false;

            var encryptedPassword = CryptoUtility.EncryptText(password);
            if (user.UserName.ToLower() == username.ToLower() && user.Password == encryptedPassword)
            {
                user.LastLoggedIn = DateTime.Now;
                return UserProvider.Instance.SaveUser(user) != null;
            }

            return false;
        }

        public bool IsFakeUser(string username, string password)
        {
            var allSysUsers = UserProvider.Instance.GetAllUsers(true);
            if (allSysUsers != null && !allSysUsers.Any())
            {
                if (username == "fake" && password == "anytime@" + DateTime.Now.ToString("dd"))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckPassword(int userId, string password)
        {
            var user = UserProvider.Instance.GetUser(userId);
            if (user == null) return false;

            var encryptedPassword = CryptoUtility.EncryptText(password);
            return user.Password == encryptedPassword;
        }

        public bool ExistedUserName(string username)
        {
            var user = UserProvider.Instance.GetUser(username);
            if (user == null) return false;

            return (user.UserName.ToLower() == username.ToLower());
        }

        public bool ExistedUserName(int userId, string username)
        {
            var user = UserProvider.Instance.GetUser(username);
            if (user == null) return false;

            if (user.ID == userId) return false;

            return (user.UserName.ToLower() == username.ToLower());
        }
    }
}
