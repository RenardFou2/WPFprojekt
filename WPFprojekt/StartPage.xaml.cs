using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WPFprojekt
{
	public partial class StartPage : Window
	{
		public StartPage()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Storyboard storyboard = (Storyboard)this.Resources["TextAnimation"];

			storyboard.Begin();
		}

		private void btnStart_Click(object sender, RoutedEventArgs e)
		{
			App.Current.MainWindow = new MainWindow();

			App.Current.MainWindow.Show();

			this.Close();
		}


	}
}
