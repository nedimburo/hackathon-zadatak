using System;
using System.Collections.Generic;
using System.Globalization;

public class Foo
{
    /*
    * Complete the 'IsBuyerWinner' function below.
    *
    * The function is expected to return an Integer.
    * The function accepts following parameters:
    * 1. List (STRING_ARRAY) - codeList
    * 2. List (STRING_ARRAY) - shoppingCart
    */
    public static int IsBuyerWinner(List<string> codeList, List<string> shoppingCart)
    {
        // First we check if the entered amount for code list is greater than shopping cart
        // User can't be winner because in order to win he needs to get the required combination
        // But if the user buys less, that means that he doesn't have enough items for the required combination
        // So the function returns 0
        if (codeList.Count > shoppingCart.Count)
        {
            return 0;
        }
        // Then we check if the groups have more combined items than shopping cart
        // Because the Count in code list only counts groups and not their items
        // And just like previously, if the shopping cart has less items the function will return 0
        int tempSum = 0;
        for (int x=0; x<codeList.Count; x++)
        {
            string[] partedCodeList = codeList[x].Split(' ');
            tempSum += partedCodeList.Length;
        }
        if (tempSum > shoppingCart.Count)
        {
            return 0;
        }
        // Main part of function
        int forCodeList = 0, forShoppingCart = 0;
        while (true)
        {
            string[] separatedItems = codeList[forCodeList].Split(' '); // This line of code creates an array that has items of one group
            List<string> partOfShoppingCart = new List<string>(); // Creating a list which will cotain same number of items as the current group
            // Making a copy of items from shopping cart into the new list, but before that we check if the sum of current shopping cart index
            // and length of the current group is bigger than the number of items in a shopping cart, if it is the while loop will break
            if (forShoppingCart + separatedItems.Length <= shoppingCart.Count)
            {
                for (int i = forShoppingCart; i < forShoppingCart + separatedItems.Length; i++)
                {
                    partOfShoppingCart.Add(shoppingCart[i]);
                }
            }
            else
            {
                break;
            }
            // Fist Boolean for checking if the items are equal and second Boolean for bypassing that checking if the
            // codelist has anything as a part of the required combination
            bool tempCheck = true;
            bool checkAnything = true;
            for (int i = 0; i < separatedItems.Length; i++)
            {
                // This if statement is used to bypass checking for code list item "anything", which means that user can
                // add any item to a shopping cart and it will be approved for the required combination
                if (separatedItems[i] != "anything")
                {
                    checkAnything = false;
                }
                // If the item is not anything, then we check if the items from the group and the items from the part of a shopping cart
                // are the same
                if (!checkAnything)
                {
                    if (separatedItems[i] != partOfShoppingCart[i])
                    {
                        tempCheck = false;
                        break;
                    }
                }
                checkAnything = true;
            }
            if (tempCheck)
            {
                // If the whole group is equal then we set each code list item(in this case group) to be of value "found"
                // which will be used later for checking which value to return
                codeList[forCodeList] = "found";
                // And the index for code list is increased which means we move onto the next group of items in the code list
                forCodeList++;
            }
            // The index for shopping cart increases which will be the new starting point for the future part of a shopping cart
            forShoppingCart++;
            // Used to delete all the items and make a fresh list
            partOfShoppingCart.Clear();
            // (Just in case) If statements to use to break the while loop.
            if (forShoppingCart == shoppingCart.Count)
            {
                break;
            }
            if (forCodeList == codeList.Count)
            {
                break;
            }
        }
        // This part of the code is used to check which value to return
        bool checkAllCodeList = true;
        for (int y = 0; y < codeList.Count; y++)
        {
            // If all the elements of the code list are set to found than that means that user has gotten the required combination
            // else, the function returns zero
            if (codeList[y] != "found")
            {
                checkAllCodeList = false;
            }
        }
        if (checkAllCodeList)
        {
            return 1;
        }
        return 0;
    }
}
public class Solution
{
    public static void Main(string[] args)
    {
        int codeListCount = Convert.ToInt32(Console.ReadLine().Trim());
        List<string> codeList = new List<string>();
        for (int i = 0; i < codeListCount; i++)
        {
            string codeListItem = Console.ReadLine();
            codeList.Add(codeListItem);
        }
        int shoppingCartCount = Convert.ToInt32(Console.ReadLine().Trim());
        List<string> shoppingCart = new List<string>();
        for (int i = 0; i < shoppingCartCount; i++)
        {
            string shoppingCartItem = Console.ReadLine();
            shoppingCart.Add(shoppingCartItem);
        }
        int foo = Foo.IsBuyerWinner(codeList, shoppingCart);
        Console.WriteLine(foo);
    }
}