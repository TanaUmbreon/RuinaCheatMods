using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingAssistGift
{
    /// <summary>
    /// 状態「月光の輪」
    /// カスタマイズされた様々な効果を発揮する。
    /// </summary>
    public class BattleUnitBuf_MoonlightRing : BattleUnitBuf
    {
        /// <summary>状態の固定付与数</summary>
        private const int FixedStackCount = 0;

        /// <summary>適用中の効果</summary>
        private EffectModel effect;

        public override BufPositiveType positiveType => BufPositiveType.None;
        public override bool Hide => effect == EffectModel.None;
        public override string bufActivatedText => Singleton<BattleEffectTextsXmlList>.Instance.GetEffectTextDesc(keywordId, effect.Name);
        protected override string keywordId => "MoonlightRing";
        protected override string keywordIconId => "MoonlightRingBuf";

        /// <summary>
        /// <see cref="BattleUnitBuf_MoonlightRing"/> の新しいインスタンスを生成します。
        /// </summary>
        public BattleUnitBuf_MoonlightRing()
        {
            effect = EffectModel.None;
            stack = FixedStackCount;
        }

        public override void OnAddBuf(int addedStack)
        {
            stack = FixedStackCount;
        }

        /// <summary>
        /// 指定した効果を適用します。
        /// </summary>
        /// <param name="newEffect"></param>
        public void Apply(EffectModel newEffect)
        {
            EffectModel oldEffect = effect;
            effect = newEffect ?? throw new ArgumentNullException(nameof(newEffect));
            UpdateBufIcon();

            UpdateHp();
            int hpDiff = newEffect.StatBonus.hpAdder - oldEffect.StatBonus.hpAdder;

            //_owner.SetHp(_owner.MaxHp);
            //_owner.breakDetail.breakGauge = _owner.breakDetail.GetDefaultBreakGauge();
            //_owner.breakDetail.breakLife = _owner.breakDetail.breakGauge;
            //_owner.cardSlotDetail.SetPlayPoint(_owner.cardSlotDetail.GetMaxPlayPoint());
        }

        /// <summary>
        /// 状態アイコンの表示を、現在適用されている効果を元に更新します。
        /// </summary>
        private void UpdateBufIcon()
        {
            PrivateAccess.SetField<BattleUnitBuf>(this, "_iconInit", false);
            GetBufIcon();
        }

        private void UpdateHp()
        {

        }

        public override StatBonus GetStatBonus()
            => effect.StatBonus;

        //public override int MaxPlayPointAdder()
        //{
        //    try
        //    {
        //        return effect.MaxPlayPointAdder;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Instance.ErrorOnExceptionThrown(ex);
        //        return base.MaxPlayPointAdder();
        //    }
        //}

        public override void OnRoundStart()
        {
            try
            {
                //effect.RecoverTo(_owner);
                //effect.AddBufsTo(_owner);
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }
    }
}
