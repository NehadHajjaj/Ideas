using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Project.ForEntry;
namespace Project.Helpers.Validators
{
  

    public class MaxLengthValidatorBehavior : Behavior<CustomEntry>
        {
            public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(MaxLengthValidatorBehavior), 0);

            public int MaxLength
            {
                get { return (int)GetValue(MaxLengthProperty); }
                set { SetValue(MaxLengthProperty, value); }
            }

            protected override void OnAttachedTo(CustomEntry bindable)
            {
                bindable.TextChanged += bindable_TextChanged;
            }

            private void bindable_TextChanged(object sender, TextChangedEventArgs e)
            {
                if (e.NewTextValue.Length >= MaxLength)
                    ((CustomEntry)sender).Text = e.NewTextValue.Substring(0, MaxLength);
            }

            protected override void OnDetachingFrom(CustomEntry bindable)
            {
                bindable.TextChanged -= bindable_TextChanged;

            }
        }
    }

