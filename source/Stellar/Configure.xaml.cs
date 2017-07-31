﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using Stellar.Properties;
using System.Configuration;

/* ----------------------------------------------------------------------
    Stellar ~ RetroArch Nightly Updater by wyzrd
    https://stellarupdater.github.io
    https://forums.libretro.com/users/wyzrd

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.If not, see <http://www.gnu.org/licenses/>. 

    Image Credit: ESO & NASA (CC)
   ---------------------------------------------------------------------- */

namespace Stellar
{
    /// <summary>
    /// Interaction logic for Configure.xaml
    /// </summary>
    public partial class Configure : Window
    {
        private MainWindow mainwindow;

        public static string sevenZipPath; // 7-Zip Config Settings Path (public - pass data)
        public static string winRARPath; // WinRAR Config Settings Path (public - pass data)
        public static string logPath; // stellar.log Config Settings Path (public - pass data)
        public static bool logEnable; //checkBoxLogConfig, Enable or Disable Log, true or false - (public - pass data)

        public static string theme; // Background Theme Image

        public Configure()
        {
            // Configure, dont remove
        }

        public Configure(MainWindow mainwindow) // Pass Constructor from MainWindow
        {
            InitializeComponent();

            this.mainwindow = mainwindow;


            // -----------------------------------------------
            // Prevent Loading Corrupt App.Config
            // -----------------------------------------------
            try
            {
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            }
            catch (ConfigurationErrorsException ex)
            {
                string filename = ex.Filename;

                if (File.Exists(filename) == true)
                {
                    File.Delete(filename);
                    Properties.Settings.Default.Upgrade();
                    // Properties.Settings.Default.Reload();
                }
                else
                {

                }
            }


            // -----------------------------------------------
            // Load Theme
            // -----------------------------------------------
            try
            {
                // First Time Use
                if (string.IsNullOrEmpty(Settings.Default["comboboxThemes"].ToString())) // null check
                {
                    comboBoxThemeConfig.SelectedItem = "Milky Way";

                    // Save Selected Item for next launch
                    Settings.Default["comboboxThemes"] = "Milky Way";
                    Settings.Default.Save();
                }
                // Saved Settings
                else
                {
                    comboBoxThemeConfig.SelectedItem = Settings.Default["comboboxThemes"];
                }
            }
            catch
            {

            }

            // -----------------------------------------------
            // Load 7-Zip Path from Saved Settings
            // -----------------------------------------------
            try
            {
                // First Time Use
                if (string.IsNullOrEmpty(Settings.Default["sevenZipPath"].ToString())) // null check
                {
                    sevenZipPath = "<auto>";
                    textBox7zipConfig.Text = sevenZipPath;
                }
                // Saved Settings
                else
                {
                    sevenZipPath = Settings.Default["sevenZipPath"].ToString();
                    textBox7zipConfig.Text = sevenZipPath;
                }
            }
            catch
            {

            }

            // -----------------------------------------------
            // Load WinRAR Path from Saved Settings
            // -----------------------------------------------
            try
            {
                // First Time Use
                if (string.IsNullOrEmpty(Settings.Default["winRARPath"].ToString())) // null check
                {
                    winRARPath = "<auto>";
                    textBoxWinRARConfig.Text = winRARPath;
                }
                // Saved Settings
                else
                {
                    winRARPath = Settings.Default["winRARPath"].ToString();
                    textBoxWinRARConfig.Text = winRARPath;
                }
            }
            catch
            {

            }

            // -----------------------------------------------
            // Load Log Enable/Disable from Saved Settings
            // -----------------------------------------------
            try
            {
                // First Time Use
                if (string.IsNullOrEmpty(Settings.Default.logEnable.ToString())) // null check
                {
                    logEnable = false;
                    checkBoxLogConfig.IsChecked = false;
                }
                // Saved Settings
                else
                {
                    logEnable = Settings.Default.logEnable;
                    checkBoxLogConfig.IsChecked = Settings.Default.checkBoxLog;
                }
            }
            catch
            {

            }

            // -----------------------------------------------
            // Load Log Path from Saved Settings
            // -----------------------------------------------
            try
            {
                // First Time Use
                if (string.IsNullOrEmpty(Settings.Default["logPath"].ToString())) // null check
                {
                    logPath = string.Empty;
                    textBoxLogConfig.Text = logPath;
                }
                // Saved Settings
                else
                {
                    logPath = Settings.Default["logPath"].ToString();
                    textBoxLogConfig.Text = logPath;
                }
            }
            catch
            {

            }
        }

        // ----------------------------------------------------------------------------------------------
        // METHODS 
        // ----------------------------------------------------------------------------------------------

        // -----------------------------------------------
        // 7-Zip Folder Browser Popup 
        // -----------------------------------------------
        public void sevenZipFolderBrowser() // Method
        {
            var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                textBox7zipConfig.Text = OpenFileDialog.FileName;

                // Set the sevenZipPath string
                sevenZipPath = textBox7zipConfig.Text;

                try
                {
                    // Save 7-zip Path for next launch
                    Settings.Default["sevenZipPath"] = textBox7zipConfig.Text;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
                catch
                {

                }

            }
        }

        // -----------------------------------------------
        // WinRAR Folder Browser Popup 
        // -----------------------------------------------
        public void winRARFolderBrowser() // Method
        {
            var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                textBoxWinRARConfig.Text = OpenFileDialog.FileName;

                // Set the winRARPath string
                winRARPath = textBoxWinRARConfig.Text;

                try
                {
                    // Save WinRAR Path for next launch
                    Settings.Default["winRARPath"] = textBoxWinRARConfig.Text;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
                catch
                {

                }
            }
        }

        // -----------------------------------------------
        // Log Folder Browser Popup 
        // -----------------------------------------------
        public void logFolderBrowser() // Method
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                textBoxLogConfig.Text = folderBrowserDialog.SelectedPath + "\\"; //end with backslash

                // Set the winRARPath string
                logPath = textBoxLogConfig.Text;

                try
                {
                    // Save 7-zip Path for next launch
                    Settings.Default["logPath"] = textBoxLogConfig.Text;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
                catch
                {

                }
            }
        }



        // ----------------------------------------------------------------------------------------------
        // CONTROLS
        // ----------------------------------------------------------------------------------------------

        // -----------------------------------------------
        // 7-Zip Textbox Click
        // -----------------------------------------------
        private void textBox7zipConfig_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            sevenZipFolderBrowser();
        }

        // -----------------------------------------------
        // 7-Zip Textbox (Text Changed)
        // -----------------------------------------------
        private void textBox7zipConfig_TextChanged(object sender, TextChangedEventArgs e)
        {
            // dont use
        }

        // -----------------------------------------------
        // 7-Zip Auto Path (On Click)
        // -----------------------------------------------
        private void button7zipAuto_Click(object sender, RoutedEventArgs e)
        {
            // Display Folder Path in Textbox
            textBox7zipConfig.Text = "<auto>";

            // Set the sevenZipPath string
            sevenZipPath = textBox7zipConfig.Text; //<auto>

            try
            {
                // Save 7-zip Path path for next launch
                Settings.Default["sevenZipPath"] = textBox7zipConfig.Text;
                Settings.Default.Save();
                Settings.Default.Reload();
            }
            catch
            {

            }
        }



        // -----------------------------------------------
        // WinRAR Textbox Click
        // -----------------------------------------------
        private void textBoxWinRARConfig_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            winRARFolderBrowser();
        }

        // -----------------------------------------------
        // WinRAR Auto Path (On Click)
        // -----------------------------------------------
        private void buttonWinRARAuto_Click(object sender, RoutedEventArgs e)
        {
            // Display Folder Path in Textbox
            textBoxWinRARConfig.Text = "<auto>";

            // Set the winRARPath string
            winRARPath = textBoxWinRARConfig.Text; //<auto>

            try
            {
                // Save 7-zip Path path for next launch
                Settings.Default["winRARPath"] = textBoxWinRARConfig.Text;
                Settings.Default.Save();
                Settings.Default.Reload();
            }
            catch
            {

            }
        }



        // -----------------------------------------------
        // Log Checkbox (Checked)
        // -----------------------------------------------
        private void checkBoxLogConfig_Checked(object sender, RoutedEventArgs e)
        {
            // Enable the Log
            logEnable = true;

            // must be done this way or you get "convert object to bool error"
            if (checkBoxLogConfig.IsChecked == true)
            {
                try
                {
                    // Save Checkbox Settings
                    Settings.Default.checkBoxLog = true;
                    Settings.Default.Save();
                    Settings.Default.Reload();

                    // Save Log Enable Settings
                    Settings.Default.logEnable = true;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
                catch
                {

                }
            }
            else if (checkBoxLogConfig.IsChecked == false)
            {
                try
                {
                    // Save Checkbox Settings
                    Settings.Default.checkBoxLog = false;
                    Settings.Default.Save();
                    Settings.Default.Reload();

                    // Save Log Enable Settings
                    Settings.Default.logEnable = false;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
                catch
                {

                }
            }
        }

        // -----------------------------------------------
        // Log Checkbox (Unchecked)
        // -----------------------------------------------
        private void checkBoxLogConfig_Unchecked(object sender, RoutedEventArgs e)
        {
            // Disable the Log
            logEnable = false;

            // must be done this way or you get "convert object to bool error"
            if (checkBoxLogConfig.IsChecked == true)
            {
                try
                {
                    // Save Checkbox Settings
                    Settings.Default.checkBoxLog = true;
                    Settings.Default.Save();
                    Settings.Default.Reload();

                    // Save Log Enable Settings
                    Settings.Default.logEnable = true;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
                catch
                {

                }
            }
            else if (checkBoxLogConfig.IsChecked == false)
            {
                try
                {
                    // Save Checkbox Settings
                    Settings.Default.checkBoxLog = false;
                    Settings.Default.Save();
                    Settings.Default.Reload();

                    // Save Log Enable Settings
                    Settings.Default.logEnable = false;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
                catch
                {

                }
            }
        }

        // -----------------------------------------------
        // Log Textbox (On Click)
        // -----------------------------------------------
        private void textBoxLogConfig_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            logFolderBrowser();
        }

        // -----------------------------------------------
        // Log Auto Path (On Click)
        // -----------------------------------------------
        private void buttonLogAuto_Click(object sender, RoutedEventArgs e)
        {
            // Uncheck Log Checkbox
            checkBoxLogConfig.IsChecked = false;

            // Clear Path in Textbox
            textBoxLogConfig.Text = string.Empty;

            // Set the sevenZipPath string
            logPath = string.Empty;
            try
            {
                // Save Log Path path for next launch
                Settings.Default["logPath"] = string.Empty;
                Settings.Default.Save();
                Settings.Default.Reload();
            }
            catch
            {

            }
        }

        // -----------------------------------------------
        // Clear All Saved Settings Button
        // -----------------------------------------------
        private void buttonClearAllSavedSettings_Click(object sender, RoutedEventArgs e)
        {
            // Revert 7-Zip
            textBox7zipConfig.Text = "<auto>";
            sevenZipPath = textBox7zipConfig.Text;

            // Revert WinRAR
            textBoxWinRARConfig.Text = "<auto>";
            winRARPath = textBoxWinRARConfig.Text;

            // Revert Log
            checkBoxLogConfig.IsChecked = false;
            textBoxLogConfig.Text = string.Empty;
            logPath = string.Empty;

            Properties.Settings.Default.Reset();
        }

        // -----------------------------------------------
        // Set Theme (Combobox)
        // -----------------------------------------------
        private void comboBoxThemeConfig_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the Main Window, may require configure window's owner set in MainWindow.xaml.cs
            //MainWindow mainwindow = this.Owner as MainWindow;

            //Application.Current.MainWindow = Configure;

            // Black
            if ((string)comboBoxThemeConfig.SelectedItem == "Black") //not used
            {
                // Call Method
                //removeTheme();
            }

            // Milky Way
            else if ((string)comboBoxThemeConfig.SelectedItem == "Milky Way")
            {
                theme = "MilkyWay";

                // Change Theme Resource
                App.Current.Resources.MergedDictionaries.Clear();
                App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("Theme" + theme + ".xaml", UriKind.RelativeOrAbsolute)
                });

                //// Image Credit
                labelTheme.Content = "ESO";
            }

            // Spiral Galaxy
            else if ((string)comboBoxThemeConfig.SelectedItem == "Spiral Galaxy")
            {
                theme = "SpiralGalaxy";

                // Change Theme Resource
                App.Current.Resources.MergedDictionaries.Clear();
                App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("Theme" + theme + ".xaml", UriKind.RelativeOrAbsolute)
                });

                //// Image Credit
                labelTheme.Content = "ESO, NGC 1232";
            }

            // Spiral Nebula
            else if ((string)comboBoxThemeConfig.SelectedItem == "Spiral Nebula")
            {
                theme = "SpiralNebula";

                // Change Theme Resource
                App.Current.Resources.MergedDictionaries.Clear();
                App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("Theme" + theme + ".xaml", UriKind.RelativeOrAbsolute)
                });

                // Image Credit
                labelTheme.Content = "NASA, NGC 5189";
            }

            // Solar Flare
            else if ((string)comboBoxThemeConfig.SelectedItem == "Solar Flare")
            {
                theme = "SolarFlare";

                // Change Theme Resource
                App.Current.Resources.MergedDictionaries.Clear();
                App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("Theme" + theme + ".xaml", UriKind.RelativeOrAbsolute)
                });

                // Image Credit
                labelTheme.Content = "NASA";
            }

            // Flaming Star
            else if ((string)comboBoxThemeConfig.SelectedItem == "Flaming Star")
            {
                theme = "FlamingStar";

                // Change Theme Resource
                App.Current.Resources.MergedDictionaries.Clear();
                App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("Theme" + theme + ".xaml", UriKind.RelativeOrAbsolute)
                });

                // Image Credit
                labelTheme.Content = "NASA, IC 405";
            }

            // Dark Galaxy
            else if ((string)comboBoxThemeConfig.SelectedItem == "Dark Galaxy")
            {
                theme = "DarkGalaxy";

                // Change Theme Resource
                App.Current.Resources.MergedDictionaries.Clear();
                App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("Theme" + theme + ".xaml", UriKind.RelativeOrAbsolute)
                });

                // Image Credit
                labelTheme.Content = "NASA, M100";
            }

            // Lagoon
            else if ((string)comboBoxThemeConfig.SelectedItem == "Lagoon")
            {
                theme = "Lagoon";

                // Change Theme Resource
                App.Current.Resources.MergedDictionaries.Clear();
                App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("Theme" + theme + ".xaml", UriKind.RelativeOrAbsolute)
                });

                // Image Credit
                labelTheme.Content = "NASA, IC 405";
            }

            // Dark Nebula
            else if ((string)comboBoxThemeConfig.SelectedItem == "Dark Nebula")
            {
                theme = "DarkNebula";

                // Change Theme Resource
                App.Current.Resources.MergedDictionaries.Clear();
                App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("Theme" + theme + ".xaml", UriKind.RelativeOrAbsolute)
                });

                // Image Credit
                labelTheme.Content = "NASA, Rho Ophiuchi";
            }

            // -------------------------
            // Save Selected Theme
            // -------------------------
            try
            {
                // Save Theme
                Settings.Default["themes"] = Configure.theme;
                Settings.Default.Save();
                Settings.Default.Reload();

                // Save ComboBox Selected Item
                Settings.Default["comboboxThemes"] = comboBoxThemeConfig.SelectedItem.ToString();
                Settings.Default.Save();
                Settings.Default.Reload();
            }
            catch
            {

            }

        }
    }

}