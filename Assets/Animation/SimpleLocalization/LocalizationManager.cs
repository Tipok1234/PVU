using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Localization manager.
	/// </summary>
    public static class LocalizationManager
    {
        public enum LanguageEnum
        {
            English = 0,
            Russian = 1
        }


		/// <summary>
		/// Fired when localization changed.
		/// </summary>
        public static event Action LocalizationChanged = () => { }; 

        private static readonly Dictionary<string, Dictionary<string, string>> _languageDictionary = new Dictionary<string, Dictionary<string, string>>();
        private static string _language = "English";

		/// <summary>
		/// Get or set language.
		/// </summary>
        public static string Language
        {
            get { return _language; }
            set { _language = value; LocalizationChanged(); }
        }

		/// <summary>
		/// Set default language.
		/// </summary>
        public static void AutoLanguage()
        {
            Language = "English";
        }

        public static void SetLanguage(LanguageEnum languageEnum)
        {
            switch(languageEnum)
            {
                case LanguageEnum.English:
                    _language = "English";
                    break;
                case LanguageEnum.Russian:
                    _language = "Russian";
                    break;
            }

            LocalizationChanged();
        }

		/// <summary>
		/// Read localization spreadsheets.
		/// </summary>
		public static void Read(string path = "Localization")
        {
            if (_languageDictionary.Count > 0) return;

            var textAssets = Resources.LoadAll<TextAsset>(path);

            foreach (var textAsset in textAssets)
            {
                var text = ReplaceMarkers(textAsset.text);
                var matches = Regex.Matches(text, "\"[\\s\\S]+?\"");

                foreach (Match match in matches)
                {
					text = text.Replace(match.Value, match.Value.Replace("\"", null).Replace(",", "[comma]").Replace("\n", "[newline]"));
                }

                var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
				var languages = lines[0].Split(',').Select(i => i.Trim()).ToList();

				for (var i = 1; i < languages.Count; i++)
                {
                    if (!_languageDictionary.ContainsKey(languages[i]))
                    {
                        _languageDictionary.Add(languages[i], new Dictionary<string, string>());
                    }
                }
				
                for (var i = 1; i < lines.Length; i++)
                {
					var columns = lines[i].Split(',').Select(j => j.Trim()).Select(j => j.Replace("[comma]", ",").Replace("[newline]", "\n")).ToList();
					var key = columns[0];

                    if (string.IsNullOrEmpty(key))
                        continue;

                    for (var j = 1; j < languages.Count; j++)
                    {
                        _languageDictionary[languages[j]].Add(key, columns[j]);
                    }
                }
            }

            AutoLanguage();
        }

		/// <summary>
		/// Get localized value by localization key.
		/// </summary>
        public static string Localize(string localizationKey)
        {
            if (_languageDictionary.Count == 0)
            {
                Read();
            }

            if (!_languageDictionary.ContainsKey(Language)) throw new KeyNotFoundException("Language not found: " + Language);
            if (!_languageDictionary[Language].ContainsKey(localizationKey)) throw new KeyNotFoundException("Translation not found: " + localizationKey);

            return _languageDictionary[Language][localizationKey];
        }

	    /// <summary>
	    /// Get localized value by localization key.
	    /// </summary>
		public static string Localize(string localizationKey, params object[] args)
        {
            var pattern = Localize(localizationKey);

            return string.Format(pattern, args);
        }

        private static string ReplaceMarkers(string text)
        {
            return text.Replace("[Newline]", "\n");
        }
    }
}