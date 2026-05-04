using System;
using System.Collections.Generic;
using MEMOMed.Models.Interfaces;
using Microsoft.Data.Sqlite;

namespace MEMOMed.Models.DataClasses.DataAccessObjects;

public class MedicineDao : IGenericDao<Medicine>
{
    public void InsertEntity(Medicine entity)
    {
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              INSERT INTO Medicine
                              VALUES (NULL, @Name, @Description, @ScheduleDays, @ScheduleDayTime)
                              """;
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@ScheduleDays", entity.GetDayScheduleString());
            cmd.Parameters.AddWithValue("@ScheduleDayTime", entity.GetTimeScheduleString());

            cmd.ExecuteNonQuery();
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public Medicine? GrabEntityById(int id)
    {
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM Medicine
                              WHERE Id = @id
                              """;
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            Medicine? entity = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    entity = new Medicine(
                        null,
                        reader.GetString(1),
                        reader.GetString(2),
                        Medicine.TimeScheduleFromString(reader.GetString(3)),
                        Medicine.DayScheduleFromString(reader.GetString(4)));
                }
            }
            else
            {
                throw new Exception("Record not found");
            }

            return entity;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public List<Medicine>? GetAllEntities()
    {
        List<Medicine> entityList = [];
        using var conn = new SqliteConnection(Constants.DbConnectionString);
        conn.Open();
        try
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM Medicine
                              """;
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                while (reader.Read())
                {
                    var medicine = new Medicine(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        Medicine.TimeScheduleFromString(reader.GetString(3)),
                        Medicine.DayScheduleFromString(reader.GetString(4)));
                    entityList.Add(medicine);
                }
            }

            return entityList;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public List<Medicine>? GrabAllEntities()
    {
        List<Medicine>? entityList = null;
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM Medicine
                              """;
            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                entityList = [];
                while (reader.Read())
                {
                    var entity = new Medicine(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        Medicine.TimeScheduleFromString(reader.GetString(3)),
                        Medicine.DayScheduleFromString(reader.GetString(4))
                    );
                    entityList.Add(entity);
                }
            }

            return entityList;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public void UpdateEntity(Medicine newEntity, int oldId)
    {
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              UPDATE Medicine
                              SET Name = @Name,
                                  Description = @Description,
                                  DaySchedule = @DaySchedule,
                                  TimeSchedule = @TimeSchedule
                              WHERE Id = @id
                              """;
            cmd.Parameters.AddWithValue("@Name", newEntity.Name);
            cmd.Parameters.AddWithValue("@Description", newEntity.Description);
            cmd.Parameters.AddWithValue("@DaySchedule", newEntity.GetDayScheduleString());
            cmd.Parameters.AddWithValue("@TimeSchedule", newEntity.GetTimeScheduleString());
            cmd.Parameters.AddWithValue("@id", oldId);

            cmd.ExecuteNonQuery();
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void DeleteEntity(int id)
    {
        try
        {
            using var conn = new SqliteConnection(Constants.DbConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              DELETE FROM Medicine
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