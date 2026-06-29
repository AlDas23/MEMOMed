using System.Collections.Generic;

namespace MEMOMed.Models.Interfaces;

public interface IMeasurementDao<T>
{
    // Add
    void InsertRecord(T record);
    // Read
    T? GrabRecordById(int id);
    List<T>? GrabRecordsByPersonId(int personId);
    List<T>? GrabRecordsByDate(string date);
    // Update
    void UpdateRecord(T newRecord, int oldId);
    // Delete
    void DeleteRecord(int id);
}