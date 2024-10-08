﻿using Microsoft.EntityFrameworkCore;

namespace Persistence.DbInitializer;

public class DbInitializer : IDbInitializer
{
    private readonly DataContext _db;
    public DbInitializer(DataContext db)
    {
        _db = db;
    }
    public void Initialize()
    {
        try
        {
            var createDB = _db.Database.EnsureCreated();

            if (createDB is false)
            {
                throw new Exception("Fail to create database");
            }

            //if (_db.Database.GetPendingMigrations().Any())
            //{
            //    _db.Database.Migrate();
            //}
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return;
    }
}

