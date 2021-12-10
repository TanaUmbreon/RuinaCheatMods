using System;

namespace TestingAssistGift
{
    /// <summary>
    /// 戦闘表象「月光の輪」のパッシブ。
    /// 全てのキャラクターに対してカスタマイズされた様々な効果を発揮する。
    /// </summary>
    public class GiftPassiveAbility_197000 : PassiveAbilityBase
    {
        public override void OnCreated()
        {
            try
            {
                // 指定司書以外が装着していた場合、効果を発揮しない
                if (!owner.UnitData.unitData.isSephirah)
                {
                    Log.Instance.WarningWithCaller($"指定司書以外が装着しているため効果は発揮しません。 (OwnerSephirah: {owner.UnitData.unitData.OwnerSephirah}, name: '{owner.UnitData.unitData.name}')");
                    return;
                }

                var model = new MoonlightRingModel();
                model.ApplyEffectAll();
                model.AddReloadCardAllPlayers();
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }
    }
}
