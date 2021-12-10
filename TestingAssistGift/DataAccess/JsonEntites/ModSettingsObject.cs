using System;
using System.Collections.Generic;

namespace TestingAssistGift.DataAccess.JsonEntites
{
    /// <summary>
    /// MOD 設定データを格納します。
    /// </summary>
    public class ModSettingsObject
    {
        /// <summary>
        /// 戦闘表象「月光の輪」の効果が有効であることを表すフラグを取得または設定します。
        /// </summary>
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// 各キャラクター毎に適用する効果を取得または設定します。
        /// </summary>
        public ApplyingEffectNameObject ApplyingEffectName { get; set; } = new ApplyingEffectNameObject();

        /// <summary>
        /// 各キャラクターに適用できる効果のコレクションを取得または設定します。
        /// </summary>
        public IEnumerable<EffectObject> Effects { get; set; } = Array.Empty<EffectObject>();

        /// <summary>
        /// 値が null のプロパティを初期化します。
        /// </summary>
        public void InitializeNullProperties()
        {
            if (ApplyingEffectName == null)
            {
                ApplyingEffectName = new ApplyingEffectNameObject();
            }

            if (Effects == null)
            {
                Effects = Array.Empty<EffectObject>();
            }
            foreach (var effect in Effects)
            {
                effect.InitializeNullProperties();
            }
        }
    }
}
