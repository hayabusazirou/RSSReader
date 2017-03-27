using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.Web.Syndication;

namespace RSSReader
{
    public class Library
    {
        private async void load(ItemsControl list, Uri uri)
        {
            SyndicationClient client = new SyndicationClient();
            SyndicationFeed feed = await client.RetrieveFeedAsync(uri);
                        
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

                }
                list.Focus(FocusState.Keyboard);
            }
        }
    }
}
