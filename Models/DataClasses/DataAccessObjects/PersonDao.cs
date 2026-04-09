using System;
using System.Collections.Generic;
using MEMOMed.Models.Interfaces;
using Microsoft.Data.Sqlite;

namespace MEMOMed.Models.DataClasses.DataAccessObjects;

public class PersonDao : IGenericDao<Person>
{
    public void InsertEntity(Person entity)
    {
        try
        {
            using var conn = new SqliteConnection(Constants.DbPath);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              INSERT INTO Person
                              VALUES (NULL, @FirstName, @LastName)
                              """;
            cmd.Parameters.AddWithValue("@FirstName", entity.FirstName);
            cmd.Parameters.AddWithValue("@LastName", entity.LastName);

            cmd.ExecuteNonQuery();
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public Person? GrabEntityById(int id)
    {
        Person entity = new Person();
        try
        {
            using var conn = new SqliteConnection(Constants.DbPath);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              SELECT * 
                              FROM Person
                              WHERE Id = @id
                              """;
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                while (reader.Read())
                {
                    entity.Id = reader.GetInt32(0);
                    entity.FirstName = reader.GetString(1);
                    entity.LastName = reader.GetString(2);
                }
            }

            using var cmd2 = conn.CreateCommand();
            cmd2.CommandText = """
                               SELECT MedicineId
                               FROM PersonMedicine
                               WHERE PersonId = @id
                               """;
            cmd2.Parameters.AddWithValue("@id", id);
            using var reader2 = cmd2.ExecuteReader();
            if (!reader2.Read()) return entity;
            MedicineDao medicineDao = new MedicineDao();
            while (reader2.Read())
            {
                var medicine = medicineDao.GrabEntityById(reader2.GetInt32(0));
                if (medicine == null)
                {
                    continue;
                }

                entity.MedicineList.Add(medicine);
            }

            return entity;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public void UpdateEntity(Person newEntity, int oldId)
    {
        try
        {
            using var conn = new SqliteConnection(Constants.DbPath);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              UPDATE Person
                              SET FirstName = @FirstName, LastName = @LastName
                              WHERE Id = @id
                              """;
            cmd.Parameters.AddWithValue("@FirstName", newEntity.FirstName);
            cmd.Parameters.AddWithValue("@LastName", newEntity.LastName);
            cmd.Parameters.AddWithValue("@id", oldId);
            cmd.ExecuteNonQuery();
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void UpdateEntityMedicineList(int personId, List<int> medicineIds)
    {
        using var conn = new SqliteConnection(Constants.DbPath);
        conn.Open();
        using var tran = conn.BeginTransaction();
        try
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              DELETE FROM PersonMedicine
                              WHERE PersonId = @personId
                              """;
            cmd.Parameters.AddWithValue("@personId", personId);
            cmd.ExecuteNonQuery();

            using var cmd2 = conn.CreateCommand();
            cmd2.CommandText = """
                               INSERT INTO PersonMedicine
                               VALUES (@PersonId, @MedicineId)
                               """;
            var personParam = cmd2.Parameters.AddWithValue("@PersonId", System.Data.DbType.Int32);
            var medicineParam = cmd2.Parameters.AddWithValue("@MedicineId", System.Data.DbType.Int32);
            
            personParam.Value = personId;
            foreach (var medicineId in medicineIds)
            {
                medicineParam.Value = medicineId;
                cmd2.ExecuteNonQuery();
            }
            
            tran.Commit();
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
            tran.Rollback();
        }
    }

    public void DeleteEntity(int id)
    {
        using var conn = new SqliteConnection(Constants.DbPath);
        conn.Open();
        try
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = """
                              DELETE FROM Person
                              WHERE PersonId = @id
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