using System;
using System.Collections.Generic;
using System.Linq;
using GameSave;
using HarmonyLib;

namespace TestingAssistGift
{
    [Harmony]
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
                var harmony = new Harmony("TestingAssistGift");
                harmony.PatchAll();
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }

        #endregion

        #region 戦闘表象の削除

        /// <summary>
        /// <see cref="UnitDataModel.LoadFromSaveData"/> メソッドが呼び出される前に実行されます。
        /// 指定司書以外がこの MOD で追加された戦闘表象を所有していれば削除します。
        /// </summary>
        /// <param name="__instance">メソッドを呼び出したインスタンス。</param>
        /// <param name="data"></param>
        [HarmonyPatch(typeof(UnitDataModel), "LoadFromSaveData")]
        [HarmonyPrefix]
        public static bool UnitDataModel_LoadFromSaveData_Prefix(UnitDataModel __instance, SaveData data)
        {
            try
            {
                if (__instance.isSephirah) { return true; }

                SaveData giftInventoryData = data.GetData(UnitDataModel.save_giftInventory);
                RemoveMoonlightRing(giftInventoryData.GetData(GiftInventory.save_equipList));
                RemoveMoonlightRing(giftInventoryData.GetData(GiftInventory.save_unequipList));
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }

            return true;
        }

        /// <summary>
        /// 指定した戦闘表象の装備リストまたは非装備リストから、この MOD で追加された戦闘表象を削除します。
        /// </summary>
        /// <param name="giftIdListSaveData"></param>
        private static void RemoveMoonlightRing(SaveData giftIdListSaveData)
        {
            List<SaveData> _list = PrivateAccess.GetField<List<SaveData>>(giftIdListSaveData, "_list");

            var removingSaveDataList = new List<SaveData>(_list.Where(s => s.GetIntSelf() == Resource.MoonlightRing));
            if (removingSaveDataList.Count <= 0) { return; }

            foreach (SaveData removingSaveData in removingSaveDataList)
            {
                _list.Remove(removingSaveData);
            }
        }

        #endregion

        #region 戦闘表象の入手

        /// <summary>
        /// <see cref="UnitDataModel.LoadFromSaveData"/> メソッドが呼び出された後に実行されます。
        /// 指定司書にこの MOD で追加された戦闘表象を追加します。
        /// </summary>
        /// <param name="__instance">メソッドを呼び出したインスタンス。</param>
        /// <param name="data"></param>
        [HarmonyPatch(typeof(UnitDataModel), "LoadFromSaveData")]
        [HarmonyPostfix]
        public static void UnitDataModel_LoadFromSaveData_Postfix(UnitDataModel __instance)
        {
            try
            {
                if (!__instance.isSephirah) { return; }

                GiftModel addedGift = __instance.giftInventory.AddGift(Resource.MoonlightRing);
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }

		#endregion
	}
}
