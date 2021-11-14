using ClassScheduler.Models;
using ClassScheduler.Services;

using Reactive.Bindings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassScheduler.ViewModels
{
    public class ClassesViewModel
    {
        private readonly IClassDataStore _service;

        public ClassesViewModel()
        {
            _service = ServiceProvider.GetService<IClassDataStore>();
            InitData();
            Refresh.Subscribe(() => InitData());
        }

        public ReactiveCollection<ClassModel> Classes { get; } = new();

        public ReactiveProperty<bool> IsRefreshing { get; } = new();

        public ReactiveCommand Refresh { get; } = new();

        public void InitData()
        {
            if (IsRefreshing.Value)
                return;

            Task.Run(async () =>
            {
                IsRefreshing.Value = true;
                Classes.ClearOnScheduler();

                await foreach (var item in _service.GetClasses())
                {
                    Classes.AddOnScheduler(item);
                }

                IsRefreshing.Value = false;
            });
        }
    }
}
