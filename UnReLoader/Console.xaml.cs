using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NecroNet.UnReLoader.Commands;

namespace NecroNet.UnReLoader
{
	/// <summary>
	/// Interaction logic for MyControl.xaml
	/// </summary>
	public partial class Console : UserControl
	{
		public Console()
		{
			InitializeComponent();
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			var version = Assembly.GetExecutingAssembly().GetName().Version;
			Lines = new ObservableCollection<string>(new[] { string.Format("You are using UnReLoader v{0}.{1}.{2}{3}, type help for list of commands.", version.Major, version.Minor, version.Build, version.Major == 0 ? " (beta)" : string.Empty) });
			CommandHistory = new LinkedList<string>();

			ConsoleArea.ItemsSource = Lines;
		}

		private ObservableCollection<string> Lines { get; set; }

		private LinkedList<string> CommandHistory { get; set; }

		private LinkedListNode<string> CurrentCommand { get; set; }

		private void WriteToConsole(string line)
		{
			Lines.Add(line);
		}

		private void PromptTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Enter:
					var command = PromptTextBox.Text;

					if(string.IsNullOrEmpty(command)) return;

					WriteToConsole("> " + command);
					CommandExecutor.Execute(command, s => Dispatcher.Invoke(new Action(() => WriteToConsole(s))));
					CurrentCommand = CommandHistory.AddLast(command);
					ConsoleScroll.ScrollToBottom();
					PromptTextBox.Clear();
					PromptTextBox.Focus();
					break;
				case Key.Up:
					if(CurrentCommand == null) return;

					PromptTextBox.Text = CurrentCommand.Value;
					PromptTextBox.CaretIndex = PromptTextBox.Text.Length;

					if (CurrentCommand.Previous != null)
					{
						CurrentCommand = CurrentCommand.Previous;
					}

					break;
				case Key.Down:
					if (CurrentCommand == null) return;

					if (CurrentCommand.Next != null)
					{
						CurrentCommand = CurrentCommand.Next;
						PromptTextBox.Text = CurrentCommand.Value;
						PromptTextBox.CaretIndex = PromptTextBox.Text.Length;
					}
					else
					{
						PromptTextBox.Clear();
					}
					break;
			}
		}
	}
}