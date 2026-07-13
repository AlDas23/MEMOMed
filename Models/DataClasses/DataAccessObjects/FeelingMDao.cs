using System;
using System.Collections.Generic;
using MEMOMed.Models.Interfaces;
using Microsoft.Data.Sqlite;

namespace MEMOMed.Models.DataClasses.DataAccessObjects;

public class FeelingMDao : IMeasurementDao<FeelingMeasurement> // TO BE REMOVED
{
    public void InsertRecord(FeelingMeasurement record)
    {
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              INSERT INTO FeelingMeasurements
                              VALUES (NULL, @PersonId, @Datetime, @Medication, @Feeling)
                              """;
            cmd.Parameters.AddWithValue("@PersonId", record.PersonId);
            cmd.Parameters.AddWithValue("@Datetime", record.Date);
            cmd.Parameters.AddWithValue("@Medication", record.Medication);
            cmd.Parameters.AddWithValue("@Feeling", record.Feeling);

            cmd.ExecuteNonQuery();
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public FeelingMeasurement? GrabRecordById(int id)
    {
        FeelingMeasurement? record = null;
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM FeelingMeasurements
                              WHERE Id = @id
                              """;
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())

                    record = new FeelingMeasurement(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
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

    public List<FeelingMeasurement>? GrabRecordsByPersonId(int personId)
    {
        List<FeelingMeasurement>? fmList = null;
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM FeelingMeasurements
                              WHERE PersonId = @PersonId
                              """;
            cmd.Parameters.AddWithValue("@PersonId", personId);
            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                fmList = [];
                while (reader.Read())
                {
                    FeelingMeasurement record = new(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                    );
                    fmList.Add(record);
                }
            }

            return fmList;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public List<FeelingMeasurement>? GrabRecordsByDate(string date)
    {
        var fmList = new List<FeelingMeasurement>();
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
                    FeelingMeasurement record = new(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                    );
                    fmList.Add(record);
                }
            }
            else
            {
                throw new Exception("Record not found");
            }

            return fmList;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public void UpdateRecord(FeelingMeasurement newRecord, int oldId)
    {
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              UPDATE FeelingMeasurements
                              SET Datetime = @Datetime,
                                  Medication = @Medication,
                                  Feeling = @Feeling
                              WHERE Id = @id
                              """;
            cmd.Parameters.AddWithValue("@Datetime", newRecord.Date);
            cmd.Parameters.AddWithValue("@Medication", newRecord.Medication);
            cmd.Parameters.AddWithValue("@Feeling", newRecord.Feeling);

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
                              DELETE FROM FeelingMeasurements
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