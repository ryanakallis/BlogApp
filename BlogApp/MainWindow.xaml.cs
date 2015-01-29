using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

namespace BlogApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private BlogContext _context = new BlogContext();
		public MainWindow()
		{
			InitializeComponent();
		} 

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

			System.Windows.Data.CollectionViewSource blogViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("blogViewSource")));
			// Load data by setting the CollectionViewSource.Source property:
			// blogViewSource.Source = [generic data source]

			_context.Blogs.Load();


			blogViewSource.Source = _context.Blogs.Local; 
		}

		private void saveButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (var post in _context.Posts.Local.ToList())
			{
				if (post.Blog == null)
				{
					_context.Posts.Remove(post);
				}
			}

			_context.SaveChanges();
			// Refresh the grids so the database generated values show up. 
			this.blogDataGrid.Items.Refresh();
			this.postsDataGrid.Items.Refresh();
		}


		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			base.OnClosing(e);
			this._context.Dispose();
		} 

		
	}
}
