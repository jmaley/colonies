// Title: MainScreen.cs
// Author: Joe Maley
// Date: 6-9-2013

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
    /// The home screen with a menu widget for Play, Options and Exit.
    /// </summary>
    sealed class MainScreen : Screen
    {
        private MenuWidget menuWidget;

        public MainScreen(ScreenManager screenManager) : base(screenManager)
        {
            //TextButtonWidget textButtonWidget = new TextButtonWidget(spriteBatch, "test", 0, body.Center.X, 0, body.Center.Y, Widget.Anchor.MID_CENTER);
            //menuWidget = new MenuWidget(spriteBatch, 0, body.Center.X, 0, body.Center.Y, Widget.Anchor.MID_CENTER);

            //menuWidget.AddMenuItem(new MenuItem("Play", removeMenu, "Fonts\\SpriteFont1"));
            //menuWidget.AddMenuItem(new MenuItem("Options", toOptionsScreen, "Fonts\\SpriteFont1"));
            //menuWidget = new MenuWidget(spriteBatch, "Textures\\UI\\bear", "Main Menu", 0, body.Center.X, 0, body.Top, Widget.Anchor.TOP_CENTER);
            //menuWidget = new MenuWidget(spriteBatch, "Textures\\UI\\bear", "Main Menu", 0, body.Center.X, 400, body.Top, Widget.Anchor.BOTTOM_CENTER);
            System.Diagnostics.Debug.WriteLine(body.Height);
            menuWidget = new MenuWidget(spriteBatch, "Textures\\UI\\bear", "Main Menu", -500, body.Center.X, body.Height/2 , body.Top, Widget.Anchor.TOP_CENTER);
            menuWidget.AddMenuItem("Play", ToGameScreen);
            menuWidget.AddMenuItem("Options", ToOptionsScreen);
            ModelDisplayWidget mdw = new ModelDisplayWidget(spriteBatch, "Textures\\UI\\temp", 500, body.Center.X, body.Height / 2, body.Top, Widget.Anchor.TOP_CENTER);
            widgets.Add(menuWidget);
            widgets.Add(mdw);
            //widgets.Add(new TextButtonWidget(spriteBatch, "jimmy beatle garbage pants", ToOptionsScreen, 0, body.Left, 0, body.Top, Widget.Anchor.TOP_LEFT));
        }

        
        /// <summary>
        /// TO-DO
        /// </summary>
        protected override void HandleScreenInput()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// TO-DO
        /// </summary>
        protected override void DrawScreen()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// TO-DO
        /// </summary>
        protected override void UpdateScreen(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        private void RemoveMenu()
        {
            //widgets.Remove(menuWidget);

        }


        protected void ToGameScreen()
        {
            screenManager.SetScreen(new GameScreen(screenManager));
        }

        protected void ToOptionsScreen()
        {
          /*  widgets.Remove(menuWidget);
            menuWidget = new MenuWidget(spriteBatch, "Textures\\UI\\bear", "Options Menu", -500, body.Center.X, body.Height / 2, body.Top, Widget.Anchor.TOP_CENTER);
            menuWidget.AddMenuItem("Graphical Settings", ToOptionsScreen);
            menuWidget.AddMenuItem("Network Settings", ToOptionsScreen);
            widgets.Add(menuWidget);
           * */

            screenManager.SetScreen(new OptionScreen(screenManager));
        }

    }
}
