using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using incalltask.Controls;
using incalltask.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRenderer))]
namespace incalltask.Droid.Renderer
{
    public class RoundedEntryRenderer : EntryRenderer
    {
        public RoundedEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var gradient = new GradientDrawable();
                var roundedEntry = Element as RoundedEntry;
                var padding = (int)Utils.ConvertDpToPixel(Context, 10);

                if (roundedEntry != null)
                {
                    gradient.SetStroke(roundedEntry.BorderThickness, roundedEntry.BorderColor.ToAndroid());
                    gradient.SetCornerRadius(Utils.ConvertDpToPixel(Context, roundedEntry.BorderRadius));
                    gradient.SetColor(roundedEntry.EntryBackgroundColor.ToAndroid());



                    SetReturnType(roundedEntry);

                    // Editor Action is called when the return button is pressed
                    Control.EditorAction += (object sender, TextView.EditorActionEventArgs args) =>
                    {
                        if (roundedEntry.ReturnType != ReturnType.Next)
                            roundedEntry.Unfocus();

                        // Call all the methods attached to base_entry event handler Completed
                        roundedEntry.InvokeCompleted();
                    };

                }

                if (!roundedEntry.DisplaySuggestions)
                {
                    Control.InputType = Android.Text.InputTypes.TextFlagNoSuggestions;
                }

                Control.Background = gradient;
                Control.SetPadding(padding, padding, padding, padding);
            }
        }

        private void SetReturnType(RoundedEntry entry)
        {
            ReturnType type = entry.ReturnType;

            switch (type)
            {
                case ReturnType.Go:
                    Control.ImeOptions = ImeAction.Go;
                    Control.SetImeActionLabel("Go", ImeAction.Go);
                    break;
                case ReturnType.Next:
                    Control.ImeOptions = ImeAction.Next;
                    Control.SetImeActionLabel("Next", ImeAction.Next);
                    break;
                case ReturnType.Send:
                    Control.ImeOptions = ImeAction.Send;
                    Control.SetImeActionLabel("Send", ImeAction.Send);
                    break;
                case ReturnType.Search:
                    Control.ImeOptions = ImeAction.Search;
                    Control.SetImeActionLabel("Search", ImeAction.Search);
                    break;
                default:
                    Control.ImeOptions = ImeAction.Done;
                    Control.SetImeActionLabel("Done", ImeAction.Done);
                    break;
            }
        }
    }
}