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
using SeriousGameEngine.CMS;

namespace SeriousGameEngine.TemplateElemente
{

    public class OptionUIElement : DockPanel, IDisposable
    {
        int marginL = 10;
        int marginT = 10;
        int marginR = 10;
        int marginB = 0;

        private float width = 200.0f;
        private float height = 38;
        public Border border = new Border();
        Tooltip tooltipElement;
        public OptionUIElement(string optionName, string tooltip)
        {
            // DockPanel
            Name = optionName;
            Margin = new Thickness(marginL, marginT, marginR, marginB);
            Height = height;
            //SetDock(this, Dock.Top);
            LastChildFill = true;

            //TextBlock
            TextBlock textName = new TextBlock();
            textName.Text = optionName;
            textName.Width = width;
            //textName.Height = height;
            textName.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.foregroundColor1));
            textName.HorizontalAlignment = HorizontalAlignment.Left;
            textName.VerticalAlignment = VerticalAlignment.Center;

            SetDock(textName, Dock.Left);
            Children.Add(textName);

            // Tooltip Image
            tooltipElement = new Tooltip(tooltip);
            SetDock(tooltipElement, Dock.Left);
            Children.Add(tooltipElement);

            // border
            border.Name = "Border_" + optionName;
            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF91BAD5));
            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF91BAD5));
            border.BorderThickness = new Thickness(1, 1, 1, 1);
            border.CornerRadius = new CornerRadius(8, 8, 8, 8);
            border.HorizontalAlignment = HorizontalAlignment.Stretch;
            SetDock(border, Dock.Right);            
            Children.Add(border);
        }

        public virtual void Dispose()
        {
            tooltipElement.Dispose();
        }
    }

    public class HeaderElement : TextBlock
    {
        int marginL = 5;
        int marginT = 0;
        int marginR = 0;
        int marginB = 0;
        
        private int fontSize = 18;
        private FontWeight fontWeight = SystemFonts.CaptionFontWeight;

        public HeaderElement(string headerText)
        {
            this.Text = headerText;
            Margin = new Thickness(marginL, marginT, marginR, marginB);

            FontSize = fontSize;
            FontWeight = fontWeight;
            FontFamily = Design.AppFont;
            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF9C8A87));
        }
    }

    public class YesNoOptionElement : OptionUIElement
    {
        CheckBox checkBox;

        public YesNoOptionElement(string id, string optionName, string tooltip, bool value = false) : base(optionName, tooltip)
        {
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;

            checkBox = new CheckBox();
            checkBox.Name = "Checkbox_" + id.Replace('/', '_');
            checkBox.IsChecked = value;
            checkBox.HorizontalAlignment = HorizontalAlignment.Center;
            checkBox.VerticalAlignment = VerticalAlignment.Center;
            checkBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF91BAD5));
            checkBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF91BAD5));
            checkBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF0A2630));

            border.Child = checkBox;
        }

        public bool GetValue()
        {
            return (bool)checkBox.IsChecked;
        }
    }

    public class TextOptionElement : OptionUIElement
    {
        Border border;
        TextBox textBox;

        public TextOptionElement(string id, string optionName, string tooltip, string value = "") : base(optionName, tooltip)
        {
            // border
            border = new Border();
            border.Name = id.Replace('/', '_');
            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF91BAD5));
            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF91BAD5));
            border.BorderThickness = new Thickness(1, 1, 1, 1);
            border.CornerRadius = new CornerRadius(4, 4, 4, 4);
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;

            // textbox
            textBox = new TextBox();
            textBox.Name = "TextBox_" + id.Replace('/', '_');
            textBox.Text = value;
            textBox.Height = 30;
            textBox.Background = new SolidColorBrush(Colors.Transparent);
            textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF0A2630));
            textBox.FontFamily = Design.AppFont;
            textBox.UndoLimit = 8;
            textBox.BorderThickness = new Thickness(0);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;

            border.Child = textBox;

            //SetDock(border, Dock.Right);
            Children.Add(border);
        }

        public string GetValue()
        {
            return (string)textBox.Text;
        }
    }

    public class RealNumOptionElement : OptionUIElement
    {
        TextBox textBox = new TextBox();

        public RealNumOptionElement(string id, string optionName, string tooltip, string value = "0") : base(optionName, tooltip)
        {
            textBox.Name = "Textbox_" + id.Replace('/','_');
            textBox.Text = "" + value;

            textBox.BorderBrush = new SolidColorBrush(Colors.Transparent);
            textBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF91BAD5));
            textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF0A2630));
            
            textBox.FontFamily = Design.AppFont;

            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            
            textBox.Height = 30;

            textBox.UndoLimit = 8;

            // add elements
            border.Child = textBox;
        }

        public int GetValue()
        {
            return int.Parse(textBox.Text);
        }
    }

    public class DecimalNumOptionElement : OptionUIElement
    {
        TextBox textBox = new TextBox();

        public DecimalNumOptionElement(string id, string optionName, string tooltip, string value = "0.0") : base(optionName, tooltip)
        {
            // textbox
            textBox.Name = "Textbox_" + id.Replace('/', '_');
            textBox.Text = value;
            
            textBox.BorderBrush = new SolidColorBrush(Colors.Transparent);
            textBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF91BAD5));
            textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF0A2630));

            textBox.FontFamily = Design.AppFont;
            
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            
            textBox.Height = 30;
            
            textBox.UndoLimit = 8;

            // add elements
            border.Child = textBox;
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
            border.VerticalAlignment = VerticalAlignment.Center;

            colorPicker = new ColorPicker();
            colorPicker.Name = "ColorPicker_" + id.Replace('/', '_');
            colorPicker.SelectedColor = value;
            colorPicker.Width = 400;
            colorPicker.VerticalAlignment = VerticalAlignment.Center;
            colorPicker.HorizontalAlignment = HorizontalAlignment.Right;
            colorPicker.Background = new SolidColorBrush(Colors.Transparent);

            border.Child = colorPicker;
        }

        public Color GetValue()
        {
            return Colors.White;
        }
    }

    public class EnumOptionElement : OptionUIElement
    {
        ComboBox dropDown = new ComboBox();

        public EnumOptionElement(string id, string optionName, string tooltip, string[] enumOptions) : base(optionName, tooltip)
        {
            // dropdown
            dropDown.Name = "DropDown_" + id.Replace('/', '_');
            dropDown.ItemsSource = enumOptions;
            dropDown.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF0A2630));
            dropDown.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF91BAD5));
            dropDown.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dropDown.VerticalAlignment = VerticalAlignment.Center;
            dropDown.HorizontalAlignment = HorizontalAlignment.Stretch;
            dropDown.Height = 30;
            
            // add elements
            border.Child = dropDown;
        }

        public int GetValue()
        {
            return dropDown.SelectedIndex;
        }
    }

    public class GraphicOptionElement : DragDropGraphicElement
    {
        public GraphicOptionElement(string id, string optionName, string tooltip, string value = "") : base(new [] {".png", ".tif", ".jpg", ".jpeg"},id, optionName, tooltip, value)
        {  
            
        }

        public string GetValue()
        {
            return path;
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

    public class ArrayOptionElement : OptionUIElement
    {
        TextBox countTextBox = new TextBox();
        StackPanel contentElements = new StackPanel();
        OptionDataElement[] optionDataElements;

        public ArrayOptionElement(string id, string optionName, string tooltip, OptionDataElement[] optionDataElements) : base(optionName, tooltip)
        {
            this.optionDataElements = optionDataElements;
            countTextBox = new TextBox();
            countTextBox.Name = id.Replace('/', '_');
            countTextBox.Width = 30;
            countTextBox.TextChanged += new TextChangedEventHandler(PopulateContent);

            contentElements.Name = id.Replace('/', '_') + "_Content";

            border.Child = contentElements;
            Children.Add(countTextBox);
        }

        /// <summary>
        /// Adds the appropriate options to the stackpanel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void PopulateContent(object sender, TextChangedEventArgs args)
        {
            contentElements.Children.Clear();

            if(string.IsNullOrEmpty(countTextBox.Text))
            { 
                return;
            }

            int amount = 0;
            amount = int.Parse(countTextBox.Text);
            
            if (amount < 0)
            {
                amount = 0;
            }

            for (int i = 0; i < amount; i++)
            {
                foreach(var element in optionDataElements)
                {
                    switch (element.Option)
                    {
                        case SGGE.OPTION.COLOR:
                            contentElements.Children.Add(new ColorOptionElement(element.Path, element.Name, element.Tooltip, Colors.White));
                            break;
                        case SGGE.OPTION.YES_NO_OPTION:
                            contentElements.Children.Add(new YesNoOptionElement(element.Path, element.Name, element.Tooltip));
                            break;
                        case SGGE.OPTION.GRAPHICS:
                            contentElements.Children.Add(new GraphicOptionElement(element.Path, element.Name, element.Tooltip));
                            break;
                        case SGGE.OPTION.SOUND_FILE:
                            contentElements.Children.Add(new AudioOptionElement(element.Path, element.Name, element.Tooltip));
                            break;
                        case SGGE.OPTION.REAL_NUM:
                            contentElements.Children.Add(new RealNumOptionElement(element.Path, element.Name, element.Tooltip));
                            break;
                        case SGGE.OPTION.DECIMAL_NUM:
                            contentElements.Children.Add(new DecimalNumOptionElement(element.Path, element.Name, element.Tooltip));
                            break;
                        case SGGE.OPTION.ENUM:
                            EnumOptionDataElement eode = (EnumOptionDataElement)element;
                            contentElements.Children.Add(new EnumOptionElement(element.Path, element.Name, element.Tooltip, eode.EnumValues));
                            break;
                        case SGGE.OPTION.TEXT:
                            contentElements.Children.Add(new TextOptionElement(element.Path, element.Name, element.Tooltip));
                            break;
                        case SGGE.OPTION.ARRAY:
                            // currently not supported
                            break;
                    }
                }
            }
        }
    }

    public class DragDropGraphicElement : OptionUIElement
    {
        public string path = "";
        private StackPanel stackPanel = new StackPanel();
        private Border innerBorder = new Border();
        private TextBlock pathTextBlock = new TextBlock();
        private Image image = new Image();

        private string[] format;
        public DragDropGraphicElement(string[] format, string id, string optionName, string tooltip, string path) : base(optionName, tooltip)
        {
            if (!string.IsNullOrEmpty(path))
            {
                this.path = path;
            }
            this.format = format;
            this.Height = 184;

            //image.Source = 

            // inner border
            innerBorder.AllowDrop = true;
            innerBorder.Height = 123;
            innerBorder.Drop += DragAndDrop;

            // textblock
            pathTextBlock.Text = path;
            pathTextBlock.FontSize = 14;
            pathTextBlock.FontFamily = Design.AppFont;
            pathTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.FF0A2630));

            // add elements
            innerBorder.Child = image;

            stackPanel.Children.Add(innerBorder);
            stackPanel.Children.Add(pathTextBlock);

            border.Child = stackPanel;
        }

        public override void Dispose()
        {
            innerBorder.Drop -= DragAndDrop;
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

                pathTextBlock.Text = path;

                // copy the image into a resources folder
            }
        }
    }

    public class DragDropAudioElement : OptionUIElement
    {
        public string path;
        private StackPanel stackPanel = new StackPanel();
        private Border innerBorder = new Border();
        private TextBlock pathTextBlock = new TextBlock();
        private Image image = new Image();

        private string[] format;
        public DragDropAudioElement(string[] format, string id, string optionName, string tooltip, string path) : base(optionName, tooltip)
        {
            if (!string.IsNullOrEmpty(path))
            {
                this.path = path;
            }
            this.format = format;
            this.Height = 184;

            // inner border
            innerBorder.AllowDrop = true;
            innerBorder.Drop += DragAndDrop;
            innerBorder.Height = 123;
            innerBorder.Child = image;

            // path text block
            pathTextBlock.Text = path;

            // image
            //image.Source = 

            // add elements
            innerBorder.Child = image;

            stackPanel.Children.Add(innerBorder);
            stackPanel.Children.Add(pathTextBlock);

            border.Child = stackPanel;
        }

        public override void Dispose()
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

                pathTextBlock.Text = path;

                // copy file and save in a resource folder
            }
        }
    }

    public class Tooltip : Image, IDisposable
    {
        string tooltipText;
        public Tooltip(string tooltipText)
        {
            // Image
            this.tooltipText = tooltipText;
            Width = 30;
            Height = 30;
            HorizontalAlignment = HorizontalAlignment.Center;
            Margin = new Thickness(0,0,5,0);
            VerticalAlignment = VerticalAlignment.Center;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(Environment.CurrentDirectory + "/Resource/Tooltip.png");
            bitmap.EndInit();
            // Bitmap
            Source = bitmap;

            this.MouseLeftButtonDown += new MouseButtonEventHandler(OnClick);
        }

        void OnClick(object sender, MouseButtonEventArgs args)
        {
            System.Windows.MessageBox.Show(tooltipText, "Tooltip");
        }

        public void Dispose()
        {
            this.MouseLeftButtonDown -= new MouseButtonEventHandler(OnClick);
        }
    }
}
