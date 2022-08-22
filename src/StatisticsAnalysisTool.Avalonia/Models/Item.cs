﻿using StatisticsAnalysisTool.Avalonia.Common;
using StatisticsAnalysisTool.Avalonia.Enumerations;
using StatisticsAnalysisTool.Avalonia.Models.ItemSearch;
using StatisticsAnalysisTool.Avalonia.Models.ItemsJsonModel;
using StatisticsAnalysisTool.Avalonia.Settings;
using ShopCategory = StatisticsAnalysisTool.Avalonia.Enumerations.ShopCategory;

namespace StatisticsAnalysisTool.Avalonia.Models
{
    public class Item
    {
        //private BitmapImage _icon;
        public string LocalizationNameVariable { get; set; } = string.Empty;
        public string LocalizationDescriptionVariable { get; set; } = string.Empty;
        public LocalizedNames LocalizedNames { get; set; } = new();
        public int Index { get; set; }
        public string UniqueName { get; set; } = string.Empty;

        public string LocalizedNameAndEnglish => LanguageController.CurrentCultureInfo?.TextInfo.CultureName.ToUpper() == ApplicationSettings.DefaultLanguageCultureName
            ? $"{ItemController.LocalizedName(LocalizedNames, null, UniqueName)}{GetUniqueNameIfDebug()}"
            : $"{ItemController.LocalizedName(LocalizedNames, null, UniqueName)}" +
              $"\n{ItemController.LocalizedName(LocalizedNames, ApplicationSettings.DefaultLanguageCultureName, string.Empty)}{GetUniqueNameIfDebug()}";

        public string LocalizedName => ItemController.LocalizedName(LocalizedNames, null, UniqueName);

        public int Level => ItemController.GetItemLevel(UniqueName);
        public int Tier => ItemController.GetItemTier(this);

        public string TierLevelString
        {
            get
            {
                var tier = (Tier is <= 8 and >= 1) ? Tier.ToString() : "?";
                return $"T{tier}.{Level}";
            }
        }

        //[JsonIgnore]
        //public BitmapImage Icon => Dispatcher.UIThread.InvokeAsync(() => _icon ??= ImageController.GetItemImage(UniqueName));

        public ItemJsonObject? FullItemInformation { get; set; }
        public ShopCategory ShopCategory { get; set; }
        public ShopSubCategory ShopShopSubCategory1 { get; set; }
        public int AlertModeMinSellPriceIsUndercutPrice { get; set; }
        public bool IsAlertActive { get; set; }
        public bool IsFavorite { get; set; }
        private string GetUniqueNameIfDebug()
        {
#if DEBUG
            return $"\n{UniqueName}";
#else
            return string.Empty;
#endif
        }
    }
}