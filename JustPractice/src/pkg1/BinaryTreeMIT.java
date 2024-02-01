package pkg1;

public class BinaryTreeMIT {
    class Node {
        int data;
        Node left;
        Node right;
        Node parent;

        public Node(int data) {
            this.data = data;
        }
    }

    Node root;
    int height;

    public Node delete(Node node){
        // Can delete the leaf node only to maintain the traversal order.
        Node temp;
        if(node.left != null){
            temp = predecessor(node.left);
        }else
        if(node.right != null){
            temp = successor(node.right);
        }
        int data = node.data;
        node.data = temp.data;
    }
    public Node insertAfter(Node node, int data){
        if(node.right != null){
            node = subtreeFirst(node.right);
            node.left = new Node(data);
            node.left.parent = node;
            return node;
        }
        node.right = new Node(data);
        node.right.parent = node;
        return node;
    }

    public Node insertBefore(Node node, int data){
        if(node.left != null){
            node = subtreeLast(node.left);
            node.right = new Node(data);
            node.right.parent = node;
            return node;
        }
        node.left = new Node(data);
        node.left.parent = node;
        return node;
    }
    public Node subtreeFirst(Node node){
        if (node.left != null){
            return subtreeFirst(node.left);
        }
        return node;
    }
    public Node subtreeLast(Node node){
        if(node.right != null){
            return subtreeLast(node.right);
        }
        return node;
    }
    public Node successor(Node node){
        if(node.right != null){
            return subtreeFirst(node.right);
        }
        while(node.parent != null && node.parent.right == node){
            node = node.parent;
        }
        return node.parent;
    }

    public Node predecessor(Node node){
        if(node.left != null){
            return subtreeLast(node.left);
        }
        while(node.parent == null && node.parent.left == node){
            node = node.parent;
        }
        return node.parent;
    }

    public void display(Node root){
        if (root == null){
            return;
        }
        display(root.left);
        System.out.print(" " + root.data);
        display(root.right);
    }

    public void init(BinaryTreeMIT bt) {
        Node node = new Node(1);
        root = node;

        // Level 1
        root.left = new Node(2);
        root.right = new Node(3);

        // Set parent for level 1 nodes
        root.left.parent = root;
        root.right.parent = root;

        // Level 2
        root.left.left = new Node(4);
        root.left.right = new Node(5);
        root.right.left = new Node(6);
        root.right.right = new Node(7);

        // Set parent for level 2 nodes
        root.left.left.parent = root.left;
        root.left.right.parent = root.left;
        root.right.left.parent = root.right;
        root.right.right.parent = root.right;

        // Level 3
        root.left.left.left = new Node(8);
        root.left.left.right = new Node(9);
        root.left.right.left = new Node(10);
        root.left.right.right = new Node(11);
        root.right.left.left = new Node(12);
        root.right.left.right = new Node(13);
        root.right.right.left = new Node(14);
        root.right.right.right = new Node(15);

        // Set parent for level 3 nodes
        root.left.left.left.parent = root.left.left;
        root.left.left.right.parent = root.left.left;
        root.left.right.left.parent = root.left.right;
        root.left.right.right.parent = root.left.right;
        root.right.left.left.parent = root.right.left;
        root.right.left.right.parent = root.right.left;
        root.right.right.left.parent = root.right.right;
        root.right.right.right.parent = root.right.right;
        height = computeHeight(root);
    }

    public int computeHeight(Node node) {
        if (node == null) {
            return -1;
        }

        int leftHeight = computeHeight(node.left);
        int rightHeight = computeHeight(node.right);

        return 1 + Math.max(leftHeight, rightHeight);
    }


}
