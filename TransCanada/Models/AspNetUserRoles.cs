using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections.Generic;

namespace TransCanada.Models
{
  public class AspNetUserRoles
  {
    public string Id { get; set; }

    public string Name { get; set; }

    public List<AspNetUserRoles> Category { get; set; }

    public List<User_Roles> Users { get; set; }
  }
}
