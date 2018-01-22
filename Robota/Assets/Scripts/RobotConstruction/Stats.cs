public interface Stats
{
    Stats AtLeastZero();
    int StatCount();
    int GetStatByIndex(int index);
    string GetStatNameByIndex(int index);
}
