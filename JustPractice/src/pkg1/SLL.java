package pkg1;

import java.util.Scanner;

public class SLL{
    public static void main(String[] args) {
        CLL ll = new CLL();
        String dec = "y";
        Scanner sc= new Scanner(System.in);
        ll.display();
        while(dec.equals("y")){
            System.out.println("What? 1. Add at start, 2. Add at end, 3. Insert Before, 4. Display, 5. delete, 6. update");
            int ch = sc.nextInt();
            switch(ch){
                case 1:
                    ll.addAtStartNode(sc.nextInt());
                    ll.display();
                    break;
                case 2:
                    ll.addAtEnd(sc.nextInt());
                    ll.display();
                    break;
                case 3:
                    ll.insertBefore(sc.nextInt(), sc.nextInt());
                    ll.display();
                    break;
                case 4:
                    ll.display();
                    break;
                case 5:
                    ll.delete(sc.nextInt());
                    ll.display();
                    break;
                case 6 :
                    ll.update(sc.nextInt(), sc.nextInt());
                    break;
            }
            System.out.println("Wanna cont :: yn");
            dec = sc.next();
        }
    }

}
