using System;
using System.Collections.Generic;

enum WeekDays
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
}

class Lesson
{
    public string Label { get; set; }
    public int Number { get; set; }
    public int WeekNumber { get; set; }
    public WeekDays WeekDay { get; set; }
    public string GroupNumber { get; set; }

    private static char currentLabel = 'A';

    public Lesson()
    {
        Label = currentLabel.ToString();
        currentLabel++;
        Number = new Random().Next(1, 4);
        WeekNumber = new Random().Next(0, 3);
        WeekDay = (WeekDays)new Random().Next(0, 5);
        GroupNumber = new Random().Next(11, 14).ToString();
    }

    public Lesson(string label, int number, int weekNumber, WeekDays weekDay, string groupNumber)
    {
        Label = label;
        Number = number;
        WeekNumber = weekNumber;
        WeekDay = weekDay;
        GroupNumber = groupNumber;
    }

    public override string ToString()
    {
        string russianWeekDay = GetRussianWeekDay(WeekDay);
        return $"{Label}, {Number} пара {russianWeekDay}, {GroupNumber} группа, {(WeekNumber == 0 ? "каждую неделю" : $"в {WeekNumber} неделю")}";
    }

    private string GetRussianWeekDay(WeekDays weekDay)
    {
        switch (weekDay)
        {
            case WeekDays.Monday:
                return "понедельник";
            case WeekDays.Tuesday:
                return "вторник";
            case WeekDays.Wednesday:
                return "среда";
            case WeekDays.Thursday:
                return "четверг";
            case WeekDays.Friday:
                return "пятница";
            default:
                return "неизвестный день недели";
        }
    }
}
class Schedule
{
    public string Name { get; set; }
    public List<Lesson> ListLesson { get; set; }

    public Schedule(string name)
    {
        Name = name;
        ListLesson = new List<Lesson>();
    }

    public void Add(Lesson lesson)
    {
        ListLesson.Add(lesson);
    }

    public void Sort(IComparer<Lesson> comparer)
    {
        ListLesson.Sort(comparer);
    }

    public override string ToString()
    {
        string result = "";
        int index = 1;

        foreach (var lesson in ListLesson)
        {
            result += $"{index}. {lesson.ToString()}\n";
            index++;
        }

        return result;
    }
}

class LabelComparer : IComparer<Lesson>
{
    public int Compare(Lesson x, Lesson y)
    {
        return x.Label.CompareTo(y.Label);
    }
}

class GroupComparer : IComparer<Lesson>
{
    public int Compare(Lesson x, Lesson y)
    {
        int result = x.GroupNumber.CompareTo(y.GroupNumber);
        if (result == 0)
        {
            result = x.WeekDay.CompareTo(y.WeekDay);
            if (result == 0)
            {
                result = x.Number.CompareTo(y.Number);
            }
        }
        return result;
    }
}

class WeekNumberComparer : IComparer<Lesson>
{
    public int Compare(Lesson x, Lesson y)
    {
        int result = x.WeekNumber.CompareTo(y.WeekNumber);
        if (result == 0)
        {
            result = x.WeekDay.CompareTo(y.WeekDay);
        }
        return result;
    }
}

class WeekDaysComparer : IComparer<Lesson>
{
    public int Compare(Lesson x, Lesson y)
    {
        int result = x.WeekDay.CompareTo(y.WeekDay);
        if (result == 0)
        {
            result = x.Number.CompareTo(y.Number);
        }
        return result;
    }
}

class MainClass
{
    public static void Main()
    {
        Lesson les1 = new Lesson();
        Lesson les2 = new Lesson();
        Lesson les3 = new Lesson();
        Lesson lesMat = new Lesson("Матан", 1, 0, WeekDays.Monday, "11");
        Lesson lesAlg = new Lesson("Алгебра", 2, 1, WeekDays.Monday, "11");
        Lesson lesAlgPract = new Lesson("Алгебра", 2, 1, WeekDays.Monday, "21");

        Schedule schedule = new Schedule("ФМиКН");
        schedule.Add(les3);
        schedule.Add(les2);
        schedule.Add(les1);
        schedule.Add(lesAlg);
        schedule.Add(lesAlgPract);
        schedule.Add(lesMat);
        Console.WriteLine(schedule);

        schedule.Sort(new WeekDaysComparer());

        Console.WriteLine(schedule);
    }
}
