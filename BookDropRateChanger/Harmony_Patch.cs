using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using Newtonsoft.Json;
using UnityEngine;

namespace BookDropRateChanger
{
    [Harmony()]
    public class Harmony_Patch
    {
        #region スタートアップ

        /// <summary>
        /// <see cref="Harmony_Patch"/> の新しいインスタンスを生成します。
        /// </summary>
        public Harmony_Patch()
        {
            try
            {
                var harmony = new Harmony("BookDropRateChanger");
                harmony.PatchAll();
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }

        #endregion

        #region 本のドロップ数の変更

        /// <summary>設定ファイルのパス</summary>
        private static readonly string SettingsFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ModSettings.json");

        /// <summary>変更倍率の最小値</summary>
        private const float MinRate = 0.0f;
        /// <summary>変更倍率の最大値</summary>
        private const float MaxRate = 10.0f;
        /// <summary>設定ファイルから読み込みできなかった時に使用する規定の変更倍率</summary>
        private const float DefaultRate = 2.0f;
        /// <summary>倍率変更後に適応する最低ドロップ数</summary>
        private const float MinResult = 1.0f;

        /// <summary>
        /// <see cref="DropTable.CalculateDropProb(float, int)"/> メソッドが呼び出された後に実行されます。
        /// ドロップする本の数を設定ファイルの変更倍率に従って変更します。
        /// </summary>
        /// <param name="__instance">メソッドを呼び出したインスタンス。</param>
        /// <param name="__result">メソッドの戻り値。ドロップする本の数。</param>
        /// <param name="originProb"></param>
        /// <param name="emotionLevel"></param>
        [HarmonyPatch(typeof(DropTable), "CalculateDropProb")]
        [HarmonyPostfix]
        public static void DropTable_CalculateDropProb_Postfix(DropTable __instance, ref float __result, float originProb, int emotionLevel)
        {
            try
            {
                float rate = LoadDropRate();

                __result *= rate;
                __result = Mathf.Max(__result, MinResult);
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }

        /// <summary>
        /// ドロップ数の変更倍率を設定ファイルから読み込みます。
        /// </summary>
        /// <returns></returns>
        private static float LoadDropRate()
        {
            try
            {
                string json = File.ReadAllText(SettingsFilePath);
                var settings = JsonConvert.DeserializeObject<BookDropRateChangerSettings>(json);
                
                return Mathf.Clamp(settings.DropRate, MinRate, MaxRate);
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
                return DefaultRate;
            }
        }

        #endregion
    }
}
