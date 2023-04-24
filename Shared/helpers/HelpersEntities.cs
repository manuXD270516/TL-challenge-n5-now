using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.helpers
{
    public class HelpersEntities
    {

        public static string SOFT_DELETE_TAG = "SoftDelete";
        public static bool isNull<T>(T model) => model == null;

    }
}
