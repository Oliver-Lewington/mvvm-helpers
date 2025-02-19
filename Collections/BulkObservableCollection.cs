using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MvvmHelpers.Collections
{
    public class BulkObservableCollection<T> : ObservableCollection<T>
    {
        private bool _suppressNotification = false;

        public void AddRange(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            _suppressNotification = true;
            foreach (var item in items)
            {
                Add(item); // Uses base class method
            }
            _suppressNotification = false;

            // Manually trigger a single reset event
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_suppressNotification)
            {
                base.OnCollectionChanged(e);
            }
        }
    }
}
