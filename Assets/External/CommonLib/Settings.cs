namespace CommonLib
{
    using System;
    using CommonLib.Collections;
    using UnityEngine;

    [Serializable]
    public class SettingsCollection : DynamicSerializationTable
    {

    }

    [Serializable]
    public class Settings : MonoBehaviour, ISettings
    {
        [SerializeField]
        private SettingsCollection settings = new SettingsCollection();

        public Setting GetSetting(string name)
        {
            object setting;
            this.settings.TryGetValue(name, out setting);
            return setting as Setting;
        }

        public Setting AddSetting(string name, float value)
        {
            return this.GetOrAddSetting(name, value);
        }

        public Setting AddSetting(string name, int value)
        {
            return this.GetOrAddSetting(name, value);
        }

        public Setting AddSetting(string name, bool value)
        {
            return this.GetOrAddSetting(name, value);
        }

        public Setting AddSetting(string name, string value)
        {
            return this.GetOrAddSetting(name, value);
        }

        public void SetFloat(string name, float value)
        {
            Setting setting = this.GetOrAddSetting(name, value);
            setting.Value = value;
        }

        public void SetInt(string name, int value)
        {
            Setting setting = this.GetOrAddSetting(name, value);
            setting.Value = value;
        }

        public void SetBool(string name, bool value)
        {
            Setting setting = this.GetOrAddSetting(name, value);
            setting.Value = value;
        }

        public void SetString(string name, string value)
        {
            Setting setting = this.GetOrAddSetting(name, value);
            setting.Value = value;
        }

        public float GetFloat(string name, float @default = 0)
        {
            Setting setting = this.GetOrAddSetting(name, @default);
            return (float)setting.Value;
        }

        public int GetInt(string name, int @default = 0)
        {
            Setting setting = this.GetOrAddSetting(name, @default);
            return (int)setting.Value;
        }

        public bool GetBool(string name, bool @default = false)
        {
            Setting setting = this.GetOrAddSetting(name, @default);
            return (bool)setting.Value;
        }

        public string GetString(string name, string @default = null)
        {
            Setting setting = this.GetOrAddSetting(name, @default);
            return (string)setting.Value;
        }

        private Setting GetOrAddSetting(string name, object value)
        {
            object setting;
            if (!this.settings.TryGetValue(name, out setting))
            {
                setting = new Setting { Name = name, Type = value.GetType(), Value = value };
            }
            return setting as Setting;
        }

        private Setting GetOrAddSetting(string name, Type type)
        {
            object setting;
            if (!this.settings.TryGetValue(name, out setting))
            {
                object value = this.GetDefaultValue(type);
                setting = new Setting { Name = name, Type = type, Value = value };
            }
            return setting as Setting;
        }

        private object GetDefaultValue(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }

    public interface IReadonlySettings
    {
        float GetFloat(string name, float @default = default(float));
        int GetInt(string name, int @default = default(int));
        bool GetBool(string name, bool @default = default(bool));
        string GetString(string name, string @default = default(string));
    }

    public interface ISettings : IReadonlySettings
    {
        Setting GetSetting(string name);

        Setting AddSetting(string name, float value);
        Setting AddSetting(string name, int value);
        Setting AddSetting(string name, bool value);
        Setting AddSetting(string name, string value);

        void SetFloat(string name, float value);
        void SetInt(string name, int value);
        void SetBool(string name, bool value);
        void SetString(string name, string value);
    }

    [Serializable]
    public class Setting : ISerializationCallbackReceiver
    {
        private string json;

        public string Name;
        public Type Type;

        [NonSerialized]
        public object Value;

        public void OnBeforeSerialize()
        {
            this.json = JsonUtility.ToJson(this.Value);
        }

        public void OnAfterDeserialize()
        {
            this.Value = JsonUtility.FromJson(this.json, this.Type);
        }
    }
}
