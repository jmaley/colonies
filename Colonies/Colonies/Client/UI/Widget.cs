// Title: Widget.cs
// Author: Joe Maley
// Date: 6-9-2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Colonies.Client.Core;

namespace Colonies.Client.UI
{
    /// <summary>
    /// Widgets are used to make up a user interface.
    /// </summary>
    abstract class Widget
    {
        /// <summary>
        /// The anchor specifies the where the origin of the widget is located.
        /// </summary>
        public enum Anchor
        {
            TOP_LEFT,
            TOP_CENTER,
            TOP_RIGHT,
            MID_LEFT,
            MID_CENTER,
            MID_RIGHT,
            BOTTOM_LEFT,
            BOTTOM_CENTER,
            BOTTOM_RIGHT
        }

        protected InputManager inputManager;
        protected SpriteBatch spriteBatch;

        protected static SpriteFont fontHeader = AssetManager.GetInstance().getAsset<SpriteFont>("Fonts\\Base02Hdr");
        protected static SpriteFont fontStandard = AssetManager.GetInstance().getAsset<SpriteFont>("Fonts\\Base02Std");
        protected static SpriteFont fontSmall = AssetManager.GetInstance().getAsset<SpriteFont>("Fonts\\Base02Sml");

        protected Texture2D background;
        protected Rectangle body;

        protected String anchor = null;

        /// <summary>
        /// Creates a generic widget.
        /// </summary>
        public Widget(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.inputManager = InputManager.GetInstance();
        }

        /// <summary>
        /// Creates a new widget with desired background texture.
        /// </summary>
        public Widget(SpriteBatch spriteBatch, String background, int offsetX, int parentX, int offsetY, int parentY, Anchor anchor) : this (spriteBatch)
        {
            this.background = AssetManager.GetInstance().getAsset<Texture2D>(background);

            BuildBody(this.background.Width, this.background.Height, offsetX, parentX, offsetY, parentY, anchor);
        }

        /// <summary>
        /// Creates a new widget with desired background texture.
        /// </summary>
        public Widget(SpriteBatch spriteBatch, SpriteFont font, String text, int offsetX, int parentX, int offsetY, int parentY, Anchor anchor) : this (spriteBatch)
        {
            BuildBody((int)font.MeasureString(text).X, (int)font.MeasureString(text).Y, offsetX, parentX, offsetY, parentY, anchor);
        }

        /// <summary>
        /// Builds the body of the widget.
        /// </summary>
        private void BuildBody(int width, int height, int offsetX, int parentX, int offsetY, int parentY, Anchor anchor)
        {
            int x = parentX + offsetX;
            int y = parentY + offsetY;

            body = new Rectangle(x, y, width, height);

            switch (anchor)
            {
                case Anchor.TOP_LEFT:
                    body.Offset(0, 0);
                    break;
                case Anchor.TOP_CENTER:
                    body.Offset(-(body.Width / 2), 0);
                    break;
                case Anchor.TOP_RIGHT:
                    body.Offset(-body.Width, 0);
                    break;
                case Anchor.MID_LEFT:
                    body.Offset(0, -(body.Height / 2));
                    break;
                case Anchor.MID_CENTER:
                    body.Offset(-(body.Width / 2), -(body.Height / 2));
                    break;
                case Anchor.MID_RIGHT:
                    body.Offset(-body.Width, -(body.Height / 2));
                    break;
                case Anchor.BOTTOM_LEFT:
                    body.Offset(0, -body.Height);
                    break;
                case Anchor.BOTTOM_CENTER:
                    body.Offset(-(body.Width / 2), -body.Height);
                    break;
                case Anchor.BOTTOM_RIGHT:
                    body.Offset(-body.Width, -body.Height);
                    break;
            }
        }

        /// <summary>
        /// Handles widget input.
        /// </summary>
        /// <returns>True if clicked.</returns>
        public bool HandleInput()
        {
            if (IsClicked() == true)
            {
                HandleWidgetInput();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Draws the widget.
        /// </summary>
        public void Draw()
        {
            // draw the background before drawing fore-ground widget textures
            if (background != null)
                spriteBatch.Draw(background, body, Color.White);
            
            DrawWidget();
        }

        /// <summary>
        /// Handles widget input (child implementation).
        /// </summary>
        public abstract void HandleWidgetInput();

        /// <summary>
        /// Draws the widget (child implementation).
        /// </summary>
        public abstract void DrawWidget();

        /// <summary>
        /// Checks if the widget has been clicked.
        /// </summary>
        /// <returns>True if clicked, otherwise false.</returns>
        public bool IsClicked()
        {
            // check left-click first to prevent unnecessary bounding computation
            if (inputManager.IsNewLeftClick())
            {
                int x = inputManager.GetMouseX();
                int y = inputManager.GetMouseY();

                if (x > body.Left && x <= body.Right &&
                    y > body.Top && y <= body.Bottom)
                    return true;
            }

            return false;
        }
    }
}
