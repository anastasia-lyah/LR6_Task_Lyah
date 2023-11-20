using System;
class StudZ //студент, що складає залік
{
    private bool[] results; //результат здачі заліку
    public StudZ(bool result1, bool result2, bool result3) //конструктор
    {
        results = new bool[] { result1, result2, result3 };
    }
    //властивостi для читання значень полiв
    public bool Result1 => results[0];
    public bool Result2 => results[1];
    public bool Result3 => results[2];
}
class StudE //судент, що складає іспит
{
    private int[] grades; //оцінки 
    public StudE(int grade1, int grade2, int grade3) //конструктор
    {
        grades = new int[] { grade1, grade2, grade3 };
    }
    //властивостi для читання значень полiв
    public int Grade1 => grades[0];
    public int Grade2 => grades[1];
    public int Grade3 => grades[2];
}
class Za4et //залік
{
    private StudZ student;
    private bool sessionPassed; //результат здачі залікової сесії
                                //
    public event EventHandler Event1; //студент здав усi залiки
    public event EventHandler Event2; //студент здав не всi залiки
    public bool SessionPassed => sessionPassed; //властивість для читання
    public Za4et(StudZ student) //конструктор із параметром класу StudZ
    {
        this.student = student;
        this.sessionPassed = false;
    }
    public void ProcessResults()
    {
        //якщо всi залiки зданi, то логiчному полю надається значення true i генерується подiя Event1
        if (student.Result1 && student.Result2 && student.Result3)
        {
            sessionPassed = true;
            Event1?.Invoke(this, EventArgs.Empty);
        }
        //якщо зданi не всi залiки, то генерується подiя Event2
        else
        {            
            Event2?.Invoke(this, EventArgs.Empty);
        }
    }
}
class Exam //іспит
{
    private StudE student;

    public event EventHandler Event3; //студент склав усi iспити
    public event EventHandler Event4; //студент склав не всi iспити    
    public Exam(StudE student) //конструктор iз параметром класу StudE
    {
        this.student = student;
    }    
    public void ProcessResults()
    {
        //якщо студент не має двiйок, то генерується подiя Event3
        if (!(student.Grade1 == 2 || student.Grade2 == 2 || student.Grade3 == 2))
        {
            Event3?.Invoke(this, EventArgs.Empty);
        }
        //iнакше генерується подiя Event4
        else
        {
            Event4?.Invoke(this, EventArgs.Empty);
        }
    }
}
class Prepod //викладач
{    
    public void OnEvent1(object sender, EventArgs e) //обробник подiї Event1
    {
        Console.WriteLine("Залiки здав. Допускається до iспитiв");
    }

    public void OnEvent2(object sender, EventArgs e) //обробник подiї Event2
    {
        Console.WriteLine("Не всi залiки здав. До iспитiв не допускається");
    }

    public void OnEvent3(object sender, EventArgs e) //обробник подiї Event3
    {
        Console.WriteLine("Iспити склав. Переведено на наступний курс");
    }

    public void OnEvent4(object sender, EventArgs e) //обробник подiї Event4
    {
        Console.WriteLine("Не всi iспити склав. На перездачу");
    }
}
class Program
{
    static void Main()
    {
        //випадково створюються значення трьох логiчних змiнних
        Random random = new Random();
        bool result1 = random.Next(2) == 0;
        bool result2 = random.Next(2) == 0;
        bool result3 = random.Next(2) == 0;        
        Console.WriteLine($"Залiки: {result1}, {result2}, {result3}");

        //об'єкт класу StudZ
        StudZ studentZ = new StudZ(result1, result2, result3);

        //об'єкт класу Za4et iз створеним об'єктом класу StudZ як параметр
        Za4et za4et = new Za4et(studentZ);

        //об'єкт класу Prepod
        Prepod prepod = new Prepod();

        //пiдписуємо об'єкт класу Prepod на подiї Event1 та Event2
        za4et.Event1 += prepod.OnEvent1;
        za4et.Event2 += prepod.OnEvent2;

        //до об'єкта класу Za4et застосовується метод, який генерує подiю Event1 або подiю Event2
        za4et.ProcessResults();

        //якщо студент здав усi залiки
        if (za4et.SessionPassed)
        {
            //випадково створюються значення трьох числових змiнних
            int grade1 = random.Next(2, 6);
            int grade2 = random.Next(2, 6);
            int grade3 = random.Next(2, 6);           
            Console.WriteLine($"Оцінки: {grade1}, {grade2}, {grade3}");

            //об'єкт класу StudE
            StudE studentE = new StudE(grade1, grade2, grade3);

            //об'єкт класу Exam iз створеним об'єктом класу StudE як параметр
            Exam exam = new Exam(studentE);

            //пiдписуємо об'єкт класу Prepod на подiї Event3 та Event4
            exam.Event3 += prepod.OnEvent3;
            exam.Event4 += prepod.OnEvent4;

            //до об'єкта класу Exam застосовується метод, який генерує подiю Event3 або подiю Event4
            exam.ProcessResults();
        }
    }
}
