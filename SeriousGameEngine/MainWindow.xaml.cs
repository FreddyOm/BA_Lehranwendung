using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SeriousGameEngine.TemplateElemente;
using SeriousGameEngine.CMS;
using System.ComponentModel;
using System.Collections.Generic;
using SGGE;

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

        private SGGEDataManager content;
        private SaveUtility saveUtility;

        NewGameButton newGameButton;

        #region sideboard buttons

        SideboardButton homeButton;
        SideboardButton settingsButton;
        SideboardButton userButton;

        #endregion sideboard buttons

        #region template buttons

        TemplateButton actionButton;
        TemplateButton adventureButton;
        TemplateButton puzzleButton;
        TemplateButton simulationButton;
        TemplateButton rpgButton;
        TemplateButton favButton;

        #endregion template buttons

        #region subject buttons

        SubjectButton scienceButton;
        SubjectButton langButton;
        SubjectButton politicButton;
        SubjectButton furtherButton;
        SubjectButton favSubjectButton;

        #endregion subject buttons

        #region navigation buttons

        NavigationButton subjectsNavButton;
        NavigationButton templatesNavButton;
        NavigationButton modifyNavButton;
        NavigationButton exportNavButton;

        #endregion navigation buttons

        #region template elements

        TemplateElement jumpNRunElement;
        TemplateElement raceElement;
        TemplateElement conveyorBeltElement;

        TemplateElement laraCroftElement;
        TemplateElement adventureGameElement;

        TemplateElement tetrisElement;
        TemplateElement portalsElement;

        TemplateElement farmSimulatorElement;
        TemplateElement reignCntryElement;

        TemplateElement RPG_Element;

        #endregion template elements

        #region subject elements

        SubjectElement mathElement;
        SubjectElement physicsElement;
        SubjectElement chemistryElement;
        SubjectElement biologyElement;
        SubjectElement informaticsElement;

        SubjectElement germanElement;
        SubjectElement englishElement;
        SubjectElement frenchElement;
        SubjectElement spanishElement;
        SubjectElement latinElement;

        SubjectElement historyElement;
        SubjectElement politicsElement;
        SubjectElement ethicsElement;
        SubjectElement religionElement;

        SubjectElement sportsElement;
        SubjectElement artElement;
        SubjectElement musicElement;
        SubjectElement psychologyElement;

        #endregion subject elements

        #region init

        /// <summary>
        /// Constructor of the window
        /// </summary>
        public MainWindow()
        {
            // Win
            InitializeComponent();
            
            // App
            InitScreens();
            InitSubjects();
            InitGameTemplates();
            InitNavigation();
            InitNewGameButton();
            InitSideboardButtons();

            // CMS
            content = new SGGEDataManager();
            saveUtility = new SaveUtility();

            //Set Home menu
            SetScreen(SCREEN.HOME);
            homeButton.Select();
            MoveSideBoard(false);

            SideboardSmall.MouseDown += new System.Windows.Input.MouseButtonEventHandler(MousePressedSmallSideboard);
            Sideboard.MouseDown += new System.Windows.Input.MouseButtonEventHandler(MousePressedBigSideboard);

            Search_Textbox.GotFocus += new RoutedEventHandler(SearchboxClicked);
            Search_Textbox.LostFocus += new RoutedEventHandler(SearchboxLost);
        }


        #endregion init

        #region deinit

        /// <summary>
        /// Event that is called when the window is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Window_Closing(object sender, CancelEventArgs e)
        {
            DeInitTemplates();
            DeInitSubjects();
            DeInitNavigation();
            DeInitNewGameButton();
            DeInitSideboardButtons();
            SideboardSmall.MouseDown -= new System.Windows.Input.MouseButtonEventHandler(MousePressedSmallSideboard);
            Sideboard.MouseDown -= new System.Windows.Input.MouseButtonEventHandler(MousePressedBigSideboard);
            saveUtility.Dispose();
        }

        #endregion deinit

        #region buttons

        #region category buttons

        public void OnCategoryButtonClicked(object sender, RoutedEventArgs e)
        {
            CategoryButton button = (CategoryButton)sender;
            LoadOptions(button.Text);
        }

        #endregion category buttons

        #region navigation

        private void Button_Homescreen_Click(object sender, RoutedEventArgs e)
        {
            SetScreen(SCREEN.HOME);
            NavigationButton.lastSelected?.Deselect();
        }

        private void Button_Subjetcs_Click(object sender, RoutedEventArgs e)
        {
            SetScreen(SCREEN.SUBJECT);
            SetSubject(SUBJECT.SCIENCE);
            SideboardButton.lastSelected?.Deselect();
        }

        private void Button_Templates_Click(object sender, RoutedEventArgs e)
        {
            SetScreen(SCREEN.TEMPLATE);
            SideboardButton.lastSelected?.Deselect();
        }

        private void Button_Modify_Click(object sender, RoutedEventArgs e)
        {
            SetScreen(SCREEN.MODIFY);
            SideboardButton.lastSelected?.Deselect();
        }

        private void Button_Export_Click(object sender, RoutedEventArgs e)
        {
            SetScreen(SCREEN.EXPORT);
            SideboardButton.lastSelected?.Deselect();
        }

        private void Button_Konto_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion navigation

        #region subjects

        private void Button_Science_Click(object sender, RoutedEventArgs e)
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

        #region user

        private void Button_NewGame_Click(object sender, RoutedEventArgs e)
        {
            subjectsNavButton.Select();
        }

        private void Button_Profil_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Opt_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion user

        private void SearchButtonClicked(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Suche wird aktuell noch nicht unterstützt!");
        }

        private void SearchboxClicked(object sender, RoutedEventArgs args)
        {
            Search_Textbox.Text = "";
        }

        private void SearchboxLost(object sender, RoutedEventArgs args)
        {
            if(string.IsNullOrEmpty(Search_Textbox.Text))
            {
                Search_Textbox.Text = "Suche...";
            }
        }

        #endregion buttons

        #region screens

        /// <summary>
        /// Initializes the screens
        /// </summary>
        private void InitScreens()
        {
            screens[0] = Grid_MenuSettings;
            screens[1] = Grid_Homescreen;
            screens[2] = Grid_Subjectscreen;
            screens[3] = Grid_Templatescreen;
            screens[4] = Grid_Modifyscreen;
            screens[5] = Grid_Exportscreen;
        }

        /// <summary>
        /// Sets the screen to a specific screen
        /// </summary>
        /// <param name="screen"></param>
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
                    scienceButton.Select();
                    break;
                case SCREEN.TEMPLATE:
                    screens[(int)SCREEN.TEMPLATE].Visibility = Visibility.Visible;
                    screens[(int)SCREEN.MENUSETTINGS].Visibility = Visibility.Visible;
                    SetGameTemplateCategory(TEMPLATE_CATEGORY.ACTION);
                    actionButton.Select();
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

        #region menu
        
        /// <summary>
        /// Receiver of the mouse pressed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MousePressedSmallSideboard(object sender, System.Windows.Input.MouseButtonEventArgs args) 
        {
            MoveSideBoard(true);
        }

        /// <summary>
        /// Receiver of the mouse pressed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MousePressedBigSideboard(object sender, System.Windows.Input.MouseButtonEventArgs args)
        {
            MoveSideBoard(false);
        }

        /// <summary>
        /// Moves the sideboard menu in or out
        /// </summary>
        /// <param name="_out"></param>
        private void MoveSideBoard(bool _out)
        {
            if(_out)
            {
                SideboardSmall.Visibility = Visibility.Hidden;
                Sideboard.Visibility = Visibility.Visible;
                Grid_MenuSettings.Width = Sideboard.Width;
            }
            else
            {
                Sideboard.Visibility = Visibility.Hidden;
                SideboardSmall.Visibility = Visibility.Visible;
                Grid_MenuSettings.Width = SideboardSmall.Width;
            }
        }

        #endregion menu

        #region subjects

        /// <summary>
        /// Initializes all subjects
        /// </summary>
        private void InitSubjects()
        {
            subjects[0] = Border_SubjectsScience;
            subjects[1] = Border_SubjectsLanguages;
            subjects[2] = Border_SubjectsPolitics;
            subjects[3] = Border_SubjectsFurther;

            scienceButton = new SubjectButton("Naturwissenschaften");
            langButton = new SubjectButton("Sprachen");
            politicButton = new SubjectButton("Gesellschaftliches");
            furtherButton = new SubjectButton("Weitere");
            favSubjectButton = new SubjectButton("Favoriten");

            scienceButton.click += Button_Science_Click;
            langButton.click += Button_Languages_Click;
            politicButton.click += Button_Politic_Click;
            furtherButton.click += Button_Further_Click;
            favSubjectButton.click += Button_Favourits_Click;

            Grid_SubjectsMenu.Children.Add(scienceButton);
            Grid_SubjectsMenu.Children.Add(langButton);
            Grid_SubjectsMenu.Children.Add(politicButton);
            Grid_SubjectsMenu.Children.Add(furtherButton);
            Grid_SubjectsMenu.Children.Add(favSubjectButton);


            mathElement = new SubjectElement("Mathematik");
            physicsElement = new SubjectElement("Physik");
            chemistryElement = new SubjectElement("Chemie");
            biologyElement = new SubjectElement("Biologie");
            informaticsElement = new SubjectElement("Informatik");

            germanElement = new SubjectElement("Deutsch");
            englishElement = new SubjectElement("Englisch");
            frenchElement = new SubjectElement("Französisch");
            spanishElement = new SubjectElement("Spanisch");
            latinElement = new SubjectElement("Latein");

            historyElement = new SubjectElement("Geschichte");
            politicsElement = new SubjectElement("Politik");
            ethicsElement = new SubjectElement("Ethik");
            religionElement = new SubjectElement("Religion");

            sportsElement = new SubjectElement("Sport");
            artElement = new SubjectElement("Kunst");
            musicElement = new SubjectElement("Musik");
            psychologyElement = new SubjectElement("Psychologie");

            Grid_SubjectsScience.Children.Add(mathElement);
            Grid_SubjectsScience.Children.Add(physicsElement);
            Grid_SubjectsScience.Children.Add(chemistryElement);
            Grid_SubjectsScience.Children.Add(biologyElement);
            Grid_SubjectsScience.Children.Add(informaticsElement);

            Grid_SubjectsLanguages.Children.Add(germanElement);
            Grid_SubjectsLanguages.Children.Add(englishElement);
            Grid_SubjectsLanguages.Children.Add(frenchElement);
            Grid_SubjectsLanguages.Children.Add(spanishElement);
            Grid_SubjectsLanguages.Children.Add(latinElement);

            Grid_SubjectsPolitics.Children.Add(historyElement);
            Grid_SubjectsPolitics.Children.Add(politicsElement);
            Grid_SubjectsPolitics.Children.Add(ethicsElement);
            Grid_SubjectsPolitics.Children.Add(religionElement);

            Grid_SubjectsFurther.Children.Add(sportsElement);
            Grid_SubjectsFurther.Children.Add(artElement);
            Grid_SubjectsFurther.Children.Add(musicElement);
            Grid_SubjectsFurther.Children.Add(psychologyElement);
        }

        private void DeInitSubjects()
        {
            scienceButton.click -= Button_Science_Click;
            langButton.click -= Button_Languages_Click;
            politicButton.click -= Button_Politic_Click;
            furtherButton.click -= Button_Further_Click;
            favSubjectButton.click -= Button_Favourits_Click;

            scienceButton.Dispose();
            langButton.Dispose();
            politicButton.Dispose();
            furtherButton.Dispose();
            favSubjectButton.Dispose();
        }

        /// <summary>
        /// Sets a specific subject 
        /// </summary>
        /// <param name="subject">The subject to load</param>
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

        /// <summary>
        /// Initializes the game template categories
        /// </summary>
        private void InitGameTemplates()
        {
            gameTemplates[0] = Border_TemplateAction;
            gameTemplates[1] = Border_TemplateAdventure;
            gameTemplates[2] = Border_TemplatePuzzle;
            gameTemplates[3] = Border_TemplateSimulation;
            gameTemplates[4] = Border_TemplateRPG;
            gameTemplates[5] = Border_TemplateFavourites;

            actionButton = new TemplateButton("Actionspiel");
            actionButton.click += Button_Action_Click;

            adventureButton = new TemplateButton("Abenteuerspiel");
            adventureButton.click += Button_Adventure_Click;

            puzzleButton = new TemplateButton("Puzzlespiel");
            puzzleButton.click += Button_Puzzle_Click;

            simulationButton = new TemplateButton("Simulation");
            simulationButton.click += Button_Simulation_Click;

            rpgButton = new TemplateButton("Rollenspiel");
            rpgButton.click += Button_Roleplay_Click;

            favButton = new TemplateButton("Favoriten");
            favButton.click += Button_TemplatesFavorits_Click;

            Grid_Templatesmenu.Children.Add(actionButton);
            Grid_Templatesmenu.Children.Add(adventureButton);
            Grid_Templatesmenu.Children.Add(puzzleButton);
            Grid_Templatesmenu.Children.Add(simulationButton);
            Grid_Templatesmenu.Children.Add(rpgButton);
            Grid_Templatesmenu.Children.Add(favButton);

            jumpNRunElement = new TemplateElement("You Better Jump'n'Run", "Das Jump'n'Run Spiel dient zum Training\nvon " +
                "Reaktionsschnelligkeit und Konzen-\ntration. Spielende müssen einen\nHindernisparkour überqueren und dabei\nAufgaben lösen.");
            
            raceElement = new TemplateElement("Faster", "Das Rennspiel ist ein gutes Training für die\nmotorischen" +
                " Fähigkeiten.\nSpielende messen sich in einem Wett-\nkampf auf Zeit miteinander.");
            
            conveyorBeltElement = new TemplateElement("Fließbandspiel", "Das Fließbandspiel hilft Spielenden unter\n" +
                "Zeitdruck instinktive Entscheidungen zu\ntreffen.\nSpielende müssen die auf dem Fließband\nankommenden Elemente richtig sortieren.");

            laraCroftElement = new TemplateElement("Abenteuer Beispielspiel", "Das Adventurespiel ermöglicht\nes Spielenden in einer Fantasieumgebung Abenteur zu erleben und dabei ihr Wissen zu stärken");
            adventureGameElement = new TemplateElement("Abenteuerspiel", "...");

            tetrisElement = new TemplateElement("Blöcke stapeln", "Mit Hilfe des Spiels kann das räumliche\nDenken von Spielenden unterstütz wer-\nden und ein besser Vorstellung von\nFormen und Volumen entstehen.");
            portalsElement = new TemplateElement("Beispielspiel Puzzle", "...");

            farmSimulatorElement = new TemplateElement("Meine kleine Farm", "Spielende müssen sich um eine fiktive\nFarm kümmern um Verantwortung zu\nübernehmen und zu lernen, mit Res-\nsourcen umzugehen.");
            reignCntryElement = new TemplateElement("Beispielspiel Simulation", "...");

            RPG_Element = new TemplateElement("Model UN", "In dem Rollenspiel Model UN, stellen Spielende die United Nations nache und lernen wichtige Entscheidungen zu treffen.");

            Grid_TemplateAction.Children.Add(jumpNRunElement);
            Grid_TemplateAction.Children.Add(raceElement);
            Grid_TemplateAction.Children.Add(conveyorBeltElement);

            Grid_TemplateAdventure.Children.Add(laraCroftElement);
            Grid_TemplateAdventure.Children.Add(adventureGameElement);

            Grid_TemplatePuzzle.Children.Add(tetrisElement);
            Grid_TemplatePuzzle.Children.Add(portalsElement);

            Grid_TemplateSimulation.Children.Add(farmSimulatorElement);
            Grid_TemplateSimulation.Children.Add(reignCntryElement);

            Grid_TemplateRPG.Children.Add(RPG_Element);

        }

        private void DeInitTemplates()
        {
            actionButton.click -= Button_Action_Click;
            adventureButton.click -= Button_Adventure_Click;
            puzzleButton.click -= Button_Puzzle_Click;
            simulationButton.click -= Button_Simulation_Click;
            rpgButton.click -= Button_Roleplay_Click;
            favButton.click -= Button_TemplatesFavorits_Click;

            actionButton.Dispose();
            adventureButton.Dispose();
            puzzleButton.Dispose();
            simulationButton.Dispose();
            rpgButton.Dispose();
            favButton.Dispose();
        }

        /// <summary>
        /// Loads a specific category for the templates
        /// </summary>
        /// <param name="cat"></param>
        private void SetGameTemplateCategory(TEMPLATE_CATEGORY cat)
        {
            foreach(Border category in gameTemplates)
            {
                category.Visibility = Visibility.Hidden;
            }

            gameTemplates[(int)cat].Visibility = Visibility.Visible;
        }


        #endregion game templates

        #region navigation

        private void InitNavigation()
        {
            subjectsNavButton = new NavigationButton("Fächer", 60);
            templatesNavButton = new NavigationButton("Vorlagen", 75);
            modifyNavButton = new NavigationButton("Bearbeiten", 90);
            exportNavButton = new NavigationButton("Exportieren", 100);

            subjectsNavButton.click += Button_Subjetcs_Click;
            templatesNavButton.click += Button_Templates_Click;
            modifyNavButton.click += Button_Modify_Click;
            exportNavButton.click += Button_Export_Click;

            NavigationPanel.Children.Add(subjectsNavButton);
            NavigationPanel.Children.Add(templatesNavButton);
            NavigationPanel.Children.Add(modifyNavButton);
            NavigationPanel.Children.Add(exportNavButton);

        }

        private void DeInitNavigation()
        {
            subjectsNavButton.click -= Button_Subjetcs_Click;
            templatesNavButton.click -= Button_Templates_Click;
            modifyNavButton.click -= Button_Modify_Click;
            exportNavButton.click -= Button_Export_Click;

            subjectsNavButton.Dispose();
            templatesNavButton.Dispose();
            modifyNavButton.Dispose();
            exportNavButton.Dispose();
        }

        #endregion navigation

        #region new game

        private void InitNewGameButton()
        {
            newGameButton = new NewGameButton("Neues Serious Game erstellen");
            newGameButton.click += Button_NewGame_Click;

            Button_NewGameGrid.Children.Add(newGameButton);
        }

        private void DeInitNewGameButton()
        {
            newGameButton.click -= Button_NewGame_Click;
            newGameButton.Dispose();
        }

        #endregion new game

        #region sideboard

        private void InitSideboardButtons() 
        {
            homeButton = new SideboardButton("Startseite");
            settingsButton = new SideboardButton("Einstellungen");
            userButton = new SideboardButton("Benutzerkonto");

            homeButton.click += Button_Homescreen_Click;
            settingsButton.click += Button_Settings_Click;
            userButton.click += Button_Profil_Click;

            SideboardButtons.Children.Add(homeButton);
            SideboardButtons.Children.Add(settingsButton);
            SideboardButtons.Children.Add(userButton);
        }

        private void DeInitSideboardButtons()
        {
            homeButton.click -= Button_Homescreen_Click;
            settingsButton.click -= Button_Settings_Click;
            userButton.click -= Button_Profil_Click;
        }

        #endregion sideboard

        #region cms

        #region category

        /// <summary>
        /// Creates the category menu
        /// </summary>
        public void FillCategories()
        {
            //Remove delegates and delete all existing buttons
            for(int i = 0; i < Category_Buttons.Children.Count; i++)
            {
                if(Category_Buttons.Children[i] is CategoryButton)
                {
                    CategoryButton b = Category_Buttons.Children[i] as CategoryButton;
                    if (b.HasEventHandler)
                        b.click -= OnCategoryButtonClicked;
                    b.Dispose();
                }
            }

            Category_Buttons.Children.Clear();

            //Rebuild all categories
            foreach(var e in content.GetAllCategories())
            {
                CategoryButton button = new CategoryButton(e.Category);
                button.click += OnCategoryButtonClicked;
                button.HasEventHandler = true;
                Category_Buttons.Children.Add(button);
            }

            if(Category_Buttons.Children.Count >0)
            {
                CategoryButton firstButton = Category_Buttons.Children[0] as CategoryButton;
                firstButton.Select();
            }
                
        }

        #endregion category

        #region options 

        /// <summary>
        /// Loads all options of a specific category
        /// </summary>
        /// <param name="category"></param>
        private void LoadOptions(string category)
        {

            // dispose all elements and clear list
            foreach (var element in Options_Panel.Children)
            {
                if(element is OptionUIElement)
                {
                    OptionUIElement oue = (OptionUIElement)element;
                    oue.Dispose();
                }
            }

            Options_Panel.Children.Clear();

            //categories
            OptionDataElement[] categoryElements = content.GetElementsOfCategory(category);

            // filter headings and list every heading once
            List<string> header = new List<string>();

            foreach(var element in categoryElements)
            {
                if(!header.Contains(element.Header))
                    header.Add(element.Header);
            }

            // add all fitting options to their headings
            foreach(string h in header)
            {
                if(!string.IsNullOrEmpty(h))
                {
                    Options_Panel.Children.Add(new HeaderElement(h));
                    
                    foreach (var element in categoryElements)
                    {
                        if (element.Header.Equals(h))
                        {
                            CreateOption(element);
                        }
                    }
                }
            }

            // check if other elements without headings exist
            List<OptionDataElement> otherElements = new List<OptionDataElement>();

            foreach(var element in categoryElements)
            {
                if (string.IsNullOrEmpty(element.Header))
                {
                    otherElements.Add(element);
                }
            }

            // if not, return
            if(otherElements.Count == 0) { return; }

            // otherwise add all elements under the "other" heading
            Options_Panel.Children.Add(new HeaderElement("Sonstige"));

            foreach (var element in categoryElements)
            {
                if (string.IsNullOrEmpty(element.Header))
                {
                    CreateOption(element);
                }
            }
        }

        /// <summary>
        /// Creates a option element for a specific option data
        /// </summary>
        /// <param name="ode">The option data element</param>
        private void CreateOption(OptionDataElement ode)
        {
            switch (ode.Option)
            {
                case SGGE.OPTION.COLOR:
                    var colorValue = saveUtility.LoadOption(ode.Path) as OptionColorValue;
                    if(colorValue == null)
                    {
                        Options_Panel.Children.Add(new ColorOptionElement(ode.Path, ode.Name, ode.Tooltip, Colors.White));
                    }
                    else
                    {
                        Options_Panel.Children.Add(new ColorOptionElement(ode.Path, ode.Name, ode.Tooltip, (Color)ColorConverter.ConvertFromString(colorValue.Value)));
                    }
                    
                    break;
                case SGGE.OPTION.YES_NO_OPTION:
                    var yesNoValue = saveUtility.LoadOption(ode.Path) as OptionYesNoValue;
                    if(yesNoValue == null)
                    {
                        Options_Panel.Children.Add(new YesNoOptionElement(ode.Path, ode.Name, ode.Tooltip));
                    }
                    else
                    {
                        Options_Panel.Children.Add(new YesNoOptionElement(ode.Path, ode.Name, ode.Tooltip, yesNoValue.Value));
                    }
                    break;
                case SGGE.OPTION.GRAPHICS:
                    var graphicsValue = saveUtility.LoadOption(ode.Path) as OptionGraphicValue;
                    if(graphicsValue == null)
                    {
                        Options_Panel.Children.Add(new GraphicOptionElement(ode.Path, ode.Name, ode.Tooltip));
                    }
                    else
                    {
                        Options_Panel.Children.Add(new GraphicOptionElement(ode.Path, ode.Name, ode.Tooltip, graphicsValue.Value));
                    }
                    break;
                case SGGE.OPTION.SOUND_FILE:
                    var soundFileValue = saveUtility.LoadOption(ode.Path) as OptionAudioValue;
                    if(soundFileValue == null)
                    {
                        Options_Panel.Children.Add(new AudioOptionElement(ode.Path, ode.Name, ode.Tooltip));
                    }
                    else
                    {
                        Options_Panel.Children.Add(new AudioOptionElement(ode.Path, ode.Name, ode.Tooltip, soundFileValue.Value));
                    }
                    break;
                case SGGE.OPTION.REAL_NUM:
                    var realNumValue = saveUtility.LoadOption(ode.Path) as OptionRealValue;
                    if(realNumValue == null)
                    {
                        Options_Panel.Children.Add(new RealNumOptionElement(ode.Path, ode.Name, ode.Tooltip));
                    }
                    else
                    {
                        Options_Panel.Children.Add(new RealNumOptionElement(ode.Path, ode.Name, ode.Tooltip, realNumValue.Value));
                    }
                    break;
                case SGGE.OPTION.DECIMAL_NUM:
                    var decimalNumValue = saveUtility.LoadOption(ode.Path) as OptionDecimalValue;
                    if(decimalNumValue == null)
                    {
                        Options_Panel.Children.Add(new DecimalNumOptionElement(ode.Path, ode.Name, ode.Tooltip));
                    }
                    else
                    {
                        Options_Panel.Children.Add(new DecimalNumOptionElement(ode.Path, ode.Name, ode.Tooltip, decimalNumValue.Value));
                    }
                    break;
                case SGGE.OPTION.ENUM:
                    EnumOptionDataElement eode = (EnumOptionDataElement)ode;
                    var enumValue = saveUtility.LoadOption(ode.Path) as OptionEnumValue;
                    if(enumValue == null)
                    {
                        Options_Panel.Children.Add(new EnumOptionElement(ode.Path, ode.Name, ode.Tooltip, eode.EnumValues));
                    }
                    else
                    {
                        Options_Panel.Children.Add(new EnumOptionElement(ode.Path, ode.Name, ode.Tooltip, eode.EnumValues, 0));//TODO: Hier noch nachbessern
                    }
                    break;
                case SGGE.OPTION.TEXT:
                    var textValue = saveUtility.LoadOption(ode.Path) as OptionTextValue;
                    if(textValue == null)
                    {
                        Options_Panel.Children.Add(new TextOptionElement(ode.Path, ode.Name, ode.Tooltip));
                    }
                    else
                    {
                        Options_Panel.Children.Add(new TextOptionElement(ode.Path, ode.Name, ode.Tooltip, textValue.Value));
                    }
                    break;
                case SGGE.OPTION.ARRAY:
                    ArrayOptionDataElement aode = (ArrayOptionDataElement)ode;
                    var arrayValue = saveUtility.LoadOption(ode.Path) as OptionArrayValue;
                    if(arrayValue == null)
                    {
                        Options_Panel.Children.Add(new ArrayOptionElement(aode.Path, aode.Name, aode.Tooltip, aode.subOptionElements, 0));
                    }
                    else
                    {
                        Options_Panel.Children.Add(new ArrayOptionElement(aode.Path, aode.Name, aode.Tooltip, aode.subOptionElements, 0)); //TODO: Hier noch nachbessern
                    }
                    break;
            }
        }

        #endregion options

        #endregion cms
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
