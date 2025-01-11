namespace Examination_management_system
{
    class CompleteTheSentence : Question
    {
        public string CorrectAnswer { get; set; }

        public CompleteTheSentence(string text, string correctAnswer, string difficulty)
            : base(text, difficulty)
        {
            CorrectAnswer = correctAnswer;
        }

        public override bool CheckAnswer(string answer)
        {
            return CorrectAnswer.ToLower() == answer.ToLower();
        }

    }
}


