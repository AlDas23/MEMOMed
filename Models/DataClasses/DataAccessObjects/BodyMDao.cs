using System;
using System.Collections.Generic;
using MEMOMed.Models.Interfaces;
using Microsoft.Data.Sqlite;

namespace MEMOMed.Models.DataClasses.DataAccessObjects;

public class BodyMDao : IMeasurementDao<BodyMeasurement>
{
    public void InsertRecord(BodyMeasurement record)
    {
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              INSERT INTO BodyMeasurements
                              VALUES (NULL, @PersonId, @Datetime, @Temperature)
                              """;
            cmd.Parameters.AddWithValue("@PersonId", record.PersonId);
            cmd.Parameters.AddWithValue("@Datetime", record.Date);
            cmd.Parameters.AddWithValue("@Temperature", record.Temperature);

            cmd.ExecuteNonQuery();
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public BodyMeasurement? GrabRecordById(int id)
    {
        BodyMeasurement record = new();
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM BodyMeasurements
                              WHERE Id = @id
                              """;
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    record.Id = reader.GetInt32(0);
                    record.PersonId = reader.GetInt32(1);
                    record.Date = reader.GetString(2);
                    record.Temperature = reader.GetDouble(3);
                }
            }
            else
            {
                throw new Exception("Record not found");
            }

            return record;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public List<BodyMeasurement>? GrabRecordsByPersonId(int personId)
    {
        var bmList = new List<BodyMeasurement>();
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM BodyMeasurements
                              WHERE PersonId = @PersonId
                              """;
            cmd.Parameters.AddWithValue("@PersonId", personId);
            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BodyMeasurement record = new()
                    {
                        Id = reader.GetInt32(0),
                        PersonId = reader.GetInt32(1),
                        Date = reader.GetString(2),
                        Temperature = reader.GetDouble(3)
                    };
                    bmList.Add(record);
                }
            }
            else
            {
                throw new Exception("Record not found");
            }

            return bmList;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public List<BodyMeasurement>? GrabRecordsByDate(string date)
    {
        var bmList = new List<BodyMeasurement>();
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM BodyMeasurements
                              WHERE DateTime = @Datetime
                              """;
            cmd.Parameters.AddWithValue("@Datetime", date);
            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BodyMeasurement record = new()
                    {
                        Id = reader.GetInt32(0),
                        PersonId = reader.GetInt32(1),
                        Date = reader.GetString(2),
                        Temperature = reader.GetDouble(3)
                    };
                    bmList.Add(record);
                }
            }
            else
            {
                throw new Exception("Record not found");
            }

            return bmList;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public void UpdateRecord(BodyMeasurement newRecord, int oldId)
    {
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              UPDATE BodyMeasurements
                              SET Datetime = @Datetime,
                                  Temperature = @Temperature,
                              WHERE Id = @id
                              """;
            cmd.Parameters.AddWithValue("@Datetime", newRecord.Date);
            cmd.Parameters.AddWithValue("@Temperature", newRecord.Temperature);

            cmd.ExecuteNonQuery();
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void DeleteRecord(int id)
    {
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              DELETE FROM BodyMeasurements
                              WHERE Id = @id
                              """;
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}