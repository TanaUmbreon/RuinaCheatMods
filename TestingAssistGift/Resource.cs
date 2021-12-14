using System.IO;
using System.Reflection;

namespace TestingAssistGift
{
    /// <summary>
    /// リソース情報を格納します。
    /// </summary>
    public static class Resource
    {
        /// <summary>MOD 設定ファイルのパス</summary>
        public static readonly string JsonSettingsFilePath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "ModSettings.json");

        #region 戦闘表象

        /// <summary>戦闘表象「月光の輪」の ID</summary>
        public static readonly int MoonlightRing = 197000;
        /// <summary>戦闘表象「月光の輪」の表示名</summary>
        public static readonly string MoonlightRingName = "月光の輪";


        #endregion

        #region バトル ページ

        /// <summary>バトル ページ「設定リロード」の LOR ID</summary>
        public static readonly LorId ReloadCardId = new LorId("", 197000);
        /// <summary>バトル ページ「設定リロード」の表示名</summary>
        public static readonly string ReloadCardName = "設定リロード";

        #endregion
    }
}
