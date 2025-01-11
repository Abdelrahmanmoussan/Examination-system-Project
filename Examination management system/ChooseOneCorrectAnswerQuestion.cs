namespace Examination_management_system
{
    class ChooseOneCorrectAnswer : Question
    {
        public string[] Options { get; set; }
        public string CorrectAnswer { get; set; }

        public ChooseOneCorrectAnswer(string text, string[] options, string correctAnswer, string difficulty)
            : base(text, difficulty)
        {
            Options = options;
            CorrectAnswer = correctAnswer;
        }

        public override bool CheckAnswer(string answer)
        {
            return CorrectAnswer.ToLower() == answer.ToLower();
        }

    }
}


