using System;
using System.Collections.Generic;

public class Arrays
{
    // -------------------------------------------------------
    // Part 1: MultiplesOf
    // -------------------------------------------------------

    /*
     * PLAN: MultiplesOf
     * -----------------
     * 1. Create a new double array of length 'count'.
     * 2. Use a for-loop to iterate from 0 to count - 1.
     * 3. For each index i:
     * a. Calculate (i + 1) * number.
     * b. Store the result in the array at index i.
     * 4. After the loop, return the filled array.
     */

    public static double[] MultiplesOf(double number, int count)
    {
        double[] result = new double[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    // -------------------------------------------------------
    // Part 2: RotateListRight 
    // -------------------------------------------------------

    /*
     * PLAN: RotateListRight (using reversals)
     * ---------------------
     * 1. Handle edge cases: If the list is empty, amount is 0, or amount is a multiple of list count,
     * no rotation is needed, so return.
     * 2. Normalize 'amount' to be within the bounds of the list length: amount = amount % data.Count.
     * 3. If 'amount' is now 0 after normalization, return.
     * 4. Reverse the entire list.
     * 5. Reverse the first 'amount' elements of the list.
     * 6. Reverse the remaining 'data.Count - amount' elements of the list.
     * 7. The list is now rotated to the right by the specified 'amount'.
     */

    public static void RotateListRight(List<int> data, int amount)
    {
        int count = data.Count;

        // Handle edge cases: no rotation needed
        if (count == 0 || amount == 0)
        {
            return;
        }

        // Normalize amount to prevent unnecessary full rotations
        // and handle 'amount' greater than 'count'
        amount = amount % count;

        // If amount becomes 0 after normalization, no rotation needed
        if (amount == 0)
        {
            return;
        }

        // Strategy: Reverse the whole list, then reverse parts
        // Example: {1, 2, 3, 4, 5}, amount = 2
        // 1. Reverse all: {5, 4, 3, 2, 1}
        // 2. Reverse first 'amount' (2) elements: {4, 5, 3, 2, 1}
        // 3. Reverse remaining 'count - amount' (3) elements: {4, 5, 1, 2, 3}

        // Step 1: Reverse the entire list
        Reverse(data, 0, count - 1);

        // Step 2: Reverse the first 'amount' elements
        Reverse(data, 0, amount - 1);

        // Step 3: Reverse the remaining 'count - amount' elements
        Reverse(data, amount, count - 1);
    }

    // Helper method to reverse a portion of the list
    private static void Reverse(List<int> list, int start, int end)
    {
        while (start < end)
        {
            int temp = list[start];
            list[start] = list[end];
            list[end] = temp;
            start++;
            end--;
        }
    }
}