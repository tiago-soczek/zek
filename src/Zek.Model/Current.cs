using System;

namespace Zek.Model
{
    public static class Current
    {
        public static string User => Environment.UserName;
        public static DateTimeOffset Now => DateTimeOffset.Now;
    }
}
