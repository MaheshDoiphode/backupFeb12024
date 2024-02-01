public class Main {
    public static void main(String[] args) {
        MyThread thread = new MyThread();
        thread.start();
        thread.interrupt();
        for (int i=0; i<100; i++){
            System.out.println(Thread.currentThread().getName() + " : " + i );
        }

    }
}


class MyThread extends Thread{

    public void run(){
        Thread.currentThread().setName("child");
        try{
            Thread.sleep(1000);
        }catch (InterruptedException e){
            System.out.println("interrrupted");
        }
        for (int i=0; i<100; i++){
            System.out.println(Thread.currentThread().getName() + " : " + i );
        }
    }
}