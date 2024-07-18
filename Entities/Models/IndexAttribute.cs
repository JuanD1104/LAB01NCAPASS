namespace DAL.Models
{
    internal class IndexAttribute : Attribute
    {
        private string v;
        private string name;

        public IndexAttribute(string v, string Name)
        {
            this.v = v;
            name = Name;
        }

        public string Name { get; set; }
    }
}