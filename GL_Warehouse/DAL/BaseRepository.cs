using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DAL
{
    public abstract class BaseRepository
    {
        protected string _connectionString;

        public BaseRepository()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("connectionstrings.json");
            var config = builder.Build();
            _connectionString = config.GetConnectionString("defaultConnection");
        }
    }
}
