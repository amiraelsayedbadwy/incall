﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using incalltask.Controls;
using incalltask.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(RoundedView), typeof(RoundedViewRenderer))]
namespace incalltask.Droid.Renderer
{
    class RoundedViewRenderer : ViewRenderer<RoundedView, Android.Views.View>
    {
        public RoundedViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<RoundedView> e)
        {
            base.OnElementChanged(e);

            var gradient = new GradientDrawable();

            if (Element is RoundedView roundedView)
            {
                gradient.SetStroke(roundedView.BorderThickness, roundedView.BorderColor.ToAndroid());
                gradient.SetCornerRadius(roundedView.BorderRadius);
                gradient.SetColor(roundedView.BackgroundColor.ToAndroid());
            }

            Background = gradient;
        }
    }
}