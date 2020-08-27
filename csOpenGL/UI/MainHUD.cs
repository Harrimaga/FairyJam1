using OpenTK.Input;
using QuickFont;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    public static class MainHUD
    {

        public static Sprite TopBar, Menu, ResourceBack, ResourceLine;
        public static QFont ResourceFontTop, ResourceFontBot;

        public static List<DrawnButton> buttons;
        public static DrawnButton pauseButton, turnButton;
        public static Sprite[] sprites;

        public static void Init() 
        {
            TopBar = new Sprite(1920, 70, 0, 1);
            Menu = new Sprite(60, 60, 0, 1);
            ResourceBack = new Sprite(120, 60, 0, 1);
            ResourceLine = new Sprite(60, 4, 0, 1);

            sprites = new Sprite[7]{
                new Sprite(25, 25, 0, Textures.Get(Textures.money)),
                new Sprite(25, 25, 0, Textures.Get(Textures.materials)),
                new Sprite(25, 25, 0, Textures.Get(Textures.food)),
                new Sprite(25, 25, 0, Textures.Get(Textures.population)),
                new Sprite(25, 25, 0, Textures.Get(Textures.fuel)),
                new Sprite(25, 25, 0, Textures.Get(Textures.bacon)),
                new Sprite(25, 25, 0, Textures.Get(Textures.circle))
            };

            pauseButton = new DrawnButton("", 1920 - 245, 5, 240, 60, () => { Globals.paused = !Globals.paused; }, 0, 0, 1, true);
            turnButton = new DrawnButton("", 1920 - 240, 1080 - 80, 240, 80, () => { Globals.map.Turn(); }, 1, 0, 0, true);
            buttons = new List<DrawnButton>()
            {
                pauseButton,
                turnButton
            };

            ResourceFontTop = new QFont("Fonts/times.ttf", 16, new QuickFont.Configuration.QFontBuilderConfiguration());
            ResourceFontBot = new QFont("Fonts/times.ttf", 12, new QuickFont.Configuration.QFontBuilderConfiguration());
        } 

        public static string Round(double x)
        {
            return Math.Truncate((x * 100) / 100).ToString();
        }

        public static void Draw()
        {
            Nation n = Globals.PlayerNation;
            // Top bar
            TopBar.DrawLate(0, 0, false, 0, 132f/255f, 157f/255f, 1, 1);
            //  - Menu
            Menu.DrawLate(5, 5, false, 0, 45f / 255, 38f / 255, 1, 1);
            //  - Resources
            // Money
            ResourceBack.DrawLate(70, 5, false, 0, 45f / 255, 38f / 255, 1, 1);

            Window.window.DrawTextCentered(Round(n.Money), 140, 10, true, ResourceFontTop);
            ResourceLine.DrawLate(110, 35, false, 0, 0, 0, 0, 1);
            Window.window.DrawTextCentered(Globals.PlayerNation.resourceChanges[0] > 0 ? "+" + Globals.PlayerNation.resourceChanges[0].ToString("N2") : Globals.PlayerNation.resourceChanges[0].ToString("N2"), 140, 40, Globals.PlayerNation.resourceChanges[0] < 0 ? 1 : 0, Globals.PlayerNation.resourceChanges[0] > 0 ? 1 : 0, 0, 1, true, ResourceFontBot);
            // Materials
            ResourceBack.DrawLate(195, 5, false, 0, 45f / 255, 38f / 255, 1, 1);
            
            Window.window.DrawTextCentered(Round(n.Materials), 265, 10, true, ResourceFontTop);
            ResourceLine.DrawLate(235, 35, false, 0, 0, 0, 0, 1);
            Window.window.DrawTextCentered(Globals.PlayerNation.resourceChanges[1] > 0 ? "+" + Globals.PlayerNation.resourceChanges[1].ToString("N2") : Globals.PlayerNation.resourceChanges[1].ToString("N2"), 265, 40, Globals.PlayerNation.resourceChanges[1] < 0 ? 1 : 0, Globals.PlayerNation.resourceChanges[1] > 0 ? 1 : 0, 0, 1, true, ResourceFontBot);
            // Food
            ResourceBack.DrawLate(320, 5, false, 0, 45f / 255, 38f / 255, 1, 1);
            
            Window.window.DrawTextCentered(Round(n.Food), 390, 10, true, ResourceFontTop);
            ResourceLine.DrawLate(360, 35, false, 0, 0, 0, 0, 1);
            Window.window.DrawTextCentered(Globals.PlayerNation.resourceChanges[2] > 0 ? "+" + Globals.PlayerNation.resourceChanges[2].ToString("N2") : Globals.PlayerNation.resourceChanges[2].ToString("N2"), 390, 40, Globals.PlayerNation.resourceChanges[2] < 0 ? 1 : 0, Globals.PlayerNation.resourceChanges[2] > 0 ? 1 : 0, 0, 1, true, ResourceFontBot);
            // Fuel
            ResourceBack.DrawLate(445, 5, false, 0, 45f / 255, 38f / 255, 1, 1);
            
            Window.window.DrawTextCentered(Round(n.Fuel), 515, 10, true, ResourceFontTop);
            ResourceLine.DrawLate(485, 35, false, 0, 0, 0, 0, 1);
            Window.window.DrawTextCentered(Globals.PlayerNation.resourceChanges[3] > 0 ? "+" + Globals.PlayerNation.resourceChanges[3].ToString("N2") : Globals.PlayerNation.resourceChanges[3].ToString("N2"), 515, 40, Globals.PlayerNation.resourceChanges[3] < 0 ? 1 : 0, Globals.PlayerNation.resourceChanges[3] > 0 ? 1 : 0, 0, 1, true, ResourceFontBot);
            // Population
            ResourceBack.DrawLate(570, 5, false, 0, 45f / 255, 38f / 255, 1, 1);
            
            Window.window.DrawTextCentered(Round(n.Population), 640, 10, true, ResourceFontTop);
            ResourceLine.DrawLate(610, 35, false, 0, 0, 0, 0, 1);
            Window.window.DrawTextCentered(Globals.PlayerNation.resourceChanges[4] > 0 ? "+" + Globals.PlayerNation.resourceChanges[4].ToString("N2") : Globals.PlayerNation.resourceChanges[4].ToString("N2"), 640, 40, Globals.PlayerNation.resourceChanges[4] < 0 ? 1 : 0, Globals.PlayerNation.resourceChanges[4] > 0 ? 1 : 0, 0, 1, true, ResourceFontBot);
            // Happiness
            ResourceBack.DrawLate(695, 5, false, 0, 45f / 255, 38f / 255, 1, 1);
            
            Window.window.DrawTextCentered(Round(n.Happiness), 765, 10, true, ResourceFontTop);
            ResourceLine.DrawLate(735, 35, false, 0, 0, 0, 0, 1);
            Window.window.DrawTextCentered(Globals.PlayerNation.resourceChanges[5] > 0 ? "+" + Globals.PlayerNation.resourceChanges[5].ToString("N2") : Globals.PlayerNation.resourceChanges[5].ToString("N2"), 765, 40, Globals.PlayerNation.resourceChanges[5] < 0 ? 1 : 0, Globals.PlayerNation.resourceChanges[5] > 0 ? 1 : 0, 0, 1, true, ResourceFontBot);
            // Techpoints
            ResourceBack.DrawLate(820, 5, false, 0, 45f / 255, 38f / 255, 1, 1);
            
            Window.window.DrawTextCentered(Round(n.TechPoints), 890, 10, true, ResourceFontTop);
            ResourceLine.DrawLate(860, 35, false, 0, 0, 0, 0, 1);
            Window.window.DrawTextCentered(Globals.PlayerNation.resourceChanges[6] > 0 ? "+" + Globals.PlayerNation.resourceChanges[6].ToString("N2") : Globals.PlayerNation.resourceChanges[6].ToString("N2"), 890, 40, Globals.PlayerNation.resourceChanges[6] < 0 ? 1 : 0, Globals.PlayerNation.resourceChanges[6] > 0 ? 1 : 0, 0, 1, true, ResourceFontBot);

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].DrawLate(75 + i * 125, 23, false);
            }

            //  - Techtrees
            //  - Nation View
            //  - Pause BG/turn number
            pauseButton.Draw();



            // Bottom
            //  - Minimap
            //  - Next Turn button
            turnButton.Draw();
        }

        public static void MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            if (e.Button == MouseButton.Left)
            {
                for (int i = buttons.Count - 1; i >= 0; i--)
                {
                    DrawnButton button = buttons[i];
                    if (button.IsInButton(mx, my))
                    {
                        button.OnClick();
                        break;
                    }
                }
            }
        }
    }
}
