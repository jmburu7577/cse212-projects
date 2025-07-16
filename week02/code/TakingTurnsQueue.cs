public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        Person person = _people.Dequeue();

        // Handle infinite turns (0 or less)
        if (person.Turns <= 0)
        {
            _people.Enqueue(person);
        }
        // Handle finite turns
        else if (person.Turns > 1)
        {
            person.Turns -= 1;
            _people.Enqueue(person);
        }
        // if Turns == 1, do not requeue (they're out of turns)

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}
