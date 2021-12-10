using System.Linq;
using TestingAssistGift.DataAccess;

namespace TestingAssistGift
{
    /// <summary>
    /// 戦闘表象「月光の輪」の機能を実装します。
    /// </summary>
    public class MoonlightRingModel
    {
        /// <summary>MOD 設定データにアクセスするリポジトリ オブジェクト</summary>
        private readonly IModSettingsRepository repository;

        /// <summary>
        /// <see cref="MoonlightRingModel"/> の新しいインスタンスを生成します。
        /// </summary>
        public MoonlightRingModel()
        {
            repository = new JsonModSettingsRepository();
        }

        /// <summary>
        /// 敵味方全ての生存キャラクターに効果を適用します。
        /// </summary>
        public void ApplyEffectAll()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
            {
                var buf = target.bufListDetail.GetActivatedBuf<BattleUnitBuf_MoonlightRing>();
                if (buf == null)
                {
                    buf = new BattleUnitBuf_MoonlightRing();
                    target.bufListDetail.AddBuf(buf);
                }

                EffectModel effect = repository.GetEffect(target);
                buf.Apply(effect);

                Log.Instance.InfomationWithCaller("Called.");
                foreach (var card in ItemXmlDataList.instance.GetCardList())
                {
                    Log.Instance.Infomation($"{{ id: '{card.id}', name: '{card.Name}' }}");
                }
            }
        }

        /// <summary>
        /// 味方全ての生存キャラクターに個人特殊ページ「設定リロード」を追加します。
        /// </summary>
        public void AddReloadCardAllPlayers()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList(Faction.Player))
            {
                target.personalEgoDetail.AddCard(Resource.ReloadCardId);
            }
        }
    }

    internal static class BattleUnitBufListDetailExtension
    {
        /// <summary>
        /// 指定した状態の型に一致する、この幕に付与されている状態を取得します。
        /// </summary>
        /// <typeparam name="T">取得する状態の型。</typeparam>
        /// <param name="bufListDetail"></param>
        /// <returns>指定した型の状態がこの幕に付与されている場合はその状態、付与されていない場合は null を返します。</returns>
        public static T GetActivatedBuf<T>(this BattleUnitBufListDetail bufListDetail) where T : BattleUnitBuf
            => bufListDetail.GetActivatedBufList().OfType<T>().FirstOrDefault();
    }
}
