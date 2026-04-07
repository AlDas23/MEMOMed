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
            using var conn = new SqliteConnection(Constants.DbPath);
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
        var entity = new Medicine();
        try
        {
            using var conn = new SqliteConnection(Constants.DbPath);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM Medicine
                              WHERE Id = @id
                              """;
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    entity.Id = reader.GetInt32(0);
                    entity.Name = reader.GetString(1);
                    entity.Description = reader.GetString(2);
                    entity.SetDayScheduleFromString(reader.GetString(3));
                    entity.SetTimeScheduleFromString(reader.GetString(4));
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

    public List<Medicine>? GrabAllEntities()
    {
        var entityList = new List<Medicine>();
        try
        {
            using var conn = new SqliteConnection(Constants.DbPath);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM Medicine
                              """;
            using var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var entity = new Medicine()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                    };
                    entity.SetDayScheduleFromString(reader.GetString(3));
                    entity.SetTimeScheduleFromString(reader.GetString(4));
                    entityList.Add(entity);
                }
            }
            else
            {
                throw new Exception("Record not found");
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
            using var conn = new SqliteConnection(Constants.DbPath);
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
            using var conn = new SqliteConnection(Constants.DbPath);
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