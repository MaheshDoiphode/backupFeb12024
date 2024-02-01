package Day1.Arrays.Part1.PascalTriangle;

/*
* https://takeuforward.org/data-structure/program-to-generate-pascals-triangle/
* */
public class Variation1 {
    public static void main(String[] args){
        int r = 5;
        int c = 3;
        int element = pascalTriangle(r, c);
        System.out.println("Element at r, c is : " + element);
    }//- main
	
	
	static int pascalTriangle(int r, int c){
		int element = (int) nCr(r-1, c-1);
		return element;
	}//- pascalTriangle end
	
	static long nCr (int n, int r){
		long res = 1;
		for(int i=0; i<r; i++){
			res = res * (n-i);
			res = res / (i+1);
		}
		return res;
	}
	

}
