﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Service
{
    public interface ISentenceTaskService
    {
        IEnumerable<SentenceTask> Get();

        SentenceTask GetById(int id);

        Task<SentenceTask> Edit(SentenceTask task);

        Task<SentenceTask> Add(SentenceTask task);

        Task<int> Delete(int id);
    }
}
