using System;
using System.IO;

namespace MEMOMed;

public static class Constants
{
    public static readonly string DbPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MEMOMed",
            "MEMOMed.db");

    public static readonly string DbConnectionString = $"Data Source={DbPath}";
    public const int CurrentDbVersion = 2;

    public static readonly string DbSchema = $"""
                                              CREATE TABLE IF NOT EXISTS System(
                                                Version INTEGER NOT NULL  
                                              );                         
                                              INSERT INTO SYSTEM VALUES ({CurrentDbVersion});
                                              CREATE TABLE IF NOT EXISTS HeartMeasurements (
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                  PersonId INTEGER,
                                                  DateTime TEXT NOT NULL,
                                                  Sys INTEGER NOT NULL,
                                                  Dia INTEGER NOT NULL,
                                                  HRhythm INTEGER NOT NULL,
                                                  Feeling TEXT, 
                                                  Medication TEXT,
                                                  IsArrhythmia BOOLEAN NOT NULL,
                                                  FOREIGN KEY (PersonId) REFERENCES Person (Id)
                                              );
                                              CREATE TABLE IF NOT EXISTS BodyMeasurements (
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                  PersonId INTEGER,
                                                  DateTime TEXT NOT NULL,
                                                  Temperature REAL NOT NULL,
                                                  FOREIGN KEY (PersonId) REFERENCES Person (Id)
                                              ); 
                                              CREATE TABLE IF NOT EXISTS Medicine (
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                  Name TEXT NOT NULL UNIQUE,
                                                  Description TEXT NOT NULL,
                                                  ScheduleDays TEXT NOT NULL,
                                                  ScheduleDayTime TEXT NOT NULL
                                              );
                                              CREATE TABLE IF NOT EXISTS Person (
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                  FirstName TEXT NOT NULL,
                                                  LastName TEXT NOT NULL
                                              );
                                              CREATE TABLE IF NOT EXISTS PersonMedicine (
                                                  PersonId INTEGER NOT NULL,
                                                  MedicineId INTEGER NOT NULL,
                                                  FOREIGN KEY (PersonId) REFERENCES Person (Id)
                                                  ON DELETE CASCADE,
                                                  FOREIGN KEY (MedicineId) REFERENCES Medicine (Id)
                                                  ON DELETE CASCADE
                                              );
                                              """;

    public static int? SelectedPersonId;
}