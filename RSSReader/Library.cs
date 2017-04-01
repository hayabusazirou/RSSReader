using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.Web.Syndication;
using Windows.UI.Popups;

namespace RSSReader
{
    public class Library
    {
        private async void load(ItemsControl list, Uri uri)
        {
            SyndicationClient client = new SyndicationClient();
            SyndicationFeed feed = null;
            try
            {
                feed = await client.RetrieveFeedAsync(uri);
            }
            catch
            {
                ShowDialog("", MessageStrings.ExceptionMessages.NoFeed);
            }
                        
            if (feed != null)
            {
                //listの中を削除する
                list.Items.Clear();

                foreach (SyndicationItem item in feed.Items)
                {
                    list.Items.Add(item);
                }
            }
        }

        public void Go(ref ItemsControl list, string value, KeyRoutedEventArgs arg)
        {
            if (arg.Key == Windows.System.VirtualKey.Enter)
            {
                try
                {
                    load(list, new Uri(value));
                }
                catch
                {
                    ShowDialog("", MessageStrings.ExceptionMessages.NotUri);
                }
                list.Focus(FocusState.Keyboard);
            }
        }

        private async void ShowDialog(string title, string message)
        {
            await new MessageDialog(title, message).ShowAsync();
        }
    }
}
