namespace TestingAssistGift.DataAccess
{
    /// <summary>
    /// MOD 設定データへのアクセスを提供します。
    /// </summary>
    public interface IModSettingsRepository
    {
        /// <summary>
        /// 指定したキャラクターに適用させる効果を取得します。
        /// </summary>
        /// <param name="target">効果を取得する対象のキャラクター。</param>
        /// <returns>指定したキャラクターに適用させる効果。月光の輪の効果が無効化されていたり、指定したキャラクターに適用する効果が未定義または無効であったりする場合は、 <see cref="EffectModel.None"/> を返します。</returns>
        EffectModel GetEffect(BattleUnitModel target);
    }
}
