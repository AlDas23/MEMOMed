using System;
using System.Collections.Generic;
using MEMOMed.Models.Interfaces;
using Microsoft.Data.Sqlite;

namespace MEMOMed.Models.DataClasses.DataAccessObjects;

public class HeartMDao : IMeasurementDao<HeartMeasurement>
{
    public void InsertRecord(HeartMeasurement record)
    {
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              INSERT INTO HeartMeasurements
                              VALUES (NULL, @PersonId, @Datetime, @Sys, @Dia, @HRhythm, @IsArrhythmia)
                              """;
            cmd.Parameters.AddWithValue("@PersonId", record.PersonId);
            cmd.Parameters.AddWithValue("@Datetime", record.Datetime);
            cmd.Parameters.AddWithValue("@Sys", record.Sys);
            cmd.Parameters.AddWithValue("@Dia", record.Dia);
            cmd.Parameters.AddWithValue("@HRhythm", record.HRhythm);
            cmd.Parameters.AddWithValue("@IsArrhythmia", record.IsArrhythmia);

            cmd.ExecuteNonQuery();
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public HeartMeasurement? GrabRecordById(int id)
    {
        HeartMeasurement record = new();
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM HeartMeasurements
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
                    record.Datetime = reader.GetString(2);
                    record.Sys = reader.GetInt32(3);
                    record.Dia = reader.GetInt32(4);
                    record.HRhythm = reader.GetInt32(5);
                    record.IsArrhythmia = reader.GetInt32(6);
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

    public List<HeartMeasurement>? GrabRecordsByPersonId(int personId)
    {
        var hmList = new List<HeartMeasurement>();
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM HeartMeasurements
                              WHERE PersonId = @PersonId
                              """;
            cmd.Parameters.AddWithValue("@PersonId", personId);
            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    HeartMeasurement record = new()
                    {
                        Id = reader.GetInt32(0),
                        PersonId = reader.GetInt32(1),
                        Datetime = reader.GetString(2),
                        Sys = reader.GetInt32(3),
                        Dia = reader.GetInt32(4),
                        HRhythm = reader.GetInt32(5),
                        IsArrhythmia = reader.GetInt32(6)
                    };
                    hmList.Add(record);
                }
            }
            else
            {
                throw new Exception("Record not found");
            }

            return hmList;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public List<HeartMeasurement>? GrabRecordsByDate(string date)
    {
        var hmList = new List<HeartMeasurement>();
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM HeartMeasurements
                              WHERE DateTime = @Datetime
                              """;
            cmd.Parameters.AddWithValue("@Datetime", date);
            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    HeartMeasurement record = new()
                    {
                        Id = reader.GetInt32(0),
                        PersonId = reader.GetInt32(1),
                        Datetime = reader.GetString(2),
                        Sys = reader.GetInt32(3),
                        Dia = reader.GetInt32(4),
                        HRhythm = reader.GetInt32(5),
                        IsArrhythmia = reader.GetInt32(6)
                    };
                    hmList.Add(record);
                }
            }
            else
            {
                throw new Exception("Record not found");
            }

            return hmList;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public void UpdateRecord(HeartMeasurement newRecord, int oldId)
    {
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              UPDATE HeartMeasurements
                              SET Datetime = @Datetime,
                                  Sys = @Sys,
                                  Dia = @Dia,
                                  HRhythm = @HRhythm,
                                  IsArrhythmia = @IsArrhythmia
                              WHERE Id = @id
                              """;
            cmd.Parameters.AddWithValue("@Datetime", newRecord.Datetime);
            cmd.Parameters.AddWithValue("@Sys", newRecord.Sys);
            cmd.Parameters.AddWithValue("@Dia", newRecord.Dia);
            cmd.Parameters.AddWithValue("@HRhythm", newRecord.HRhythm);
            cmd.Parameters.AddWithValue("@IsArrhythmia", newRecord.IsArrhythmia);
            cmd.Parameters.AddWithValue("@id", oldId);
            
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
                              DELETE FROM HeartMeasurements
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