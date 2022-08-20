using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Xceed.Wpf.Toolkit;
using SeriousGameEngine.CMS;
using System.Linq;
using SGGE;
using System.IO;
using System.Collections.Generic;

namespace SeriousGameEngine.TemplateElemente
{
    /// <summary>
    /// The base class for every UI element
    /// </summary>
    public class OptionUIElement : DockPanel, IDisposable
    {
        public delegate void OnValueChanged(string optionID, OptionUIElement value, OPTION option);
        public static event OnValueChanged onValueChanged;

        string id;

        int marginL = 10;
        int marginT = 10;
        int marginR = 10;
        int marginB = 0;

        private float width = 200.0f;
        public Border border = new Border();
        Tooltip tooltipElement;
        public OptionUIElement(string id, string optionName, string tooltip)
        {
            this.id = id;
            // DockPanel
            Name = optionName;
            Margin = new Thickness(marginL, marginT, marginR, marginB);
            //Height = height;
            //SetDock(this, Dock.Top);
            LastChildFill = true;

            //TextBlock
            TextBlock textName = new TextBlock();
            textName.Text = optionName;
            textName.Width = width;
            //textName.Height = height;
            textName.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design._3D77B1));
            textName.HorizontalAlignment = HorizontalAlignment.Left;
            textName.VerticalAlignment = VerticalAlignment.Top;

            SetDock(textName, Dock.Left);
            Children.Add(textName);

            // Tooltip Image
            tooltipElement = new Tooltip(tooltip);
            SetDock(tooltipElement, Dock.Left);
            Children.Add(tooltipElement);

            // border
            border.Name = "Border_" + optionName;
            border.Background = new SolidColorBrush(Colors.Transparent);
            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.C9E2F2));
            border.BorderThickness = new Thickness(2);
            border.CornerRadius = new CornerRadius(4);
            border.HorizontalAlignment = HorizontalAlignment.Stretch;
            SetDock(border, Dock.Right);            
            Children.Add(border);
        }

        public virtual void Dispose()
        {
            tooltipElement.Dispose();
        }

        public void ElementValueChanged( OptionUIElement element, OPTION option)
        {
            onValueChanged?.Invoke(id, element, option);
        }
    }

    /// <summary>
    /// The UI element that describes a header for a collection of options
    /// </summary>
    public class HeaderElement : TextBlock
    {
        int marginL = 5;
        int marginT = 10;
        int marginR = 0;
        int marginB = 5;
        
        private int fontSize = 14;
        private FontWeight fontWeight = SystemFonts.CaptionFontWeight;

        public HeaderElement(string headerText)
        {
            this.Text = headerText;
            Margin = new Thickness(marginL, marginT, marginR, marginB);

            FontSize = fontSize;
            FontWeight = fontWeight;
            FontFamily = new FontFamily("Sinkin Sans 300 Light");
            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design._3D77B1));
        }
    }

    /// <summary>
    /// The UI element that describes a yes or no option
    /// </summary>
    public class YesNoOptionElement : OptionUIElement
    {
        CheckBox checkBox = new CheckBox();

        public YesNoOptionElement(string id, string optionName, string tooltip, bool value = false) : base(id, optionName, tooltip)
        {
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;

            checkBox.Name = "Checkbox_" + id.Replace('/', '_');
            checkBox.IsChecked = value;
            checkBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            checkBox.VerticalAlignment = VerticalAlignment.Center;
            checkBox.Background = new SolidColorBrush(Colors.Transparent);
            checkBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.C9E2F2));
            checkBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.C9E2F2));
            checkBox.Margin = new Thickness(350,5,15,5);
            border.Child = checkBox;

            checkBox.Click += new RoutedEventHandler(ValueChanged);
        }
        private void ValueChanged(object sender, RoutedEventArgs args)
        {
            ElementValueChanged(this, OPTION.YES_NO_OPTION);
        }

        public bool GetValue()
        {
            return (bool)checkBox.IsChecked;
        }

        public override void Dispose()
        {
            checkBox.Click -= new RoutedEventHandler(ValueChanged);
            base.Dispose();
        }
    }

    /// <summary>
    /// The UI element that describes a text
    /// </summary>
    public class TextOptionElement : OptionUIElement
    {
        TextBox textBox;

        public TextOptionElement(string id, string optionName, string tooltip, string value = "") : base(id, optionName, tooltip)
        {
            // textbox
            textBox = new TextBox();
            textBox.Name = "TextBox_" + id.Replace('/', '_');
            textBox.Text = value;
            textBox.Height = 30;
            textBox.Background = new SolidColorBrush(Colors.Transparent);
            textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design._3D77B1));
            textBox.FontFamily = new FontFamily("Sinkin Sans 200 X Light");
            textBox.UndoLimit = 8;
            textBox.BorderThickness = new Thickness(0);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.Margin = new Thickness(2, 2, 2, 2);

            textBox.TextChanged += new TextChangedEventHandler(ValueChanged);

            border.Child = textBox;

        }
        private void ValueChanged(object sender, TextChangedEventArgs args)
        {
            ElementValueChanged(this, OPTION.TEXT);
        }
        public string GetValue()
        {
            return (string)textBox.Text;
        }

        public override void Dispose()
        {
            textBox.TextChanged -= new TextChangedEventHandler(ValueChanged);
            base.Dispose();
        }
    }

    /// <summary>
    /// The UI element that describes a real number
    /// </summary>
    public class RealNumOptionElement : OptionUIElement
    {
        TextBox textBox = new TextBox();

        public RealNumOptionElement(string id, string optionName, string tooltip, int value = 0) : base(id, optionName, tooltip)
        {
            textBox.Name = "Textbox_" + id.Replace('/','_');
            textBox.Text = "" + value;

            textBox.BorderBrush = new SolidColorBrush(Colors.Transparent);
            textBox.Background = new SolidColorBrush(Colors.Transparent);
            textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));
            
            textBox.FontFamily = new FontFamily("Sinkin Sans 200 X Light");

            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            
            textBox.Height = 30;
            textBox.Margin = new Thickness(2,2,2,2);

            textBox.UndoLimit = 8;

            textBox.TextChanged += new TextChangedEventHandler(ValueChanged);

            // add elements
            border.Child = textBox;
        }

        private void ValueChanged(object sender, TextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(textBox.Text)) { return; }

            ElementValueChanged(this, OPTION.REAL_NUM);
        }

        public int GetValue()
        {
            int num;
            try
            {
                num = int.Parse(textBox.Text);
            }
            catch
            {
                num = 0;
            }
            return num;
        }

        public override void Dispose()
        {
            textBox.TextChanged -= new TextChangedEventHandler(ValueChanged);
            base.Dispose();
        }
    }

    /// <summary>
    /// The UI element that describes a decimal number
    /// </summary>
    public class DecimalNumOptionElement : OptionUIElement
    {
        TextBox textBox = new TextBox();

        public DecimalNumOptionElement(string id, string optionName, string tooltip, float value = 0.0f) : base(id, optionName, tooltip)
        {
            // textbox
            textBox.Name = "Textbox_" + id.Replace('/', '_');
            textBox.Text = "" + value;
            
            textBox.BorderBrush = new SolidColorBrush(Colors.Transparent);
            textBox.Background = new SolidColorBrush(Colors.Transparent);
            textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));

            textBox.FontFamily = new FontFamily("Sinkin Sans 200 X Light");

            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            
            textBox.Height = 30;
            textBox.Margin = new Thickness(2,2,2,2);
            
            textBox.UndoLimit = 8;

            textBox.TextChanged += new TextChangedEventHandler(ValueChanged);

            // add elements
            border.Child = textBox;
        }

        private void ValueChanged(object sender, TextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(textBox.Text)) { return; }

            ElementValueChanged(this, OPTION.DECIMAL_NUM);
        }

        //Get the custom set value
        public float GetValue()
        {
            float num;
            try
            {
                num  = float.Parse(textBox.Text);
            }
            catch
            {
                num = 0;
            }
            return num;
        }

        public override void Dispose()
        {
            textBox.TextChanged -= new TextChangedEventHandler(ValueChanged);
            base.Dispose();
        }
    }

    /// <summary>
    /// The UI element that describes a color
    /// </summary>
    public class ColorOptionElement : OptionUIElement
    {
        ColorPicker colorPicker;

        public ColorOptionElement(string id, string optionName, string tooltip, Color value) : base(id, optionName, tooltip)
        {
            border.VerticalAlignment = VerticalAlignment.Center;

            colorPicker = new ColorPicker();
            colorPicker.Name = "ColorPicker_" + id.Replace('/', '_');
            colorPicker.SelectedColor = value;
            colorPicker.Width = 400;
            colorPicker.VerticalAlignment = VerticalAlignment.Center;
            colorPicker.HorizontalAlignment = HorizontalAlignment.Right;
            colorPicker.Background = new SolidColorBrush(Colors.Transparent);
            colorPicker.Margin = new Thickness(2);

            colorPicker.SelectedColorChanged += new RoutedPropertyChangedEventHandler<Color?>(ValueChanged);

            border.Child = colorPicker;
        }

        private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<Color?> args)
        {
            ElementValueChanged(this, OPTION.COLOR);
        }

        public Color GetValue()
        {
            Color c;
            try
            {
                c = (Color)colorPicker.SelectedColor;
            }
            catch
            {
                c = Colors.White;
            }

            return c;
        }

        public override void Dispose()
        {
            colorPicker.SelectedColorChanged -= new RoutedPropertyChangedEventHandler<Color?>(ValueChanged);
            base.Dispose();
        }
    }

    /// <summary>
    /// The UI element that describes an enum
    /// </summary>
    public class EnumOptionElement : OptionUIElement
    {
        ComboBox dropDown = new ComboBox();

        public EnumOptionElement(string id, string optionName, string tooltip, string[] enumOptions, int value = 0) : base(id, optionName, tooltip)
        {
            // dropdown
            dropDown.Name = "DropDown_" + id.Replace('/', '_');
            dropDown.ItemsSource = enumOptions;
            dropDown.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design._3D77B1));
            dropDown.Background = new SolidColorBrush(Colors.Transparent);
            dropDown.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dropDown.VerticalAlignment = VerticalAlignment.Center;
            dropDown.HorizontalAlignment = HorizontalAlignment.Stretch;
            dropDown.VerticalContentAlignment = VerticalAlignment.Center;
            dropDown.Height = 30;
            dropDown.Margin = new Thickness(2);
            dropDown.FontFamily = new FontFamily("Sinkin Sans 200 X Light");
            dropDown.SelectedIndex = value;

            dropDown.SelectionChanged += new SelectionChangedEventHandler(ValueChanged);
            // add elements
            border.Child = dropDown;
        }

        private void ValueChanged(object sender, SelectionChangedEventArgs args)
        {
            ElementValueChanged(this, OPTION.ENUM);
        }

        public int GetValue()
        {
            return dropDown.SelectedIndex;
        }

        public override void Dispose()
        {
            dropDown.SelectionChanged -= new SelectionChangedEventHandler(ValueChanged);
            base.Dispose();
        }
    }

    /// <summary>
    /// The UI element that describes an image
    /// </summary>
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

    /// <summary>
    /// The UI element that describes an audio file
    /// </summary>
    public class AudioOptionElement : DragDropAudioElement
    {
        public AudioOptionElement(string id, string optionName, string tooltip, string value = "") : base(new[] { ".wav", ".mp3" }, id, optionName, tooltip, value)
        { }

        public string GetValue()
        {
            return path;
        }
    }

    /// <summary>
    /// The UI element that describes an array
    /// </summary>
    public class ArrayOptionElement : OptionUIElement
    {
        TextBox countTextBox = new TextBox();
        TextBlock showHideText = new TextBlock();
        StackPanel contentElements = new StackPanel();
        OptionDataElement[] optionDataElements;
        Grid spacing;
        CheckBox checkBox = new CheckBox();
        private int maxElements = 99;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="optionName"></param>
        /// <param name="tooltip"></param>
        /// <param name="optionDataElements"></param>
        public ArrayOptionElement(string id, string optionName, string tooltip, OptionDataElement[] optionDataElements, int amount, List<OptionValue[]> values = null) : base(id, optionName, tooltip)
        {
            this.optionDataElements = optionDataElements;
            countTextBox.Name = id.Replace('/', '_');
            countTextBox.Width = 30;
            countTextBox.Height = 26;
            countTextBox.TextChanged += new TextChangedEventHandler(PopulateContent);
            countTextBox.TextChanged += new TextChangedEventHandler(ValueChanged);
            countTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            countTextBox.HorizontalAlignment = HorizontalAlignment.Right;
            countTextBox.FontFamily = new FontFamily("Sinkin Sans 200 X Light");
            countTextBox.Text = "" + amount;

            contentElements.Name = id.Replace('/', '_') + "_Content";
            border.BorderThickness = new Thickness(2);
            border.Background = new SolidColorBrush(Colors.Transparent);
            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.C9E2F2));
            border.Margin = new Thickness(0,2,0,0);

            checkBox.IsChecked = true;
            checkBox.VerticalAlignment = VerticalAlignment.Center;
            checkBox.HorizontalAlignment = HorizontalAlignment.Center;

            checkBox.Click += new RoutedEventHandler(ShowHideArrayOptions);

            showHideText.Text = "Anzeigen: ";
            showHideText.FontFamily = new FontFamily("Sinkin Sans 200 X Light");
            showHideText.VerticalAlignment = VerticalAlignment.Center;
            showHideText.HorizontalAlignment= HorizontalAlignment.Left;
            showHideText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design._3D77B1));

            SetDock(border, Dock.Bottom);

            border.Child = contentElements;
            Children.Add(showHideText);
            Children.Add(checkBox);
            Children.Add(countTextBox);
        }

        /// <summary>
        /// Update array when checkbox is (un-)checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ShowHideArrayOptions(object sender, RoutedEventArgs args)
        {
            PopulateContent(null, null);
        }

        /// <summary>
        /// Adds the appropriate options to the stackpanel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void PopulateContent(object sender, TextChangedEventArgs args)
        {
            // dispose elements to avoid memory leak and clear them afterwards
            foreach(var element in contentElements.Children)
            {
                if(element is OptionUIElement)
                {
                    OptionUIElement e = element as OptionUIElement;
                    e.Dispose();
                }                
            }

            contentElements.Children.Clear();

            // return if checkbox is not checked
            if (!(bool)checkBox.IsChecked) { return; }

            // no valid inputs
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
            else if(amount > maxElements)
            {
                System.Windows.MessageBox.Show($"Es werden maximal {maxElements} Elemente unterstützt.\nGebe eine Anzahl zwischen 0 und {maxElements} an.");
                amount = 0;
            }

            // show elements
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
                // add spacing 
                    spacing = new Grid();
                    spacing.Height = 1;
                    spacing.HorizontalAlignment = HorizontalAlignment.Stretch;

                    spacing.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design.C9E2F2));
                    spacing.Margin = new Thickness(5, 10, 5, 0);

                    contentElements.Children.Add(spacing);
            }
        }

        private void ValueChanged(object sender, TextChangedEventArgs args)
        {
            ElementValueChanged(this, OPTION.ARRAY);
        }

        /// <summary>
        /// Unsubscribe events
        /// </summary>
        public override void Dispose()
        {
            countTextBox.TextChanged -= new TextChangedEventHandler(PopulateContent);
            checkBox.Click -= new RoutedEventHandler(ShowHideArrayOptions);
            countTextBox.TextChanged -= new TextChangedEventHandler(ValueChanged);
            base.Dispose();
        }
    }

    /// <summary>
    /// The UI element that describes an image
    /// </summary>
    public class DragDropGraphicElement : OptionUIElement
    {
        public string path = "";
        private StackPanel stackPanel = new StackPanel();
        private Border innerBorder = new Border();
        private TextBlock pathTextBlock = new TextBlock();
        private Image image = new Image();

        private string[] format;
        public DragDropGraphicElement(string[] format, string id, string optionName, string tooltip, string path) : base(id, optionName, tooltip)
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
            innerBorder.Background = new SolidColorBrush(Colors.Transparent);

            // textblock
            FileInfo fileInfo = new FileInfo(path);
            pathTextBlock.Text = fileInfo.Name;
            pathTextBlock.FontSize = 14;
            pathTextBlock.FontFamily = new FontFamily("Sinkin Sans 200 X Light");
            pathTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design._3D77B1));
            pathTextBlock.HorizontalAlignment = HorizontalAlignment.Center;

            // add elements
            innerBorder.Child = image;

            stackPanel.Children.Add(innerBorder);
            stackPanel.Children.Add(pathTextBlock);

            border.Child = stackPanel;
        }

        public override void Dispose()
        {
            innerBorder.Drop -= DragAndDrop;
            base.Dispose();
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

                FileInfo f = new FileInfo(path);
                pathTextBlock.Text = f.Name;

                // copy the image into a resources folder
                ElementValueChanged(this, OPTION.GRAPHICS);

                File.Copy(path, SaveUtility.RESOURCE_PATH + "/" + f.Name);
            }
        }
    }

    /// <summary>
    /// The UI element that describes an adio file
    /// </summary>
    public class DragDropAudioElement : OptionUIElement
    {
        public string path;
        private StackPanel stackPanel = new StackPanel();
        private Border innerBorder = new Border();
        private TextBlock pathTextBlock = new TextBlock();
        private Image image = new Image();

        private string[] format;
        public DragDropAudioElement(string[] format, string id, string optionName, string tooltip, string path) : base(id, optionName, tooltip)
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
            innerBorder.Background = new SolidColorBrush(Colors.Transparent);

            // path text block
            FileInfo fileInfo = new FileInfo(path);
            pathTextBlock.Text = fileInfo.Name;
            pathTextBlock.FontSize = 14;
            pathTextBlock.FontFamily = new FontFamily("Sinkin Sans 200 X Light");
            pathTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Design._3D77B1));
            pathTextBlock.HorizontalAlignment = HorizontalAlignment.Center;

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
            base.Dispose();
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
                FileInfo f = new FileInfo(path);
                pathTextBlock.Text = f.Name;

                // copy file and save in a resource folder
                ElementValueChanged(this, OPTION.SOUND_FILE);

                File.Copy(path, SaveUtility.RESOURCE_PATH + "/" + f.Name);
            }
        }
    }

    /// <summary>
    /// The tooltip for every option ui element
    /// </summary>
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
            VerticalAlignment = VerticalAlignment.Top;

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
