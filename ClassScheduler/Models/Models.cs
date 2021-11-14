using System;

namespace ClassScheduler.Models
{
    public record SubjectModel(string Name, string Teacher, string Key);

    public record ReportModel(SubjectModel Subject, DateOnly Expire, int Number, bool IsSubmitted, Guid Id = default)
    {
        public bool IsExpired => DateOnly.FromDateTime(DateTime.Now) < Expire;
    }

    public record ClassModel(DateOnly Date, TimeOnly Start, TimeOnly End, SubjectModel Subject, Guid Id);
}
