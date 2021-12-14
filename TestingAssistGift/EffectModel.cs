using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestingAssistGift
{
    /// <summary>
    /// 戦闘表象「月光の輪」によってキャラクターに適用させる効果を実装します。
    /// </summary>
    public class EffectModel
    {
        private const int MinHpAdder = 0;
        private const int MaxHpAdder = 500;
        private const int MinBreakGageAdder = 0;
        private const int MaxBreakGageAdder = 500;
        private const int MinPlayPointAdder = 0;
        private const int MaxPlayPointAdder = 5;
        private const int MinHpRecover = 0;
        private const int MaxHpRecover = 1000;
        private const int MinBreakRecover = 0;
        private const int MaxBreakRecover = 1000;
        private const int MinPlayPointRecover = 0;
        private const int MaxPlayPointRecover = 10;
        private const int MinBufStack = 0;
        private const int MaxBufStack = 50;

        /// <summary>効果なしを表します。</summary>
        public static readonly EffectModel None = new EffectModel("None");

        #region プロパティ

        /// <summary>
        /// 効果名を取得します。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 能力値ボーナスを取得します。
        /// </summary>
        public StatBonus StatBonus { get; }

        /// <summary>
        /// 光の最大値の増加量を取得します。
        /// </summary>
        public int PlayPointAdder { get; }

        /// <summary>
        /// 毎幕の体力の回復量を取得します。
        /// </summary>
        public int HpRecover { get; }

        /// <summary>
        /// 毎幕の混乱抵抗値の回復量を取得します。
        /// </summary>
        public int BreakRecover { get; }

        /// <summary>
        /// 毎幕の光の回復量を取得します。
        /// </summary>
        public int PlayPointRecover { get; }

        /// <summary>
        /// 毎幕のの付与数を取得します。
        /// </summary>
        public int StrengthStack { get; }

        /// <summary>
        /// 毎幕のの付与数を取得します。
        /// </summary>
        public int WeakStack { get; }

        /// <summary>
        /// 毎幕のの付与数を取得します。
        /// </summary>
        public int EnduranceStack { get; }

        /// <summary>
        /// 毎幕のの付与数を取得します。
        /// </summary>
        public int DisarmStack { get; }

        /// <summary>
        /// 毎幕のの付与数を取得します。
        /// </summary>
        public int QuicknessStack { get; }

        /// <summary>
        /// 毎幕のの付与数を取得します。
        /// </summary>
        public int BindingStack { get; }

        /// <summary>
        /// 毎幕のの付与数を取得します。
        /// </summary>
        public int ProtectionStack { get; }

        /// <summary>
        /// 毎幕のの付与数を取得します。
        /// </summary>
        public int VulnerableStack { get; }

        /// <summary>
        /// 毎幕のの付与数を取得します。
        /// </summary>
        public int BreakProtectionStack { get; }

        /// <summary>
        /// 毎幕のの付与数を取得します。
        /// </summary>
        public int BurnStack { get; }

        /// <summary>
        /// 毎幕のの付与数を取得します。
        /// </summary>
        public int ParalysisStack { get; }

        /// <summary>
        /// 毎幕のの付与数を取得します。
        /// </summary>
        public int BleedingStack { get; }

        #endregion

        /// <summary>
        /// <see cref="EffectModel"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hpAdder"></param>
        /// <param name="breakGageAdder"></param>
        /// <param name="playPointAdder"></param>
        /// <param name="hpRecover"></param>
        /// <param name="breakRecover"></param>
        /// <param name="playPointRecover"></param>
        /// <param name="strengthStack"></param>
        /// <param name="weakStack"></param>
        /// <param name="enduranceStack"></param>
        /// <param name="disarmStack"></param>
        /// <param name="quicknessStack"></param>
        /// <param name="bindingStack"></param>
        /// <param name="protectionStack"></param>
        /// <param name="vulnerableStack"></param>
        /// <param name="breakProtectionStack"></param>
        /// <param name="burnStack"></param>
        /// <param name="paralysisStack"></param>
        /// <param name="bleedingStack"></param>
        public EffectModel(
            string name,
            int hpAdder = 0,
            int breakGageAdder = 0,
            int playPointAdder = 0,
            int hpRecover = 0,
            int breakRecover = 0,
            int playPointRecover = 0,
            int strengthStack = 0,
            int weakStack = 0,
            int enduranceStack = 0,
            int disarmStack = 0,
            int quicknessStack = 0,
            int bindingStack = 0,
            int protectionStack = 0,
            int vulnerableStack = 0,
            int breakProtectionStack = 0,
            int burnStack = 0,
            int paralysisStack = 0,
            int bleedingStack = 0)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

            StatBonus = new StatBonus()
            {
                hpAdder = Mathf.Clamp(hpAdder, MinHpAdder, MaxHpAdder),
                breakGageAdder = Mathf.Clamp(breakGageAdder, MinBreakGageAdder, MaxBreakGageAdder),
            };

            PlayPointAdder = Mathf.Clamp(playPointAdder, MinPlayPointAdder, MaxPlayPointAdder);
            HpRecover = Mathf.Clamp(hpRecover, MinHpRecover, MaxHpRecover);
            BreakRecover = Mathf.Clamp(breakRecover, MinBreakRecover, MaxBreakRecover);
            PlayPointRecover = Mathf.Clamp(playPointRecover, MinPlayPointRecover, MaxPlayPointRecover);
            StrengthStack = Mathf.Clamp(strengthStack, MinBufStack, MaxBufStack);
            WeakStack = Mathf.Clamp(weakStack, MinBufStack, MaxBufStack);
            EnduranceStack = Mathf.Clamp(enduranceStack, MinBufStack, MaxBufStack);
            DisarmStack = Mathf.Clamp(disarmStack, MinBufStack, MaxBufStack);
            QuicknessStack = Mathf.Clamp(quicknessStack, MinBufStack, MaxBufStack);
            BindingStack = Mathf.Clamp(bindingStack, MinBufStack, MaxBufStack);
            ProtectionStack = Mathf.Clamp(protectionStack, MinBufStack, MaxBufStack);
            VulnerableStack = Mathf.Clamp(vulnerableStack, MinBufStack, MaxBufStack);
            BreakProtectionStack = Mathf.Clamp(breakProtectionStack, MinBufStack, MaxBufStack);
            BurnStack = Mathf.Clamp(burnStack, MinBufStack, MaxBufStack);
            ParalysisStack = Mathf.Clamp(paralysisStack, MinBufStack, MaxBufStack);
            BleedingStack = Mathf.Clamp(bleedingStack, MinBufStack, MaxBufStack);
        }

        /// <summary>
        /// 指定したキャラクターのステータスを設定されている値で回復します。
        /// </summary>
        /// <param name="owner"></param>
        public void RecoverTo(BattleUnitModel owner)
        {
            if (HpRecover > 0)
            {
                owner.RecoverHP(HpRecover);
            }

            if (BreakRecover > 0 && !owner.IsBreakLifeZero())
            {
                owner.breakDetail.RecoverBreak(BreakRecover);
            }

            if (PlayPointRecover > 0)
            {
                owner.cardSlotDetail.RecoverPlayPoint(PlayPointRecover);
            }
        }

        /// <summary>
        /// 指定したキャラクターに設定されている状態を付与します。
        /// </summary>
        /// <param name="owner"></param>
        public void AddBufsTo(BattleUnitModel owner)
        {
            AddBufTo(owner, KeywordBuf.Strength, StrengthStack);
            AddBufTo(owner, KeywordBuf.Weak, WeakStack);
            AddBufTo(owner, KeywordBuf.Endurance, EnduranceStack);
            AddBufTo(owner, KeywordBuf.Disarm, DisarmStack);
            AddBufTo(owner, KeywordBuf.Quickness, QuicknessStack);
            AddBufTo(owner, KeywordBuf.Binding, BindingStack);
            AddBufTo(owner, KeywordBuf.Protection, ProtectionStack);
            AddBufTo(owner, KeywordBuf.Vulnerable, VulnerableStack);
            AddBufTo(owner, KeywordBuf.BreakProtection, BreakProtectionStack);
            AddBufTo(owner, KeywordBuf.Burn, BurnStack);
            AddBufTo(owner, KeywordBuf.Paralysis, ParalysisStack);
            AddBufTo(owner, KeywordBuf.Bleeding, BleedingStack);
        }

        private void AddBufTo(BattleUnitModel owner, KeywordBuf buf, int stack)
        {
            if (stack <= 0) { return; }

            owner.bufListDetail.AddKeywordBufThisRoundByEtc(buf, stack);
        }
    }
}
