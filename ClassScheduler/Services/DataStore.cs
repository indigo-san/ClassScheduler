using ClassScheduler.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassScheduler.Services
{
    public interface IClassSchedulerService
    {
    }

    public interface ISubjectDataStore
    {
        IAsyncEnumerable<SubjectModel> GetSubjects();

        Task AddItem(SubjectModel subject);
        
        Task RemoveItem(SubjectModel subject);
        
        Task<SubjectModel> RefreshItem(SubjectModel subject);

        public async IAsyncEnumerable<SubjectModel> FindTeacher(string teacher)
        {
            await foreach (var item in GetSubjects())
            {
                if (item.Teacher == teacher)
                {
                    yield return item;
                }
            }
        }
    }

    public interface IClassDataStore
    {
        // 登録されているすべての授業を取得
        IAsyncEnumerable<ClassModel> GetAllClasses();

        // 今後の授業を取得
        public async IAsyncEnumerable<ClassModel> GetClasses()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var now = TimeOnly.FromDateTime(DateTime.Now);
            await foreach (var item in GetAllClasses())
            {
                if (item.Date >= today && item.Start > now)
                {
                    yield return item;
                }
            }
        }

        // 本日の授業を取得
        public async IAsyncEnumerable<ClassModel> GetTodaysClasses()
        {
            var now = DateOnly.FromDateTime(DateTime.Now);
            await foreach (var item in GetAllClasses())
            {
                if (item.Date == now)
                {
                    yield return item;
                }
            }
        }

        // 終わった授業を取得
        public async IAsyncEnumerable<ClassModel> GetFinishedClasses()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var now = TimeOnly.FromDateTime(DateTime.Now);
            await foreach (var item in GetAllClasses())
            {
                if (item.Date <= today && item.Start < now)
                {
                    yield return item;
                }
            }
        }

        // 授業を登録
        // Idが設定されているClassModelを返す
        Task<ClassModel> AddItem(ClassModel model);

        // ClassModelを更新
        Task<ClassModel> RefreshItem(ClassModel model);

        // ClassModelを削除
        Task RemoveItem(ClassModel model);
    }

    public interface IReportDataStore
    {
        // 期限切れを含めたすべてのレポートを返す
        IAsyncEnumerable<ReportModel> GetAllReports();

        // まだ期限のあるレポートを返す
        public async IAsyncEnumerable<ReportModel> GetReports()
        {
            await foreach (var item in GetAllReports())
            {
                if (!item.IsExpired)
                {
                    yield return item;
                }
            }
        }

        // 期限切れのレポートを返す
        public async IAsyncEnumerable<ReportModel> GetExpiredReports()
        {
            await foreach (var item in GetAllReports())
            {
                if (item.IsExpired)
                {
                    yield return item;
                }
            }
        }

        // レポートを登録
        // Idが設定されているReportModelを返す
        Task<ReportModel> AddItem(ReportModel report);

        // レポートを更新
        Task<ReportModel> RefreshItem(ReportModel report);

        // レポートを削除
        Task RemoveItem(ReportModel report);
    }
}
