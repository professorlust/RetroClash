using RetroGames.Files.CsvReader;
using RetroRoyale.Files.CsvHelpers;

namespace RetroRoyale.Files.Logic
{
    public class Locales : Data
    {
        public Locales(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string IconSWF { get; set; }

        public string IconExportName { get; set; }

        public bool HasEvenSpaceCharacters { get; set; }

        public string UsedSystemFont { get; set; }
    }
}