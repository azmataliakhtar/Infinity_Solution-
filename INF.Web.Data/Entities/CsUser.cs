using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("User")]
    public class CsUser : EntityBase
    {
        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("LastLoggedIn")]
        public DateTime LastLoggedIn { get; set; }

        [Column("IsActived")]
        public bool IsActived { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("RoleID")]
        public int RoleID { get; set; }

        [Column("LastUpdated")]
        public DateTime LastUpdated { get; set; }

        [Column("UpdatedBy")]
        public string UpdatedBy { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        public string ComfirmPassword { get; set; }

        public string ComfirmEmail { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}",LastName, FirstName); }
        }

        public UserRoles UserRole
        {
            get
            {
                if (RoleID == 1)
                    return UserRoles.Administrator;
                else if (RoleID == 2)
                    return UserRoles.Manager;
                else if (RoleID == 3)
                    return UserRoles.RestrictedUser;
                else
                    return UserRoles.UnDefined;
            }
        }
    }

    /// <summary>
    /// Defines the user roles
    /// </summary>
    public enum UserRoles : short
    {
        UnDefined = 0,
        Administrator = 1,
        Manager = 2,
        RestrictedUser = 3
    }
}
