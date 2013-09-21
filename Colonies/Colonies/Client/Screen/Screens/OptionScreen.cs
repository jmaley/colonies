// Title: OptionScreen.cs
// Author: Chris Abel
// Date: 7-6-2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Colonies.Client.UI.Widgets;
using Colonies.Client.UI;
using Colonies.Client.Core;

namespace Colonies.Client.Screen.Screens
{
    /// <summary>
    /// The options screen with a menu widget for [[PUT THINGS HERE]]
    /// </summary>
    sealed class OptionScreen : Screen
    {
        private MenuWidget optionWidget;

        public OptionScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            optionWidget = new MenuWidget(spriteBatch, "Textures\\UI\\bear", "Options Menu", -500, body.Center.X, body.Height / 2, body.Top, Widget.Anchor.TOP_CENTER);
            optionWidget.AddMenuItem("Back", ToMainScreen);
            widgets.Add(optionWidget);
        }

        protected override void HandleScreenInput()
        {
            //throw new NotImplementedException();
        }

        protected override void DrawScreen()
        {
          //  throw new NotImplementedException();
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
          //  throw new NotImplementedException();
        }

        private void RemoveMenu()
        {
        }

        protected void ToMainScreen()
        {
            screenManager.SetScreen(new MainScreen(screenManager));
        }
    }
}
