using System;
using System.Threading.Tasks;

namespace Tawala.Domain.Common
{
    public static class CommonHelper
    {
        public static Tuple<string, string> SplitFullName(string fullName)
        {
            if (!string.IsNullOrEmpty(fullName))
            {
                var names = fullName.Split(' ');
                if (names.Length == 2)
                    if (!string.IsNullOrEmpty(names[0]) && !string.IsNullOrEmpty(names[1]))
                        return new Tuple<string, string>(names[0], names[1]);
            }
            return null;
        }

    }
}
