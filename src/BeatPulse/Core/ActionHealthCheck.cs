﻿using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace BeatPulse.Core
{
    public class ActionHealthCheck
        : IBeatPulseHealthCheck
    {
        private readonly Func<HttpContext, (string, bool)> _check;
        private readonly string _name;
        private readonly string _defaultPath;

        public string HealthCheckName => _name;

        public string HealthCheckDefaultPath => _defaultPath;

        public ActionHealthCheck(string name, string defaultPath,Func<HttpContext,(string, bool)> check)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _defaultPath = defaultPath ?? throw new ArgumentNullException(nameof(_defaultPath));
            _check = check ?? throw new ArgumentNullException(nameof(check));
        }

        public Task<(string, bool)> IsHealthy(HttpContext context,bool isDevelopment)
        {
            return Task.FromResult(_check(context));
        }
    }
}
