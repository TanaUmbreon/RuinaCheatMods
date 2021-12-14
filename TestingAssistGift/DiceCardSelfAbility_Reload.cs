using System;

namespace TestingAssistGift
{
    /// <summary>
    /// バトルページ効果「リロード」
    /// 「月光の輪」の効果設定を再ロードして、全てのキャラクターに効果を適用しなおす。
    /// </summary>
    public class DiceCardSelfAbility_reload : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            try
            {
                var model = new MoonlightRingModel();
                model.ApplyEffectAll();
                model.AddReloadCard(unit);
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }

        #region ターゲット可能なキャラクターの設定

        // リロード可能な状況を増やす為、自分自身含めて全てのキャラクターを選択可能にしておく。

        public override bool IsTargetableAllUnit() => true;

        public override bool IsTargetableSelf() => true;

        #endregion
    }
}
