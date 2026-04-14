using System;
using System.IO;
using MEMOMed.Models.DataClasses;

namespace MEMOMed;

public static class Constants
{
    public static readonly string DbPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MEMOMed",
            "MEMOMed.db");

    public static readonly string DbConnectionString = $"Data Source={DbPath}";
    
    public static Person? SelectedPerson;
}