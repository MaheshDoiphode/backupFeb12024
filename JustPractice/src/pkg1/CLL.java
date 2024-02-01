package pkg1;

class Node {
    int data;
    Node next;

    public int getData() {
        return data;
    }

    public Node(int data) {
        this.data = data;
    }

    public void setData(int data) {
        this.data = data;
    }

    public Node getNext() {
        return next;
    }

    public void setNext(Node next) {
        this.next = next;
    }

    public Node(int data, Node next) {
        this.data = data;
        this.next = next;
    }
}
public class CLL {
    Node head;
    Node tail;

    // Add
    public Node addAtStartNode(int data) {
        Node node = new Node(data);
        if (head == null) {
            head = node;
            tail = node;
            return node;
        }
        node.next = head;
        head = node;
        return node;
    }

    public Node addAtEnd(int data) {
        Node node = new Node(data);
        if (tail == null) {
            head = node;
            tail = node;
            return node;
        }
        tail.next = node;
        tail = node;
        return node;
    }

    public Node insertBefore(int dataBefore, int data) {
        Node node = new Node(data);
        if (head.data == dataBefore) {
            node.next = head;
            head = node;
            return node;
        }
        Node temp = head;
        while (temp.next != null) {
            if (temp.next.data == dataBefore) {
                break;
            }
            temp = temp.next;
        }
        Node nodeBefore = temp.next;
        temp.next = node;
        node.next = nodeBefore;
        return node;
    }

    public void display() {
        if (head == null) {
            return;
        }
        Node temp = head;
        System.out.println("==================================");
        while (temp.next != null) {
            System.out.print(temp.data + "->");
            temp = temp.next;
        }
        System.out.println(temp.data);
        System.out.println("==================================");
    }

    // Delete
    public int delete(int data) {
        Node temp = head;
        if (getSize() == 1) {
            head = null;
            tail = null;
            return data;
        }
        if (head.data == data) {
            head = head.next;
            return data;
        }
        while (temp != null) {
            if (temp.next.data == data) {
                break;
            }
            temp = temp.next;
        }
        temp.next = temp.next.next;
        return data;
    }

    public int deleteFromEnd(){
        if(tail == null){
            return Integer.MIN_VALUE;
        }
        if(head.next == null){
            int data = head.data;
            head = null;
            return data;
        }
        Node temp = head;
        while(temp.next != tail){
            temp = temp.next;
        }
        tail = temp;
        int data = temp.data;
        temp.next = null;
        return data;
    }



    public int getSize() {
        Node temp = head;
        int count = 1;
        while (temp.next != null) {
            System.out.print(temp.data + "->");
            temp = temp.next;
            count++;
        }
        return count;
    }


    public Node update(int data, int newData) {
        if (head == null) {
            return null;
        }
        Node temp = head;
        while (temp != null) {
            if (temp.data == data) {
                temp.data = newData;
                break;
            }
            temp = temp.next;
        }
        return temp;
    }
}
