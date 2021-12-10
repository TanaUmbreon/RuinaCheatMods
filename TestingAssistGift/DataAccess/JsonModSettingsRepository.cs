using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TestingAssistGift.DataAccess.JsonEntites;

namespace TestingAssistGift.DataAccess
{
    /// <summary>
    /// JSON ファイル形式で格納された MOD 設定データへのアクセスを提供します。
    /// </summary>
    public class JsonModSettingsRepository : IModSettingsRepository
    {
        /// <summary>MOD 設定データ</summary>
        private readonly ModSettingsObject settings;

        /// <summary>
        /// <see cref="JsonModSettingsRepository"/> の新しいインスタンスを生成します。
        /// </summary>
        public JsonModSettingsRepository()
        {
            try
            {
                string json = File.ReadAllText(Resource.JsonSettingsFilePath);
                ModSettingsObject settings = JsonConvert.DeserializeObject<ModSettingsObject>(json);
                settings.InitializeNullProperties();
                this.settings = settings;
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
                Log.Instance.Warning("設定ファイルを読み込みできなかった為、既定値を使用します。");
                settings = new ModSettingsObject();
            }
        }

        public EffectModel GetEffect(BattleUnitModel target)
        {
            if (!settings.Enabled) { return EffectModel.None; }

            switch (target.faction)
            {
                case Faction.Enemy:
                    switch (target.index)
                    {
                        case 0:
                            return GetEffect(settings.ApplyingEffectName.Enemy1);
                        case 1:
                            return GetEffect(settings.ApplyingEffectName.Enemy2);
                        case 2:
                            return GetEffect(settings.ApplyingEffectName.Enemy3);
                        case 3:
                            return GetEffect(settings.ApplyingEffectName.Enemy4);
                        case 4:
                            return GetEffect(settings.ApplyingEffectName.Enemy5);
                    }
                    break;

                case Faction.Player:
                    switch (target.index)
                    {
                        case 0:
                            return GetEffect(settings.ApplyingEffectName.Player1);
                        case 1:
                            return GetEffect(settings.ApplyingEffectName.Player2);
                        case 2:
                            return GetEffect(settings.ApplyingEffectName.Player3);
                        case 3:
                            return GetEffect(settings.ApplyingEffectName.Player4);
                        case 4:
                            return GetEffect(settings.ApplyingEffectName.Player5);
                    }
                    break;
            }

            Log.Instance.WarningWithCaller($"{target.UnitData.unitData.name}のキャラクター位置 ({target.index}) が範囲外です。規定値を使用します。");
            return EffectModel.None;
        }

        /// <summary>
        /// 指定した効果名に一致する効果を返します。
        /// </summary>
        /// <param name="effectName">取得する効果の名前。</param>
        /// <returns>指定した名前に完全一致する効果。該当する効果が複数ある場合は最初に一致した効果を返します。該当する効果がない場合は既定の効果を返します。</returns>
        private EffectModel GetEffect(string effectName)
        {
            if (effectName == null) { return EffectModel.None; }

            EffectObject effect = settings.Effects.FirstOrDefault(e => e.Name == effectName);
            if (effect == null)
            {
                Log.Instance.WarningWithCaller($"効果名 '{effectName}' は定義されていません。規定値を使用します。");
                return EffectModel.None;
            }

            return effect.CreateEffectModel();
        }
    }
}
