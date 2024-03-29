﻿namespace Ionix.Data.SQLite
{
    using System;
    using Common;

    internal static class GlobalInternal
    {
        internal const char Prefix = '@';

        internal static readonly string BeginStatement = "BEGIN;" + Environment.NewLine;
        internal static readonly string EndStatement = Environment.NewLine + "END;";
    }

    internal sealed class ValueSetter : DbValueSetter
    {
        internal static readonly ValueSetter Instance = new ValueSetter();

        private ValueSetter()
        {
        }

        public override char Prefix => GlobalInternal.Prefix;
    }
}