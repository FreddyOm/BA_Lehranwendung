using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System;
using System.Windows;

namespace SeriousGameEngine.TemplateElemente
{
    /// <summary>
    /// A class that defines a category button
    /// </summary>
    internal class CategoryButton : TextBlock, IDisposable
    {
        static CategoryButton lastSelected = null;

        bool isSelected = false;

        public delegate void Click(object sender, RoutedEventArgs args);
        public event Click click;

        public bool HasEventHandler { get; set; }
        public FontFamily normalFont = new FontFamily("Sinkin Sans 300 Light");
        public FontFamily highlightedFont =new FontFamily("Sinkin Sans 500 Medium");
        public CategoryButton(string catName)
        {
            this.Name = catName;
            this.Text = catName;
            this.Height = 30;
            this.Margin = new Thickness(10);

            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Center;

            this.FontFamily = normalFont;
            this.FontSize = 14;

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));

            this.MouseEnter += new MouseEventHandler(MEnter);
            this.MouseLeave += new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);

        }

        /// <summary>
        /// Mouse entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MEnter(object sender, MouseEventArgs args )
        {
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Mouse exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MExit(object sender, MouseEventArgs args)
        {
            if(!isSelected)
                this.FontFamily = normalFont;
        }

        private void MDown(object sender, MouseButtonEventArgs args)
        {
            isSelected = true;
            click?.Invoke(sender, args);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
            }
            this.FontFamily = highlightedFont;
        }

        public void Deselect()
        {
            this.isSelected = false;
            this.FontFamily = normalFont;
        }

        public void Select()
        {
            isSelected = true;
            click?.Invoke(this, null);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
            }
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.MouseEnter -= new MouseEventHandler(MEnter);
            this.MouseLeave -= new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);
        }
    }

    internal class TemplateButton : TextBlock, IDisposable
    {
        static TemplateButton lastSelected = null;

        bool isSelected = false;

        public delegate void Click(object sender, RoutedEventArgs args);
        public event Click click;

        public bool HasEventHandler { get; set; }
        public FontFamily normalFont = new FontFamily("Sinkin Sans 300 Light");
        public FontFamily highlightedFont = new FontFamily("Sinkin Sans 500 Medium");
        public TemplateButton(string catName)
        {
            this.Name = catName;
            this.Text = catName;
            this.Height = 30;
            this.Margin = new System.Windows.Thickness(10);

            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            this.FontFamily = normalFont;
            this.FontSize = 14;

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));

            this.MouseEnter += new MouseEventHandler(MEnter);
            this.MouseLeave += new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);

        }

        /// <summary>
        /// Mouse entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MEnter(object sender, MouseEventArgs args)
        {
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Mouse exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MExit(object sender, MouseEventArgs args)
        {
            if (!isSelected)
                this.FontFamily = normalFont;
        }

        private void MDown(object sender, MouseButtonEventArgs args)
        {
            isSelected = true;
            click?.Invoke(sender, args);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
            }
            this.FontFamily = highlightedFont;
        }

        public void Deselect()
        {
            this.isSelected = false;
            this.FontFamily = normalFont;
        }

        public void Select()
        {
            isSelected = true;
            click?.Invoke(this, null);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
            }
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.MouseEnter -= new MouseEventHandler(MEnter);
            this.MouseLeave -= new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);
        }
    }

    internal class SubjectButton : TextBlock, IDisposable
    {
        static SubjectButton lastSelected = null;

        bool isSelected = false;

        public delegate void Click(object sender, RoutedEventArgs args);
        public event Click click;

        public bool HasEventHandler { get; set; }
        public FontFamily normalFont = new FontFamily("Sinkin Sans 300 Light");
        public FontFamily highlightedFont = new FontFamily("Sinkin Sans 500 Medium");
        public SubjectButton(string catName)
        {
            this.Name = catName;
            this.Text = catName;
            this.Height = 30;
            this.Margin = new System.Windows.Thickness(10);

            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            this.FontFamily = normalFont;
            this.FontSize = 14;

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));

            this.MouseEnter += new MouseEventHandler(MEnter);
            this.MouseLeave += new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);

        }

        /// <summary>
        /// Mouse entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MEnter(object sender, MouseEventArgs args)
        {
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Mouse exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MExit(object sender, MouseEventArgs args)
        {
            if (!isSelected)
                this.FontFamily = normalFont;
        }

        private void MDown(object sender, MouseButtonEventArgs args)
        {
            isSelected = true;
            click?.Invoke(sender, args);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
               
            }
            this.FontFamily = highlightedFont;
        }

        public void Deselect()
        {
            this.isSelected = false;
            this.FontFamily = normalFont;
        }

        public void Select()
        {
            isSelected = true;
            click?.Invoke(this, null);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
            }
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.MouseEnter -= new MouseEventHandler(MEnter);
            this.MouseLeave -= new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);
        }
    }

    internal class NavigationButton : TextBlock, IDisposable
    {
        public static NavigationButton lastSelected = null;

        bool isSelected = false;

        public delegate void Click(object sender, RoutedEventArgs args);
        public event Click click;

        public bool HasEventHandler { get; set; }
        public FontFamily normalFont = new FontFamily("Sinkin Sans 200 X Light");
        public FontFamily highlightedFont = new FontFamily("Sinkin Sans 500 Medium");
        public NavigationButton(string catName, int width)
        {
            this.Name = catName;
            this.Text = catName;
            this.Height = 30;
            this.FontFamily = normalFont;
            this.FontSize = 14;
            this.Width = width;
            this.Margin = new Thickness(20,5,0,0);

            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));

            this.MouseEnter += new MouseEventHandler(MEnter);
            this.MouseLeave += new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);

        }

        /// <summary>
        /// Mouse entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MEnter(object sender, MouseEventArgs args)
        {
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Mouse exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MExit(object sender, MouseEventArgs args)
        {
            if (!isSelected)
                this.FontFamily = normalFont;
        }

        private void MDown(object sender, MouseButtonEventArgs args)
        {
            isSelected = true;
            click?.Invoke(sender, args);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
            }
            this.FontFamily = highlightedFont;
        }

        public void Deselect()
        {
            this.isSelected = false;
            this.FontFamily = normalFont;
        }

        public void Select()
        {
            isSelected = true;
            click?.Invoke(this, null);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
            }
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.MouseEnter -= new MouseEventHandler(MEnter);
            this.MouseLeave -= new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);
        }
    }

    internal class NewGameButton : TextBlock, IDisposable
    {
        public static NewGameButton lastSelected = null;

        bool isSelected = false;

        public delegate void Click(object sender, RoutedEventArgs args);
        public event Click click;

        public bool HasEventHandler { get; set; }
        public FontFamily normalFont = new FontFamily("Sinkin Sans 300 Light");
        public FontFamily highlightedFont = new FontFamily("Sinkin Sans 500 Medium");
        public NewGameButton(string catName)
        {
            this.Name = catName.Replace(' ', '_');
            this.Text = catName;
            this.FontFamily = normalFont;
            this.FontSize = 12;
            this.Margin = new Thickness(10);

            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;
            

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));

            this.MouseEnter += new MouseEventHandler(MEnter);
            this.MouseLeave += new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);

        }

        /// <summary>
        /// Mouse entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MEnter(object sender, MouseEventArgs args)
        {
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Mouse exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MExit(object sender, MouseEventArgs args)
        {
            if (!isSelected)
                this.FontFamily = normalFont;
        }

        private void MDown(object sender, MouseButtonEventArgs args)
        {
            click?.Invoke(sender, args);
        }

        public void Deselect()
        {
            this.isSelected = false;
            this.FontFamily = normalFont;
        }

        public void Select()
        {
            isSelected = true;
            click?.Invoke(this, null);
            if(lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
            }
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.MouseEnter -= new MouseEventHandler(MEnter);
            this.MouseLeave -= new MouseEventHandler(MExit);
        }
    }

    internal class SideboardButton : TextBlock, IDisposable
    {
        public static SideboardButton lastSelected = null;

        bool isSelected = false;

        public delegate void Click(object sender, RoutedEventArgs args);
        public event Click click;

        public bool HasEventHandler { get; set; }
        public FontFamily normalFont = new FontFamily("Sinkin Sans 200 X Light");
        public FontFamily highlightedFont = new FontFamily("Sinkin Sans 500 Medium");
        public SideboardButton(string catName)
        {
            this.Name = catName;
            this.Text = catName;
            this.Height = 40;
            this.Margin = new System.Windows.Thickness(20,0,0,0);

            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            this.FontFamily = normalFont;
            this.FontSize = 16;

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));

            this.MouseEnter += new MouseEventHandler(MEnter);
            this.MouseLeave += new MouseEventHandler(MExit);
            this.PreviewMouseDown += new MouseButtonEventHandler(MDown);

        }

        /// <summary>
        /// Mouse entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MEnter(object sender, MouseEventArgs args)
        {
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Mouse exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MExit(object sender, MouseEventArgs args)
        {
            if (!isSelected)
                this.FontFamily = normalFont;
        }

        private void MDown(object sender, MouseButtonEventArgs args)
        {
            isSelected = true;
            click?.Invoke(sender, args);
            args.Handled = true;
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
                this.FontFamily = highlightedFont;
            }
        }

        public void Deselect()
        {
            this.isSelected = false;
            this.FontFamily = normalFont;
        }

        public void Select()
        {
            isSelected = true;
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
                this.FontFamily = highlightedFont;
            }
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.MouseEnter -= new MouseEventHandler(MEnter);
            this.MouseLeave -= new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);
        }
    }

    internal class SubjectElement: Border, IDisposable
    {
        static SubjectElement lastSelected = null;

        bool isSelected = false;

        public delegate void Click(object sender, RoutedEventArgs args);
        public event Click click;

        public bool HasEventHandler { get; set; }
        public FontFamily normalFont = new FontFamily("Sinkin Sans 300 Light");
        public FontFamily highlightedFont = new FontFamily("Sinkin Sans 500 Medium");

        private TextBlock text = new TextBlock();

        public SubjectElement(string catName)
        {
            this.Name = catName;
            text.Text = catName;
            this.Height = 122;
            this.Width = 180;
            this.Margin = new Thickness(10);
            this.BorderThickness = new Thickness(2);
            this.CornerRadius = new CornerRadius(8);

            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;

            text.FontFamily = normalFont;
            text.FontSize = 14;

            this.Background = new SolidColorBrush(Colors.Transparent);
            text.Background = new SolidColorBrush(Colors.Transparent);
            this.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C9E2F2"));
            text.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));

            text.VerticalAlignment = VerticalAlignment.Center;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            this.Child = text;

            this.MouseEnter += new MouseEventHandler(MEnter);
            this.MouseLeave += new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);

        }

        /// <summary>
        /// Mouse entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MEnter(object sender, MouseEventArgs args)
        {
            text.FontFamily = highlightedFont;
            this.BorderThickness = new Thickness(3);
        }

        /// <summary>
        /// Mouse exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MExit(object sender, MouseEventArgs args)
        {
            if (!isSelected)
            {
                text.FontFamily = normalFont;
                this.BorderThickness = new Thickness(2);
            }
                
        }

        private void MDown(object sender, MouseButtonEventArgs args)
        {
            isSelected = true;
            click?.Invoke(sender, args);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
                text.FontFamily = highlightedFont;
            }
        }

        public void Deselect()
        {
            this.isSelected = false;
            text.FontFamily = normalFont;
            this.BorderThickness = new Thickness(2);
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.MouseEnter -= new MouseEventHandler(MEnter);
            this.MouseLeave -= new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);
        }
    }

    internal class TemplateElement : Border, IDisposable
    {
        static TemplateElement lastSelected = null;

        bool isSelected = false;

        public delegate void Click(object sender, RoutedEventArgs args);
        public event Click click;

        public bool HasEventHandler { get; set; }
        public FontFamily normalFont = new FontFamily("Sinkin Sans 300 Light");
        public FontFamily highlightedFont = new FontFamily("Sinkin Sans 500 Medium");

        private TextBlock headerText = new TextBlock();
        private TextBlock descriptionText = new TextBlock();
        private DockPanel dockPanel = new DockPanel();
        private Border preview = new Border();

        public TemplateElement(string templateName, string description_text)
        {
            this.Height = 260;
            this.Width = 260;
            this.Margin = new Thickness(8);
            this.BorderThickness = new Thickness(2);
            this.CornerRadius = new CornerRadius(8);

            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;
            this.Background = new SolidColorBrush(Colors.Transparent);
            this.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C9E2F2"));
            this.Child = dockPanel;

            #region header Text

            headerText.Text = templateName;
            headerText.FontFamily = normalFont;
            headerText.FontSize = 14;

            headerText.Background = new SolidColorBrush(Colors.Transparent);
            headerText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));

            headerText.VerticalAlignment = VerticalAlignment.Center;
            headerText.HorizontalAlignment = HorizontalAlignment.Center;

            #endregion header Text

            #region description Text
            
            descriptionText.Text = description_text;
            descriptionText.FontFamily = new FontFamily("Sinkin Sans 400 Light");
            descriptionText.FontSize = 10;
            descriptionText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B7BABA"));
            descriptionText.Margin = new Thickness(8);
            #endregion description Text

            #region preview

            preview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));
            preview.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C9E2F2"));
            preview.BorderThickness = new Thickness(2);
            preview.CornerRadius = new CornerRadius(8);
            preview.Margin = new Thickness(8, 0, 8, 10);
            preview.Height = 120;
            preview.VerticalAlignment = VerticalAlignment.Bottom;
            #endregion preview

            DockPanel.SetDock(headerText, Dock.Top);
            DockPanel.SetDock(descriptionText, Dock.Top);
            DockPanel.SetDock(preview, Dock.Bottom);

            dockPanel.Children.Add(headerText);
            dockPanel.Children.Add(descriptionText);
            dockPanel.Children.Add(preview);

            this.MouseEnter += new MouseEventHandler(MEnter);
            this.MouseLeave += new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);
        }

        /// <summary>
        /// Mouse entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MEnter(object sender, MouseEventArgs args)
        {
            headerText.FontFamily = highlightedFont;
            this.BorderThickness = new Thickness(3);
        }

        /// <summary>
        /// Mouse exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MExit(object sender, MouseEventArgs args)
        {
            if (!isSelected)
            {
                headerText.FontFamily = normalFont;
                this.BorderThickness = new Thickness(2);
            }

        }

        private void MDown(object sender, MouseButtonEventArgs args)
        {
            isSelected = true;
            click?.Invoke(sender, args);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
                headerText.FontFamily = highlightedFont;
            }
        }

        public void Deselect()
        {
            this.isSelected = false;
            headerText.FontFamily = normalFont;
            this.BorderThickness = new Thickness(2);
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.MouseEnter -= new MouseEventHandler(MEnter);
            this.MouseLeave -= new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);
        }
    }

}
