using System.IO;
using System.Reflection;

namespace TestingAssistGift
{
    /// <summary>
    /// リソース情報を格納します。
    /// </summary>
    public static class Resource
    {
        #region パス

        /// <summary>この MOD のルート フォルダーのパス (DLL が格納されているフォルダー)</summary>
        private static readonly string RootDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>ローカライズ フォルダーのパス</summary>
        private static readonly string LocalizeDirectoryPath = Path.Combine(RootDirectoryPath, "Localize");

        /// <summary>MOD 設定ファイルのパス</summary>
        public static readonly string JsonSettingsFilePath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "ModSettings.json");

        /// <summary>
        /// 現在使用している言語の効果テキスト フォルダーのパスを取得します。
        /// </summary>
        public static string EffectTextsDirectoryPath => Path.Combine(
            RootDirectoryPath,
            GlobalGameManager.Instance.CurrentOption.language,
            "EffectTexts");

        #endregion

        #region 戦闘表象

        /// <summary>戦闘表象「月光の輪」の ID</summary>
        public static readonly int MoonlightRing = 197000;
        /// <summary>戦闘表象「月光の輪」の表示名</summary>
        public static readonly string MoonlightRingName = "月光の輪";


        #endregion

        #region バトル ページ

        public static readonly string PackageId = "TestingAssistGift";

        /// <summary>バトル ページ「設定リロード」の LOR ID</summary>
        public static readonly LorId ReloadCardId = new LorId("", 197000);
        /// <summary>バトル ページ「設定リロード」の表示名</summary>
        public static readonly string ReloadCardName = "設定リロード";

        #endregion
    }
}
