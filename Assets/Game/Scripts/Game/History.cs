using System.Collections.Generic;

public class History
{
    public Stack<AnimalCannon> animals = new Stack<AnimalCannon>();

    public void Push(AnimalCannon cannon)
    {
        animals.Push(cannon);
    }

    public AnimalCannon Pop()
    {
        if(animals.Count > 0) {
            return animals.Pop();
        }
        return null;
    }
    public void Clear()
    {
        animals.Clear();
    }
}