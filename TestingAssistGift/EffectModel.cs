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
        /// <summary>効果なしを表します。</summary>
        public static readonly EffectModel None = new EffectModel("None");

        private const int MinHpAdder = 0;
        private const int MaxHpAdder = 500;
        private const int MinBreakGageAdder = 0;
        private const int MaxBreakGageAdder = 500;
        private const int MinPlayPointAdder = 0;
        private const int MaxPlayPointAdder = 5;

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
        /// <see cref="EffectModel"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hpAdder"></param>
        /// <param name="breakGageAdder"></param>
        /// <param name="playPointAdder"></param>
        public EffectModel(string name, int hpAdder = 0, int breakGageAdder = 0, int playPointAdder = 0)
        {
            if (name == null) { throw new ArgumentNullException(nameof(name)); }

            Name = name;

            StatBonus = new StatBonus()
            {
                hpAdder = Mathf.Clamp(hpAdder, MinHpAdder, MaxHpAdder),
                breakGageAdder = Mathf.Clamp(breakGageAdder, MinBreakGageAdder, MaxBreakGageAdder),
            };

            PlayPointAdder = Mathf.Clamp(playPointAdder, MinPlayPointAdder, MaxPlayPointAdder);
        }
    }
}
