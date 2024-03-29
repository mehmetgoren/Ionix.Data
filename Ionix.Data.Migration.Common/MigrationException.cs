﻿namespace Ionix.Data.Migration.Common
{
    using System;

    public class MigrationException : Exception
    {
        public MigrationException(string message)
            : base(message)
        {
        }

        public MigrationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}