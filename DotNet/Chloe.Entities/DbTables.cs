using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
    public static class DbTables
    {
        public static IQuery<User> GetUsers(this IDbContext dbContext)
        {
            return dbContext.Query<User>();
        }
        public static IQuery<WikiDocument> GetWikiDocuments(this IDbContext dbContext)
        {
            return dbContext.Query<WikiDocument>();
        }

        public static IQuery<MALU_Users> GetInv_users(this IDbContext dbContext)
        {
            return dbContext.Query<MALU_Users>();
        }
        public static IQuery<Sys_Role> GetSys_Role(this IDbContext dbContext)
        {
            return dbContext.Query<Sys_Role>();
        }
        public static IQuery<Sys_UserLogOn> GetSys_UserLogOns(this IDbContext dbContext)
        {
            return dbContext.Query<Sys_UserLogOn>();
        }
        public static IQuery<Sys_Module> GetSys_Modules(this IDbContext dbContext)
        {
            return dbContext.Query<Sys_Module>();
        }
        public static IQuery<Sys_RoleAuthorize> GetSys_RoleAuthorizes(this IDbContext dbContext)
        {
            return dbContext.Query<Sys_RoleAuthorize>();
        }

        public static IQuery<PlaceInfo> GetPlaceInfo(this IDbContext dbContext)
        {
            return dbContext.Query<PlaceInfo>();
        }

    }
}
