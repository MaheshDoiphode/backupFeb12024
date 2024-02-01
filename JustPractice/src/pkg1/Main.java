package pkg1;

import java.util.Arrays;
import java.util.Objects;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        // __---------------------- Stack part
        /*StackLL stack = new StackLL();
        String dec = "y";
        while(dec.equals("y")){
            System.out.println("What? 1. Push, 2. POP, 3. Peek, 4. Display,");
            int ch = sc.nextInt();
            switch(ch){
                case 1:
                    stack.push(sc.nextInt());
                    stack.display();
                    break;
                case 2:
                    stack.pop();
                    stack.display();
                    break;
                case 3:
                    System.out.println(stack.peek());
                    break;
                case 4:
                    stack.display();
                    break;
                default :
                    System.out.println("get off");
                    break;
            }

            System.out.println("Wanna cont - y n:: ");
            dec = sc.next();
        }*/
        //- ________________Stack

        //--------------------- QUEUE
        /*QueueLL queue = new QueueLL();
        String dec = "y";
        while (dec.equals("y")) {
            System.out.println("What? 1. Enqueue, 2. Dequeue, 3. front, 4. rear, 5. Display, 6. isEmpty");
            int ch = sc.nextInt();
            switch (ch) {
                case 1:
                    System.out.println(queue.enqueue(sc.nextInt()));
                    queue.display();
                    break;
                case 2:
                    System.out.println(queue.dequeue());
                    queue.display();
                    break;
                case 3:
                    System.out.println(queue.getFront());
                    break;
                case 4:
                    System.out.println(queue.getRear());
                    break;
                case 5:
                    queue.display();
                    break;
                case 6:
                    System.out.println(queue.isEmpty());
                    break;
                default:
                    System.out.println("get off");
                    break;
            }

            System.out.println("Wanna cont - y n:: ");
            dec = sc.next();
        }*/
        //--------------------- QUEUE

        //--------------------- Binary Tree
        /*BinaryTree tree = new BinaryTree();
        tree.populate(sc);
        tree.prettyDisplay();
        */
        //------------ Binary Tree

        //------------- BinaryTreeMIT
        BinaryTreeMIT tree = new BinaryTreeMIT();
        tree.init(tree);
        tree.display(tree.root);
        tree.insertBefore(tree.root.left, sc.nextInt());
        tree.display(tree.root);
        tree.insertBefore(tree.root.right.left, sc.nextInt());
        tree.display(tree.root);

        //------------ BinaryTreeMIT







    }//- Main


}