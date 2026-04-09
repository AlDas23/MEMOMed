using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace MEMOMed.DBControl;

public static class DbInitialize
{
    public static async Task InitializeAsync(string connectionPath)
    {
        await using var conn = new SqliteConnection(connectionPath);
        await conn.OpenAsync();
        
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = """
            CREATE TABLE IF NOT EXISTS HeartMeasurements (
                Id INTEGER PRIMARY KEY AUTOINCREMENT
                PersonId INTEGER
                DateTime TEXT NOT NULL
                Sys INTEGER NOT NULL
                Dia INTEGER NOT NULL
                HRhytm INTEGER NOT NULL
                IsArrhythmia BOOLEAN NOT NULL
                FOREIGN KEY (PersonId) REFERENCES Person (Id)
            );
            CREATE TABLE IF NOT EXISTS BodyMeasurements (
                Id INTEGER PRIMARY KEY AUTOINCREMENT
                PersonId INTEGER
                DateTime TEXT NOT NULL
                Temperature REAL NOT NULL
                FOREIGN KEY (PersonId) REFERENCES Person (Id)
            ); 
            CREATE TABLE IF NOT EXISTS FeelingMeasurements (
                Id INTEGER PRIMARY KEY AUTOINCREMENT
                PersonId INTEGER
                DateTime TEXT NOT NULL
                Medication TEXT
                Feeling TEXT NOT NULL
                FOREIGN KEY (PersonId) REFERENCES Person (Id)
            );
            CREATE TABLE IF NOT EXISTS Medicine (
                Id INTEGER PRIMARY KEY AUTOINCREMENT
                Name TEXT NOT NULL UNIQUE
                Description TEXT NOT NULL
                ScheduleDays TEXT NOT NULL
                ScheduleDayTime TEXT NOT NULL
            );
            CREATE TABLE IF NOT EXISTS Person (
                Id INTEGER PRIMARY KEY AUTOINCREMENT
                FirstName TEXT NOT NULL
                LastName TEXT NOT NULL
            );
            CREATE TABLE IF NOT EXISTS PersonMedicine (
                PersonId INTEGER NOT NULL
                MedicineId INTEGER NOT NULL
                FOREIGN KEY (PersonId) REFERENCES Person (Id)
                ON DELETE CASCADE
                FOREIGN KEY (MedicineId) REFERENCES Medicine (Id)
                ON DELETE CASCADE
            )
            """;
        await cmd.ExecuteNonQueryAsync();
    }
}