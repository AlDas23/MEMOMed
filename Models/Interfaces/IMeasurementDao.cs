using System;
using System.Collections.Generic;

namespace MEMOMed.Models.Interfaces;

public interface IMeasurementDao
{
    // Add
    void InsertRecord(List<string> record);
    // Read
    List<string> GrabRecordById(int id);
    List<List<string>> GrabRecordsByPersonId(int personId);
    List<List<string>> GrabRecordsByDate(DateTime date);
    // Update
    void UpdateRecord(List<string> newRecord, int oldId);
    // Delete
    void DeleteRecord(int id);
}