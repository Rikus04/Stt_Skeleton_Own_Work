 public class Packer
    {
        static string pack(string filePath)
        {

            double weightAllowed;

            int itemCount = 0; // counts all items 
            double[,] items = new double[15, 3]; //  value in 2D array 0 = index , 1 = weight , 2 = costing
            string rline; // clone of line in foreach to be able to cut and munipilate line 

            //final output variables
            string betterItemsReturn = ""; //return variable
            double[,] betterItem = new double[15, 3]; // allows for cloning of item in packages, will allow for further working with items in package as required.
            double totalWeight = 0; //variable to compare versus weight constraint                  
            int betterItemCount = 1; // sets initial value to 1 as 1 instance is passed to compare on for loop
            bool allItems = false; //variable to end when all instances has been compaired

            try
            {

                string[] lines = File.ReadAllLines(filePath);

                // Iterate over each line using foreach
                foreach (string line in lines)
                {

                    //method created through VS , cleans up code
                    indexItems(out weightAllowed, out itemCount, items, out rline, line, out betterItem, out totalWeight);
                    while (Convert.ToDouble(totalWeight) < weightAllowed && (allItems != true))
                    {
                        Console.WriteLine("Looping");

                        // initializes values to have something to compare against.
                        for (int o = 0; o < 3; o++)
                        {
                            betterItem[0, o] = items[0, o];
                        }


                        //compares pricing to determine higher pricing only if the weight is also better will the line be added to the betterItem
                        //for loop for all items counted in line
                        for (int i = 0; i < itemCount; i++)
                        {
                            totalWeight = 0; // Initially set weight to 0

                            for (int j = 0; j < betterItemCount; j++)
                            {
                                if ((items[i, 2] != 0) && //checks that the array of 15 does not have blanks being filled into better item
                                    (items[i, 2] > betterItem[j, 2]) && // pricing constraint - item price should be greater
                                    (items[i, 1] <= weightAllowed)) // weight constraint - item weight should be less than or equal to maximum allowed weight
                                {
                                    //if all constraints are met item is added to better item
                                    for (int k = 0; k < 3; k++)
                                    {
                                        betterItem[j, k] = items[i, k];
                                    }

                                    betterItemCount++;
                                }

                                Console.ReadLine();
                            }
                            // checks end of all items counted in line if all items compaired breaks while loop
                            if (i == itemCount - 1)
                            {
                                allItems = true;
                            }

                            totalWeight = 0; // will set totalWeight back to 0 after all lines compaired

                            for (int l = 0; l < betterItem.GetLength(0); l++)
                            {
                                if (betterItem[l, 0] != 0)
                                {
                                    betterItemsReturn += betterItem[l, 0]; // will be used to return items to return
                                }
                            }
                        }
                    }
                    betterItemsReturn += "\n";
                }

                return betterItemsReturn;
            }
            catch (Exception e) // no need for 15 item constraint to be messaged as the exception
                                // would catch it.
            {
                Console.WriteLine(e.Message);
                return " ";
            }


        }

        private static void indexItems(out double weightAllowed, out int itemCount, double[,] items, out string rline, string line, out double[,] betterItem, out double totalWeight)
        {
            rline = line;

            itemCount = 0; // will reset item count after each line is read.
            Console.WriteLine(rline); // testing

            weightAllowed = Convert.ToDouble(rline.Substring(0, rline.IndexOf(':')));
            rline = rline.Remove(0, rline.IndexOf(':') + 2);
            Console.WriteLine("Weight Allowed : " + weightAllowed);

            while (rline.Length > 0)
            {
                //Allocates Index to 2D array
                items[itemCount, 0] = Convert.ToDouble(rline.Substring(1, rline.IndexOf(",") - 1));
                Console.WriteLine("Index: " + items[itemCount, 0]);
                rline = rline.Remove(0, rline.IndexOf(',') + 1);
                //Console.WriteLine(rline);

                //Allocates Weight in 2D array
                items[itemCount, 1] = Convert.ToDouble(rline.Substring(0, rline.IndexOf(",")), CultureInfo.InvariantCulture);
                Console.WriteLine("Weight: " + items[itemCount, 1]);
                rline = rline.Remove(0, rline.IndexOf(',') + 2);
                //Console.WriteLine(rline);

                //Allocates Pricing in 2D array
                items[itemCount, 2] = Convert.ToDouble(rline.Substring(0, rline.IndexOf(")")));
                Console.WriteLine("Pricing: Euro " + items[itemCount, 2]);
                if (rline.Length != rline.IndexOf(")") + 1)
                {
                    rline = rline.Remove(0, rline.IndexOf(')') + 2);
                    //Console.WriteLine(rline);
                }
                else
                    rline = "";

                itemCount++;

                if (Convert.ToDouble(items[itemCount, 2]) > 100)
                {
                    Console.WriteLine(" Max cost of an item is ≤ 100");
                    Console.ReadLine();
                }
            }


            string indexResults = ""; // string to be used to return results
            betterItem = new double[15, 3];
            totalWeight = 0;
            // once more using string because of e
        }
    }
    private static void indexItems(out double weightAllowed, out int itemCount, double[,] items, out string rline, string line, out double[,] betterItem, out double totalWeight)
    {
        rline = line;

        itemCount = 0; // will reset item count after each line is read.
        Console.WriteLine(rline); // testing

        weightAllowed = Convert.ToDouble(rline.Substring(0, rline.IndexOf(':')));
        rline = rline.Remove(0, rline.IndexOf(':') + 2);
        Console.WriteLine("Weight Allowed : " + weightAllowed);

        while (rline.Length > 0)
        {
            //Allocates Index to 2D array
            items[itemCount, 0] = Convert.ToDouble(rline.Substring(1, rline.IndexOf(",") - 1));
            Console.WriteLine("Index: " + items[itemCount, 0]);
            rline = rline.Remove(0, rline.IndexOf(',') + 1);
            //Console.WriteLine(rline);

            //Allocates Weight in 2D array
            items[itemCount, 1] = Convert.ToDouble(rline.Substring(0, rline.IndexOf(",")), CultureInfo.InvariantCulture);
            Console.WriteLine("Weight: " + items[itemCount, 1]);
            rline = rline.Remove(0, rline.IndexOf(',') + 2);
            //Console.WriteLine(rline);

            //Allocates Pricing in 2D array
            items[itemCount, 2] = Convert.ToDouble(rline.Substring(0, rline.IndexOf(")")));
            Console.WriteLine("Pricing: Euro " + items[itemCount, 2]);
            if (rline.Length != rline.IndexOf(")") + 1)
            {
                rline = rline.Remove(0, rline.IndexOf(')') + 2);
                //Console.WriteLine(rline);
            }
            else
                rline = "";

            itemCount++;

            if (Convert.ToDouble(items[itemCount, 2]) > 100)
            {
                Console.WriteLine(" Max cost of an item is ≤ 100");
                Console.ReadLine();
            }
        }


        string indexResults = ""; // string to be used to return results
        betterItem = new double[15, 3];
        totalWeight = 0;
        // once more using string because of e
    }