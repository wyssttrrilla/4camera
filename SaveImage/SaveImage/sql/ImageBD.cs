using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;

namespace SaveImage.sql
{
    internal class ImageBD
    {
        SQLiteConnection db;
        public ImageBD(string dbPath)
        {
            db = new SQLiteConnection(dbPath);
            db.CreateTable<ImageS>();
        }

        public IEnumerable<ImageS> GetItems()
        {
            return db.Table<ImageS>().ToList();
        }

        public ImageS GetItem(int id)
        {
            return db.Get<ImageS>(id);
        }

        public int DeleteItem(int id)
        {
            return db.Delete<ImageS>(id);
        }

        public int SaveItem(ImageS item)
        {
            if (item.Id != 0)
            {
                db.Update(item);
                return item.Id;
            }
            else
            {
                return db.Insert(item);
            }
        }
    }
}
