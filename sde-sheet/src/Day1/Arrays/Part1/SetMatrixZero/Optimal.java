package Day1.Arrays.Part1.SetMatrixZero;
import java.util.*;
/*
Approach:
The steps are as follows:

First, we will traverse the matrix and mark the proper cells of 1st row and 1st column with 0 accordingly. The marking will be like this: if cell(i, j) contains 0, we will mark the i-th row i.e. matrix[i][0] with 0 and we will mark j-th column i.e. matrix[0][j] with 0.
If i is 0, we will mark matrix[0][0] with 0 but if j is 0, we will mark the col0 variable with 0 instead of marking matrix[0][0] again.
After step 1 is completed, we will modify the cells from (1,1) to (n-1, m-1) using the values from the 1st row, 1st column, and col0 variable.
We will not modify the 1st row and 1st column of the matrix here as the modification of the rest of the matrix(i.e. From (1,1) to (n-1, m-1)) is dependent on that row and column.
Finally, we will change the 1st row and column using the values from matrix[0][0] and col0 variable. Here also we will change the row first and then the column.
If matrix[0][0] = 0, we will change all the elements from the cell (0,1) to (0, m-1), to 0.
If col0 = 0, we will change all the elements from the cell (0,0) to (n-1, 0), to 0.
Observations: Why in the second step, we are first marking the matrix from the cell (1,1) to (n-1, m-1) and not from (0,0):
*/
public class Optimal {
	public static void main(String[] args){
		ArrayList<ArrayList<Integer>> matrix = new ArrayList<>();
		matrix.add(new ArrayList<>(Arrays.asList(1, 1, 1)));
		matrix.add(new ArrayList<>(Arrays.asList(1, 0, 1)));
		matrix.add(new ArrayList<>(Arrays.asList(1, 1, 1)));

		int n = matrix.size();
		int m = matrix.get(0).size();
		ArrayList<ArrayList<Integer>> ans = zeroMatrix(matrix, m, n);

		for(ArrayList<Integer> list : ans){
			for(Integer ele : list){
				System.out.print(ele + " ");
			}
			System.out.println();
		}

	}//- main meth

	static ArrayList<ArrayList<Integer>> zeroMatrix(ArrayList<ArrayList<Integer>> matrix, int m, int n){
		int col0 = 1;

		// step 1 : Traverse the matrix
		// mark 1st row and col accodringly.
		for(int i=0; i<n; i++){
			for(int j=0; j<m; j++){
				if(matrix.get(i).get(j) == 0){
					// mark ith row
					matrix.get(i).set(0, 0);
					// mark jth col
					if(j != 0){
						matrix.get(0).set(j, 0);
					}else{
						col0 = 0;
					}

				}
			}
		}
		// step 2 : Mark with 0 from (1, 1) to (n-1, m-1)
		for(int i=1; i<n; i++){
			for(int j=1; j<m; j++){
				if(matrix.get(i).get(j) != 0){
					// check for col and row
					if(matrix.get(i).get(0)==0 || matrix.get(0).get(j) == 0){
						matrix.get(i).set(j, 0);
					}
				}
			}
		}

		//Step 3: Finally mark the 1st col & then 1st row :
		if(matrix.get(0).get(0) == 0 ){
			for(int j=0; j<m; j++){
				matrix.get(0).set(j, 0);
			}
		}
		if(col0 == 0){
			for(int i=0; i>n; i++){
				matrix.get(i).set(0, 0);
			}
		}
		return matrix;
	}//- zeroMatrix end


}//- Optimal end

/**


 */
