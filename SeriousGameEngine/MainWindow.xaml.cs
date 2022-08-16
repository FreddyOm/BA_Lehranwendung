﻿using System;
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
using SeriousGameEngine.TemplateElemente;
using SeriousGameEngine.CMS;

namespace SeriousGameEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Grid[] screens = new Grid[6];
        private Border[] subjects = new Border[4];
        private Border[] gameTemplates = new Border[6];

        private StackPanel options;
        private StackPanel categories;

        private SGGEDataManager content;

        public MainWindow()
        {
            InitializeComponent();

            options = Options_Panel;
            categories = Grid_Categories_Menu;
            
            InitScreens();
            InitSubjects();
            InitGameTemplates();

            SetScreen(SCREEN.HOME);
            
            content = new SGGEDataManager();

            var element = content.GetElement("Logik/Kategorien/Anzahl");
            element?.ToString();

            //CreateOption();
        }

        #region buttons

        public void OnCategoryButtonClicked(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            LoadOptions((string)button.Content);
        }

        #region navigation

        private void Button_Homescreen_Click(object sender, RoutedEventArgs e)
        {
            SetScreen(SCREEN.HOME);
        }

        private void Button_Subjetcs_Click(object sender, RoutedEventArgs e)
        {
            SetScreen(SCREEN.SUBJECT);
            SetSubject(SUBJECT.SCIENCE);
        }

        private void Button_Templates_Click(object sender, RoutedEventArgs e)
        {
            SetScreen(SCREEN.TEMPLATE);
        }

        private void Button_Modify_Click(object sender, RoutedEventArgs e)
        {
            SetScreen(SCREEN.MODIFY);
        }

        private void Button_Export_Click(object sender, RoutedEventArgs e)
        {
            SetScreen(SCREEN.EXPORT);
        }

        private void Button_Konto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion navigation

        #region subjects

        private void Button_Sciene_Click(object sender, RoutedEventArgs e)
        {
            SetSubject(SUBJECT.SCIENCE);
        }

        private void Button_Languages_Click(object sender, RoutedEventArgs e)
        {
            SetSubject(SUBJECT.LANGUAGE);
        }

        private void Button_Politic_Click(object sender, RoutedEventArgs e)
        {
            SetSubject(SUBJECT.POLITICS);
        }

        private void Button_Favourits_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Further_Click(object sender, RoutedEventArgs e)
        {
            SetSubject(SUBJECT.FURTHER);
        }

        #endregion subjects

        #region templates

        private void Button_Action_Click(object sender, RoutedEventArgs e)
        {
            SetGameTemplateCategory(TEMPLATE_CATEGORY.ACTION);
        }

        private void Button_Adventure_Click(object sender, RoutedEventArgs e)
        {
            SetGameTemplateCategory(TEMPLATE_CATEGORY.ADVENTURE);
        }

        private void Button_Puzzle_Click(object sender, RoutedEventArgs e)
        {
            SetGameTemplateCategory(TEMPLATE_CATEGORY.PUZZLE);
        }

        private void Button_Simulation_Click(object sender, RoutedEventArgs e)
        {
            SetGameTemplateCategory(TEMPLATE_CATEGORY.SIMULATION);
        }

        private void Button_Roleplay_Click(object sender, RoutedEventArgs e)
        {
            SetGameTemplateCategory(TEMPLATE_CATEGORY.RPG);
        }

        private void Button_TemplatesFavorits_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion templates

        private void Button_NewGame_Click(object sender, RoutedEventArgs e)
        {
            SetScreen(SCREEN.SUBJECT);
        }

        private void Button_Profil_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Opt_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion buttons

        #region screens

        private void InitScreens()
        {
            screens[0] = Grid_MenuSettings;
            screens[1] = Grid_Homescreen;
            screens[2] = Grid_Subjectscreen;
            screens[3] = Grid_Templatescreen;
            screens[4] = Grid_Modifyscreen;
            screens[5] = Grid_Exportscreen;
        }

        private void SetScreen(SCREEN screen)
        {
            foreach(Grid grid in screens)
            {
                grid.Visibility = Visibility.Hidden;
            }

            switch(screen)
            {
                case SCREEN.SUBJECT:
                    screens[(int)SCREEN.SUBJECT].Visibility = Visibility.Visible;
                    screens[(int)SCREEN.MENUSETTINGS].Visibility = Visibility.Visible;
                    SetSubject(SUBJECT.SCIENCE);
                    break;
                case SCREEN.TEMPLATE:
                    screens[(int)SCREEN.TEMPLATE].Visibility = Visibility.Visible;
                    screens[(int)SCREEN.MENUSETTINGS].Visibility = Visibility.Visible;
                    SetGameTemplateCategory(TEMPLATE_CATEGORY.ACTION);
                    break;
                case SCREEN.MODIFY:
                    screens[(int)SCREEN.MODIFY].Visibility = Visibility.Visible;
                    screens[(int)SCREEN.MENUSETTINGS].Visibility = Visibility.Visible;
                    FillCategories();
                    break;
                case SCREEN.EXPORT:
                    screens[(int)SCREEN.EXPORT].Visibility = Visibility.Visible;
                    screens[(int)SCREEN.MENUSETTINGS].Visibility = Visibility.Visible;
                    break;
                default:
                    //HOME
                    screens[(int)SCREEN.HOME].Visibility = Visibility.Visible;
                    screens[(int)SCREEN.MENUSETTINGS].Visibility = Visibility.Visible;
                    break;
            }
        }

        #endregion screens

        #region subjects

        private void InitSubjects()
        {
            subjects[0] = Border_SubjectsScience;
            subjects[1] = Border_SubjectsLanguages;
            subjects[2] = Border_SubjectsPolitics;
            subjects[3] = Border_SubjectsFurther;
        }

        private void SetSubject(SUBJECT subject)
        {
            foreach(Border sub in subjects)
            { 
                sub.Visibility = Visibility.Hidden;
            }

            subjects[(int)subject].Visibility = Visibility.Visible;
        }

        #endregion subjects

        #region game templates

        private void InitGameTemplates()
        {
            gameTemplates[0] = Border_TemplateAction;
            gameTemplates[1] = Border_TemplateAdventure;
            gameTemplates[2] = Border_TemplatePuzzle;
            gameTemplates[3] = Border_TemplateSimulation;
            gameTemplates[4] = Border_TemplateRPG;
            gameTemplates[5] = Border_TemplateFavourites;
        }

        private void SetGameTemplateCategory(TEMPLATE_CATEGORY cat)
        {
            foreach(Border category in gameTemplates)
            {
                category.Visibility = Visibility.Hidden;
            }

            gameTemplates[(int)cat].Visibility = Visibility.Visible;
        }


        #endregion game templates


        #region cms

        #region category

        public void FillCategories()
        {
            //Remove delegates and delete all existing buttons
            for(int i = 0; i < Category_Buttons.Children.Count; i++)
            {
                if(Category_Buttons.Children[i] is CategoryButton)
                {
                    CategoryButton b = Category_Buttons.Children[i] as CategoryButton;
                    if (b.HasEventHandler)
                        b.Click -= new RoutedEventHandler(OnCategoryButtonClicked);
                }
            }

            Category_Buttons.Children.RemoveRange(0, Category_Buttons.Children.Count);

            //Rebuild all categories
            foreach(var e in content.GetAllCategories())
            {
                CategoryButton button = new CategoryButton(e.Category);
                button.Click += new RoutedEventHandler(OnCategoryButtonClicked);
                button.HasEventHandler = true;
                Category_Buttons.Children.Add(button);
            }
        }

        #endregion category

        #region options 

        private void LoadOptions(string category)
        {
            options.Children.RemoveRange(0, options.Children.Count);

            OptionDataElement[] categoryElements = content.GetElementsOfCategory(category);

            foreach(var element in categoryElements)
            {
                CreateOption(element);
            }
        }

        #endregion options

        #endregion cms

        private void CreateOption(OptionDataElement ode)
        {
            switch(ode.Option)
            {
                case SGGE.OPTION.COLOR:
                    options.Children.Add(new ColorOptionElement(ode.Path, ode.Name, ode.Tooltip, Colors.White));
                    break;
                case SGGE.OPTION.YES_NO_OPTION:
                    options.Children.Add(new YesNoOptionElement(ode.Path, ode.Name, ode.Tooltip));
                    break;
                case SGGE.OPTION.GRAPHICS:
                    options.Children.Add(new GraphicOptionElement(ode.Path, ode.Name, ode.Tooltip));
                    break;
                case SGGE.OPTION.SOUND_FILE:
                    options.Children.Add(new AudioOptionElement(ode.Path, ode.Name, ode.Tooltip));
                    break;
                case SGGE.OPTION.REAL_NUM:
                    options.Children.Add(new RealNumOptionElement(ode.Path, ode.Name, ode.Tooltip));
                    break;
                case SGGE.OPTION.DECIMAL_NUM:
                    options.Children.Add(new DecimalNumOptionElement(ode.Path, ode.Name, ode.Tooltip));
                    break;
                case SGGE.OPTION.ENUM:
                    EnumOptionDataElement eode = (EnumOptionDataElement)ode;
                    options.Children.Add(new EnumOptionElement(ode.Path, ode.Name, ode.Tooltip, eode.EnumValues));
                    break;
                case SGGE.OPTION.TEXT:
                    options.Children.Add(new TextOptionElement(ode.Path, ode.Name, ode.Tooltip));
                    break;
                case SGGE.OPTION.ARRAY:
                    break;
            }       
        }
    }

    public enum SCREEN
    {
        MENUSETTINGS = 0,
        HOME = 1,
        SUBJECT = 2,
        TEMPLATE = 3,
        MODIFY = 4,
        EXPORT = 5
    }
    public enum SUBJECT
    {
        SCIENCE = 0,
        LANGUAGE = 1,
        POLITICS = 2,
        FURTHER = 3,
    }
    public enum TEMPLATE_CATEGORY
    { 
        ACTION = 0,
        ADVENTURE = 1,
        PUZZLE = 2,
        SIMULATION = 3,
        RPG = 4
    }

}
