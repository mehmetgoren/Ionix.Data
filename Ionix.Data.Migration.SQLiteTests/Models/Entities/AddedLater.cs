﻿namespace Ionix.Data.Migration.SQLiteTests.Models
{
    using Ionix.Data.Common;
    using Migration.Common;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [MigrationVersion(Migration102.VersionNo)]
    [Table("AddedLater")]
    [TableIndex("Name", Unique = true)]
    public class AddedLater
    {
        [DbSchema(IsKey = true, DatabaseGeneratedOption = StoreGeneratedPattern.Identity)]
        public int ControllerId { get; set; }

        public int? OpUserId { get; set; }

        [DbSchema(DefaultValue = "CURRENT_TIMESTAMP")]
        public DateTime? OpDate { get; set; }

        [DbSchema(MaxLength = 15)] public string OpIp { get; set; }

        [DbSchema(MaxLength = 50)] public string Name { get; set; }
    }
}