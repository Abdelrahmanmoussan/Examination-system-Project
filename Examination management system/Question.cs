namespace Examination_management_system
{
    abstract class Question
    {
        public string Text { get; set; }
        public string Difficulty { get; set; }

        protected Question(string text, string difficulty)
        {
            Text = text;
            Difficulty = difficulty;
        }

        public abstract bool CheckAnswer(string answer);
    }
}


