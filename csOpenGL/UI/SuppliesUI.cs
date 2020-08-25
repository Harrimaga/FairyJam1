using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    class SuppliesUI : OneMinuteUI
    {
        private double currentPoints;

        public SuppliesUI()
        {
            currentPoints = 0;
            for(int i=0;i<5;i++){
                for(int j=0;j<6;j++){
                    int amount = 0;
                    int x = 1920/2 - 25;
                    switch(j) {
                    case 0: amount = -100;
                        x -= 350;
                        break;
                    case 1: amount = -10;
                        x -= 300;
                        break;
                    case 2: amount = -1;
                        x -= 250;
                        break;
                    case 3: amount = 1;
                        x += 200;
                        break;
                    case 4: amount = 10;
                        x += 250;
                        break;
                    case 5: amount = 100;
                        x += 300;
                        break;
                    }
                    int k = i;
                    buttons.Add(new DrawnButton("" + amount, x, 200 + 50*i, 50, 50, () => { double realAmount = currentPoints + amount > Balance.SuppliesPointBuy ?Balance.SuppliesPointBuy-currentPoints : amount  ;currentPoints += Globals.PlayerNation.AddSuppliesPointBuy(k, realAmount); }, 0.5f, 0.5f, 0.5f));
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            double materials, food, fuel, money, population;

            // Draw Point Limit
            Window.window.DrawTextCentered(currentPoints + "/" + Balance.SuppliesPointBuy, 1920 / 2, 100);

            Window.window.DrawTextCentered("Materials/" + Globals.PlayerNation.Materials, 1920 / 2, 200);
            Window.window.DrawTextCentered("Food/" + Globals.PlayerNation.Food, 1920 / 2, 250);
            Window.window.DrawTextCentered("Fuel/" + Globals.PlayerNation.Fuel, 1920 / 2, 300);
            Window.window.DrawTextCentered("Population/" + Globals.PlayerNation.Population, 1920 / 2, 350);
            Window.window.DrawTextCentered("Money/" + Globals.PlayerNation.Money, 1920 / 2, 400);

            // Draw Material Selector
        }
    }
}
