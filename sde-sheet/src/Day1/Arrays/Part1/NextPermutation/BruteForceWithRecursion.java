package Day1.Arrays.Part1.NextPermutation;
/*
https://takeuforward.org/data-structure/next_permutation-find-next-lexicographically-greater-permutation/
Approach :
Step 1: Find all possible permutations of elements present and store them.

Step 2: Search input from all possible permutations.

Step 3: Print the next permutation present right after it.

*/

import java.util.*;
import java.util.stream.Collectors;

public class BruteForceWithRecursion {
    public static void main(String[] args) {
        int[] nums = new int[]{1, 3, 2};
        Arrays.sort(nums);
        Solution sol = new Solution();
        List<List<Integer>> ls = sol.permute(nums);
        List<Integer> num = Arrays.stream(nums).boxed().toList();
        int a = 0, i = 0;
        for (List<Integer> l : ls) {
            if (l.equals(num)) {
                a = i+1;
            }
            for (Integer ele : l) {
                System.out.print(ele + " ");
            }
            System.out.println();
            i++;
        }
        if(a>=num.size()){
            System.out.println("Next Permutation is : " + ls.get(0));
        }else{
            System.out.println("Next Permutation is : " + ls.get(a));
        }

    }

}

class Solution {

    public List<List<Integer>> permute(int[] nums) {
        List<List<Integer>> ans = new ArrayList<>();
        recurPermute(0, nums, ans);
        return ans;
    }

    private void recurPermute(int index, int[] nums, List<List<Integer>> ans) {
        if (index == nums.length) {
            // Copy the ds to ans
            List<Integer> ds = new ArrayList<>();
            for (int num : nums) {
                ds.add(num);
            }
            ans.add(new ArrayList<>(ds));
            return;
        }
        for (int i = index; i < nums.length; i++) {
            swap(i, index, nums);
            recurPermute(index + 1, nums, ans);
            swap(i, index, nums);
        }
    }

    private void swap(int i, int j, int[] nums) {
        int t = nums[i];
        nums[i] = nums[j];
        nums[j] = t;
    }
}

