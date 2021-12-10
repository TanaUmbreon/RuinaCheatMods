using UnityEngine;

namespace TestingAssistGift.DataAccess.JsonEntites
{
    /// <summary>
    /// 毎幕回復するステータスを格納します。
    /// </summary>
    public class RecoveringStatOnRoundStartObject
    {
        private const int MinValue = 0;
        private const int MaxValue = 1000;
        private const int MinPlayPointValue = 0;
        private const int MaxPlayPointValue = 10;
        private const int DefaultValue = 0;

        /// <summary>
        /// 体力の回復量を取得または設定します。
        /// </summary>
        public int HpRecover { get; set; } = 0;

        /// <summary>
        /// 混乱抵抗値の回復量を取得または設定します。
        /// </summary>
        public int BreakRecover { get; set; } = 0;

        /// <summary>
        /// 光の回復量を取得または設定します。
        /// </summary>
        public int PlayPoint { get; set; } = 0;

        /// <summary>
        /// 指定したキャラクターのステータスを設定されている値で回復します。
        /// </summary>
        /// <param name="owner"></param>
        public void RecoverTo(BattleUnitModel owner)
        {
            int clampedHp = Mathf.Clamp(HpRecover, MinValue, MaxValue);
            if (clampedHp > 0)
            {
                owner.RecoverHP(clampedHp);
            }

            int clampedBreak = Mathf.Clamp(BreakRecover, MinValue, MaxValue);
            if (clampedBreak > 0 && !owner.IsBreakLifeZero())
            {
                owner.RecoverBreakLife(clampedBreak);
            }

            int clampedPlay = Mathf.Clamp(PlayPoint, MinPlayPointValue, MaxPlayPointValue);
            if (clampedPlay > 0)
            {
                owner.cardSlotDetail.RecoverPlayPoint(clampedPlay);
            }
        }
    }
}
