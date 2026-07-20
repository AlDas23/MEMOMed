using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace MEMOMed.DBControl;

public static class DbInitialize
{
    public static async Task InitializeAsync(string connectionPath)
    {
        var isNewDb = false;
        var directory = Path.GetDirectoryName(Constants.DbPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        else if (File.Exists(Constants.DbPath) &&
                 !IsDbVersionCorrect(await GetExistingDbSchema(connectionPath), Constants.CurrentDbVersion))
        {
            await using (var fs = new FileStream(Constants.DbPath, FileMode.Truncate)) {} // Temporary solution
            isNewDb = true;
        }

        await using var conn = new SqliteConnection(connectionPath);
        await conn.OpenAsync();

        await using var cmd = conn.CreateCommand();
        cmd.CommandText = Constants.DbSchema;
        await cmd.ExecuteNonQueryAsync();
        if (isNewDb)
        {
            cmd.CommandText = Constants.DbInit;
            await cmd.ExecuteNonQueryAsync();
        }
    }

    private static async Task<int> GetExistingDbSchema(string connectionPath)
    {
        try
        {
            await using var conn = new SqliteConnection(connectionPath);
            await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            cmd.CommandText =
                "SELECT Version FROM System";

            await using var reader = await cmd.ExecuteReaderAsync();
            var version = string.Empty;
            while (await reader.ReadAsync())
            {
                if (!reader.IsDBNull(0))
                {
                    version = reader.GetString(0);
                }
            }

            return string.IsNullOrEmpty(version) ? 0 : int.Parse(version);
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
        }
    }

    private static bool IsDbVersionCorrect(int existingDbVersion, int requiredDbVersion)
    {
        return existingDbVersion >= requiredDbVersion;
    }
}