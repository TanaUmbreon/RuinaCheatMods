using UnityEngine;

namespace TestingAssistGift.DataAccess.JsonEntites
{
    /// <summary>
    /// 毎幕付与する状態とその付与数を格納します。
    /// </summary>
    public class AddingBufsOnRoundStartObject
    {
        private const int MinValue = 0;
        private const int MaxValue = 50;
        private const int DefaultValue = 0;

        /// <summary>
        /// 毎幕付与するパワーの数を取得または設定します。
        /// </summary>
        public int Strength { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する虚弱の数を取得または設定します。
        /// </summary>
        public int Weak { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する忍耐の数を取得または設定します。
        /// </summary>
        public int Endurance { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する武装解除の数を取得または設定します。
        /// </summary>
        public int Disarm { get; set; } = 0;

        /// <summary>
        /// 毎幕付与するクイックの数を取得または設定します。
        /// </summary>
        public int Quickness { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する束縛の数を取得または設定します。
        /// </summary>
        public int Binding { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する保護の数を取得または設定します。
        /// </summary>
        public int Protection { get; set; } = DefaultValue;

        /// <summary>
        /// 毎幕付与する脆弱の数を取得または設定します。
        /// </summary>
        public int Vulnerable { get; set; } = DefaultValue;

        /// <summary>
        /// 毎幕付与する混乱保護の数を取得または設定します。
        /// </summary>
        public int BreakProtection { get; set; } = DefaultValue;

        /// <summary>
        /// 毎幕付与する火傷の数を取得または設定します。
        /// </summary>
        public int Burn { get; set; } = DefaultValue;

        /// <summary>
        /// 毎幕付与する麻痺の数を取得または設定します。
        /// </summary>
        public int Paralysis { get; set; } = DefaultValue;

        /// <summary>
        /// 毎幕付与する出血の数を取得または設定します。
        /// </summary>
        public int Bleeding { get; set; } = DefaultValue;

        /// <summary>
        /// 指定したキャラクターに設定されている状態を付与します。
        /// </summary>
        /// <param name="owner"></param>
        public void AddBufsTo(BattleUnitModel owner)
        {
            AddBufTo(owner, KeywordBuf.Strength, Strength);
            AddBufTo(owner, KeywordBuf.Weak, Weak);
            AddBufTo(owner, KeywordBuf.Endurance, Endurance);
            AddBufTo(owner, KeywordBuf.Disarm, Disarm);
            AddBufTo(owner, KeywordBuf.Quickness, Quickness);
            AddBufTo(owner, KeywordBuf.Binding, Binding);
            AddBufTo(owner, KeywordBuf.Protection, Protection);
            AddBufTo(owner, KeywordBuf.Vulnerable, Vulnerable);
            AddBufTo(owner, KeywordBuf.BreakProtection, BreakProtection);
            AddBufTo(owner, KeywordBuf.Burn, Burn);
            AddBufTo(owner, KeywordBuf.Paralysis, Paralysis);
            AddBufTo(owner, KeywordBuf.Bleeding, Bleeding);
        }

        private static void AddBufTo(BattleUnitModel owner, KeywordBuf buf, int stack)
        {
            int clampedStack = Mathf.Clamp(stack, MinValue, MaxValue);
            if (clampedStack <= 0) { return; }

            owner.bufListDetail.AddKeywordBufThisRoundByEtc(buf, clampedStack);
        }
    }
}
