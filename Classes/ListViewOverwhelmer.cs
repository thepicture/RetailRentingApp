using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;

namespace RetailRentingApp.Classes
{
    public class ListViewOverwhelmer<T>
    {
        private IList<T> collection;
        private IList<T> clearingCollection;
        private readonly ItemsControl itemsControl;
        private readonly DispatcherTimer timer;

        public ListViewOverwhelmer(ItemsControl itemsControl, IList<T> collection)
        {
            this.collection = collection;
            this.itemsControl = itemsControl;

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(Properties.Settings.Default.UPDATE_INTERVAL)
            };

            timer.Tick += TimerForListView;
        }

        /// <summary>
        /// Non-blocking method for ListView filling with the items.
        /// </summary>
        public void Overwhelm()
        {
            DisableTimerIfItIsEnabled();
            clearingCollection = new List<T>(collection.ToList());
            timer.Start();
        }

        private void DisableTimerIfItIsEnabled()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }
        }

        private void TimerForListView(object sender, EventArgs e)
        {
            bool hasAnyMoreItems = clearingCollection.Count > 0;

            if (hasAnyMoreItems == false)
            {
                DisableTimerIfItIsEnabled();
                return;
            }

            AddAnotherArea();
        }

        private void AddAnotherArea()
        {
            T item = clearingCollection[0];
            clearingCollection = clearingCollection.Skip(1).ToList();
            _ = itemsControl.Items.Add(item);
        }
    }
}
