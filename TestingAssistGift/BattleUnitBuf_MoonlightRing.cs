using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestingAssistGift
{
    /// <summary>
    /// 状態「月光の輪」
    /// カスタマイズされた様々な効果を発揮する。
    /// </summary>
    public class BattleUnitBuf_MoonlightRing : BattleUnitBuf
    {
        /// <summary>付与数の上限値</summary>
        private const int MaxStack = 0;

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
            stack = MaxStack;
        }

        public override void OnAddBuf(int addedStack)
            => stack = MaxStack;

        public override StatBonus GetStatBonus()
            => effect.StatBonus;

        public override int MaxPlayPointAdder()
            => effect.PlayPointAdder;

        public override void OnRoundStart()
        {
            try
            {
                effect.RecoverTo(_owner);
                effect.AddBufsTo(_owner);
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }

        /// <summary>
        /// 指定した効果を適用します。
        /// </summary>
        /// <param name="newEffect"></param>
        public void Apply(EffectModel newEffect)
        {
            if (newEffect == null) { throw new ArgumentNullException(nameof(newEffect)); }

            EffectModel oldEffect = ChangeEffect(newEffect);
            UpdateHp(oldEffect, newEffect);
            UpdateBreak(oldEffect, newEffect);
            UpdatePlayPoint(oldEffect, newEffect);
            UpdateBufs(oldEffect, newEffect);
        }

        /// <summary>
        /// <see cref="effect"/> フィールドの値を指定した効果に変更し、状態アイコンの表示・非表示を切り替えます。
        /// </summary>
        /// <param name="newEffect"></param>
        /// <returns>変更前の効果。</returns>
        private EffectModel ChangeEffect(EffectModel newEffect)
        {
            EffectModel oldEffect = effect;
            effect = newEffect;

            PrivateAccess.SetField<BattleUnitBuf>(this, "_iconInit", false);
            GetBufIcon();

            return oldEffect;
        }

        private void UpdateHp(EffectModel oldEffect, EffectModel newEffect)
        {
            int adderDiff = newEffect.StatBonus.hpAdder - oldEffect.StatBonus.hpAdder;

            int newHp = Mathf.Clamp(
                Convert.ToInt32(_owner.hp) + adderDiff,
                _owner.Book.DeadLine + 1,
                _owner.MaxHp);

            _owner.SetHp(newHp);
        }

        private void UpdateBreak(EffectModel oldEffect, EffectModel newEffect)
        {
            _owner.breakDetail.breakGauge = _owner.breakDetail.GetDefaultBreakGauge();

            if (_owner.breakDetail.IsBreakLifeZero()) { return; }

            int adderDiff = newEffect.StatBonus.breakAdder - oldEffect.StatBonus.breakAdder;

            int newBreak = Mathf.Clamp(
                _owner.breakDetail.breakLife + adderDiff,
                1,
                _owner.breakDetail.breakGauge);

            _owner.breakDetail.breakLife = newBreak;
        }

        private void UpdatePlayPoint(EffectModel oldEffect, EffectModel newEffect)
        {
            int adderDiff = newEffect.PlayPointAdder - oldEffect.PlayPointAdder;

            int newPlayPoint = Mathf.Clamp(
                _owner.cardSlotDetail.PlayPoint + adderDiff,
                0,
                _owner.cardSlotDetail.GetMaxPlayPoint());

            _owner.cardSlotDetail.SetPlayPoint(newPlayPoint);
        }

        private void UpdateBufs(EffectModel oldEffect, EffectModel newEffect)
        {
            UpdateBuf(KeywordBuf.Strength, newEffect.StrengthStack - oldEffect.StrengthStack);
            UpdateBuf(KeywordBuf.Weak, newEffect.WeakStack - oldEffect.WeakStack);
            UpdateBuf(KeywordBuf.Endurance, newEffect.EnduranceStack - oldEffect.EnduranceStack);
            UpdateBuf(KeywordBuf.Disarm, newEffect.DisarmStack - oldEffect.DisarmStack);
            UpdateBuf(KeywordBuf.Quickness, newEffect.QuicknessStack - oldEffect.QuicknessStack);
            UpdateBuf(KeywordBuf.Binding, newEffect.BindingStack - oldEffect.BindingStack);
            UpdateBuf(KeywordBuf.Protection, newEffect.ProtectionStack - oldEffect.ProtectionStack);
            UpdateBuf(KeywordBuf.Vulnerable, newEffect.VulnerableStack - oldEffect.VulnerableStack);
            UpdateBuf(KeywordBuf.BreakProtection, newEffect.BreakProtectionStack - oldEffect.BreakProtectionStack);
            UpdateBuf(KeywordBuf.Burn, newEffect.BurnStack - oldEffect.BurnStack);
            UpdateBuf(KeywordBuf.Paralysis, newEffect.ParalysisStack - oldEffect.ParalysisStack);
            UpdateBuf(KeywordBuf.Bleeding, newEffect.BleedingStack - oldEffect.BleedingStack);
        }

        private void UpdateBuf(KeywordBuf bufType, int stackDiff)
        {
            if (stackDiff == 0) { return; }
            if (stackDiff > 0)
            {
                _owner.bufListDetail.AddKeywordBufThisRoundByEtc(bufType, stackDiff);
                return;
            }

            var buf = _owner.bufListDetail.GetActivatedBuf(bufType);
            if (buf == null) { return; }
            buf.stack += stackDiff;
            if (buf.stack > 0) { return; }
            _owner.bufListDetail.RemoveBuf(buf);
        }
    }
}
