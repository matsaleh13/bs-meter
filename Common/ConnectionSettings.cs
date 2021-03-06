﻿using Formo;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Common
{
    /// <summary>
    /// Responsible for loading connection strings from the config file and injecting
    /// any environment variables needed.
    /// </summary>
    public class ConnectionSettings
    {
        public ConnectionSettings(string envFile)
        {
            // HACK
            // Load the Redis instance host address into the environment with this.
            // Necessary for now because the docker-machine may not always have the same IP.

            EnvironmentFile.Load(envFile);
            var env = Environment.GetEnvironmentVariables();
        }

        string GetConnectionString(string name)
        {
            // TODO: handle errors
            var conn = ConfigurationManager.ConnectionStrings[name];
            return Environment.ExpandEnvironmentVariables(conn.ConnectionString);
        }

        readonly IDictionary<string, string> _connectionStrings = new Dictionary<string, string>();
        public string this[string name]
        {
            get
            {
                string connString;
                lock(_connectionStrings)
                if (!_connectionStrings.TryGetValue(name, out connString))
                {
                    connString = GetConnectionString(name);
                    if (!string.IsNullOrEmpty(connString))
                    {
                        _connectionStrings[name] = connString;
                    }
                }

                return connString;
            }
        }

    }
}
