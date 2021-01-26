namespace Suggester.APIv2{
    public class Suggestion{
        public int Id{get; set;}
        public int Sid{get; set;}
        public string Text{get; set;}
        public bool IsAnswered{get; set;}
        public bool Like{get; set;}
    }
}