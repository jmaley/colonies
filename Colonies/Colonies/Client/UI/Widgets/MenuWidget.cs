using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Colonies.Client.UI.Widgets
{
    class MenuWidget : Widget
    {
        private String title;
        private TextButtonWidget titleBW;
        private List<TextButtonWidget> menuItems;
        private SpriteFont titleFont;
        private int numItems;

        private static int itemYBuffer = 50;

        /// <summary>
        /// A menu widget needs everything a normal widget has plus a title for the menu
        /// </summary>
        /// SpriteBatch spriteBatch, String background, int offsetX, int parentX, int offsetY, int parentY, Anchor anchor
        public MenuWidget(SpriteBatch spriteBatch, String background, String name, int offsetX, int parentX, int offsetY, int parentY, Anchor anchor)
            : base(spriteBatch, background, offsetX, parentX, offsetY, parentY, anchor)
        {
            this.title = name;
            this.titleBW = new TextButtonWidget(spriteBatch, name, HandleWidgetInput, offsetX, parentX, offsetY, parentY, Widget.Anchor.TOP_CENTER);
            this.menuItems = new List<TextButtonWidget>();
            this.titleFont = fontHeader;
            this.numItems = 0;
        }

        /// <summary>
        /// Handles widget input (child implementation).
        /// </summary>
        public override void HandleWidgetInput()
        {
            System.Diagnostics.Debug.WriteLine("Menu Hi");
            foreach (TextButtonWidget item in menuItems)
            {
                item.HandleInput();
            }
        }

        /// <summary>
        /// Draws the widget (child implementation).
        /// </summary>
        public override void DrawWidget()
        {
            titleBW.Draw();

            //Vector2 position = new Vector2(baseX + background.Width/2  (titleFont.MeasureString(title).X / 2), baseY + itemYBuffer);
            //spriteBatch.DrawString(titleFont, title, position, Color.Yellow);

            //position = titleFont.MeasureString(title);
            int count = 1;
            foreach (TextButtonWidget item in menuItems)
            {
                //float x = baseX + (background.Width / 2);
                //float y = baseY + position.Y + (itemYBuffer * (count + 2));
                //item.SetPos(x, y);
                item.Draw();
                count++;
            }
        }

        public void AddMenuItem(String item, Action action)
        {
            menuItems.Add(new TextButtonWidget(spriteBatch, item, action, 0, body.Center.X, (numItems * (int)fontStandard.MeasureString(item).Y), body.Center.Y, Widget.Anchor.TOP_CENTER));
            numItems++;
        }

    }
}