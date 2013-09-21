// Title: TextButtonWidget.cs
// Author: Joe Maley, James Wells
// Date: 6-23-2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Colonies.Client.UI.Widgets
{
    class TextButtonWidget : Widget
    {
        private String text;
        private Action action;

         /// <summary>
        /// Create a text button widget.
        /// </summary>
        public TextButtonWidget(SpriteBatch spriteBatch, String text, Action action, int offsetX, int parentX, int offsetY, int parentY, Anchor anchor)
            : base(spriteBatch, fontStandard, text, offsetX, parentX, offsetY, parentY, anchor)
        {
            this.text = text;
            this.action = action;
        }

        /// <summary>
        /// Handles widget input (child implementation).
        /// </summary>
        public override void HandleWidgetInput()
        {
            action.Invoke();
        }

        /// <summary>
        /// Handles widget input (child implementation).
        /// </summary>
        public override void DrawWidget()
        {
            spriteBatch.DrawString(fontStandard, text, new Vector2(body.Left, body.Top), Color.Tomato);
        }

    }
}
