using System;
using System.Data.SQLite;
using System.IO;

namespace MBTilesToTMS.API.Provider
{
    public class MbTileProvider
    {
        private readonly string _connectionString;

        public MbTileProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MemoryStream GetTile(int level, int col, int row)
        {
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            //Image image=null;
            var stream = new MemoryStream();
            using (var command = new SQLiteCommand(connection))
            {
                int tmsCol, tmsRow;
                ConvertGoogleTileToTMSTile(level, row, col, out tmsRow, out tmsCol);
                command.CommandText = "SELECT [tile_data] FROM [tiles] WHERE zoom_level = @zoom AND tile_column = @col AND tile_row = @row";
                command.Parameters.Add(new SQLiteParameter("zoom", level));
                command.Parameters.Add(new SQLiteParameter("col", tmsCol));
                command.Parameters.Add(new SQLiteParameter("row", tmsRow));
                var tileObj = command.ExecuteScalar();
                if (tileObj != null)
                {
                    stream = new MemoryStream((byte[])tileObj);
                    //var bitmap = new Bitmap(stream);
                    //image = bitmap;
                }
            }
            connection.Close();
            connection.Dispose();
            return  stream;
        }

        public static void ConvertGoogleTileToTMSTile(int level, int row, int col, out int outRow, out int outCol)
        {
            outCol = col;
            outRow = ((int)(Math.Pow(2.0, (double)level) - 1.0)) - row;
        }

    }
}