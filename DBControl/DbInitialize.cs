using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace MEMOMed.DBControl;

public static class DbInitialize
{
    public static async Task InitializeAsync(string connectionPath, string dbSchema)
    {
        var directory = Path.GetDirectoryName(Constants.DbPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        else if (File.Exists(Constants.DbPath) && !IsDbSchemaCorrect(await GetExistingDbSchema(connectionPath), dbSchema))
        {
            File.Delete(Constants.DbPath);
        }

        await using var conn = new SqliteConnection(connectionPath);
        await conn.OpenAsync();

        await using var cmd = conn.CreateCommand();
        cmd.CommandText = dbSchema;
        await cmd.ExecuteNonQueryAsync();
    }

    private static async Task<string> GetExistingDbSchema(string connectionPath)
    {
        await using var conn = new SqliteConnection(connectionPath);
        await conn.OpenAsync();

        await using var cmd = conn.CreateCommand();
        cmd.CommandText =
            "SELECT sql FROM sqlite_master WHERE type='table' OR type='index' OR type='view' OR type='trigger' ORDER BY type, name";
        await using var reader = await cmd.ExecuteReaderAsync();
        var schemaParts = new System.Text.StringBuilder();
        while (await reader.ReadAsync())
        {
            var sql = reader.GetString(0);
            if (!string.IsNullOrWhiteSpace(sql))
            {
                schemaParts.AppendLine(sql);
                schemaParts.AppendLine(";");
            }
        }

        return schemaParts.ToString().Trim();
    }

    private static bool IsDbSchemaCorrect(string existingDbSchema, string dbSchema)
    {
        var normalizedExistingSchema = Regex.Replace(existingDbSchema, @"\s+", " ");
        normalizedExistingSchema = Regex.Replace(normalizedExistingSchema, @"^\s+|\s+$", "", RegexOptions.Multiline);
        var normalizedSchema = Regex.Replace(dbSchema, @"\s+", " ");
        normalizedSchema = Regex.Replace(normalizedSchema, @"^\s+|\s+$", "", RegexOptions.Multiline);

        return normalizedSchema == normalizedExistingSchema;
    }
}