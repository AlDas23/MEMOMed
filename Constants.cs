using System;
using System.IO;

namespace MEMOMed;

public static class Constants
{
    public static readonly string DbPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MEMOMed",
            "MEMOMed.db");
}