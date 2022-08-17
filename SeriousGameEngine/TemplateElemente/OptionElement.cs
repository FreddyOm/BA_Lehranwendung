using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using System.IO;

namespace SeriousGameEngine.TemplateElemente
{
    public class OptionUIElement : DockPanel
    {
        int marginL = 5;
        int marginT = 5;
        int marginR = 5;
        int marginB = 0;
        private float width = 200.0f;
        private float height = 25.0f;
        public static string backgroundColor1 = "#FF0A2630";
        public static string backgroundColor2 = "#FF9C8A87";
        public static string foregroundColor1 = "#FFFFFFFF";
        public static string foregroundColor2 = "#FF91BAD5";

        public OptionUIElement(string optionName, string tooltip)
        {
            this.Name = optionName;
            this.Margin = new Thickness(marginL, marginT, marginR, marginB);

            Label labelName = new Label();
            Label labelTooltip = new Label();

            labelName.Content = optionName;
            labelName.Width = width;
            labelName.Height = height;
            labelName.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(foregroundColor1));

            labelTooltip.Content = tooltip;
            labelTooltip.Width = width;
            labelTooltip.Height = height;
            labelTooltip.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(foregroundColor1));

            SetDock(labelName, Dock.Left);
            Children.Add(labelName);

            SetDock(labelTooltip, Dock.Left);
            Children.Add(labelTooltip);
        }
    }

    public class YesNoOptionElement : OptionUIElement
    {
        CheckBox checkBox;

        public YesNoOptionElement(string id, string optionName, string tooltip, bool value = false) : base(optionName, tooltip)
        {
            checkBox = new CheckBox();
            checkBox.Name = id.Replace('/', '_');
            checkBox.IsChecked = value;

            SetDock(checkBox, Dock.Right);
            Children.Add(checkBox);
        }

        public bool GetValue()
        {
            return (bool)checkBox.IsChecked;
        }
    }

    public class TextOptionElement : OptionUIElement
    {
        TextBox textBox;

        public TextOptionElement(string id, string optionName, string tooltip, string value = "") : base(optionName, tooltip)
        {
            textBox = new TextBox();
            textBox.Name = id.Replace('/', '_');
            textBox.Text = value;

            SetDock(textBox, Dock.Right);
            Children.Add(textBox);
        }

        public string GetValue()
        {
            return (string)textBox.Text;
        }
    }

    public class RealNumOptionElement : OptionUIElement
    {
        TextBox textBox;

        public RealNumOptionElement(string id, string optionName, string tooltip, string value = "0") : base(optionName, tooltip)
        {
            textBox = new TextBox();
            textBox.Name = id.Replace('/','_');
            textBox.Text = "" + value;

            SetDock(textBox, Dock.Right);
            Children.Add(textBox);
        }

        public int GetValue()
        {
            return int.Parse(textBox.Text);
        }
    }

    public class DecimalNumOptionElement : OptionUIElement
    {
        TextBox textBox;

        public DecimalNumOptionElement(string id, string optionName, string tooltip, string value = "0.0") : base(optionName, tooltip)
        {
            textBox = new TextBox();
            textBox.Name = id.Replace('/', '_'); ;
            textBox.Text = "" + value;

            SetDock(textBox, Dock.Right);
            Children.Add(textBox);
        }

        public float GetValue()
        {
            return float.Parse(textBox.Text);
        }
    }

    public class ColorOptionElement : OptionUIElement
    {
        ColorPicker colorPicker;

        public ColorOptionElement(string id, string optionName, string tooltip, Color value) : base(optionName, tooltip)
        {
            colorPicker = new ColorPicker();
            colorPicker.Name = id.Replace('/', '_');
            colorPicker.SelectedColor = value;

            SetDock(colorPicker, Dock.Right);
            Children.Add(colorPicker);
        }

        public Color GetValue()
        {
            return Colors.White;
        }
    }

    public class EnumOptionElement : OptionUIElement
    {
        ComboBox dropDown;

        public EnumOptionElement(string id, string optionName, string tooltip, string[] enumOptions) : base(optionName, tooltip)
        {
            dropDown = new ComboBox();
            dropDown.Name = id.Replace('/', '_');
            dropDown.ItemsSource = enumOptions;
            
            SetDock(dropDown, Dock.Right);
            Children.Add(dropDown);
        }

        public int GetValue()
        {
            return dropDown.SelectedIndex;
        }
    }

    public class DragDropGraphicElement : OptionUIElement
    {
        public string path;
        private Border border;
        private Image image;

        private string[] format;
        public DragDropGraphicElement(string[] format, string id, string optionName, string tooltip, string path) : base(optionName, tooltip)
        {
            if (!string.IsNullOrEmpty(path))
            {
                this.path = path;
            }
            this.format = format;

            border = new Border();
            image = new Image();
            border.CornerRadius = new CornerRadius(3,3,3,3);
            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(OptionUIElement.backgroundColor2));
            border.Width = 50;
            border.Height = 50;

            border.AllowDrop = true;
            
            border.Drop += DragAndDrop;

            DockPanel.SetDock(border, Dock.Left);
            Children.Add(border);
 
            DockPanel.SetDock(image, Dock.Right);
            Children.Add(image);

        }

        public void Dispose()
        {
            this.Drop -= DragAndDrop;
        }

        private void DragAndDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var data = (string[])e.Data.GetData(DataFormats.FileDrop);
                path = data[0];

                if (!format.Any<string>(ending => path.ToLower().EndsWith(ending)))
                {
                    string errorMsg = "Aktuell werden nur folgemde Dateitypen unterstützt:\n";

                    foreach(string ext in format)
                    {
                        errorMsg += "\"" + ext + "\" \n";
                    }

                    System.Windows.MessageBox.Show(errorMsg);
                    return;
                }
                
                try
                {
                    image.Width = 150;
                    image.BeginInit();
                    image.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(path);
                    image.EndInit();
                }catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
                    

            }
        }
    }

    public class GraphicOptionElement : DragDropGraphicElement
    {
        public GraphicOptionElement(string id, string optionName, string tooltip, string value = "") : base(new [] {".png", ".tif", ".jpg", ".jpeg"},id, optionName, tooltip, value)
        {  }

        public string GetValue()
        {
            return path;
        }
    }

    public class DragDropAudioElement : OptionUIElement
    {
        public string path;
        private Border border;
        private Image image;

        private string[] format;
        public DragDropAudioElement(string[] format, string id, string optionName, string tooltip, string path) : base(optionName, tooltip)
        {
            if (!string.IsNullOrEmpty(path))
            {
                this.path = path;
            }
            this.format = format;

            border = new Border();
            image = new Image();
            border.CornerRadius = new CornerRadius(3, 3, 3, 3);
            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(OptionUIElement.backgroundColor2));
            border.Width = 50;
            border.Height = 50;

            border.AllowDrop = true;

            border.Drop += DragAndDrop;

            DockPanel.SetDock(border, Dock.Left);
            Children.Add(border);

            DockPanel.SetDock(image, Dock.Right);
            Children.Add(image);

        }

        public void Dispose()
        {
            this.Drop -= DragAndDrop;
        }

        private void DragAndDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var data = (string[])e.Data.GetData(DataFormats.FileDrop);
                path = data[0];

                if (!format.Any<string>(ending => path.ToLower().EndsWith(ending)))
                {
                    string errorMsg = "Aktuell werden nur folgemde Dateitypen unterstützt:\n";

                    foreach (string ext in format)
                    {
                        errorMsg += "\"" + ext + "\" \n";
                    }

                    System.Windows.MessageBox.Show(errorMsg);
                    return;
                }

                try
                {
                    image.Width = 50;
                    image.BeginInit();
                    string imgPath = Environment.CurrentDirectory + "/Resource/AudioDatei1.png";
                    image.Source = (ImageSource) new ImageSourceConverter().ConvertFromString(imgPath);
                    image.EndInit();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
            }
        }
    }

    public class AudioOptionElement : DragDropAudioElement
    {
        public AudioOptionElement(string id, string optionName, string tooltip, string value = "") : base(new[] { ".wav", ".mp3" }, id, optionName, tooltip, value)
        { }

        public string GetValue()
        {
            return path;
        }
    }
}
