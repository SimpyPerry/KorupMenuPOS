using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KorupMenuPOS
{
    public class CommentEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            //Setup
            entry.TextChanged += OnEntryTextChanged;
            //entry.Completed += OnEntryCompleted;
            base.OnAttachedTo(entry);
        }


        protected override void OnDetachingFrom(Entry entry)
        {
            //Clean up
            entry.TextChanged -= OnEntryTextChanged;
            //entry.Completed -= OnEntryCompleted;
            base.OnDetachingFrom(entry);
        }


        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                var entry = ((Entry)sender);

                if(entry.Text.Length > 200)
                {
                    string entryText = entry.Text;

                    entryText = entryText.Remove(entryText.Length - 1);

                    entry.Text = entryText;
                }
            }
        }
    }
}
