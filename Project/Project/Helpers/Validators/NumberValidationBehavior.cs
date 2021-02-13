using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Project.ForEntry;
namespace Project.Helpers.Validators
{
  

  
        public class NumberValidationBehavior : Behavior<CustomEntry>
        {
            protected override void OnAttachedTo(CustomEntry entry)
            {
                entry.TextChanged += OnEntryTextChanged;
                base.OnAttachedTo(entry);
            }

            protected override void OnDetachingFrom(CustomEntry entry)
            {
                entry.TextChanged -= OnEntryTextChanged;
                base.OnDetachingFrom(entry);
            }

            void OnEntryTextChanged(object sender, TextChangedEventArgs args)
            {
                int result;

                bool isValid = int.TryParse(args.NewTextValue, out result);

                ((CustomEntry)sender).TextColor = isValid ? Color.Default : Color.Red;
            }
        }
    }

