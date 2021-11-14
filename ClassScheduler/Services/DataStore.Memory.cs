using ClassScheduler.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassScheduler.Services
{
    public class MemoryClassSchedulerService : IClassSchedulerService
    {
        public MemoryClassSchedulerService()
        {
            Subjects = new()
            {
                new SubjectModel("数学", "AAA", "Math"),
                new SubjectModel("国語", "BBB", "Japanese"),
                new SubjectModel("英語", "CCC", "English"),
                new SubjectModel("化学", "DDD", "Science"),
            };

            var date = DateOnly.FromDateTime(DateTime.Now).AddDays(3);
            var date1 = DateOnly.FromDateTime(DateTime.Now).AddDays(7);

            Reports = new()
            {
                new ReportModel(Subjects[0], date, 1, false, Guid.NewGuid()),
                new ReportModel(Subjects[0], date, 2, false, Guid.NewGuid()),
                new ReportModel(Subjects[0], date, 3, false, Guid.NewGuid()),

                new ReportModel(Subjects[1], date, 1, false, Guid.NewGuid()),
                new ReportModel(Subjects[1], date, 2, false, Guid.NewGuid()),

                new ReportModel(Subjects[2], date, 1, false, Guid.NewGuid()),
                new ReportModel(Subjects[2], date, 2, false, Guid.NewGuid()),
                new ReportModel(Subjects[2], date, 3, false, Guid.NewGuid()),
                new ReportModel(Subjects[2], date, 4, false, Guid.NewGuid()),

                new ReportModel(Subjects[3], date, 1, false, Guid.NewGuid()),
            };

            Classes = new()
            {
                new ClassModel(date1, new TimeOnly(9, 0), new TimeOnly(9, 50), Subjects[0], Guid.NewGuid()),
                new ClassModel(date1, new TimeOnly(10, 0), new TimeOnly(10, 50), Subjects[1], Guid.NewGuid()),
                new ClassModel(date1, new TimeOnly(11, 0), new TimeOnly(11, 50), Subjects[2], Guid.NewGuid()),
                new ClassModel(date1, new TimeOnly(12, 0), new TimeOnly(12, 50), Subjects[3], Guid.NewGuid()),
            };
        }

        public List<SubjectModel> Subjects { get; }

        public List<ReportModel> Reports { get; }

        public List<ClassModel> Classes { get; }
    }

    public class MemorySubjectDataStore : ISubjectDataStore
    {
        private readonly MemoryClassSchedulerService _service;

        public MemorySubjectDataStore(IClassSchedulerService service)
        {
            _service = (MemoryClassSchedulerService)service;
        }

        public Task AddItem(SubjectModel subject)
        {
            _service.Subjects.Add(subject);
            return Task.CompletedTask;
        }

        public async IAsyncEnumerable<SubjectModel> GetSubjects()
        {
            foreach (var item in _service.Subjects)
            {
                await Task.Delay(10);
                yield return item;
            }
        }

        public async Task<SubjectModel> RefreshItem(SubjectModel subject)
        {
            for (int i = 0; i < _service.Subjects.Count; i++)
            {
                await Task.Delay(10);

                var item = _service.Subjects[i];
                if (item.Key == subject.Key)
                {
                    _service.Subjects[i] = item;
                    return item;
                }
            }

            return subject;
        }

        public async Task RemoveItem(SubjectModel subject)
        {
            for (int i = 0; i < _service.Subjects.Count; i++)
            {
                await Task.Delay(10);

                var item = _service.Subjects[i];
                if (item.Key == subject.Key)
                {
                    _service.Subjects.RemoveAt(i);
                    return;
                }
            }
        }
    }

    public class MemoryReportDataStore : IReportDataStore
    {
        private readonly MemoryClassSchedulerService _service;

        public MemoryReportDataStore(IClassSchedulerService service)
        {
            _service = (MemoryClassSchedulerService)service;
        }

        public Task<ReportModel> AddItem(ReportModel report)
        {
            report = report with
            {
                Id = Guid.NewGuid()
            };

            _service.Reports.Add(report);
            return Task.FromResult(report);
        }

        public async IAsyncEnumerable<ReportModel> GetAllReports()
        {
            foreach (var item in _service.Reports)
            {
                await Task.Delay(10);
                yield return item;
            }
        }

        public async Task<ReportModel> RefreshItem(ReportModel report)
        {
            for (int i = 0; i < _service.Reports.Count; i++)
            {
                await Task.Delay(10);

                var item = _service.Reports[i];
                if (item.Id == report.Id)
                {
                    _service.Reports[i] = item;
                    return item;
                }
            }

            return report;
        }

        public async Task RemoveItem(ReportModel report)
        {
            for (int i = 0; i < _service.Reports.Count; i++)
            {
                await Task.Delay(10);

                var item = _service.Reports[i];
                if (item.Id == report.Id)
                {
                    _service.Subjects.RemoveAt(i);
                    return;
                }
            }
        }
    }

    public class MemoryClassDataStore : IClassDataStore
    {
        private readonly MemoryClassSchedulerService _service;

        public MemoryClassDataStore(IClassSchedulerService service)
        {
            _service = (MemoryClassSchedulerService)service;
        }

        public Task<ClassModel> AddItem(ClassModel model)
        {
            model = model with
            {
                Id = Guid.NewGuid()
            };

            _service.Classes.Add(model);
            return Task.FromResult(model);
        }

        public async IAsyncEnumerable<ClassModel> GetAllClasses()
        {
            foreach (var item in _service.Classes)
            {
                await Task.Delay(10);
                yield return item;
            }
        }

        public async Task<ClassModel> RefreshItem(ClassModel model)
        {
            for (int i = 0; i < _service.Classes.Count; i++)
            {
                await Task.Delay(10);

                var item = _service.Classes[i];
                if (item.Id == model.Id)
                {
                    _service.Classes[i] = item;
                    return item;
                }
            }

            return model;
        }

        public async Task RemoveItem(ClassModel model)
        {
            for (int i = 0; i < _service.Classes.Count; i++)
            {
                await Task.Delay(10);

                var item = _service.Classes[i];
                if (item.Id == model.Id)
                {
                    _service.Classes.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
