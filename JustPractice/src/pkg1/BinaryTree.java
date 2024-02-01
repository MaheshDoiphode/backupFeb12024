package pkg1;

import java.util.Scanner;

public class BinaryTree {

    private static class Node{
        int val;
        Node left;
        Node right;
        public Node(int val) {
            this.val = val;
        }

    }//- Node

    private Node root;

    //Insert elements
    public void populate(Scanner sc){
        System.out.println("Enter the root Node.");
        int val = sc.nextInt();
        root = new Node(val);
        populate(sc, root);
    }
    public void populate(Scanner sc, Node node){
        System.out.println("Do you wanna add left node? to " + node.val + " y/n");
        char ch = sc.next().charAt(0);
        if(ch == 'y'){
            System.out.println("Enter the value to add to the left of " + node.val + " ");
            node.left = new Node(sc.nextInt());
            populate(sc, node.left);
        }
        System.out.println("Do you wanna add right node? to " + node.val + " y/n");
        ch = sc.next().charAt(0);
        if(ch == 'y'){
            System.out.println("Enter the value to add to the right of " + node.val + " ");
            node.right = new Node(sc.nextInt());
            populate(sc, node.right);
        }

    }
    public void prettyDisplay(){
        prettyDisplay(root, 0);
    }
    public void prettyDisplay(Node node, int level){
        if(node == null){
            return;
        }
        prettyDisplay(node.right, level + 1);
        if(level == 0){
            System.out.print(node.val);
        }
        else{
            for(int i=0; i<level-1; i++){
                System.out.print("|    ");
            }
            System.out.println("|--->" + node.val);
        }
        prettyDisplay(node.left, level + 1);
    }

}


