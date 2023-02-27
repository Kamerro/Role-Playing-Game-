using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_01
{
    public class Shop
    {
        List<Items> avaiableItems = new List<Items>();
        //Items, that will be reset after refreshing offer in shop.
        List<Items> showedItems = new List<Items>();

        public void showItems(int amount)
        {

            //Remove all items every time, the adventurer comes into shop
           if(showedItems.Count > 0)
            {
                showedItems.RemoveRange(0, showedItems.Count);
            }
           //Sort which items will be shown where 'amount' is limiter.
            int index = 0;
            foreach(Items item in avaiableItems.Where(item => item.cost<=amount))
            {
                showedItems.Add(item);
                Console.WriteLine($"{++index}:{item.ToString()}");
            }
        }

        public int getCostOfItemByIndex(int i)
        {
            Items item = showedItems[i-1];

            return item.cost;
        }
        public Items getItemByIndex(int i)
        {
            Items item = showedItems[i-1];
            return item;
        }
        //endofcomment;x
        public void addItem(Items item)
        {
            avaiableItems.Add(item);
        }
        public int itemsCount()
        {
            return avaiableItems.Count;
        }

        public void removeItemByIndex(int index)
        {
            Console.WriteLine();
            avaiableItems.Remove(showedItems[index-1]);
            
        }

    }
}
