using System;

using System.Timers;


namespace Examination_management_system
{

    internal class Program
    {
        static List<Question> questions = new();
        static Dictionary<int, string> studentAnswers = new();
        static System.Timers.Timer examTimer;






        static void Main(string[] args)
        {
            while (true) // while لما تبقي true بتخلي بعد ما المدرس بيخلص اسئله يعيد السوال هل عاوز تحول لوضع الطالب ولا نقفل النظام 
            {
                Console.WriteLine("Select mode:\n1- Teacher mode \n2- Student mode");
                int choose = int.Parse(Console.ReadLine());

                if (choose == 1)
                {
                    TeacherMode();
                }
                else if (choose == 2)
                {
                    StudentMode();
                }
                else
                {
                    Console.WriteLine("Please Choose again");
                }
            }
        }
        static void TeacherMode()
        {
            Console.WriteLine("Write the name of the exam subject: ");
            string examName = Console.ReadLine();

            Console.WriteLine("Inter how many questions do you want in Exam: ");
            int NumOfQuestion = int.Parse(Console.ReadLine());

            for (int i = 0; i < NumOfQuestion; i++)
            {

                Console.WriteLine("Select question Type:\n1- Choose the currect answer \n2- True or False \n3- Fill in the Blank ");
                int questionType = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the question difficulty level:\n1- Easy\n2- Medium\n3- Hard");
                int levelQuestion = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the question text: ");
                string questionText = Console.ReadLine();

                string difficulty = levelQuestion switch
                {
                    1 => "Easy",
                    2 => "Medium",
                    3 => "Hard",
                    _ => "Medium" // معلومه وصلتلها ان _ بتخلي الوضع الافتراضي للامتحان هو medium
                };

                if (questionType == 1)
                {
                    Console.WriteLine("Select the correct answer (separate options with commas):");
                    string[] options = Console.ReadLine().Split(',');   //لازم نحط الفصله دي عشان يفصل الاختيارات عن بعض 

                    Console.WriteLine("Enter the correct answer:");
                    string correctAnswer = Console.ReadLine();

                    questions.Add(new ChooseOneCorrectAnswer(questionText, options, correctAnswer, difficulty));
                }
                else if (questionType == 2)
                {


                    Console.WriteLine("True or False");
                    string correctAnswer = Console.ReadLine();

                    questions.Add(new TrueOrFalseQuestion(questionText, correctAnswer, difficulty));
                }
                else if (questionType == 3)
                {
                    Console.WriteLine("Enter the correct answer");
                    string correctAnswer = Console.ReadLine();


                    questions.Add(new CompleteTheSentence(questionText, correctAnswer, difficulty));
                }
                else
                {
                    Console.WriteLine("Invalid question type");
                }
            }
            Console.WriteLine("Questions saved successfully");

            Console.WriteLine("\n Do you want to switch to student mode? press 'y' to switch, if you don't switch press any key to exit");
            string key = Console.ReadLine();
            if (key == "y")
            {
                StudentMode();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        static void StudentMode()
        {
            if (questions.Count == 0)
            {
                Console.WriteLine("No questions available. Please add questions first.");
                return;
            }

            Console.WriteLine("The exam will start now. You have 30 minutes to complete it.");


            examTimer = new System.Timers.Timer(30 * 60 * 1000);   //30 دقيقه بالظبط
            examTimer.Elapsed += EndExam;    // هنا بنربط الامتحان ب ان الوقت المحدد خلص
            examTimer.AutoReset = false;    // تاكيد ان الوقت شغال مره واحده
            examTimer.Start();             // بدا العد التنازلي للوقت المحدد للامتحان 

            Console.WriteLine("Start answering the questions:");

            foreach (var question in questions)
            {
                Console.WriteLine($"\n{question.Text}");

                if (question is ChooseOneCorrectAnswer mcq)
                {
                    for (int i = 0; i < mcq.Options.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}- {mcq.Options[i]}"); // {i + 1} عشان ارقم كل اختيار
                    }
                }

                Console.WriteLine("Enter your answer:");
                string answer = Console.ReadLine();
                studentAnswers.Add(questions.IndexOf(question), answer);
            }

            EndExam(null, null); 
        }
        static void EndExam(object sender, ElapsedEventArgs e)
        {
            examTimer.Stop();

            Console.WriteLine("\nTime's up! The exam is over.");

            int correctAnswers = 0;

            for (int i = 0; i < questions.Count; i++)
            {
                if (studentAnswers.ContainsKey(i))
                {
                    string studentAnswer = studentAnswers[i];
                    if (questions[i].CheckAnswer(studentAnswer))
                    {
                        correctAnswers++;
                    }
                }
            }

            double percentage = (correctAnswers * 100) / questions.Count;

            string grade = percentage >= 90 ? "Excellent" :
                           percentage >= 75 ? "Very Good" :
                           percentage >= 60 ? "Good" :
                           percentage >= 50 ? "Pass" : "Fail";

            Console.WriteLine($"Your final score: {correctAnswers}/{questions.Count} ({percentage}%) - {grade}");

            Environment.Exit(0);
        }

    }
}


