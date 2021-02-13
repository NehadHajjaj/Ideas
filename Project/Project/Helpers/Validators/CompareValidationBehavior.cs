using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Project.ForEntry;

namespace Project.Helpers.Validators
{
    

    

        public class CompareValidationBehavior : Behavior<CustomEntry>
        {

            public static BindableProperty TextProperty = BindableProperty.Create<CompareValidationBehavior, string>(tc => tc.Text, string.Empty, BindingMode.TwoWay);

            public string Text
            {
                get
                {
                    return (string)GetValue(TextProperty);
                }
                set
                {
                    SetValue(TextProperty, value);
                }
            }


            protected override void OnAttachedTo(CustomEntry bindable)
            {
                bindable.TextChanged += HandleTextChanged;
                base.OnAttachedTo(bindable);
            }

            void HandleTextChanged(object sender, TextChangedEventArgs e)
            {
                bool IsValid = false;
                IsValid = e.NewTextValue == Text;

                ((CustomEntry)sender).TextColor = IsValid ? Color.Default : Color.Red;
            }

            protected override void OnDetachingFrom(CustomEntry bindable)
            {
                bindable.TextChanged -= HandleTextChanged;
                base.OnDetachingFrom(bindable);
            }
        }
    }

