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
                              VALUES (NULL, @PersonId, @Datetime, @Sys, @Dia, @HRhythm, @Feeling, @Medication, @IsArrhythmia)
                              """;
            cmd.Parameters.AddWithValue("@PersonId", record.PersonId);
            cmd.Parameters.AddWithValue("@Datetime", record.Date);
            cmd.Parameters.AddWithValue("@Sys", record.Sys);
            cmd.Parameters.AddWithValue("@Dia", record.Dia);
            cmd.Parameters.AddWithValue("@HRhythm", record.HRhythm);
            cmd.Parameters.AddWithValue("@Feeling", record.Feeling);
            cmd.Parameters.AddWithValue("@Medication", record.Medication);
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
        HeartMeasurement? record = null;
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

                    record = new HeartMeasurement(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetInt32(3),
                        reader.GetInt32(4),
                        reader.GetInt32(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetBoolean(8)
                    );
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
        List<HeartMeasurement> hmList = [];
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
                hmList = [];
                while (reader.Read())
                {
                    HeartMeasurement record = new(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetInt32(3),
                        reader.GetInt32(4),
                        reader.GetInt32(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetBoolean(8)
                    );
                    hmList.Add(record);
                }
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
                    HeartMeasurement record = new(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetInt32(3),
                        reader.GetInt32(4),
                        reader.GetInt32(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetBoolean(8)
                    );
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
                                  Feeling = @Feeling,
                                  Medication = @Medication,
                                  IsArrhythmia = @IsArrhythmia
                              WHERE Id = @id
                              """;
            cmd.Parameters.AddWithValue("@Datetime", newRecord.Date);
            cmd.Parameters.AddWithValue("@Sys", newRecord.Sys);
            cmd.Parameters.AddWithValue("@Dia", newRecord.Dia);
            cmd.Parameters.AddWithValue("@HRhythm", newRecord.HRhythm);
            cmd.Parameters.AddWithValue("@Feeling", newRecord.Feeling);
            cmd.Parameters.AddWithValue("@Medication", newRecord.Medication);
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