package pkg1;

public class StackLL {
    CLL ll = new CLL();
    public int push (int data){
        return ll.addAtEnd(data).data;
    }
    public int pop(){
        return ll.deleteFromEnd();
    }
    public int peek(){
        return ll.tail.data;
    }
    public boolean isEmpty(){
        return ll.head==null;
    }
    public void display(){
        ll.display();
    }

}

