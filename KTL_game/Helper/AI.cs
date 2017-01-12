using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KTL_game.Helper
{
    public class AI
    {
        private int[] Table { get; set; }
        private List<int> TableValue { get; set; }
        private int SequenceLength { get; set; }
        private bool Started { get; set; }

        public AI(int gameLenght, int seriesLength, int colorsCount)
        {
            this.Table = new int[gameLenght];
            for (int i = 0; i < this.Table.Length; i++)
                this.Table[i] = -1;
            
            this.TableValue = new List<int>();
            for (int i = 0; i < colorsCount; i++)
                this.TableValue.Add(-1);

            this.SequenceLength = seriesLength;
            this.Started = false;
        }

        public int chooseColor(int buttonIndex, List<int> randomColors)
        {
            ///pierwszy ruch losowo
            if(this.Started == false)
            {
                this.Started = true;
                Random rand = new Random();
                int rndMove = rand.Next(randomColors.Count - 1);
                this.Table[buttonIndex] = rndMove;
                return rndMove;
            }
            //---------------------

            int bestValue = 0;
            int bestIndex = 0;
            for(int i = 0 ; i < randomColors.Count ; i ++)
            {
                int tmpValue = predictValue(buttonIndex, randomColors[i]);
                if(tmpValue >=bestValue)
                {
                    bestValue = tmpValue;
                    bestIndex = i;
                }
            }

            this.Table[buttonIndex] = bestIndex;
            this.TableValue[bestIndex] = bestValue;
            return bestIndex;
        }

        public int predictValue(int index, int color)
        {
            int newValue = 0;
            List<int> oldColor = new List<int>();

            for (int i = 0; i < this.Table.Length; i++ )
            {
                if(this.Table[i] == color)
                {
                    oldColor.Add(i);
                }
            }

            if (oldColor.Count < 1)
                return 0;
            else
            {
                newValue = newColorValue(oldColor, index);
            }

            return newValue; 
        }

        public int newColorValue(List<int> oldColor,int newColor)
        {
            if (oldColor.Count == 1)
            {
                int firstIndex = oldColor[0];
                int firstFree = -1;
                int lastFree = -1;
                for(int i = 0 ; i < this.Table.Length ; i++)
                {
                    if(firstFree == -1 && this.Table[i] == -1)
                        firstFree = i;
                    if (firstFree != -1 && this.Table[i] == -1)
                        lastFree = i;
                }
                int maxR = (int)Math.Round(((double)lastFree - (double)firstFree) / ((double)this.SequenceLength - 1.0));
                

            }
            else
            {

            }
                
            return 0;
        }
    }
}
