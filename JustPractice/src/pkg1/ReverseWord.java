package pkg1;

import java.util.*;

public class ReverseWord {

    public static void main(String[] args) {

         //String ss = "   The Sky   is Blue  ";
        System.out.println(maxOperations(new int[]{3,1,3,4,3}, 6));
    }


    public static int maxOperations(int[] nums, int k) {
        int count = 0;
        Map<Integer, Integer> map = new HashMap<>();
        for (int num : nums) {
            if (map.getOrDefault(k - num, 0) > 0) {
                count++;
                map.put(k - num, map.get(k - num) - 1);
            } else {
                map.put(num, map.getOrDefault(num, 0) + 1);
            }
        }
        return count;
    }


    //--------------------------------

    public static String reverseWords(String s) {
        char[] arr = s.toCharArray();

        // Step 1: Reverse the entire string
        reverse(arr, 0, arr.length - 1);

        // Step 2: Reverse each word
        int start = 0;
        for (int end = 0; end < arr.length; end++) {
            if (arr[end] == ' ') {
                reverse(arr, start, end - 1);
                start = end + 1;
            }
        }

        // Reverse the last word
        reverse(arr, start, arr.length - 1);

        // Step 3: Remove extra spaces
        int i = 0, j = 0;
        while (j < arr.length) {
            while (j < arr.length && arr[j] == ' ') j++;  // Skip spaces
            while (j < arr.length && arr[j] != ' ') arr[i++] = arr[j++];  // Keep non-spaces
            while (j < arr.length && arr[j] == ' ') j++;  // Skip spaces
            if (j < arr.length) arr[i++] = ' ';  // Keep only one space
        }

        return new String(arr, 0, i);
    }

    private static void reverse(char[] arr, int start, int end) {
        while (start < end) {
            char temp = arr[start];
            arr[start] = arr[end];
            arr[end] = temp;
            start++;
            end--;
        }
    }

}
