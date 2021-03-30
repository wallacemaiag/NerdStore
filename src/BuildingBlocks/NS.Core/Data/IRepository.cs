﻿using NS.Core.DomainObjects;
using System;

namespace NS.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnityOfWork unityOfWork { get; }
    }
}
