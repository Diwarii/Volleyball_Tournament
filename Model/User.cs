using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work.Model
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!; // В реальном приложении храните хэш пароля!
        public string Name { get; set; } = null!;
        public int Age { get; set; }
    }
}
