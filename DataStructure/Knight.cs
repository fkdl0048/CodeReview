namespace DataStructure
{
    class Knight : IComparable<Knight>
    {
        public int ID {get; set;}

        public int CompareTo(Knight? other)
        {
            if (ID == other.ID)
                return 0;

            return ID < other.ID ? 1 : -1;
        }
    }
}