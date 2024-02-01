package pkg1;

import java.util.NoSuchElementException;

public class QueueLL {

    CLL ll = new CLL();
    public int enqueue(int data){
        return ll.addAtStartNode(data).data;
    }
    public int dequeue() {
        return ll.deleteFromEnd();
    }

    public boolean isEmpty(){
        return ll.head == null;
    }
    public int getRear(){
        if(isEmpty()){
            throw new NoSuchElementException("Queue UnderFlow.");
        }
        return ll.head.data;
    }

    public int getFront(){
        if(isEmpty()){
            throw new NoSuchElementException("Queue UnderFlow for front.");
        }
        return ll.tail.data;
    }

    public void display(){
        ll.display();
    }

}
