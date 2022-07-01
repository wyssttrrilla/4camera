using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;

namespace SaveImage.sql
{
    [Table("Project")]
    internal class ImageS
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        [Unique]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Puth { get; set; }
    }
}
