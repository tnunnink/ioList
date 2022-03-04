﻿using System.Collections.Generic;
using ioList.Model;

namespace ioList.Abstractions
{
    public interface IListFileService
    {
        void Clear();
        IEnumerable<ListFile> GetAll();
        void Add(ListFile listFile);
        void Remove(ListFile listFile); 
    }
    
    
}