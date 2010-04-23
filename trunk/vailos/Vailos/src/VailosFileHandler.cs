using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vailos
{
    class VailosFileHandler
    {
        public string removeIllegalFilenameChars(string inString)
        {
            bool isBad;
            char[] illegalChars = { '<', '>', ':', '"', '/', '\\', '|', '?', '*'};
            string testString = inString;

            for (int i = 0; i < inString.Length; i++)
            {
                foreach (char bad in illegalChars)
                {
                    if (testString[i] == bad)
                    {
                        isBad = true;
                        break;
                    }
                    else
                    {
                        isBad = false;
                    }
                }

            }

            return null;
        }
    }
}
