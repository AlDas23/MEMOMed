using System;
using System.Collections.Generic;
using MEMOMed.Models.Interfaces;
using Microsoft.Data.Sqlite;

namespace MEMOMed.Models.DataClasses.DataAccessObjects;

public class HeartMDao : IMeasurementDao
{
    private SqliteConnection _conn = new("DataSource = ./DB/MEMOMed.db");

    public void InsertRecord(List<string> record)
    {
        throw new NotImplementedException();
    }

    public List<string> GrabRecordById(int id)
    {
        throw new NotImplementedException();
    }

    public List<List<string>> GrabRecordsByPersonId(int personId)
    {
        throw new NotImplementedException();
    }

    public List<List<string>> GrabRecordsByDate(DateTime date)
    {
        throw new NotImplementedException();
    }

    public void UpdateRecord(List<string> newRecord, int oldId)
    {
        throw new NotImplementedException();
    }

    public void DeleteRecord(int id)
    {
        throw new NotImplementedException();
    }
}