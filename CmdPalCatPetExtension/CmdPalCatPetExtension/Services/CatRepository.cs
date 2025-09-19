using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using CmdPalCatPetExtension.Models;

namespace CmdPalCatPetExtension.Services
{
    public enum CatChangeType
    {
        Created,
        Updated,
        Deleted
    }

    public static class CatRepository
    {
        private static readonly string Folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CmdPalCatPetExtension");
        private static readonly string FilePath = Path.Combine(Folder, "virtualcat.json");

        // Raised whenever a cat is saved/changed so listeners can refresh UI
        public static event Action<CatChangeType>? CatChanged;

        public static void Save(VirtualCat cat)
        {
            Directory.CreateDirectory(Folder);
            bool wasNew = !File.Exists(FilePath);
            // update timestamp to reflect when we persist
            cat.LastUpdatedUtc = DateTime.UtcNow;
            var json = JsonSerializer.Serialize(cat, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
            // Notify listeners that a cat was saved/changed
            if (wasNew)
            {
                CatChanged?.Invoke(CatChangeType.Created);
            }
            else
            {
                CatChanged?.Invoke(CatChangeType.Updated);
            }
        }

        public static bool Delete()
        {
            if (!File.Exists(FilePath))
            {
                return false;
            }

            try
            {
                File.Delete(FilePath);
                CatChanged?.Invoke(CatChangeType.Deleted);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static VirtualCat? Load()
        {
            if (!File.Exists(FilePath))
            {
                return null;
            }

            try
            {
                var json = File.ReadAllText(FilePath);
                var cat = JsonSerializer.Deserialize<VirtualCat>(json);
                if (cat != null)
                {
                    // Ensure collections are initialized for older saved files
                    if (cat.Achievements == null) cat.Achievements = new HashSet<string>();
                    // initialize claimed achievements if missing
                    var claimedProp = cat.GetType().GetProperty("ClaimedAchievements");
                    if (claimedProp != null)
                    {
                        var val = claimedProp.GetValue(cat) as HashSet<string>;
                        if (val == null) cat.GetType().GetProperty("ClaimedAchievements")?.SetValue(cat, new HashSet<string>());
                    }
                }

                return cat;
            }
            catch
            {
                return null;
            }
        }
    }
}
