using System;
using System.Collections.Concurrent;
using System.Threading;


public class Queue<T>
{

    private T[] queue;

    private int head = 0;
    private int tail = 0;
    private int size;
    private int TIMEOUT = 2;

    public Queue(int size = 10)
    {
        this.size = size;
        queue = new T[size];
    }

    public void Enqueue(T element)
    {
        if (tail > size)
        throw new Exception("Queue Overflow");

        queue[tail] = element;
        if (tail == size)
        tail = 1;
        else
        tail++;
    }

    public T Dequeue()
    {
        T element = queue[head];
        if (head == size)
        head = 1;
        else
        head++;

        return element;
    }
}