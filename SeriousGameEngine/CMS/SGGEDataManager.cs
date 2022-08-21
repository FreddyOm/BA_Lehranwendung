using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using SGGE;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using SeriousGameEngine.TemplateElemente;
using System.Windows.Media;

namespace SeriousGameEngine.CMS
{
    /// <summary>
    /// A class to load and provide the data from the SGGEoptions.sgge file
    /// </summary>
    public class SGGEDataManager
    {
        private string LOAD_PATH;
        private Dictionary<string, OptionDataElement> optionsDict = new Dictionary<string, OptionDataElement>();

        #region init

        /// <summary>
        /// The Constructor of the data manager
        /// </summary>
        public SGGEDataManager()
        {
            LOAD_PATH = Environment.CurrentDirectory + "/Resource/SGGEoptions.sgge";
            LoadOptions();
        }

        /// <summary>
        /// Loads all options from the 
        /// </summary>
        /// <returns></returns>
        public bool LoadOptions()
        {
            //Try to load file
            try
            {
                StreamReader sr = new StreamReader(LOAD_PATH);

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    OptionData od = JsonConvert.DeserializeObject<OptionData>(line);

                    if (od.Option == OPTION.ARRAY)
                    {
                        // Arrays get a special treatment. -> Deserializing as OptionData and afterwards casting as EnumOptionData/ArrayOptionData doesn't work!

                        //deserialize the line dynamically via "JObject.Parse"
                        var aoe = JObject.Parse(line);
                        List<OptionData> subOptions = new List<OptionData>();

                        //traverse the suboptions individually
                        foreach (var option in aoe["Options"])
                        {
                            // deserialize a general OptionData object
                            var subOption = JsonConvert.DeserializeObject<OptionData>(option.ToString());
                            switch(subOption.Option)
                            {
                                case OPTION.ARRAY:
                                    // not supported
                                    continue;
                                case OPTION.ENUM:
                                    //deserialize the object specifically as an enum so no data is lost during the deserialization
                                    subOptions.Add(JsonConvert.DeserializeObject<EnumOptionData>(option.ToString()));
                                    break;
                                default:
                                    subOptions.Add(subOption);
                                    break;
                            }
                        }
                        // add all general data to the dictionary and pass the subOption list as an array
                        optionsDict.Add(od.Path, new ArrayOptionDataElement(od.Path, od.Tooltip, OPTION.ARRAY, subOptions.ToArray()));
                    }
                    else if (od.Option == OPTION.ENUM)
                    {
                        var eod = JsonConvert.DeserializeObject<EnumOptionData>(line);
                        optionsDict.Add(eod.Path, new EnumOptionDataElement(eod.Path, eod.Tooltip, eod.Option, eod.Enumerables));
                    }
                    else
                    {
                        optionsDict.Add(od.Path, new OptionDataElement(od.Path, od.Tooltip, od.Option));
                    }
                }

                sr.Close();

                return true;
            }
            //otherwise show exception message
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Die \".sgge\" Datei konnte nicht gefunden werden oder nicht korrekt gelesen werden.\nStelle sicher, dass sie am erwarteten Zielpfad ist.");
                return false;
            }
        }

        #endregion init

        #region element getter

        /// <summary>
        /// Gets an option element with a specific id.
        /// </summary>
        /// <param name="elementPath"></param>
        /// <returns></returns>
        public OptionDataElement GetElement(string elementPath)
        {
            if(!optionsDict.ContainsKey(elementPath))
                return null;

            return optionsDict[elementPath];
        }

        /// <summary>
        /// Gets all option elements available for that game.
        /// </summary>
        /// <returns></returns>
        public OptionDataElement[] GetAllElements()
        {
            return optionsDict.Values.ToArray();
        }

        /// <summary>
        /// Gets a list with all categories. No redundancies.
        /// </summary>
        /// <returns></returns>
        public OptionDataElement[] GetAllCategories()
        {
            List<OptionDataElement> oe = new List<OptionDataElement>();

            foreach(var element in GetAllElements())
            {
                if(!oe.Any( el => el.Category == element.Category))
                {
                    oe.Add(element);
                }
            }

            return oe.ToArray();
        }

        /// <summary>
        /// Gets all option elements that share one specific category.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public OptionDataElement[] GetElementsOfCategory(string categoryName)
        {
            List<OptionDataElement> oe = new List<OptionDataElement>();

            foreach (var element in GetAllElements())
            {
                if (element.Category.Equals(categoryName))
                {
                    oe.Add(element);
                }
            }

            return oe.ToArray();
        }

        #endregion element getter
    }

    #region option data elements

    /// <summary>
    /// A data container to store all data from the SGGE Options.
    /// </summary>
    public class OptionDataElement
    {
        public static int _index = 0;
        public string Path { get; private set; }
        public string Category { get;  private set; }
        public string Header { get;  private set; }
        public string Name { get;  private set; }
        public string Tooltip { get; private set; }
        public SGGE.OPTION Option { get; private set; }

        /// <summary>
        /// Constructor sets values and splits the path to the correct category
        /// </summary>
        /// <param name="path"></param>
        /// <param name="tooltip"></param>
        /// <param name="option"></param>
        public OptionDataElement(string path, string tooltip, SGGE.OPTION option)
        {
            string[] pathSplit = path.Split('/');

            switch(pathSplit.Length)
            {
                case 3:
                    Category = pathSplit[0];
                    Header = pathSplit[1];
                    Name = pathSplit[2];
                    break;

                case 2:
                    Category = pathSplit[0];
                    Name = pathSplit[1];
                    Header = "";
                    break;

                default:
                    Category = "Sonstige";
                    Name = "";
                    Header = "Element_" + _index++;
                    break;
            }

            Tooltip = tooltip;
            Option = option;
            Path = path;
        }
    }

    /// <summary>
    /// A data container for an array option.
    /// </summary>
    public class ArrayOptionDataElement : OptionDataElement
    {
        public OptionDataElement[] subOptionElements;
        public ArrayOptionDataElement(string path, string tooltip, OPTION option, OptionData[] subOptions) : base(path, tooltip, option)
        {
            subOptionElements = new OptionDataElement[subOptions.Length];

            for(int i = 0; i < subOptions.Length; i++)
            {
                if(subOptions[i].Option == OPTION.ARRAY)
                {
                    System.Windows.MessageBox.Show($"Multidimensionale Arrays werden aktuell nicht unterstützt!\nDas Element {subOptions[i].Path} wird übersprungen.");
                    continue;
                }
                else if(subOptions[i].Option == OPTION.ENUM)
                {
                    var enumOption = (EnumOptionData) subOptions[i];
                    subOptionElements[i] = new EnumOptionDataElement(enumOption.Path, enumOption.Tooltip, OPTION.ENUM, enumOption.Enumerables);
                }
                else
                {
                    subOptionElements[i] = new OptionDataElement(subOptions[i].Path, subOptions[i].Tooltip, subOptions[i].Option);
                } 
            }
        }
    }

    /// <summary>
    /// A data container for an enum option.
    /// </summary>
    public class EnumOptionDataElement : OptionDataElement
    {
        public string[] EnumValues;
        public EnumOptionDataElement(string path, string tooltip, SGGE.OPTION option, string[] _enum) : base(path, tooltip, option)
        {
            EnumValues = _enum;
        }
    }

    #endregion option data elements

    public class SaveUtility : IDisposable
    {
        public static string SAVE_PATH;
        public static string SAVE_FILE = "/appsave.sgge";
        public static string RESOURCE_PATH;

        private Dictionary<string, OptionValue> saveValues = new Dictionary<string, OptionValue>();

        public SaveUtility()
        {
            SAVE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SGGEApp/application";
            RESOURCE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SGGEApp/resources";

            // folder
            if (!Directory.Exists(SAVE_PATH))
            {
                Directory.CreateDirectory(SAVE_PATH);
            }

            if(!Directory.Exists(RESOURCE_PATH))
            {
                Directory.CreateDirectory(RESOURCE_PATH);
            }

            // file
            if(!File.Exists(SAVE_PATH + SAVE_FILE))
            {
                File.Create(SAVE_PATH + SAVE_FILE);
            }

            DeserializeOptions();

            OptionUIElement.onValueChanged += SaveOptions;
        }

        public string[] GetFullOptionID(string id)
        {
            List<string> ids = new List<string>();

            foreach(var kvp in saveValues)
            {
                if(kvp.Key.Contains(id))
                {
                    ids.Add(kvp.Key);
                }
            }

            return ids.ToArray();
        }

        public void Remove(string id)
        {
            if (saveValues.ContainsKey(id))
            {
                saveValues.Remove(id);
            }
        }

        /// <summary>
        /// Saves changed values into the dictionary.
        /// </summary>
        /// <param name="optionID"></param>
        /// <param name="value"></param>
        /// <param name="option"></param>
        private void SaveOptions(string optionID, OptionUIElement value, OPTION option)
        { 
            if(saveValues.ContainsKey(optionID))
            {
                saveValues.Remove(optionID);
            }

            switch(option)
            {
                case OPTION.YES_NO_OPTION:
                    YesNoOptionElement yno = (YesNoOptionElement) value;
                    saveValues.Add(optionID, new OptionYesNoValue(OPTION.YES_NO_OPTION, optionID, yno.GetValue()));
                    break;
                case OPTION.REAL_NUM:
                    RealNumOptionElement rn = (RealNumOptionElement)value;
                    saveValues.Add(optionID, new OptionRealValue(OPTION.REAL_NUM, optionID, rn.GetValue()));
                    break;
                case OPTION.DECIMAL_NUM:
                    DecimalNumOptionElement dno = (DecimalNumOptionElement)value;
                    saveValues.Add(optionID, new OptionDecimalValue(OPTION.DECIMAL_NUM, optionID, dno.GetValue()));
                    break;
                case OPTION.COLOR:
                    ColorOptionElement co = (ColorOptionElement)value;
                    saveValues.Add(optionID, new OptionColorValue(OPTION.COLOR, optionID, Design.HexConverter(co.GetValue())));
                    break;
                case OPTION.TEXT:
                    TextOptionElement to = (TextOptionElement)value;
                    saveValues.Add(optionID, new OptionTextValue(OPTION.TEXT, optionID, to.GetValue()));
                    break;
                case OPTION.SOUND_FILE:
                    AudioOptionElement so = (AudioOptionElement)value;
                    saveValues.Add(optionID, new OptionAudioValue(OPTION.SOUND_FILE, optionID, so.GetValue()));
                    break;
                case OPTION.GRAPHICS:
                    GraphicOptionElement element = (GraphicOptionElement)value;
                    saveValues.Add(optionID, new OptionGraphicValue(OPTION.GRAPHICS, optionID, element.GetValue()));
                    break;
                case OPTION.ARRAY:
                    ArrayOptionElement ao = (ArrayOptionElement)value;
                    saveValues.Add(optionID, new OptionArrayValue(OPTION.ARRAY, optionID, ao.GetArrayCount(), ao.GetValue()));
                    break;
                case OPTION.ENUM:
                    EnumOptionElement eo = (EnumOptionElement)value;
                    saveValues.Add(optionID, new OptionEnumValue(OPTION.ENUM, optionID, eo.GetValue()));
                    break;
            }

            SerializeValues();
        }

        /// <summary>
        /// Loads an option with a specific id
        /// </summary>
        /// <param name="optionID"></param>
        /// <returns></returns>
        public OptionValue LoadOption(string optionID)
        {
            if(saveValues.ContainsKey(optionID))
            {
                switch(saveValues[optionID].Option)
                {
                    case OPTION.YES_NO_OPTION:
                        return saveValues[optionID] as OptionYesNoValue;
                    case OPTION.REAL_NUM:
                        return saveValues[optionID] as OptionRealValue;
                    case OPTION.DECIMAL_NUM:
                        return saveValues[optionID] as OptionDecimalValue;
                    case OPTION.GRAPHICS:
                        return saveValues[optionID] as OptionGraphicValue;
                    case OPTION.SOUND_FILE:
                        return saveValues[optionID] as OptionAudioValue;
                    case OPTION.ENUM:
                        return saveValues[optionID] as OptionEnumValue;
                    case OPTION.ARRAY:
                        return saveValues[optionID] as OptionArrayValue;
                    case OPTION.TEXT:
                        return saveValues[optionID] as OptionTextValue;
                    case OPTION.COLOR:
                        return saveValues[optionID] as OptionColorValue;
                }
            }

            return null;
        }

        /// <summary>
        /// Serializes the dictionary to save all values
        /// </summary>
        private void SerializeValues()
        {
            StreamWriter sw = new StreamWriter(SAVE_PATH + SAVE_FILE);

            foreach (KeyValuePair<string, OptionValue> option in saveValues)
            {
                string json;
                json = JsonConvert.SerializeObject(option.Value);
                sw.WriteLine(json);
            }

            sw.Close();
        }

        /// <summary>
        /// Deserializes options
        /// </summary>
        private void DeserializeOptions()
        {
            StreamReader sr = new StreamReader(SAVE_PATH + SAVE_FILE);
            
            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();

                OptionValue value = JsonConvert.DeserializeObject<OptionValue>(line);

                switch(value.Option)
                {
                    case OPTION.GRAPHICS:
                        var go = JsonConvert.DeserializeObject<OptionGraphicValue>(line);
                        if (!saveValues.ContainsKey(value.Path)){saveValues.Add(value.Path, go);}
                        break;
                    case OPTION.YES_NO_OPTION:
                        var yno = JsonConvert.DeserializeObject<OptionYesNoValue>(line);
                        if (!saveValues.ContainsKey(value.Path)) { saveValues.Add(value.Path, yno); }
                        break;
                    case OPTION.TEXT:
                        var to = JsonConvert.DeserializeObject<OptionTextValue>(line);
                        if (!saveValues.ContainsKey(value.Path)) { saveValues.Add(value.Path, to); }
                        break;
                    case OPTION.SOUND_FILE:
                        var so = JsonConvert.DeserializeObject<OptionAudioValue>(line);
                        if (!saveValues.ContainsKey(value.Path)) { saveValues.Add(value.Path, so); }
                        break;
                    case OPTION.DECIMAL_NUM:
                        var dno = JsonConvert.DeserializeObject<OptionDecimalValue>(line);
                        if (!saveValues.ContainsKey(value.Path)) { saveValues.Add(value.Path, dno); }
                        break;
                    case OPTION.REAL_NUM:
                        var rno = JsonConvert.DeserializeObject<OptionRealValue>(line);
                        if (!saveValues.ContainsKey(value.Path)) { saveValues.Add(value.Path, rno); }
                        break;
                    case OPTION.ENUM:
                        var eo = JsonConvert.DeserializeObject<OptionEnumValue>(line);
                        if (!saveValues.ContainsKey(value.Path)) { saveValues.Add(value.Path, eo); }
                        break;
                    case OPTION.ARRAY:
                        var ao = JsonConvert.DeserializeObject<OptionArrayValue>(line);
                        if (!saveValues.ContainsKey(value.Path)) { saveValues.Add(value.Path, ao); }
                        break;
                    case OPTION.COLOR:
                        var co = JsonConvert.DeserializeObject<OptionColorValue>(line);
                        if (!saveValues.ContainsKey(value.Path)) { saveValues.Add(value.Path, co); }
                        break;
                }
            }
        }

        /// <summary>
        /// Dispose class
        /// </summary>
        public void Dispose()
        {
            OptionUIElement.onValueChanged -= SaveOptions;
        }
    }
}
