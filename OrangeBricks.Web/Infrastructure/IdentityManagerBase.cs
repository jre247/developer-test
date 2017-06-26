using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeBricks.Web.Infrastructure
{
    public interface IdentityManagerBase
    {
        bool RoleExists(string name);
        bool CreateRole(string name);
        bool AddUserToRole(string userId, string roleName);
        string GetLoggedInUserId();
    }
}
