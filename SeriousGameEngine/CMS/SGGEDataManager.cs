using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGGE;
using SGGE_Runtime;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace SeriousGameEngine.CMS
{
    /// <summary>
    /// A class to load and provide the data from the SGGEoptions.sgge file
    /// </summary>
    public class SGGEDataManager
    {
        public string LOAD_PATH;

        private Dictionary<string, OptionDataElement> optionsDict = new Dictionary<string, OptionDataElement>();

        public SGGEDataManager()
        {
            LOAD_PATH = Environment.CurrentDirectory + "/Resource/SGGEoptions.sgge";
            LoadOptions();
        }

        public OptionDataElement GetElement(string elementPath)
        {
            if(!optionsDict.ContainsKey(elementPath))
                return null;

            return optionsDict[elementPath];
        }

        public OptionDataElement[] GetAllElements()
        {
            return optionsDict.Values.ToArray();
        }

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
        /// 
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

                    var od = JsonConvert.DeserializeObject<OptionData>(line);

                    if (od.Option == SGGE.OPTION.ARRAY)
                    {
                        ArrayOptionData aod = (ArrayOptionData)od;
                        optionsDict.Add(aod.Path, new ArrayOptionDataElement(aod.Path, aod.Tooltip, aod.Option, aod.Options.ToArray()));
                    }
                    else if(od.Option == SGGE.OPTION.ENUM)
                    {
                        OptionData eod = od;
                        optionsDict.Add(eod.Path, new EnumOptionDataElement(eod.Path, eod.Tooltip, eod.Option, null));
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
    }



    /// <summary>
    /// A container to store all data from the SGGE Options
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

    public class ArrayOptionDataElement : OptionDataElement
    {
        public OptionDataElement[] optionDataElements;
        public ArrayOptionDataElement(string path, string tooltip, SGGE.OPTION option, OptionData[] subOptions) : base(path, tooltip, option)
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

    public class EnumOptionDataElement : OptionDataElement
    {
        public Array EnumValues;
        public EnumOptionDataElement(string path, string tooltip, SGGE.OPTION option, Array _enum) : base(path, tooltip, option)
        {
            EnumValues = _enum;
        }
    }
}
