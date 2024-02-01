package Day1.Arrays.Part1.SetMatrixZero;
import java.util.*;
// https://takeuforward.org/data-structure/set-matrix-zero/
// Problem Statement: Given a matrix if an element in the matrix is 0
// then you will have to set its entire column and row to 0 and then return the matrix.

// Approach
// First, we will use two loops(nested loops) to traverse all the cells of the matrix.
//If any cell (i,j) contains the value 0, we will mark all cells in row i and column j with -1 except those which contain 0.
//We will perform step 2 for every cell containing 0.
//Finally, we will mark all the cells containing -1 with 0.
// Thus, the given matrix will be modified according to the question.

import java.util.ArrayList;

public class BruteForce {
    public static void main(String[] args){
        ArrayList<ArrayList<Integer>> matrix = new ArrayList<>();
		matrix.add(new ArrayList<>(Arrays.asList(1, 1, 1)));
		matrix.add(new ArrayList<>(Arrays.asList(1, 0, 1)));
		matrix.add(new ArrayList<>(Arrays.asList(1, 1, 1)));
		int n = matrix.size();
		int m = matrix.get(0).size();
		
		ArrayList<ArrayList<Integer>> ans = zeroMatrix(matrix, n, m);
		System.out.println("The final matrix");
		for(ArrayList<Integer> row : ans ){
			for(Integer ele : row){
				System.out.print(ele + " ");
			}
			System.out.println();
		}
		
    }
	
	
	static ArrayList<ArrayList<Integer>> zeroMatrix(ArrayList<ArrayList<Integer>> matrix, int n, int m){
		for(int i=0; i<n; i++){
			for(int j=0; j<m; j++){
				if(matrix.get(i).get(j) == 0){
					markRow(matrix, n, m, i);
					markCol(matrix, n, m, j);
				}
			}
		}
		
		// finally, mark all -1 as 0
		for(int i=0; i<n; i++){
			for(int j=0; j<m; j++){
				if(matrix.get(i).get(j) == -1){
					matrix.get(i).set(j, 0);
				}
			}
		}
		return matrix;
	}
	
	static void markRow(ArrayList<ArrayList<Integer>> matrix, int n, int m, int i){
		for(int j=0; j<m; j++){
			if(matrix.get(i).get(j) != 0){
				matrix.get(i).set(j, -1);
			}
		}
	}
	static void markCol(ArrayList<ArrayList<Integer>> matrix, int n, int m, int j){
		for(int i=0; i<n; i++){
			if(matrix.get(i).get(j) != 0){
				matrix.get(i).set(j, -1);
			}
		}
	}
 
}
