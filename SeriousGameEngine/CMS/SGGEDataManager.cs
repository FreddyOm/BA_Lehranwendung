using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using SGGE;
using Newtonsoft.Json;

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
                        ArrayOptionData aod = JsonConvert.DeserializeObject<ArrayOptionData>(line);
                        optionsDict.Add(aod.Path, new ArrayOptionDataElement(aod.Path, aod.Tooltip, aod.Option, aod.Options.ToArray()));
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
            catch (Exception e)
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
        public OptionDataElement[] optionDataElements;
        public ArrayOptionDataElement(string path, string tooltip, SGGE.OPTION option, IOptionData[] subOptions) : base(path, tooltip, option)
        {
            optionDataElements = new OptionDataElement[subOptions.Length];

            for(int i = 0; i < subOptions.Length; i++)
            {
                if(subOptions[i].Option == SGGE.OPTION.ARRAY)
                {
                    System.Windows.MessageBox.Show("Multidimensional arrays are currently not supported!");
                    continue;
                }
                else if(subOptions[i].Option == SGGE.OPTION.ENUM)
                {

                }
                else
                {
                    optionDataElements[i] = new OptionDataElement(subOptions[i].Path, subOptions[i].Tooltip, subOptions[i].Option);
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
}
